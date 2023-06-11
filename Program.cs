using Discord_Bot;
using Discord_Bot.Client;
using Discord_Bot.DataModels;
using Newtonsoft.Json;

var token = JsonConvert.DeserializeObject<Configs>(File.ReadAllText("configs.json"))?.Token;
var database = new DatabaseContext();

if (string.IsNullOrWhiteSpace(token) || database == null)
{
    Console.WriteLine("Invalid configs");
    return;
}

var client = new ClientHandler(token, database);
client.Run().Wait();