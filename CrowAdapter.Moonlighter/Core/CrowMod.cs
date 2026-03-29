public abstract class CrowMod
{
    public static BepInEx.Logging.ManualLogSource Logger;

    public abstract string Name { get; }
    public abstract string Version { get; }
    public abstract void OnLoad();
    public abstract void OnUnload();
    public abstract void OnUpdate();
}