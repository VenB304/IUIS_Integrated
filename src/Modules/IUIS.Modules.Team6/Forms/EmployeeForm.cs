using IUIS.Core.Models;

namespace IUIS.Modules.Team6.Forms
{
    /// <summary>
    /// Modal dialog for adding or editing an Employee record.
    /// Pass an existing Employee to pre-populate for editing; leave null to add new.
    /// </summary>
    public class EmployeeForm : Form
    {
        public Employee? Employee { get; private set; }

        private readonly TextBox _txtFirstName = new() { Width = 250 };
        private readonly TextBox _txtMiddleName = new() { Width = 250 };
        private readonly TextBox _txtLastName  = new() { Width = 250 };
        private readonly TextBox _txtPosition  = new() { Width = 250 };
        private readonly ComboBox _cbSex        = new() { Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DateTimePicker _dtpBirthDate = new() { Width = 250, Format = DateTimePickerFormat.Short };
        private readonly TextBox _txtPhone   = new() { Width = 250 };
        private readonly TextBox _txtEmail   = new() { Width = 250 };
        private readonly TextBox _txtHourlyRate = new() { Width = 250 };
        private readonly ComboBox _cbDepartment = new() { Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly ComboBox _cbStatus     = new() { Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly DateTimePicker _dtpDateHired = new() { Width = 250, Format = DateTimePickerFormat.Short };
        private readonly TextBox _txtAddress = new() { Width = 250, Multiline = true, Height = 48, ScrollBars = ScrollBars.Vertical };

        private readonly Employee? _existing;
        private readonly List<Department> _departments;

        public EmployeeForm(string title, List<Department> departments, Employee? existing = null)
        {
            _existing = existing;
            _departments = departments;
            Text = title;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox   = false;
            MinimizeBox   = false;
            StartPosition = FormStartPosition.CenterParent;
            Width  = 420;
            Height = 650;

            // Department dropdown
            _cbDepartment.Items.Add("(None)");
            foreach (var d in departments)
                _cbDepartment.Items.Add(d.Name);
            _cbDepartment.SelectedIndex = 0;

            // Sex dropdown
            _cbSex.Items.AddRange(new object[] { "Male", "Female" });
            _cbSex.SelectedIndex = 0;

            // Status dropdown — matches Day 3 spec: Active, Inactive, Resigned, Terminated, Retired
            _cbStatus.Items.AddRange(new object[] { "Active", "Inactive", "Resigned", "Terminated", "Retired" });
            _cbStatus.SelectedIndex = 0;

            // Pre-populate when editing
            if (existing != null)
            {
                _txtFirstName.Text = existing.FirstName;
                _txtMiddleName.Text = existing.MiddleName;
                _txtLastName.Text  = existing.LastName;
                _txtPosition.Text  = existing.Position;
                
                var deptObj = departments.FirstOrDefault(d => d.DepartmentId == existing.DepartmentId);
                var deptName = deptObj?.Name ?? "(None)";
                var di = _cbDepartment.Items.IndexOf(deptName);
                if (di >= 0) _cbDepartment.SelectedIndex = di;

                var sexi = _cbSex.Items.IndexOf(existing.Sex);
                if (sexi >= 0) _cbSex.SelectedIndex = sexi;

                var si = _cbStatus.Items.IndexOf(existing.EmploymentStatus);
                if (si >= 0) _cbStatus.SelectedIndex = si;

                if (DateTime.TryParse(existing.DateHired, out var dh)) _dtpDateHired.Value = dh;
                if (DateTime.TryParse(existing.BirthDate, out var bd)) _dtpBirthDate.Value = bd;
                
                _txtEmail.Text      = existing.Email;
                _txtPhone.Text      = existing.ContactNumber;
                _txtHourlyRate.Text = existing.HourlyRate > 0 ? existing.HourlyRate.ToString("F2") : string.Empty;
                _txtAddress.Text    = existing.Address;
            }

            BuildLayout();
        }

        private void BuildLayout()
        {
            var layout = new TableLayoutPanel
            {
                Dock        = DockStyle.Fill,
                Padding     = new Padding(14),
                ColumnCount = 2,
                RowCount    = 15,
                AutoSize    = true
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            int row = 0;
            void AddRow(string label, Control ctrl, int height = 0)
            {
                if (height > 0) ctrl.Height = height;
                layout.Controls.Add(new Label
                {
                    Text    = label,
                    Anchor  = AnchorStyles.Left | AnchorStyles.Top,
                    Padding = new Padding(0, 5, 0, 0)
                }, 0, row);
                layout.Controls.Add(ctrl, 1, row);
                row++;
            }

            AddRow("First Name *", _txtFirstName);
            AddRow("Middle Name",  _txtMiddleName);
            AddRow("Last Name *",  _txtLastName);
            AddRow("Position *",   _txtPosition);
            AddRow("Sex",          _cbSex);
            AddRow("Birth Date",   _dtpBirthDate);
            AddRow("Contact No.",  _txtPhone);
            AddRow("Email",        _txtEmail);
            AddRow("Hourly Rate",  _txtHourlyRate);
            AddRow("Department",   _cbDepartment);
            AddRow("Status",       _cbStatus);
            AddRow("Date Hired",   _dtpDateHired);
            AddRow("Address",      _txtAddress, 48);

            var btnSave   = new Button { Text = "Save",   DialogResult = DialogResult.OK,     Width = 80 };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 80 };
            btnSave.Click += BtnSave_Click;

            var btnPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock          = DockStyle.Fill
            };
            btnPanel.Controls.AddRange(new Control[] { btnCancel, btnSave });

            layout.SetColumnSpan(btnPanel, 2);
            layout.Controls.Add(btnPanel, 0, row);

            Controls.Add(layout);
            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(_txtLastName.Text)  ||
                string.IsNullOrWhiteSpace(_txtPosition.Text))
            {
                MessageBox.Show("First Name, Last Name, and Position are required (*)",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            if (_txtFirstName.Text.Any(char.IsDigit) ||
                _txtMiddleName.Text.Any(char.IsDigit) ||
                _txtLastName.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Employee names (First Name, Middle Name, Last Name) cannot contain numbers.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            var phone = _txtPhone.Text.Trim();
            if (!string.IsNullOrEmpty(phone))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^09\d{9}$"))
                {
                    MessageBox.Show("Contact number must be a valid Philippine mobile number starting with '09' followed by 9 digits (e.g., 09123456789).",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            if (_dtpBirthDate.Value > DateTime.Today)
            {
                MessageBox.Show("Birth Date cannot be in the future.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            if (_dtpDateHired.Value < _dtpBirthDate.Value)
            {
                MessageBox.Show("Date Hired cannot be earlier than Birth Date.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            var email = _txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Please enter a valid email address (e.g., employee@example.com).",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            double hourlyRate = 0;
            var hourlyRateText = _txtHourlyRate.Text.Trim();
            if (!string.IsNullOrEmpty(hourlyRateText))
            {
                if (!double.TryParse(hourlyRateText, out hourlyRate) || hourlyRate < 0)
                {
                    MessageBox.Show("Hourly rate must be a valid non-negative number.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
            }

            var deptName = _cbDepartment.SelectedItem?.ToString();
            var deptObj = _departments.FirstOrDefault(d => d.Name == deptName);
            var deptId = deptObj?.DepartmentId ?? string.Empty;

            Employee = new Employee
            {
                EmployeeId       = _existing?.EmployeeId ?? string.Empty,
                FirstName        = _txtFirstName.Text.Trim(),
                MiddleName       = _txtMiddleName.Text.Trim(),
                LastName         = _txtLastName.Text.Trim(),
                Position         = _txtPosition.Text.Trim(),
                Sex              = _cbSex.SelectedItem?.ToString() ?? "Male",
                BirthDate        = _dtpBirthDate.Value.ToString("yyyy-MM-dd"),
                ContactNumber    = _txtPhone.Text.Trim(),
                Email            = _txtEmail.Text.Trim(),
                HourlyRate       = hourlyRate,
                DepartmentId     = deptId,
                EmploymentStatus = _cbStatus.SelectedItem?.ToString() ?? "Active",
                DateHired        = _dtpDateHired.Value.ToString("yyyy-MM-dd"),
                Address          = _txtAddress.Text.Trim()
            };
        }
    }
}

