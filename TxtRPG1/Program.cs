using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TxtRPG1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //게임 시작 준비
            Character player;
            Item[] items =
            {
                    new Item("수련자　갑옷", Item.Type.armor, 5, "수련에　도움을　주는　갑옷입니다.", 1000),
                    new Item("무쇠갑옷", Item.Type.armor, 9, "무쇠로　만들어져　튼튼한　갑옷입니다.", 2000),
                    new Item("스파르타의　갑옷", Item.Type.armor, 15, "스파르타의　전사들이　사용했다는 전설의　갑옷입니다.", 3500),
                    new Item("낡은　검", Item.Type.weapon, 2, "쉽게　볼　수　있는　낡은　검　입니다.", 600),
                    new Item("청동　도끼", Item.Type.weapon, 5, "어디선가　사용됐던거　같은　도끼입니다.", 1500),
                    new Item("스파르타의　창", Item.Type.weapon, 7, "스파르타의　전사들이　사용했다는　전설의　창입니다.", 3000)
            };
            string name;

            try
            { Load(out player, items, "save.json"); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                do
                {
                    Console.Clear();
                    Console.WriteLine("스파르타 던전에 오신 것을 환영합니다.");
                    Console.WriteLine("당신의 이름을 입력해 주세요");
                    Console.WriteLine();
                } while ((name = Console.ReadLine()) == "");
                player = new Character(name);
            }

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
                Save(shop, "save.json");
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

        public static void Save(Shop shop, string path)
        {
            var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            string jString = JsonSerializer.Serialize(shop, options);

            File.WriteAllText(path, jString);
            Console.WriteLine("저장 완료");
            Thread.Sleep(500);
        }

        public static void Load(out Character player, Item[] items, string path)
        {
            string jString = File.ReadAllText(path);

            string[] objs = jString.Split("},\"Items\":");

            ////아이템 복구하기 - 아이템의 판매여부 변경
            //1. 아이템 객체별로 문자열 분리
            string[] iString = objs[1].Replace("[{", "").Replace("}]", "").Split("},{");
            //. 각 배열의 판매여부 변경
            for (int i = 0; i < iString.Length; i++)
            { items[i].Bought = (iString[i].Split(",")[5].Split(":")[1] == "true"); ; }

            ////플레이어 복구하기
            player = new Character(objs[0].Replace("{\"Player\":{", ""), items);
        }
    }
}