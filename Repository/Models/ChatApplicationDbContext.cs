using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public partial class ChatApplicationDbContext : DbContext
{
    public ChatApplicationDbContext()
    {
    }

    public ChatApplicationDbContext(DbContextOptions<ChatApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FileUpload> FileUploads { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ChatApplicationDB;User Id=sa;Password=1234;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileUpload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FileUplo__3213E83F81F332DC");

            entity.ToTable("FileUpload");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FileExtension)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("file_extension");
            entity.Property(e => e.FilePath)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("file_path");
            entity.Property(e => e.FileSize).HasColumnName("file_size");
            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.MimeType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mime_type");
            entity.Property(e => e.OriginalFilename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("original_filename");
            entity.Property(e => e.StoredFilename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("stored_filename");
            entity.Property(e => e.UploadStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("uploading")
                .HasColumnName("upload_status");

            entity.HasOne(d => d.Message).WithMany(p => p.FileUploads)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FileUploa__messa__59FA5E80");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Membersh__3213E83F2BA1B2A7");

            entity.ToTable("Membership");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.JoinedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("joined_at");
            entity.Property(e => e.LeftAt)
                .HasColumnType("datetime")
                .HasColumnName("left_at");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("member")
                .HasColumnName("role");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Message__3213E83F3BED2D1B");

            entity.ToTable("Message");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsEdited)
                .HasDefaultValue(false)
                .HasColumnName("is_edited");
            entity.Property(e => e.MessageType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("text")
                .HasColumnName("message_type");
            entity.Property(e => e.ReplyToId).HasColumnName("reply_to_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ReplyTo).WithMany(p => p.InverseReplyTo)
                .HasForeignKey(d => d.ReplyToId)
                .HasConstraintName("FK__Message__reply_t__5441852A");

            entity.HasOne(d => d.Room).WithMany(p => p.Messages)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Message__room_id__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Message__user_id__534D60F1");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room__3213E83F22C05E08");

            entity.ToTable("Room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.MaxMembers)
                .HasDefaultValue(100)
                .HasColumnName("max_members");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.RoomType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("public")
                .HasColumnName("room_type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__created_by__44FF419A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F81EBD47D");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616441CDE46A").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5720871AE2B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsOnline)
                .HasDefaultValue(false)
                .HasColumnName("is_online");
            entity.Property(e => e.LastSeen)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_seen");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
