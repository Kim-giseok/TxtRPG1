using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TxtRPG1
{
    internal class Dungeon
    {
        public enum Level { 쉬운, 일반, 어려운 };
        public Level Lv { get; }
        public int Reward { get; }
        public int DefCut { get; }

        public Dungeon(Level level, int reward, int defCut)
        {
            Lv = level;
            Reward = reward;
            DefCut = defCut;
        }

        public void Enter(Character player)
        {
            if (player.Def < DefCut && (new Random().Next(0, 100) <= 40))
            { Fail(player); }
            else //dungeon clear
            { Clear(player); }
        }

        void Fail(Character player)
        {
            byte choice;
            do
            {
                Console.Clear();
                Console.WriteLine("클리어 실패");
                Console.WriteLine($"{Lv} 던전을 클리어 하지 못했습니다.");
                Console.WriteLine();
                Console.WriteLine("[탐험 결과]");

                Console.Write($"체력 {player.Hp} -> ");
                player.GetDamage(player.Hp / 2);
                Console.WriteLine(player.Hp);

                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out choice) && choice == 0)
                { break; }
                Program.WrongSelectDisplay();
            } while (true);
        }

        void Clear(Character player)
        {
            byte choice;
            do
            {
                Console.Clear();
                Console.WriteLine("던전 클리어");
                Console.WriteLine("축하합니다!!");
                Console.WriteLine($"{Lv} 던전을 클리어 하였습니다.");
                Console.WriteLine();
                Console.WriteLine("[탐험 결과]");

                Console.Write($"체력 {player.Hp} -> ");
                player.GetDamage((new Random().Next(20, 35) - (player.Def - DefCut)));
                Console.WriteLine(player.Hp);

                Console.Write($"Gold {player.Gold} G -> ");
                player.Reward(Reward);
                Console.WriteLine($"{player.Gold} G");

                Console.WriteLine();
                if (player.Exp == 0)
                { Console.WriteLine($"레벨 업! Lv.{player.Level - 1} -> Lv.{player.Level}"); }
                else
                { Console.WriteLine(); }
                Console.WriteLine($"앞으로 {player.Level - player.Exp}회 더 클리어 하면 레벨 업.");

                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                if (Program.Choice(out choice) && choice == 0)
                { break; }
                Program.WrongSelectDisplay();
            } while (true);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Lv}　던전");

            for (int i = sb.Length; i <= 5; i++)
            { sb.Append("　"); }
            sb.Append("\t| ");

            sb.Append($"방어력　{DefCut:D2}　이상　권장");

            return sb.ToString();
        }
    }
}
