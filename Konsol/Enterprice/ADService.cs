using System;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Enterprice
{
    public class ADService
    {
        public ADService()
        {
            try
            {
                var config = JsonSerializer.Deserialize<Config>(
                    File.ReadAllText("appsettings.json")
                );
                _server = config?.AD?.Server ?? "";
                _username = config?.AD?.Username ?? "";
                _password = config?.AD?.Password ?? "";
                _domain = config?.AD?.Domain ?? "";
                Console.WriteLine($"Server: {_server}");
                Console.WriteLine($"Username: {_username}");
                Console.WriteLine($"Password: {_password}");
                Console.WriteLine($"Domain: {_domain}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved indlæsning af konfiguration: {ex.Message}");
            }
        }

        private readonly string? _server;
        private readonly string? _username;
        private readonly string? _password;
        private readonly string? _domain;

        public void Start()
        {
            var status = GetServerStatus();
            Console.WriteLine(status);
        }

        public LdapConnection Connect()
        {
            try
            {
                var credential = new NetworkCredential($"{_username}@{_domain}", _password);
                var connection = new LdapConnection(_server)
                {
                    Credential = credential,
                    AuthType = AuthType.Negotiate,
                };
                return connection;
            }
            catch (Exception ex)
            {
                // Returnér null hvis der opstår fejl
                Console.WriteLine($"Fejl ved oprettelse af forbindelse: {ex.Message}");
                return null;
            }
        }

        public string GetServerStatus()
        {
            using (var connection = Connect())
            {
                if (connection == null)
                    return "Ikke kørende - kunne ikke oprette forbindelse.";
                try
                {
                    connection.Bind();
                    return "Kørende";
                }
                catch (LdapException ex)
                {
                    return $"Ikke kørende - LDAP fejl: {ex.Message}";
                }
                catch (Exception ex)
                {
                    return $"Ikke kørende - {ex.Message}";
                }
            }
        }
    }

    public class Config
    {
        public ADConfig? AD { get; set; }
    }

    public class ADConfig
    {
        public string? Server { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Domain { get; set; }
    }
}
