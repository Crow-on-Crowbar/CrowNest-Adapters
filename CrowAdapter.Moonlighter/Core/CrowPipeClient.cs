using System;
using System.IO.Pipes;
using System.Text;

namespace CrowAdapter.Moonlighter.Core
{
    public static class CrowPipeClient
    {
        private static NamedPipeClientStream pipeClient;
        private static readonly string pipeName = "CrowNest_Pipe";

        public static bool IsConnected => pipeClient != null && pipeClient.IsConnected;

        public static void Initialize()
        {
            if (IsConnected) return;

            try
            {
                // 1. 기존 리소스 정리
                pipeClient?.Dispose();

                // 2. 서버와 동일한 양방향 스트림 생성 (권한 일치)
                pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);

                // 3. 연결 시도 (타임아웃 1초)
                pipeClient.Connect(1000);
                UnityEngine.Debug.Log("[CrowNest] Successfully connected to Pipe!");
            }
            catch (Exception e)
            {
                // 클로드가 지적한 4번 해결: 에러 메시지 상세 출력
                UnityEngine.Debug.LogError($"[CrowNest] Pipe Connection Error: {e.Message}");
                pipeClient = null;
            }
        }

        public static void Send(byte type, string message)
        {
            if (pipeClient == null || !pipeClient.IsConnected) return;

            try
            {
                byte[] body = Encoding.UTF8.GetBytes(message);

                // Construct binary packet: [Type(1) | Length(4) | Body(N)]
                byte[] packet = new byte[1 + 4 + body.Length];

                // Set Packet Type
                packet[0] = type;

                // Set Payload Length (4-byte integer)
                byte[] lengthBytes = BitConverter.GetBytes(body.Length);
                Array.Copy(lengthBytes, 0, packet, 1, 4);

                // Set Payload Data
                Array.Copy(body, 0, packet, 5, body.Length);

                // Send the entire packet buffer to the server
                pipeClient.Write(packet, 0, packet.Length);
            }
            catch (Exception)
            {
                // Fail silently to prevent game crashes
            }
        }
    }
}