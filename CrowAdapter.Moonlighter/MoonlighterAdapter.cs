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
            CrowModLoader.Register(new ExampleMod());

            MoonlighterAPI.OnDayTimeChanged += (time) =>
                Logger.LogInfo($"[CrowNest] Day time: {time}");
            MoonlighterAPI.OnShopOpened += () =>
                Logger.LogInfo("[CrowNest] Shop opened!");
            MoonlighterAPI.OnRoomEntered += (room) =>
                Logger.LogInfo($"[CrowNest] Entered room: {room.name}");
        }
        private void Update()
        {
            CrowModLoader.UpdateAll();
        }
    }
}