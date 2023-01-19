using IndentifyApp.WebApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace IndentifyApp.WebApp.SignalRHubs;

/// <summary>
///     Имитация идентификации пользователя
/// </summary>
public class IdentifyProfileHub : Hub
{
    public async Task Send(UserProfileModel profile)
    {
        var status = "В процессе.";
        await Clients.All.SendAsync("Receive", status);

        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(1000);
            status += ".";
            await Clients.All.SendAsync("Receive", status);
        }

        await Clients.All.SendAsync("Receive", "Успешно!");
    }
}
