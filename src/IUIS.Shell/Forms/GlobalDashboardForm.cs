using IUIS.Core.Modules;
using IUIS.Core.Session;
using IUIS.Core.Theming;

namespace IUIS.Shell.Forms
{
    /// <summary>
    /// The main navigation surface. Renders one card per registered module and
    /// launches whichever the user picks.
    ///
    /// This form knows nothing about any specific team — it walks the
    /// <see cref="ModuleRegistry"/> and talks only to <see cref="IModule"/>.
    /// Adding a ninth module requires no change here.
    /// </summary>
    public sealed class GlobalDashboardForm : Form
    {
        private readonly UserSession    _session;
        private readonly ModuleRegistry _registry;

        private readonly System.Windows.Forms.Timer _clockTimer = new() { Interval = 1000 };
        private readonly Label           _lblClock  = new();
        private readonly FlowLayoutPanel _cardsPanel = new();

        /// <summary>
        /// True when the user pressed Log Out rather than closing the window.
        /// The login form reads this to decide between re-showing itself and exiting.
        /// </summary>
        public bool LogoutRequested { get; private set; }

        public GlobalDashboardForm(UserSession session, ModuleRegistry registry)
        {
            _session  = session  ?? throw new ArgumentNullException(nameof(session));
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));

            Text          = "Batangas State University — IUIS Dashboard";
            Size          = new Size(1000, 700);
            MinimumSize   = new Size(720, 520);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor     = BsuTheme.PageBackground;

            BuildUI();
            RenderCards();

