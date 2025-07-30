using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MessageService
    {
        private readonly ChatApplicationDbContext _context;

        public MessageService(ChatApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Message> SendMessageAsync(Message msg)
        {
            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();
            return msg;
        }

        public async Task<List<Message>> GetMessagesByRoom(int roomId)
        {
            return await _context.Messages
                .Where(m => m.RoomId == roomId)
                .ToListAsync();
        }
    }

}
