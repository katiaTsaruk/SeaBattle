using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SeaBattle
{
    public class AllProfiles
    {
        public List<PlayerProfile> profiles = new List<PlayerProfile>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerProfile>));
        public FileStream fs = new FileStream("Profiles.xml", FileMode.OpenOrCreate);

        public void AddToList(string name, string password)
        {
            PlayerProfile profile = new PlayerProfile(name, password, 0);
            profiles.Add(profile);
        }

        public PlayerProfile SetProfile(string name, string password)
        {
            PlayerProfile playerProf = new PlayerProfile(name, password,0);
            foreach (PlayerProfile profile in profiles)
            {
                if (name == profile.name && password == profile.password)
                {
                    playerProf = profile;
                }
            }
            return playerProf;
        }
        public void Save()
        {
            serializer.Serialize(fs, profiles);
            fs.Close();
        }

        public void Load()
        {
            profiles = serializer.Deserialize(fs) as List<PlayerProfile>;
            fs.Close();
        }
    }
}   