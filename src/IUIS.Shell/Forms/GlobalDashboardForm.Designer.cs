using IUIS.Core.Theming;

namespace IUIS.Shell.Forms
{
    partial class GlobalDashboardForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalDashboardForm));
            _clockTimer = new System.Windows.Forms.Timer(components);
            _header = new Panel();
            _rightPanel = new FlowLayoutPanel();
            _btnLogout = new Button();
            _lblWelcome = new Label();
            _lblHeaderTitle = new Label();
            _lblClock = new Label();
            _picLogo = new PictureBox();
            _cardsPanel = new FlowLayoutPanel();
            _header.SuspendLayout();
            _rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_picLogo).BeginInit();
            SuspendLayout();
            // 
            // _clockTimer
            // 
            _clockTimer.Interval = 1000;
            // 
            // _header
            // 
            _header.BackColor = Color.FromArgb(139, 0, 26);
            _header.Controls.Add(_rightPanel);
            _header.Controls.Add(_lblHeaderTitle);
            _header.Controls.Add(_lblClock);
            _header.Controls.Add(_picLogo);
            _header.Dock = DockStyle.Top;
            _header.Location = new Point(0, 0);
            _header.Name = "_header";
            _header.Size = new Size(984, 100);
            _header.TabIndex = 0;
            // 
            // _rightPanel
            // 
            _rightPanel.AutoSize = true;
            _rightPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _rightPanel.BackColor = Color.Transparent;
            _rightPanel.Controls.Add(_btnLogout);
            _rightPanel.Controls.Add(_lblWelcome);
            _rightPanel.Dock = DockStyle.Right;
            _rightPanel.FlowDirection = FlowDirection.RightToLeft;
            _rightPanel.Location = new Point(867, 0);
            _rightPanel.Name = "_rightPanel";
            _rightPanel.Padding = new Padding(0, 33, 25, 0);
            _rightPanel.Size = new Size(117, 100);
            _rightPanel.TabIndex = 0;
            _rightPanel.WrapContents = false;
            // 
            // _btnLogout
            // 
            _btnLogout.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            _btnLogout.Location = new Point(12, 33);
            _btnLogout.Margin = new Padding(0);
            _btnLogout.Name = "_btnLogout";
            _btnLogout.Size = new Size(80, 30);
            _btnLogout.TabIndex = 0;
            _btnLogout.Text = "Log Out";
            _btnLogout.Click += BtnLogout_Click;
            // 
            // _lblWelcome
            // 
            _lblWelcome.AutoSize = true;
            _lblWelcome.Font = new Font("Segoe UI", 12F);
            _lblWelcome.ForeColor = Color.FromArgb(255, 210, 44);
            _lblWelcome.Location = new Point(0, 39);
            _lblWelcome.Margin = new Padding(0, 6, 12, 0);
            _lblWelcome.Name = "_lblWelcome";
            _lblWelcome.Size = new Size(0, 21);
            _lblWelcome.TabIndex = 2;
            // 
            // _lblHeaderTitle
            // 
            _lblHeaderTitle.AutoSize = true;
            _lblHeaderTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            _lblHeaderTitle.ForeColor = Color.White;
            _lblHeaderTitle.Location = new Point(105, 20);
            _lblHeaderTitle.Name = "_lblHeaderTitle";
            _lblHeaderTitle.Size = new Size(448, 41);
            _lblHeaderTitle.TabIndex = 1;
            _lblHeaderTitle.Text = "Batangas State University - IUIS";
            // 
            // _lblClock
            // 
            _lblClock.AutoSize = true;
            _lblClock.Font = new Font("Segoe UI", 10F);
            _lblClock.ForeColor = Color.FromArgb(220, 220, 220);
            _lblClock.Location = new Point(110, 60);
            _lblClock.Name = "_lblClock";
            _lblClock.Size = new Size(0, 19);
            _lblClock.TabIndex = 2;
            // 
            // _picLogo
            // 
            _picLogo.BackColor = Color.Transparent;
            _picLogo.Image = (Image)resources.GetObject("_picLogo.Image");
            _picLogo.Location = new Point(25, 18);
            _picLogo.Name = "_picLogo";
            _picLogo.Size = new Size(64, 64);
            _picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            _picLogo.TabIndex = 3;
            _picLogo.TabStop = false;
            // 
            // _cardsPanel
            // 
            _cardsPanel.AutoScroll = true;
            _cardsPanel.Dock = DockStyle.Fill;
            _cardsPanel.Location = new Point(0, 100);
            _cardsPanel.Name = "_cardsPanel";
            _cardsPanel.Padding = new Padding(35);
            _cardsPanel.Size = new Size(984, 861);
            _cardsPanel.TabIndex = 1;
            // 
            // GlobalDashboardForm
            // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(984, 961);
            Controls.Add(_cardsPanel);
            Controls.Add(_header);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "GlobalDashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Batangas State University - IUIS Dashboard";
            _header.ResumeLayout(false);
            _header.PerformLayout();
            _rightPanel.ResumeLayout(false);
            _rightPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer _clockTimer;
        private Panel _header;
        private FlowLayoutPanel _rightPanel;
        private Button _btnLogout;
        private Label _lblWelcome;
        private Label _lblHeaderTitle;
        private Label _lblClock;
        private FlowLayoutPanel _cardsPanel;
        private PictureBox _picLogo;
    }
}
