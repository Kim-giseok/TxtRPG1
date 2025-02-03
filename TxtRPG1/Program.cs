namespace TxtRPG1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player = new Character("chad");

            byte choice;
        START:
            while (!StartScene(out choice) && 1 <= choice && choice <= 3)
            {
                Console.WriteLine("잘못된 입력입니다");
                Console.ReadKey();
            }

            switch (choice)
            {
                case 1:
                    player.ShowStat(out choice);
                    break;
                case 2:
                    player.Inventory(out choice);
                    break;
                case 3:
                    break;
            }
            if (choice == 0)
            { goto START; }
        }

        public static bool StartScene(out byte choice)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");

            return Choice(out choice);
        }
        public static bool Choice(out byte choice)
        {
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            return byte.TryParse(Console.ReadLine(), out choice);
        }
    }
}