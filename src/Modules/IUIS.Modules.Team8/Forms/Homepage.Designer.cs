namespace IUIS.Modules.Team8.Forms
{
    partial class Homepage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.logoPanel = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.btnNavDashboard = new System.Windows.Forms.Button();
            this.btnNavEnrollStudent = new System.Windows.Forms.Button();
            this.btnNavEnrolledSubjects = new System.Windows.Forms.Button();
            this.btnNavSectionAssignment = new System.Windows.Forms.Button();
            this.btnNavTuitionAssessment = new System.Windows.Forms.Button();
            this.btnNavEnrollmentHistory = new System.Windows.Forms.Button();
            this.sidebarDivider = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            
            // TabControl for Swappable screens
            this.tcPages = new System.Windows.Forms.TabControl();
            
            // 6 TabPages
            this.tpDashboard = new System.Windows.Forms.TabPage();
            this.tpEnroll = new System.Windows.Forms.TabPage();
            this.tpSubjects = new System.Windows.Forms.TabPage();
            this.tpSection = new System.Windows.Forms.TabPage();
            this.tpAssessment = new System.Windows.Forms.TabPage();
            this.tpHistory = new System.Windows.Forms.TabPage();

            // ================= tpDashboard Controls =================
            this.lblBreadcrumb = new System.Windows.Forms.LinkLabel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.headerDivider = new System.Windows.Forms.Panel();
            this.cardEnrolled = new System.Windows.Forms.Panel();
            this.lblEnrolledCount = new System.Windows.Forms.Label();
            this.lblEnrolledLabel = new System.Windows.Forms.Label();
            this.cardPending = new System.Windows.Forms.Panel();
            this.lblPendingCount = new System.Windows.Forms.Label();
            this.lblPendingLabel = new System.Windows.Forms.Label();
            this.cardUnpaid = new System.Windows.Forms.Panel();
            this.lblUnpaidCount = new System.Windows.Forms.Label();
            this.lblUnpaidLabel = new System.Windows.Forms.Label();
            this.cardOpenSections = new System.Windows.Forms.Panel();
            this.lblOpenSectionsCount = new System.Windows.Forms.Label();
            this.lblOpenSectionsLabel = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblTerm = new System.Windows.Forms.Label();
            this.cboTerm = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvEnrollments = new System.Windows.Forms.DataGridView();
            this.btnNewEnrollment = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnUpdateEnrollment = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();

            // ================= tpEnroll Controls =================
            this.lblBreadcrumbEnroll = new System.Windows.Forms.LinkLabel();
            this.lblTitleEnroll = new System.Windows.Forms.Label();
            this.headerDividerEnroll = new System.Windows.Forms.Panel();
            // Stepper
            this.pnlStepperEnroll = new System.Windows.Forms.Panel();
            this.lblStep1Enroll = new System.Windows.Forms.Label();
            this.lblStep2Enroll = new System.Windows.Forms.Label();
            this.lblStep3Enroll = new System.Windows.Forms.Label();
            this.lblStep4Enroll = new System.Windows.Forms.Label();
            this.lblStep5Enroll = new System.Windows.Forms.Label();
            // Fields
            this.lblStudentNo = new System.Windows.Forms.Label();
            this.txtStudentNo = new System.Windows.Forms.TextBox();
            this.btnLoadStudent = new System.Windows.Forms.Button();
            this.dgvStudentsList = new System.Windows.Forms.DataGridView();
            this.lblFullNameLabel = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblProgramLabel = new System.Windows.Forms.Label();
            this.txtProgram = new System.Windows.Forms.TextBox();
            this.lblYearLevelLabel = new System.Windows.Forms.Label();
            this.txtYearLevel = new System.Windows.Forms.TextBox();
            this.lblSchoolYearEnroll = new System.Windows.Forms.Label();
            this.cboSchoolYearEnroll = new System.Windows.Forms.ComboBox();
            this.lblSemesterEnroll = new System.Windows.Forms.Label();
            this.cboSemesterEnroll = new System.Windows.Forms.ComboBox();
            this.lblEnrollmentType = new System.Windows.Forms.Label();
            this.cboEnrollmentType = new System.Windows.Forms.ComboBox();
            this.lblStudentType = new System.Windows.Forms.Label();
            this.cboStudentType = new System.Windows.Forms.ComboBox();
            // Actions
            this.btnCancelEnroll = new System.Windows.Forms.Button();
            this.btnNextEnroll = new System.Windows.Forms.Button();

            // ================= tpSubjects Controls =================
            this.lblBreadcrumbSubjects = new System.Windows.Forms.LinkLabel();
            this.lblTitleSubjects = new System.Windows.Forms.Label();
            this.headerDividerSubjects = new System.Windows.Forms.Panel();
            // Stepper
            this.pnlStepperSubjects = new System.Windows.Forms.Panel();
            this.lblStep1Subjects = new System.Windows.Forms.Label();
            this.lblStep2Subjects = new System.Windows.Forms.Label();
            this.lblStep3Subjects = new System.Windows.Forms.Label();
            this.lblStep4Subjects = new System.Windows.Forms.Label();
            this.lblStep5Subjects = new System.Windows.Forms.Label();
            // Fields
            this.lblSubjectsSearch = new System.Windows.Forms.Label();
            this.txtSubjectsSearch = new System.Windows.Forms.TextBox();
            this.lblCurriculumYear = new System.Windows.Forms.Label();
            this.cboCurriculumYear = new System.Windows.Forms.ComboBox();
            this.dgvSubjectsSelect = new System.Windows.Forms.DataGridView();
            this.lblTotalSubjectsLabel = new System.Windows.Forms.Label();
            this.txtTotalSubjects = new System.Windows.Forms.TextBox();
            this.lblTotalUnitsLabel = new System.Windows.Forms.Label();
            this.txtTotalUnits = new System.Windows.Forms.TextBox();
            this.lblMaxUnitsLabel = new System.Windows.Forms.Label();
            this.txtMaxUnits = new System.Windows.Forms.TextBox();
            // Actions
            this.btnBackSubjects = new System.Windows.Forms.Button();
            this.btnDropSelected = new System.Windows.Forms.Button();
            this.btnNextSubjects = new System.Windows.Forms.Button();

            // ================= tpSection Controls =================
            this.lblBreadcrumbSection = new System.Windows.Forms.LinkLabel();
            this.lblTitleSection = new System.Windows.Forms.Label();
            this.headerDividerSection = new System.Windows.Forms.Panel();
            // Stepper
            this.pnlStepperSection = new System.Windows.Forms.Panel();
            this.lblStep1Section = new System.Windows.Forms.Label();
            this.lblStep2Section = new System.Windows.Forms.Label();
            this.lblStep3Section = new System.Windows.Forms.Label();
            this.lblStep4Section = new System.Windows.Forms.Label();
            this.lblStep5Section = new System.Windows.Forms.Label();
            // Fields
            this.dgvSectionAssign = new System.Windows.Forms.DataGridView();
            this.pnlConflictChecker = new System.Windows.Forms.Panel();
            this.lblConflictHeader = new System.Windows.Forms.Label();
            this.lblConflictMessage = new System.Windows.Forms.Label();
            // Actions
            this.btnBackSection = new System.Windows.Forms.Button();
            this.btnAutoAssign = new System.Windows.Forms.Button();
            this.btnNextSection = new System.Windows.Forms.Button();

            // ================= tpAssessment Controls =================
            this.lblBreadcrumbAssessment = new System.Windows.Forms.LinkLabel();
            this.lblTitleAssessment = new System.Windows.Forms.Label();
            this.headerDividerAssessment = new System.Windows.Forms.Panel();
            // Stepper
            this.pnlStepperAssessment = new System.Windows.Forms.Panel();
            this.lblStep1Assessment = new System.Windows.Forms.Label();
            this.lblStep2Assessment = new System.Windows.Forms.Label();
            this.lblStep3Assessment = new System.Windows.Forms.Label();
            this.lblStep4Assessment = new System.Windows.Forms.Label();
            this.lblStep5Assessment = new System.Windows.Forms.Label();
            // Fields
            this.lblAssessedUnitsLabel = new System.Windows.Forms.Label();
            this.txtAssessedUnits = new System.Windows.Forms.TextBox();
            this.lblRatePerUnitLabel = new System.Windows.Forms.Label();
            this.txtRatePerUnit = new System.Windows.Forms.TextBox();
            this.lblLabUnitsLabel = new System.Windows.Forms.Label();
            this.txtLabUnits = new System.Windows.Forms.TextBox();
            this.lblPaymentPlan = new System.Windows.Forms.Label();
            this.cboPaymentPlan = new System.Windows.Forms.ComboBox();
            this.dgvFeesBreakdown = new System.Windows.Forms.DataGridView();
            this.lblAmountPaidLabel = new System.Windows.Forms.Label();
            this.txtAmountPaid = new System.Windows.Forms.TextBox();
            // Actions
            this.btnBackAssessment = new System.Windows.Forms.Button();
            this.btnPrintAssessment = new System.Windows.Forms.Button();
            this.btnConfirmEnrollment = new System.Windows.Forms.Button();

            // ================= tpHistory Controls =================
            this.lblBreadcrumbHistory = new System.Windows.Forms.LinkLabel();
            this.lblTitleHistory = new System.Windows.Forms.Label();
            this.headerDividerHistory = new System.Windows.Forms.Panel();
            this.lblHistorySearch = new System.Windows.Forms.Label();
            this.txtHistorySearch = new System.Windows.Forms.TextBox();
            this.lblHistorySY = new System.Windows.Forms.Label();
            this.cboHistorySY = new System.Windows.Forms.ComboBox();
            this.lblHistoryAction = new System.Windows.Forms.Label();
            this.cboHistoryAction = new System.Windows.Forms.ComboBox();
            this.btnHistoryFilter = new System.Windows.Forms.Button();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.btnHistoryView = new System.Windows.Forms.Button();
            this.btnHistoryExport = new System.Windows.Forms.Button();

            this.sidebarPanel.SuspendLayout();
            this.logoPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.tcPages.SuspendLayout();
            this.tpDashboard.SuspendLayout();
            this.cardEnrolled.SuspendLayout();
            this.cardPending.SuspendLayout();
            this.cardUnpaid.SuspendLayout();
            this.cardOpenSections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnrollments)).BeginInit();
            this.tpEnroll.SuspendLayout();
            this.pnlStepperEnroll.SuspendLayout();
            this.tpSubjects.SuspendLayout();
            this.pnlStepperSubjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjectsSelect)).BeginInit();
            this.tpSection.SuspendLayout();
            this.pnlStepperSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSectionAssign)).BeginInit();
            this.pnlConflictChecker.SuspendLayout();
            this.tpAssessment.SuspendLayout();
            this.pnlStepperAssessment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeesBreakdown)).BeginInit();
            this.tpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();

            // 
            // Homepage Form Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 531);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.sidebarDivider);
            this.Controls.Add(this.sidebarPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(1020, 560);
            this.Name = "Homepage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Team 8 — Enrollment Management System";

            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.White;
            this.sidebarPanel.Controls.Add(this.logoPanel);
            this.sidebarPanel.Controls.Add(this.btnNavDashboard);
            this.sidebarPanel.Controls.Add(this.btnNavEnrollStudent);
            this.sidebarPanel.Controls.Add(this.btnNavEnrolledSubjects);
            this.sidebarPanel.Controls.Add(this.btnNavSectionAssignment);
            this.sidebarPanel.Controls.Add(this.btnNavTuitionAssessment);
            this.sidebarPanel.Controls.Add(this.btnNavEnrollmentHistory);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Size = new System.Drawing.Size(210, 531);

            // 
            // logoPanel
            // 
            this.logoPanel.BackColor = System.Drawing.Color.White;
            this.logoPanel.Controls.Add(this.lblLogo);
            this.logoPanel.Location = new System.Drawing.Point(15, 15);
            this.logoPanel.Size = new System.Drawing.Size(180, 52);
            this.logoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintDashedBorder);

            // 
            // lblLogo
            // 
            this.lblLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLogo.ForeColor = System.Drawing.Color.DarkGray;
            this.lblLogo.Location = new System.Drawing.Point(0, 0);
            this.lblLogo.Text = "LOGO / SYSTEM NAME";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnNavDashboard
            // 
            this.btnNavDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnNavDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavDashboard.FlatAppearance.BorderSize = 0;
            this.btnNavDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavDashboard.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNavDashboard.ForeColor = System.Drawing.Color.White;
            this.btnNavDashboard.Location = new System.Drawing.Point(15, 87);
            this.btnNavDashboard.Size = new System.Drawing.Size(180, 38);
            this.btnNavDashboard.Text = "Dashboard";
            this.btnNavDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDashboard.UseVisualStyleBackColor = false;
            this.btnNavDashboard.Click += new System.EventHandler(this.BtnNavDashboard_Click);

            // 
            // btnNavEnrollStudent
            // 
            this.btnNavEnrollStudent.BackColor = System.Drawing.Color.White;
            this.btnNavEnrollStudent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavEnrollStudent.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnNavEnrollStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavEnrollStudent.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNavEnrollStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnNavEnrollStudent.Location = new System.Drawing.Point(15, 132);
            this.btnNavEnrollStudent.Size = new System.Drawing.Size(180, 38);
            this.btnNavEnrollStudent.Text = "Enroll Student";
            this.btnNavEnrollStudent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavEnrollStudent.UseVisualStyleBackColor = false;
            this.btnNavEnrollStudent.Click += new System.EventHandler(this.BtnNavEnrollStudent_Click);
            this.btnNavEnrollStudent.MouseEnter += new System.EventHandler(this.SidebarButton_MouseEnter);
            this.btnNavEnrollStudent.MouseLeave += new System.EventHandler(this.SidebarButton_MouseLeave);

            // 
            // btnNavEnrolledSubjects
            // 
            this.btnNavEnrolledSubjects.BackColor = System.Drawing.Color.White;
            this.btnNavEnrolledSubjects.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavEnrolledSubjects.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnNavEnrolledSubjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavEnrolledSubjects.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNavEnrolledSubjects.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnNavEnrolledSubjects.Location = new System.Drawing.Point(15, 177);
            this.btnNavEnrolledSubjects.Size = new System.Drawing.Size(180, 38);
            this.btnNavEnrolledSubjects.Text = "Enrolled Subjects";
            this.btnNavEnrolledSubjects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavEnrolledSubjects.UseVisualStyleBackColor = false;
            this.btnNavEnrolledSubjects.Click += new System.EventHandler(this.BtnNavEnrolledSubjects_Click);
            this.btnNavEnrolledSubjects.MouseEnter += new System.EventHandler(this.SidebarButton_MouseEnter);
            this.btnNavEnrolledSubjects.MouseLeave += new System.EventHandler(this.SidebarButton_MouseLeave);

            // 
            // btnNavSectionAssignment
            // 
            this.btnNavSectionAssignment.BackColor = System.Drawing.Color.White;
            this.btnNavSectionAssignment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavSectionAssignment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnNavSectionAssignment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavSectionAssignment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNavSectionAssignment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnNavSectionAssignment.Location = new System.Drawing.Point(15, 222);
            this.btnNavSectionAssignment.Size = new System.Drawing.Size(180, 38);
            this.btnNavSectionAssignment.Text = "Section Assignment";
            this.btnNavSectionAssignment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavSectionAssignment.UseVisualStyleBackColor = false;
            this.btnNavSectionAssignment.Click += new System.EventHandler(this.BtnNavSectionAssignment_Click);
            this.btnNavSectionAssignment.MouseEnter += new System.EventHandler(this.SidebarButton_MouseEnter);
            this.btnNavSectionAssignment.MouseLeave += new System.EventHandler(this.SidebarButton_MouseLeave);

            // 
            // btnNavTuitionAssessment
            // 
            this.btnNavTuitionAssessment.BackColor = System.Drawing.Color.White;
            this.btnNavTuitionAssessment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavTuitionAssessment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnNavTuitionAssessment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavTuitionAssessment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNavTuitionAssessment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnNavTuitionAssessment.Location = new System.Drawing.Point(15, 267);
            this.btnNavTuitionAssessment.Size = new System.Drawing.Size(180, 38);
            this.btnNavTuitionAssessment.Text = "Tuition Assessment";
            this.btnNavTuitionAssessment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavTuitionAssessment.UseVisualStyleBackColor = false;
            this.btnNavTuitionAssessment.Click += new System.EventHandler(this.BtnNavTuitionAssessment_Click);
            this.btnNavTuitionAssessment.MouseEnter += new System.EventHandler(this.SidebarButton_MouseEnter);
            this.btnNavTuitionAssessment.MouseLeave += new System.EventHandler(this.SidebarButton_MouseLeave);

            // 
            // btnNavEnrollmentHistory
            // 
            this.btnNavEnrollmentHistory.BackColor = System.Drawing.Color.White;
            this.btnNavEnrollmentHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavEnrollmentHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnNavEnrollmentHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavEnrollmentHistory.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNavEnrollmentHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnNavEnrollmentHistory.Location = new System.Drawing.Point(15, 312);
            this.btnNavEnrollmentHistory.Size = new System.Drawing.Size(180, 38);
            this.btnNavEnrollmentHistory.Text = "Enrollment History";
            this.btnNavEnrollmentHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavEnrollmentHistory.UseVisualStyleBackColor = false;
            this.btnNavEnrollmentHistory.Click += new System.EventHandler(this.BtnNavEnrollmentHistory_Click);
            this.btnNavEnrollmentHistory.MouseEnter += new System.EventHandler(this.SidebarButton_MouseEnter);
            this.btnNavEnrollmentHistory.MouseLeave += new System.EventHandler(this.SidebarButton_MouseLeave);

            // 
            // sidebarDivider
            // 
            this.sidebarDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.sidebarDivider.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarDivider.Size = new System.Drawing.Size(2, 531);

            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.tcPages);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            // 
            // tcPages
            // 
            this.tcPages.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcPages.Controls.Add(this.tpDashboard);
            this.tcPages.Controls.Add(this.tpEnroll);
            this.tcPages.Controls.Add(this.tpSubjects);
            this.tcPages.Controls.Add(this.tpSection);
            this.tcPages.Controls.Add(this.tpAssessment);
            this.tcPages.Controls.Add(this.tpHistory);
            this.tcPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPages.ItemSize = new System.Drawing.Size(0, 1);
            this.tcPages.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;

            // ========================================================================
            // tpDashboard (Dashboard Screen)
            // ========================================================================
            this.tpDashboard.BackColor = System.Drawing.Color.White;
            this.tpDashboard.Controls.Add(this.lblBreadcrumb);
            this.tpDashboard.Controls.Add(this.lblTitle);
            this.tpDashboard.Controls.Add(this.headerDivider);
            this.tpDashboard.Controls.Add(this.cardEnrolled);
            this.tpDashboard.Controls.Add(this.cardPending);
            this.tpDashboard.Controls.Add(this.cardUnpaid);
            this.tpDashboard.Controls.Add(this.cardOpenSections);
            this.tpDashboard.Controls.Add(this.lblSearch);
            this.tpDashboard.Controls.Add(this.txtSearch);
            this.tpDashboard.Controls.Add(this.lblTerm);
            this.contentPanel.Controls.Add(this.cboTerm); // handled below
            this.tpDashboard.Controls.Add(this.cboTerm);
            this.tpDashboard.Controls.Add(this.btnSearch);
            this.tpDashboard.Controls.Add(this.dgvEnrollments);
            this.tpDashboard.Controls.Add(this.btnNewEnrollment);
            this.tpDashboard.Controls.Add(this.btnView);
            this.tpDashboard.Controls.Add(this.btnUpdateEnrollment);
            this.tpDashboard.Controls.Add(this.btnRefresh);
            this.tpDashboard.Location = new System.Drawing.Point(4, 5);
            this.tpDashboard.Padding = new System.Windows.Forms.Padding(16, 18, 16, 18);
            this.tpDashboard.Size = new System.Drawing.Size(864, 522);

            this.lblBreadcrumb.AutoSize = true;
            this.lblBreadcrumb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBreadcrumb.ForeColor = System.Drawing.Color.Gray;
            this.lblBreadcrumb.Location = new System.Drawing.Point(16, 10);
            this.lblBreadcrumb.Text = "Home";

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(14, 25);
            this.lblTitle.Text = "Enrollment Dashboard — S.Y. 2025–2026, 1st Semester";

            this.headerDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.headerDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.headerDivider.Location = new System.Drawing.Point(16, 56);
            this.headerDivider.Size = new System.Drawing.Size(830, 2);

            // Stats Cards
            this.cardEnrolled.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.cardEnrolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardEnrolled.Controls.Add(this.lblEnrolledCount);
            this.cardEnrolled.Controls.Add(this.lblEnrolledLabel);
            this.cardEnrolled.Location = new System.Drawing.Point(16, 68);
            this.cardEnrolled.Size = new System.Drawing.Size(195, 68);

            this.lblEnrolledCount.AutoSize = true;
            this.lblEnrolledCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEnrolledCount.Location = new System.Drawing.Point(8, 4);
            this.lblEnrolledCount.Text = "000";

            this.lblEnrolledLabel.AutoSize = true;
            this.lblEnrolledLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEnrolledLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblEnrolledLabel.Location = new System.Drawing.Point(9, 42);
            this.lblEnrolledLabel.Text = "ENROLLED";

            this.cardPending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.cardPending.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPending.Controls.Add(this.lblPendingCount);
            this.cardPending.Controls.Add(this.lblPendingLabel);
            this.cardPending.Location = new System.Drawing.Point(223, 68);
            this.cardPending.Size = new System.Drawing.Size(195, 68);

            this.lblPendingCount.AutoSize = true;
            this.lblPendingCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPendingCount.Location = new System.Drawing.Point(8, 4);
            this.lblPendingCount.Text = "000";

            this.lblPendingLabel.AutoSize = true;
            this.lblPendingLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPendingLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblPendingLabel.Location = new System.Drawing.Point(9, 42);
            this.lblPendingLabel.Text = "PENDING";

            this.cardUnpaid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.cardUnpaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardUnpaid.Controls.Add(this.lblUnpaidCount);
            this.cardUnpaid.Controls.Add(this.lblUnpaidLabel);
            this.cardUnpaid.Location = new System.Drawing.Point(430, 68);
            this.cardUnpaid.Size = new System.Drawing.Size(195, 68);

            this.lblUnpaidCount.AutoSize = true;
            this.lblUnpaidCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUnpaidCount.Location = new System.Drawing.Point(8, 4);
            this.lblUnpaidCount.Text = "000";

            this.lblUnpaidLabel.AutoSize = true;
            this.lblUnpaidLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUnpaidLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblUnpaidLabel.Location = new System.Drawing.Point(9, 42);
            this.lblUnpaidLabel.Text = "UNPAID BALANCE";

            this.cardOpenSections.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.cardOpenSections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardOpenSections.Controls.Add(this.lblOpenSectionsCount);
            this.cardOpenSections.Controls.Add(this.lblOpenSectionsLabel);
            this.cardOpenSections.Location = new System.Drawing.Point(637, 68);
            this.cardOpenSections.Size = new System.Drawing.Size(209, 68);

            this.lblOpenSectionsCount.AutoSize = true;
            this.lblOpenSectionsCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOpenSectionsCount.Location = new System.Drawing.Point(8, 4);
            this.lblOpenSectionsCount.Text = "000";

            this.lblOpenSectionsLabel.AutoSize = true;
            this.lblOpenSectionsLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOpenSectionsLabel.ForeColor = System.Drawing.Color.Gray;
            this.lblOpenSectionsLabel.Location = new System.Drawing.Point(9, 42);
            this.lblOpenSectionsLabel.Text = "OPEN SECTIONS";

            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSearch.ForeColor = System.Drawing.Color.Gray;
            this.lblSearch.Location = new System.Drawing.Point(16, 146);
            this.lblSearch.Text = "Search student (ID / Name)";

            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(16, 164);
            this.txtSearch.Size = new System.Drawing.Size(402, 25);
            this.txtSearch.TabIndex = 1;

            this.lblTerm.AutoSize = true;
            this.lblTerm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTerm.ForeColor = System.Drawing.Color.Gray;
            this.lblTerm.Location = new System.Drawing.Point(430, 146);
            this.lblTerm.Text = "Term";

            this.cboTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTerm.Location = new System.Drawing.Point(430, 164);
            this.cboTerm.Size = new System.Drawing.Size(195, 25);
            this.cboTerm.TabIndex = 2;

            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(637, 163);
            this.btnSearch.Size = new System.Drawing.Size(209, 27);
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            this.btnSearch.MouseEnter += new System.EventHandler(this.DarkButton_MouseEnter);
            this.btnSearch.MouseLeave += new System.EventHandler(this.DarkButton_MouseLeave);

            this.dgvEnrollments.AllowUserToAddRows = false;
            this.dgvEnrollments.AllowUserToDeleteRows = false;
            this.dgvEnrollments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEnrollments.BackgroundColor = System.Drawing.Color.White;
            this.dgvEnrollments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvEnrollments.ColumnHeadersHeight = 28;
            this.dgvEnrollments.Location = new System.Drawing.Point(16, 205);
            this.dgvEnrollments.MultiSelect = false;
            this.dgvEnrollments.Name = "dgvEnrollments";
            this.dgvEnrollments.ReadOnly = true;
            this.dgvEnrollments.RowHeadersVisible = false;
            this.dgvEnrollments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEnrollments.Size = new System.Drawing.Size(830, 245);
            this.dgvEnrollments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;

            this.btnNewEnrollment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewEnrollment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnNewEnrollment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewEnrollment.FlatAppearance.BorderSize = 0;
            this.btnNewEnrollment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewEnrollment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNewEnrollment.ForeColor = System.Drawing.Color.White;
            this.btnNewEnrollment.Location = new System.Drawing.Point(16, 465);
            this.btnNewEnrollment.Size = new System.Drawing.Size(150, 36);
            this.btnNewEnrollment.Text = "+ New Enrollment";
            this.btnNewEnrollment.Click += new System.EventHandler(this.BtnNewEnrollment_Click);
            this.btnNewEnrollment.MouseEnter += new System.EventHandler(this.DarkButton_MouseEnter);
            this.btnNewEnrollment.MouseLeave += new System.EventHandler(this.DarkButton_MouseLeave);

            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnView.BackColor = System.Drawing.Color.White;
            this.btnView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnView.FlatAppearance.BorderSize = 2;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnView.Location = new System.Drawing.Point(176, 465);
            this.btnView.Size = new System.Drawing.Size(80, 36);
            this.btnView.Text = "View";
            this.btnView.Click += new System.EventHandler(this.BtnView_Click);
            this.btnView.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnView.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnUpdateEnrollment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateEnrollment.BackColor = System.Drawing.Color.White;
            this.btnUpdateEnrollment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateEnrollment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnUpdateEnrollment.FlatAppearance.BorderSize = 2;
            this.btnUpdateEnrollment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateEnrollment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUpdateEnrollment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnUpdateEnrollment.Location = new System.Drawing.Point(266, 465);
            this.btnUpdateEnrollment.Size = new System.Drawing.Size(100, 36);
            this.btnUpdateEnrollment.Text = "Update";
            this.btnUpdateEnrollment.Click += new System.EventHandler(this.BtnUpdateEnrollment_Click);
            this.btnUpdateEnrollment.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnUpdateEnrollment.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.BackColor = System.Drawing.Color.White;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnRefresh.FlatAppearance.BorderSize = 2;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnRefresh.Location = new System.Drawing.Point(376, 465);
            this.btnRefresh.Size = new System.Drawing.Size(90, 36);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            this.btnRefresh.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnRefresh.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            // ========================================================================
            // tpEnroll (Enroll Student Step 1)
            // ========================================================================
            this.tpEnroll.BackColor = System.Drawing.Color.White;
            this.tpEnroll.Controls.Add(this.lblBreadcrumbEnroll);
            this.tpEnroll.Controls.Add(this.lblTitleEnroll);
            this.tpEnroll.Controls.Add(this.headerDividerEnroll);
            this.tpEnroll.Controls.Add(this.pnlStepperEnroll);
            this.tpEnroll.Controls.Add(this.lblStudentNo);
            this.tpEnroll.Controls.Add(this.txtStudentNo);
            this.tpEnroll.Controls.Add(this.btnLoadStudent);
            this.tpEnroll.Controls.Add(this.dgvStudentsList);
            this.tpEnroll.Controls.Add(this.lblFullNameLabel);
            this.tpEnroll.Controls.Add(this.txtFullName);
            this.tpEnroll.Controls.Add(this.lblProgramLabel);
            this.tpEnroll.Controls.Add(this.txtProgram);
            this.tpEnroll.Controls.Add(this.lblYearLevelLabel);
            this.tpEnroll.Controls.Add(this.txtYearLevel);
            this.tpEnroll.Controls.Add(this.lblSchoolYearEnroll);
            this.tpEnroll.Controls.Add(this.cboSchoolYearEnroll);
            this.tpEnroll.Controls.Add(this.lblSemesterEnroll);
            this.tpEnroll.Controls.Add(this.cboSemesterEnroll);
            this.tpEnroll.Controls.Add(this.lblEnrollmentType);
            this.tpEnroll.Controls.Add(this.cboEnrollmentType);
            this.tpEnroll.Controls.Add(this.lblStudentType);
            this.tpEnroll.Controls.Add(this.cboStudentType);
            this.tpEnroll.Controls.Add(this.btnCancelEnroll);
            this.tpEnroll.Controls.Add(this.btnNextEnroll);
            this.tpEnroll.Location = new System.Drawing.Point(4, 5);
            this.tpEnroll.Padding = new System.Windows.Forms.Padding(16, 18, 16, 18);
            this.tpEnroll.Size = new System.Drawing.Size(864, 522);

            this.lblBreadcrumbEnroll.AutoSize = true;
            this.lblBreadcrumbEnroll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBreadcrumbEnroll.ForeColor = System.Drawing.Color.Gray;
            this.lblBreadcrumbEnroll.Location = new System.Drawing.Point(16, 10);
            this.lblBreadcrumbEnroll.Text = "Home › Enroll Student";

            this.lblTitleEnroll.AutoSize = true;
            this.lblTitleEnroll.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitleEnroll.Location = new System.Drawing.Point(14, 25);
            this.lblTitleEnroll.Text = "New Enrollment";

            this.headerDividerEnroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.headerDividerEnroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.headerDividerEnroll.Location = new System.Drawing.Point(16, 56);
            this.headerDividerEnroll.Size = new System.Drawing.Size(830, 2);

            // Stepper
            this.pnlStepperEnroll.Controls.Add(this.lblStep1Enroll);
            this.pnlStepperEnroll.Controls.Add(this.lblStep2Enroll);
            this.pnlStepperEnroll.Controls.Add(this.lblStep3Enroll);
            this.pnlStepperEnroll.Controls.Add(this.lblStep4Enroll);
            this.pnlStepperEnroll.Controls.Add(this.lblStep5Enroll);
            this.pnlStepperEnroll.Location = new System.Drawing.Point(16, 68);
            this.pnlStepperEnroll.Size = new System.Drawing.Size(830, 28);

            this.lblStep1Enroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.lblStep1Enroll.ForeColor = System.Drawing.Color.White;
            this.lblStep1Enroll.Location = new System.Drawing.Point(0, 0);
            this.lblStep1Enroll.Size = new System.Drawing.Size(160, 28);
            this.lblStep1Enroll.Text = "1 · Student";
            this.lblStep1Enroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep1Enroll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblStep2Enroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep2Enroll.ForeColor = System.Drawing.Color.Gray;
            this.lblStep2Enroll.Location = new System.Drawing.Point(166, 0);
            this.lblStep2Enroll.Size = new System.Drawing.Size(160, 28);
            this.lblStep2Enroll.Text = "2 · Subjects";
            this.lblStep2Enroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep2Enroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep3Enroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep3Enroll.ForeColor = System.Drawing.Color.Gray;
            this.lblStep3Enroll.Location = new System.Drawing.Point(332, 0);
            this.lblStep3Enroll.Size = new System.Drawing.Size(160, 28);
            this.lblStep3Enroll.Text = "3 · Section";
            this.lblStep3Enroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep3Enroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep4Enroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep4Enroll.ForeColor = System.Drawing.Color.Gray;
            this.lblStep4Enroll.Location = new System.Drawing.Point(498, 0);
            this.lblStep4Enroll.Size = new System.Drawing.Size(160, 28);
            this.lblStep4Enroll.Text = "4 · Assessment";
            this.lblStep4Enroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep4Enroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep5Enroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep5Enroll.ForeColor = System.Drawing.Color.Gray;
            this.lblStep5Enroll.Location = new System.Drawing.Point(664, 0);
            this.lblStep5Enroll.Size = new System.Drawing.Size(166, 28);
            this.lblStep5Enroll.Text = "5 · Confirm";
            this.lblStep5Enroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep5Enroll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Student No Fields
            this.lblStudentNo.AutoSize = true;
            this.lblStudentNo.Location = new System.Drawing.Point(16, 105);
            this.lblStudentNo.Text = "Search Student (ID or Name):";

            this.txtStudentNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtStudentNo.Location = new System.Drawing.Point(16, 125);
            this.txtStudentNo.Size = new System.Drawing.Size(280, 25);

            this.btnLoadStudent.BackColor = System.Drawing.Color.White;
            this.btnLoadStudent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadStudent.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnLoadStudent.FlatAppearance.BorderSize = 2;
            this.btnLoadStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadStudent.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLoadStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnLoadStudent.Location = new System.Drawing.Point(306, 124);
            this.btnLoadStudent.Size = new System.Drawing.Size(110, 27);
            this.btnLoadStudent.Text = "Search";
            this.btnLoadStudent.Click += new System.EventHandler(this.BtnLoadStudent_Click);
            this.btnLoadStudent.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnLoadStudent.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            // dgvStudentsList
            this.dgvStudentsList.AllowUserToAddRows = false;
            this.dgvStudentsList.AllowUserToDeleteRows = false;
            this.dgvStudentsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvStudentsList.BackgroundColor = System.Drawing.Color.White;
            this.dgvStudentsList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvStudentsList.ColumnHeadersHeight = 28;
            this.dgvStudentsList.Location = new System.Drawing.Point(16, 160);
            this.dgvStudentsList.MultiSelect = false;
            this.dgvStudentsList.Name = "dgvStudentsList";
            this.dgvStudentsList.ReadOnly = true;
            this.dgvStudentsList.RowHeadersVisible = false;
            this.dgvStudentsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudentsList.Size = new System.Drawing.Size(400, 280);
            this.dgvStudentsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;

            this.lblFullNameLabel.AutoSize = true;
            this.lblFullNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFullNameLabel.Location = new System.Drawing.Point(440, 105);
            this.lblFullNameLabel.Text = "Full Name";

            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.Location = new System.Drawing.Point(440, 125);
            this.txtFullName.Size = new System.Drawing.Size(390, 25);
            this.txtFullName.ReadOnly = true;
            this.txtFullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            this.lblProgramLabel.AutoSize = true;
            this.lblProgramLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProgramLabel.Location = new System.Drawing.Point(440, 165);
            this.lblProgramLabel.Text = "Program";

            this.txtProgram.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.txtProgram.Location = new System.Drawing.Point(440, 185);
            this.txtProgram.Size = new System.Drawing.Size(185, 25);
            this.txtProgram.ReadOnly = true;
            this.txtProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            this.lblYearLevelLabel.AutoSize = true;
            this.lblYearLevelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblYearLevelLabel.Location = new System.Drawing.Point(645, 165);
            this.lblYearLevelLabel.Text = "Year Level";

            this.txtYearLevel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtYearLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYearLevel.Location = new System.Drawing.Point(645, 185);
            this.txtYearLevel.Size = new System.Drawing.Size(185, 25);
            this.txtYearLevel.ReadOnly = true;
            this.txtYearLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            // Dropdowns row
            this.lblSchoolYearEnroll.AutoSize = true;
            this.lblSchoolYearEnroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSchoolYearEnroll.Location = new System.Drawing.Point(440, 235);
            this.lblSchoolYearEnroll.Text = "School Year";

            this.cboSchoolYearEnroll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSchoolYearEnroll.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSchoolYearEnroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.cboSchoolYearEnroll.Location = new System.Drawing.Point(440, 255);
            this.cboSchoolYearEnroll.Size = new System.Drawing.Size(185, 25);
            this.cboSchoolYearEnroll.Items.AddRange(new object[] { "2025-2026", "2026-2027" });
            this.cboSchoolYearEnroll.SelectedIndex = 0;

            this.lblSemesterEnroll.AutoSize = true;
            this.lblSemesterEnroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSemesterEnroll.Location = new System.Drawing.Point(645, 235);
            this.lblSemesterEnroll.Text = "Semester";

            this.cboSemesterEnroll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSemesterEnroll.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSemesterEnroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSemesterEnroll.Location = new System.Drawing.Point(645, 255);
            this.cboSemesterEnroll.Size = new System.Drawing.Size(185, 25);
            this.cboSemesterEnroll.Items.AddRange(new object[] { "1st Semester", "2nd Semester", "Summer" });
            this.cboSemesterEnroll.SelectedIndex = 0;

            this.lblEnrollmentType.AutoSize = true;
            this.lblEnrollmentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEnrollmentType.Location = new System.Drawing.Point(440, 305);
            this.lblEnrollmentType.Text = "Enrollment Type";

            this.cboEnrollmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEnrollmentType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboEnrollmentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.cboEnrollmentType.Location = new System.Drawing.Point(440, 325);
            this.cboEnrollmentType.Size = new System.Drawing.Size(185, 25);
            this.cboEnrollmentType.Items.AddRange(new object[] { "Regular", "Irregular" });
            this.cboEnrollmentType.SelectedIndex = 0;

            this.lblStudentType.AutoSize = true;
            this.lblStudentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStudentType.Location = new System.Drawing.Point(645, 305);
            this.lblStudentType.Text = "Student Type";

            this.cboStudentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStudentType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStudentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStudentType.Location = new System.Drawing.Point(645, 325);
            this.cboStudentType.Size = new System.Drawing.Size(185, 25);
            this.cboStudentType.Items.AddRange(new object[] { "New", "Old", "Transferee" });
            this.cboStudentType.SelectedIndex = 1; // Default Old

            // Nav buttons
            this.btnCancelEnroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelEnroll.BackColor = System.Drawing.Color.White;
            this.btnCancelEnroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelEnroll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnCancelEnroll.FlatAppearance.BorderSize = 2;
            this.btnCancelEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelEnroll.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancelEnroll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnCancelEnroll.Location = new System.Drawing.Point(16, 465);
            this.btnCancelEnroll.Size = new System.Drawing.Size(100, 36);
            this.btnCancelEnroll.Text = "Cancel";
            this.btnCancelEnroll.Click += new System.EventHandler(this.BtnCancelEnroll_Click);
            this.btnCancelEnroll.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnCancelEnroll.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnNextEnroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextEnroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnNextEnroll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextEnroll.FlatAppearance.BorderSize = 0;
            this.btnNextEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextEnroll.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNextEnroll.ForeColor = System.Drawing.Color.White;
            this.btnNextEnroll.Location = new System.Drawing.Point(646, 465);
            this.btnNextEnroll.Size = new System.Drawing.Size(200, 36);
            this.btnNextEnroll.Text = "Next: Select Subjects →";
            this.btnNextEnroll.Click += new System.EventHandler(this.BtnNextEnroll_Click);
            this.btnNextEnroll.MouseEnter += new System.EventHandler(this.DarkButton_MouseEnter);
            this.btnNextEnroll.MouseLeave += new System.EventHandler(this.DarkButton_MouseLeave);


            // ========================================================================
            // tpSubjects (Enrolled Subjects Step 2)
            // ========================================================================
            this.tpSubjects.BackColor = System.Drawing.Color.White;
            this.tpSubjects.Controls.Add(this.lblBreadcrumbSubjects);
            this.tpSubjects.Controls.Add(this.lblTitleSubjects);
            this.tpSubjects.Controls.Add(this.headerDividerSubjects);
            this.tpSubjects.Controls.Add(this.pnlStepperSubjects);
            this.tpSubjects.Controls.Add(this.lblSubjectsSearch);
            this.tpSubjects.Controls.Add(this.txtSubjectsSearch);
            this.tpSubjects.Controls.Add(this.lblCurriculumYear);
            this.tpSubjects.Controls.Add(this.cboCurriculumYear);
            this.tpSubjects.Controls.Add(this.dgvSubjectsSelect);
            this.tpSubjects.Controls.Add(this.lblTotalSubjectsLabel);
            this.tpSubjects.Controls.Add(this.txtTotalSubjects);
            this.tpSubjects.Controls.Add(this.lblTotalUnitsLabel);
            this.tpSubjects.Controls.Add(this.txtTotalUnits);
            this.tpSubjects.Controls.Add(this.lblMaxUnitsLabel);
            this.tpSubjects.Controls.Add(this.txtMaxUnits);
            this.tpSubjects.Controls.Add(this.btnBackSubjects);
            this.tpSubjects.Controls.Add(this.btnDropSelected);
            this.tpSubjects.Controls.Add(this.btnNextSubjects);
            this.tpSubjects.Location = new System.Drawing.Point(4, 5);
            this.tpSubjects.Padding = new System.Windows.Forms.Padding(16, 18, 16, 18);
            this.tpSubjects.Size = new System.Drawing.Size(864, 522);

            this.lblBreadcrumbSubjects.AutoSize = true;
            this.lblBreadcrumbSubjects.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBreadcrumbSubjects.ForeColor = System.Drawing.Color.Gray;
            this.lblBreadcrumbSubjects.Location = new System.Drawing.Point(16, 10);
            this.lblBreadcrumbSubjects.Text = "Home › Enroll Student › Subjects";

            this.lblTitleSubjects.AutoSize = true;
            this.lblTitleSubjects.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitleSubjects.Location = new System.Drawing.Point(14, 25);
            this.lblTitleSubjects.Text = "Select Subjects — Student Details";

            this.headerDividerSubjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.headerDividerSubjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.headerDividerSubjects.Location = new System.Drawing.Point(16, 56);
            this.headerDividerSubjects.Size = new System.Drawing.Size(830, 2);

            // Stepper
            this.pnlStepperSubjects.Controls.Add(this.lblStep1Subjects);
            this.pnlStepperSubjects.Controls.Add(this.lblStep2Subjects);
            this.pnlStepperSubjects.Controls.Add(this.lblStep3Subjects);
            this.pnlStepperSubjects.Controls.Add(this.lblStep4Subjects);
            this.pnlStepperSubjects.Controls.Add(this.lblStep5Subjects);
            this.pnlStepperSubjects.Location = new System.Drawing.Point(16, 68);
            this.pnlStepperSubjects.Size = new System.Drawing.Size(830, 28);

            this.lblStep1Subjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep1Subjects.ForeColor = System.Drawing.Color.Gray;
            this.lblStep1Subjects.Location = new System.Drawing.Point(0, 0);
            this.lblStep1Subjects.Size = new System.Drawing.Size(160, 28);
            this.lblStep1Subjects.Text = "1 · Student";
            this.lblStep1Subjects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep1Subjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep2Subjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.lblStep2Subjects.ForeColor = System.Drawing.Color.White;
            this.lblStep2Subjects.Location = new System.Drawing.Point(166, 0);
            this.lblStep2Subjects.Size = new System.Drawing.Size(160, 28);
            this.lblStep2Subjects.Text = "2 · Subjects";
            this.lblStep2Subjects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep2Subjects.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblStep3Subjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep3Subjects.ForeColor = System.Drawing.Color.Gray;
            this.lblStep3Subjects.Location = new System.Drawing.Point(332, 0);
            this.lblStep3Subjects.Size = new System.Drawing.Size(160, 28);
            this.lblStep3Subjects.Text = "3 · Section";
            this.lblStep3Subjects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep3Subjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep4Subjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep4Subjects.ForeColor = System.Drawing.Color.Gray;
            this.lblStep4Subjects.Location = new System.Drawing.Point(498, 0);
            this.lblStep4Subjects.Size = new System.Drawing.Size(160, 28);
            this.lblStep4Subjects.Text = "4 · Assessment";
            this.lblStep4Subjects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep4Subjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep5Subjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep5Subjects.ForeColor = System.Drawing.Color.Gray;
            this.lblStep5Subjects.Location = new System.Drawing.Point(664, 0);
            this.lblStep5Subjects.Size = new System.Drawing.Size(166, 28);
            this.lblStep5Subjects.Text = "5 · Confirm";
            this.lblStep5Subjects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep5Subjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Search Fields
            this.lblSubjectsSearch.AutoSize = true;
            this.lblSubjectsSearch.Location = new System.Drawing.Point(16, 108);
            this.lblSubjectsSearch.Text = "Available subjects (from Team 3 — read-only)";

            this.txtSubjectsSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSubjectsSearch.Location = new System.Drawing.Point(16, 126);
            this.txtSubjectsSearch.Size = new System.Drawing.Size(534, 25);
            this.txtSubjectsSearch.TextChanged += new System.EventHandler(this.TxtSubjectsSearch_TextChanged);

            this.lblCurriculumYear.AutoSize = true;
            this.lblCurriculumYear.Location = new System.Drawing.Point(568, 108);
            this.lblCurriculumYear.Text = "Curriculum Year";

            this.cboCurriculumYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurriculumYear.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCurriculumYear.Location = new System.Drawing.Point(568, 126);
            this.cboCurriculumYear.Size = new System.Drawing.Size(278, 25);
            this.cboCurriculumYear.Items.AddRange(new object[] { "All Years", "1st Year", "2nd Year", "3rd Year", "4th Year" });
            this.cboCurriculumYear.SelectedIndex = 0;
            this.cboCurriculumYear.SelectedIndexChanged += new System.EventHandler(this.CboCurriculumYear_SelectedIndexChanged);

            // Grid
            this.dgvSubjectsSelect.AllowUserToAddRows = false;
            this.dgvSubjectsSelect.AllowUserToDeleteRows = false;
            this.dgvSubjectsSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSubjectsSelect.BackgroundColor = System.Drawing.Color.White;
            this.dgvSubjectsSelect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSubjectsSelect.ColumnHeadersHeight = 28;
            this.dgvSubjectsSelect.Location = new System.Drawing.Point(16, 162);
            this.dgvSubjectsSelect.Size = new System.Drawing.Size(830, 235);
            this.dgvSubjectsSelect.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvSubjectsSelect_CellContentClick);

            // Totals Row
            this.lblTotalSubjectsLabel.AutoSize = true;
            this.lblTotalSubjectsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalSubjectsLabel.Location = new System.Drawing.Point(16, 408);
            this.lblTotalSubjectsLabel.Text = "Total Subjects";

            this.txtTotalSubjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalSubjects.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTotalSubjects.Location = new System.Drawing.Point(16, 427);
            this.txtTotalSubjects.Size = new System.Drawing.Size(120, 25);
            this.txtTotalSubjects.ReadOnly = true;
            this.txtTotalSubjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            this.lblTotalUnitsLabel.AutoSize = true;
            this.lblTotalUnitsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalUnitsLabel.Location = new System.Drawing.Point(150, 408);
            this.lblTotalUnitsLabel.Text = "Total Units";

            this.txtTotalUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalUnits.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTotalUnits.Location = new System.Drawing.Point(150, 427);
            this.txtTotalUnits.Size = new System.Drawing.Size(120, 25);
            this.txtTotalUnits.ReadOnly = true;
            this.txtTotalUnits.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            this.lblMaxUnitsLabel.AutoSize = true;
            this.lblMaxUnitsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaxUnitsLabel.Location = new System.Drawing.Point(284, 408);
            this.lblMaxUnitsLabel.Text = "Max Allowed Units";

            this.txtMaxUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMaxUnits.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaxUnits.Location = new System.Drawing.Point(284, 427);
            this.txtMaxUnits.Size = new System.Drawing.Size(130, 25);
            this.txtMaxUnits.ReadOnly = true;
            this.txtMaxUnits.Text = "24";
            this.txtMaxUnits.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            // Action Buttons
            this.btnBackSubjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackSubjects.BackColor = System.Drawing.Color.White;
            this.btnBackSubjects.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackSubjects.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnBackSubjects.FlatAppearance.BorderSize = 2;
            this.btnBackSubjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackSubjects.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBackSubjects.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnBackSubjects.Location = new System.Drawing.Point(16, 465);
            this.btnBackSubjects.Size = new System.Drawing.Size(100, 36);
            this.btnBackSubjects.Text = "← Back";
            this.btnBackSubjects.Click += new System.EventHandler(this.BtnBackSubjects_Click);
            this.btnBackSubjects.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnBackSubjects.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnDropSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDropSelected.BackColor = System.Drawing.Color.White;
            this.btnDropSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDropSelected.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnDropSelected.FlatAppearance.BorderSize = 2;
            this.btnDropSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDropSelected.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDropSelected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnDropSelected.Location = new System.Drawing.Point(126, 465);
            this.btnDropSelected.Size = new System.Drawing.Size(130, 36);
            this.btnDropSelected.Text = "Drop Selected";
            this.btnDropSelected.Click += new System.EventHandler(this.BtnDropSelected_Click);
            this.btnDropSelected.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnDropSelected.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnNextSubjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextSubjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnNextSubjects.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextSubjects.FlatAppearance.BorderSize = 0;
            this.btnNextSubjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextSubjects.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNextSubjects.ForeColor = System.Drawing.Color.White;
            this.btnNextSubjects.Location = new System.Drawing.Point(646, 465);
            this.btnNextSubjects.Size = new System.Drawing.Size(200, 36);
            this.btnNextSubjects.Text = "Next: Assign Sections →";
            this.btnNextSubjects.Click += new System.EventHandler(this.BtnNextSubjects_Click);
            this.btnNextSubjects.MouseEnter += new System.EventHandler(this.DarkButton_MouseEnter);
            this.btnNextSubjects.MouseLeave += new System.EventHandler(this.DarkButton_MouseLeave);


            // ========================================================================
            // tpSection (Section Assignment Step 3)
            // ========================================================================
            this.tpSection.BackColor = System.Drawing.Color.White;
            this.tpSection.Controls.Add(this.lblBreadcrumbSection);
            this.tpSection.Controls.Add(this.lblTitleSection);
            this.tpSection.Controls.Add(this.headerDividerSection);
            this.tpSection.Controls.Add(this.pnlStepperSection);
            this.tpSection.Controls.Add(this.dgvSectionAssign);
            this.tpSection.Controls.Add(this.pnlConflictChecker);
            this.tpSection.Controls.Add(this.btnBackSection);
            this.tpSection.Controls.Add(this.btnAutoAssign);
            this.tpSection.Controls.Add(this.btnNextSection);
            this.tpSection.Location = new System.Drawing.Point(4, 5);
            this.tpSection.Padding = new System.Windows.Forms.Padding(16, 18, 16, 18);
            this.tpSection.Size = new System.Drawing.Size(864, 522);

            this.lblBreadcrumbSection.AutoSize = true;
            this.lblBreadcrumbSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBreadcrumbSection.ForeColor = System.Drawing.Color.Gray;
            this.lblBreadcrumbSection.Location = new System.Drawing.Point(16, 10);
            this.lblBreadcrumbSection.Text = "Home › Enroll Student › Sections";

            this.lblTitleSection.AutoSize = true;
            this.lblTitleSection.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitleSection.Location = new System.Drawing.Point(14, 25);
            this.lblTitleSection.Text = "Assign Section per Subject";

            this.headerDividerSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.headerDividerSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.headerDividerSection.Location = new System.Drawing.Point(16, 56);
            this.headerDividerSection.Size = new System.Drawing.Size(830, 2);

            // Stepper
            this.pnlStepperSection.Controls.Add(this.lblStep1Section);
            this.pnlStepperSection.Controls.Add(this.lblStep2Section);
            this.pnlStepperSection.Controls.Add(this.lblStep3Section);
            this.pnlStepperSection.Controls.Add(this.lblStep4Section);
            this.pnlStepperSection.Controls.Add(this.lblStep5Section);
            this.pnlStepperSection.Location = new System.Drawing.Point(16, 68);
            this.pnlStepperSection.Size = new System.Drawing.Size(830, 28);

            this.lblStep1Section.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep1Section.ForeColor = System.Drawing.Color.Gray;
            this.lblStep1Section.Location = new System.Drawing.Point(0, 0);
            this.lblStep1Section.Size = new System.Drawing.Size(160, 28);
            this.lblStep1Section.Text = "1 · Student";
            this.lblStep1Section.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep1Section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep2Section.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep2Section.ForeColor = System.Drawing.Color.Gray;
            this.lblStep2Section.Location = new System.Drawing.Point(166, 0);
            this.lblStep2Section.Size = new System.Drawing.Size(160, 28);
            this.lblStep2Section.Text = "2 · Subjects";
            this.lblStep2Section.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep2Section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep3Section.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.lblStep3Section.ForeColor = System.Drawing.Color.White;
            this.lblStep3Section.Location = new System.Drawing.Point(332, 0);
            this.lblStep3Section.Size = new System.Drawing.Size(160, 28);
            this.lblStep3Section.Text = "3 · Section";
            this.lblStep3Section.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep3Section.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblStep4Section.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep4Section.ForeColor = System.Drawing.Color.Gray;
            this.lblStep4Section.Location = new System.Drawing.Point(498, 0);
            this.lblStep4Section.Size = new System.Drawing.Size(160, 28);
            this.lblStep4Section.Text = "4 · Assessment";
            this.lblStep4Section.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep4Section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep5Section.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep5Section.ForeColor = System.Drawing.Color.Gray;
            this.lblStep5Section.Location = new System.Drawing.Point(664, 0);
            this.lblStep5Section.Size = new System.Drawing.Size(166, 28);
            this.lblStep5Section.Text = "5 · Confirm";
            this.lblStep5Section.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep5Section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Grid
            this.dgvSectionAssign.AllowUserToAddRows = false;
            this.dgvSectionAssign.AllowUserToDeleteRows = false;
            this.dgvSectionAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSectionAssign.BackgroundColor = System.Drawing.Color.White;
            this.dgvSectionAssign.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSectionAssign.ColumnHeadersHeight = 28;
            this.dgvSectionAssign.Location = new System.Drawing.Point(16, 112);
            this.dgvSectionAssign.Size = new System.Drawing.Size(830, 200);

            // Conflict Checker panel
            this.pnlConflictChecker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlConflictChecker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.pnlConflictChecker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlConflictChecker.Controls.Add(this.lblConflictHeader);
            this.pnlConflictChecker.Controls.Add(this.lblConflictMessage);
            this.pnlConflictChecker.Location = new System.Drawing.Point(16, 325);
            this.pnlConflictChecker.Size = new System.Drawing.Size(830, 115);

            this.lblConflictHeader.AutoSize = true;
            this.lblConflictHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConflictHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.lblConflictHeader.Location = new System.Drawing.Point(10, 10);
            this.lblConflictHeader.Text = "Conflict Checker";

            this.lblConflictMessage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConflictMessage.ForeColor = System.Drawing.Color.DarkRed;
            this.lblConflictMessage.Location = new System.Drawing.Point(10, 30);
            this.lblConflictMessage.Size = new System.Drawing.Size(810, 75);
            this.lblConflictMessage.Text = "✓ No schedule or section capacity conflicts detected.";

            // Actions
            this.btnBackSection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackSection.BackColor = System.Drawing.Color.White;
            this.btnBackSection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackSection.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnBackSection.FlatAppearance.BorderSize = 2;
            this.btnBackSection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackSection.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBackSection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnBackSection.Location = new System.Drawing.Point(16, 465);
            this.btnBackSection.Size = new System.Drawing.Size(100, 36);
            this.btnBackSection.Text = "← Back";
            this.btnBackSection.Click += new System.EventHandler(this.BtnBackSection_Click);
            this.btnBackSection.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnBackSection.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnAutoAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAutoAssign.BackColor = System.Drawing.Color.White;
            this.btnAutoAssign.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutoAssign.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnAutoAssign.FlatAppearance.BorderSize = 2;
            this.btnAutoAssign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoAssign.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAutoAssign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnAutoAssign.Location = new System.Drawing.Point(126, 465);
            this.btnAutoAssign.Size = new System.Drawing.Size(110, 36);
            this.btnAutoAssign.Text = "Auto-Assign";
            this.btnAutoAssign.Click += new System.EventHandler(this.BtnAutoAssign_Click);
            this.btnAutoAssign.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnAutoAssign.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnNextSection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnNextSection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextSection.FlatAppearance.BorderSize = 0;
            this.btnNextSection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextSection.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNextSection.ForeColor = System.Drawing.Color.White;
            this.btnNextSection.Location = new System.Drawing.Point(646, 465);
            this.btnNextSection.Size = new System.Drawing.Size(200, 36);
            this.btnNextSection.Text = "Next: Assessment →";
            this.btnNextSection.Click += new System.EventHandler(this.BtnNextSection_Click);
            this.btnNextSection.MouseEnter += new System.EventHandler(this.DarkButton_MouseEnter);
            this.btnNextSection.MouseLeave += new System.EventHandler(this.DarkButton_MouseLeave);


            // ========================================================================
            // tpAssessment (Tuition Assessment Step 4 & 5)
            // ========================================================================
            this.tpAssessment.BackColor = System.Drawing.Color.White;
            this.tpAssessment.Controls.Add(this.lblBreadcrumbAssessment);
            this.tpAssessment.Controls.Add(this.lblTitleAssessment);
            this.tpAssessment.Controls.Add(this.headerDividerAssessment);
            this.tpAssessment.Controls.Add(this.pnlStepperAssessment);
            this.tpAssessment.Controls.Add(this.lblAssessedUnitsLabel);
            this.tpAssessment.Controls.Add(this.txtAssessedUnits);
            this.tpAssessment.Controls.Add(this.lblRatePerUnitLabel);
            this.tpAssessment.Controls.Add(this.txtRatePerUnit);
            this.tpAssessment.Controls.Add(this.lblLabUnitsLabel);
            this.tpAssessment.Controls.Add(this.txtLabUnits);
            this.tpAssessment.Controls.Add(this.lblPaymentPlan);
            this.tpAssessment.Controls.Add(this.cboPaymentPlan);
            this.tpAssessment.Controls.Add(this.dgvFeesBreakdown);
            this.tpAssessment.Controls.Add(this.lblAmountPaidLabel);
            this.tpAssessment.Controls.Add(this.txtAmountPaid);
            this.tpAssessment.Controls.Add(this.btnBackAssessment);
            this.tpAssessment.Controls.Add(this.btnPrintAssessment);
            this.tpAssessment.Controls.Add(this.btnConfirmEnrollment);
            this.tpAssessment.Location = new System.Drawing.Point(4, 5);
            this.tpAssessment.Padding = new System.Windows.Forms.Padding(16, 18, 16, 18);
            this.tpAssessment.Size = new System.Drawing.Size(864, 522);

            this.lblBreadcrumbAssessment.AutoSize = true;
            this.lblBreadcrumbAssessment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBreadcrumbAssessment.ForeColor = System.Drawing.Color.Gray;
            this.lblBreadcrumbAssessment.Location = new System.Drawing.Point(16, 10);
            this.lblBreadcrumbAssessment.Text = "Home › Enroll Student › Assessment";

            this.lblTitleAssessment.AutoSize = true;
            this.lblTitleAssessment.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitleAssessment.Location = new System.Drawing.Point(14, 25);
            this.lblTitleAssessment.Text = "Assessment of Fees — Summary Details";

            this.headerDividerAssessment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.headerDividerAssessment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.headerDividerAssessment.Location = new System.Drawing.Point(16, 56);
            this.headerDividerAssessment.Size = new System.Drawing.Size(830, 2);

            // Stepper
            this.pnlStepperAssessment.Controls.Add(this.lblStep1Assessment);
            this.pnlStepperAssessment.Controls.Add(this.lblStep2Assessment);
            this.pnlStepperAssessment.Controls.Add(this.lblStep3Assessment);
            this.pnlStepperAssessment.Controls.Add(this.lblStep4Assessment);
            this.pnlStepperAssessment.Controls.Add(this.lblStep5Assessment);
            this.pnlStepperAssessment.Location = new System.Drawing.Point(16, 68);
            this.pnlStepperAssessment.Size = new System.Drawing.Size(830, 28);

            this.lblStep1Assessment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep1Assessment.ForeColor = System.Drawing.Color.Gray;
            this.lblStep1Assessment.Location = new System.Drawing.Point(0, 0);
            this.lblStep1Assessment.Size = new System.Drawing.Size(160, 28);
            this.lblStep1Assessment.Text = "1 · Student";
            this.lblStep1Assessment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep1Assessment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep2Assessment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep2Assessment.ForeColor = System.Drawing.Color.Gray;
            this.lblStep2Assessment.Location = new System.Drawing.Point(166, 0);
            this.lblStep2Assessment.Size = new System.Drawing.Size(160, 28);
            this.lblStep2Assessment.Text = "2 · Subjects";
            this.lblStep2Assessment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep2Assessment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep3Assessment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.lblStep3Assessment.ForeColor = System.Drawing.Color.Gray;
            this.lblStep3Assessment.Location = new System.Drawing.Point(332, 0);
            this.lblStep3Assessment.Size = new System.Drawing.Size(160, 28);
            this.lblStep3Assessment.Text = "3 · Section";
            this.lblStep3Assessment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep3Assessment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblStep4Assessment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.lblStep4Assessment.ForeColor = System.Drawing.Color.White;
            this.lblStep4Assessment.Location = new System.Drawing.Point(498, 0);
            this.lblStep4Assessment.Size = new System.Drawing.Size(160, 28);
            this.lblStep4Assessment.Text = "4 · Assessment";
            this.lblStep4Assessment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep4Assessment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblStep5Assessment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.lblStep5Assessment.ForeColor = System.Drawing.Color.White;
            this.lblStep5Assessment.Location = new System.Drawing.Point(664, 0);
            this.lblStep5Assessment.Size = new System.Drawing.Size(166, 28);
            this.lblStep5Assessment.Text = "5 · Confirm";
            this.lblStep5Assessment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStep5Assessment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // Row 1 inputs
            this.lblAssessedUnitsLabel.AutoSize = true;
            this.lblAssessedUnitsLabel.Location = new System.Drawing.Point(16, 108);
            this.lblAssessedUnitsLabel.Text = "Total Units";

            this.txtAssessedUnits.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAssessedUnits.Location = new System.Drawing.Point(16, 126);
            this.txtAssessedUnits.Size = new System.Drawing.Size(195, 25);
            this.txtAssessedUnits.ReadOnly = true;
            this.txtAssessedUnits.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            this.lblRatePerUnitLabel.AutoSize = true;
            this.lblRatePerUnitLabel.Location = new System.Drawing.Point(223, 108);
            this.lblRatePerUnitLabel.Text = "Rate per Unit";

            this.txtRatePerUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRatePerUnit.Location = new System.Drawing.Point(223, 126);
            this.txtRatePerUnit.Size = new System.Drawing.Size(195, 25);
            this.txtRatePerUnit.ReadOnly = true;
            this.txtRatePerUnit.Text = "500.00";
            this.txtRatePerUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            this.lblLabUnitsLabel.AutoSize = true;
            this.lblLabUnitsLabel.Location = new System.Drawing.Point(430, 108);
            this.lblLabUnitsLabel.Text = "Lab Units";

            this.txtLabUnits.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLabUnits.Location = new System.Drawing.Point(430, 126);
            this.txtLabUnits.Size = new System.Drawing.Size(195, 25);
            this.txtLabUnits.ReadOnly = true;
            this.txtLabUnits.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            this.lblPaymentPlan.AutoSize = true;
            this.lblPaymentPlan.Location = new System.Drawing.Point(637, 108);
            this.lblPaymentPlan.Text = "Payment Plan";

            this.cboPaymentPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentPlan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboPaymentPlan.Location = new System.Drawing.Point(637, 126);
            this.cboPaymentPlan.Size = new System.Drawing.Size(209, 25);
            this.cboPaymentPlan.Items.AddRange(new object[] { "Full", "Installment" });
            this.cboPaymentPlan.SelectedIndex = 0;

            // Fees breakdown Grid
            this.dgvFeesBreakdown.AllowUserToAddRows = false;
            this.dgvFeesBreakdown.AllowUserToDeleteRows = false;
            this.dgvFeesBreakdown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFeesBreakdown.BackgroundColor = System.Drawing.Color.White;
            this.dgvFeesBreakdown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFeesBreakdown.ColumnHeadersHeight = 28;
            this.dgvFeesBreakdown.Location = new System.Drawing.Point(16, 162);
            this.dgvFeesBreakdown.Size = new System.Drawing.Size(830, 200);

            // Amount Paid Input
            this.lblAmountPaidLabel.AutoSize = true;
            this.lblAmountPaidLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAmountPaidLabel.Location = new System.Drawing.Point(546, 378);
            this.lblAmountPaidLabel.Text = "Amount Paid (Input):";

            this.txtAmountPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAmountPaid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.txtAmountPaid.Location = new System.Drawing.Point(680, 374);
            this.txtAmountPaid.Size = new System.Drawing.Size(166, 27);
            this.txtAmountPaid.Text = "0.00";
            this.txtAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountPaid.TextChanged += new System.EventHandler(this.TxtAmountPaid_TextChanged);

            // Actions
            this.btnBackAssessment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackAssessment.BackColor = System.Drawing.Color.White;
            this.btnBackAssessment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackAssessment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnBackAssessment.FlatAppearance.BorderSize = 2;
            this.btnBackAssessment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackAssessment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBackAssessment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnBackAssessment.Location = new System.Drawing.Point(16, 465);
            this.btnBackAssessment.Size = new System.Drawing.Size(100, 36);
            this.btnBackAssessment.Text = "← Back";
            this.btnBackAssessment.Click += new System.EventHandler(this.BtnBackAssessment_Click);
            this.btnBackAssessment.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnBackAssessment.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnPrintAssessment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintAssessment.BackColor = System.Drawing.Color.White;
            this.btnPrintAssessment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrintAssessment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnPrintAssessment.FlatAppearance.BorderSize = 2;
            this.btnPrintAssessment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintAssessment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPrintAssessment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnPrintAssessment.Location = new System.Drawing.Point(126, 465);
            this.btnPrintAssessment.Size = new System.Drawing.Size(140, 36);
            this.btnPrintAssessment.Text = "Print Assessment";
            this.btnPrintAssessment.Click += new System.EventHandler(this.BtnPrintAssessment_Click);
            this.btnPrintAssessment.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnPrintAssessment.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnConfirmEnrollment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmEnrollment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnConfirmEnrollment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmEnrollment.FlatAppearance.BorderSize = 0;
            this.btnConfirmEnrollment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmEnrollment.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnConfirmEnrollment.ForeColor = System.Drawing.Color.White;
            this.btnConfirmEnrollment.Location = new System.Drawing.Point(646, 465);
            this.btnConfirmEnrollment.Size = new System.Drawing.Size(200, 36);
            this.btnConfirmEnrollment.Text = "Confirm Enrollment";
            this.btnConfirmEnrollment.Click += new System.EventHandler(this.BtnConfirmEnrollment_Click);
            this.btnConfirmEnrollment.MouseEnter += new System.EventHandler(this.DarkButton_MouseEnter);
            this.btnConfirmEnrollment.MouseLeave += new System.EventHandler(this.DarkButton_MouseLeave);


            // ========================================================================
            // tpHistory (Enrollment History Screen)
            // ========================================================================
            this.tpHistory.BackColor = System.Drawing.Color.White;
            this.tpHistory.Controls.Add(this.lblBreadcrumbHistory);
            this.tpHistory.Controls.Add(this.lblTitleHistory);
            this.tpHistory.Controls.Add(this.headerDividerHistory);
            this.tpHistory.Controls.Add(this.lblHistorySearch);
            this.tpHistory.Controls.Add(this.txtHistorySearch);
            this.tpHistory.Controls.Add(this.lblHistorySY);
            this.tpHistory.Controls.Add(this.cboHistorySY);
            this.tpHistory.Controls.Add(this.lblHistoryAction);
            this.tpHistory.Controls.Add(this.cboHistoryAction);
            this.tpHistory.Controls.Add(this.btnHistoryFilter);
            this.tpHistory.Controls.Add(this.dgvHistory);
            this.tpHistory.Controls.Add(this.btnHistoryView);
            this.tpHistory.Controls.Add(this.btnHistoryExport);
            this.tpHistory.Location = new System.Drawing.Point(4, 5);
            this.tpHistory.Padding = new System.Windows.Forms.Padding(16, 18, 16, 18);
            this.tpHistory.Size = new System.Drawing.Size(864, 522);

            this.lblBreadcrumbHistory.AutoSize = true;
            this.lblBreadcrumbHistory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBreadcrumbHistory.ForeColor = System.Drawing.Color.Gray;
            this.lblBreadcrumbHistory.Location = new System.Drawing.Point(16, 10);
            this.lblBreadcrumbHistory.Text = "Home › Enrollment History";

            this.lblTitleHistory.AutoSize = true;
            this.lblTitleHistory.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitleHistory.Location = new System.Drawing.Point(14, 25);
            this.lblTitleHistory.Text = "Enrollment History";

            this.headerDividerHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.headerDividerHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.headerDividerHistory.Location = new System.Drawing.Point(16, 56);
            this.headerDividerHistory.Size = new System.Drawing.Size(830, 2);

            // History filter controls
            this.lblHistorySearch.AutoSize = true;
            this.lblHistorySearch.Location = new System.Drawing.Point(16, 68);
            this.lblHistorySearch.Text = "Student No. / Name";

            this.txtHistorySearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHistorySearch.Location = new System.Drawing.Point(16, 88);
            this.txtHistorySearch.Size = new System.Drawing.Size(350, 25);

            this.lblHistorySY.AutoSize = true;
            this.lblHistorySY.Location = new System.Drawing.Point(380, 68);
            this.lblHistorySY.Text = "School Year";

            this.cboHistorySY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHistorySY.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHistorySY.Location = new System.Drawing.Point(380, 88);
            this.cboHistorySY.Size = new System.Drawing.Size(140, 25);
            this.cboHistorySY.Items.AddRange(new object[] { "All", "2025-2026", "2026-2027" });
            this.cboHistorySY.SelectedIndex = 0;

            this.lblHistoryAction.AutoSize = true;
            this.lblHistoryAction.Location = new System.Drawing.Point(535, 68);
            this.lblHistoryAction.Text = "Action";

            this.cboHistoryAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHistoryAction.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHistoryAction.Location = new System.Drawing.Point(535, 88);
            this.cboHistoryAction.Size = new System.Drawing.Size(180, 25);
            this.cboHistoryAction.Items.AddRange(new object[] { "All", "Enrolled", "Subject Dropped", "Section Changed", "Completed" });
            this.cboHistoryAction.SelectedIndex = 0;

            this.btnHistoryFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnHistoryFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistoryFilter.FlatAppearance.BorderSize = 0;
            this.btnHistoryFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistoryFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHistoryFilter.ForeColor = System.Drawing.Color.White;
            this.btnHistoryFilter.Location = new System.Drawing.Point(730, 87);
            this.btnHistoryFilter.Size = new System.Drawing.Size(116, 27);
            this.btnHistoryFilter.Text = "Filter";
            this.btnHistoryFilter.Click += new System.EventHandler(this.BtnHistoryFilter_Click);
            this.btnHistoryFilter.MouseEnter += new System.EventHandler(this.DarkButton_MouseEnter);
            this.btnHistoryFilter.MouseLeave += new System.EventHandler(this.DarkButton_MouseLeave);

            // History Grid
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvHistory.ColumnHeadersHeight = 28;
            this.dgvHistory.Location = new System.Drawing.Point(16, 128);
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(830, 320);
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;

            // History Actions
            this.btnHistoryView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHistoryView.BackColor = System.Drawing.Color.White;
            this.btnHistoryView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistoryView.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnHistoryView.FlatAppearance.BorderSize = 2;
            this.btnHistoryView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistoryView.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHistoryView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnHistoryView.Location = new System.Drawing.Point(16, 465);
            this.btnHistoryView.Size = new System.Drawing.Size(150, 36);
            this.btnHistoryView.Text = "View Enrollment";
            this.btnHistoryView.Click += new System.EventHandler(this.BtnHistoryView_Click);
            this.btnHistoryView.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnHistoryView.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.btnHistoryExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHistoryExport.BackColor = System.Drawing.Color.White;
            this.btnHistoryExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistoryExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnHistoryExport.FlatAppearance.BorderSize = 2;
            this.btnHistoryExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistoryExport.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHistoryExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnHistoryExport.Location = new System.Drawing.Point(176, 465);
            this.btnHistoryExport.Size = new System.Drawing.Size(120, 36);
            this.btnHistoryExport.Text = "Export CSV";
            this.btnHistoryExport.Click += new System.EventHandler(this.BtnHistoryExport_Click);
            this.btnHistoryExport.MouseEnter += new System.EventHandler(this.OutlineButton_MouseEnter);
            this.btnHistoryExport.MouseLeave += new System.EventHandler(this.OutlineButton_MouseLeave);

            this.sidebarPanel.ResumeLayout(false);
            this.logoPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.tcPages.ResumeLayout(false);
            this.tpDashboard.ResumeLayout(false);
            this.tpDashboard.PerformLayout();
            this.cardEnrolled.ResumeLayout(false);
            this.cardEnrolled.PerformLayout();
            this.cardPending.ResumeLayout(false);
            this.cardPending.PerformLayout();
            this.cardUnpaid.ResumeLayout(false);
            this.cardUnpaid.PerformLayout();
            this.cardOpenSections.ResumeLayout(false);
            this.cardOpenSections.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnrollments)).EndInit();
            this.tpEnroll.ResumeLayout(false);
            this.tpEnroll.PerformLayout();
            this.pnlStepperEnroll.ResumeLayout(false);
            this.tpSubjects.ResumeLayout(false);
            this.tpSubjects.PerformLayout();
            this.pnlStepperSubjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjectsSelect)).EndInit();
            this.tpSection.ResumeLayout(false);
            this.tpSection.PerformLayout();
            this.pnlStepperSection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSectionAssign)).EndInit();
            this.pnlConflictChecker.ResumeLayout(false);
            this.pnlConflictChecker.PerformLayout();
            this.tpAssessment.ResumeLayout(false);
            this.tpAssessment.PerformLayout();
            this.pnlStepperAssessment.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeesBreakdown)).EndInit();
            this.tpHistory.ResumeLayout(false);
            this.tpHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnNavDashboard;
        private System.Windows.Forms.Button btnNavEnrollStudent;
        private System.Windows.Forms.Button btnNavEnrolledSubjects;
        private System.Windows.Forms.Button btnNavSectionAssignment;
        private System.Windows.Forms.Button btnNavTuitionAssessment;
        private System.Windows.Forms.Button btnNavEnrollmentHistory;
        private System.Windows.Forms.Panel sidebarDivider;
        private System.Windows.Forms.Panel contentPanel;
        
        // TabControl container
        private System.Windows.Forms.TabControl tcPages;
        private System.Windows.Forms.TabPage tpDashboard;
        private System.Windows.Forms.TabPage tpEnroll;
        private System.Windows.Forms.TabPage tpSubjects;
        private System.Windows.Forms.TabPage tpSection;
        private System.Windows.Forms.TabPage tpAssessment;
        private System.Windows.Forms.TabPage tpHistory;

        // ================= Dashboard Screen =================
        private System.Windows.Forms.LinkLabel lblBreadcrumb;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel headerDivider;
        private System.Windows.Forms.Panel cardEnrolled;
        private System.Windows.Forms.Label lblEnrolledCount;
        private System.Windows.Forms.Label lblEnrolledLabel;
        private System.Windows.Forms.Panel cardPending;
        private System.Windows.Forms.Label lblPendingCount;
        private System.Windows.Forms.Label lblPendingLabel;
        private System.Windows.Forms.Panel cardUnpaid;
        private System.Windows.Forms.Label lblUnpaidCount;
        private System.Windows.Forms.Label lblUnpaidLabel;
        private System.Windows.Forms.Panel cardOpenSections;
        private System.Windows.Forms.Label lblOpenSectionsCount;
        private System.Windows.Forms.Label lblOpenSectionsLabel;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblTerm;
        private System.Windows.Forms.ComboBox cboTerm;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvEnrollments;
        private System.Windows.Forms.Button btnNewEnrollment;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnUpdateEnrollment;
        private System.Windows.Forms.Button btnRefresh;

        // ================= Enroll Student Step 1 =================
        private System.Windows.Forms.LinkLabel lblBreadcrumbEnroll;
        private System.Windows.Forms.Label lblTitleEnroll;
        private System.Windows.Forms.Panel headerDividerEnroll;
        private System.Windows.Forms.Panel pnlStepperEnroll;
        private System.Windows.Forms.Label lblStep1Enroll;
        private System.Windows.Forms.Label lblStep2Enroll;
        private System.Windows.Forms.Label lblStep3Enroll;
        private System.Windows.Forms.Label lblStep4Enroll;
        private System.Windows.Forms.Label lblStep5Enroll;
        private System.Windows.Forms.Label lblStudentNo;
        private System.Windows.Forms.TextBox txtStudentNo;
        private System.Windows.Forms.Button btnLoadStudent;
        private System.Windows.Forms.DataGridView dgvStudentsList;
        private System.Windows.Forms.Label lblFullNameLabel;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblProgramLabel;
        private System.Windows.Forms.TextBox txtProgram;
        private System.Windows.Forms.Label lblYearLevelLabel;
        private System.Windows.Forms.TextBox txtYearLevel;
        private System.Windows.Forms.Label lblSchoolYearEnroll;
        private System.Windows.Forms.ComboBox cboSchoolYearEnroll;
        private System.Windows.Forms.Label lblSemesterEnroll;
        private System.Windows.Forms.ComboBox cboSemesterEnroll;
        private System.Windows.Forms.Label lblEnrollmentType;
        private System.Windows.Forms.ComboBox cboEnrollmentType;
        private System.Windows.Forms.Label lblStudentType;
        private System.Windows.Forms.ComboBox cboStudentType;
        private System.Windows.Forms.Button btnCancelEnroll;
        private System.Windows.Forms.Button btnNextEnroll;

        // ================= Enrolled Subjects Step 2 =================
        private System.Windows.Forms.LinkLabel lblBreadcrumbSubjects;
        private System.Windows.Forms.Label lblTitleSubjects;
        private System.Windows.Forms.Panel headerDividerSubjects;
        private System.Windows.Forms.Panel pnlStepperSubjects;
        private System.Windows.Forms.Label lblStep1Subjects;
        private System.Windows.Forms.Label lblStep2Subjects;
        private System.Windows.Forms.Label lblStep3Subjects;
        private System.Windows.Forms.Label lblStep4Subjects;
        private System.Windows.Forms.Label lblStep5Subjects;
        private System.Windows.Forms.Label lblSubjectsSearch;
        private System.Windows.Forms.TextBox txtSubjectsSearch;
        private System.Windows.Forms.Label lblCurriculumYear;
        private System.Windows.Forms.ComboBox cboCurriculumYear;
        private System.Windows.Forms.DataGridView dgvSubjectsSelect;
        private System.Windows.Forms.Label lblTotalSubjectsLabel;
        private System.Windows.Forms.TextBox txtTotalSubjects;
        private System.Windows.Forms.Label lblTotalUnitsLabel;
        private System.Windows.Forms.TextBox txtTotalUnits;
        private System.Windows.Forms.Label lblMaxUnitsLabel;
        private System.Windows.Forms.TextBox txtMaxUnits;
        private System.Windows.Forms.Button btnBackSubjects;
        private System.Windows.Forms.Button btnDropSelected;
        private System.Windows.Forms.Button btnNextSubjects;

        // ================= Section Assignment Step 3 =================
        private System.Windows.Forms.LinkLabel lblBreadcrumbSection;
        private System.Windows.Forms.Label lblTitleSection;
        private System.Windows.Forms.Panel headerDividerSection;
        private System.Windows.Forms.Panel pnlStepperSection;
        private System.Windows.Forms.Label lblStep1Section;
        private System.Windows.Forms.Label lblStep2Section;
        private System.Windows.Forms.Label lblStep3Section;
        private System.Windows.Forms.Label lblStep4Section;
        private System.Windows.Forms.Label lblStep5Section;
        private System.Windows.Forms.DataGridView dgvSectionAssign;
        private System.Windows.Forms.Panel pnlConflictChecker;
        private System.Windows.Forms.Label lblConflictHeader;
        private System.Windows.Forms.Label lblConflictMessage;
        private System.Windows.Forms.Button btnBackSection;
        private System.Windows.Forms.Button btnAutoAssign;
        private System.Windows.Forms.Button btnNextSection;

        // ================= Tuition Assessment Step 4 & 5 =================
        private System.Windows.Forms.LinkLabel lblBreadcrumbAssessment;
        private System.Windows.Forms.Label lblTitleAssessment;
        private System.Windows.Forms.Panel headerDividerAssessment;
        private System.Windows.Forms.Panel pnlStepperAssessment;
        private System.Windows.Forms.Label lblStep1Assessment;
        private System.Windows.Forms.Label lblStep2Assessment;
        private System.Windows.Forms.Label lblStep3Assessment;
        private System.Windows.Forms.Label lblStep4Assessment;
        private System.Windows.Forms.Label lblStep5Assessment;
        private System.Windows.Forms.Label lblAssessedUnitsLabel;
        private System.Windows.Forms.TextBox txtAssessedUnits;
        private System.Windows.Forms.Label lblRatePerUnitLabel;
        private System.Windows.Forms.TextBox txtRatePerUnit;
        private System.Windows.Forms.Label lblLabUnitsLabel;
        private System.Windows.Forms.TextBox txtLabUnits;
        private System.Windows.Forms.Label lblPaymentPlan;
        private System.Windows.Forms.ComboBox cboPaymentPlan;
        private System.Windows.Forms.DataGridView dgvFeesBreakdown;
        private System.Windows.Forms.Label lblAmountPaidLabel;
        private System.Windows.Forms.TextBox txtAmountPaid;
        private System.Windows.Forms.Button btnBackAssessment;
        private System.Windows.Forms.Button btnPrintAssessment;
        private System.Windows.Forms.Button btnConfirmEnrollment;

        // ================= Enrollment History Screen =================
        private System.Windows.Forms.LinkLabel lblBreadcrumbHistory;
        private System.Windows.Forms.Label lblTitleHistory;
        private System.Windows.Forms.Panel headerDividerHistory;
        private System.Windows.Forms.Label lblHistorySearch;
        private System.Windows.Forms.TextBox txtHistorySearch;
        private System.Windows.Forms.Label lblHistorySY;
        private System.Windows.Forms.ComboBox cboHistorySY;
        private System.Windows.Forms.Label lblHistoryAction;
        private System.Windows.Forms.ComboBox cboHistoryAction;
        private System.Windows.Forms.Button btnHistoryFilter;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button btnHistoryView;
        private System.Windows.Forms.Button btnHistoryExport;
    }
}
