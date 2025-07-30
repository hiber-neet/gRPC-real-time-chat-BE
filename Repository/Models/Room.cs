using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Room
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? RoomType { get; set; }

    public int CreatedBy { get; set; }

    public int? MaxMembers { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
