using Discord_Bot.Messages;

namespace Discord_Bot.Commands
{
    internal interface ICommand
    {
        public bool RunCondition(IMessage message);

        public void Run(IMessage message);

        public bool StopCommands(IMessage message);
    }
}
