using System.Text;
using IUIS.Core.Models;
using IUIS.Core.Services;
using IUIS.Core.Session;

namespace IUIS.Modules.Team6.Forms
{
    /// <summary>
    /// Team 6's main window: employee records, departments, attendance logging,
    /// and attendance reporting across four tabs.
    /// </summary>
    public partial class EmployeeAttendanceForm : Form
    {
        /// <summary>Who opened the module. Null when the designer hosts the form.</summary>
        private readonly UserSession? _session;

        // Services
        private readonly EmployeeService _employeeService = new();
        private readonly DepartmentService _departmentService = new();
        private readonly AttendanceService _attendanceService = new();

        // Cached full data
        private List<Employee> _employees = [];
        private List<Department> _departments = [];
        private List<AttendanceRecord> _attendance = [];
        private List<AttendanceRecord> _reportRecords = [];

        // Currently filtered rows 
        private List<Employee> _filteredEmployees = [];
        private List<Department> _filteredDepartments = [];
        private List<AttendanceRecord> _filteredAttendance = [];

        // Selected objects
        private Employee? _selectedEmployee;
        private Department? _selectedDepartment;
        private AttendanceRecord? _selectedAttendance;

        // Status bar auto-clear timer
        private readonly System.Windows.Forms.Timer _statusTimer = new() { Interval = 3000 };

        // Sort state per grid: (column index, ascending)
        private (int Col, bool Asc) _empSort = (-1, true);
        private (int Col, bool Asc) _deptSort = (-1, true);
        private (int Col, bool Asc) _attSort = (-1, true);
        private (int Col, bool Asc) _rptSort = (-1, true);

        /// <summary>
        /// Parameterless constructor, required by the Windows Forms designer.
        /// At runtime the shell uses the <see cref="UserSession"/> overload instead.
        /// </summary>
        public EmployeeAttendanceForm() : this(null)
        {
        }

        /// <param name="session">The signed-in user, or null when hosted by the designer.</param>
        public EmployeeAttendanceForm(UserSession? session)
        {
            _session = session;

            InitializeComponent();

            _statusTimer.Tick += (s, e) =>
            {
                _statusTimer.Stop();
                statusBarLabel.Text = "Ready";
                statusBarLabel.ForeColor = SystemColors.ControlText;
            };

            // Make the launch context obvious at a glance: either the session
            // carried over from the dashboard, or this is a standalone dev run.
            Text = _session is not null
                ? $"{Text} — signed in as {_session.Username} ({_session.Role})"
                : $"{Text} — standalone (no login)";
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DTP_AttDate.MaxDate = DateTime.Today;
            WireEvents();
            await LoadAllDataAsync();
        }

        //  EVENT WIRING

        private void WireEvents()
        {
            SetupButtonAppearances();

            // Make every DataGridView select the full row on any cell click
            foreach (DataGridView dgv in new[] {
                DGV_EmployeeList, DGV_DepartmentList, DGV_AttendanceList, DGV_ReportList })
            {
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.MultiSelect = false;
                dgv.ReadOnly = true;
            }

            // ── Refresh buttons ──
            empRefreshBtn.Click += async (s, e) => await LoadEmployeesAsync();
            deptRefreshBtn.Click += async (s, e) => await LoadDepartmentsAsync();

            // Employee Tab
            TB_Search.TextChanged += (s, e) => FilterEmployees();
            CB_Department.SelectedIndexChanged += (s, e) => FilterEmployees();
            CB_Status.SelectedIndexChanged += (s, e) => FilterEmployees();
            clearFilterButton.Click += (s, e) => ClearEmployeeFilters();
            DGV_EmployeeList.SelectionChanged += DGV_EmployeeList_SelectionChanged;
            DGV_EmployeeList.ColumnHeaderMouseClick += DGV_EmployeeList_ColumnHeaderMouseClick;
            addEmployeeButton.Click += async (s, e) => await AddEmployeeAsync();
            editEmployeeButton.Click += async (s, e) => await EditEmployeeAsync();
            deleteEmployeeButton.Click += async (s, e) => await DeleteEmployeeAsync();
            viewEmployeeDetailsButton.Click += ViewEmployeeDetails;

            // Department Tab
            TB_DeptSearch.TextChanged += (s, e) => FilterDepartments();
            CB_Location.SelectedIndexChanged += (s, e) => FilterDepartments();
            clearDeptFilterButton.Click += (s, e) => ClearDepartmentFilters();
            DGV_DepartmentList.SelectionChanged += DGV_DepartmentList_SelectionChanged;
            DGV_DepartmentList.ColumnHeaderMouseClick += DGV_DepartmentList_ColumnHeaderMouseClick;
            addDepartmentButton.Click += async (s, e) => await AddDepartmentAsync();
            editDepartmentButton.Click += async (s, e) => await EditDepartmentAsync();
            deleteDepartmentButton.Click += async (s, e) => await DeleteDepartmentAsync();

            // Attendance Tab
            DTP_AttDate.ValueChanged += async (s, e) => await LoadAttendanceByDateAsync();
            TB_AttSearch.TextChanged += (s, e) => FilterAttendance();
            CB_AttDepartment.SelectedIndexChanged += (s, e) => FilterAttendance();
            CB_AttStatus.SelectedIndexChanged += (s, e) => FilterAttendance();
            DGV_AttendanceList.SelectionChanged += DGV_AttendanceList_SelectionChanged;
            DGV_AttendanceList.ColumnHeaderMouseClick += DGV_AttendanceList_ColumnHeaderMouseClick;
            recordTimeInButton.Click += async (s, e) => await RecordTimeInAsync();
            recordTimeOutButton.Click += async (s, e) => await RecordTimeOutAsync();
            markAbsentLeaveButton.Click += async (s, e) => await MarkAbsentOrLeaveAsync();

            // Reports Tab
            generateReportButton.Click += async (s, e) => await GenerateReportAsync();
            exportCSVButton.Click += ExportToCSV;
            DGV_ReportList.ColumnHeaderMouseClick += DGV_ReportList_ColumnHeaderMouseClick;
        }

