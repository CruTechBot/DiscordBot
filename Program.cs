using Discord;
using Discord.WebSocket;

    var _client = new DiscordSocketClient();

    _client.Log += Log;

     var token = File.ReadAllText("token.txt");

    await _client.LoginAsync(TokenType.Bot, token);
    await _client.StartAsync();

    // Block this task until the program is closed.
    await Task.Delay(-1);



Task Log(LogMessage msg)
{
    Console.WriteLine(msg.ToString());
    return Task.CompletedTask;
}

