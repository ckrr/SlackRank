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
            System.Console.WriteLine("Messages processed.");
            List<User> allUsers = JsonReader.ReadUsers();
            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(allUsers, allMessages);
            System.Console.WriteLine("Matrices filled.");
        }
    }
}
