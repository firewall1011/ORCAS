using System.Collections.Generic;
using UnityEngine;

namespace ORCAS
{
    public class BestScoreSelector : MonoBehaviour, IAdvertisementSelector
    {
        public Advertisement[] Select(Agent agent, float[] scores, List<Advertisement[]> advertisements)
        {
            int bestIndex = -1;
            float bestScore = float.MinValue;
            
            for(int i = 0; i < scores.Length; i++)
            {
                if (scores[i] >= bestScore)
                {
                    bestScore = scores[i];
                    bestIndex = i;
                }
            }

            return advertisements[bestIndex];
        }
    }
}
