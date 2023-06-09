﻿using BlazorRcon.Common.Interfaces;
using BlazorRcon.Common.Models;

namespace BlazorRcon.Common.Services;

public class RconClient : IRconClient
{
    public async Task<string?> ExecuteCommand(RconData data)
    {
        using var client = new Rcon.RconClient();
        if (await client.ConnectAsync(data.Host, data.Port ?? 25575) && await client.AuthenticateAsync(data.Password))
        {
            return await client.SendCommandAsync(data.LastCommand);
        }

        return null;
    }
}