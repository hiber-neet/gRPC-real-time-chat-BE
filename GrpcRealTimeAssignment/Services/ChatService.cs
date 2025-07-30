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

            await BroadcastMessage($"{request.Username} joined the chat.", "System", 0);

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
                await BroadcastMessage($"{request.Username} left the chat.", "System", 0);
            }
        }

        public override async Task<SendReply> SendMessage(ChatMessage request, ServerCallContext context)
        {
            var now = DateTime.Now;

            // Lấy UserId từ username
            var user = _context.Users.FirstOrDefault(u => u.Username == request.User);
            if (user == null)
            {
                return new SendReply { Success = false };
            }

            // Tạo entity Message
            var message = new Message
            {
                RoomId = request.RoomId,
                UserId = user.Id,
                Content = request.Text,
                MessageType = "text",
                CreatedAt = now,
                IsDeleted = false,
                IsEdited = false
            };

            

            // Broadcast về client
            await BroadcastMessage(request.Text, request.User, request.RoomId, now);

            return new SendReply { Success = true };
        }

        private async Task BroadcastMessage(string text, string user, int roomId, DateTime? time = null)
        {
            var message = new ChatMessage
            {
                User = user,
                Text = text,
                RoomId = 1,
                Timestamp = (time ?? DateTime.Now).ToString("HH:mm:ss")
            };

            foreach (var client in _clients.Values)
            {
                await client.WriteAsync(message);
            }
        }
    }
}
