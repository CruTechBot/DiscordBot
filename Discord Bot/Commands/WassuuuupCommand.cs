using IDiscord.DiscordModels;

namespace Discord_Bot.Commands
{
    internal class WassuuuupCommand : IMessageCommand
    {
        private readonly IDiscordClient _client;

        public WassuuuupCommand(IDiscordClient client)
        {
            _client = client;
        }
        public bool RunCondition(IMessageModel message)
        {
            return message.UsersMentioned.Any(um => um.Id == _client.BotId);
        }

        public Task Run(IMessageModel message)
        {
            message.Reply("Wazzzzzzz uuuuuuuuup");
            return Task.CompletedTask;
        }

        public bool StopCommands(IMessageModel message)
        {
            return true;
        }
    }
}
