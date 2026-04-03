namespace CrowNest.SDK
{
    public abstract class CrowAdapterBase
    {
        public abstract string Name { get; }
        public abstract string Version { get; }

        protected abstract void BindEvents();
    }
}