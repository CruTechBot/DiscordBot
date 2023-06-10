using Discord.WebSocket;

namespace Discord_Bot.Messages
{
    internal class Message : IMessage
    {
        private readonly SocketMessage _socketMessage;
        public Message(SocketMessage socketMessage)
        {
            _socketMessage = socketMessage;
        }

        public string AuthorName => _socketMessage.Author.Username;
        public ulong AuthorId => _socketMessage.Author.Id;
        public string Content => _socketMessage.Content;
        public bool IsBot => _socketMessage.Author.IsBot;
        public bool IsPrivate => _socketMessage.Channel.GetType() == typeof(SocketDMChannel);
        public string ChannelName => _socketMessage.Channel.Name;
        public string? GuildName => (_socketMessage.Channel as SocketGuildChannel)?.Guild.Name;
        public IDictionary<ulong, string> UsersMentioned => _socketMessage.MentionedUsers.ToDictionary(mu => mu.Id, mu => mu.Username);

        public async Task Reply(string content)
        {
            await _socketMessage.Channel.SendMessageAsync(content);
        }
    }
}
