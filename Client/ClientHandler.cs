using Discord_Bot.DataModels;
using Discord_Bot.Messages;
using Discord_Bot.Utilities;

namespace Discord_Bot.Client
{
    internal class ClientHandler
    {
        private readonly DiscordClient _client;
        private readonly Logger _logger;
        private readonly MessageHandler _messageHandler;
        private readonly DatabaseContext _database;

        public ClientHandler(string token, DatabaseContext database)
        {
            _database = database;
            _logger = new Logger();
            _client = new DiscordClient(this, token);
            _messageHandler = new MessageHandler(_client, _logger, _database);
        }

        public async Task Run()
        {
            await _client.Run();
            _logger.Close();
        }

        public Task Log(string msg)
        {
            _logger.Log(msg);
            return Task.CompletedTask;
        }

        public Task MessageRecieved(Message message)
        {
            if (message.AuthorId == _client.BotId)
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
    }
}
