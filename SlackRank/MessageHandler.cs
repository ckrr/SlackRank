using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace SlackRank
{
    class MessageHandler
    {
        public static List<Message> getAllMessages()
        {
            List<Message> allMessages = new List<Message>();
            string[] allChannelPaths = Directory.GetDirectories(PathConstants.PATH);
            int numChannelPaths = allChannelPaths.Length;
            for (int i = 0; i < numChannelPaths; i++)
            {
                string[] allChannelFiles = Directory.GetFiles(allChannelPaths[i]);
                System.Console.WriteLine(allChannelPaths[i]);
                int numFiles = allChannelFiles.Length;
                for (int j = 0; j < numFiles; j++)
                {
                    List<Message> allDayMessages = JsonReader.ReadMessages(allChannelFiles[j]);
                    foreach (Message message in allDayMessages)
                    {
                        if (message.subtype == null)
                        {
                            allMessages.Add(message);
                        }
                    }
                }
            }
            return allMessages;
        }
    }
}
