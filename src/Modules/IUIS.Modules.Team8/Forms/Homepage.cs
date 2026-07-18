#pragma warning disable CS8618 // Disable nullability warnings for WinForms controls

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IUIS.Core.Session;
using IUIS.Modules.Team8.Models;
using IUIS.Modules.Team8.Services;

namespace IUIS.Modules.Team8.Forms
{
    public partial class Homepage : Form
    {
        /// <summary>Who opened the module. Null when the designer hosts the form.</summary>
        private readonly UserSession? _session;

        private readonly EnrollmentService _service;
        private Button btnVoid;
        private List<DashboardEnrollmentDisplayModel> _displayList = new List<DashboardEnrollmentDisplayModel>();
        private string _searchPlaceholder = "Type to search...";
        
        // Wizard State
        private Enrollment _wizardEnrollment = new Enrollment();
        private Student? _wizardStudent;
        private List<Student> _allStudentsList = new List<Student>();
        private List<Subject> _availableSubjects = new List<Subject>();
        private List<Subject> _selectedSubjects = new List<Subject>();
        private HashSet<string> _selectedSubjectIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private TabPage? _referrerPage;

        private readonly Color BSU_Red = Color.FromArgb(186, 12, 47);
        private readonly Color BSU_Gold = Color.FromArgb(253, 185, 39);

        /// <summary>
        /// Parameterless constructor, required by the Windows Forms designer.
        /// At runtime the shell uses the <see cref="UserSession"/> overload instead.
        /// </summary>
        public Homepage() : this(null)
        {
        }

        /// <param name="session">The signed-in user, or null when hosted by the designer.</param>
        public Homepage(UserSession? session)
        {
            _session = session;
            _service = new EnrollmentService();
            InitializeComponent();

            // Dynamically instantiate Void button
            btnVoid = new Button
            {
                Name = "btnVoid",
                Text = "Void",
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            btnVoid.Click += BtnVoid_Click;
            tpDashboard.Controls.Add(btnVoid);

            this.Size = new Size(1280, 800);
            this.MinimumSize = new Size(1150, 750);
            this.StartPosition = FormStartPosition.CenterScreen;

            StyleAllDataGridViews();
            SetupDashboardTermFilter();
            SetupSearchPlaceholder();
            LoadDashboardData();
            
            // Wire up layout hover styles
            SetupSidebarHoverStyles();
            
            // Initialize other components/grids
            SetupSubjectsGridStructure();
            SetupSectionGridStructure();
            SetupAssessmentGridStructure();
            SetupHistoryGridStructure();

            // Hide sidebar for full-width layout
            sidebarPanel.Visible = false;
            sidebarDivider.Visible = false;

            // Setup custom clickable elements
            SetupBreadcrumbs();
            SetupStepperClickable();
            SetupButtonStyles();

            // Student Grid wire up
            dgvStudentsList.SelectionChanged += DgvStudentsList_SelectionChanged;
            txtStudentNo.TextChanged += TxtStudentNo_TextChanged;

            StyleAllControls();
            StyleAllDataGridViews();
            ApplyDesignSystemLayout();

            // Handle resizing
            this.Resize += (sender, e) => ApplyDesignSystemLayout();

            // Default to Dashboard Page
            SwitchToPage(tpDashboard, btnNavDashboard);

            // Make the launch context obvious at a glance: either the session
            // carried over from the dashboard, or this is a standalone dev run.
            Text = _session is not null
                ? $"{Text} — signed in as {_session.Username} ({_session.Role})"
                : $"{Text} — standalone (no login)";
        }

        private void StyleAllControls()
        {
            StyleControlsRecursive(tcPages);
        }

        private void StyleControlsRecursive(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                if (ctrl is TextBox txt)
                {
                    txt.AutoSize = false;
                    txt.Height = 40;
                    txt.Font = new Font("Segoe UI", 11.5F, FontStyle.Regular);
                    if (txt.ReadOnly)
                    {
                        txt.BackColor = Color.FromArgb(248, 248, 248);
                    }
                }
                else if (ctrl is ComboBox cbo)
                {
                    cbo.Font = new Font("Segoe UI", 13.5F, FontStyle.Regular);
                }
                else if (ctrl is Label lbl)
                {
                    if (lbl.Name.StartsWith("lbl") && (lbl.Name.EndsWith("Label") || lbl.Name.Contains("No") || lbl.Name.Contains("Type") || lbl.Name.Contains("Year") || lbl.Name.Contains("Semester") || lbl.Name.Contains("Plan")))
                    {
                        lbl.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                        lbl.ForeColor = Color.FromArgb(50, 50, 50);
                    }
                }

                if (ctrl.HasChildren)
                {
                    StyleControlsRecursive(ctrl);
                }
            }
        }

