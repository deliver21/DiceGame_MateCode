using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame_MateCode
{
    public class DiceParser
    {
        public static List<Dice> ParseDice(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException($"You must provide at least 3 dice attempts. Invalid dice number {args.Length}");
            }

            var diceList = new List<Dice>();

            foreach (var arg in args)
            {                
                string[] stringValues = arg.Split(',');
                int[] values = new int[6];

                if (stringValues.Length != 6)
                {
                    throw new ArgumentException($"Each dice must have exactly 6 integers. Invalid input: {arg}");
                }

                for (int i = 0; i < stringValues.Length; i++)
                {
                    if (!int.TryParse(stringValues[i], out values[i]))
                    {
                        throw new ArgumentException($"Invalid integer value '{stringValues[i]}' in input: {arg}");
                    }
                }

                diceList.Add(new Dice(values));
            }

            return diceList;
        }
    }


}
