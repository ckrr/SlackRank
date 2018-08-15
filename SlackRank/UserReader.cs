using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace SlackRank
{
    class UserReader
    {
        public static List<User> ReadUsers()
        {
            StreamReader streamReader = new StreamReader("../../../SlackData/users.json");
            string jsonString = streamReader.ReadToEnd();
            List<User> allUsers = JsonConvert.DeserializeObject<List<User>>(jsonString);
            return allUsers;
        }
    }
}
