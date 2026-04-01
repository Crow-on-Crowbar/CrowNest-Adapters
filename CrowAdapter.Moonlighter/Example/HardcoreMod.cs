using CrowAdapter.Moonlighter;
using System;
using UnityEngine;

public class HardcoreMod : CrowMod
{
    public override string Name => "Hardcore Gold Loss Mod";
    public override string Version => "1.0.0";

    public override void OnLoad()
    {
        MoonlighterAPI.OnPlayerDamaged += HandlePlayerDamaged;
    }

    public override void OnUnload()
    {
        MoonlighterAPI.OnPlayerDamaged -= HandlePlayerDamaged;
    }

    private void HandlePlayerDamaged(float damage)
    {
        int currentGold = HeroMerchant.Instance.GetCurrentGold();

        int lossAmount = (int)(currentGold * 0.1f);

        if (lossAmount > 0)
        {
            HeroMerchant.Instance.addGold(-lossAmount);
        }
    }

    public override void OnUpdate()
    {
    }
}