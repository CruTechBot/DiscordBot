using Discord_Bot.Client;
using Discord_Bot.Commands;
using Discord_Bot.DataModels;
using Discord_Bot.Utilities;

namespace Discord_Bot.Messages
{
    internal class MessageHandler
    {
        private readonly IDiscordClient _client;
        private readonly Logger _logger;
        private readonly DatabaseContext _database;
        private readonly List<ICommand> _botCommands;
        private readonly List<ICommand> _privateCommands;
        private readonly List<ICommand> _publicCommands;

        public MessageHandler(IDiscordClient client, Logger logger, DatabaseContext database)
        {
            _client = client;
            _logger = logger;
            _database = database;

            _botCommands = new List<ICommand>
            {
                new LogNameCommand(_logger, "Message recieved from bot {0}")
            };

            _privateCommands = new List<ICommand>
            {
                new SaveCommand(_database)
            };

            _publicCommands = new List<ICommand>
            {
                new WassuuuupCommand(_client),
                new GreetCommand(),
                new LogNameCommand(_logger, "Message recieved from user {0}")
            };
        }

        public void BotMessageHandler(IMessage message)
        {
            RunCommands(message, _botCommands);
        }

        public void PrivateMessageHandler(IMessage message)
        {
            RunCommands(message, _privateCommands);
        }

        public void ChannelMessageHandler(IMessage message)
        {
            RunCommands(message, _publicCommands);
        }

        static void RunCommands(IMessage message, IEnumerable<ICommand> commands)
        {
            foreach (var command in commands)
            {
                if (command.RunCondition(message))
                {
                    command.Run(message);
                    if (command.StopCommands(message))
                    {
                        return;
                    }
                }
            }
        }
    }
}
