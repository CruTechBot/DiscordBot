using Discord_Bot.Messages;
using Discord_Bot.Utilities;

namespace Discord_Bot.Commands
{
    internal class LogNameCommand : ICommand
    {
        private readonly Logger _logger;
        private readonly string _logText;

        public LogNameCommand(Logger logger, string logText)
        {
            _logger = logger;
            _logText = logText;
        }

        public bool RunCondition(IMessage message)
        {
            return true;
        }
        public void Run(IMessage message)
        {
            _logger.Log(string.Format(_logText, message.AuthorName));
        }

        public bool StopCommands(IMessage message)
        {
            return false;
        }
    }
}
