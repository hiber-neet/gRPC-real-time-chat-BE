using Microsoft.EntityFrameworkCore;
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

		public async Task<List<Membership>> GetMembersInRoomAsync(int roomId)
		{
			return await _context.Memberships
				.Where(m => m.RoomId == roomId && m.IsActive == true)
				.ToListAsync();
		}

		public async Task<Membership?> UpdateMembershipAsync(int id, Membership update)
		{
			var existing = await _context.Memberships.FindAsync(id);
			if (existing == null) return null;

			existing.Role = update.Role ?? existing.Role;
			existing.LeftAt = update.LeftAt;
			existing.IsActive = update.IsActive ?? existing.IsActive;
			await _context.SaveChangesAsync();
			return existing;
		}

		public async Task<bool> DeleteMembershipAsync(int id)
		{
			var membership = await _context.Memberships.FindAsync(id);
			if (membership == null) return false;

			membership.IsActive = false;
			membership.LeftAt = DateTime.Now;
			await _context.SaveChangesAsync();
			return true;
		}

	}

}
