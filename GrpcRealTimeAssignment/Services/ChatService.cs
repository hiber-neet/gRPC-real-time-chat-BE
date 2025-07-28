using Grpc.Core;
using GrpcRealTimeAssignment;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Concurrent;

namespace GrpcRealTimeAssignment.Services
{
    public class ChatService : Chat.ChatBase
    {
        private static ConcurrentDictionary<string, IServerStreamWriter<ChatMessage>> _clients = new();

        public override async Task JoinChat(JoinRequest request, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
        {
            _clients[request.Username] = responseStream;

            // Gửi chào mừng
            await BroadcastMessage($"{request.Username} joined the chat.", "System");

            try
            {
                // Chờ client giữ kết nối (real-time stream)
                while (!context.CancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1000);
                }
            }
            finally
            {
                _clients.TryRemove(request.Username, out _);
                await BroadcastMessage($"{request.Username} left the chat.", "System");
            }
        }

        public override async Task<SendReply> SendMessage(ChatMessage request, ServerCallContext context)
        {
            await BroadcastMessage(request.Text, request.User);
            return new SendReply { Success = true };
        }

        private async Task BroadcastMessage(string text, string user)
        {
            var message = new ChatMessage
            {
                User = user,
                Text = text,
                Timestamp = DateTime.Now.ToString("HH:mm:ss")
            };

            foreach (var client in _clients.Values)
            {
                await client.WriteAsync(message);
            }
        }
    }
}
