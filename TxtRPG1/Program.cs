using System.Net.Http.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TxtRPG1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //게임 시작 준비
            string jsonString;

            Character player;
            Item[] items;
            player = new Character(Console.ReadLine());

            items =
            [
                new Item("수련자　갑옷", Item.Type.armor, 5, "수련에　도움을　주는　갑옷입니다.", 1000),
                new Item("무쇠갑옷", Item.Type.armor, 9, "무쇠로　만들어져　튼튼한　갑옷입니다.", 2000),
                new Item("스파르타의　갑옷", Item.Type.armor, 15, "스파르타의　전사들이　사용했다는 전설의　갑옷입니다.", 3500),
                new Item("낡은　검", Item.Type.weapon, 2, "쉽게　볼　수　있는　낡은　검　입니다.", 600),
                new Item("청동　도끼", Item.Type.weapon, 5, "어디선가　사용됐던거　같은　도끼입니다.", 1500),
                new Item("스파르타의　창", Item.Type.weapon, 7, "스파르타의　전사들이　사용했다는　전설의　창입니다.", 3000)
            ];
            Shop shop = new Shop(player, items);

            Dungeon[] dungeons =
            {
                new Dungeon(Dungeon.Level.쉬운, 1000, 5),
                new Dungeon(Dungeon.Level.일반, 1700, 11),
                new Dungeon(Dungeon.Level.어려운, 2500, 17)
            };

            GameManager game = new GameManager(player, shop, dungeons);
            //게임 시작
            while (true)
            {
                if (!game.StartScene())
                { break; }
                Thread.Sleep(500);
            }
        }

        public static bool Choice(out byte choice)
        {
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            return byte.TryParse(Console.ReadLine(), out choice);
        }

        public static void WrongSelectDisplay()
        {
            Console.WriteLine("잘못된 입력입니다");
            Thread.Sleep(500);
        }
    }
}