using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame_MateCode
{
    public class Game
    {
        private List<Dice> _diceList;
        private Random _random = new Random();
        private HmacGenerator _hmacGen = new();

        public Game(List<Dice> diceList)
        {
            _diceList = diceList;
        }

        public void Play()
        {
            Console.WriteLine("Let's determine which makes the first move.");
            int computerChoice = _hmacGen.GenerateSecureRandom(2);
            string hmac = _hmacGen.GenerateHmac(computerChoice);

            Console.WriteLine($"HMAC={hmac}");
            Console.WriteLine("Try to guess my selection.");
            Console.WriteLine("0 - 0\n1 - 1\nX - exit\n? - help");

            string userInput = Console.ReadLine();
            if (userInput == "X") return;

            int userChoice = int.Parse(userInput);

            Console.WriteLine($"My selection: {computerChoice} (KEY={_hmacGen.RevealKey()})");
            if(userChoice == computerChoice)
            {
                Console.WriteLine("You guessed it correctly! You go first")
            }
            else
            {
                Console.WriteLine("I go first.");
            }
        }
    }
}