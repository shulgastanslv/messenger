using Client.Models;
using Client.Services;
using Client.ViewModels;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using System;
using System.IO;

namespace Client.Commands.Messages;

public class SendMediaCommand : CommandBase
{
    private readonly ChatViewModel _chatViewModel;
    private readonly ContactModel _contactReceiver;
    private readonly HttpClient _httpClient;

    public SendMediaCommand(HttpClient httpClient, ContactModel contactReceiver,
        ChatViewModel chatViewModel)
    {
        _httpClient = httpClient;
        _contactReceiver = contactReceiver;
        _chatViewModel = chatViewModel;
    }

    public override async void Execute(object? parameter)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = "Все файлы (*.*)|*.*"
        };

        var result = openFileDialog.ShowDialog();

        if (result != true) return;

        _contactReceiver.ChatId ??=
            await ChatService.CreateChatAsync(_httpClient, _contactReceiver, CancellationToken.None);


        var selectedFilePath = openFileDialog.FileName;
        var fileData = await File.ReadAllBytesAsync(selectedFilePath);
        var fileName = Path.GetFileName(selectedFilePath);

        var message = new MessageModel(
            Guid.NewGuid(),
            _chatViewModel.MessageText + "\n" + fileName,
            new MediaModel(fileData, fileName),
            Guid.Empty,
            _contactReceiver.ChatId.Value);

        var content = new StringContent(JsonConvert.SerializeObject(message),
            Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/message/send", content);

        response.EnsureSuccessStatusCode();
    }
}