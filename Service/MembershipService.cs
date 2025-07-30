using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MembershipService
    {
        private readonly ChatApplicationDbContext _context;

        public MembershipService(ChatApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Membership> JoinRoomAsync(int userId, int roomId)
        {
            var membership = new Membership { UserId = userId, RoomId = roomId };
            _context.Memberships.Add(membership);
            await _context.SaveChangesAsync();
            return membership;
        }
    }

}
