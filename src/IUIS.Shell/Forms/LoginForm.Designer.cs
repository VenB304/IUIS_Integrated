using IUIS.Core.Theming;

namespace IUIS.Shell.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            _card = new Panel();
            _lblFooter = new Label();
            _lblAttempts = new Label();
            _lblError = new Label();
            _btnLogin = new Button();
            _chkShow = new CheckBox();
            _txtPassword = new TextBox();
            _lblPasswordCaption = new Label();
            _txtUsername = new TextBox();
            _lblUsernameCaption = new Label();
            _divider = new Panel();
            _lblSubtitle = new Label();
            _lblTitleSub = new Label();
            _lblTitleMain = new Label();
            _picLogo = new PictureBox();
            _card.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_picLogo).BeginInit();
            SuspendLayout();
            // 
            // _card
            // 
            _card.BackColor = Color.White;
            _card.Controls.Add(_lblFooter);
            _card.Controls.Add(_lblAttempts);
            _card.Controls.Add(_lblError);
            _card.Controls.Add(_btnLogin);
            _card.Controls.Add(_chkShow);
            _card.Controls.Add(_txtPassword);
            _card.Controls.Add(_lblPasswordCaption);
            _card.Controls.Add(_txtUsername);
            _card.Controls.Add(_lblUsernameCaption);
            _card.Controls.Add(_divider);
            _card.Controls.Add(_lblSubtitle);
            _card.Controls.Add(_lblTitleSub);
            _card.Controls.Add(_lblTitleMain);
            _card.Controls.Add(_picLogo);
            _card.Location = new Point(50, 60);
            _card.Name = "_card";
            _card.Size = new Size(380, 454);
            _card.TabIndex = 0;
            // 
            // _lblFooter
            // 
            _lblFooter.Font = new Font("Segoe UI", 7.5F);
            _lblFooter.ForeColor = Color.FromArgb(190, 190, 190);
            _lblFooter.Location = new Point(0, 430);
            _lblFooter.Name = "_lblFooter";
            _lblFooter.Size = new Size(380, 16);
            _lblFooter.TabIndex = 13;
            _lblFooter.Text = "Batangas State University  ·  CICS";
            _lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lblAttempts
            // 
            _lblAttempts.Font = new Font("Segoe UI", 7.5F);
            _lblAttempts.ForeColor = Color.FromArgb(150, 150, 150);
            _lblAttempts.Location = new Point(40, 434);
            _lblAttempts.Name = "_lblAttempts";
            _lblAttempts.Size = new Size(300, 16);
            _lblAttempts.TabIndex = 12;
            _lblAttempts.TextAlign = ContentAlignment.MiddleCenter;
            _lblAttempts.Visible = false;
            // 
            // _lblError
            // 
            _lblError.Font = new Font("Segoe UI", 8.5F);
            _lblError.ForeColor = Color.FromArgb(139, 0, 26);
            _lblError.Location = new Point(40, 412);
            _lblError.Name = "_lblError";
            _lblError.Size = new Size(300, 20);
            _lblError.TabIndex = 11;
            _lblError.TextAlign = ContentAlignment.MiddleCenter;
            _lblError.Visible = false;
            // 
            // _btnLogin
            // 
            _btnLogin.FlatAppearance.BorderColor = Color.FromArgb(139, 0, 26);
            _btnLogin.FlatAppearance.BorderSize = 2;
            _btnLogin.FlatStyle = FlatStyle.Flat;
            _btnLogin.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            _btnLogin.Location = new Point(40, 356);
            _btnLogin.Name = "_btnLogin";
            _btnLogin.Size = new Size(300, 42);
            _btnLogin.TabIndex = 10;
            _btnLogin.Text = "🔑   Login";
            _btnLogin.Click += BtnLogin_Click;
            // 
            // _chkShow
            // 
            _chkShow.AutoSize = true;
            _chkShow.Font = new Font("Segoe UI", 8.5F);
            _chkShow.ForeColor = Color.FromArgb(110, 110, 110);
            _chkShow.Location = new Point(40, 322);
            _chkShow.Name = "_chkShow";
            _chkShow.Size = new Size(108, 19);
            _chkShow.TabIndex = 9;
            _chkShow.Text = "Show password";
            _chkShow.CheckedChanged += ChkShow_CheckedChanged;
            // 
            // _txtPassword
            // 
            _txtPassword.BorderStyle = BorderStyle.FixedSingle;
            _txtPassword.Font = new Font("Segoe UI", 10F);
            _txtPassword.Location = new Point(40, 286);
            _txtPassword.MaxLength = 64;
            _txtPassword.Name = "_txtPassword";
            _txtPassword.PasswordChar = '●';
            _txtPassword.PlaceholderText = "  Enter password";
            _txtPassword.Size = new Size(300, 25);
            _txtPassword.TabIndex = 8;
            // 
            // _lblPasswordCaption
            // 
            _lblPasswordCaption.AutoSize = true;
            _lblPasswordCaption.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            _lblPasswordCaption.ForeColor = Color.FromArgb(139, 0, 26);
            _lblPasswordCaption.Location = new Point(40, 266);
            _lblPasswordCaption.Name = "_lblPasswordCaption";
            _lblPasswordCaption.Size = new Size(57, 15);
            _lblPasswordCaption.TabIndex = 7;
            _lblPasswordCaption.Text = "Password";
            // 
            // _txtUsername
            // 
            _txtUsername.BorderStyle = BorderStyle.FixedSingle;
            _txtUsername.Font = new Font("Segoe UI", 10F);
            _txtUsername.Location = new Point(40, 220);
            _txtUsername.MaxLength = 64;
            _txtUsername.Name = "_txtUsername";
            _txtUsername.PlaceholderText = "  Enter username";
            _txtUsername.Size = new Size(300, 25);
            _txtUsername.TabIndex = 6;
            // 
            // _lblUsernameCaption
            // 
            _lblUsernameCaption.AutoSize = true;
            _lblUsernameCaption.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            _lblUsernameCaption.ForeColor = Color.FromArgb(139, 0, 26);
            _lblUsernameCaption.Location = new Point(40, 200);
            _lblUsernameCaption.Name = "_lblUsernameCaption";
            _lblUsernameCaption.Size = new Size(60, 15);
            _lblUsernameCaption.TabIndex = 5;
            _lblUsernameCaption.Text = "Username";
            // 
            // _divider
            // 
            _divider.BackColor = Color.FromArgb(220, 220, 220);
            _divider.Location = new Point(30, 180);
            _divider.Name = "_divider";
            _divider.Size = new Size(320, 2);
            _divider.TabIndex = 4;
            // 
            // _lblSubtitle
            // 
            _lblSubtitle.Font = new Font("Segoe UI", 8.5F);
            _lblSubtitle.ForeColor = Color.FromArgb(140, 140, 140);
            _lblSubtitle.Location = new Point(0, 150);
            _lblSubtitle.Name = "_lblSubtitle";
            _lblSubtitle.Size = new Size(380, 20);
            _lblSubtitle.TabIndex = 3;
            _lblSubtitle.Text = "IT 332  ·  AY 2025–2026";
            _lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lblTitleSub
            // 
            _lblTitleSub.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            _lblTitleSub.ForeColor = Color.FromArgb(139, 0, 26);
            _lblTitleSub.Location = new Point(0, 122);
            _lblTitleSub.Name = "_lblTitleSub";
            _lblTitleSub.Size = new Size(380, 26);
            _lblTitleSub.TabIndex = 2;
            _lblTitleSub.Text = "Integrated University Information System";
            _lblTitleSub.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lblTitleMain
            // 
            _lblTitleMain.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            _lblTitleMain.ForeColor = Color.FromArgb(139, 0, 26);
            _lblTitleMain.Location = new Point(0, 96);
            _lblTitleMain.Name = "_lblTitleMain";
            _lblTitleMain.Size = new Size(380, 26);
            _lblTitleMain.TabIndex = 1;
            _lblTitleMain.Text = "Batangas State University";
            _lblTitleMain.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _picLogo
            // 
            _picLogo.Image = (Image)resources.GetObject("_picLogo.Image");
            _picLogo.Location = new Point(150, 14);
            _picLogo.Name = "_picLogo";
            _picLogo.Size = new Size(80, 64);
            _picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            _picLogo.TabIndex = 0;
            _picLogo.TabStop = false;
            // 
            // LoginForm
            // 
            BackColor = Color.FromArgb(139, 0, 26);
            ClientSize = new Size(464, 521);
            Controls.Add(_card);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MaximumSize = new Size(480, 560);
            MinimizeBox = false;
            MinimumSize = new Size(480, 560);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login - Integrated University Information System";
            _card.ResumeLayout(false);
            _card.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel _card;
        private PictureBox _picLogo;
        private Label _lblTitleMain;
        private Label _lblTitleSub;
        private Label _lblSubtitle;
        private Panel _divider;
        private Label _lblUsernameCaption;
        private TextBox _txtUsername;
        private Label _lblPasswordCaption;
        private TextBox _txtPassword;
        private CheckBox _chkShow;
        private Button _btnLogin;
        private Label _lblError;
        private Label _lblAttempts;
        private Label _lblFooter;
    }
}
