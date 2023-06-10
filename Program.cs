using Discord_Bot;
using Newtonsoft.Json;

var configs = JsonConvert.DeserializeObject<Configs>(File.ReadAllText("configs.json"));
if (configs == null || string.IsNullOrWhiteSpace(configs.Token))
{
    Console.WriteLine("Invalid configs");
    return;
}

var client = new ClientHandler(configs.Token);
client.Run().Wait();