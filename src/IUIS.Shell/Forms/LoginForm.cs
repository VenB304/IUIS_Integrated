using IUIS.Core.Modules;
using IUIS.Core.Services;
using IUIS.Core.Session;
using IUIS.Core.Theming;

namespace IUIS.Shell.Forms
{
    /// <summary>
    /// The gateway to the whole system. Authenticates against the shared "users"
    /// node, then hands a <see cref="UserSession"/> to the dashboard — so no
    /// module ever asks for credentials a second time.
    /// </summary>
    public sealed class LoginForm : Form
    {
        private const int MaxAttempts = 3;

        private readonly UserService    _userService = new();
        private readonly ModuleRegistry _registry;

        // ── Controls ──────────────────────────────────────────────────────
        private readonly TextBox  _txtUsername = new();
        private readonly TextBox  _txtPassword = new();
        private readonly CheckBox _chkShow     = new();
        private readonly Button   _btnLogin    = new();
        private readonly Label    _lblError    = new();
        private readonly Label    _lblAttempts = new();

        private int _attempts;

        public LoginForm(ModuleRegistry registry)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));

            Text            = "Login — Integrated University Information System";
            Size            = new Size(480, 560);
            MinimumSize     = new Size(480, 560);
            MaximumSize     = new Size(480, 560);
            StartPosition   = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox     = false;
            BackColor       = BsuTheme.DarkRed;

            BuildUI();
        }

        private void BuildUI()
        {
            // ── White card ────────────────────────────────────────────────
            var card = new Panel
            {
                Size        = new Size(380, 440),
                BackColor   = BsuTheme.CardBackground,
                BorderStyle = BorderStyle.None
            };
            card.Location = new Point(
                (ClientSize.Width  - card.Width)  / 2,
                (ClientSize.Height - card.Height) / 2);

            // ── Branding ──────────────────────────────────────────────────
            var lblIcon = new Label
            {
                Text      = "🎓",
                Font      = new Font("Segoe UI", 32F),
                AutoSize  = false,
                Size      = new Size(card.Width, 56),
                Location  = new Point(0, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblTitle = new Label
            {
                Text      = "Batangas State University",
                Font      = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                ForeColor = BsuTheme.DarkRed,
                AutoSize  = false,
                Size      = new Size(card.Width, 26),
                Location  = new Point(0, 82),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblTitle2 = new Label
            {
                Text      = "Integrated University Information System",
                Font      = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = BsuTheme.DarkRed,
                AutoSize  = false,
                Size      = new Size(card.Width, 26),
                Location  = new Point(0, 108),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblSub = new Label
            {
                Text      = "IT 332  ·  AY 2025–2026",
                Font      = BsuTheme.Small,
                ForeColor = BsuTheme.MutedText,
                AutoSize  = false,
                Size      = new Size(card.Width, 20),
                Location  = new Point(0, 136),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var divider = new Panel
            {
                Size      = new Size(card.Width - 60, 2),
                Location  = new Point(30, 166),
                BackColor = BsuTheme.Gold
            };

            // ── Username ──────────────────────────────────────────────────
            var lblUser = new Label
            {
                Text      = "Username",
                Font      = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = BsuTheme.DarkRed,
                AutoSize  = true,
                Location  = new Point(40, 186)
            };

            _txtUsername.Location        = new Point(40, 206);
            _txtUsername.Size            = new Size(card.Width - 80, 30);
            _txtUsername.Font            = BsuTheme.Body;
            _txtUsername.BorderStyle     = BorderStyle.FixedSingle;
            _txtUsername.PlaceholderText = "Enter username";
            _txtUsername.MaxLength       = 64;

            // ── Password ──────────────────────────────────────────────────
            var lblPass = new Label
            {
                Text      = "Password",
                Font      = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = BsuTheme.DarkRed,
                AutoSize  = true,
                Location  = new Point(40, 252)
            };

            _txtPassword.Location        = new Point(40, 272);
            _txtPassword.Size            = new Size(card.Width - 80, 30);
            _txtPassword.Font            = BsuTheme.Body;
            _txtPassword.BorderStyle     = BorderStyle.FixedSingle;
            _txtPassword.PasswordChar    = '●';
            _txtPassword.PlaceholderText = "Enter password";
            _txtPassword.MaxLength       = 64;

            _chkShow.Text      = "Show password";
            _chkShow.Location  = new Point(40, 308);
            _chkShow.AutoSize  = true;
            _chkShow.Font      = BsuTheme.Small;
            _chkShow.ForeColor = Color.FromArgb(110, 110, 110);
            _chkShow.CheckedChanged += (s, e) =>
                _txtPassword.PasswordChar = _chkShow.Checked ? '\0' : '●';

            // ── Login button ──────────────────────────────────────────────
            _btnLogin.Text     = "🔑   Login";
            _btnLogin.Location = new Point(40, 342);
            _btnLogin.Size     = new Size(card.Width - 80, 42);
            _btnLogin.Font     = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            BsuTheme.StyleAsPrimary(_btnLogin);
            _btnLogin.Click += BtnLogin_Click;

            // ── Feedback ──────────────────────────────────────────────────
            _lblError.AutoSize  = false;
            _lblError.Size      = new Size(card.Width - 80, 20);
            _lblError.Location  = new Point(40, 398);
            _lblError.Font      = BsuTheme.Small;
            _lblError.ForeColor = BsuTheme.DarkRed;
            _lblError.TextAlign = ContentAlignment.MiddleCenter;
            _lblError.Visible   = false;

            _lblAttempts.AutoSize  = false;
            _lblAttempts.Size      = new Size(card.Width - 80, 16);
            _lblAttempts.Location  = new Point(40, 420);
            _lblAttempts.Font      = new Font("Segoe UI", 7.5F);
            _lblAttempts.ForeColor = Color.FromArgb(150, 150, 150);
            _lblAttempts.TextAlign = ContentAlignment.MiddleCenter;
            _lblAttempts.Visible   = false;

            var lblFooter = new Label
            {
                Text      = "Batangas State University  ·  CICS",
                Font      = new Font("Segoe UI", 7.5F),
                ForeColor = BsuTheme.SubtleText,
                AutoSize  = false,
                Size      = new Size(card.Width, 16),
                Location  = new Point(0, card.Height - 24),
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.AddRange(new Control[]
            {
                lblIcon, lblTitle, lblTitle2, lblSub,
                divider,
                lblUser, _txtUsername,
                lblPass, _txtPassword, _chkShow,
                _btnLogin,
                _lblError, _lblAttempts,
                lblFooter
            });

            Controls.Add(card);
            AcceptButton  = _btnLogin;
            ActiveControl = _txtUsername;
        }

        // ── Authentication ────────────────────────────────────────────────

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            var username = _txtUsername.Text.Trim();
            var password = _txtPassword.Text;

            // Validate locally before spending a round-trip on the network.
            if (string.IsNullOrWhiteSpace(username))
            {
                ShowError("⚠  Please enter your username.");
                _txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                ShowError("⚠  Please enter your password.");
                _txtPassword.Focus();
                return;
            }

            SetBusy(true, "⏳   Authenticating...");

            try
            {
                var user = await _userService.AuthenticateAsync(username, password);

                if (user is null)
                {
                    HandleFailedLogin();
                    return;
                }

                OpenDashboard(new UserSession(user));
            }
            catch (Exception ex)
            {
                ShowError("⚠  Connection error.");
                SetBusy(false, "🔑   Login");

                MessageBox.Show(
                    $"Could not reach the database:\n\n{ex.Message}",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OpenDashboard(UserSession session)
        {
            var dashboard = new GlobalDashboardForm(session, _registry);

            dashboard.FormClosed += (_, _) =>
            {
                if (dashboard.LogoutRequested)
                {
                    ResetForNextLogin();
                    Show();
                }
                else
                {
                    // Dashboard closed for real — close the login form too, which
                    // ends Application.Run and exits the process.
                    Close();
                }
            };

            Hide();
            dashboard.Show();
        }

        /// <summary>Clears state so a logged-out user gets a fresh form, not the last one's.</summary>
        private void ResetForNextLogin()
        {
            _attempts = 0;

            _txtUsername.Clear();
            _txtPassword.Clear();
            _chkShow.Checked = false;

            _lblError.Visible    = false;
            _lblAttempts.Visible = false;

            SetBusy(false, "🔑   Login");
            ActiveControl = _txtUsername;
        }

        private void HandleFailedLogin()
        {
            _attempts++;
            var remaining = MaxAttempts - _attempts;

            _txtPassword.Clear();
            _txtPassword.Focus();
            _lblAttempts.Visible = true;

            if (remaining <= 0)
            {
                ShowError("⛔  Too many failed attempts.");
                _lblAttempts.Text = "The application will now close.";
                _btnLogin.Enabled = false;

                var closeTimer = new System.Windows.Forms.Timer { Interval = 2500 };
                closeTimer.Tick += (s, _) =>
                {
                    closeTimer.Stop();
                    closeTimer.Dispose();
                    Close();
                };
                closeTimer.Start();
                return;
            }

            ShowError("⚠  Invalid username or password.");
            _lblAttempts.Text = $"{remaining} attempt{(remaining == 1 ? "" : "s")} remaining.";
            SetBusy(false, "🔑   Login");
        }

        private void ShowError(string message)
        {
            _lblError.Text    = message;
            _lblError.Visible = true;
        }

        private void SetBusy(bool busy, string buttonText)
        {
            _btnLogin.Enabled = !busy;
            _btnLogin.Text    = buttonText;
        }
    }
}
