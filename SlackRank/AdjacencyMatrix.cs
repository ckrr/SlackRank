using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SlackRank
{
    class AdjacencyMatrix
    {
        private int numUsers;
        private List<User> allUsers;
        private List<Message> allMessages;
        private Hashtable usersToIndex;
        private List<List<int>> reactionMatrix;
        private List<List<int>> replyMatrix;

        public AdjacencyMatrix(List<User> inputAllUsers, List<Message> inputAllMessages)
        {
            numUsers = inputAllUsers.Count;
            allUsers = inputAllUsers;
            allMessages = inputAllMessages;
            usersToIndex = new Hashtable();
            reactionMatrix = new List<List<int>>();
            replyMatrix = new List<List<int>>();
            ConstructUsersToIndex();
            InitializeMatrices();
            FillMatrices();
        }

        public List<List<int>> GetReactionMatrix()
        {
            return reactionMatrix;
        }

        public List<List<int>> GetReplyMatrix()
        {
            return replyMatrix;
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
                for (int j = 0; j < numUsers; j++)
                {
                    reactionRow.Add(0);
                }
                reactionMatrix.Add(reactionRow);
            }
            for (int i = 0; i < numUsers; i++)
            {
                List<int> replyRow = new List<int>();
                for (int j = 0; j < numUsers; j++)
                {
                    replyRow.Add(0);
                }
                replyMatrix.Add(replyRow);
            }
        }

        private void FillMatrices()
        {
            foreach (Message message in allMessages)
            {
                try
                {
                    int senderId = (int)usersToIndex[message.user];
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
        }
    }
}
