using BepInEx;
using CrowNest.SDK;
using System.Collections;
using System.Threading;
using System.Timers;
using UnityEngine;

namespace CrowNest.SDK.Unity
{
    public abstract class UnityAdapterBase : BaseUnityPlugin
    {
        private bool _isInitialized = false;
        private bool _isConnecting = false;
        private float _reconnectTimer = 5f;

        protected virtual void Awake()
        {
            CrowPipeClient.Log = (msg) => Logger.LogInfo(msg);
            CrowPipeClient.LogError = (msg) => Logger.LogError(msg);
            Logger.LogInfo($"[CrowNest] {GetType().Name} Awake!");

            new Thread(() => CrowPipeClient.Initialize()).Start();
        }

        protected virtual void Start()
        {
            StartCoroutine(LateInitialize());
        }

        private IEnumerator LateInitialize()
        {
            yield return WaitForSceneLoad();

            Logger.LogInfo("[CrowNest] Scene loaded, initializing...");
            InitializeAPI();
            BindEvents();
            _isInitialized = true;
            Logger.LogInfo("[CrowNest] All systems initialized!");
        }

        protected virtual void Update()
        {
            if (!_isInitialized) return;

            if (!CrowPipeClient.IsConnected && !_isConnecting)
            {
                _reconnectTimer += Time.deltaTime;
                if (_reconnectTimer >= 5f)
                {
                    _reconnectTimer = 0f;
                    _isConnecting = true;
                    new Thread(() => {
                        CrowPipeClient.Initialize();
                        _isConnecting = false;
                    }).Start();
                }
            }
        }

        protected abstract IEnumerator WaitForSceneLoad();
        protected abstract void InitializeAPI();
        protected abstract void BindEvents();
    }
}