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
    public partial class LoginForm : Form
    {
        private const int MaxAttempts = 3;

        private readonly UserService     _userService = new();
        private readonly ModuleRegistry? _registry;

        private int _attempts;

        /// <summary>Parameterless constructor so the Visual Studio Designer can host this form.</summary>
        public LoginForm() : this(null)
        {
        }

        public LoginForm(ModuleRegistry? registry)
        {
            _registry = registry;

            InitializeComponent();

            // Centre the card now that ClientSize is final.
            _card.Location = new Point(
                (ClientSize.Width  - _card.Width)  / 2,
                (ClientSize.Height - _card.Height) / 2);

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

        private void ChkShow_CheckedChanged(object? sender, EventArgs e) =>
            _txtPassword.PasswordChar = _chkShow.Checked ? '\0' : '●';
    }
}
