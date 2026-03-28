using BepInEx;
using UnityEngine;

namespace CrowAdapter.Moonlighter
{
    [BepInPlugin("com.crownest.moonlighter", "CrowAdapter.Moonlighter", "1.0.0")]
    public class MoonlighterAdapter : BaseUnityPlugin
    {
        private void Awake()
        {
            MoonlighterAPI.Initialize();

            MoonlighterAPI.OnPlayerDied += () =>
                Logger.LogInfo("[CrowNest] Player died.");

            MoonlighterAPI.OnPlayerDamaged += (dmg) =>
                Logger.LogInfo($"[CrowNest] Player took {dmg} damage.");

            MoonlighterAPI.OnFloorChanged += (floor) =>
                Logger.LogInfo($"[CrowNest] Entered floor {floor}.");

            Logger.LogInfo("CrowAdapter.Moonlighter loaded!");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (HeroMerchant.Instance == null) return;
                HeroMerchant.Instance.addGold(-HeroMerchant.Instance.GetCurrentGold());
                HeroMerchant.Instance.addGold(99999);
                Logger.LogInfo("[CrowNest] Gold set to 99999.");
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (HeroMerchant.Instance == null) return;
                HeroMerchant.Instance.heroMerchantStats.Die();
                Logger.LogInfo("[CrowNest] Health set to 9999.");
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                if (HeroMerchant.Instance == null) return;
                int level = CrowReflector.GetField<int>(HeroMerchant.Instance, "_currentDungeonLevel");
                Logger.LogInfo($"[CrowNest] Current dungeon level: {level}");
                CrowReflector.SetField<int>(HeroMerchant.Instance, "_currentDungeonLevel", 5);
                Logger.LogInfo("[CrowNest] Dungeon level set to 5.");
            }
        }
    }
}