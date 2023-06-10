namespace Discord_Bot.Messages
{
    internal interface IMessage
    {
        string AuthorName { get; }
        ulong AuthorId { get; }
        string Content { get; }
        bool IsBot { get; }
        bool IsPrivate { get; }
        IDictionary<ulong, string> UsersMentioned { get; }
        string ChannelName { get; }
        string? GuildName { get; }

        Task Reply(string content);
    }
}
