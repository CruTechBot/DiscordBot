using Discord_Bot.Messages;

namespace Discord_Bot.Commands
{
    internal class GreetCommand : ICommand
    {
        public GreetCommand()
        {
        }

        public bool RunCondition(IMessage message)
        {
            return message.Content.ToLower() == "hi" || message.Content.ToLower() == "hello" || message.Content.ToLower() == "gday";
        }

        public void Run(IMessage message)
        {
            message.Reply($"Gday {message.AuthorName}.");
        }

        public bool StopCommands(IMessage message)
        {
            return true;
        }
    }
}
