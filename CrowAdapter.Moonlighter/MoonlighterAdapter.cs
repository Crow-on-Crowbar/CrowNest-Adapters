using BepInEx;
using UnityEngine;

namespace CrowAdapter.Moonlighter
{
    [BepInPlugin("com.crownest.moonlighter", "CrowAdapter.Moonlighter", "1.0.0")]
    public class MoonlighterAdapter : BaseUnityPlugin
    {

        private void Awake()
        {
            Logger.LogInfo("CrowAdapter.Moonlighter loaded!");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (HeroMerchant.Instance == null) return;
                HeroMerchant.Instance.addGold(-HeroMerchant.Instance.GetCurrentGold());
                HeroMerchant.Instance.addGold(99999);
                Logger.LogInfo("Added 99999 gold!");
            }
        }
    }
}