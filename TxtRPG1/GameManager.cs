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
        public byte choice;

        public GameManager(Character player, Shop shop)
        {
            Player = player;
            Shop = shop;
        }

        public void StartScene()
        {
            do
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

                Program.Choice(out choice);
                switch (choice)
                {
                    case 1:
                        Player.ShowStat(out choice);
                        break;
                    case 2:
                        Player.Inventory(out choice);
                        break;
                    case 3:
                        Shop.ShopEnter(out choice);
                        break;
                    case 4:
                        break;
                    case 5:
                        Rest(out choice);
                        break;
                    default:
                        Program.WrongSelectDisplay();
                        break;
                }
            } while (true);
        }

        void Rest(out byte choice)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("휴식하기");
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {Player.Gold} G)");
                Console.WriteLine();
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out choice) && choice == 0)
                { break; }
                switch (choice)
                {
                    case 1:
                        if (Player.Gold >= 500)
                        {
                            Player.Rest(500);
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
            } while (true);
        }
    }
}
