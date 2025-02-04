namespace TxtRPG1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player = new Character("chad");
            Item[] items = {
                new Item("수련자　갑옷", Item.Type.armor, 5, "수련에　도움을　주는　갑옷입니다.", 1000),
                new Item("무쇠갑옷", Item.Type.armor, 9, "무쇠로　만들어져　튼튼한　갑옷입니다.", 2000),
                new Item("스파르타의　갑옷", Item.Type.armor, 15, "스파르타의　전사들이　사용했다는 전설의　갑옷입니다.", 3500),
                new Item("낡은　검", Item.Type.weapon, 2, "쉽게　볼　수　있는　낡은　검　입니다.", 600),
                new Item("청동　도끼", Item.Type.weapon, 5, "어디선가　사용됐던거　같은　도끼입니다.", 1500),
                new Item("스파르타의　창", Item.Type.weapon, 7, "스파르타의　전사들이　사용했다는　전설의　창입니다.", 3000)
            };
            Shop shop = new Shop(player, items);

            //게임 시작
            byte choice;
            do
            {
                StartScene(out choice);
                switch (choice)
                {
                    case 1:
                        player.ShowStat(out choice);
                        break;
                    case 2:
                        player.Inventory(out choice);
                        break;
                    case 3:
                        shop.ShopEnter(out choice);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        Console.ReadKey();
                        break;
                }
            } while (true);
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