        private void SetupButtonAppearances()
        {
            // Set explicit foreground colors to prevent inheriting parent styles
            clearDeptFilterButton.ForeColor = Color.Black;
            exportCSVButton.ForeColor = Color.Black;

            void ConfigureDynamicButton(Button btn, Color enabledBack, Color enabledFore)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Tag = (enabledBack, enabledFore);

                // Initialize with current enabled state
                ApplyDynamicButtonStyle(btn);

                // Register event handler to update when Enabled changes
                btn.EnabledChanged += (s, e) => ApplyDynamicButtonStyle(btn);
            }

            // Configure all custom styled buttons that change enabled states dynamically
            ConfigureDynamicButton(viewEmployeeDetailsButton, Color.FromArgb(55, 65, 81), SystemColors.ControlLightLight);
            ConfigureDynamicButton(deleteEmployeeButton, Color.FromArgb(176, 0, 26), SystemColors.ControlLightLight);
            ConfigureDynamicButton(editEmployeeButton, Color.FromArgb(255, 185, 44), Color.Black);

            ConfigureDynamicButton(deleteDepartmentButton, Color.FromArgb(176, 0, 26), SystemColors.ControlLightLight);
            ConfigureDynamicButton(editDepartmentButton, Color.FromArgb(255, 185, 44), Color.Black);

            ConfigureDynamicButton(recordTimeOutButton, Color.FromArgb(255, 185, 44), Color.Black);
        }

        private void ApplyDynamicButtonStyle(Button btn)
        {
            if (btn.Tag is (Color enabledBack, Color enabledFore))
            {
                if (btn.Enabled)
                {
                    btn.BackColor = enabledBack;
                    btn.ForeColor = enabledFore;
                }
                else
                {
                    // Modern muted colors for disabled buttons:
                    // Soft light-gray background with a muted gray text to ensure high contrast and clear readability
                    btn.BackColor = Color.FromArgb(229, 231, 235); // Gray-200
                    btn.ForeColor = Color.FromArgb(156, 163, 175); // Gray-400
                }
            }
        }


        // ── Initial load ──────────────────────────────────────────────────

        private async Task LoadAllDataAsync()
        {
            try
            {
                SetStatus("Connecting to Firebase…");
                // Establish anonymous Firebase auth before any data operations
                await FirebaseAuthService.Instance.GetTokenAsync();
            }
            catch (Exception ex)
            {
                SetStatusError($"Authentication failed: {ex.Message}");
                ShowError("authenticating with Firebase", ex);
                return; // Don't attempt data loads if auth failed
            }

            try
            {
                SetStatus("Loading employees and departments…");
                // Load both datasets concurrently to populate the in-memory cache lists first
                var deptsTask = _departmentService.GetAllAsync();
                var empsTask = _employeeService.GetAllAsync();
                
                await Task.WhenAll(deptsTask, empsTask);

                _departments = deptsTask.Result;
                _employees = empsTask.Result;

                // Bind UI elements now that both caches are ready
                SetupDepartmentUIElements();
                SetupEmployeeUIElements();
                InitReportFilters();
                await LoadAttendanceByDateAsync();
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to load data: {ex.Message}");
                ShowError("loading initial data", ex);
            }
        }

        // ═══════════════════════════════════════════════════════════════════
        //  EMPLOYEES TAB
        // ═══════════════════════════════════════════════════════════════════

        private void SetupEmployeeUIElements()
        {
            CB_Department.SelectedIndexChanged -= (s, e) => FilterEmployees();
            CB_Status.SelectedIndexChanged -= (s, e) => FilterEmployees();

            CB_Department.Items.Clear();
            CB_Department.Items.Add("All");
            foreach (var dept in _departments.Select(d => d.Name).OrderBy(n => n))
                CB_Department.Items.Add(dept);
            CB_Department.SelectedIndex = 0;

            CB_Status.Items.Clear();
            CB_Status.Items.AddRange(new object[] { "All", "Active", "Inactive", "Resigned", "Terminated", "Retired" });
            CB_Status.SelectedIndex = 0;

            CB_Department.SelectedIndexChanged += (s, e) => FilterEmployees();
            CB_Status.SelectedIndexChanged += (s, e) => FilterEmployees();

            RefreshEmployeeGrid(_employees);
        }

        private async Task LoadEmployeesAsync()
        {
            try
            {
                SetStatus("Refreshing employees…");
                _employees = await _employeeService.GetAllAsync();
                SetupEmployeeUIElements();
                SetStatusSuccess("Employees refreshed.");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to load employees: {ex.Message}");
                ShowError("loading employees", ex);
            }
        }

        private void RefreshEmployeeGrid(List<Employee> source)
        {
            _filteredEmployees = source;

            DGV_EmployeeList.SelectionChanged -= DGV_EmployeeList_SelectionChanged;

            DGV_EmployeeList.DataSource = source.Select(e => new
            {
                EmpID = e.EmployeeId,
                FullName = e.FullName,
                Position = e.Position,
                Department = _departments.FirstOrDefault(d => d.DepartmentId == e.DepartmentId)?.Name ?? "(None)",
                Status = e.EmploymentStatus
            }).ToList();

            totalRecordValueLabel.Text = source.Count.ToString();
            DGV_EmployeeList.ClearSelection();
            DGV_EmployeeQuickDetails.DataSource = null;
            _selectedEmployee = null;
            SetEmployeeButtons(false);

            DGV_EmployeeList.SelectionChanged += DGV_EmployeeList_SelectionChanged;
        }

        private void FilterEmployees()
        {
            var search = TB_Search.Text.ToLower().Trim();
            var deptName = CB_Department.SelectedItem?.ToString();
            var status = CB_Status.SelectedItem?.ToString();

            var q = _employees.AsEnumerable();

            if (!string.IsNullOrEmpty(search))
                q = q.Where(e =>
                    e.FullName.ToLower().Contains(search) ||
                    e.EmployeeId.ToLower().Contains(search) ||
                    e.Position.ToLower().Contains(search));

            if (!string.IsNullOrEmpty(deptName) && deptName != "All")
            {
                var deptObj = _departments.FirstOrDefault(d => d.Name == deptName);
                var deptId = deptObj?.DepartmentId ?? string.Empty;
                q = q.Where(e => e.DepartmentId == deptId);
            }

            if (!string.IsNullOrEmpty(status) && status != "All")
                q = q.Where(e => e.EmploymentStatus == status);

            RefreshEmployeeGrid(q.ToList());
        }

