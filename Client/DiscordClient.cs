using Discord;
using Discord.WebSocket;
using Discord_Bot.Messages;

namespace Discord_Bot.Client
{
    internal class DiscordClient : IDiscordClient
    {
        private readonly DiscordSocketClient _client;
        private readonly string _token;

        public DiscordClient(ClientHandler handler, string token)
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All
            };

            _client = new DiscordSocketClient(config);
            _client.Log += l => handler.Log(l.ToString());
            _client.MessageReceived += sm => handler.MessageRecieved(new Message(sm));
            _client.PresenceUpdated += PresenceUpdated;

            _token = token;
        }
        
        public ulong BotId => _client.CurrentUser.Id;

        public async Task Run()
        {
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            while (Console.ReadKey().KeyChar != 'q') { }
            await _client.StopAsync();
        }
        public Task PresenceUpdated(SocketUser user, SocketPresence arg2, SocketPresence arg3)
        {
            return Task.CompletedTask;
        }
    }
}
