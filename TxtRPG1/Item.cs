using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG1
{
    internal class Item
    {
        string name;
        public enum Type { armor, weapon };
        public Type ItemType { get; private set; }
        public int Stat { get; }
        string descript;

        public Item(string name, Type type, int stat, string descript)
        {
            this.name = name;
            this.ItemType = type;
            this.Stat = stat;
            this.descript = descript;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{name}\t| ");
            if (ItemType == Type.armor)
            { sb.Append("방어력"); }
            else if (ItemType == Type.weapon)
            { sb.Append("공격력"); }
            sb.Append($" +{Stat} | {descript}");

            return sb.ToString();
        }
    }
}
