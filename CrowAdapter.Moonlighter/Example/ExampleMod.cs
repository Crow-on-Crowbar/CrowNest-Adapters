using CrowAdapter.Moonlighter;
using UnityEngine;

public class ExampleMod : CrowMod
{
    public override string Name => "Example Mod";
    public override string Version => "1.0.0";

    public override void OnLoad()
    {
        Logger?.LogInfo("[CrowNest] Example Mod loaded!");
        MoonlighterAPI.OnPlayerDamaged += (dmg) =>
            Logger?.LogInfo($"[CrowNest] Player took {dmg} damage!");
        MoonlighterAPI.OnFloorChanged += (floor) =>
            Logger?.LogInfo($"[CrowNest] Entered floor {floor}!");
    }

    public override void OnUnload()
    {
        Logger?.LogInfo("[CrowNest] Example Mod unloaded!");
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (HeroMerchant.Instance == null) return;
            HeroMerchant.Instance.addGold(-HeroMerchant.Instance.GetCurrentGold());
            HeroMerchant.Instance.addGold(99999);
        }
    }
}