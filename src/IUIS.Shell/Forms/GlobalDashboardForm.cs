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
    public partial class GlobalDashboardForm : Form
    {
        /// <summary>Null only when the Visual Studio Designer hosts this form.</summary>
        private readonly UserSession?    _session;
        private readonly ModuleRegistry? _registry;

        /// <summary>
        /// True when the user pressed Log Out rather than closing the window.
        /// The login form reads this to decide between re-showing itself and exiting.
        /// </summary>
        public bool LogoutRequested { get; private set; }

        /// <summary>Parameterless constructor so the Visual Studio Designer can host this form.</summary>
        public GlobalDashboardForm() : this(null, null)
        {
        }

        public GlobalDashboardForm(UserSession? session, ModuleRegistry? registry)
        {
            _session  = session;
            _registry = registry;

            InitializeComponent();

            if (_session != null)
                _lblWelcome.Text = $"Welcome, {_session.Username} ({_session.Role})";

            RenderCards();

            _clockTimer.Tick += (s, e) => _lblClock.Text = FormatNow();
            _clockTimer.Start();

            _cardsPanel.ClientSizeChanged += (s, e) => CenterCards();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _clockTimer.Stop();
            base.OnFormClosed(e);
        }

        private static string FormatNow() =>
            DateTime.Now.ToString("dddd, MMMM dd, yyyy  •  hh:mm:ss tt");

        // ── Cards ─────────────────────────────────────────────────────────

        /// <summary>Rebuilds every card from the registry, re-checking availability.</summary>
        private void RenderCards()
        {
            if (_registry is null || _session is null)
                return;

            var registry = _registry;
            var session   = _session;

            _cardsPanel.SuspendLayout();

            foreach (Control existing in _cardsPanel.Controls)
                existing.Dispose();
            _cardsPanel.Controls.Clear();

            foreach (var module in registry.GetAccessible(session))
                _cardsPanel.Controls.Add(CreateModuleCard(module));

            _cardsPanel.ResumeLayout();
            CenterCards();
        }

        private void CenterCards()
        {
            if (_cardsPanel.Controls.Count == 0)
                return;

            int clientWidth = _cardsPanel.ClientSize.Width;
            int cardTotalWidth = 430; // 400 width + 15 margin * 2
            int totalCards = _cardsPanel.Controls.Count;

            int cols = clientWidth / cardTotalWidth;
            if (cols <= 0) cols = 1;
            if (cols > totalCards) cols = totalCards;

            int usedWidth = cols * cardTotalWidth;
            int remaining = clientWidth - usedWidth;

            int leftPadding = Math.Max(35, remaining / 2);

            if (_cardsPanel.Padding.Left != leftPadding)
            {
                _cardsPanel.Padding = new Padding(leftPadding, 35, 35, 35);
            }
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
                Text        = d.DisplayName,
                Font        = BsuTheme.Heading,
                AutoSize    = true,
                Location    = new Point(15, 75),
                ForeColor   = available ? BsuTheme.DarkRed : Color.FromArgb(150, 150, 150),
                BackColor   = Color.Transparent,
                UseMnemonic = false
            };

            var lblDesc = new Label
            {
                Text        = d.Description,
                Font        = new Font("Segoe UI", 10.5F),
                AutoSize    = false,
                Size        = new Size(360, 45),
                Location    = new Point(18, 110),
                ForeColor   = Color.DimGray,
                BackColor   = Color.Transparent,
                UseMnemonic = false
            };

            // Badge doubles as the team label and the "not built yet" warning,
            // so an unavailable module says who to chase.
            var lblBadge = new Label
            {
                Text        = available ? d.Team : $"{d.Team} · not built yet",
                Font        = new Font("Segoe UI", 8F, FontStyle.Bold),
                AutoSize    = true,
                Location    = new Point(300, 22),
                ForeColor   = available ? BsuTheme.MutedText : Color.FromArgb(190, 90, 90),
                BackColor   = Color.Transparent,
                UseMnemonic = false
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
            if (_session is null)
                return;

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

        // ── Header button handlers ───────────────────────────────────────

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            LogoutRequested = true;
            Close();
        }
    }
}
