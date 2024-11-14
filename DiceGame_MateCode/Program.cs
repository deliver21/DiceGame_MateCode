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
                game.Start();
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Example usage: DiceGame.exe 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
            }
        }
    }
}