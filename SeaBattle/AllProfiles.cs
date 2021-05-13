using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SeaBattle
{
    [Serializable]
    public class AllProfiles
    {
        public List<PlayerProfile> profiles = new List<PlayerProfile>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerProfile>));
        
        public PlayerProfile SetProfile(string name, string password)
        {
            foreach (PlayerProfile profile in profiles)
            {
                if (name == profile.name && password == profile.password)
                {
                    return profile;
                }
            }
            PlayerProfile newProfile = new PlayerProfile(name, password,0);
            profiles.Add(newProfile);
            return newProfile;
        }
        public void Save()
        {
            using (FileStream fs = new FileStream("Profiles.xml", FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, profiles);
            }
        }

        public void Load()
        {
            using (FileStream fs = new FileStream("Profiles.xml", FileMode.OpenOrCreate))
            {
                profiles = serializer.Deserialize(fs) as List<PlayerProfile>;
            }
        }
    }
}   