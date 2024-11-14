using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame_MateCode
{
    public class Dice
    {
        public int[] Values { get; }

        public Dice(int[] Values)
        {
            this.Values = Values;
        }

        public int Roll(Random random)
        {
            return Values[random.Next(Values.Length)];
        }

        public override string ToString()
        {
            return string.Join(",", Values);
        }
    }
}
