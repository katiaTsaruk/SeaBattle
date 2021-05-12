namespace SeaBattle
{
    public struct PlayerProfile
    {
        public string name;
        public string password;
        public int wins;

        public PlayerProfile(string name, string password, int wins)
        {
            this.name = name;
            this.password = password;
            this.wins = wins;
        }
    }
    
}