using Discord_Data.DataModels;
using IDiscord.DiscordModels;

namespace Discord_Bot.Commands
{
    internal class SaveCommand : IMessageCommand
    {
        private readonly DatabaseContext _database;

        public SaveCommand(DatabaseContext database)
        {
            _database = database;
        }

        public bool RunCondition(IMessageModel message)
        {
            return true;
        }

        public Task Run(IMessageModel message)
        {
            if (!_database.Users.Any(u => u.Id == message.AuthorId))
            {
                _database.Users.Add(new UserItem { Id = message.AuthorId, Name = message.AuthorName });
            }

            _database.Messages.Add(new MessageItem { UserId = message.AuthorId, Content = message.Content, SentTime = message.TimeSent });
            _database.SaveChanges();
            return Task.CompletedTask;
        }

        public bool StopCommands(IMessageModel message)
        {
            return false;
        }
    }
}
