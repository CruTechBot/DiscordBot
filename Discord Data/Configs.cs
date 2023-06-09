﻿namespace Discord_Data
{
    public class Configs
    {
        public string Token { get; set; }
        public DatabaseConfigs Database { get; set; }
    }
    
    public class DatabaseConfigs
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string ConnectionString()
        {
            return $"Server={Server}; Database={Database}; User Id={User}; Password={Password}; TrustServerCertificate=True";
        }
    }
}
