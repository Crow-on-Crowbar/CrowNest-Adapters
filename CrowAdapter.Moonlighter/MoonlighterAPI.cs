using System;

public static class MoonlighterAPI
{
    // 어댑터에서 구독할 이벤트들
    public static event Action OnPlayerDied;
    public static event Action<float> OnPlayerDamaged;
    public static event Action<int> OnFloorChanged;

    public static void Initialize()
    {
        // 1. 플레이어 사망 이벤트 구독
        HeroMerchant.OnDie += () =>
        {
            OnPlayerDied?.Invoke();
        };

        // 2. 데미지 입었을 때 (dmg: 데미지 양, enemy: 가해자 객체)
        HeroMerchant.OnDealedDamage += (dmg, enemy) =>
        {
            OnPlayerDamaged?.Invoke(dmg);
        };

        // 3. 층 변경 시 (culture: 문화권/테마, floor: 층 번호)
        HeroMerchant.OnFloorChange += (culture, floor) =>
        {
            OnFloorChanged?.Invoke(floor);
        };

        // 로깅 (선택 사항)
         Console.WriteLine("[CrowNest] MoonlighterAPI Events Subscribed.");
    }
}