using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SlackRank
{
    class AdjacencyMatrix
    {
        public int numUsers;
        public List<User> allUsers;
        public List<Message> allMessages;
        public Hashtable usersToIndex;
        public List<List<int>> reactionMatrix;
        public List<List<int>> replyMatrix;
        public List<List<int>> combinedMatrix;
        public List<int> messagesPerUser;

        public AdjacencyMatrix(List<User> inputAllUsers, List<Message> inputAllMessages)
        {
            numUsers = inputAllUsers.Count;
            allUsers = inputAllUsers;
            allMessages = inputAllMessages;
            usersToIndex = new Hashtable();
            reactionMatrix = new List<List<int>>();
            replyMatrix = new List<List<int>>();
            combinedMatrix = new List<List<int>>();
            messagesPerUser = new List<int>();
            ConstructUsersToIndex();
            InitializeMatrices();
            FillMatrices();
            RemoveInactiveUsers();
        }

        public List<List<Tuple<int,double>>> GetWeightedAdjacencyList()
        {
            List<List<Tuple<int, double>>> weightedAdjacencyList = new List<List<Tuple<int, double>>>();
            List<double> countActionsPerUser = new List<double>();
            for (int i=0; i<numUsers; i++)
            {
                weightedAdjacencyList.Add(new List<Tuple<int, double>>());
                countActionsPerUser.Add(0);
            }
            for (int i=0; i<numUsers; i++)
            {
                countActionsPerUser[i] = (double) combinedMatrix[i].Sum();
            }
            for (int i=0; i<numUsers; i++)
            {
                for (int j=0; j<numUsers; j++)
                {
                    if (combinedMatrix[i][j] > 0)
                    {
                        double weightedValue = ((double)combinedMatrix[i][j]) / countActionsPerUser[i];
                        weightedAdjacencyList[i].Add(new Tuple<int, double>(j, weightedValue));
                    }
                }
            }
            return weightedAdjacencyList;
        }

        private void ConstructUsersToIndex()
        {
            for (int i=0; i<numUsers; i++)
            {
                usersToIndex.Add(allUsers[i].id, i);
            }
        }

        private void InitializeMatrices()
        {
            for (int i = 0; i < numUsers; i++)
            {
                List<int> reactionRow = new List<int>();
                List<int> replyRow = new List<int>();
                List<int> combinedRow = new List<int>();
                for (int j = 0; j < numUsers; j++)
                {
                    reactionRow.Add(0);
                    replyRow.Add(0);
                    combinedRow.Add(0);
                }
                reactionMatrix.Add(reactionRow);
                replyMatrix.Add(replyRow);
                combinedMatrix.Add(combinedRow);
                messagesPerUser.Add(0);
            }
        }

        private void FillMatrices()
        {
            foreach (Message message in allMessages)
            {
                try
                {
                    int senderId = (int)usersToIndex[message.user];
                    messagesPerUser[senderId]++;
                    foreach (Reply reply in message.replies)
                    {
                        int replyId = (int)usersToIndex[reply.user];
                        replyMatrix[replyId][senderId]++;
                    }
                    foreach (Reaction reaction in message.reactions)
                    {
                        foreach (string user in reaction.users)
                        {
                            int reactionId = (int)usersToIndex[user];
                            reactionMatrix[reactionId][senderId]++;
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            for (int i=0; i<numUsers; i++)
            {
                for (int j=0; j<numUsers; j++)
                {
                    combinedMatrix[i][j] = Constants.REPLY_WEIGHT_FACTOR * replyMatrix[i][j] + reactionMatrix[i][j];
                }
            }
        }

        private void RemoveInactiveUsers()
        {
            List<int> activeUsers = new List<int>();
            for (int i=0; i<numUsers; i++)
            {
                if (combinedMatrix[i].Sum() > 0)
                {
                    activeUsers.Add(i);
                }
            }
            numUsers = activeUsers.Count;
            List<List<int>> combinedMatrixNew = new List<List<int>>();
            List<User> allUsersNew = new List<User>();
            for (int i=0; i<numUsers; i++)
            {
                allUsersNew.Add(allUsers[activeUsers[i]]);
                List<int> combinedRow = new List<int>();
                for (int j=0; j<numUsers; j++)
                {
                    combinedRow.Add(combinedMatrix[activeUsers[i]][activeUsers[j]]);
                }
                combinedMatrixNew.Add(combinedRow);
            }
            combinedMatrix = combinedMatrixNew;
            allUsers = allUsersNew;
        }
    }
}
