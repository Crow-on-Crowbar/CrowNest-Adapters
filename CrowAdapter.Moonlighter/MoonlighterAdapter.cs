using BepInEx;
using UnityEngine;

namespace CrowAdapter.Moonlighter
{
    [BepInPlugin("com.crownest.moonlighter", "CrowAdapter.Moonlighter", "1.0.0")]
    public class MoonlighterAdapter : BaseUnityPlugin
    {
        private void Awake()
        {
            // 1. MoonlighterAPI 초기화 (이벤트 구독 시작)
            MoonlighterAPI.Initialize();

            // 2. API 이벤트 활용 예시 (디버그 로그)
            MoonlighterAPI.OnPlayerDamaged += (dmg) =>
            {
                Logger.LogInfo($"[CrowNest] Player took {dmg} damage. Stay safe!");
            };

            MoonlighterAPI.OnFloorChanged += (floor) =>
            {
                Logger.LogInfo($"[CrowNest] Welcome to Floor {floor}.");
            };

            Logger.LogInfo("CrowAdapter.Moonlighter loaded with API system!");
        }

        private void Update()
        {
            // F1: 골드 치트 (기존 로직 유지)
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (HeroMerchant.Instance == null) return;

                // 현재 골드를 0으로 만들고 99,999 추가
                HeroMerchant.Instance.addGold(-HeroMerchant.Instance.GetCurrentGold());
                HeroMerchant.Instance.addGold(99999);

                Logger.LogInfo("Gold set to dddddd99999!");
            }

            // 예시: F2 누르면 즉시 사망 테스트 (이벤트 동작 확인용)
            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (HeroMerchant.Instance != null)
                {
                    Logger.LogInfo("Force killing player for testing...");
                    // HeroMerchant 내부의 체력 감소 함수 등을 호출해볼 수 있습니다.
                }
            }
        }
    }
}