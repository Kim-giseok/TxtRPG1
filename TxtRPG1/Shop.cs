using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG1
{
    internal class Shop
    {
        public Character Player { get; }
        public Item[] Items { get; private set; }

        public Shop(Character player, Item[] items)
        {
            Player = player;
            Items = items;
        }

        public enum Mode { Idle, Buy, Sell }
        void ShowItems(Mode mode)
        {
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Gold} G");

            if (mode == Mode.Sell)
            { Player.ShowItems(Character.Mode.Sell); return; }

            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Items.Length; i++)
            {
                Console.Write("- ");
                //구매 시 선택번호 표시
                if (mode == Mode.Buy)
                { Console.Write($"{i} "); }

                Console.Write($"{Items[i]}\t| ");
                if (Items[i].Bought)
                { Console.WriteLine("구매완료"); }
                else
                { Console.WriteLine($"{Items[i].Price} G"); }
            }
            Console.WriteLine();
        }

        public void ShopEnter(out byte choice)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상점");
                //보유골드와 상점의 아이템들을 보여줍니다.
                ShowItems(Mode.Idle);
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");


                if (Program.Choice(out choice) && choice == 0)
                { break; }
                switch (choice)
                {
                    case 1:
                        BuyItem(out choice);
                        break;
                    case 2:
                        //SellItem(out choice);
                        break;
                    default:
                        Program.WrongSelectDisplay();
                        break;
                }
            } while (true);
        }

        public void BuyItem(out byte choice)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매");
                //보유골드와 상점의 아이템들을 보여줍니다.
                ShowItems(Mode.Buy);
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out choice) && choice == 0)
                { break; }
                if (0 < choice && choice <= Items.Length)
                { 
                    //고른 아이템을 구매합니다.
                }
                else
                { Program.WrongSelectDisplay(); }
            } while (true);
        }
    }
}
