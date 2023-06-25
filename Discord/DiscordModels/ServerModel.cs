using Discord.WebSocket;

namespace IDiscord.DiscordModels
{
    internal class ServerModel : IServerModel
    {
        private readonly SocketGuild _guild;

        public ServerModel(SocketGuild guild)
        {
            _guild = guild;
        }

        public string Name => _guild.Name;
        public IEnumerable<ICategoryModel> Categories => _guild.CategoryChannels.Select(c => new CategoryModel(c));
        public IEnumerable<IChannelModel> Channels => _guild.Channels.Select(c => new ChannelModel(c));
    }
}
