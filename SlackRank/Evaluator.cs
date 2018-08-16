using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace SlackRank
{
    class Evaluator
    {
        public static void PrintUsersByMessagePercentage(List<int> messagesPerUser, List<User> allUsers)
        {
            int numUsers = allUsers.Count;
            double totalMessages = (double) messagesPerUser.Sum();

            List<Tuple<double, string>> usersByMessagePercentage = new List<Tuple<double, string>>();
            for (int i=0; i<numUsers; i++)
            {
                double messagesPercentage = 100.0 * messagesPerUser[i] / totalMessages;
                string userName = allUsers[i].profile.real_name;
                if (userName.Length == 0)
                {
                    userName = allUsers[i].name;
                }
                usersByMessagePercentage.Add(new Tuple<double, string>(messagesPercentage, userName));
            }
            usersByMessagePercentage.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            string allFileText = String.Empty;
            for (int i=0; i<numUsers; i++)
            {
                string writeName = usersByMessagePercentage[i].Item2.PadRight(30, ' ') + ",";
                string writePercentage = usersByMessagePercentage[i].Item1.ToString().Substring(0, Math.Min(4, usersByMessagePercentage[i].Item1.ToString().Length)) + "%";
                allFileText += (writeName + writePercentage + Environment.NewLine);
            }
            File.WriteAllText("../../../output.csv", allFileText);
        }
    }
}