        private void ClearEmployeeFilters()
        {
            TB_Search.Clear();
            if (CB_Department.Items.Count > 0) CB_Department.SelectedIndex = 0;
            if (CB_Status.Items.Count > 0) CB_Status.SelectedIndex = 0;
        }

        private void DGV_EmployeeList_SelectionChanged(object? sender, EventArgs e)
        {
            if (DGV_EmployeeList.SelectedRows.Count == 0)
            {
                DGV_EmployeeQuickDetails.DataSource = null;
                _selectedEmployee = null;
                SetEmployeeButtons(false);
                return;
            }

            var idx = DGV_EmployeeList.SelectedRows[0].Index;
            if (idx < 0 || idx >= _filteredEmployees.Count) return;

            _selectedEmployee = _filteredEmployees[idx];

            DGV_EmployeeQuickDetails.DataSource = new[]
            {
                new
                {
                    DateHired = _selectedEmployee.DateHired,
                    Email     = _selectedEmployee.Email,
                    Phone     = _selectedEmployee.ContactNumber,
                    Address   = _selectedEmployee.Address
                }
            };

            SetEmployeeButtons(true);
        }

        private void SetEmployeeButtons(bool on)
        {
            editEmployeeButton.Enabled = on;
            deleteEmployeeButton.Enabled = on;
            viewEmployeeDetailsButton.Enabled = on;
        }

        private async Task AddEmployeeAsync()
        {
            using var form = new EmployeeForm("Add Employee", _departments);
            if (form.ShowDialog(this) == DialogResult.OK && form.Employee != null)
            {
                try
                {
                    SetStatus("Saving new employee…");
                    await _employeeService.AddAsync(form.Employee);
                    await LoadEmployeesAsync();
                    SetStatusSuccess("Employee added successfully.");
                    ShowInfo("Employee added successfully.");
                }
                catch (Exception ex)
                {
                    SetStatusError($"Failed to add employee: {ex.Message}");
                    ShowError("adding employee", ex);
                }
            }
        }

        private async Task EditEmployeeAsync()
        {
            if (_selectedEmployee == null) return;
            using var form = new EmployeeForm("Edit Employee", _departments, _selectedEmployee);
            if (form.ShowDialog(this) == DialogResult.OK && form.Employee != null)
            {
                try
                {
                    SetStatus("Saving employee changes…");
                    await _employeeService.UpdateAsync(form.Employee);
                    await LoadEmployeesAsync();
                    SetStatusSuccess("Employee updated successfully.");
                    ShowInfo("Employee updated successfully.");
                }
                catch (Exception ex)
                {
                    SetStatusError($"Failed to update employee: {ex.Message}");
                    ShowError("updating employee", ex);
                }
            }
        }

        private async Task DeleteEmployeeAsync()
        {
            if (_selectedEmployee == null) return;
            if (!Confirm($"Delete {_selectedEmployee.FullName}? This cannot be undone.")) return;
            try
            {
                SetStatus($"Deleting {_selectedEmployee.FullName}…");
                // Cascade: clear department head reference if this employee was the head
                var headDepts = _departments.Where(d => d.DepartmentHeadId == _selectedEmployee.EmployeeId).ToList();
                foreach (var d in headDepts)
                {
                    d.DepartmentHeadId = string.Empty;
                    await _departmentService.UpdateAsync(d);
                }

                await _employeeService.DeleteAsync(_selectedEmployee.EmployeeId);
                await LoadDepartmentsAsync(); // reload to refresh grid display of head
                await LoadEmployeesAsync();
                SetStatusSuccess("Employee deleted.");
                ShowInfo("Employee deleted.");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to delete employee: {ex.Message}");
                ShowError("deleting employee", ex);
            }
        }

