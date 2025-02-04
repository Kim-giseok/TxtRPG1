﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG1
{
    [Serializable]
    internal class Character
    {
        public int Level { get; private set; }
        public string Name { get; }
        public enum Class { 전사, 궁수, 마법사 };
        public Class job { get; }
        public float Atk { get; private set; }
        public int Def { get; private set; }
        public int Hp { get; private set; }
        public int Gold { get; private set; }
        public int Exp { get; private set; }

        public List<Item> Items { get; private set; }
        public int weapon { get; }
        public int armor { get; }
        public int?[] equips { get; private set; } //장착한 아이템을 보여주는 변수(armor, weapon)

        public Character(string name)
        {
            Name = name;
            Level = 1;
            job = Class.전사;
            Atk = 10;
            Def = 5;
            Hp = 100;
            Gold = 1500;
            Exp = 0;
            Items = new List<Item>();
            weapon = (int)Item.Type.weapon;
            armor = (int)Item.Type.armor;
            equips = [null, null];
        }

        public void ShowStat(out byte choice)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv. {Level:D2}");
                Console.WriteLine($"{Name} ({job})");

                Console.Write($"공격력 : {Atk}");
                if (equips[weapon] != null)
                { Console.WriteLine($" (+{Items[(int)equips[weapon]].Stat})"); }
                else
                { Console.WriteLine(); }

                Console.Write($"방어력 : {Def}");
                if (equips[armor] != null)
                { Console.WriteLine($" (+{Items[(int)equips[armor]].Stat})"); }
                else
                { Console.WriteLine(); }


                Console.WriteLine($"체력 : {Hp}");
                Console.WriteLine($"Gold : {Gold} G");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out choice) && choice == 0)
                { break; }
                Program.WrongSelectDisplay();
            } while (true);
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
            foreach (Item item in Items)
            {
                Console.Write("- ");
                //장착 관리/판매 시 선택번호 표시
                if (mode == Mode.Equip || mode == Mode.Sell)
                { Console.Write($"{Items.IndexOf(item) + 1} "); }
                //장착여부 표시
                if ((equips[(int)item.ItemType] != null &&
                    Items[(int)equips[(int)item.ItemType]] == item))
                { Console.Write("[E]"); }

                Console.Write(item);
                //판매시 판매가 표시
                if (mode == Mode.Sell)
                { Console.WriteLine($"\t| {item.Price * 85 / 100} G"); }
                else
                { Console.WriteLine(); }
            }
            Console.WriteLine();
        }

        public void Inventory(out byte choice)
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
                        Program.WrongSelectDisplay();
                        break;
                }
            } while (true);
        }

        public void Equip(out byte choice)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착관리");
                ShowItems(Mode.Equip);
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out choice) && choice == 0)
                { break; }
                try
                {
                    int type = Items[choice - 1].ItemType == Item.Type.armor ? 0 : 1;
                    //장착해제
                    if (equips[type] != null)
                    {
                        if (type == 0)
                        { Def -= Items[(int)equips[type]].Stat; }
                        else
                        { Atk -= Items[(int)equips[type]].Stat; }

                        if (equips[type] == choice - 1)
                        { equips[type] = null; break; }
                    }
                    //장착
                    if (type == 0)
                    { Def += Items[choice - 1].Stat; }
                    else
                    { Atk += Items[choice - 1].Stat; }
                    equips[type] = choice - 1;
                }
                catch (ArgumentOutOfRangeException ex)
                { Program.WrongSelectDisplay(); }
            } while (choice != 0);
        }

        public void BuyItem(Item item)
        {
            Gold -= item.Price;
            Items.Add(item);
        }

        public void SellItem(int idx)
        {
            if (equips[(int)Items[idx].ItemType] == idx)//장착 해제
            {
                if (Items[idx].ItemType == Item.Type.armor)
                { Def -= Items[idx].Stat; }
                else
                { Atk -= Items[idx].Stat; }
                equips[(int)Items[idx].ItemType] = null;
            }
            Gold += Items[idx].Price * 85 / 100;
            Items[idx].Bought = false;
            Items.RemoveAt(idx);
        }

        public void GetDamage(int damage)
        { Hp -= damage <= Hp ? damage : Hp; }

        public void Reward(int reward)
        {
            Gold += reward;
            Gold += reward * new Random().Next((int)Atk*10, (int)Atk*20) / 1000;

            Exp++;
            if (Exp == Level)
            {
                Level++;
                Atk += 0.5f;
                Def++;
                Exp = 0;
            }
        }

        public void Rest(int price)
        {
            Gold -= price;
            Hp = 100;
        }
    }
}
