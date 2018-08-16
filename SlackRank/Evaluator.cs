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
        private AdjacencyMatrix adjacencyMatrix;
        public Evaluator(AdjacencyMatrix inputAdjacencyMatrix)
        {
            adjacencyMatrix = inputAdjacencyMatrix;
        }

        public List<Tuple<double, string>> CalcUsersByMessagePercentage()
        {
            double totalMessages = (double)adjacencyMatrix.messagesPerUser.Sum();

            List<Tuple<double, string>> usersByMessagePercentage = new List<Tuple<double, string>>();
            for (int i = 0; i < adjacencyMatrix.numUsers; i++)
            {
                double messagesPercentage = 100.0 * adjacencyMatrix.messagesPerUser[i] / totalMessages;
                usersByMessagePercentage.Add(new Tuple<double, string>(messagesPercentage, adjacencyMatrix.allUsers[i].name));
            }
            return usersByMessagePercentage;
        }

        public List<Tuple<double, string>> CalcRawActionRank()
        {
            List<Tuple<double, string>> rawActionRank = new List<Tuple<double, string>>();
            for (int i = 0; i < adjacencyMatrix.numUsers; i++)
            {
                double numActionsReceived = 0;
                for (int j = 0; j < adjacencyMatrix.numUsers; j++)
                {
                    numActionsReceived += (double)adjacencyMatrix.combinedMatrix[j][i];
                }
                rawActionRank.Add(new Tuple<double, string>(numActionsReceived, adjacencyMatrix.allUsers[i].name));
            }
            return rawActionRank;
        }

        public List<Tuple<double, string>> CalcUnweightedPageRank()
        {
            List<List<Tuple<int, double>>> weightedAdjacencyList = adjacencyMatrix.GetWeightedAdjacencyList();
            List<double> prevScores = new List<double>();
            List<double> newScores = new List<double>();
            double baselineScore = 1.0 / (adjacencyMatrix.numUsers);
            for (int i = 0; i < adjacencyMatrix.numUsers; i++)
            {
                prevScores.Add(baselineScore);
                newScores.Add(0);
            }
            List<Tuple<double, string>> unweightedPageRank = new List<Tuple<double, string>>();
            List<List<Tuple<int, double>>> adjacencyList = adjacencyMatrix.GetWeightedAdjacencyList();
            for (int h = 0; h < 20; h++)
            {
                for (int i = 0; i < adjacencyMatrix.numUsers; i++)
                {
                    newScores[i] = (1.0 - Constants.DAMPING_FACTOR) / (adjacencyMatrix.numUsers);
                }
                for (int i = 0; i < adjacencyMatrix.numUsers; i++)
                {
                    for (int j = 0; j < adjacencyList[i].Count; j++)
                    {
                        int recipientIndex = adjacencyList[i][j].Item1;
                        newScores[recipientIndex] += (Constants.DAMPING_FACTOR * prevScores[i] * adjacencyList[i][j].Item2);
                    }
                }
                double sumScores = newScores.Sum();
                for (int i = 0; i < adjacencyMatrix.numUsers; i++)
                {
                    newScores[i] /= sumScores;
                }
                for (int i = 0; i < adjacencyMatrix.numUsers; i++)
                {
                    prevScores[i] = newScores[i];
                }
            }
            for (int i = 0; i < adjacencyMatrix.numUsers; i++)
            {
                unweightedPageRank.Add(new Tuple<double, string>(100.0 * newScores[i], adjacencyMatrix.allUsers[i].name));
            }
            return unweightedPageRank;
        }


        public List<Tuple<double, string>> WeightScoresByMessagesSent(List<Tuple<double, string>> scores)
        {
            List<Tuple<double, string>> weightedScores = new List<Tuple<double, string>>();
            double weightFactor = ((double) (adjacencyMatrix.allMessages.Count)) / adjacencyMatrix.numUsers + Constants.MESSAGE_BASELINE;
            List<Tuple<double, string>> dividedScores = new List<Tuple<double, string>>();
            for (int i = 0; i < adjacencyMatrix.numUsers; i++)
            {
                weightedScores.Add(new Tuple<double, string>(scores[i].Item1 * weightFactor / (adjacencyMatrix.messagesPerUser[i] + Constants.MESSAGE_BASELINE), scores[i].Item2));
                System.Console.WriteLine(scores[i].Item1 + " " + weightFactor + " " + (adjacencyMatrix.messagesPerUser[i] + Constants.MESSAGE_BASELINE));
            }
            double divideFactor = 0;
            for (int i=0; i<adjacencyMatrix.numUsers; i++)
            {
                divideFactor += weightedScores[i].Item1;
            }
            for (int i=0; i<adjacencyMatrix.numUsers; i++)
            {
                dividedScores.Add(new Tuple<double, string>(100.0 * weightedScores[i].Item1 / divideFactor, weightedScores[i].Item2));
            }
            return dividedScores;
        }
    }
}
