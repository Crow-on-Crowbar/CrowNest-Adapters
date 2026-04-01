using BepInEx;
using UnityEngine;

namespace CrowAdapter.Moonlighter
{
    [BepInPlugin("com.crownest.moonlighter", "CrowAdapter.Moonlighter", "1.0.0")]
    public class MoonlighterAdapter : BaseUnityPlugin
    {
        private void Awake()
        {
            CrowMod.Logger = Logger;
            MoonlighterAPI.Initialize();
            CrowModLoader.Register(new HardcoreMod());
        }
        private void Update()
        {
            CrowModLoader.UpdateAll();
        }
    }
}