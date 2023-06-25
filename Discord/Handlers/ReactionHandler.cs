using Discord_Data.DataModels;
using IDiscord.DiscordModels;
using IDiscord.Utilities;

namespace IDiscord.Handlers
{
    internal class ReactionHandler
    {
        private readonly IDiscordClient _client;
        private readonly Logger _logger;
        private readonly DatabaseContext _database;
        private readonly IEnumerable<IReactionCommand>? _privateCommands;
        private readonly IEnumerable<IReactionCommand>? _publicCommands;

        public ReactionHandler(IDiscordClient client, Logger logger, DatabaseContext database, List<Func<IDiscordClient, ILogger, DatabaseContext, IReactionCommand>> privateCommands, List<Func<IDiscordClient, ILogger, DatabaseContext, IReactionCommand>> publicCommands)
        {
            _client = client;
            _logger = logger;
            _database = database;

            _privateCommands = privateCommands?.Select(bc => bc(client, logger, database));
            _publicCommands = publicCommands?.Select(bc => bc(client, logger, database));
        }

        public async Task PrivateMessageHandler(IReactionModel reaction, IMessageModel message, IChannelModel channel)
        {
            await RunCommands(reaction, message, channel, _privateCommands);
        }

        public async Task ChannelMessageHandler(IReactionModel reaction, IMessageModel message, IChannelModel channel)
        {
            await RunCommands(reaction, message, channel, _publicCommands);
        }

        static async Task RunCommands(IReactionModel reaction, IMessageModel message, IChannelModel channel, IEnumerable<IReactionCommand>? commands)
        {
            if (commands == null)
            {
                return;
            }
            foreach (var command in commands)
            {
                if (command.RunCondition(reaction, message, channel))
                {
                    await command.Run(reaction, message, channel);
                    if (command.StopCommands(reaction, message, channel))
                    {
                        return;
                    }
                }
            }
        }
    }
}
