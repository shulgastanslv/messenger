using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Client.ViewModels;

namespace Client.Commands.Messages;

public class SaveMediaCommand : CommandBase
{
    private readonly ChatViewModel _chatViewModel;

    public SaveMediaCommand(ChatViewModel chatViewModel)
    {
        _chatViewModel = chatViewModel;
    }

    public override bool CanExecute(object? parameter)
    {
        return _chatViewModel.SelectedMessage!.HasMedia;
    }

    public override async void Execute(object? parameter)
    {
        if (_chatViewModel.SelectedMessage?.Media == null
            || _chatViewModel.SelectedMessage?.Media == null)
            return;

        var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/Downloads";
        var filePath = Path.Combine(downloadsPath, _chatViewModel.SelectedMessage.Media.FileName);

        if (!File.Exists(filePath))
            await File.WriteAllBytesAsync(filePath, _chatViewModel.SelectedMessage.Media.FileData,
                CancellationToken.None);

        var startInfo = new ProcessStartInfo
        {
            FileName = filePath,
            UseShellExecute = true,
            Verb = "open"
        };

        Process.Start(startInfo);
    }
}