        private void ViewEmployeeDetails(object? sender, EventArgs e)
        {
            if (_selectedEmployee == null) return;

            var deptName = _departments.FirstOrDefault(d => d.DepartmentId == _selectedEmployee.DepartmentId)?.Name ?? "(None)";

            var sb = new StringBuilder();
            sb.AppendLine($"Employee ID : {_selectedEmployee.EmployeeId}");
            sb.AppendLine($"Full Name   : {_selectedEmployee.FullName}");
            sb.AppendLine($"Sex         : {_selectedEmployee.Sex}");
            sb.AppendLine($"Birth Date  : {_selectedEmployee.BirthDate}");
            sb.AppendLine($"Position    : {_selectedEmployee.Position}");
            sb.AppendLine($"Department  : {deptName}");
            sb.AppendLine($"Hourly Rate : Php {_selectedEmployee.HourlyRate:F2}");
            sb.AppendLine($"Status      : {_selectedEmployee.EmploymentStatus}");
            sb.AppendLine($"Date Hired  : {_selectedEmployee.DateHired}");
            sb.AppendLine($"Email       : {_selectedEmployee.Email}");
            sb.AppendLine($"Phone       : {_selectedEmployee.ContactNumber}");
            sb.AppendLine($"Address     : {_selectedEmployee.Address}");

            MessageBox.Show(sb.ToString(),
                $"Details — {_selectedEmployee.FullName}",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //  DEPARTMENTS

        private void SetupDepartmentUIElements()
        {
            CB_Location.SelectedIndexChanged -= (s, e) => FilterDepartments();
            CB_Location.Items.Clear();
            CB_Location.Items.Add("All");
            foreach (var loc in _departments.Select(d => d.Location)
                                            .Where(l => !string.IsNullOrEmpty(l))
                                            .Distinct().OrderBy(l => l))
                CB_Location.Items.Add(loc);
            CB_Location.SelectedIndex = 0;
            CB_Location.SelectedIndexChanged += (s, e) => FilterDepartments();

            RefreshDepartmentGrid(_departments);
        }

        private async Task LoadDepartmentsAsync()
        {
            try
            {
                SetStatus("Refreshing departments…");
                _departments = await _departmentService.GetAllAsync();
                SetupDepartmentUIElements();
                SetStatusSuccess("Departments refreshed.");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to load departments: {ex.Message}");
                ShowError("loading departments", ex);
            }
        }

        private void RefreshDepartmentGrid(List<Department> source)
        {
            _filteredDepartments = source;

            DGV_DepartmentList.SelectionChanged -= DGV_DepartmentList_SelectionChanged;

            var empCounts = _employees
                .Where(e => !string.IsNullOrEmpty(e.DepartmentId))
                .GroupBy(e => e.DepartmentId)
                .ToDictionary(g => g.Key, g => g.Count());

            DGV_DepartmentList.DataSource = source.Select(d => new
            {
                ID = d.DepartmentId,
                DepartmentName = d.Name,
                Head = _employees.FirstOrDefault(e => e.EmployeeId == d.DepartmentHeadId)?.FullName ?? "(None)",
                Location = d.Location,
                Employees = empCounts.TryGetValue(d.DepartmentId, out var cnt) ? cnt : 0
            }).ToList();

            totalDeptsValueLabel.Text = source.Count.ToString();
            DGV_DepartmentList.ClearSelection();
            DGV_DepartmentQuickDetails.DataSource = null;
            _selectedDepartment = null;
            SetDepartmentButtons(false);

            DGV_DepartmentList.SelectionChanged += DGV_DepartmentList_SelectionChanged;
        }

        private void FilterDepartments()
        {
            var search = TB_DeptSearch.Text.ToLower().Trim();
            var location = CB_Location.SelectedItem?.ToString();

            var q = _departments.AsEnumerable();

            if (!string.IsNullOrEmpty(search))
                q = q.Where(d =>
                {
                    var headName = _employees.FirstOrDefault(e => e.EmployeeId == d.DepartmentHeadId)?.FullName ?? "";
                    return d.Name.ToLower().Contains(search) ||
                           headName.ToLower().Contains(search) ||
                           d.Location.ToLower().Contains(search);
                });

            if (!string.IsNullOrEmpty(location) && location != "All")
                q = q.Where(d => d.Location == location);

            RefreshDepartmentGrid(q.ToList());
        }

        private void ClearDepartmentFilters()
        {
            TB_DeptSearch.Clear();
            if (CB_Location.Items.Count > 0) CB_Location.SelectedIndex = 0;
        }

        private void DGV_DepartmentList_SelectionChanged(object? sender, EventArgs e)
        {
            if (DGV_DepartmentList.SelectedRows.Count == 0)
            {
                DGV_DepartmentQuickDetails.DataSource = null;
                deptQuickDetailsLabel.Text = "Employees in this Department";
                _selectedDepartment = null;
                SetDepartmentButtons(false);
                return;
            }

            var idx = DGV_DepartmentList.SelectedRows[0].Index;
            if (idx < 0 || idx >= _filteredDepartments.Count) return;

            _selectedDepartment = _filteredDepartments[idx];

            // Show employees belonging to the selected department
            var deptEmployees = _employees
                .Where(e => e.DepartmentId == _selectedDepartment.DepartmentId)
                .Select(e => new
                {
                    FullName = e.FullName,
                    Position = e.Position,
                    Status = e.EmploymentStatus,
                    Email = e.Email
                }).ToList();

            deptQuickDetailsLabel.Text = deptEmployees.Count == 0
                ? "Employees in this Department (none assigned)"
                : $"Employees in this Department ({deptEmployees.Count})";

            DGV_DepartmentQuickDetails.DataSource = deptEmployees;

            SetDepartmentButtons(true);
        }

        private void SetDepartmentButtons(bool on)
        {
            editDepartmentButton.Enabled = on;
            deleteDepartmentButton.Enabled = on;
        }

        private async Task AddDepartmentAsync()
        {
            using var form = new DepartmentForm("Add Department", _employees);
            if (form.ShowDialog(this) == DialogResult.OK && form.Department != null)
            {
                try
                {
                    SetStatus("Saving new department…");
                    await _departmentService.AddAsync(form.Department);
                    await LoadDepartmentsAsync();
                    SetStatusSuccess("Department added successfully.");
                    ShowInfo("Department added successfully.");
                }
                catch (Exception ex)
                {
                    SetStatusError($"Failed to add department: {ex.Message}");
                    ShowError("adding department", ex);
                }
            }
        }

        private async Task EditDepartmentAsync()
        {
            if (_selectedDepartment == null) return;
            using var form = new DepartmentForm("Edit Department", _employees, _selectedDepartment);
            if (form.ShowDialog(this) == DialogResult.OK && form.Department != null)
            {
                try
                {
                    SetStatus("Saving department changes…");
                    await _departmentService.UpdateAsync(form.Department);
                    await LoadDepartmentsAsync();
                    SetStatusSuccess("Department updated successfully.");
                    ShowInfo("Department updated successfully.");
                }
                catch (Exception ex)
                {
                    SetStatusError($"Failed to update department: {ex.Message}");
                    ShowError("updating department", ex);
                }
            }
        }

        private async Task DeleteDepartmentAsync()
        {
            if (_selectedDepartment == null) return;
            if (!Confirm($"Delete department '{_selectedDepartment.Name}'? This cannot be undone.")) return;
            try
            {
                SetStatus($"Deleting department '{_selectedDepartment.Name}'…");
                // Cascade: clear departmentId for employees in this department
                var deptEmps = _employees.Where(e => e.DepartmentId == _selectedDepartment.DepartmentId).ToList();
                foreach (var emp in deptEmps)
                {
                    emp.DepartmentId = string.Empty;
                    await _employeeService.UpdateAsync(emp);
                }

                await _departmentService.DeleteAsync(_selectedDepartment.DepartmentId);
                await LoadEmployeesAsync(); // reload to refresh employee list display of department
                await LoadDepartmentsAsync();
                SetStatusSuccess("Department deleted.");
                ShowInfo("Department deleted.");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to delete department: {ex.Message}");
                ShowError("deleting department", ex);
            }
        }

        // ═══════════════════════════════════════════════════════════════════
        //  ATTENDANCE TAB
        // ═══════════════════════════════════════════════════════════════════

        private async Task LoadAttendanceByDateAsync()
        {
            try
            {
                var date = DTP_AttDate.Value.ToString("yyyy-MM-dd");
                SetStatus($"Loading attendance for {date}…");
                var rawAttendance = await _attendanceService.GetByDateAsync(date);
                foreach (var r in rawAttendance)
                {
                    r.EmployeeName = _employees.FirstOrDefault(e => e.EmployeeId == r.EmployeeId)?.FullName ?? "(Unknown)";
                }
                _attendance = rawAttendance;

                CB_AttDepartment.Items.Clear();
                CB_AttDepartment.Items.Add("All");
                foreach (var d in _departments)
                    CB_AttDepartment.Items.Add(d.Name);
                CB_AttDepartment.SelectedIndex = 0;

                CB_AttStatus.Items.Clear();
                CB_AttStatus.Items.AddRange(new object[] { "All", "Present", "Late", "Absent", "On Leave" });
                CB_AttStatus.SelectedIndex = 0;

                RefreshAttendanceGrid(_attendance);
                SetStatusSuccess($"Attendance loaded — {_attendance.Count} records for {date}.");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to load attendance: {ex.Message}");
                ShowError("loading attendance", ex);
            }
        }

        private void RefreshAttendanceGrid(List<AttendanceRecord> source)
        {
            _filteredAttendance = source;

            DGV_AttendanceList.SelectionChanged -= DGV_AttendanceList_SelectionChanged;

            DGV_AttendanceList.DataSource = source.Select(r => new
            {
                EmpID = r.EmployeeId,
                FullName = r.EmployeeName,
                TimeIn = r.TimeIn,
                TimeOut = r.TimeOut,
                Hours = r.TotalHours > 0 ? $"{r.TotalHours:F2} h" : "—",
                Status = r.Status
            }).ToList();

            summaryPresentLabel.Text = $"Present: {source.Count(r => r.Status == "Present")}";
            summaryLateLabel.Text = $"Late: {source.Count(r => r.Status == "Late")}";
            summaryAbsentLabel.Text = $"Absent: {source.Count(r => r.Status == "Absent")}";
            summaryOnLeaveLabel.Text = $"On Leave: {source.Count(r => r.Status == "On Leave")}";

            DGV_AttendanceList.ClearSelection();
            _selectedAttendance = null;
            recordTimeOutButton.Enabled = false;

            DGV_AttendanceList.SelectionChanged += DGV_AttendanceList_SelectionChanged;
        }

        private void FilterAttendance()
        {
            var search = TB_AttSearch.Text.ToLower().Trim();
            var deptName = CB_AttDepartment.SelectedItem?.ToString();
            var status = CB_AttStatus.SelectedItem?.ToString();

            var q = _attendance.AsEnumerable();

            if (!string.IsNullOrEmpty(search))
                q = q.Where(r =>
                    r.EmployeeName.ToLower().Contains(search) ||
                    r.EmployeeId.ToLower().Contains(search));

            if (!string.IsNullOrEmpty(deptName) && deptName != "All")
            {
                var deptObj = _departments.FirstOrDefault(d => d.Name == deptName);
                var deptId = deptObj?.DepartmentId ?? string.Empty;
                var inDept = _employees
                    .Where(e => e.DepartmentId == deptId)
                    .Select(e => e.EmployeeId)
                    .ToHashSet();
                q = q.Where(r => inDept.Contains(r.EmployeeId));
            }

            if (!string.IsNullOrEmpty(status) && status != "All")
                q = q.Where(r => r.Status == status);

            RefreshAttendanceGrid(q.ToList());
        }

        private void DGV_AttendanceList_SelectionChanged(object? sender, EventArgs e)
        {
            if (DGV_AttendanceList.SelectedRows.Count == 0)
            {
                _selectedAttendance = null;
                recordTimeOutButton.Enabled = false;
                return;
            }

            var idx = DGV_AttendanceList.SelectedRows[0].Index;
            if (idx < 0 || idx >= _filteredAttendance.Count) return;

            _selectedAttendance = _filteredAttendance[idx];

            // Enable Time Out only when Time In exists but Time Out does not
            recordTimeOutButton.Enabled =
                !string.IsNullOrEmpty(_selectedAttendance.TimeIn) &&
                string.IsNullOrEmpty(_selectedAttendance.TimeOut);
        }

        private async Task RecordTimeInAsync()
        {
            if (DTP_AttDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Cannot record attendance for a future date.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var active = _employees.Where(e => e.EmploymentStatus == "Active").ToList();
            if (active.Count == 0)
            {
                ShowInfo("No active employees found. Please add employees first.");
                return;
            }

            using var sel = new SelectEmployeeForm(active);
            if (sel.ShowDialog(this) != DialogResult.OK || sel.SelectedEmployee == null) return;

            var employee = sel.SelectedEmployee;
            var date = DTP_AttDate.Value.ToString("yyyy-MM-dd");

            var existing = await _attendanceService.GetByEmployeeAndDateAsync(employee.EmployeeId, date);
            if (existing != null)
            {
                MessageBox.Show($"{employee.FullName} already has an attendance record for {date}.",
                    "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var now = DateTime.Now;
            var timeIn = now.ToString("hh:mm tt");
            // Late if recorded after 08:15 AM
            var status = (now.Hour > 8 || (now.Hour == 8 && now.Minute > 15)) ? "Late" : "Present";

            try
            {
                SetStatus($"Recording time in for {employee.FullName}…");
                await _attendanceService.AddAsync(new AttendanceRecord
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.FullName,
                    Date = date,
                    TimeIn = timeIn,
                    Status = status
                });
                await LoadAttendanceByDateAsync();
                SetStatusSuccess($"Time In recorded for {employee.FullName} at {timeIn} ({status}).");
                ShowInfo($"Time In recorded for {employee.FullName} at {timeIn} ({status}).");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to record Time In: {ex.Message}");
                ShowError("recording Time In", ex);
            }
        }

        private async Task RecordTimeOutAsync()
        {
            if (_selectedAttendance == null) return;

            // Re-fetch from Firebase to guarantee RecordId is populated.
            // _selectedAttendance may have been rebuilt from the grid without a valid key.
            var fresh = await _attendanceService.GetByEmployeeAndDateAsync(
                _selectedAttendance.EmployeeId,
                _selectedAttendance.Date);

            if (fresh == null)
            {
                ShowError("recording Time Out", new Exception("Attendance record not found in database. Please reload."));
                return;
            }

            if (string.IsNullOrEmpty(fresh.RecordId))
            {
                ShowError("recording Time Out", new Exception("Record ID is missing. Please reload the attendance list and try again."));
                return;
            }

            var now = DateTime.Now;
            var timeOut = now.ToString("hh:mm tt");

            // Calculate hours worked using the stored date + timeIn string
            if (DateTime.TryParse($"{fresh.Date} {fresh.TimeIn}", out var timeInDt))
                fresh.TotalHours = Math.Round(Math.Max(0, (now - timeInDt).TotalHours), 2);

            fresh.TimeOut = timeOut;

            // Auto-set Half-Day status if less than 4 hours were rendered
            if (fresh.TotalHours > 0 && fresh.TotalHours < 4)
                fresh.Status = "Half-Day";

            try
            {
                SetStatus("Recording time out…");
                await _attendanceService.UpdateAsync(fresh);
                await LoadAttendanceByDateAsync();
                SetStatusSuccess($"Time Out recorded at {timeOut}. Total: {fresh.TotalHours:F2} hrs ({fresh.Status}).");
                ShowInfo($"Time Out recorded at {timeOut}. Total: {fresh.TotalHours:F2} hrs ({fresh.Status}).");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to record Time Out: {ex.Message}");
                ShowError("recording Time Out", ex);
            }
        }

        private async Task MarkAbsentOrLeaveAsync()
        {
            if (DTP_AttDate.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Cannot record attendance/leave for a future date.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var active = _employees.Where(e => e.EmploymentStatus == "Active").ToList();
            if (active.Count == 0) { ShowInfo("No active employees found."); return; }

            using var sel = new SelectEmployeeForm(active);
            if (sel.ShowDialog(this) != DialogResult.OK || sel.SelectedEmployee == null) return;

            var employee = sel.SelectedEmployee;
            var date = DTP_AttDate.Value.ToString("yyyy-MM-dd");

            var existing = await _attendanceService.GetByEmployeeAndDateAsync(employee.EmployeeId, date);
            if (existing != null)
            {
                MessageBox.Show($"{employee.FullName} already has a record for {date}.",
                    "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Custom dialog with clearly-named buttons instead of Yes=Absent / No=On Leave
            string? status = null;
            using (var statusDlg = new Form())
            {
                statusDlg.Text = $"Mark Status — {employee.FullName}";
                statusDlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                statusDlg.MaximizeBox = false; statusDlg.MinimizeBox = false;
                statusDlg.StartPosition = FormStartPosition.CenterParent;
                statusDlg.Width = 320; statusDlg.Height = 130;

                var lbl = new Label
                {
                    Text = "Select the status to record:",
                    Location = new Point(12, 14),
                    AutoSize = true
                };
                var btnAbsent = new Button
                {
                    Text = "Mark Absent",
                    Location = new Point(12, 44),
                    Width = 120,
                    Height = 34
                };
                var btnLeave = new Button
                {
                    Text = "Mark On Leave",
                    Location = new Point(144, 44),
                    Width = 120,
                    Height = 34
                };
                var btnCancel = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(200, 88),
                    Width = 70
                };

                btnAbsent.Click += (_, _) => { status = "Absent"; statusDlg.DialogResult = DialogResult.OK; };
                btnLeave.Click += (_, _) => { status = "On Leave"; statusDlg.DialogResult = DialogResult.OK; };

                statusDlg.Controls.AddRange(new Control[] { lbl, btnAbsent, btnLeave, btnCancel });
                statusDlg.CancelButton = btnCancel;

                if (statusDlg.ShowDialog(this) != DialogResult.OK || status == null) return;
            }

            // Prompt for optional remarks (e.g., reason for absence or leave)
            var remarks = "";
            using (var remarksInput = new Form())
            {
                remarksInput.Text = "Remarks (Optional)";
                remarksInput.FormBorderStyle = FormBorderStyle.FixedDialog;
                remarksInput.MaximizeBox = false; remarksInput.MinimizeBox = false;
                remarksInput.StartPosition = FormStartPosition.CenterParent;
                remarksInput.Width = 360; remarksInput.Height = 160;

                var txt = new TextBox
                {
                    Multiline = true,
                    Width = 300,
                    Height = 56,
                    Location = new Point(12, 12),
                    PlaceholderText = "Enter reason (optional)..."
                };
                var btnOk = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(195, 76),
                    Width = 60
                };
                var btnSkip = new Button
                {
                    Text = "Skip",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(265, 76),
                    Width = 60
                };
                remarksInput.Controls.AddRange(new Control[] { txt, btnOk, btnSkip });
                remarksInput.AcceptButton = btnOk; remarksInput.CancelButton = btnSkip;

                if (remarksInput.ShowDialog(this) == DialogResult.OK)
                    remarks = txt.Text.Trim();
            }

            try
            {
                SetStatus($"Marking {employee.FullName} as {status}…");
                await _attendanceService.AddAsync(new AttendanceRecord
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.FullName,
                    Date = date,
                    Status = status,
                    Remarks = remarks
                });
                await LoadAttendanceByDateAsync();
                SetStatusSuccess($"{employee.FullName} marked as {status}.");
                ShowInfo($"{employee.FullName} marked as {status}.");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to mark status: {ex.Message}");
                ShowError("marking status", ex);
            }
        }

        // ═══════════════════════════════════════════════════════════════════
        //  REPORTS TAB
        // ═══════════════════════════════════════════════════════════════════

        private void InitReportFilters()
        {
            CB_ReportDepartment.Items.Clear();
            CB_ReportDepartment.Items.Add("All");
            foreach (var d in _departments) CB_ReportDepartment.Items.Add(d.Name);
            CB_ReportDepartment.SelectedIndex = 0;

            CB_ReportEmployee.Items.Clear();
            CB_ReportEmployee.Items.Add("All");
            foreach (var e in _employees) CB_ReportEmployee.Items.Add(e.FullName);
            CB_ReportEmployee.SelectedIndex = 0;

            CB_ReportStatus.Items.Clear();
            CB_ReportStatus.Items.AddRange(new object[] { "All", "Present", "Late", "Absent", "On Leave" });
            CB_ReportStatus.SelectedIndex = 0;

            DTP_DateFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DTP_DateTo.Value = DateTime.Now;
        }

        private async Task GenerateReportAsync()
        {
            if (DTP_DateFrom.Value.Date > DTP_DateTo.Value.Date)
            {
                MessageBox.Show("Start Date ('Date From') cannot be after End Date ('Date To').",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SetStatus("Generating report…");
                var all = await _attendanceService.GetAllAsync();
                foreach (var r in all)
                {
                    r.EmployeeName = _employees.FirstOrDefault(e => e.EmployeeId == r.EmployeeId)?.FullName ?? "(Unknown)";
                }

                var dateFrom = DTP_DateFrom.Value.Date;
                var dateTo = DTP_DateTo.Value.Date;

                var q = all.Where(r =>
                    DateTime.TryParse(r.Date, out var d) && d >= dateFrom && d <= dateTo);

                var deptName = CB_ReportDepartment.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(deptName) && deptName != "All")
                {
                    var deptObj = _departments.FirstOrDefault(d => d.Name == deptName);
                    var deptId = deptObj?.DepartmentId ?? string.Empty;
                    var inDept = _employees.Where(e => e.DepartmentId == deptId).Select(e => e.EmployeeId).ToHashSet();
                    q = q.Where(r => inDept.Contains(r.EmployeeId));
                }

                var empName = CB_ReportEmployee.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(empName) && empName != "All")
                {
                    var emp = _employees.FirstOrDefault(e => e.FullName == empName);
                    if (emp != null) q = q.Where(r => r.EmployeeId == emp.EmployeeId);
                }

                var status = CB_ReportStatus.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(status) && status != "All")
                    q = q.Where(r => r.Status == status);

                _reportRecords = q.OrderBy(r => r.Date).ThenBy(r => r.EmployeeName).ToList();

                DGV_ReportList.DataSource = _reportRecords.Select(r => new
                {
                    EmpID = r.EmployeeId,
                    FullName = r.EmployeeName,
                    Date = r.Date,
                    Hours = r.TotalHours > 0 ? $"{r.TotalHours:F2}" : "—",
                    Status = r.Status,
                    Remarks = r.Remarks
                }).ToList();

                statsPresentLabel.Text = $"Total Present: {_reportRecords.Count(r => r.Status == "Present")}";
                statsLateLabel.Text = $"Total Late: {_reportRecords.Count(r => r.Status == "Late")}";
                statsAbsentLabel.Text = $"Total Absent: {_reportRecords.Count(r => r.Status == "Absent")}";
                statsOnLeaveLabel.Text = $"Total On Leave: {_reportRecords.Count(r => r.Status == "On Leave")}";
                statsTotalHoursLabel.Text = $"Total Hours Rendered: {_reportRecords.Sum(r => r.TotalHours):F2} hrs";
                SetStatusSuccess($"Report generated — {_reportRecords.Count} records.");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to generate report: {ex.Message}");
                ShowError("generating report", ex);
            }
        }

        private void ExportToCSV(object? sender, EventArgs e)
        {
            if (_reportRecords.Count == 0)
            {
                ShowInfo("No report data to export. Click 'Generate Report' first.");
                return;
            }

            using var save = new SaveFileDialog
            {
                Title = "Export Attendance Report",
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = $"AttendanceReport_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (save.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                SetStatus("Exporting to CSV…");
                var lines = new List<string> { "EmpID,Full Name,Date,Hours,Status,Remarks" };
                lines.AddRange(_reportRecords.Select(r =>
                    $"\"{r.EmployeeId}\",\"{r.EmployeeName}\",\"{r.Date}\"," +
                    $"{r.TotalHours:F2},\"{r.Status}\",\"{r.Remarks}\""));
                File.WriteAllLines(save.FileName, lines, Encoding.UTF8);
                SetStatusSuccess($"Exported to: {Path.GetFileName(save.FileName)}");
                ShowInfo($"Report exported to:\n{save.FileName}");
            }
            catch (Exception ex)
            {
                SetStatusError($"Failed to export CSV: {ex.Message}");
                ShowError("exporting CSV", ex);
            }
        }

        // ═══════════════════════════════════════════════════════════════════
        //  HELPERS
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>Shortens a Firebase push key to 8 chars for display.</summary>
        private static string Shorten(string key) =>
            key.Length > 8 ? key[..8] : key;

        private void ShowInfo(string msg) =>
            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void ShowError(string action, Exception ex) =>
            MessageBox.Show($"Error while {action}:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private bool Confirm(string msg) =>
            MessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;

        // ── Status bar helpers ────────────────────────────────────────────

        /// <summary>Sets a neutral (in-progress) status message. Stops any pending auto-clear timer.</summary>
        private void SetStatus(string msg)
        {
            _statusTimer.Stop();
            statusBarLabel.Text = msg;
            statusBarLabel.ForeColor = SystemColors.ControlText;
        }

        /// <summary>Sets a success status message and schedules it to auto-clear to 'Ready' after 3 s.</summary>
        private void SetStatusSuccess(string msg)
        {
            _statusTimer.Stop();
            statusBarLabel.Text = msg;
            statusBarLabel.ForeColor = Color.DarkGreen;
            _statusTimer.Start();
        }

        /// <summary>Sets an error status message. Stays visible until the next operation overwrites it.</summary>
        private void SetStatusError(string msg)
        {
            _statusTimer.Stop();
            statusBarLabel.Text = msg;
            statusBarLabel.ForeColor = Color.DarkRed;
        }

        // ── Column Sorting ───────────────────────────────────────────────

        /// <summary>
        /// Sorts a list by a key selector and rebinds a DataGridView.
        /// Toggles ascending/descending on repeated clicks of the same column.
        /// Updates the grid's sort glyph to give the user visual feedback.
        /// </summary>
        private static List<T> ApplySort<T>(List<T> source, Func<T, object?> keySelector, bool ascending)
            => ascending
                ? [.. source.OrderBy(keySelector)]
                : [.. source.OrderByDescending(keySelector)];

        private void DGV_EmployeeList_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (_filteredEmployees.Count == 0) return;
            bool asc = _empSort.Col == e.ColumnIndex ? !_empSort.Asc : true;
            _empSort = (e.ColumnIndex, asc);

            Func<Employee, object?> key = e.ColumnIndex switch
            {
                0 => emp => emp.EmployeeId,
                1 => emp => emp.FullName,
                2 => emp => emp.Position,
                3 => emp => _departments.FirstOrDefault(d => d.DepartmentId == emp.DepartmentId)?.Name ?? "",
                4 => emp => emp.EmploymentStatus,
                _ => emp => emp.FullName
            };

            _filteredEmployees = ApplySort(_filteredEmployees, key, asc);
            // Rebind without resetting selection/buttons
            DGV_EmployeeList.DataSource = _filteredEmployees.Select(emp => new
            {
                EmpID      = emp.EmployeeId,
                FullName   = emp.FullName,
                Position   = emp.Position,
                Department = _departments.FirstOrDefault(d => d.DepartmentId == emp.DepartmentId)?.Name ?? "(None)",
                Status     = emp.EmploymentStatus
            }).ToList();
            UpdateSortGlyph(DGV_EmployeeList, e.ColumnIndex, asc);
        }

        private void DGV_DepartmentList_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (_filteredDepartments.Count == 0) return;
            bool asc = _deptSort.Col == e.ColumnIndex ? !_deptSort.Asc : true;
            _deptSort = (e.ColumnIndex, asc);

            var empCounts = _employees
                .Where(emp => !string.IsNullOrEmpty(emp.DepartmentId))
                .GroupBy(emp => emp.DepartmentId)
                .ToDictionary(g => g.Key, g => g.Count());

            Func<Department, object?> key = e.ColumnIndex switch
            {
                0 => d => d.DepartmentId,
                1 => d => d.Name,
                2 => d => _employees.FirstOrDefault(emp => emp.EmployeeId == d.DepartmentHeadId)?.FullName ?? "",
                3 => d => d.Location,
                4 => d => empCounts.TryGetValue(d.DepartmentId, out var cnt) ? (object)cnt : 0,
                _ => d => d.Name
            };

            _filteredDepartments = ApplySort(_filteredDepartments, key, asc);
            DGV_DepartmentList.DataSource = _filteredDepartments.Select(d => new
            {
                ID             = d.DepartmentId,
                DepartmentName = d.Name,
                Head           = _employees.FirstOrDefault(emp => emp.EmployeeId == d.DepartmentHeadId)?.FullName ?? "(None)",
                Location       = d.Location,
                Employees      = empCounts.TryGetValue(d.DepartmentId, out var cnt) ? cnt : 0
            }).ToList();
            UpdateSortGlyph(DGV_DepartmentList, e.ColumnIndex, asc);
        }

        private void DGV_AttendanceList_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (_filteredAttendance.Count == 0) return;
            bool asc = _attSort.Col == e.ColumnIndex ? !_attSort.Asc : true;
            _attSort = (e.ColumnIndex, asc);

            Func<AttendanceRecord, object?> key = e.ColumnIndex switch
            {
                0 => r => r.EmployeeId,
                1 => r => r.EmployeeName,
                2 => r => r.TimeIn,
                3 => r => r.TimeOut,
                4 => r => r.TotalHours,
                5 => r => r.Status,
                _ => r => r.EmployeeName
            };

            _filteredAttendance = ApplySort(_filteredAttendance, key, asc);
            DGV_AttendanceList.DataSource = _filteredAttendance.Select(r => new
            {
                EmpID    = r.EmployeeId,
                FullName = r.EmployeeName,
                TimeIn   = r.TimeIn,
                TimeOut  = r.TimeOut,
                Hours    = r.TotalHours > 0 ? $"{r.TotalHours:F2} h" : "—",
                Status   = r.Status
            }).ToList();
            UpdateSortGlyph(DGV_AttendanceList, e.ColumnIndex, asc);
        }

