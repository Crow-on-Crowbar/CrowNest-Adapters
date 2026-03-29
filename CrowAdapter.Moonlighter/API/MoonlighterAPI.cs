using System;
using UnityEngine;

namespace CrowAdapter.Moonlighter
{
    public static class MoonlighterAPI
    {
        public static event Action OnPlayerDied;
        public static event Action<float> OnPlayerDamaged;
        public static event Action<int> OnFloorChanged;
        public static event Action OnShopOpened;
        public static event Action<DungeonRoom, DungeonRoom> OnRoomChanged;
        public static event Action<DungeonRoom> OnRoomEntered;
        public static event Action<DungeonRoom> OnRoomExited;
        public static event Action OnTownLoadStarted;
        public static event Action<bool> OnGamePaused;
        public static event Action OnTownLoaded;
        public static event Action<bool> OnDungeonLoaded;
        public static event Action OnShopClosed;
        public static event Action OnWillWokeUp;
        public static event Action<DayTime> OnDayTimeChanged;

        public static void Initialize()
        {
            HeroMerchant.OnDie += () => OnPlayerDied?.Invoke();
            HeroMerchant.OnDealedDamage += (dmg, enemy) => OnPlayerDamaged?.Invoke(dmg);
            HeroMerchant.OnFloorChange += (culture, floor) => OnFloorChanged?.Invoke(floor);
            ShopManager.OnShopOpen += () => OnShopOpened?.Invoke();
            DungeonManager.OnHeroChangingRoom += (from, to) => OnRoomChanged?.Invoke(from, to);
            DungeonManager.OnEnterRoom += (room) => OnRoomEntered?.Invoke(room);
            DungeonManager.OnExitRoom += (room) => OnRoomExited?.Invoke(room);
            GameManager.OnTownLoadStarted += () => OnTownLoadStarted?.Invoke();
            GameManager.OnGamePaused += (paused) => OnGamePaused?.Invoke(paused);
            GameManager.Instance.OnTownLoaded += () => OnTownLoaded?.Invoke();
            GameManager.Instance.OnDungeonLoaded += (tutorial) => OnDungeonLoaded?.Invoke(tutorial);
            ShopManager.OnShopClose += () => OnShopClosed?.Invoke();
            ShopManager.OnWillWakeAnimationEnded += () => OnWillWokeUp?.Invoke();
            TimeManagerV2.Instance.OnDayTimeChange += (time) => OnDayTimeChanged?.Invoke(time);

        }
    }
}