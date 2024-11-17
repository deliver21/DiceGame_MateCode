namespace DiceGame_MateCode
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var diceList = DiceParser.ParseDice(args);
                var game = new Game(diceList);
                game.Play();               
                Console.WriteLine("Thank you for playing!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Example usage: DiceGame.exe 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}