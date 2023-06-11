using Discord_Bot.Client;
using Discord_Bot.Messages;

namespace Discord_Bot.Commands
{
    internal class WassuuuupCommand : ICommand
    {
        private readonly IDiscordClient _client;

        public WassuuuupCommand(IDiscordClient client)
        {
            _client = client;
        }
        public bool RunCondition(IMessage message)
        {
            return message.UsersMentioned.ContainsKey(_client.BotId);
        }

        public void Run(IMessage message)
        {
            message.Reply("Wazzzzzzz uuuuuuuuup");
        }

        public bool StopCommands(IMessage message)
        {
            return true;
        }
    }
}
