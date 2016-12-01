using System;

namespace SecuredMail.DisposeImp
{
    public class NotifiesWhenDisposed : DisposableObject, INotifyWhenDisposed
    {
        public event EventHandler Disposed;
        public override void Dispose(bool disposing)
        {
            lock (this)
            {
                if (disposing && !IsDisposed)
                {
                    Disposed?.Invoke(this, EventArgs.Empty);
                    Disposed = null;
                }

                base.Dispose(disposing);
            }
        }
    }

}
