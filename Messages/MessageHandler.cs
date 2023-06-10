using Discord_Bot.Utilities;

namespace Discord_Bot.Messages
{
    internal class MessageHandler
    {
        private ulong _self;
        private readonly Logger _logger;
        public MessageHandler(Logger logger)
        {
            _logger = logger;
        }

        public void SetSelf(ulong self)
        { 
            _self = self;
        }

        public void BotMessageHandler(IMessage message)
        {
            _logger.Log($"Message recieved from bot {message.AuthorName}");
        }

        public void PrivateMessageHandler(IMessage message)
        {
            message.Reply("Hi " + message.AuthorName);
        }

        public void ChannelMessageHandler(IMessage message)
        {
            if (message.UsersMentioned.ContainsKey(_self))
            {
                message.Reply("Wazzzzzzz uuuuuuuuup");
            }
        }
    }
}
