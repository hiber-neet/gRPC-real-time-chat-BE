using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string? AvatarUrl { get; set; }

    public bool? IsOnline { get; set; }

    public DateTime? LastSeen { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
