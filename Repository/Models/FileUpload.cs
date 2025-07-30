using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class FileUpload
{
    public int Id { get; set; }

    public int MessageId { get; set; }

    public string OriginalFilename { get; set; } = null!;

    public string StoredFilename { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public long FileSize { get; set; }

    public string? MimeType { get; set; }

    public string? FileExtension { get; set; }

    public string? UploadStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Message Message { get; set; } = null!;
}