            _clockTimer.Tick += (s, e) => _lblClock.Text = FormatNow();
            _clockTimer.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _clockTimer.Stop();
            _clockTimer.Dispose();
            base.OnFormClosed(e);
        }

        private static string FormatNow() =>
            DateTime.Now.ToString("dddd, MMMM dd, yyyy  •  hh:mm:ss tt");

        // ── Layout ────────────────────────────────────────────────────────

        private void BuildUI()
        {
            var header = BuildHeader();

            _cardsPanel.Dock       = DockStyle.Fill;
            _cardsPanel.Padding    = new Padding(35);
            _cardsPanel.AutoScroll = true;

            // Fill is added before Top so it occupies the remaining space
            // correctly — WinForms docks in reverse z-order.
            Controls.Add(_cardsPanel);
            Controls.Add(header);
        }

        private Panel BuildHeader()
        {
            var header = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 100,
                BackColor = BsuTheme.DarkRed
            };

            var lblTitle = new Label
            {
                Text      = "Batangas State University — IUIS",
                Font      = BsuTheme.Title,
                ForeColor = Color.White,
                AutoSize  = true,
                Location  = new Point(25, 20)
            };

            _lblClock.Text      = FormatNow();
            _lblClock.Font      = BsuTheme.Body;
            _lblClock.ForeColor = Color.FromArgb(220, 220, 220);
            _lblClock.AutoSize  = true;
            _lblClock.Location  = new Point(30, 60);

            var lblUser = new Label
            {
                Text      = $"Welcome, {_session.Username} ({_session.Role})",
                Font      = new Font("Segoe UI", 12F),
                ForeColor = BsuTheme.Gold,
                AutoSize  = true,
                Margin    = new Padding(0, 6, 12, 0)
            };

            var btnLogout = new Button
            {
                Text   = "Log Out",
                Font   = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Size   = new Size(80, 30),
                Margin = new Padding(0)
            };
            BsuTheme.StyleAsOutlined(btnLogout);
            btnLogout.Click += (s, e) =>
            {
                LogoutRequested = true;
                Close();
            };

            var btnRefresh = new Button
            {
                Text   = "Refresh",
                Font   = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Size   = new Size(80, 30),
                Margin = new Padding(0, 0, 10, 0)
            };
            BsuTheme.StyleAsOutlined(btnRefresh);
            // During integration week a teammate may build their .exe while the
            // dashboard is already open; this re-checks availability without a restart.
            btnRefresh.Click += (s, e) => RenderCards();

            // Docking right (rather than setting Left by hand) keeps this pinned
            // correctly as the window resizes, with no Resize handler needed.
            var rightPanel = new FlowLayoutPanel
            {
                Dock          = DockStyle.Right,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents  = false,
                AutoSize      = true,
                AutoSizeMode  = AutoSizeMode.GrowAndShrink,
                Padding       = new Padding(0, 33, 25, 0),
                BackColor     = Color.Transparent
            };
            // In RightToLeft flow the first control added sits furthest right.
            rightPanel.Controls.Add(btnLogout);
            rightPanel.Controls.Add(btnRefresh);
            rightPanel.Controls.Add(lblUser);

            header.Controls.Add(rightPanel);
            header.Controls.Add(lblTitle);
            header.Controls.Add(_lblClock);

            return header;
        }

        // ── Cards ─────────────────────────────────────────────────────────

        /// <summary>Rebuilds every card from the registry, re-checking availability.</summary>
        private void RenderCards()
        {
            _cardsPanel.SuspendLayout();

            foreach (Control existing in _cardsPanel.Controls)
                existing.Dispose();
            _cardsPanel.Controls.Clear();

            foreach (var module in _registry.GetAccessible(_session))
                _cardsPanel.Controls.Add(CreateModuleCard(module));

            _cardsPanel.ResumeLayout();
        }

        private Button CreateModuleCard(IModule module)
        {
            var available = module.IsAvailable;
            var d         = module.Descriptor;

            var card = new Button
            {
                Size      = new Size(400, 160),
                Margin    = new Padding(15),
                BackColor = available ? BsuTheme.CardBackground : Color.FromArgb(238, 238, 238),
                FlatStyle = FlatStyle.Flat,
                Cursor    = available ? Cursors.Hand : Cursors.Default,
                TextAlign = ContentAlignment.TopLeft,
                Padding   = new Padding(10),
                Tag       = module
            };
            card.FlatAppearance.BorderColor = BsuTheme.Border;
            card.FlatAppearance.BorderSize  = 1;

            var lblIcon = new Label
            {
                Text      = d.Icon,
                Font      = new Font("Segoe UI", 32F),
                AutoSize  = true,
                Location  = new Point(15, 15),
                BackColor = Color.Transparent
            };

            var lblTitle = new Label
            {
                Text      = d.DisplayName,
                Font      = BsuTheme.Heading,
                AutoSize  = true,
                Location  = new Point(15, 75),
                ForeColor = available ? BsuTheme.DarkRed : Color.FromArgb(150, 150, 150),
                BackColor = Color.Transparent
            };

            var lblDesc = new Label
            {
                Text      = d.Description,
                Font      = new Font("Segoe UI", 10.5F),
                AutoSize  = false,
                Size      = new Size(360, 45),
                Location  = new Point(18, 110),
                ForeColor = Color.DimGray,
                BackColor = Color.Transparent
            };

            // Badge doubles as the team label and the "not built yet" warning,
            // so an unavailable module says who to chase.
            var lblBadge = new Label
            {
                Text      = available ? d.Team : $"{d.Team} · not built yet",
                Font      = new Font("Segoe UI", 8F, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(300, 22),
                ForeColor = available ? BsuTheme.MutedText : Color.FromArgb(190, 90, 90),
                BackColor = Color.Transparent
            };

            card.Controls.AddRange([lblIcon, lblTitle, lblDesc, lblBadge]);

            WireCardBehaviour(card, module, available);
            return card;
        }

        /// <summary>
        /// Hover and click have to be wired on the child labels too — a label
        /// swallows the mouse events for the region it covers, so without this
        /// the card goes dead wherever there is text.
        /// </summary>
        private void WireCardBehaviour(Button card, IModule module, bool available)
        {
            void SetHover(bool hovering)
            {
                if (!available) return;

                card.BackColor = hovering ? BsuTheme.HoverBackground : BsuTheme.CardBackground;
                card.FlatAppearance.BorderColor = hovering ? BsuTheme.Gold : BsuTheme.Border;
                card.FlatAppearance.BorderSize  = hovering ? 2 : 1;
            }

            void OnEnter(object? s, EventArgs e) => SetHover(true);
            void OnLeave(object? s, EventArgs e) => SetHover(false);
            void OnClick(object? s, EventArgs e) => LaunchModule(module);

            card.MouseEnter += OnEnter;
            card.MouseLeave += OnLeave;
            card.Click      += OnClick;

            foreach (Control child in card.Controls)
            {
                child.MouseEnter += OnEnter;
                child.MouseLeave += OnLeave;
                child.Click      += OnClick;
            }
        }

        // ── Launching ─────────────────────────────────────────────────────

        private void LaunchModule(IModule module)
        {
            try
            {
                UseWaitCursor = true;
                module.Launch(_session, this);
            }
            catch (ModuleLaunchException ex)
            {
                // Expected, human-readable failures: not built, missing file, etc.
                MessageBox.Show(this, ex.Message, $"{module.Descriptor.DisplayName}",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"An unexpected error occurred while opening {module.Descriptor.DisplayName}:\n\n{ex.Message}",
                    "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }
    }
}
