using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace SlackRank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Message> allMessages = MessageHandler.getAllMessages();
            List<User> allUsers = JsonReader.ReadUsers();
            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(allUsers, allMessages);
            Evaluator.PrintUsersByMessagePercentage(adjacencyMatrix.GetMessagesPerUser(), allUsers);
        }
    }
}
