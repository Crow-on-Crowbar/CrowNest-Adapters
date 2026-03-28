using UnityEngine;

public class ExampleMod : CrowMod
{
    public override string Name => "Example Mod";
    public override string Version => "1.0.0";

    public override void OnLoad()
    {
        Debug.Log("[CrowNest] Example Mod loaded!");

    }

    public override void OnUnload()
    {
        // Code to run when the mod is unloaded
        Debug.Log("[CrowNest] Example Mod unloaded!");
    }

    public override void OnUpdate()
    {
        // Code to run when the mod is loaded
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (HeroMerchant.Instance == null) return;
            HeroMerchant.Instance.addGold(-HeroMerchant.Instance.GetCurrentGold());
            HeroMerchant.Instance.addGold(99999);
        }
    }
}