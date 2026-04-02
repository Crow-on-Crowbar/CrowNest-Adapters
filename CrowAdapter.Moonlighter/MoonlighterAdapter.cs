using BepInEx;
using CrowAdapter.Moonlighter.Core;
using System;
using UnityEngine;

namespace CrowAdapter.Moonlighter
{
    [BepInPlugin("com.crownest.moonlighter", "CrowAdapter.Moonlighter", "1.0.0")]
    public class MoonlighterAdapter : BaseUnityPlugin
    {
        void Awake()
        {
            // 1. Initialize the Pipe Client first
            CrowPipeClient.Initialize();
            CrowPipeClient.Send(0x01, "[SYSTEM] CrowPipe Client Initialized.");

            // 2. Initialize Moonlighter Internal API
            MoonlighterAPI.Initialize();
            CrowPipeClient.Send(0x01, "[SYSTEM] MoonlighterAPI Hooking Complete.");

            // 3. Bind API Events to Pipe Signals (Lambda expressions)
            BindEventsToPipe();
        }

        void Update()
        {
            if (!CrowPipeClient.IsConnected)
            {
                CrowPipeClient.Initialize();
            }
        }

        private void BindEventsToPipe()
        {
            // Player Events
            MoonlighterAPI.OnPlayerDamaged += (dmg) =>
                CrowPipeClient.Send(0x02, $"[EVENT] Player Damaged: {dmg}");

            MoonlighterAPI.OnPlayerDied += () =>
                CrowPipeClient.Send(0x02, "[EVENT] Player Died.");

            // Shop Events
            MoonlighterAPI.OnShopOpened += () =>
                CrowPipeClient.Send(0x02, "[EVENT] Shop Opened.");

            MoonlighterAPI.OnShopClosed += () =>
                CrowPipeClient.Send(0x02, "[EVENT] Shop Closed.");

            // Dungeon/World Events
            MoonlighterAPI.OnRoomEntered += (room) =>
                CrowPipeClient.Send(0x02, $"[EVENT] Entered Room: {room.name}");

            MoonlighterAPI.OnDayTimeChanged += (time) =>
                CrowPipeClient.Send(0x01, $"[WORLD] Time Changed: {time}");
        }
    }
}