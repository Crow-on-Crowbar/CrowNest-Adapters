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
            if (Input.GetKeyUp(KeyCode.F1))
            {

            }
        }
    }
}