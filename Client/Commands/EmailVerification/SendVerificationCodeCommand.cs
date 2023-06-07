using System;
using System.Net.Http;
using System.Text;
using Client.Services;
using Client.Stores;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Commands.EmailVerification;

public class SendVerificationCodeCommand : ViewModelCommand
{
    private readonly HttpClient _httpClient;

    private readonly UserStore _userStore;

    private readonly EmailVerificationViewModel _emailVerificationViewModel;

    private readonly int _code;

    public SendVerificationCodeCommand(UserStore userStore, HttpClient httpClient,
        EmailVerificationViewModel emailVerificationViewModel, int code)
    {
        _userStore = userStore;
        _httpClient = httpClient;
        _emailVerificationViewModel = emailVerificationViewModel;
        _code = code;
    }

    public override async void Execute(object? parameter)
    {
        _emailVerificationViewModel.IsLoading = true;

        var content = new StringContent(JsonConvert.SerializeObject(new
        {
            recipient = _userStore.User.Email,
            body = _code.ToString()
        }), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/emailVerification/", content);

        response.EnsureSuccessStatusCode();

        _emailVerificationViewModel.IsLoading = false;
    }
}