using System;
using System.Collections.Concurrent;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace CrowNest.SDK
{
    public static class CrowPipeClient
    {
        private static NamedPipeClientStream _inPipe;  // C++ → C# (읽기)
        private static NamedPipeClientStream _outPipe; // C# → C++ (쓰기)
        private static readonly string inPipeName = "CrowNest_Pipe_In";
        private static readonly string outPipeName = "CrowNest_Pipe_Out";
        private static readonly ConcurrentQueue<(byte, string)> _sendQueue = new ConcurrentQueue<(byte, string)>();
        private static bool _isSendLoopRunning = false;

        public static Action<string> Log;
        public static Action<string> LogError;
        public static Action<byte, string> OnPacketReceived;

        public static bool IsConnected => _inPipe != null && _inPipe.IsConnected
                                       && _outPipe != null && _outPipe.IsConnected;

        public static void Initialize()
        {
            if (IsConnected) return;

            try
            {
                _inPipe?.Dispose();
                _outPipe?.Dispose();

                _inPipe = new NamedPipeClientStream(".", inPipeName, PipeDirection.In);
                _outPipe = new NamedPipeClientStream(".", outPipeName, PipeDirection.Out);

                _inPipe.Connect(1000);
                _outPipe.Connect(1000);

                Log?.Invoke("[CrowNest] Connected to pipe!");
                StartReceiveLoop();
                StartSendLoop();
            }
            catch (Exception e)
            {
                LogError?.Invoke($"[CrowNest] Pipe error: {e.Message}");
                _inPipe = null;
                _outPipe = null;
            }
        }

        public static void Send(byte type, string message)
        {
            if (!IsConnected) return;
            _sendQueue.Enqueue((type, message));
        }

        private static void StartSendLoop()
        {
            if (_isSendLoopRunning) return;
            _isSendLoopRunning = true;

            var thread = new Thread(() =>
            {
                while (IsConnected)
                {
                    if (_sendQueue.TryDequeue(out var packet))
                    {
                        try
                        {
                            byte[] body = Encoding.UTF8.GetBytes(packet.Item2);
                            byte[] data = new byte[1 + 4 + body.Length];
                            data[0] = packet.Item1;
                            byte[] lengthBytes = BitConverter.GetBytes(body.Length);
                            Array.Copy(lengthBytes, 0, data, 1, 4);
                            Array.Copy(body, 0, data, 5, body.Length);
                            _outPipe.Write(data, 0, data.Length);
                        }
                        catch (Exception e)
                        {
                            LogError?.Invoke($"[CrowNest] Send error: {e.Message}");
                        }
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
                _isSendLoopRunning = false;
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private static void StartReceiveLoop()
        {
            var thread = new Thread(() =>
            {
                try
                {
                    while (IsConnected)
                    {
                        byte[] typeBuf = new byte[1];
                        try
                        {
                            if (_inPipe.Read(typeBuf, 0, 1) == 0) continue;
                        }
                        catch (TimeoutException) { continue; }

                        byte type = typeBuf[0];

                        byte[] lenBuf = new byte[4];
                        try
                        {
                            if (_inPipe.Read(lenBuf, 0, 4) == 0) continue;
                        }
                        catch (TimeoutException) { continue; }

                        int length = BitConverter.ToInt32(lenBuf, 0);
                        if (length <= 0) continue;

                        byte[] dataBuf = new byte[length];
                        try
                        {
                            if (_inPipe.Read(dataBuf, 0, length) == 0) continue;
                        }
                        catch (TimeoutException) { continue; }

                        string data = Encoding.UTF8.GetString(dataBuf);
                        Log?.Invoke($"[CrowNest] Received - Type: {type}, Data: {data}");
                        OnPacketReceived?.Invoke(type, data);
                    }
                }
                catch (Exception e)
                {
                    LogError?.Invoke($"[CrowNest] ReceiveLoop error: {e.Message}");
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}