<#
.SYNOPSIS
    Seeds the shared Firebase Realtime Database with Team 6 sample data.

.DESCRIPTION
    Pushes departments, employees, and attendance using relational IDs
    (DEP-XXXX, EMP-XXXX, ATT-XXXX), and creates the default admin login if no
    users exist yet.

    Credentials are read from src/IUIS.Core/Configuration/firebase.local.json
    rather than hard-coded here — this file is committed, that one is not.

.PARAMETER Force
    Skip the confirmation prompt. The database is shared by the whole class;
    this script DELETES the departments, employees, and attendance nodes before
    reseeding, so it asks first unless you pass -Force.

.EXAMPLE
    .\SeedFirebase.ps1
    .\SeedFirebase.ps1 -Force
#>
[CmdletBinding()]
param(
    [switch]$Force
)

$ErrorActionPreference = "Stop"

# ── Load configuration ────────────────────────────────────────────────────
$ConfigPath = Join-Path $PSScriptRoot "..\src\IUIS.Core\Configuration\firebase.local.json"

if (-not (Test-Path $ConfigPath)) {
    Write-Error @"
Could not find firebase.local.json at:
  $ConfigPath

Copy firebase.example.json to firebase.local.json and fill in the real values.
"@
    exit 1
}

$cfg = Get-Content $ConfigPath -Raw | ConvertFrom-Json

$ApiKey = $cfg.WebApiKey
$DbUrl  = $cfg.DatabaseUrl.TrimEnd('/')
$Email  = $cfg.ServiceEmail
$Pass   = $cfg.ServicePassword

# ── Confirm, because this wipes shared data ───────────────────────────────
if (-not $Force) {
    Write-Host ""
    Write-Host "  WARNING: this DELETES and reseeds these nodes:" -ForegroundColor Yellow
    Write-Host "    departments, employees, attendance" -ForegroundColor Yellow
    Write-Host "  Database: $DbUrl" -ForegroundColor Yellow
    Write-Host "  Anything your teammates added will be lost." -ForegroundColor Yellow
    Write-Host ""

    $answer = Read-Host "  Type 'yes' to continue"
    if ($answer -ne "yes") {
        Write-Host "Cancelled. Nothing was changed." -ForegroundColor Green
        exit 0
    }
}

# ── Step 1: Authenticate ──────────────────────────────────────────────────
Write-Host "Authenticating with Firebase..." -ForegroundColor Cyan
$authJson = @{ email = $Email; password = $Pass; returnSecureToken = $true } | ConvertTo-Json -Compress
$auth = Invoke-RestMethod -Uri "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=$ApiKey" -Method POST -Body $authJson -ContentType "application/json"
$token = $auth.idToken
Write-Host "Auth successful." -ForegroundColor Green

# ── Helpers ───────────────────────────────────────────────────────────────
function Put-Node($path, $key, $bodyObj) {
    $json = $bodyObj | ConvertTo-Json -Compress
    $null = Invoke-RestMethod -Uri "$DbUrl/$path/$key.json?auth=$token" -Method PUT -Body $json -ContentType "application/json"
    return $key
}

# ── Step 2: Clear old records ─────────────────────────────────────────────
Write-Host "Clearing departments, employees, attendance..." -ForegroundColor Cyan
foreach ($node in @("departments", "employees", "attendance")) {
    try {
        $null = Invoke-RestMethod -Uri "$DbUrl/$node.json?auth=$token" -Method DELETE
    } catch {
        Write-Warning "Failed to clear '$node'. Continuing..."
    }
}
Write-Host "Cleared." -ForegroundColor Green

# ── Step 3: Departments ───────────────────────────────────────────────────
Write-Host "Seeding departments..." -ForegroundColor Cyan

Put-Node "departments" "DEP-0001" @{ departmentName="Information Technology Office"; departmentHeadId="EMP-0002"; location="Admin Building, Room 101" } | Out-Null
Put-Node "departments" "DEP-0002" @{ departmentName="Registrar's Office";            departmentHeadId="EMP-0001"; location="Main Building, Ground Floor" } | Out-Null
Put-Node "departments" "DEP-0003" @{ departmentName="Finance and Accounting Office";  departmentHeadId="EMP-0003"; location="Admin Building, Room 202" } | Out-Null
Put-Node "departments" "DEP-0004" @{ departmentName="General Services";               departmentHeadId="EMP-0004"; location="Maintenance Building" } | Out-Null
Put-Node "departments" "DEP-0005" @{ departmentName="Human Resources Office";         departmentHeadId="";         location="Admin Building, Room 303" } | Out-Null

Write-Host "  5 departments seeded." -ForegroundColor Green

