using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG1
{
    internal class Character
    {
        public int Level { get; private set; }
        public string Name { get; private set; }
        public enum Class { 전사, 궁수, 마법사 };
        public Class job { get; private set; }
        public int Atk { get; private set; }
        public int Def { get; private set; }
        public int Hp { get; private set; }
        public int Gold { get; private set; }

        List<Item> items = new List<Item>();
        int weapon = (int)Item.Type.weapon, armor = (int)Item.Type.armor;
        int?[] equips = { null, null }; //장착한 아이템을 보여주는 변수(armor, weapon)

        public Character(string name)
        {
            Name = name;
            Level = 1;
            job = Class.전사;
            Atk = 10;
            Def = 5;
            Hp = 100;
            Gold = 1500;
        }

        public bool ShowStat(out byte choice)
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {Level:D2}");
            Console.WriteLine($"{Name} ({job})");

            Console.Write($"공격력 : {Atk}");
            if (equips[weapon] != null)
            { Console.WriteLine($" (+{items[(int)equips[weapon]].Stat})"); }
            else
            { Console.WriteLine(); }

            Console.Write($"방어력 : {Def}");
            if (equips[armor] != null)
            { Console.WriteLine($" (+{items[(int)equips[armor]].Stat})"); }
            else
            { Console.WriteLine(); }


            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            return Program.Choice(out choice);
        }

        public enum Mode { Inventory, Equip, Sell };
        public void ShowItems(Mode mode)
        {
            if (mode != Mode.Sell)
            {
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
            }
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            foreach (Item item in items)
            {
                Console.Write("- ");
                //장착 관리 시 선택번호 표시
                if (mode == Mode.Equip)
                { Console.Write($"{items.IndexOf(item) + 1} "); }
                //장착여부 표시
                if ((equips[(int)item.ItemType] != null &&
                    items[(int)equips[(int)item.ItemType]] == item))
                { Console.Write("[E]"); }

                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        public bool Inventory(out byte choice)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                ShowItems(Mode.Inventory);
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out choice) && choice == 0)
                { break; }
                switch (choice)
                {
                    case 1:
                        Equip(out choice);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        Console.ReadKey();
                        break;
                }
            } while (true);

            return true;
        }

        public bool Equip(out byte choice)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착관리");
                ShowItems(Mode.Equip);
                Console.WriteLine("0. 나가기");


                if (Program.Choice(out choice) && choice == 0)
                { break; }
                if (choice <= items.Count)
                {
                    int type = items[choice - 1].ItemType == Item.Type.armor ? 0 : 1;
                    //장착해제
                    if (equips[type] != null)
                    {
                        if (type == 0)
                        { Def -= items[(int)equips[type]].Stat; }
                        else
                        { Atk -= items[(int)equips[type]].Stat; }

                        if (equips[type] == choice - 1)
                        { equips[type] = null; break; }
                    }
                    //장착
                    if (type == 0)
                    { Def += items[choice - 1].Stat; }
                    else
                    { Atk += items[choice - 1].Stat; }
                    equips[type] = choice - 1;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다");
                    Console.ReadKey();
                }
            } while (choice != 0);
            return true;
        }

        public void GetItem(Item item)
        {
            items.Add(item);
        }
    }
}
