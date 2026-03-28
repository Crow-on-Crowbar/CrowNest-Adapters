using System;
using UnityEngine;

namespace CrowAdapter.Moonlighter
{
    public static class MoonlighterAPI
    {
        public static event Action OnPlayerDied;
        public static event Action<float> OnPlayerDamaged;
        public static event Action<int> OnFloorChanged;

        public static void Initialize()
        {
            HeroMerchant.OnDie += () => OnPlayerDied?.Invoke();
            HeroMerchant.OnDealedDamage += (dmg, enemy) => OnPlayerDamaged?.Invoke(dmg);
            HeroMerchant.OnFloorChange += (culture, floor) => OnFloorChanged?.Invoke(floor);

            Debug.Log("[CrowNest] MoonlighterAPI initialized.");
        }
    }
}