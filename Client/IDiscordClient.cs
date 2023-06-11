namespace Discord_Bot.Client
{
    internal interface IDiscordClient
    {
        public ulong BotId { get; }

        public Task Run();
    }
}
