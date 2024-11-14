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
            if(args.Length<3)
            {
                throw new ArgumentException("You must provide at least 3 dice attempt");
            }

            var diceList= new List<Dice>();
            foreach( var arg in args)
            {
                var values=arg.Split(',').Select(int.Parse).ToArray();
                if (values.Length != 6)
                    throw new ArgumentException($"Each die must have exactly 6 integers. Invalid input: {arg}");

                diceList.Add(new Dice(values));
            }
            return diceList;
        }
    }
}
