using UnityEngine;
using System.Collections.Generic;
using BepInEx;

public static class CrowModLoader
{
    private static List<CrowMod> loadedMods = new List<CrowMod>();

    public static BepInEx.Logging.ManualLogSource Logger;

    public static void Register(CrowMod mod)
    {
        loadedMods.Add(mod);
        mod.OnLoad();
        Logger?.LogInfo($"[CrowNest] Loaded mod: {mod.Name} v{mod.Version}");
    }

    public static void UnregisterAll()
    {
        foreach (var mod in loadedMods)
        {
            mod.OnUnload();
            Debug.Log($"[CrowNest] Unloaded mod: {mod.Name} v{mod.Version}");
        }
        loadedMods.Clear();
    }

    public static void UpdateAll()
    {
        foreach (var mod in loadedMods)
        {
            mod.OnUpdate();
        }
    }
}