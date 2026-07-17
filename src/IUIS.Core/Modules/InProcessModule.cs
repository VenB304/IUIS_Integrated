using IUIS.Core.Session;

namespace IUIS.Core.Modules
{
    /// <summary>
    /// Base class for a module that lives in the same process as the shell.
    ///
    /// This is what each team should aim for: reference IUIS.Core, build a class
    /// library, subclass this, and return your main form from
    /// <see cref="CreateMainForm"/>. Everything else — window handling, error
    /// reporting, single-instance behaviour — is inherited.
    /// </summary>
    public abstract class InProcessModule : IModule
    {
        /// <summary>The already-open window, if any. Lets a second click re-focus rather than duplicate.</summary>
        private Form? _openForm;

        /// <inheritdoc/>
        public abstract ModuleDescriptor Descriptor { get; }

        /// <summary>Compiled into the shell, so it is always available.</summary>
        public virtual bool IsAvailable => true;

        /// <inheritdoc/>
        /// <remarks>Open to everyone by default; override to restrict by role.</remarks>
        public virtual bool CanAccess(UserSession session) => true;

        /// <summary>
        /// Builds this module's main window. Called once per launch; the shell
        /// disposes the form when the user closes it.
        /// </summary>
        protected abstract Form CreateMainForm(UserSession session);

        /// <inheritdoc/>
        public void Launch(UserSession session, IWin32Window owner)
        {
            ArgumentNullException.ThrowIfNull(session);

            // Already open — bring it forward instead of opening a second copy.
            if (_openForm is { IsDisposed: false })
            {
                if (_openForm.WindowState == FormWindowState.Minimized)
                    _openForm.WindowState = FormWindowState.Normal;

                _openForm.BringToFront();
                _openForm.Activate();
                return;
            }

            Form form;
            try
            {
                form = CreateMainForm(session);
            }
            catch (Exception ex)
            {
                throw new ModuleLaunchException(
                    $"The {Descriptor.DisplayName} module failed to start: {ex.Message}", ex);
            }

            _openForm = form;
            form.FormClosed += (_, _) => _openForm = null;

            // Shown non-modally so the user can keep several modules open at once
            // and watch data sync between them — the whole point of the demo.
            form.Show(owner);
        }
    }
}