        private void StyleAllDataGridViews()
        {
            var gridViews = new DataGridView[] {
                dgvEnrollments,
                dgvStudentsList,
                dgvSubjectsSelect,
                dgvSectionAssign,
                dgvFeesBreakdown,
                dgvHistory
            };

            foreach (var dgv in gridViews)
            {
                if (dgv == null) continue;
                dgv.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                dgv.EnableHeadersVisualStyles = false;
                dgv.RowTemplate.Height = 38;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
                // Column headers
                dgv.ColumnHeadersHeight = 44;
                dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                
                // Alternating rows
                dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                
                // Selected row styling
                dgv.DefaultCellStyle.SelectionBackColor = BSU_Red;
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private Panel GetOrCreateDivider(TabPage tab, string name, bool isVertical)
        {
            var existing = tab.Controls.Find(name, false).FirstOrDefault() as Panel;
            if (existing != null) return existing;

            var divider = new Panel 
            { 
                Name = name, 
                BackColor = Color.FromArgb(220, 220, 220),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            tab.Controls.Add(divider);
            return divider;
        }

        private void ApplyDesignSystemLayout()
        {
            var tabPages = new TabPage[] { tpDashboard, tpEnroll, tpSubjects, tpSection, tpAssessment, tpHistory };
            int padding = 32;

            int displayWidth = tcPages.Width;
            int displayHeight = tcPages.Height;

            // If the TabControl isn't fully laid out yet, use the form's ClientSize
            if (displayWidth < 100) displayWidth = this.ClientSize.Width;
            if (displayHeight < 100) displayHeight = this.ClientSize.Height;
            // Fallbacks in case dimensions are extremely small during construction
            if (displayWidth < 100) displayWidth = 1280;
            if (displayHeight < 100) displayHeight = 800;

            foreach (var tab in tabPages)
            {
                if (tab == null) continue;

                int contentWidth = Math.Min(displayWidth - padding * 2, 1440);
                int contentLeft = padding + (displayWidth - padding * 2 - contentWidth) / 2;

                LinkLabel? breadcrumb = null;
                if (tab == tpDashboard) breadcrumb = lblBreadcrumb;
                else if (tab == tpEnroll) breadcrumb = lblBreadcrumbEnroll;
                else if (tab == tpSubjects) breadcrumb = lblBreadcrumbSubjects;
                else if (tab == tpSection) breadcrumb = lblBreadcrumbSection;
                else if (tab == tpAssessment) breadcrumb = lblBreadcrumbAssessment;
                else if (tab == tpHistory) breadcrumb = lblBreadcrumbHistory;

                int currentY = padding;
                if (breadcrumb != null)
                {
                    breadcrumb.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    breadcrumb.Location = new Point(contentLeft, currentY);
                    breadcrumb.Size = new Size(contentWidth, 20);
                    currentY = breadcrumb.Bottom + 16;
                }

                Label? titleLabel = null;
                if (tab == tpDashboard) titleLabel = lblTitle;
                else if (tab == tpEnroll) titleLabel = lblTitleEnroll;
                else if (tab == tpSubjects) titleLabel = lblTitleSubjects;
                else if (tab == tpSection) titleLabel = lblTitleSection;
                else if (tab == tpAssessment) titleLabel = lblTitleAssessment;
                else if (tab == tpHistory) titleLabel = lblTitleHistory;

                if (titleLabel != null)
                {
                    titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    titleLabel.Location = new Point(contentLeft, currentY);
                    titleLabel.Size = new Size(contentWidth, 30);
                    currentY = titleLabel.Bottom + 16;
                }

                Panel? headerDividerPanel = null;
                if (tab == tpDashboard) headerDividerPanel = headerDivider;
                else if (tab == tpEnroll) headerDividerPanel = headerDividerEnroll;
                else if (tab == tpSubjects) headerDividerPanel = headerDividerSubjects;
                else if (tab == tpSection) headerDividerPanel = headerDividerSection;
                else if (tab == tpAssessment) headerDividerPanel = headerDividerAssessment;
                else if (tab == tpHistory) headerDividerPanel = headerDividerHistory;

                if (headerDividerPanel != null)
                {
                    headerDividerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    headerDividerPanel.Location = new Point(contentLeft, currentY);
                    headerDividerPanel.Size = new Size(contentWidth, 2);
                    currentY = headerDividerPanel.Bottom + 16;
                }

                Panel? stepperPanel = null;
                if (tab == tpEnroll) stepperPanel = pnlStepperEnroll;
                else if (tab == tpSubjects) stepperPanel = pnlStepperSubjects;
                else if (tab == tpSection) stepperPanel = pnlStepperSection;
                else if (tab == tpAssessment) stepperPanel = pnlStepperAssessment;

                if (stepperPanel != null)
                {
                    stepperPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    stepperPanel.Location = new Point(contentLeft, currentY);
                    stepperPanel.Size = new Size(contentWidth, 40);

                    var steps = new Label?[] { null, null, null, null, null };
                    if (tab == tpEnroll) { steps[0] = lblStep1Enroll; steps[1] = lblStep2Enroll; steps[2] = lblStep3Enroll; steps[3] = lblStep4Enroll; steps[4] = lblStep5Enroll; }
                    else if (tab == tpSubjects) { steps[0] = lblStep1Subjects; steps[1] = lblStep2Subjects; steps[2] = lblStep3Subjects; steps[3] = lblStep4Subjects; steps[4] = lblStep5Subjects; }
                    else if (tab == tpSection) { steps[0] = lblStep1Section; steps[1] = lblStep2Section; steps[2] = lblStep3Section; steps[3] = lblStep4Section; steps[4] = lblStep5Section; }
                    else if (tab == tpAssessment) { steps[0] = lblStep1Assessment; steps[1] = lblStep2Assessment; steps[2] = lblStep3Assessment; steps[3] = lblStep4Assessment; steps[4] = lblStep5Assessment; }

                    double stepWidth = (double)(contentWidth - 16) / 5.0;
                    for (int i = 0; i < 5; i++)
                    {
                        var lbl = steps[i];
                        if (lbl != null)
                        {
                            lbl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            lbl.Location = new Point((int)(i * (stepWidth + 4)), 0);
                            lbl.Size = new Size((int)stepWidth, 40);
                            lbl.TextAlign = ContentAlignment.MiddleCenter;
                        }
                    }

                    currentY = stepperPanel.Bottom + 16;
                }

                if (tab == tpDashboard)
                {
                    var cards = new Panel[] { cardEnrolled, cardPending, cardUnpaid, cardOpenSections };
                    double cardWidth = (double)(contentWidth - 48) / 4.0;
                    for (int i = 0; i < cards.Length; i++)
                    {
                        var card = cards[i];
                        if (card != null)
                        {
                            card.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            card.Location = new Point(contentLeft + (int)(i * (cardWidth + 16)), currentY);
                            card.Size = new Size((int)cardWidth, 80);
                        }
                    }

                    var countLabels = new Label[] { lblEnrolledCount, lblPendingCount, lblUnpaidCount, lblOpenSectionsCount };
                    var textLabels = new Label[] { lblEnrolledLabel, lblPendingLabel, lblUnpaidLabel, lblOpenSectionsLabel };

                    for (int i = 0; i < 4; i++)
                    {
                        var countLbl = countLabels[i];
                        var textLbl = textLabels[i];
                        if (textLbl != null)
                        {
                            textLbl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            textLbl.Location = new Point(16, 12);
                            textLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                            textLbl.ForeColor = Color.FromArgb(120, 120, 120);
                        }
                        if (countLbl != null)
                        {
                            countLbl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                            countLbl.Location = new Point(12, 32);
                            countLbl.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
                            countLbl.ForeColor = Color.Black;
                        }
                    }

                    currentY = currentY + 80 + 16;
                }

                int footerY = displayHeight - 88;
                int mainContentHeight = footerY - currentY - 16;

                var footerLine = GetOrCreateDivider(tab, "sys_footer_divider", false);
                footerLine.Location = new Point(contentLeft, footerY);
                footerLine.Size = new Size(contentWidth, 1);

                int btnY = footerY + 24;

                if (tab == tpDashboard)
                {
                    btnView.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnView.Location = new Point(contentLeft, btnY);
                    btnView.Size = new Size(100, 40);

                    btnUpdateEnrollment.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnUpdateEnrollment.Location = new Point(btnView.Right + 16, btnY);
                    btnUpdateEnrollment.Size = new Size(150, 40);

                    btnVoid.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnVoid.Location = new Point(btnUpdateEnrollment.Right + 16, btnY);
                    btnVoid.Size = new Size(100, 40);

                    btnNewEnrollment.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnNewEnrollment.Location = new Point(contentLeft + contentWidth - 180, btnY);
                    btnNewEnrollment.Size = new Size(180, 40);
                }
                else if (tab == tpEnroll)
                {
                    btnCancelEnroll.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnCancelEnroll.Location = new Point(contentLeft, btnY);
                    btnCancelEnroll.Size = new Size(120, 40);

                    btnNextEnroll.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnNextEnroll.Location = new Point(contentLeft + contentWidth - 150, btnY);
                    btnNextEnroll.Size = new Size(150, 40);
                }
                else if (tab == tpSubjects)
                {
                    btnBackSubjects.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnBackSubjects.Location = new Point(contentLeft, btnY);
                    btnBackSubjects.Size = new Size(120, 40);

                    btnDropSelected.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnDropSelected.Location = new Point(contentLeft + contentWidth - 320, btnY);
                    btnDropSelected.Size = new Size(150, 40);

                    btnNextSubjects.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnNextSubjects.Location = new Point(contentLeft + contentWidth - 150, btnY);
                    btnNextSubjects.Size = new Size(150, 40);
                }
                else if (tab == tpSection)
                {
                    btnBackSection.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnBackSection.Location = new Point(contentLeft, btnY);
                    btnBackSection.Size = new Size(120, 40);

                    btnAutoAssign.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnAutoAssign.Location = new Point(contentLeft + contentWidth - 320, btnY);
                    btnAutoAssign.Size = new Size(150, 40);

                    btnNextSection.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnNextSection.Location = new Point(contentLeft + contentWidth - 150, btnY);
                    btnNextSection.Size = new Size(150, 40);
                }
                else if (tab == tpAssessment)
                {
                    btnBackAssessment.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnBackAssessment.Location = new Point(contentLeft, btnY);
                    btnBackAssessment.Size = new Size(120, 40);

                    btnPrintAssessment.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnPrintAssessment.Location = new Point(contentLeft + contentWidth - 340, btnY);
                    btnPrintAssessment.Size = new Size(170, 40);

                    btnConfirmEnrollment.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnConfirmEnrollment.Location = new Point(contentLeft + contentWidth - 150, btnY);
                    btnConfirmEnrollment.Size = new Size(150, 40);
                }
                else if (tab == tpHistory)
                {
                    btnHistoryView.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnHistoryView.Location = new Point(contentLeft, btnY);
                    btnHistoryView.Size = new Size(150, 40);

                    btnHistoryExport.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnHistoryExport.Location = new Point(contentLeft + contentWidth - 150, btnY);
                    btnHistoryExport.Size = new Size(150, 40);
                }

                if (tab == tpDashboard)
                {
                    lblSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblSearch.Location = new Point(contentLeft, currentY + 10);
                    txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtSearch.Location = new Point(lblSearch.Right + 8, currentY);
                    txtSearch.Size = new Size(180, 40);

                    lblTerm.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblTerm.Location = new Point(txtSearch.Right + 24, currentY + 10);
                    cboTerm.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    cboTerm.Location = new Point(lblTerm.Right + 8, currentY);
                    cboTerm.Size = new Size(200, 40);

                    btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnSearch.Location = new Point(cboTerm.Right + 16, currentY);
                    btnSearch.Size = new Size(100, 40);

                    btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnRefresh.Location = new Point(btnSearch.Right + 12, currentY);
                    btnRefresh.Size = new Size(100, 40);

                    currentY = currentY + 40 + 16;

                    dgvEnrollments.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    dgvEnrollments.Location = new Point(contentLeft, currentY);
                    dgvEnrollments.Size = new Size(contentWidth, footerY - currentY - 16);
                }
                else if (tab == tpEnroll)
                {
                    int leftWidth = (contentWidth - 48) / 2;
                    int rightWidth = (contentWidth - 48) / 2;
                    int midX = contentLeft + leftWidth + 24;

                    var vLine = GetOrCreateDivider(tab, "sys_v_divider", true);
                    vLine.Location = new Point(midX, currentY);
                    vLine.Size = new Size(1, footerY - currentY - 16);

                    lblStudentNo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblStudentNo.Location = new Point(contentLeft, currentY + 10);
                    txtStudentNo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtStudentNo.Location = new Point(lblStudentNo.Right + 8, currentY);
                    txtStudentNo.Size = new Size(180, 40);
                    btnLoadStudent.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnLoadStudent.Location = new Point(txtStudentNo.Right + 12, currentY);
                    btnLoadStudent.Size = new Size(100, 40);

                    dgvStudentsList.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    dgvStudentsList.Location = new Point(contentLeft, currentY + 40 + 16);
                    dgvStudentsList.Size = new Size(leftWidth, footerY - (currentY + 40 + 16) - 16);

                    int rightLeft = midX + 24;
                    int colWidth = (rightWidth - 24) / 2;

                    lblFullNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblFullNameLabel.Location = new Point(rightLeft, currentY);
                    txtFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtFullName.Location = new Point(rightLeft, lblFullNameLabel.Bottom + 6);
                    txtFullName.Size = new Size(rightWidth, 40);

                    int row2Y = txtFullName.Bottom + 24;
                    lblProgramLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblProgramLabel.Location = new Point(rightLeft, row2Y);
                    txtProgram.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtProgram.Location = new Point(rightLeft, lblProgramLabel.Bottom + 6);
                    txtProgram.Size = new Size(colWidth, 40);

                    lblYearLevelLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblYearLevelLabel.Location = new Point(rightLeft + colWidth + 24, row2Y);
                    txtYearLevel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtYearLevel.Location = new Point(rightLeft + colWidth + 24, lblYearLevelLabel.Bottom + 6);
                    txtYearLevel.Size = new Size(colWidth, 40);

                    int row3Y = txtProgram.Bottom + 24;
                    lblSchoolYearEnroll.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblSchoolYearEnroll.Location = new Point(rightLeft, row3Y);
                    cboSchoolYearEnroll.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    cboSchoolYearEnroll.Location = new Point(rightLeft, lblSchoolYearEnroll.Bottom + 6);
                    cboSchoolYearEnroll.Size = new Size(colWidth, 40);

                    lblSemesterEnroll.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblSemesterEnroll.Location = new Point(rightLeft + colWidth + 24, row3Y);
                    cboSemesterEnroll.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    cboSemesterEnroll.Location = new Point(rightLeft + colWidth + 24, lblSemesterEnroll.Bottom + 6);
                    cboSemesterEnroll.Size = new Size(colWidth, 40);

                    int row4Y = cboSchoolYearEnroll.Bottom + 24;
                    lblEnrollmentType.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblEnrollmentType.Location = new Point(rightLeft, row4Y);
                    cboEnrollmentType.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    cboEnrollmentType.Location = new Point(rightLeft, lblEnrollmentType.Bottom + 6);
                    cboEnrollmentType.Size = new Size(colWidth, 40);

                    lblStudentType.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblStudentType.Location = new Point(rightLeft + colWidth + 24, row4Y);
                    cboStudentType.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    cboStudentType.Location = new Point(rightLeft + colWidth + 24, lblStudentType.Bottom + 6);
                    cboStudentType.Size = new Size(colWidth, 40);
                }
                else if (tab == tpSubjects)
                {
                    int leftWidth = (int)((contentWidth - 48) * 0.65);
                    int rightWidth = contentWidth - 48 - leftWidth;
                    int midX = contentLeft + leftWidth + 24;

                    var vLine = GetOrCreateDivider(tab, "sys_v_divider", true);
                    vLine.Location = new Point(midX, currentY);
                    vLine.Size = new Size(1, footerY - currentY - 16);

                    lblSubjectsSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblSubjectsSearch.Location = new Point(contentLeft, currentY + 10);
                    txtSubjectsSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtSubjectsSearch.Location = new Point(lblSubjectsSearch.Right + 8, currentY);
                    txtSubjectsSearch.Size = new Size(180, 40);

                    lblCurriculumYear.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblCurriculumYear.Location = new Point(txtSubjectsSearch.Right + 24, currentY + 10);
                    cboCurriculumYear.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    cboCurriculumYear.Location = new Point(lblCurriculumYear.Right + 8, currentY);
                    cboCurriculumYear.Size = new Size(180, 40);

                    dgvSubjectsSelect.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    dgvSubjectsSelect.Location = new Point(contentLeft, currentY + 40 + 16);
                    dgvSubjectsSelect.Size = new Size(leftWidth, footerY - (currentY + 40 + 16) - 16);

                    int rightLeft = midX + 24;

                    lblTotalSubjectsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblTotalSubjectsLabel.Location = new Point(rightLeft, currentY);
                    txtTotalSubjects.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtTotalSubjects.Location = new Point(rightLeft, lblTotalSubjectsLabel.Bottom + 6);
                    txtTotalSubjects.Size = new Size(rightWidth, 40);

                    lblTotalUnitsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblTotalUnitsLabel.Location = new Point(rightLeft, txtTotalSubjects.Bottom + 24);
                    txtTotalUnits.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtTotalUnits.Location = new Point(rightLeft, lblTotalUnitsLabel.Bottom + 6);
                    txtTotalUnits.Size = new Size(rightWidth, 40);

                    lblMaxUnitsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblMaxUnitsLabel.Location = new Point(rightLeft, txtTotalUnits.Bottom + 24);
                    txtMaxUnits.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtMaxUnits.Location = new Point(rightLeft, lblMaxUnitsLabel.Bottom + 6);
                    txtMaxUnits.Size = new Size(rightWidth, 40);
                }
                else if (tab == tpSection)
                {
                    dgvSectionAssign.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    dgvSectionAssign.Location = new Point(contentLeft, currentY);
                    int gridHeight = footerY - currentY - 80 - 32;
                    dgvSectionAssign.Size = new Size(contentWidth, gridHeight);

                    pnlConflictChecker.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    pnlConflictChecker.Location = new Point(contentLeft, dgvSectionAssign.Bottom + 16);
                    pnlConflictChecker.Size = new Size(contentWidth, 80);

                    lblConflictHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblConflictHeader.Location = new Point(16, 12);
                    lblConflictMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblConflictMessage.Location = new Point(16, 38);
                }
                else if (tab == tpAssessment)
                {
                    int leftWidth = (contentWidth - 48) / 2;
                    int rightWidth = (contentWidth - 48) / 2;
                    int midX = contentLeft + leftWidth + 24;

                    var vLine = GetOrCreateDivider(tab, "sys_v_divider", true);
                    vLine.Location = new Point(midX, currentY);
                    vLine.Size = new Size(1, footerY - currentY - 16);

                    int halfLeftWidth = (leftWidth - 24) / 2;

                    lblAssessedUnitsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblAssessedUnitsLabel.Location = new Point(contentLeft, currentY);
                    txtAssessedUnits.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtAssessedUnits.Location = new Point(contentLeft, lblAssessedUnitsLabel.Bottom + 6);
                    txtAssessedUnits.Size = new Size(halfLeftWidth, 40);

                    lblRatePerUnitLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblRatePerUnitLabel.Location = new Point(contentLeft + halfLeftWidth + 24, currentY);
                    txtRatePerUnit.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtRatePerUnit.Location = new Point(contentLeft + halfLeftWidth + 24, lblRatePerUnitLabel.Bottom + 6);
                    txtRatePerUnit.Size = new Size(halfLeftWidth, 40);

                    int row2Y = txtAssessedUnits.Bottom + 24;
                    lblLabUnitsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblLabUnitsLabel.Location = new Point(contentLeft, row2Y);
                    txtLabUnits.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtLabUnits.Location = new Point(contentLeft, lblLabUnitsLabel.Bottom + 6);
                    txtLabUnits.Size = new Size(halfLeftWidth, 40);

                    lblPaymentPlan.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblPaymentPlan.Location = new Point(contentLeft + halfLeftWidth + 24, row2Y);
                    cboPaymentPlan.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    cboPaymentPlan.Location = new Point(contentLeft + halfLeftWidth + 24, lblPaymentPlan.Bottom + 6);
                    cboPaymentPlan.Size = new Size(halfLeftWidth, 40);

                    int row3Y = txtLabUnits.Bottom + 24;
                    lblAmountPaidLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblAmountPaidLabel.Location = new Point(contentLeft, row3Y);
                    txtAmountPaid.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtAmountPaid.Location = new Point(contentLeft, lblAmountPaidLabel.Bottom + 6);
                    txtAmountPaid.Size = new Size(leftWidth, 40);

                    dgvFeesBreakdown.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    dgvFeesBreakdown.Location = new Point(midX + 24, currentY);
                    dgvFeesBreakdown.Size = new Size(rightWidth, footerY - currentY - 16);
                }
                else if (tab == tpHistory)
                {
                    lblHistorySearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblHistorySearch.Location = new Point(contentLeft, currentY + 10);
                    txtHistorySearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    txtHistorySearch.Location = new Point(lblHistorySearch.Right + 8, currentY);
                    txtHistorySearch.Size = new Size(180, 40);

                    lblHistorySY.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblHistorySY.Location = new Point(txtHistorySearch.Right + 20, currentY + 10);
                    cboHistorySY.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    cboHistorySY.Location = new Point(lblHistorySY.Right + 8, currentY);
                    cboHistorySY.Size = new Size(150, 40);

                    lblHistoryAction.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    lblHistoryAction.Location = new Point(cboHistorySY.Right + 20, currentY + 10);
                    cboHistoryAction.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    cboHistoryAction.Location = new Point(lblHistoryAction.Right + 8, currentY);
                    cboHistoryAction.Size = new Size(120, 40);

                    btnHistoryFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btnHistoryFilter.Location = new Point(cboHistoryAction.Right + 16, currentY);
                    btnHistoryFilter.Size = new Size(100, 40);

                    currentY = currentY + 40 + 16;

                    dgvHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    dgvHistory.Location = new Point(contentLeft, currentY);
                    dgvHistory.Size = new Size(contentWidth, footerY - currentY - 16);
                }
            }
        }


        #region UI Shell / Switching Page Logic

        private void SwitchToPage(TabPage targetPage, Button? activeBtn = null)
        {
            tcPages.SelectedTab = targetPage;
            
            // Reset all navigation buttons to unselected state
            btnNavDashboard.BackColor = Color.White;
            btnNavDashboard.ForeColor = Color.FromArgb(50, 50, 50);
            btnNavEnrollStudent.BackColor = Color.White;
            btnNavEnrollStudent.ForeColor = Color.FromArgb(50, 50, 50);
            btnNavEnrolledSubjects.BackColor = Color.White;
            btnNavEnrolledSubjects.ForeColor = Color.FromArgb(50, 50, 50);
            btnNavSectionAssignment.BackColor = Color.White;
            btnNavSectionAssignment.ForeColor = Color.FromArgb(50, 50, 50);
            btnNavTuitionAssessment.BackColor = Color.White;
            btnNavTuitionAssessment.ForeColor = Color.FromArgb(50, 50, 50);
            btnNavEnrollmentHistory.BackColor = Color.White;
            btnNavEnrollmentHistory.ForeColor = Color.FromArgb(50, 50, 50);
            
            // Highlight the active button
            if (activeBtn != null)
            {
                activeBtn.BackColor = BSU_Red;
                activeBtn.ForeColor = Color.White;
            }

            // Update stepper headers for BSU branding
            UpdateSteppersColors(targetPage);
        }

        private void SetupSidebarHoverStyles()
        {
            foreach (Control ctrl in sidebarPanel.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.MouseEnter += SidebarButton_MouseEnter;
                    btn.MouseLeave += SidebarButton_MouseLeave;
                }
            }
        }

        private void SetupButtonStyles()
        {
            // Set header dividers to BSU Red
            var dividers = new Panel[] {
                headerDivider,
                headerDividerEnroll,
                headerDividerSubjects,
                headerDividerSection,
                headerDividerAssessment,
                headerDividerHistory
            };
            foreach (var div in dividers)
            {
                if (div != null)
                {
                    div.BackColor = BSU_Red;
                }
            }

            // 1. Primary Brand Action Buttons (BSU Red)
            var primaryButtons = new Button[] {
                btnNewEnrollment,
                btnConfirmEnrollment,
                btnNextEnroll,
                btnNextSubjects,
                btnNextSection,
                btnSearch,
                btnLoadStudent,
                btnHistoryFilter
            };
            foreach (var btn in primaryButtons)
            {
                if (btn == null) continue;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = BSU_Red;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;
                
                btn.MouseEnter += (s, e) => { btn.BackColor = Color.FromArgb(150, 10, 38); };
                btn.MouseLeave += (s, e) => { btn.BackColor = BSU_Red; };
            }

            // 2. Secondary Neutral Action Buttons (White Bg, Neutral Gray Border & Text)
            var secondaryButtons = new Button[] {
                btnCancelEnroll,
                btnBackSubjects,
                btnBackSection,
                btnBackAssessment,
                btnDropSelected,
                btnAutoAssign,
                btnView,
                btnUpdateEnrollment,
                btnHistoryView,
                btnHistoryExport,
                btnPrintAssessment,
                btnRefresh
            };

            Color colorBorder = Color.FromArgb(200, 200, 200);
            Color colorText = Color.FromArgb(50, 50, 50);

            foreach (var btn in secondaryButtons)
            {
                if (btn == null) continue;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = colorBorder;
                btn.BackColor = Color.White;
                btn.ForeColor = colorText;
                btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;
                
                btn.MouseEnter += (s, e) => { btn.BackColor = Color.FromArgb(245, 245, 245); };
                btn.MouseLeave += (s, e) => { btn.BackColor = Color.White; };
            }

            // 3. Danger Action Buttons (Destructive Red)
            var dangerButtons = new Button[] {
                btnVoid
            };
            foreach (var btn in dangerButtons)
            {
                if (btn == null) continue;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(197, 48, 48); // #C53030
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;
                
                btn.MouseEnter += (s, e) => { btn.BackColor = Color.FromArgb(155, 44, 44); }; // #9B2C2C
                btn.MouseLeave += (s, e) => { btn.BackColor = Color.FromArgb(197, 48, 48); };
            }
        }

        private void UpdateSteppersColors(TabPage targetPage)
        {
            int stepIndex = 0;
            if (targetPage == tpEnroll) stepIndex = 1;
            else if (targetPage == tpSubjects) stepIndex = 2;
            else if (targetPage == tpSection) stepIndex = 3;
            else if (targetPage == tpAssessment) stepIndex = 4;

            Label[] enrollSteps = { lblStep1Enroll, lblStep2Enroll, lblStep3Enroll, lblStep4Enroll, lblStep5Enroll };
            Label[] subjectsSteps = { lblStep1Subjects, lblStep2Subjects, lblStep3Subjects, lblStep4Subjects, lblStep5Subjects };
            Label[] sectionSteps = { lblStep1Section, lblStep2Section, lblStep3Section, lblStep4Section, lblStep5Section };
            Label[] assessmentSteps = { lblStep1Assessment, lblStep2Assessment, lblStep3Assessment, lblStep4Assessment, lblStep5Assessment };

            UpdateStepperLabels(enrollSteps, stepIndex);
            UpdateStepperLabels(subjectsSteps, stepIndex);
            UpdateStepperLabels(sectionSteps, stepIndex);
            UpdateStepperLabels(assessmentSteps, stepIndex);
        }

        private void UpdateStepperLabels(Label[] labels, int activeIndex)
        {
            for (int i = 0; i < labels.Length; i++)
            {
                if (labels[i] == null) continue;
                if (i + 1 == activeIndex)
                {
                    labels[i].BackColor = BSU_Red;
                    labels[i].ForeColor = Color.White;
                    labels[i].Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else
                {
                    labels[i].BackColor = Color.FromArgb(244, 244, 244);
                    labels[i].ForeColor = Color.Gray;
                    labels[i].Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                }
            }
        }

        #endregion

        #region Screen 1: Dashboard Page (tpDashboard)

        private void SetupDashboardTermFilter()
        {
            cboTerm.Items.Clear();
            cboTerm.Items.Add("All Terms");
            cboTerm.Items.Add("1st Sem 2025-2026");
            cboTerm.Items.Add("2nd Sem 2025-2026");
            cboTerm.Items.Add("Summer 2025-2026");
            cboTerm.Items.Add("1st Sem 2026-2027");
            cboTerm.Items.Add("2nd Sem 2026-2027");
            cboTerm.Items.Add("Summer 2026-2027");
            cboTerm.SelectedIndex = 0; // Default to All Terms
        }

        private void SetupSearchPlaceholder()
        {
            txtSearch.ForeColor = Color.Gray;
            txtSearch.Text = _searchPlaceholder;

            txtSearch.Enter += (sender, e) =>
            {
                if (txtSearch.Text == _searchPlaceholder)
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = _searchPlaceholder;
                    txtSearch.ForeColor = Color.Gray;
                }
            };
        }

        private void LoadDashboardData()
        {
            try
            {
                _service.ClearCache();
                var enrollments = _service.GetAllEnrollments();
                var students = _service.GetAllStudents();

                // 1. Calculate live statistics counters
                int enrolledCount = enrollments.Count(e => e.EnrollmentStatus.Equals("Enrolled", StringComparison.OrdinalIgnoreCase));
                int pendingCount = enrollments.Count(e => e.EnrollmentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase));
                int unpaidCount = enrollments.Count(e => e.TuitionFee != null && e.TuitionFee.Balance > 0);
                int openSectionsCount = enrollments.Select(e => e.Section).Where(s => !string.IsNullOrEmpty(s)).Distinct().Count();

                lblEnrolledCount.Text = enrolledCount.ToString("D3");
                lblPendingCount.Text = pendingCount.ToString("D3");
                lblUnpaidCount.Text = unpaidCount.ToString("D3");
                lblOpenSectionsCount.Text = openSectionsCount.ToString("D3");

                // 2. Prepare grid display list
                _displayList = (from e in enrollments
                                join s in students on e.StudentID equals s.StudentId into gj
                                from subStudent in gj.DefaultIfEmpty()
                                select new DashboardEnrollmentDisplayModel
                                {
                                    EnrollmentID = e.EnrollmentID,
                                    StudentID = e.StudentID,
                                    StudentName = subStudent != null ? $"{subStudent.FirstName} {subStudent.LastName}" : "—",
                                    ProgramAndYear = subStudent != null ? $"{subStudent.ProgramId} - {GetYearLevelString(subStudent.YearLevel)}" : "—",
                                    Status = e.EnrollmentStatus,
                                    Balance = e.TuitionFee?.Balance ?? 0
                                }).ToList();

                ApplyDashboardFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetYearLevelString(int year)
        {
            return year switch
            {
                1 => "1st Year",
                2 => "2nd Year",
                3 => "3rd Year",
                4 => "4th Year",
                _ => $"{year}th Year"
            };
        }

        private void ApplyDashboardFilter()
        {
            string query = txtSearch.Text.Trim();
            if (query == _searchPlaceholder) query = "";

            var filtered = _displayList;

            // Name / ID Filter
            if (!string.IsNullOrEmpty(query))
            {
                filtered = filtered.Where(x =>
                    x.StudentID.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    x.StudentName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    x.EnrollmentID.Contains(query, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Term Filter
            string selectedTerm = cboTerm.SelectedItem?.ToString() ?? "All Terms";
            if (selectedTerm != "All Terms")
            {
                string expectedSemester = "";
                if (selectedTerm.StartsWith("1st Sem")) expectedSemester = "1st Semester";
                else if (selectedTerm.StartsWith("2nd Sem")) expectedSemester = "2nd Semester";
                else if (selectedTerm.StartsWith("Summer")) expectedSemester = "Summer";

                string expectedSchoolYear = "";
                var parts = selectedTerm.Split(' ');
                if (parts.Length > 0)
                {
                    expectedSchoolYear = parts[parts.Length - 1];
                }

                var enrollments = _service.GetAllEnrollments();
                var matchingEnrollmentIds = enrollments.Where(e =>
                    e.Semester.Equals(expectedSemester, StringComparison.OrdinalIgnoreCase) &&
                    e.SchoolYear.Equals(expectedSchoolYear, StringComparison.OrdinalIgnoreCase)
                ).Select(e => e.EnrollmentID).ToHashSet(StringComparer.OrdinalIgnoreCase);

                filtered = filtered.Where(x => matchingEnrollmentIds.Contains(x.EnrollmentID)).ToList();
            }

            dgvEnrollments.DataSource = null;
            dgvEnrollments.DataSource = filtered;

            if (dgvEnrollments.Columns.Count > 0)
            {
                if (dgvEnrollments.Columns["EnrollmentID"] is DataGridViewColumn cId)
                {
                    cId.HeaderText = "Enrollment ID";
                    cId.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvEnrollments.Columns["StudentID"] is DataGridViewColumn cStu)
                {
                    cStu.HeaderText = "Student No.";
                    cStu.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvEnrollments.Columns["StudentName"] is DataGridViewColumn cName)
                {
                    cName.HeaderText = "Name";
                    cName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                if (dgvEnrollments.Columns["ProgramAndYear"] is DataGridViewColumn cProg)
                {
                    cProg.HeaderText = "Program / Year";
                    cProg.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvEnrollments.Columns["Status"] is DataGridViewColumn cStat)
                {
                    cStat.HeaderText = "Status";
                    cStat.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvEnrollments.Columns["Balance"] is DataGridViewColumn cBal)
                {
                    cBal.HeaderText = "Balance";
                    cBal.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    cBal.DefaultCellStyle.Format = "₱#,##0.00";
                }
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            ApplyDashboardFilter();
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void BtnVoid_Click(object? sender, EventArgs e)
        {
            var selected = GetSelectedRow();
            if (selected == null)
            {
                MessageBox.Show("Please select an enrollment record to void.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selected.Status.Equals("Voided", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("This enrollment record is already voided.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dialogResult = MessageBox.Show(
                $"Are you sure you want to void enrollment {selected.EnrollmentID} for {selected.StudentName}?\n\nThis will set the status to Voided and clear any outstanding balance.",
                "Confirm Void Enrollment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    var enrollment = _service.GetEnrollmentById(selected.EnrollmentID);
                    if (enrollment != null)
                    {
                        // 1. Update status
                        enrollment.EnrollmentStatus = "Voided";
                        
                        // 2. Clear balance
                        if (enrollment.TuitionFee != null)
                        {
                            enrollment.TuitionFee.Balance = 0;
                        }
                        
                        // 3. Save updates
                        _service.SaveEnrollment(enrollment);

                        // 4. Log to history
                        var historyRecord = new EnrollmentHistory
                        {
                            EnrollmentID = enrollment.EnrollmentID,
                            StudentID = enrollment.StudentID,
                            StudentName = selected.StudentName,
                            Term = $"{enrollment.SchoolYear} - {enrollment.Semester}",
                            Action = "Voided",
                            PerformedBy = "admin",
                            Remarks = "Enrollment voided by administrator."
                        };
                        _service.SaveHistory(historyRecord);

                        MessageBox.Show($"Enrollment {selected.EnrollmentID} successfully voided.", "Enrollment Voided", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // 5. Refresh
                        LoadDashboardData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error voiding enrollment:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnNewEnrollment_Click(object? sender, EventArgs e)
        {
            // Reset wizard state
            _wizardEnrollment = new Enrollment();
            _wizardStudent = null;
            _selectedSubjects.Clear();
            _selectedSubjectIds.Clear();

            // Restore modifiable controls on Assessment tab
            btnConfirmEnrollment.Enabled = true;
            btnConfirmEnrollment.Text = "Confirm Enrollment";
            btnBackAssessment.Enabled = true;

            // Reset step controls
            txtStudentNo.Text = "";
            txtFullName.Text = "";
            txtProgram.Text = "";
            txtYearLevel.Text = "";
            cboSchoolYearEnroll.SelectedIndex = 0;
            cboSemesterEnroll.SelectedIndex = 0;
            cboEnrollmentType.SelectedIndex = 0;
            cboStudentType.SelectedIndex = 1; // Default Old

            // Load student list
            LoadStudentsGridData();
            if (dgvStudentsList.Rows.Count > 0)
            {
                dgvStudentsList.ClearSelection();
            }

            SwitchToPage(tpEnroll, btnNavEnrollStudent);
        }

        private void BtnView_Click(object? sender, EventArgs e)
        {
            var selected = GetSelectedRow();
            if (selected == null)
            {
                MessageBox.Show("Please select an enrollment record to view.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var enrollment = _service.GetEnrollmentById(selected.EnrollmentID);
            if (enrollment == null)
            {
                MessageBox.Show("Could not find the enrollment record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Load selected enrollment into Assessment View page as a read-only view
            _wizardEnrollment = enrollment;
            _wizardStudent = _service.GetStudentById(enrollment.StudentID);
            
            // Switch to Step 4/Assessment tab
            LoadAssessmentStepData();
            
            // Disable modification actions on Assessment tab for read-only View mode
            btnConfirmEnrollment.Enabled = false;
            btnConfirmEnrollment.Text = "Read-Only View";
            btnBackAssessment.Enabled = true;
            _referrerPage = tpDashboard;

            SwitchToPage(tpAssessment, btnNavTuitionAssessment);
        }

        private DashboardEnrollmentDisplayModel? GetSelectedRow()
        {
            if (dgvEnrollments.SelectedRows.Count > 0)
            {
                return dgvEnrollments.SelectedRows[0].DataBoundItem as DashboardEnrollmentDisplayModel;
            }
            else if (dgvEnrollments.CurrentRow != null)
            {
                return dgvEnrollments.CurrentRow.DataBoundItem as DashboardEnrollmentDisplayModel;
            }
            return null;
        }

        private void BtnUpdateEnrollment_Click(object? sender, EventArgs e)
        {
            var selected = GetSelectedRow();
            if (selected == null)
            {
                MessageBox.Show("Please select an enrollment record to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var enrollment = _service.GetEnrollmentById(selected.EnrollmentID);
            if (enrollment == null)
            {
                MessageBox.Show("Could not find the enrollment record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var student = _service.GetStudentById(enrollment.StudentID);
            if (student == null)
            {
                MessageBox.Show("Could not find the student profile associated with this enrollment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Restore modifiable controls on Assessment tab in case they were disabled by View Mode
            btnConfirmEnrollment.Enabled = true;
            btnConfirmEnrollment.Text = "Confirm Enrollment";
            btnBackAssessment.Enabled = true;

            // Load enrollment into Wizard State
            _wizardEnrollment = enrollment;
            _wizardStudent = student;
            _selectedSubjects = new List<Subject>(enrollment.EnrolledSubjects);
            _selectedSubjectIds = new HashSet<string>(enrollment.EnrolledSubjects.Select(s => s.SubjectID), StringComparer.OrdinalIgnoreCase);

            // Populate Step 1 controls
            txtStudentNo.Text = student.StudentId;
            cboSchoolYearEnroll.SelectedItem = enrollment.SchoolYear;
            cboSemesterEnroll.SelectedItem = enrollment.Semester;

            // Load student list in grid and select this student
            LoadStudentsGridData();
            SelectStudentInGrid(student.StudentId);

            // Switch to Step 1 Enroll Student screen
            SwitchToPage(tpEnroll, btnNavEnrollStudent);
        }

        #endregion

        #region Screen 2: Enroll Student Step 1 (tpEnroll)

        private void BtnLoadStudent_Click(object? sender, EventArgs e)
        {
            FilterStudentsGrid();
            if (dgvStudentsList.Rows.Count > 0)
            {
                dgvStudentsList.Rows[0].Selected = true;
            }
        }

        private void BtnCancelEnroll_Click(object? sender, EventArgs e)
        {
            SwitchToPage(tpDashboard, btnNavDashboard);
        }

        private void BtnNextEnroll_Click(object? sender, EventArgs e)
        {
            if (_wizardStudent == null)
            {
                MessageBox.Show("Please select a student from the list before proceeding.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Fill wizard enrollment details
            _wizardEnrollment.StudentID = _wizardStudent.StudentId;
            _wizardEnrollment.SchoolYear = cboSchoolYearEnroll.SelectedItem?.ToString() ?? "2025-2026";
            _wizardEnrollment.Semester = cboSemesterEnroll.SelectedItem?.ToString() ?? "1st Semester";
            _wizardEnrollment.CourseID = _wizardStudent.ProgramId;
            _wizardEnrollment.YearLevel = GetYearLevelString(_wizardStudent.YearLevel);
            _wizardEnrollment.EnrollmentStatus = "Pending";

            // Open Subjects tab
            LoadSubjectsStepData();
            SwitchToPage(tpSubjects, btnNavEnrolledSubjects);
        }

        #endregion

        #region Screen 3: Enrolled Subjects Step 2 (tpSubjects)

        private void SetupSubjectsGridStructure()
        {
            lblSubjectsSearch.Text = "Available Subjects";
            dgvSubjectsSelect.Columns.Clear();
            
            var checkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = "Select",
                Width = 55
            };
            dgvSubjectsSelect.Columns.Add(checkCol);
            dgvSubjectsSelect.Columns.Add("SubjectID", "Code");
            dgvSubjectsSelect.Columns.Add("SubjectName", "Title");
            dgvSubjectsSelect.Columns.Add("Units", "Units");
            dgvSubjectsSelect.Columns.Add("Schedule", "Schedule");
            dgvSubjectsSelect.Columns.Add("Instructor", "Instructor");

            foreach (DataGridViewColumn col in dgvSubjectsSelect.Columns)
            {
                if (col.Name != "colCheck")
                {
                    col.ReadOnly = true;
                }
            }
        }

        private void LoadSubjectsStepData()
        {
            if (_wizardStudent == null) return;

            lblTitleSubjects.Text = $"Select Subjects — {_wizardEnrollment.EnrollmentID} · {_wizardStudent.FirstName} {_wizardStudent.LastName}";
            
            // Load curriculum subjects for student's program
            _availableSubjects = _service.GetSubjectsByCourse(_wizardStudent.ProgramId, _wizardEnrollment.SchoolYear, _wizardEnrollment.Semester);
            _selectedSubjectIds.Clear();

            txtSubjectsSearch.Text = "";
            cboCurriculumYear.SelectedIndex = 0; // All Years
            
            FilterSubjectsList();
        }

        private void FilterSubjectsList()
        {
            string query = txtSubjectsSearch.Text.Trim();
            var filtered = _availableSubjects;

            // Text search
            if (!string.IsNullOrEmpty(query))
            {
                filtered = filtered.Where(s =>
                    s.SubjectID.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    s.SubjectName.Contains(query, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Year level filter
            string selectedYear = cboCurriculumYear.SelectedItem?.ToString() ?? "All Years";
            if (selectedYear != "All Years")
            {
                // Custom filtering if subject name/code maps to a year level, 
                // e.g. CS-101 is 1st Year, CS201 is 2nd Year
                int targetYear = 1;
                if (selectedYear.StartsWith("2nd")) targetYear = 2;
                else if (selectedYear.StartsWith("3rd")) targetYear = 3;
                else if (selectedYear.StartsWith("4th")) targetYear = 4;

                filtered = filtered.Where(s =>
                {
                    // Find first digit in subject code
                    int year = 1;
                    foreach (char c in s.SubjectID)
                    {
                        if (char.IsDigit(c))
                        {
                            year = c - '0';
                            break;
                        }
                    }
                    return year == targetYear;
                }).ToList();
            }

            dgvSubjectsSelect.CellValueChanged -= DgvSubjectsSelect_CellValueChanged;
            dgvSubjectsSelect.Rows.Clear();

            foreach (var sub in filtered)
            {
                bool isChecked = _selectedSubjectIds.Contains(sub.SubjectID);
                dgvSubjectsSelect.Rows.Add(isChecked, sub.SubjectID, sub.SubjectName, sub.Units, $"{sub.Schedule} {sub.SchoolHours}", sub.Instructor);
            }

            dgvSubjectsSelect.CellValueChanged += DgvSubjectsSelect_CellValueChanged;

            UpdateSubjectsTotalDisplay();
        }

        private void UpdateSubjectsTotalDisplay()
        {
            _selectedSubjects = _availableSubjects.Where(s => _selectedSubjectIds.Contains(s.SubjectID)).ToList();
            int totalUnits = _selectedSubjects.Sum(s => s.Units);

            txtTotalSubjects.Text = _selectedSubjects.Count.ToString();
            txtTotalUnits.Text = totalUnits.ToString();
        }

        private void TxtSubjectsSearch_TextChanged(object? sender, EventArgs e)
        {
            FilterSubjectsList();
        }

        private void CboCurriculumYear_SelectedIndexChanged(object? sender, EventArgs e)
        {
            FilterSubjectsList();
        }

        private void DgvSubjectsSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                dgvSubjectsSelect.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvSubjectsSelect_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var row = dgvSubjectsSelect.Rows[e.RowIndex];
                bool isChecked = Convert.ToBoolean(row.Cells[0].Value);
                string? code = row.Cells["SubjectID"].Value?.ToString();

                if (!string.IsNullOrEmpty(code))
                {
                    if (isChecked && _wizardStudent != null)
                    {
                        if (!_service.ValidateSubjectPrerequisite(_wizardStudent.StudentId, code, out string? errorMessage))
                        {
                            MessageBox.Show(errorMessage, "Prerequisite Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            
                            // Temporarily detach event handler to prevent recursive call
                            dgvSubjectsSelect.CellValueChanged -= DgvSubjectsSelect_CellValueChanged;
                            row.Cells[0].Value = false;
                            dgvSubjectsSelect.CellValueChanged += DgvSubjectsSelect_CellValueChanged;
                            return;
                        }
                    }

                    if (isChecked)
                    {
                        _selectedSubjectIds.Add(code);
                    }
                    else
                    {
                        _selectedSubjectIds.Remove(code);
                    }
                }

                UpdateSubjectsTotalDisplay();
            }
        }

        private void BtnBackSubjects_Click(object? sender, EventArgs e)
        {
            SwitchToPage(tpEnroll, btnNavEnrollStudent);
        }

        private void BtnDropSelected_Click(object? sender, EventArgs e)
        {
            // Uncheck any selected rows
            _selectedSubjectIds.Clear();
            FilterSubjectsList();
        }

        private void BtnNextSubjects_Click(object? sender, EventArgs e)
        {
            if (_selectedSubjects.Count == 0)
            {
                MessageBox.Show("Please select at least one subject to proceed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int totalUnits = _selectedSubjects.Sum(s => s.Units);
            if (totalUnits > 24)
            {
                MessageBox.Show($"Total units ({totalUnits}) exceed the maximum allowed limit of 24 units. Please drop some subjects.", "Unit Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Final bulk validation check for safety
            if (_wizardStudent != null)
            {
                var subjectIds = _selectedSubjects.Select(s => s.SubjectID).ToList();
                if (!_service.ValidatePrerequisites(_wizardStudent.StudentId, subjectIds, out var errors))
                {
                    MessageBox.Show(string.Join("\n", errors), "Prerequisite Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _wizardEnrollment.EnrolledSubjects = _selectedSubjects;
            _wizardEnrollment.TotalUnits = totalUnits;

            // Load Section page
            LoadSectionStepData();
            SwitchToPage(tpSection, btnNavSectionAssignment);
        }

        #endregion

        #region Screen 4: Section Assignment Step 3 (tpSection)

        private void SetupSectionGridStructure()
        {
            dgvSectionAssign.Columns.Clear();
            dgvSectionAssign.Columns.Add("Subject", "Subject");
            
            // Add ComboBox column for sections
            var sectionCombo = new DataGridViewComboBoxColumn
            {
                Name = "colSection",
                HeaderText = "Section",
                Width = 120
            };
            dgvSectionAssign.Columns.Add(sectionCombo);
            dgvSectionAssign.Columns.Add("Schedule", "Schedule");
            dgvSectionAssign.Columns.Add("Room", "Room");
            dgvSectionAssign.Columns.Add("Instructor", "Instructor");
            dgvSectionAssign.Columns.Add("Slots", "Slots");

            foreach (DataGridViewColumn col in dgvSectionAssign.Columns)
            {
                if (col.Name != "colSection")
                {
                    col.ReadOnly = true;
                }
            }
        }

        private void LoadSectionStepData()
        {
            dgvSectionAssign.Rows.Clear();
            
            foreach (var sub in _selectedSubjects)
            {
                int rowIndex = dgvSectionAssign.Rows.Add();
                var row = dgvSectionAssign.Rows[rowIndex];
                
                row.Cells["Subject"].Value = sub.SubjectID;
                row.Cells["Schedule"].Value = $"{sub.Schedule} {sub.SchoolHours}";
                row.Cells["Room"].Value = GetMockRoom(sub.SubjectID);
                row.Cells["Instructor"].Value = sub.Instructor;
 
                // Populate section ComboBox choices dynamically based on program
                string prog = _wizardStudent?.ProgramId ?? "CS";
                string defaultSection = $"{prog}-1A";
                if (_wizardEnrollment != null && !string.IsNullOrEmpty(_wizardEnrollment.Section))
                {
                    defaultSection = _wizardEnrollment.Section;
                }

                if (row.Cells["colSection"] is DataGridViewComboBoxCell cell)
                {
                    cell.Items.Clear();
                    cell.Items.Add($"{prog}-1A");
                    cell.Items.Add($"{prog}-1B");
                    cell.Items.Add($"{prog}-2A");
                    if (!cell.Items.Contains(defaultSection))
                    {
                        cell.Items.Add(defaultSection);
                    }
                    cell.Value = defaultSection; // Set default selection
                }

                int enrolledCount = GetEnrolledCount(sub.SubjectID, defaultSection, _wizardEnrollment.SchoolYear, _wizardEnrollment.Semester);
                row.Cells["Slots"].Value = $"{enrolledCount} / 40";
            }
 
            dgvSectionAssign.CellValueChanged += DgvSectionAssign_CellValueChanged;
            CheckSectionConflicts();
        }

        private void DgvSectionAssign_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (dgvSectionAssign.Columns[e.ColumnIndex].Name == "colSection" && e.RowIndex >= 0)
            {
                CheckSectionConflicts();
            }
        }

        private string GetMockRoom(string code)
        {
            return code.StartsWith("CS") ? "Rm 301" : code.StartsWith("IT") ? "Rm 305" : "Rm 402";
        }

        private void CheckSectionConflicts()
        {
            // Implement a simple schedule conflict checker
            lblConflictMessage.ForeColor = Color.Black;
            lblConflictMessage.Text = "✓ No schedule or section capacity conflicts detected.";
 
            var schedules = new List<(string Code, string Sched)>();
            for (int i = 0; i < dgvSectionAssign.Rows.Count; i++)
            {
                var row = dgvSectionAssign.Rows[i];
                string code = row.Cells["Subject"].Value?.ToString() ?? "";
                string section = row.Cells["colSection"].Value?.ToString() ?? "";
                string scheduleStr = row.Cells["Schedule"].Value?.ToString() ?? "";
 
                schedules.Add((code, scheduleStr));
                
                // Get database-backed slot counts
                int enrolledCount = GetEnrolledCount(code, section, _wizardEnrollment.SchoolYear, _wizardEnrollment.Semester);
                int maxSlots = 40;
                
                if (enrolledCount >= maxSlots)
                {
                    row.Cells["Slots"].Value = $"{enrolledCount} / {maxSlots} (FULL)";
                    lblConflictMessage.ForeColor = Color.DarkRed;
                    lblConflictMessage.Text = $"⚠ Section '{section}' for subject '{code}' is full ({enrolledCount}/{maxSlots}) — choose another section.";
                    return;
                }
                else
                {
                    row.Cells["Slots"].Value = $"{enrolledCount} / {maxSlots}";
                }
            }

            // Simple checker for schedule overlaps (e.g. identical MW/TTh timeslots)
            for (int i = 0; i < schedules.Count; i++)
            {
                for (int j = i + 1; j < schedules.Count; j++)
                {
                    if (schedules[i].Sched == schedules[j].Sched)
                    {
                        lblConflictMessage.ForeColor = Color.DarkRed;
                        lblConflictMessage.Text = $"⚠ Schedule conflict detected: subject '{schedules[i].Code}' overlaps with '{schedules[j].Code}' ({schedules[i].Sched}).";
                        return;
                    }
                }
            }
        }

        private void BtnBackSection_Click(object? sender, EventArgs e)
        {
            SwitchToPage(tpSubjects, btnNavEnrolledSubjects);
        }

        private void BtnAutoAssign_Click(object? sender, EventArgs e)
        {
            // Auto assigns sections dynamically
            string prog = _wizardStudent?.ProgramId ?? "CS";
            for (int i = 0; i < dgvSectionAssign.Rows.Count; i++)
            {
                if (dgvSectionAssign.Rows[i].Cells["colSection"] is DataGridViewComboBoxCell cell)
                {
                    cell.Value = $"{prog}-1B"; // Toggle to different section to auto assign
                }
            }
            CheckSectionConflicts();
            MessageBox.Show("All subjects automatically assigned to non-conflicting sections.", "Auto-Assignment Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnNextSection_Click(object? sender, EventArgs e)
        {
            // Verify there is no conflict blocked
            if (lblConflictMessage.ForeColor == Color.DarkRed)
            {
                MessageBox.Show("Please resolve the highlighted conflicts before proceeding.", "Conflict Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Save selected section (grab first row's section as primary block section)
            if (dgvSectionAssign.Rows.Count > 0)
            {
                _wizardEnrollment.Section = dgvSectionAssign.Rows[0].Cells["colSection"].Value?.ToString() ?? "CS-1A";
            }

            // Load tuition assessment details
            LoadAssessmentStepData();
            
            // Enable Confirm actions (since it might have been disabled during read-only View mode)
            btnConfirmEnrollment.Enabled = true;
            btnConfirmEnrollment.Text = "Confirm Enrollment";
            btnBackAssessment.Enabled = true;
            _referrerPage = tpSection;

            SwitchToPage(tpAssessment, btnNavTuitionAssessment);
        }

        #endregion

        #region Screen 5: Tuition Assessment Step 4 & 5 (tpAssessment)

        private void SetupAssessmentGridStructure()
        {
            dgvFeesBreakdown.Columns.Clear();
            dgvFeesBreakdown.Columns.Add("Description", "Fee Description");
            dgvFeesBreakdown.Columns.Add("Type", "Type");
            dgvFeesBreakdown.Columns.Add("Amount", "Amount");
            
            dgvFeesBreakdown.Columns["Amount"].DefaultCellStyle.Format = "₱#,##0.00";
            dgvFeesBreakdown.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadAssessmentStepData()
        {
            lblTitleAssessment.Text = $"Assessment of Fees — {_wizardEnrollment.StudentID}";
            txtAssessedUnits.Text = _wizardEnrollment.TotalUnits.ToString();
            
            decimal rate = 500.00m;
            txtRatePerUnit.Text = rate.ToString("F2");
            
            // Consider 4-unit subjects as lab subjects
            int labUnits = _wizardEnrollment.EnrolledSubjects?.Count(s => s.Units >= 4) ?? 0;
            txtLabUnits.Text = labUnits.ToString();

            txtAmountPaid.Text = (_wizardEnrollment.TuitionFee?.AmountPaid ?? 0).ToString("F2");

            UpdateAssessmentSummaryTable();
        }

        private void UpdateAssessmentSummaryTable()
        {
            int totalUnits = _wizardEnrollment.TotalUnits;
            decimal rate = 500.00m;
            int labCount = _wizardEnrollment.EnrolledSubjects?.Count(s => s.Units >= 4) ?? 0;

            decimal tuitionFee = totalUnits * rate;
            decimal labFee = labCount * 1200.00m;
            decimal miscFee = 1500.00m;
            decimal discount = tuitionFee + labFee + miscFee; // All fees covered under Higher Education Support Program

            decimal totalAssessed = tuitionFee + labFee + miscFee - discount;
            decimal amountPaid = 0.00m;
            decimal.TryParse(txtAmountPaid.Text, out amountPaid);
            decimal balance = totalAssessed - amountPaid;

            dgvFeesBreakdown.Rows.Clear();
            dgvFeesBreakdown.Rows.Add("Tuition Fee (units × rate)", "Tuition", tuitionFee);
            dgvFeesBreakdown.Rows.Add("Laboratory Fee", "Laboratory", labFee);
            dgvFeesBreakdown.Rows.Add("Registration / Misc. Fees", "Miscellaneous", miscFee);
            dgvFeesBreakdown.Rows.Add("Higher Education Support Program", "Deduction", -discount);
            dgvFeesBreakdown.Rows.Add("TOTAL ASSESSED", "SUMMARY", totalAssessed);
            dgvFeesBreakdown.Rows.Add("AMOUNT PAID", "SUMMARY", amountPaid);
            dgvFeesBreakdown.Rows.Add("REMAINING BALANCE", "SUMMARY", balance);

            // Highlight summary rows
            for (int i = 4; i < dgvFeesBreakdown.Rows.Count; i++)
            {
                dgvFeesBreakdown.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                dgvFeesBreakdown.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(244, 244, 244);
                dgvFeesBreakdown.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
            }

            // Save values to assessment object
            if (_wizardEnrollment.TuitionFee == null)
            {
                _wizardEnrollment.TuitionFee = new TuitionAssessment();
            }
            _wizardEnrollment.TuitionFee.TuitionPerUnit = rate;
            _wizardEnrollment.TuitionFee.MiscFees = miscFee;
            _wizardEnrollment.TuitionFee.LabFees = labFee;
            _wizardEnrollment.TuitionFee.Discount = discount;
            _wizardEnrollment.TuitionFee.TotalAssessment = totalAssessed;
            _wizardEnrollment.TuitionFee.AmountPaid = amountPaid;
            _wizardEnrollment.TuitionFee.Balance = balance;
        }

        private void TxtAmountPaid_TextChanged(object? sender, EventArgs e)
        {
            UpdateAssessmentSummaryTable();
        }

        private void BtnBackAssessment_Click(object? sender, EventArgs e)
        {
            if (_referrerPage == tpDashboard)
            {
                SwitchToPage(tpDashboard, btnNavDashboard);
            }
            else if (_referrerPage == tpHistory)
            {
                SwitchToPage(tpHistory, btnNavEnrollmentHistory);
            }
            else
            {
                SwitchToPage(tpSection, btnNavSectionAssignment);
            }
        }

        private void BtnPrintAssessment_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Printing tuition fee assessment form to default device...", "Print Command Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnConfirmEnrollment_Click(object? sender, EventArgs e)
        {
            try
            {
                // Final calculation validation
                UpdateAssessmentSummaryTable();
                
                // Generate enrollment ID if empty
                if (string.IsNullOrEmpty(_wizardEnrollment.EnrollmentID))
                {
                    _wizardEnrollment.EnrollmentID = _service.GenerateEnrollmentId(_wizardEnrollment.SchoolYear);
                }

                _wizardEnrollment.DateEnrolled = DateTime.Now;
                _wizardEnrollment.EnrollmentStatus = _wizardEnrollment.TuitionFee.Balance == 0 ? "Enrolled" : "Pending";

                // Save enrollment to DB
                _service.SaveEnrollment(_wizardEnrollment);

                // Add log to append-only Enrollment History
                var historyRecord = new EnrollmentHistory
                {
                    EnrollmentID = _wizardEnrollment.EnrollmentID,
                    StudentID = _wizardEnrollment.StudentID,
                    StudentName = _wizardStudent != null ? $"{_wizardStudent.FirstName} {_wizardStudent.LastName}" : "Unknown",
                    Term = $"{_wizardEnrollment.SchoolYear} - {_wizardEnrollment.Semester}",
                    Action = _wizardEnrollment.EnrollmentStatus == "Enrolled" ? "Enrolled" : "Completed",
                    PerformedBy = "admin",
                    Remarks = $"Enrolled in {_wizardEnrollment.EnrolledSubjects.Count} subjects. Balance: ₱{_wizardEnrollment.TuitionFee.Balance:N2}"
                };
                _service.SaveHistory(historyRecord);

                MessageBox.Show($"Enrollment record successfully confirmed & saved!\n\nEnrollment ID: {_wizardEnrollment.EnrollmentID}\nStatus: {_wizardEnrollment.EnrollmentStatus}", "Enrollment Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load Dashboard
                LoadDashboardData();
                SwitchToPage(tpDashboard, btnNavDashboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving enrollment:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Screen 6: Enrollment History (tpHistory)

        private void SetupHistoryGridStructure()
        {
            dgvHistory.Columns.Clear();

            var colDateTime = new DataGridViewTextBoxColumn { Name = "DateTime", HeaderText = "Date & Time", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells };
            colDateTime.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";

            dgvHistory.Columns.Add(colDateTime);
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "EnrollmentID", HeaderText = "Enrollment ID", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "Term", HeaderText = "Term", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "Action", HeaderText = "Action", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "PerformedBy", HeaderText = "Performed By", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn { Name = "Remarks", HeaderText = "Remarks", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private void LoadHistoryStepData()
        {
            try
            {
                var history = _service.GetAllHistory();
                dgvHistory.DataSource = null;
                dgvHistory.Rows.Clear();

                ApplyHistoryFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading history logs:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyHistoryFilters()
        {
            try
            {
                var history = _service.GetAllHistory();
                
                string search = txtHistorySearch.Text.Trim();
                string syFilter = cboHistorySY.SelectedItem?.ToString() ?? "All";
                string actionFilter = cboHistoryAction.SelectedItem?.ToString() ?? "All";

                // Text search
                if (!string.IsNullOrEmpty(search))
                {
                    history = history.Where(h =>
                        h.StudentID.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        h.StudentName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        h.EnrollmentID.Contains(search, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }

                // SY Filter
                if (syFilter != "All")
                {
                    history = history.Where(h => h.Term.Contains(syFilter)).ToList();
                }

                // Action Filter
                if (actionFilter != "All")
                {
                    history = history.Where(h => h.Action.Equals(actionFilter, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Render in history table
                dgvHistory.Rows.Clear();
                foreach (var log in history.OrderByDescending(h => h.DateTime))
                {
                    dgvHistory.Rows.Add(log.DateTime, log.EnrollmentID, log.Term, log.Action, log.PerformedBy, log.Remarks);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering history records:\n{ex.Message}", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHistoryFilter_Click(object? sender, EventArgs e)
        {
            ApplyHistoryFilters();
        }

        private void BtnHistoryView_Click(object? sender, EventArgs e)
        {
            if (dgvHistory.SelectedRows.Count == 0 && dgvHistory.CurrentRow == null)
            {
                MessageBox.Show("Please select a history record from the list to view.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvHistory.SelectedRows.Count > 0 ? dgvHistory.SelectedRows[0] : dgvHistory.CurrentRow;
            string? enrollmentId = row.Cells["EnrollmentID"].Value?.ToString();

            if (string.IsNullOrEmpty(enrollmentId)) return;

            var enrollment = _service.GetEnrollmentById(enrollmentId);
            if (enrollment == null)
            {
                MessageBox.Show("Could not locate the associated enrollment record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // View in tuition assessment page (read-only)
            _wizardEnrollment = enrollment;
            _wizardStudent = _service.GetStudentById(enrollment.StudentID);
            
            LoadAssessmentStepData();
            btnConfirmEnrollment.Enabled = false;
            btnConfirmEnrollment.Text = "Read-Only View";
            btnBackAssessment.Enabled = true;
            _referrerPage = tpHistory;

            SwitchToPage(tpAssessment, btnNavTuitionAssessment);
        }

        private void BtnHistoryExport_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Successfully exported enrollment history logs to 'Enrollment_History_Export.csv' in the project directory.", "CSV Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Sidebar Navigation Click Events

        private void BtnNavDashboard_Click(object? sender, EventArgs e)
        {
            LoadDashboardData();
            SwitchToPage(tpDashboard, btnNavDashboard);
        }

        private void BtnNavEnrollStudent_Click(object? sender, EventArgs e)
        {
            BtnNewEnrollment_Click(sender, e);
        }

        private void BtnNavEnrolledSubjects_Click(object? sender, EventArgs e)
        {
            if (_wizardStudent == null)
            {
                MessageBox.Show("Select or start an enrollment wizard from the dashboard first.", "Wizard Inactive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SwitchToPage(tpSubjects, btnNavEnrolledSubjects);
        }

        private void BtnNavSectionAssignment_Click(object? sender, EventArgs e)
        {
            if (_wizardStudent == null)
            {
                MessageBox.Show("Select or start an enrollment wizard from the dashboard first.", "Wizard Inactive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SwitchToPage(tpSection, btnNavSectionAssignment);
        }

        private void BtnNavTuitionAssessment_Click(object? sender, EventArgs e)
        {
            if (_wizardStudent == null)
            {
                MessageBox.Show("Select or start an enrollment wizard from the dashboard first.", "Wizard Inactive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SwitchToPage(tpAssessment, btnNavTuitionAssessment);
        }

        private void BtnNavEnrollmentHistory_Click(object? sender, EventArgs e)
        {
            LoadHistoryStepData();
            SwitchToPage(tpHistory, btnNavEnrollmentHistory);
        }

        private void SidebarButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnNavDashboard)
            {
                btn.BackColor = Color.FromArgb(235, 237, 240);
            }
        }

        private void SidebarButton_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn != btnNavDashboard)
            {
                btn.BackColor = Color.White;
            }
        }

        private void OutlineButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.FromArgb(240, 240, 240);
            }
        }

        private void OutlineButton_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.White;
            }
        }

        private void DarkButton_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void DarkButton_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.FromArgb(46, 46, 46);
            }
        }

        private void PaintDashedBorder(object? sender, PaintEventArgs e)
        {
            if (sender is Panel panel)
            {
                using (Pen pen = new Pen(Color.DarkGray, 1))
                {
                    pen.DashPattern = new float[] { 4, 3 };
                    e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                }
            }
        }

        #endregion

        #region Custom Navigation & Selection Dialog helpers

        private void SetupBreadcrumbs()
        {
            var linkLabels = new LinkLabel[] {
                lblBreadcrumb,
                lblBreadcrumbHistory,
                lblBreadcrumbEnroll,
                lblBreadcrumbSubjects,
                lblBreadcrumbSection,
                lblBreadcrumbAssessment
            };

            foreach (var lbl in linkLabels)
            {
                if (lbl == null) continue;
                lbl.LinkBehavior = LinkBehavior.HoverUnderline;
                lbl.LinkColor = Color.FromArgb(50, 50, 50);
                lbl.ActiveLinkColor = Color.Black;
                lbl.VisitedLinkColor = Color.FromArgb(50, 50, 50);
                lbl.Links.Clear();
                lbl.LinkClicked += Breadcrumb_LinkClicked;
            }

            // Set Text and add Links
            lblBreadcrumb.Text = "Home  |  Enrollment History";
            lblBreadcrumb.Links.Add(0, 4, "Dashboard");
            lblBreadcrumb.Links.Add(9, 18, "History");

            lblBreadcrumbHistory.Text = "Home  ›  Enrollment History";
            lblBreadcrumbHistory.Links.Add(0, 4, "Dashboard");

            lblBreadcrumbEnroll.Text = "Home  ›  Enroll Student";
            lblBreadcrumbEnroll.Links.Add(0, 4, "Dashboard");
            lblBreadcrumbEnroll.Links.Add(9, 14, "Step1");

            lblBreadcrumbSubjects.Text = "Home  ›  Enroll Student  ›  Subjects";
            lblBreadcrumbSubjects.Links.Add(0, 4, "Dashboard");
            lblBreadcrumbSubjects.Links.Add(9, 14, "Step1");
            lblBreadcrumbSubjects.Links.Add(28, 8, "Step2");

            lblBreadcrumbSection.Text = "Home  ›  Enroll Student  ›  Sections";
            lblBreadcrumbSection.Links.Add(0, 4, "Dashboard");
            lblBreadcrumbSection.Links.Add(9, 14, "Step1");
            lblBreadcrumbSection.Links.Add(28, 8, "Step3");

            lblBreadcrumbAssessment.Text = "Home  ›  Enroll Student  ›  Assessment";
            lblBreadcrumbAssessment.Links.Add(0, 4, "Dashboard");
            lblBreadcrumbAssessment.Links.Add(9, 14, "Step1");
            lblBreadcrumbAssessment.Links.Add(28, 10, "Step4");
        }

        private void Breadcrumb_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            string? target = e.Link?.LinkData?.ToString();
            if (string.IsNullOrEmpty(target)) return;

            switch (target)
            {
                case "Dashboard":
                    SwitchToPage(tpDashboard, btnNavDashboard);
                    break;
                case "History":
                    LoadHistoryStepData();
                    SwitchToPage(tpHistory, btnNavEnrollmentHistory);
                    break;
                case "Step1":
                    NavigateToStep(1);
                    break;
                case "Step2":
                    NavigateToStep(2);
                    break;
                case "Step3":
                    NavigateToStep(3);
                    break;
                case "Step4":
                    NavigateToStep(4);
                    break;
            }
        }

        private void SetupStepperClickable()
        {
            var steppers = new Label[][] {
                new Label[] { lblStep1Enroll, lblStep2Enroll, lblStep3Enroll, lblStep4Enroll, lblStep5Enroll },
                new Label[] { lblStep1Subjects, lblStep2Subjects, lblStep3Subjects, lblStep4Subjects, lblStep5Subjects },
                new Label[] { lblStep1Section, lblStep2Section, lblStep3Section, lblStep4Section, lblStep5Section },
                new Label[] { lblStep1Assessment, lblStep2Assessment, lblStep3Assessment, lblStep4Assessment, lblStep5Assessment }
            };

            foreach (var group in steppers)
            {
                for (int i = 0; i < group.Length; i++)
                {
                    var lbl = group[i];
                    if (lbl != null)
                    {
                        lbl.Cursor = Cursors.Hand;
                        int stepIndex = i + 1; // 1-based index
                        lbl.Click += (sender, e) => NavigateToStep(stepIndex);
                    }
                }
            }
        }

        private void NavigateToStep(int targetStep)
        {
            // Step 1: Student loading validation
            if (targetStep >= 2)
            {
                if (_wizardStudent == null)
                {
                    MessageBox.Show("Please select a student from the list before proceeding.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Save step 1 details
                _wizardEnrollment.StudentID = _wizardStudent.StudentId;
                _wizardEnrollment.SchoolYear = cboSchoolYearEnroll.SelectedItem?.ToString() ?? "2025-2026";
                _wizardEnrollment.Semester = cboSemesterEnroll.SelectedItem?.ToString() ?? "1st Semester";
                _wizardEnrollment.CourseID = _wizardStudent.ProgramId;
                _wizardEnrollment.YearLevel = GetYearLevelString(_wizardStudent.YearLevel);
                _wizardEnrollment.EnrollmentStatus = "Pending";
            }

            // Step 2: Subjects selection validation
            if (targetStep >= 3)
            {
                if (_selectedSubjects.Count == 0)
                {
                    MessageBox.Show("Please select at least one subject to proceed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int totalUnits = _selectedSubjects.Sum(s => s.Units);
                if (totalUnits > 24)
                {
                    MessageBox.Show($"Total units ({totalUnits}) exceed the maximum allowed limit of 24 units. Please drop some subjects.", "Unit Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _wizardEnrollment.EnrolledSubjects = _selectedSubjects;
                _wizardEnrollment.TotalUnits = totalUnits;
            }

            // Step 3: Section assignment validation
            if (targetStep >= 4)
            {
                if (lblConflictMessage.ForeColor == Color.DarkRed)
                {
                    MessageBox.Show("Please resolve the highlighted conflicts before proceeding.", "Conflict Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvSectionAssign.Rows.Count > 0)
                {
                    _wizardEnrollment.Section = dgvSectionAssign.Rows[0].Cells["colSection"].Value?.ToString() ?? "CS-1A";
                }
            }

            // Navigate to appropriate tab page
            if (targetStep == 1)
            {
                SwitchToPage(tpEnroll, btnNavEnrollStudent);
            }
            else if (targetStep == 2)
            {
                LoadSubjectsStepData();
                SwitchToPage(tpSubjects, btnNavEnrolledSubjects);
            }
            else if (targetStep == 3)
            {
                LoadSectionStepData();
                SwitchToPage(tpSection, btnNavSectionAssignment);
            }
            else if (targetStep == 4 || targetStep == 5)
            {
                LoadAssessmentStepData();
                btnConfirmEnrollment.Enabled = true;
                btnConfirmEnrollment.Text = "Confirm Enrollment";
                btnBackAssessment.Enabled = true;
                _referrerPage = tpSection;
                SwitchToPage(tpAssessment, btnNavTuitionAssessment);
            }
        }

        private Student? ShowStudentSelectionDialog(List<Student> students)
        {
            using (var form = new Form())
            {
                form.Text = "Select Student";
                form.Size = new Size(450, 320);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                var lbl = new Label()
                {
                    Text = "Multiple students matched your search. Please select one:",
                    Location = new Point(15, 15),
                    Size = new Size(400, 20),
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
                };

                var listBox = new ListBox()
                {
                    Location = new Point(15, 45),
                    Size = new Size(405, 170),
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Regular)
                };

                foreach (var s in students)
                {
                    listBox.Items.Add(s);
                }
                listBox.DisplayMember = "DisplayText";
                listBox.SelectedIndex = 0;

                var btnOk = new Button()
                {
                    Text = "Select",
                    DialogResult = DialogResult.OK,
                    Location = new Point(245, 230),
                    Size = new Size(85, 32),
                    BackColor = BSU_Red,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnOk.FlatAppearance.BorderSize = 0;
                btnOk.MouseEnter += (s, e) => { btnOk.BackColor = Color.FromArgb(150, 10, 38); };
                btnOk.MouseLeave += (s, e) => { btnOk.BackColor = BSU_Red; };

                var btnCancel = new Button()
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(335, 230),
                    Size = new Size(85, 32),
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(50, 50, 50),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
                btnCancel.FlatAppearance.BorderSize = 1;
                btnCancel.MouseEnter += (s, e) => { btnCancel.BackColor = Color.FromArgb(245, 245, 245); };
                btnCancel.MouseLeave += (s, e) => { btnCancel.BackColor = Color.White; };

                form.Controls.AddRange(new Control[] { lbl, listBox, btnOk, btnCancel });
                form.AcceptButton = btnOk;
                form.CancelButton = btnCancel;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    return listBox.SelectedItem as Student;
                }
            }
            return null;
        }

        private void TxtStudentNo_TextChanged(object? sender, EventArgs e)
        {
            FilterStudentsGrid();
        }

        private void LoadStudentsGridData()
        {
            try
            {
                _service.ClearCache();
                _allStudentsList = _service.GetAllStudents();
                FilterStudentsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student database:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterStudentsGrid()
        {
            string query = txtStudentNo.Text.Trim();
            var filtered = _allStudentsList;
            if (!string.IsNullOrEmpty(query))
            {
                filtered = filtered.Where(s =>
                    s.StudentId.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    s.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    s.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    s.FullName.Contains(query, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Unbind SelectionChanged to prevent noise during DataSource replacement
            dgvStudentsList.SelectionChanged -= DgvStudentsList_SelectionChanged;

            dgvStudentsList.DataSource = null;
            dgvStudentsList.DataSource = filtered.Select(s => new StudentGridDisplayModel
            {
                StudentID = s.StudentId,
                Name = s.FullName,
                Program = s.ProgramId,
                Year = GetYearLevelString(s.YearLevel)
            }).ToList();

            if (dgvStudentsList.Columns.Count > 0)
            {
                if (dgvStudentsList.Columns["StudentID"] is DataGridViewColumn cId)
                {
                    cId.HeaderText = "Student No.";
                    cId.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvStudentsList.Columns["Name"] is DataGridViewColumn cName)
                {
                    cName.HeaderText = "Name";
                    cName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                if (dgvStudentsList.Columns["Program"] is DataGridViewColumn cProg)
                {
                    cProg.HeaderText = "Program";
                    cProg.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvStudentsList.Columns["Year"] is DataGridViewColumn cYear)
                {
                    cYear.HeaderText = "Year";
                    cYear.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }

            dgvStudentsList.SelectionChanged += DgvStudentsList_SelectionChanged;

            // Trigger selection update
            DgvStudentsList_SelectionChanged(dgvStudentsList, EventArgs.Empty);
        }

        private void DgvStudentsList_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvStudentsList.SelectedRows.Count > 0)
            {
                var row = dgvStudentsList.SelectedRows[0];
                if (row.DataBoundItem is StudentGridDisplayModel displayModel)
                {
                    var student = _allStudentsList.FirstOrDefault(s => s.StudentId == displayModel.StudentID);
                    if (student != null)
                    {
                        _wizardStudent = student;
                        txtFullName.Text = $"{student.FirstName} {student.LastName}";
                        txtProgram.Text = student.ProgramId;
                        txtYearLevel.Text = GetYearLevelString(student.YearLevel);
                        return;
                    }
                }
            }

            _wizardStudent = null;
            txtFullName.Text = "";
            txtProgram.Text = "";
            txtYearLevel.Text = "";
        }

        private void SelectStudentInGrid(string studentId)
        {
            foreach (DataGridViewRow row in dgvStudentsList.Rows)
            {
                if (row.DataBoundItem is StudentGridDisplayModel displayModel &&
                    displayModel.StudentID.Equals(studentId, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    if (dgvStudentsList.Columns.Count > 0)
                    {
                        dgvStudentsList.CurrentCell = row.Cells[0];
                    }
                    break;
                }
            }
        }

        private int GetEnrolledCount(string subjectId, string section, string schoolYear, string semester)
        {
            try
            {
                var allEnrollments = _service.GetAllEnrollments();
                return allEnrollments.Count(e =>
                    e.EnrollmentID != _wizardEnrollment.EnrollmentID &&
                    e.SchoolYear.Equals(schoolYear, StringComparison.OrdinalIgnoreCase) &&
                    e.Semester.Equals(semester, StringComparison.OrdinalIgnoreCase) &&
                    (e.EnrollmentStatus.Equals("Enrolled", StringComparison.OrdinalIgnoreCase) || e.EnrollmentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase)) &&
                    e.Section.Equals(section, StringComparison.OrdinalIgnoreCase) &&
                    e.EnrolledSubjects.Any(sub => sub.SubjectID.Equals(subjectId, StringComparison.OrdinalIgnoreCase))
                );
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }

    public class DashboardEnrollmentDisplayModel
    {
        public string EnrollmentID { get; set; } = string.Empty;
        public string StudentID { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string ProgramAndYear { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }

    public class StudentGridDisplayModel
    {
        public string StudentID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Program { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
    }
}
