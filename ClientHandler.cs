using Discord;
using Discord.WebSocket;
using Discord_Bot.DataModels;
using Discord_Bot.Messages;
using Discord_Bot.Utilities;

namespace Discord_Bot
{
    internal class ClientHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly string _token;
        private readonly Logger _logger;
        private readonly MessageHandler _messageHandler;
        private readonly DatabaseContext _database;

        public ClientHandler(string token, DatabaseContext database)
        {
            _database = database;
            _logger = new Logger();
            _messageHandler = new MessageHandler(_logger, _database);

            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All
            };
            _client = new DiscordSocketClient(config);
            _client.Log += Log;
            _client.MessageReceived += MessageRecieved;
            _client.PresenceUpdated += PresenceUpdated;

            _token = token;
        }

        public async Task Run()
        {
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
            while (_client.CurrentUser == null) {}
            _messageHandler.SetSelf(_client.CurrentUser.Id);
            while (Console.ReadKey().KeyChar != 'q'){}
            await _client.StopAsync();
            _logger.Close();
        }

        Task Log(LogMessage msg)
        {
            _logger.Log(msg);
            return Task.CompletedTask;
        }

        Task MessageRecieved(SocketMessage socketMessage)
        {
            var message = new Message(socketMessage);
            if (message.AuthorId == _client.CurrentUser.Id)
            {
                return Task.CompletedTask;
            }

            _logger.Log($"{message.AuthorName}({(message.IsPrivate ? "private" : $"{message.GuildName},{message.ChannelName}")}): {message.Content}");

            if (message.IsBot)
            {
                _messageHandler.BotMessageHandler(message);
            }
            else if (message.IsPrivate)
            {
                _messageHandler.PrivateMessageHandler(message);
            }
            else
            {
                _messageHandler.ChannelMessageHandler(message);
            }

            return Task.CompletedTask;
        }

        static Task PresenceUpdated(SocketUser user, SocketPresence arg2, SocketPresence arg3)
        {
            return Task.CompletedTask;
        }
    }
}
