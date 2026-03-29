using System.Collections.Generic;

public static class CrowModLoader
{
    private static List<CrowMod> loadedMods = new List<CrowMod>();

    public static void Register(CrowMod mod)
    {
        loadedMods.Add(mod);
        mod.OnLoad();
        CrowMod.Logger?.LogInfo($"[CrowNest] Loaded mod: {mod.Name} v{mod.Version}");
    }

    public static void UnregisterAll()
    {
        foreach (var mod in loadedMods)
        {
            mod.OnUnload();
            CrowMod.Logger?.LogInfo($"[CrowNest] Unloaded mod: {mod.Name} v{mod.Version}");
        }
        loadedMods.Clear();
    }

    public static void UpdateAll()
    {
        foreach (var mod in loadedMods)
            mod.OnUpdate();
    }
}