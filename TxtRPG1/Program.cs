namespace TxtRPG1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Character player = new Character("chad");
            player.ShowStat();
        }
    }
}