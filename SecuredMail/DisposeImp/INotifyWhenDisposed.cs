using System;

namespace SecuredMail.DisposeImp
{
    public interface INotifyWhenDisposed : IDisposableObject
    {
        /// <summary>
        /// Occurs when the object is disposed.
        /// </summary>
        event EventHandler Disposed;
    }
}