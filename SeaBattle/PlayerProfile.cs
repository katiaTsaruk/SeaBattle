using System;

namespace SeaBattle
{
    [Serializable]
    public class PlayerProfile
    {
        public string name;
        public string password;
        public int wins;

        public PlayerProfile()
        {
            
        }
        public PlayerProfile(string name, string password, int wins)
        {
            this.name = name;
            this.password = password;
            this.wins = wins;
        }
    }
    
}