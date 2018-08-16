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
        public static List<Message> GetAllMessages()
        {
            List<Message> allMessages = new List<Message>();
            string[] allChannelPaths = Directory.GetDirectories(Constants.PATH);
            int numChannelPaths = allChannelPaths.Length;
            for (int i = 0; i < numChannelPaths; i++)
            {
                if (Constants.SINGLE_CHANNEL == "" || allChannelPaths[i].IndexOf(Constants.SINGLE_CHANNEL) != -1)
                {
                    string[] allChannelFiles = Directory.GetFiles(allChannelPaths[i]);
                    int numFiles = allChannelFiles.Length;
                    for (int j = 0; j < numFiles; j++)
                    {
                        List<Message> allDayMessages = JsonReader.ReadMessages(allChannelFiles[j]);
                        foreach (Message message in allDayMessages)
                        {
                            if (message.subtype == "")
                            {
                                allMessages.Add(message);
                            }
                        }
                    }
                }
            }
            return allMessages;
        }
    }
}
