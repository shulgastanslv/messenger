using System;
using Client.Models;
using Client.Properties;
using Client.Stores;

namespace Client.Services;

public static class UserStoreSettingsService
{
    public static UserStore GetUserStore()
    {
        return new UserStore
        {
            User = new UserModel(
                Settings.Default.Id,
                Settings.Default.Username,
                Settings.Default.Password),
            Token = Settings.Default.Token,
            LastResponseTime = Settings.Default.LastResponseTime
        };
    }

    public static void SaveUserStore(UserStore userStore)
    {
        Settings.Default.Id = userStore.User.Id;
        Settings.Default.Username = userStore.User.Username;
        Settings.Default.Password = userStore.User.Password;
        Settings.Default.Token = userStore.Token;
        Settings.Default.LastResponseTime = userStore.LastResponseTime;
        Settings.Default.Save();
    }

    public static void DeleteUserStore(UserStore userStore)
    {
        userStore.User.Id = Guid.Empty;
        userStore.User.Username = null;
        userStore.User.Password = null;
        userStore.Token = null;
        userStore.LastResponseTime = DateTime.MinValue;
    }
}