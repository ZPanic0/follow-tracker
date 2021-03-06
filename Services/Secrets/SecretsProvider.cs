﻿using Services.Secrets.Models;
using System;
using System.IO;
using System.Text.Json;

namespace Services.Secrets
{
    public class SecretsProvider : ISecretsProvider
    {
        private readonly SecretsModel secrets;
        public SecretsProvider()
        {
            secrets = JsonSerializer.Deserialize<SecretsModel>(File.ReadAllText($@"{Environment.CurrentDirectory}\secrets.json"));
        }

        public string GetConnectionString() => secrets.ConnectionString;

        public string GetClientId() => secrets.Twitch.ClientId;

        public string GetClientSecret() => secrets.Twitch.ClientSecret;
    }
}
