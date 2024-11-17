using DiceGame_MateCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class ProbabilityCalculator
    {
        public void PrintProbabilityTable(List<Dice> diceList)
        {
            Console.WriteLine("\nProbability Table:");
            Console.WriteLine("------------------------------");

            for (int i = 0; i < diceList.Count; i++)
            {
                Console.Write($"Dice {i + 1}:  ");
                for (int j = 0; j < diceList.Count; j++)
                {
                    if (i == j)
                    {
                        Console.Write("   -   ");
                    }
                    else
                    {
                        double probability = CalculateWinningProbability(diceList[i], diceList[j]);
                        Console.Write($"{probability:P2} ");
                    }
                }
                Console.WriteLine();
            }
        }

        private double CalculateWinningProbability(Dice dice1, Dice dice2)
        {
            int wins = 0, losses = 0;
            foreach (var roll1 in dice1.Values)
            {
                foreach (var roll2 in dice2.Values)
                {
                    if (roll1 > roll2) wins++;
                    else if (roll1 < roll2) losses++;
                }
            }
            return (double)wins / (wins + losses);
        }
    }
}
