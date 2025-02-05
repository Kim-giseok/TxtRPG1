using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG1
{
    [Serializable]
    internal class Item
    {
        public string Name { get; }
        public enum Type { armor, weapon };
        public Type ItemType { get; }
        public int Stat { get; }
        public string Descript { get; }
        public int Price { get; }
        public bool Bought { get; set; }

        public Item(string name, Type type, int stat, string descript, int price)
        {
            Name = name;
            ItemType = type;
            Stat = stat;
            Descript = descript;
            Price = price;
            Bought = false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Name}");
            for (int i = Name.Length; i <= 7; i++)
            { sb.Append('　'); }
            sb.Append("\t| ");

            if (ItemType == Type.armor)
            { sb.Append("방어력"); }
            else if (ItemType == Type.weapon)
            { sb.Append("공격력"); }

            sb.Append($" + {Stat}\t| {Descript}");
            for (int i = Descript.Length; i < 25; i++)
            { sb.Append("　"); }

            return sb.ToString();
        }
    }
}
