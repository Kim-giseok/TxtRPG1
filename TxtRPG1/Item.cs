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
        Type type;
        int stat;
        string descript;

        public Item(string name, Type type, int stat, string descript)
        {
            this.name = name;
            this.type = type;
            this.stat = stat;
            this.descript = descript;
        }

        public override string ToString()
        { 
            StringBuilder sb = new StringBuilder();
            sb.Append($"{name}\t| ");
            if(type == Type.armor)
            { sb.Append("방어력"); }
            else if(type == Type.weapon)
            { sb.Append("공격력"); }
            sb.Append($" +{stat} | {descript}");

            return sb.ToString();
        }
    }
}
