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

        public void ShowStat()
        {
            Console.Clear();
            Console.WriteLine($"Lv. {level:D2}");
            Console.WriteLine($"{name} ({job})");
            Console.WriteLine($"공격력 : {atk}");
            Console.WriteLine($"방어력 : {def}");
            Console.WriteLine($"체력 : {hp}");
            Console.WriteLine($"Gold : {gold}");
            Console.WriteLine();
            Console.WriteLine($"0. 나가기");
        }
    }
}
