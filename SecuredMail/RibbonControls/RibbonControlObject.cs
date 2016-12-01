using System.Diagnostics;
using System.Threading.Tasks;

namespace SecuredMail.RibbonControls
{
    /// <summary>
    /// Base class for RibbonControls
    /// </summary>
    public abstract class RibbonControlObject: IRibbonControlObject
    {
        protected RibbonControlObject(string id, object context, string tag)
        {
            Id = id;
            Context = context;
            Tag = tag;
        }

        public string Id { get; }
        public object Context { get; }
        public string Tag { get; }

        public virtual bool GetEnabled()
        {
            return true;
        }

        public virtual Task<bool> OnActionCallback()
        {
            Debug.WriteLine($"You clicked Control Id:{this.Id}");

            return Task.Run(() => true);
        }

    }
}
