using System;
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
        public Class Job { get; }
        public float Atk { get; private set; }
        public int Def { get; private set; }
        public int Hp { get; private set; }
        public int Gold { get; private set; }
        public int Exp { get; private set; }

        public List<Item> Items { get; private set; }
        public Dictionary<Item.Type, int?> Equips { get; private set; }

        public Character(string name)
        {
            Name = name;
            Level = 1;
            Job = Class.전사;
            Atk = 10;
            Def = 5;
            Hp = 100;
            Gold = 1500;
            Exp = 0;

            Items = new List<Item>();

            Equips = new Dictionary<Item.Type, int?>()
            {
                [Item.Type.armor] = null,
                [Item.Type.weapon] = null
            };
        }

        public Character(string save, Item[] items)
        {
            //1. 세이브 데이터를 보유아이템 앞쪽과 뒷쪽으로 나누기
            string[] info = save.Split("\"Items\":[{");
            //1-1. 앞쪽에서 능력치 필드값을 추출해 필드에 넣어주기
            string[] stats = info[0].Split(",");
            Level = int.Parse(stats[0].Split(":")[1]);
            Name = stats[1].Split(":")[1].Replace("\"", "");
            Job = (Class)int.Parse(stats[2].Split(":")[1]);
            Atk = float.Parse(stats[3].Split(":")[1]);
            Def = int.Parse(stats[4].Split(":")[1]);
            Hp = int.Parse(stats[5].Split(":")[1]);
            Gold = int.Parse(stats[6].Split(":")[1]);
            Exp = int.Parse(stats[7].Split(":")[1]);

            //2. 뒷쪽에서 보유 아이템들만 추출하기
            info = info[1].Split("}],");
            //2-1. 보유아이템들을 개체별로 분리하기
            string[] iStrings = info[0].Split("},{");
            //2-2. 개체의 이름값만 추출하기
            for (int i = 0; i < iStrings.Length; i++)
            { iStrings[i] = iStrings[i].Split(",")[0].Split(":")[1].Replace("\"", "").Replace("\\u3000", "　"); }
            //2-3. 아이템 배열에서 이름이 일치하는 아이템을 찾아서 리스트에 넣어주기
            Items = new List<Item>();
            foreach (string str in iStrings)
            {
                foreach (var item in items)
                {
                    if (item.Name == str)
                    { Items.Add(item); }
                }
            }

            //3. 추출하고 남은 부분에서 equips값 찾아서 넣어주기
            stats = info[1].Split("Equips\":{")[1].Replace("}", "").Split(",");
            stats[0] = stats[0].Split(":")[1];
            stats[1] = stats[1].Split(":")[1];

            Equips = new Dictionary<Item.Type, int?>()
            {
                [Item.Type.armor] = null,
                [Item.Type.weapon] = null
            };
            if (stats[0] != "null")
            { Equips[Item.Type.armor] = int.Parse(stats[0]); }
            if (stats[1] != "null")
            { Equips[Item.Type.weapon] = int.Parse(stats[1]); }
        }

        public void ShowStat()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv. {Level:D2}");
                Console.WriteLine($"{Name} ({Job})");

                Console.Write($"공격력 : {Atk}");
                if (Equips[Item.Type.weapon] != null)
                { Console.WriteLine($" (+{Items[(int)Equips[Item.Type.weapon]].Stat})"); }
                else
                { Console.WriteLine(); }

                Console.Write($"방어력 : {Def}");
                if (Equips[Item.Type.armor] != null)
                { Console.WriteLine($" (+{Items[(int)Equips[Item.Type.armor]].Stat})"); }
                else
                { Console.WriteLine(); }

                Console.WriteLine($"체력 : {Hp}");
                Console.WriteLine($"Gold : {Gold} G");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out byte choice) && choice == 0)
                { break; }
                Program.WrongSelectDisplay();
            }
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
                if ((Equips[item.ItemType] != null &&
                    Items[(int)Equips[item.ItemType]] == item))
                { Console.Write("[E]"); }
                //아이템정보 표시
                Console.Write(item);
                //판매시 판매가 표시
                if (mode == Mode.Sell)
                { Console.WriteLine($"\t| {item.Price * 85 / 100} G"); }
                else
                { Console.WriteLine(); }
            }
            Console.WriteLine();
        }

        public void Inventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                ShowItems(Mode.Inventory);
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out byte choice) && choice == 0)
                { break; }
                switch (choice)
                {
                    case 1:
                        Equip();
                        break;
                    default:
                        Program.WrongSelectDisplay();
                        break;
                }
            }
        }

        public void Equip()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착관리");
                ShowItems(Mode.Equip);
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out byte choice) && choice == 0)
                { break; }
                try
                {
                    Item.Type type = Items[choice - 1].ItemType;
                    //장착해제
                    if (Equips[Items[choice - 1].ItemType] != null)
                    {
                        if (type == Item.Type.armor)
                        { Def -= Items[(int)Equips[type]].Stat; }
                        else
                        { Atk -= Items[(int)Equips[type]].Stat; }

                        if (Equips[type] == choice - 1)
                        { Equips[type] = null; break; }
                    }
                    //장착
                    if (type == 0)
                    { Def += Items[choice - 1].Stat; }
                    else
                    { Atk += Items[choice - 1].Stat; }
                    Equips[type] = choice - 1;
                }
                catch (ArgumentOutOfRangeException ex)
                { Program.WrongSelectDisplay(); }
            }
        }

        public void BuyItem(Item item)
        {
            Gold -= item.Price;
            Items.Add(item);
        }

        public void SellItem(int idx)
        {
            if (Equips[Items[idx].ItemType] == idx)//장착 해제
            {
                if (Items[idx].ItemType == Item.Type.armor)
                { Def -= Items[idx].Stat; }
                else
                { Atk -= Items[idx].Stat; }
                Equips[Items[idx].ItemType] = null;
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
            Gold += reward * new Random().Next((int)Atk * 10, (int)Atk * 20) / 1000;

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
