using Grpc.Core;
using GrpcRealTimeAssignment;
using Repository.Models;
using Service;
using System.Collections.Concurrent;

namespace GrpcRealTimeAssignment.Services
{
    public class ChatService : Chat.ChatBase
    {
        private static ConcurrentDictionary<string, IServerStreamWriter<ChatMessage>> _clients = new();
        private readonly MessageService _messageService;
        private readonly ChatApplicationDbContext _context;

        public ChatService(MessageService messageService, ChatApplicationDbContext context)
        {
            _messageService = messageService;
            _context = context;
        }

        public override async Task JoinChat(JoinRequest request, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
        {
            _clients[request.Username] = responseStream;

            await BroadcastMessage($"{request.Username} joined the chat.", "System");

            try
            {
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
            var now = DateTime.Now;
         

            // Tạo entity Message
            var message = new Message
            {
                RoomId = 1,
                UserId = 1,
                Content = request.Text,
                MessageType = "text",
                CreatedAt = now,
                IsDeleted = false,
                IsEdited = false
            };

            
            
            // Broadcast về client
            await BroadcastMessage(request.Text, request.User, now);
            await _messageService.SendMessageAsync(message);

            return new SendReply { Success = true };
        }

        private async Task BroadcastMessage(string text, string user, DateTime? time = null)
        {
            var message = new ChatMessage
            {
                User = user,
                Text = text,             
                Timestamp = (time ?? DateTime.Now).ToString("HH:mm:ss")
            };

            foreach (var client in _clients.Values)
            {
                await client.WriteAsync(message);
            }
        }
    }
}
