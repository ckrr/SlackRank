using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace SlackRank
{
    class JsonReader
    {
        public static List<User> ReadUsers()
        {
            StreamReader streamReader = new StreamReader(Constants.USERS_PATH);
            string jsonString = streamReader.ReadToEnd();
            List<User> allUsers = JsonConvert.DeserializeObject<List<User>>(jsonString);
            for (int i=0; i<allUsers.Count; i++)
            {
                allUsers[i].name = CleanName(allUsers[i]);
            }
            return allUsers;
        }
        public static List<Message> ReadMessages(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            string jsonString = streamReader.ReadToEnd();
            List<Message> allMessages = JsonConvert.DeserializeObject<List<Message>>(jsonString);
            return allMessages;
        }

        private static string CleanName(User user)
        {
            string cleanName = user.profile.real_name;
            if (cleanName.Length == 0)
            {
                cleanName = user.name;
            }
            return cleanName;
        }
}
}
