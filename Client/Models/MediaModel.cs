namespace Client.Models;

public class MediaModel
{
    public MediaModel(byte[] fileData, string fileName)
    {
        FileData = fileData;
        FileName = fileName;
    }

    public string FileName { get; set; }

    public byte[] FileData { get; set; }
}