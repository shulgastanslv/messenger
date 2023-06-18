namespace Domain.Entities.Messages;

public class Media
{
    public Media(byte[]? fileData = null, string? fileName = null)
    {
        FileData = fileData;
        FileName = fileName;
    }

    public string? FileName { get; set; }

    public byte[]? FileData { get; set; }
}