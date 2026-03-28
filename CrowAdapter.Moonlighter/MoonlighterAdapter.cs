using BepInEx;
using UnityEngine;

namespace CrowAdapter.Moonlighter
{
    [BepInPlugin("com.crownest.moonlighter", "CrowAdapter.Moonlighter", "1.0.0")]
    public class MoonlighterAdapter : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("[CrowNest] Awake called!");
            CrowModLoader.Register(new ExampleMod());
            Logger.LogInfo("[CrowNest] Register called!");
        }

        private void Update()
        {
            CrowModLoader.UpdateAll();
        }
    }
}