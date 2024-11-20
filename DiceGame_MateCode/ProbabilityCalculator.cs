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
            Console.WriteLine("Probability of the win for the user:");

            PrintTableBorder(diceList.Count);
            PrintHeaderRow(diceList);
            PrintTableBorder(diceList.Count);

            for (int i = 0; i < diceList.Count; i++)
            {
                PrintRow(diceList, i);
                PrintTableBorder(diceList.Count);
            }
        }

        private void PrintTableBorder(int diceCount)
        {
            Console.WriteLine(new string('+', diceCount * 14 + 15));
        }

        private void PrintHeaderRow(List<Dice> diceList)
        {
            Console.Write("| User dice v  ");
            foreach (var dice in diceList)
            {
                Console.Write($"| {string.Join(",", dice.Values).PadRight(11)} ");
            }
            Console.WriteLine("|");
        }

        private void PrintRow(List<Dice> diceList, int rowIndex)
        {
            Console.Write($"| {string.Join(",", diceList[rowIndex].Values).PadRight(11)} ");
            for (int j = 0; j < diceList.Count; j++)
            {
                if (rowIndex == j)
                {
                    PrintDiagonalElement();
                }
                else
                {
                    double probability = CalculateWinningProbability(diceList[rowIndex], diceList[j]);
                    PrintProbability(probability);
                }
            }
            Console.WriteLine("|");
        }

        private void PrintDiagonalElement()
        {
            Console.Write($"| {"- (0.3333)".PadRight(11)} ");
        }

        private void PrintProbability(double probability)
        {
            Console.Write($"| {probability:F4}      ");
        }

        private double CalculateWinningProbability(Dice dice1, Dice dice2)
        {
            int wins = 0;
            foreach (var roll1 in dice1.Values)
            {
                foreach (var roll2 in dice2.Values)
                {
                    if (roll1 > roll2) wins++;
                }
            }
            return (double)wins / 36;
        }
    }
}
