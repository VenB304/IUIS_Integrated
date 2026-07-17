using IUIS.Core.Models;

namespace IUIS.Modules.Team6.Forms
{
    /// <summary>
    /// Modal dialog for adding or editing a Department record.
    /// </summary>
    public class DepartmentForm : Form
    {
        public Department? Department { get; private set; }

        private readonly TextBox _txtName     = new() { Width = 260 };
        private readonly ComboBox _cbHead     = new() { Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
        private readonly TextBox _txtLocation = new() { Width = 260 };

        private readonly Department? _existing;
        private readonly List<Employee> _employees;

        public DepartmentForm(string title, List<Employee> employees, Department? existing = null)
        {
            _existing = existing;
            _employees = employees;
            Text = title;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox   = false;
            MinimizeBox   = false;
            StartPosition = FormStartPosition.CenterParent;
            Width  = 400;
            Height = 230;

            // Head dropdown
            _cbHead.Items.Add("(None)");
            foreach (var emp in employees)
                _cbHead.Items.Add(emp.FullName);
            _cbHead.SelectedIndex = 0;

            if (existing != null)
            {
                _txtName.Text     = existing.Name;
                _txtLocation.Text = existing.Location;

                var headEmp = employees.FirstOrDefault(e => e.EmployeeId == existing.DepartmentHeadId);
                var headName = headEmp?.FullName ?? "(None)";
                var hi = _cbHead.Items.IndexOf(headName);
                if (hi >= 0) _cbHead.SelectedIndex = hi;
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
                RowCount    = 5,
                AutoSize    = true
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            int row = 0;
            void AddRow(string label, Control ctrl)
            {
                layout.Controls.Add(new Label
                {
                    Text    = label,
                    Anchor  = AnchorStyles.Left | AnchorStyles.Top,
                    Padding = new Padding(0, 5, 0, 0)
                }, 0, row);
                layout.Controls.Add(ctrl, 1, row);
                row++;
            }

            AddRow("Name *",   _txtName);
            AddRow("Head",     _cbHead);
            AddRow("Location", _txtLocation);

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
            if (string.IsNullOrWhiteSpace(_txtName.Text))
            {
                MessageBox.Show("Department Name is required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            var selectedHeadName = _cbHead.SelectedItem?.ToString();
            var headEmp = _employees.FirstOrDefault(e => e.FullName == selectedHeadName);
            var headId = headEmp?.EmployeeId ?? string.Empty;

            Department = new Department
            {
                DepartmentId     = _existing?.DepartmentId ?? string.Empty,
                Name             = _txtName.Text.Trim(),
                DepartmentHeadId = headId,
                Location         = _txtLocation.Text.Trim()
            };
        }
    }
}

