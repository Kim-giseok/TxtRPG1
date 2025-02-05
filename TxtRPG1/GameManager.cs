using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG1
{
    internal class GameManager
    {
        public Character Player { get; }
        public Shop Shop { get; }
        public Dungeon[] Dungeons { get; }
        public int price { get; }

        public GameManager(Character player, Shop shop, Dungeon[] dungeons)
        {
            Player = player;
            Shop = shop;
            Dungeons = dungeons;
            price = 500;
        }

        public void StartScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("6. 저장하기");
                Console.WriteLine("0. 종료하기");

                if (Program.Choice(out byte choice) && choice == 0)
                { break; }
                switch (choice)
                {
                    case 1:
                        Player.ShowStat();
                        break;
                    case 2:
                        Player.Inventory();
                        break;
                    case 3:
                        Shop.ShopEnter();
                        break;
                    case 4:
                        DungeonEntrance();
                        break;
                    case 5:
                        Rest();
                        break;
                    case 6:
                        Program.Save(Shop, "save.json");
                        break;
                    default:
                        Program.WrongSelectDisplay();
                        break;
                }
            }
        }

        void DungeonEntrance()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("던전입장");
                Console.WriteLine($"이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();

                if (Player.Hp <= 0) //체력이 0이면 던전에 들어가지 못하게 하기
                {
                    Console.WriteLine("체력이 없어 던전에 들어갈 수 없습니다.");
                    Thread.Sleep(1000);
                    break;
                }
                //던전 보여주기
                for (int i = 0; i < Dungeons.Length; i++)
                { Console.WriteLine($"{i + 1}. {Dungeons[i]}"); }
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out byte choice) && choice == 0)
                { break; }
                try
                { Dungeons[choice - 1].Enter(Player); }
                catch (IndexOutOfRangeException ex)
                { Program.WrongSelectDisplay(); }
            }
        }

        void Rest()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("휴식하기");
                Console.WriteLine($"{price} G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {Player.Gold} G)");
                Console.WriteLine();
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out byte choice) && choice == 0)
                { break; }
                switch (choice)
                {
                    case 1:
                        if (Player.Hp == 100)
                        { Console.WriteLine("이미 충분히 쉬었습니다."); }
                        else if (Player.Gold >= price)
                        {
                            Player.Rest(price);
                            Console.WriteLine("휴식을 완료했습니다.");
                        }
                        else
                        { Console.WriteLine("Gold가 부족합니다."); }
                        Thread.Sleep(500);
                        break;
                    default:
                        Program.WrongSelectDisplay();
                        break;
                }
            }
        }
    }
}
