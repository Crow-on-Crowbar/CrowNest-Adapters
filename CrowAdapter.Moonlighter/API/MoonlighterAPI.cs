using System;

namespace CrowAdapter.Moonlighter
{
    public static class MoonlighterAPI
    {
        // Player
        public static event Action OnPlayerDied;
        public static event Action<float> OnPlayerDamaged;
        public static event Action<int> OnFloorChanged;

        // Shop
        public static event Action OnShopOpened;
        public static event Action OnShopClosed;
        public static event Action OnWillWokeUp;

        // Dungeon
        public static event Action<DungeonRoom, DungeonRoom> OnRoomChanged;
        public static event Action<DungeonRoom> OnRoomEntered;
        public static event Action<DungeonRoom> OnRoomExited;

        // World
        public static event Action OnTownLoadStarted;
        public static event Action OnTownLoaded;
        public static event Action<bool> OnDungeonLoaded;
        public static event Action<bool> OnGamePaused;
        public static event Action<DayTime> OnDayTimeChanged;

        public static void Initialize()
        {
            HeroMerchant.OnDie += () => OnPlayerDied?.Invoke();
            HeroMerchant.OnDealedDamage += (dmg, enemy) => OnPlayerDamaged?.Invoke(dmg);
            HeroMerchant.OnFloorChange += (culture, floor) => OnFloorChanged?.Invoke(floor);
            ShopManager.OnShopOpen += () => OnShopOpened?.Invoke();
            ShopManager.OnShopClose += () => OnShopClosed?.Invoke();
            ShopManager.OnWillWakeAnimationEnded += () => OnWillWokeUp?.Invoke();
            DungeonManager.OnHeroChangingRoom += (from, to) => OnRoomChanged?.Invoke(from, to);
            DungeonManager.OnEnterRoom += (room) => OnRoomEntered?.Invoke(room);
            DungeonManager.OnExitRoom += (room) => OnRoomExited?.Invoke(room);
            GameManager.OnTownLoadStarted += () => OnTownLoadStarted?.Invoke();
            GameManager.OnGamePaused += (paused) => OnGamePaused?.Invoke(paused);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnTownLoaded += () => OnTownLoaded?.Invoke();
                GameManager.Instance.OnDungeonLoaded += (tutorial) => OnDungeonLoaded?.Invoke(tutorial);
            }

            if (TimeManagerV2.Instance != null)
                TimeManagerV2.Instance.OnDayTimeChange += (time) => OnDayTimeChanged?.Invoke(time);
        }
    }
}