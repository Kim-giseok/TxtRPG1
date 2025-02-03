using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG1
{
    internal class Character
    {
        int level;
        string name;
        enum Class { 전사, 궁수, 마법사 };
        Class job;
        int atk, def, hp, gold;

        List<Item> items = new List<Item>();

        public Character(string name)
        {
            this.name = name;
            level = 1;
            job = Class.전사;
            atk = 10;
            def = 5;
            hp = 100;
            gold = 1500;
        }

        public bool ShowStat(out byte choice)
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {level:D2}");
            Console.WriteLine($"{name} ({job})");
            Console.WriteLine($"공격력 : {atk}");
            Console.WriteLine($"방어력 : {def}");
            Console.WriteLine($"체력 : {hp}");
            Console.WriteLine($"Gold : {gold}");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            return Program.Choice(out choice);
        }

        public bool Inventory(out byte choice)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            foreach (Item item in items)
            {
                Console.Write($"- {item}");
            }
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            return Program.Choice(out choice);
        }
    }
}
