using BepInEx;
using CrowNest.SDK;
using CrowNest.SDK.Unity;
using System.Collections;
using UnityEngine;

namespace CrowAdapter.Moonlighter
{
    [BepInPlugin("com.crownest.moonlighter", "CrowAdapter.Moonlighter", "1.0.0")]
    public class MoonlighterAdapter : UnityAdapterBase
    {
        protected override IEnumerator WaitForSceneLoad()
        {
            while (ShopManager.Instance == null)
                yield return null;
        }

        protected override void InitializeAPI()
        {
            MoonlighterAPI.Initialize();

            CrowPipeClient.OnPacketReceived += (type, data) =>
            {
                if (type == 0x10)
                {
                    if (int.TryParse(data, out int amount))
                    {
                        Logger.LogInfo($"[CrowNest] SET_GOLD: {amount}");
                        HeroMerchant.Instance?.addGold(
                            -HeroMerchant.Instance.GetCurrentGold() + amount
                        );
                    }
                }
            };
        }

        protected override void BindEvents()
        {
            MoonlighterAPI.OnPlayerDamaged += (dmg) =>
                CrowPipeClient.Send(0x02, $"[EVENT] Player Damaged: {dmg}");

            MoonlighterAPI.OnPlayerDied += () =>
                CrowPipeClient.Send(0x02, "[EVENT] Player Died.");

            MoonlighterAPI.OnShopOpened += () =>
                CrowPipeClient.Send(0x02, "[EVENT] Shop Opened.");

            MoonlighterAPI.OnShopClosed += () =>
                CrowPipeClient.Send(0x02, "[EVENT] Shop Closed.");

            MoonlighterAPI.OnRoomEntered += (room) =>
                CrowPipeClient.Send(0x02, $"[EVENT] Entered Room: {room.name}");

            MoonlighterAPI.OnDayTimeChanged += (time) =>
                CrowPipeClient.Send(0x01, $"[WORLD] Time Changed: {time}");
        }
    }
}