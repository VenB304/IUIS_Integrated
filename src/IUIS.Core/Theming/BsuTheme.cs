namespace IUIS.Core.Theming
{
    /// <summary>
    /// The shared Batangas State University palette and fonts.
    ///
    /// Every module reads its colours from here, which is what keeps eight
    /// separately-written modules looking like one application. Hard-coding
    /// <c>Color.FromArgb(139, 0, 26)</c> in a form defeats the purpose.
    /// </summary>
    public static class BsuTheme
    {
        // ── Brand colours ─────────────────────────────────────────────────

        /// <summary>Primary brand red. Headers, titles, emphasis.</summary>
        public static readonly Color DarkRed = Color.FromArgb(139, 0, 26);

        /// <summary>Accent gold. Call-to-action buttons, dividers, highlights.</summary>
        public static readonly Color Gold = Color.FromArgb(255, 210, 44);

        // ── Neutrals ──────────────────────────────────────────────────────

        public static readonly Color PageBackground = Color.FromArgb(245, 245, 245);
        public static readonly Color CardBackground = Color.White;
        public static readonly Color HoverBackground = Color.FromArgb(250, 250, 250);
        public static readonly Color Border         = Color.FromArgb(220, 220, 220);
        public static readonly Color MutedText      = Color.FromArgb(140, 140, 140);
        public static readonly Color SubtleText     = Color.FromArgb(190, 190, 190);

        // ── Fonts ─────────────────────────────────────────────────────────

        public static Font Title      => new("Segoe UI Semibold", 22F, FontStyle.Bold);
        public static Font Heading    => new("Segoe UI Semibold", 15F, FontStyle.Bold);
        public static Font Subheading => new("Segoe UI Semibold", 11F, FontStyle.Bold);
        public static Font Body       => new("Segoe UI", 10F);
        public static Font Small      => new("Segoe UI", 8.5F);

        // ── Helpers ───────────────────────────────────────────────────────

        /// <summary>Applies the standard gold call-to-action styling to a button.</summary>
        public static void StyleAsPrimary(Button button)
        {
            ArgumentNullException.ThrowIfNull(button);

            button.BackColor = Gold;
            button.ForeColor = Color.Black;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor   = false;
            button.Cursor = Cursors.Hand;
        }

        /// <summary>Applies the standard outlined-on-red styling to a button.</summary>
        public static void StyleAsOutlined(Button button)
        {
            ArgumentNullException.ThrowIfNull(button);

            button.BackColor = Color.Transparent;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.White;
            button.Cursor = Cursors.Hand;
        }
    }
}
