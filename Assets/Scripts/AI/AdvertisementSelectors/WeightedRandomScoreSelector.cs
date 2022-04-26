using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ORCAS
{
    public class WeightedRandomScoreSelector : MonoBehaviour, IAdvertisementSelector
    {
        [SerializeField, Tooltip("Will choose between top n high-scoring")] 
        private int n = 3;

        public Advertisement[] Select(Agent agent, float[] scores, List<Advertisement[]> advertisements)
        {
            SortedList<float, int> sortedScores = new SortedList<float, int>((scores.Length));

            int repetitions = 0;
            for (int i = 0; i < scores.Length; i++)
            {
                try
                {
                    sortedScores.Add(scores[i], i);
                }
                catch
                {
                    repetitions++;
                    scores[i] += 0.000000001f * repetitions;
                    i--;
                    if (repetitions > n) break;
                }
            }

            int length = n < scores.Length ? n : scores.Length;
            
            float total = SumTopNScores(sortedScores.Keys, length);
            int selectedIndex = MakeGuess(sortedScores, total, length);

            return advertisements[selectedIndex];

            int MakeGuess(SortedList<float, int> sortedScores, float total, int length)
            {
                float randomNum = Random.value;
                int selectedIndex = sortedScores.Values.Last();
                float acc = 0f;
                for (int i = sortedScores.Count - length; i < sortedScores.Count; i++)
                {
                    acc += (sortedScores.Keys[i] / total);
                    if (randomNum <= acc)
                    {
                        selectedIndex = sortedScores.Values[i];
                        break;
                    }
                }

                return selectedIndex;
            }

            float SumTopNScores(IList<float> scores, int n)
            {
                float total = 0f;
                for (int i = scores.Count - 1; i >= scores.Count - n; i--)
                {
                    total += scores[i];
                }

                return total;
            }
        }
    }
}