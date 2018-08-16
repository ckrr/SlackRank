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
            List<Message> allMessages = MessageHandler.GetAllMessages();
            List<User> allUsers = JsonReader.ReadUsers();
            AdjacencyMatrix adjacencyMatrix = new AdjacencyMatrix(allUsers, allMessages);
            Evaluator evaluator = new Evaluator(adjacencyMatrix);

            List<Tuple<double, string>> usersByMessagePercentage = evaluator.CalcUsersByMessagePercentage();
            List<Tuple<double, string>> rawActionRank = evaluator.CalcRawActionRank();
            List<Tuple<double, string>> unweightedPageRank = evaluator.CalcUnweightedPageRank();
            List<Tuple<double, string>> weightedPageRank = evaluator.WeightScoresByMessagesSent(unweightedPageRank);

            Output.WriteTupleListToFile(usersByMessagePercentage, Constants.USERS_BY_MESSAGE_PERCENTAGE_PATH);
            Output.WriteTupleListToFile(rawActionRank, Constants.RAW_ACTION_RANK_PATH);
            Output.WriteTupleListToFile(unweightedPageRank, Constants.UNWEIGHTED_PAGE_RANK_PATH);
            Output.WriteTupleListToFile(weightedPageRank, Constants.WEIGHTED_PAGE_RANK_PATH);
        }
    }
}