        private void DGV_ReportList_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (_reportRecords.Count == 0) return;
            bool asc = _rptSort.Col == e.ColumnIndex ? !_rptSort.Asc : true;
            _rptSort = (e.ColumnIndex, asc);

            Func<AttendanceRecord, object?> key = e.ColumnIndex switch
            {
                0 => r => r.EmployeeId,
                1 => r => r.EmployeeName,
                2 => r => r.Date,
                3 => r => r.TotalHours,
                4 => r => r.Status,
                5 => r => r.Remarks,
                _ => r => r.Date
            };

            _reportRecords = ApplySort(_reportRecords, key, asc);
            DGV_ReportList.DataSource = _reportRecords.Select(r => new
            {
                EmpID    = r.EmployeeId,
                FullName = r.EmployeeName,
                Date     = r.Date,
                Hours    = r.TotalHours > 0 ? $"{r.TotalHours:F2}" : "—",
                Status   = r.Status,
                Remarks  = r.Remarks
            }).ToList();
            UpdateSortGlyph(DGV_ReportList, e.ColumnIndex, asc);
        }

        /// <summary>Shows a sort glyph on the clicked column header and clears others.</summary>
        private static void UpdateSortGlyph(DataGridView dgv, int colIndex, bool ascending)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            dgv.Columns[colIndex].HeaderCell.SortGlyphDirection =
                ascending ? SortOrder.Ascending : SortOrder.Descending;
        }

        // ── Designer event handler stubs ─────────────────────────────────
        private void employeeTab_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void DGV_EmployeeList_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void departmentLabel_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void totalRecordValueLabel_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void deleteEmployeeButton_Click(object sender, EventArgs e) { }
        private void locationLabel_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void attSummaryPanel_Paint(object sender, PaintEventArgs e) { }
        private void summaryPresentLabel_Click(object sender, EventArgs e) { }
        private void statsLateLabel_Click(object sender, EventArgs e) { }
        private void statsPresentLabel_Click(object sender, EventArgs e) { }
        private void statsAbsentLabel_Click(object sender, EventArgs e) { }
        private void reportFiltersPanel_Paint(object sender, PaintEventArgs e) { }
        private void label12_Click(object sender, EventArgs e) { }
    }
}
