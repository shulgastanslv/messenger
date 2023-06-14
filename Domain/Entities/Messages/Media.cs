﻿namespace Domain.Entities.Messages;

public class Media : Message
{
    public Media(Guid id, string content, Guid sender,
        Guid receiverChatId, byte[] fileData, string fileName)
        : base(id, content, sender, receiverChatId)
    {
        FileData = fileData;
        FileName = fileName;
    }

    public string FileName { get; set; }

    public byte[] FileData { get; set; }
}