# ── Step 4: Employees ─────────────────────────────────────────────────────
Write-Host "Seeding employees..." -ForegroundColor Cyan

Put-Node "employees" "EMP-0001" @{ departmentId="DEP-0002"; firstName="Maria";  middleName="Santos"; lastName="Cruz";        position="Registrar Staff";          sex="Female"; birthDate="1990-05-12"; contactNumber="09171234567"; email="maria.cruz@bsu.edu.ph";        hourlyRate=250.0; dateHired="2015-06-01"; address="Batangas City"; employmentStatus="Active" } | Out-Null
Put-Node "employees" "EMP-0002" @{ departmentId="DEP-0001"; firstName="Juan";   middleName="Dela";   lastName="Cruz";        position="IT Technician";            sex="Male";   birthDate="1988-11-23"; contactNumber="09281234567"; email="juan.cruz@bsu.edu.ph";         hourlyRate=200.0; dateHired="2013-08-15"; address="Lipa City";     employmentStatus="Active" } | Out-Null
Put-Node "employees" "EMP-0003" @{ departmentId="DEP-0003"; firstName="Ana";    middleName="Reyes";  lastName="Lopez";       position="Finance Officer";          sex="Female"; birthDate="1985-02-14"; contactNumber="09391234567"; email="ana.lopez@bsu.edu.ph";         hourlyRate=300.0; dateHired="2010-02-20"; address="Tanauan City";  employmentStatus="Active" } | Out-Null
Put-Node "employees" "EMP-0004" @{ departmentId="DEP-0004"; firstName="Carlos"; middleName="Gomez";  lastName="Villanueva";  position="Utility Worker";           sex="Male";   birthDate="1993-07-08"; contactNumber="09451234567"; email="carlos.villanueva@bsu.edu.ph"; hourlyRate=120.0; dateHired="2020-09-01"; address="Batangas City"; employmentStatus="Active" } | Out-Null
Put-Node "employees" "EMP-0005" @{ departmentId="DEP-0002"; firstName="Rosa";   middleName="";       lastName="Dela Torre";  position="Administrative Assistant"; sex="Female"; birthDate="1992-04-30"; contactNumber="09561234567"; email="rosa.delatorre@bsu.edu.ph";    hourlyRate=180.0; dateHired="2018-04-10"; address="Lipa City";     employmentStatus="Inactive" } | Out-Null

Write-Host "  5 employees seeded." -ForegroundColor Green

# ── Step 5: Attendance ────────────────────────────────────────────────────
Write-Host "Seeding attendance..." -ForegroundColor Cyan

Put-Node "attendance" "ATT-0001" @{ employeeId="EMP-0001"; date="2026-07-13"; timeIn="08:00 AM"; timeOut="05:00 PM"; hoursRendered=9.0;  status="Present";  remarks="" } | Out-Null
Put-Node "attendance" "ATT-0002" @{ employeeId="EMP-0002"; date="2026-07-13"; timeIn="08:17 AM"; timeOut="05:00 PM"; hoursRendered=8.72; status="Late";     remarks="Arrived 17 minutes late" } | Out-Null
Put-Node "attendance" "ATT-0003" @{ employeeId="EMP-0003"; date="2026-07-13"; timeIn="";         timeOut="";         hoursRendered=0.0;  status="Absent";   remarks="No notification received" } | Out-Null
Put-Node "attendance" "ATT-0004" @{ employeeId="EMP-0004"; date="2026-07-13"; timeIn="08:00 AM"; timeOut="05:00 PM"; hoursRendered=9.0;  status="Present";  remarks="" } | Out-Null
Put-Node "attendance" "ATT-0005" @{ employeeId="EMP-0005"; date="2026-07-13"; timeIn="";         timeOut="";         hoursRendered=0.0;  status="On Leave"; remarks="Approved sick leave" } | Out-Null

Write-Host "  5 attendance records seeded." -ForegroundColor Green

# ── Step 6: Default login (only if no users exist) ────────────────────────
# Never clobber the users node: other teams' logins live here too.
Write-Host "Checking users node..." -ForegroundColor Cyan
$users = Invoke-RestMethod -Uri "$DbUrl/users.json?auth=$token" -Method GET

if ($null -eq $users) {
    Put-Node "users" "USR-0001" @{ username="admin"; password="admin"; role="Administrator" } | Out-Null
    Write-Host "  No users found — created default admin / admin." -ForegroundColor Green
} else {
    $count = @($users.PSObject.Properties).Count
    Write-Host "  $count user(s) already exist — left untouched." -ForegroundColor Green
}

Write-Host ""
Write-Host "Seeding complete." -ForegroundColor Yellow
