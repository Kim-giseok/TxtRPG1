namespace TxtRPG1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player = new Character("chad");
            Item ironArmor = new Item("무쇠갑옷", Item.Type.armor, 5, "무쇠로 만들어져 튼튼한 갑옷입니다.");
            Item spartaSpear = new Item("스파르타의 창", Item.Type.weapon, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.");
            Item oldSword = new Item("낡은 검", Item.Type.weapon, 2, "쉽게 볼 수 있는 낡은 검 입니다.");

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
                    player.GetItem(ironArmor);
                    player.GetItem(spartaSpear);
                    player.GetItem(oldSword);
                    choice = 0;
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