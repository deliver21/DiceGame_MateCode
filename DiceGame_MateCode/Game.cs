using DiceGame;
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
        private HmacGenerator _hmacGen = new();
        private readonly ProbabilityCalculator _probabilityCalculator;

        public Game(List<Dice> diceList)
        {
            _diceList = diceList;
            _probabilityCalculator = new ProbabilityCalculator();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Dice Game!");
            Console.WriteLine("Type 'X' to exit, '?' for help.");

            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("0 - Play the game");
                Console.WriteLine("? - Show probability table");
                Console.WriteLine("X - Exit");

                string input = Console.ReadLine()?.Trim().ToUpper();

                switch (input)
                {
                    case "?":
                        ShowProbabilityTable();
                        break;

                    case "0":
                        Play();
                        break;

                    case "X":
                        Console.WriteLine("Exiting game. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please enter '0', '?', or 'X'.");
                        break;
                }
            }
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
                Console.WriteLine("You guessed it correctly! You go first");
            }
            else
            {
                Console.WriteLine("I go first.");
            }
        }

        private void ShowProbabilityTable()
        {
            _probabilityCalculator.PrintProbabilityTable(_diceList);
        }
    }
}