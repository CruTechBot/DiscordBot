using Discord.WebSocket;

namespace IDiscord.DiscordModels
{
    internal class UserModel : IUserModel
    {
        private readonly ulong _id;
        private readonly SocketUser? _user;

        public UserModel(SocketUser user)
        {
            _id = user.Id;
            _user = user;
        }
        public UserModel(ulong id)
        {
            _id = id;
            _user = null;
        }

        public ulong Id => _id;
        public string? Name => _user?.Username;
    }
}
