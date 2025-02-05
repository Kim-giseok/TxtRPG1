using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG1
{
    [Serializable]
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
            Console.WriteLine();
            //판매일 경우 플래이어가 소유중인 아이템을 보여줍니다.
            if (mode == Mode.Sell)
            { Player.ShowItems(Character.Mode.Sell); return; }

            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Items.Length; i++)
            {
                Console.Write("- ");
                //구매 시 선택번호 표시
                if (mode == Mode.Buy)
                { Console.Write($"{i + 1} "); }

                Console.Write($"{Items[i]}\t| ");
                //가격 및 판매여부 표시
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
                        SellItem(out choice);
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
                try
                {
                    //고른 아이템을 구매합니다.
                    if (Items[choice - 1].Bought) //이미 구매했을 경우
                    { Console.WriteLine("이미 구매한 아이템입니다."); }
                    else if (Items[choice - 1].Price <= Player.Gold) //금액이 충분한 경우
                    {
                        Player.BuyItem(Items[choice - 1]);
                        Items[choice - 1].Bought = true;
                        Console.WriteLine("구매를 완료했습니다.");
                    }
                    else //금액이 부족한 경우
                    { Console.WriteLine("Gold 가 부족합니다."); }
                    Thread.Sleep(500);
                }
                catch (IndexOutOfRangeException ex)
                { Program.WrongSelectDisplay(); }
            } while (true);
        }

        public void SellItem(out byte choice)
        {
            do
            {
                Console.Clear();

                Console.WriteLine("상점 - 아이템 판매");
                //보유골드와 자신의 아이템들을 보여줍니다.
                ShowItems(Mode.Sell);
                Console.WriteLine("0. 나가기");
                if (Program.Choice(out choice) && choice == 0)
                { break; }
                try
                {
                    Player.SellItem(choice - 1);
                    Console.WriteLine("판매가 완료되었습니다.");
                    Thread.Sleep(500);
                }
                catch (ArgumentOutOfRangeException ex)
                { Program.WrongSelectDisplay(); }
            } while (true);
        }
    }
}
