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
        private readonly List<Dice> diceList;
        private readonly HmacGenerator hmacGen;
        private readonly ProbabilityCalculator probability;

        public Game(List<Dice> diceList)
        {
            this.diceList = diceList;
            hmacGen = new HmacGenerator();
            probability = new ProbabilityCalculator();
        }

        public void Play()
        {
            Dice playerDice , computerDice;
            int computerChoice = GenerateRandomChoice();
            bool playerGoesFirst = DetermineFirstMove(computerChoice);
            if(playerGoesFirst)
            {
                playerDice = SelectPlayerDice(null);
                computerDice = SelectComputerDice(playerDice);
            }
            else
            {
                computerDice = SelectComputerDice(null);
                playerDice = SelectPlayerDice(computerDice);
            }          

            PlayRound(playerDice, computerDice, playerGoesFirst);
        }

        private int GenerateRandomChoice()
        {
            int choice = hmacGen.GenerateSecureRandom(2);
            string hmac = hmacGen.GenerateHmac(choice);
            Console.WriteLine($"I selected a random value in the range 0..1.\nHMAC={hmac}");
            return choice;
        }

        private bool DetermineFirstMove(int computerChoice)
        {
            int userChoice = GetUserGuess();
            Console.WriteLine($"My selection: {computerChoice} (KEY={hmacGen.RevealKey()})");
            bool playerFirst = userChoice == computerChoice;
            Console.WriteLine(playerFirst ? "You go first!" : "I go first.");
            return playerFirst;
        }

        private int GetUserGuess()
        {
            Console.WriteLine("Try to guess my selection.\n0 - 0\n1 - 1\nX - exit\n? - help");
            Console.Write("Your selection : ");
            string input = Console.ReadLine();
            if (input == "?")
            {
                probability.PrintProbabilityTable(diceList);
                GetUserGuess();
            }
            if (input.ToUpper() == "X") Environment.Exit(0);
            return int.TryParse(input, out int guess) && (guess == 0 || guess == 1) ? guess : -1;
        }

        private Dice SelectPlayerDice(Dice excludedDice)
        {
            Console.WriteLine("Choose your dice:");
            var availableDice = excludedDice == null
                ? diceList
                : diceList.Where(d => d != excludedDice).ToList();

            for (int i = 0; i < availableDice.Count; i++)
            {
                Console.WriteLine($"{i} - {string.Join(",", availableDice[i].Values)}");
            }

            int choice = GetUserInput("Your selection: ", availableDice.Count);
            Console.WriteLine($"You chose the dice: {string.Join(",", availableDice[choice].Values)}");
            return availableDice[choice];
        }

        private Dice SelectComputerDice(Dice excludedDice)
        {
            int choice;
            Dice computerDice;
            do
            {
                choice = hmacGen.GenerateSecureRandom(diceList.Count);
                computerDice = diceList[choice];
                Console.WriteLine($"I chose the dice: {string.Join(",", computerDice.Values)}");
            }
            while (diceList[choice] == excludedDice);
            
            return computerDice;
        }

        private void PlayRound(Dice playerDice, Dice computerDice, bool playerGoesFirst)
        {
            int computerThrow = 0, playerThrow = 0;

            if (!playerGoesFirst)
            {
                computerThrow = ComputerTurn(computerDice);
                playerThrow = PlayerTurn(playerDice);
            }
            else
            {
                playerThrow = PlayerTurn(playerDice);
                computerThrow = ComputerTurn(computerDice);
            }

            DisplayResult(playerThrow, computerThrow);
        }

        private int ComputerTurn(Dice dice)
        {
            Console.WriteLine("It's time for my throw.");
            int computerThrow = GenerateAndRevealComputerThrow();
            int playerNumber = GetPlayerInput();
            int result = (computerThrow + playerNumber) % 6;
            return DisplayComputerThrowResult(dice, computerThrow, playerNumber, result);
        }

        private int GenerateAndRevealComputerThrow()
        {
            int throwValue = hmacGen.GenerateSecureRandom(6);
            string hmac = hmacGen.GenerateHmac(throwValue);
            Console.WriteLine($"I selected a random value in the range 0..5 (HMAC={hmac})");
            return throwValue;
        }

        private int DisplayComputerThrowResult(Dice dice, int computerThrow, int playerNumber, int result)
        {
            Console.WriteLine($"My number is {computerThrow} (KEY={hmacGen.RevealKey()})");
            Console.WriteLine($"The result is {computerThrow} + {playerNumber} = {result} (mod 6)");
            int throwResult = dice.Values[result];
            Console.WriteLine($"My throw is {throwResult}");
            return throwResult;
        }

        private int PlayerTurn(Dice dice)
        {
            Console.WriteLine("It's time for your throw.");
            int computerNumber = GenerateAndRevealComputerThrow();
            int playerNumber = GetPlayerInput();
            int result = (computerNumber + playerNumber) % 6;
            return DisplayPlayerThrowResult(dice, computerNumber, playerNumber, result);
        }

        private int DisplayPlayerThrowResult(Dice dice, int computerNumber, int playerNumber, int result)
        {
            Console.WriteLine($"My number is {computerNumber} (KEY={hmacGen.RevealKey()})");
            Console.WriteLine($"The result is {computerNumber} + {playerNumber} = {result} (mod 6)");
            int throwResult = dice.Values[result];
            Console.WriteLine($"Your throw is {throwResult}");
            return throwResult;
        }

        private int GetPlayerInput()
        {
            return GetUserInput("Add your number modulo 6:\n0 - 0\n1 - 1\n2 - 2\n3 - 3\n4 - 4\n5 - 5\nX - exit\n? - help\nYour selection :", 6);
        }

        private int GetUserInput(string prompt, int maxValidValue)
        {
            Console.Write(prompt);
            //Console.Write("Your selection : ");
            string input = Console.ReadLine();
            if (input == "?")
            {
                probability.PrintProbabilityTable(diceList);
                GetUserInput(prompt, maxValidValue);
            }
            if (input?.ToUpper() == "X") Environment.Exit(0);
            return int.TryParse(input, out int choice) && choice >= 0 && choice < maxValidValue ? choice : 0;
        }

        private void DisplayResult(int playerThrow, int computerThrow)
        {
            if (playerThrow > computerThrow)
                Console.WriteLine($"You win ({playerThrow} > {computerThrow})!");
            else if (playerThrow < computerThrow)
                Console.WriteLine($"I win ({computerThrow} > {playerThrow})!");
            else
                Console.WriteLine("It's a tie!");
        }
    }
}