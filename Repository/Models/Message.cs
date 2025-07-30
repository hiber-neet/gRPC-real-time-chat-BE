using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Message
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public string? MessageType { get; set; }

    public int? ReplyToId { get; set; }

    public bool? IsEdited { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<FileUpload> FileUploads { get; set; } = new List<FileUpload>();

    public virtual ICollection<Message> InverseReplyTo { get; set; } = new List<Message>();

    public virtual Message? ReplyTo { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
