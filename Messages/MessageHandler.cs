using Discord_Bot.DataModels;
using Discord_Bot.Utilities;

namespace Discord_Bot.Messages
{
    internal class MessageHandler
    {
        private ulong _self;
        private readonly Logger _logger;
        private readonly DatabaseContext _database;
        public MessageHandler(Logger logger, DatabaseContext database)
        {
            _logger = logger;
            _database = database;
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
            if (!_database.Users.Any(u => u.Id == message.AuthorId))
            {
                _database.Users.Add(new UserItem { Id = message.AuthorId, Name = message.AuthorName });
            }

            _database.Messages.Add(new MessageItem { UserId = message.AuthorId, Content = message.Content, SentTime = DateTime.UtcNow });
            _database.SaveChanges();
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
