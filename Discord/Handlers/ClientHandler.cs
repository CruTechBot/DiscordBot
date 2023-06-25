using Discord_Data.DataModels;
using IDiscord.DiscordModels;
using IDiscord.Utilities;

namespace IDiscord.Handlers
{
    public class ClientHandler
    {
        private readonly DiscordClient _client;
        private readonly Logger _logger;
        private readonly MessageHandler _messageHandler;
        private readonly ReactionHandler _reactionHandler;
        private readonly DatabaseContext _database;

        public ClientHandler(string token, 
            List<Func<IDiscordClient, ILogger, DatabaseContext, IMessageCommand>> botCommands = null,
            List<Func<IDiscordClient, ILogger, DatabaseContext, IMessageCommand>> privateCommands = null,
            List<Func<IDiscordClient, ILogger, DatabaseContext, IMessageCommand>> publicCommands = null,
            List<Func<IDiscordClient, ILogger, DatabaseContext, IReactionCommand>> privateReactionCommands = null,
            List<Func<IDiscordClient, ILogger, DatabaseContext, IReactionCommand>> publicReactionCommands = null)
        {
            _database = new DatabaseContext();
            _logger = new Logger();
            _client = new DiscordClient(this, token);
            _messageHandler = new MessageHandler(_client, _logger, _database, botCommands, privateCommands, publicCommands);
            _reactionHandler = new ReactionHandler(_client, _logger, _database, privateReactionCommands, publicReactionCommands);
        }

        public async Task Run()
        {
            await _client.Run();
            _logger.Close();
        }

        internal async Task Log(string msg)
        {
            _logger.Log(msg);
        }

        internal async Task MessageRecieved(IMessageModel message)
        {
            if (message.AuthorId == _client.BotId)
            {
                return;
            }

            _logger.Log($"{message.AuthorName}({(message.IsPrivate ? "private" : $"{message.Channel.Server.Name},{message.Channel.Name}")}): {message.Content}");

            if (message.IsBot)
            {
                await _messageHandler.BotMessageHandler(message);
            }
            else if (message.IsPrivate)
            {
                await _messageHandler.PrivateMessageHandler(message);
            }
            else
            {
                await _messageHandler.ChannelMessageHandler(message);
            }
        }

        internal async Task ReactionRecieved(IMessageModel message, IChannelModel channel, IReactionModel reaction)
        {
            if (reaction.User.Id == _client.BotId)
            {
                return;
            }
            
            if (channel.IsDM)
            {
                await _reactionHandler.PrivateMessageHandler(reaction,message,channel);
            }
            else
            {
                await _reactionHandler.ChannelMessageHandler(reaction, message, channel);
            }
        }
    }
}
