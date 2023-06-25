using IDiscord.DiscordModels;

namespace Discord_Bot.Commands
{
    internal class GreetCommand : IMessageCommand
    {
        public GreetCommand()
        {
        }

        public bool RunCondition(IMessageModel message)
        {
            return message.Content.ToLower() == "hi" || message.Content.ToLower() == "hello" || message.Content.ToLower() == "gday";
        }

        public Task Run(IMessageModel message)
        {
            message.Reply($"Gday {message.AuthorName}.");
            return Task.CompletedTask;
        }

        public bool StopCommands(IMessageModel message)
        {
            return true;
        }
    }
}
