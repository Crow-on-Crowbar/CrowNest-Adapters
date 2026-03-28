public abstract class CrowMod
{
    public abstract void OnLoad();
    public abstract void OnUnload();
    public abstract void OnUpdate();

    public abstract string Name { get; }
    public abstract string Version { get; }
}