using IUIS.Core.Models;

namespace IUIS.Modules.Team6.Forms
{
    /// <summary>
    /// Simple modal dialog that shows a ComboBox of active employees to pick from.
    /// Used by Record Time In and Mark Absent / On Leave actions.
    /// </summary>
    public class SelectEmployeeForm : Form
    {
        public Employee? SelectedEmployee { get; private set; }

        private readonly ComboBox _cbEmployee = new()
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width         = 300
        };

        private readonly List<Employee> _employees;

        public SelectEmployeeForm(List<Employee> employees)
        {
            _employees = employees;
            Text = "Select Employee";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox   = false;
            MinimizeBox   = false;
            StartPosition = FormStartPosition.CenterParent;
            Width  = 400;
            Height = 150;

            foreach (var emp in employees)
                _cbEmployee.Items.Add(emp.FullName);

            if (_cbEmployee.Items.Count > 0)
                _cbEmployee.SelectedIndex = 0;

            BuildLayout();
        }

        private void BuildLayout()
        {
            var layout = new TableLayoutPanel
            {
                Dock        = DockStyle.Fill,
                Padding     = new Padding(14),
                ColumnCount = 2,
                RowCount    = 2,
                AutoSize    = true
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            layout.Controls.Add(new Label
            {
                Text    = "Employee:",
                Anchor  = AnchorStyles.Left | AnchorStyles.Top,
                Padding = new Padding(0, 5, 0, 0)
            }, 0, 0);
            layout.Controls.Add(_cbEmployee, 1, 0);

            var btnOK     = new Button { Text = "OK",     DialogResult = DialogResult.OK,     Width = 80 };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 80 };

            btnOK.Click += (s, e) =>
            {
                if (_cbEmployee.SelectedIndex >= 0)
                    SelectedEmployee = _employees[_cbEmployee.SelectedIndex];
            };

            var btnPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock          = DockStyle.Fill
            };
            btnPanel.Controls.AddRange(new Control[] { btnCancel, btnOK });
            layout.SetColumnSpan(btnPanel, 2);
            layout.Controls.Add(btnPanel, 0, 1);

            Controls.Add(layout);
            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }
    }
}
