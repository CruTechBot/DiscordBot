using Discord_Bot.DataModels;
using Discord_Bot.Messages;

namespace Discord_Bot.Commands
{
    internal class SaveCommand : ICommand
    {
        private readonly DatabaseContext _database;

        public SaveCommand(DatabaseContext database)
        {
            _database = database;
        }

        public bool RunCondition(IMessage message)
        {
            return true;
        }

        public void Run(IMessage message)
        {
            if (!_database.Users.Any(u => u.Id == message.AuthorId))
            {
                _database.Users.Add(new UserItem { Id = message.AuthorId, Name = message.AuthorName });
            }

            _database.Messages.Add(new MessageItem { UserId = message.AuthorId, Content = message.Content, SentTime = DateTime.UtcNow });
            _database.SaveChanges();
        }

        public bool StopCommands(IMessage message)
        {
            return false;
        }
    }
}
