using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoomService
    {
        private readonly ChatApplicationDbContext _context;

        public RoomService(ChatApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

		// Get by Id
		public async Task<Room?> GetRoomByIdAsync(int id)
		{
			return await _context.Rooms.FindAsync(id);
		}

		// Update
		public async Task<Room?> UpdateRoomAsync(int id, Room updatedRoom)
		{
			var existing = await _context.Rooms.FindAsync(id);
			if (existing == null) return null;

			existing.Name = updatedRoom.Name;
			existing.Description = updatedRoom.Description;
			existing.MaxMembers = updatedRoom.MaxMembers;
			existing.RoomType = updatedRoom.RoomType;
			existing.IsActive = updatedRoom.IsActive;
			existing.UpdatedAt = DateTime.Now;

			await _context.SaveChangesAsync();
			return existing;
		}

		// Delete (soft delete)
		public async Task<bool> DeleteRoomAsync(int id)
		{
			var room = await _context.Rooms.FindAsync(id);
			if (room == null) return false;

			room.IsActive = false;
			room.UpdatedAt = DateTime.Now;
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
