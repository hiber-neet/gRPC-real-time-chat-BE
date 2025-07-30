using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Membership
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public string? Role { get; set; }

    public DateTime? JoinedAt { get; set; }

    public DateTime? LeftAt { get; set; }

    public bool? IsActive { get; set; }
}
