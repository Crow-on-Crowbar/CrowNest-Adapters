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
            CrowModLoader.Register(new ExampleMod());
        }

        private void Update()
        {
            CrowModLoader.UpdateAll();
        }
    }
}