using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameRTIPPO
{
    public class Player
    {
        public string Name { get; set; }
        public Bitmap Image { get; set; }
        public int Balance { get; set; }
        public int Points { get; set; }
        public bool isWinner { get; set; }
        public bool isKicked { get; set; }

        public Player(string name, Bitmap image, int balance, int points, bool winner, bool kicked)
        {
            Name = name;
            Image = image;
            Balance = balance;
            Points = points;
            isWinner = winner;
            isKicked = kicked;
        }

        public static Player[] CreatePlayers()
        {
            //аватары
            Bitmap[] avatar = new Bitmap[11];
 
            avatar[0] = new Bitmap(GameRTIPPO.Properties.Resources._1);
            avatar[1] = new Bitmap(GameRTIPPO.Properties.Resources._2);
            avatar[2] = new Bitmap(GameRTIPPO.Properties.Resources._3);
            avatar[3] = new Bitmap(GameRTIPPO.Properties.Resources._4);
            avatar[4] = new Bitmap(GameRTIPPO.Properties.Resources._5);
            avatar[5] = new Bitmap(GameRTIPPO.Properties.Resources._61);
            avatar[6] = new Bitmap(GameRTIPPO.Properties.Resources._7);
            avatar[7] = new Bitmap(GameRTIPPO.Properties.Resources._8);
            avatar[8] = new Bitmap(GameRTIPPO.Properties.Resources._9);
            avatar[9] = new Bitmap(GameRTIPPO.Properties.Resources._10);
            avatar[10] = new Bitmap(GameRTIPPO.Properties.Resources._11);

            //имена
            var nicknames = new List<string>();
            nicknames.AddRange(new List<string>{
                "Dima",
                "Alex",
                "Vova",
                "Nero",
                "Egor",
                "Ivan",
                "Maks",
                "Leha",
                "Vlad",
                "Oleg",
            });
            ;
           
            List<Bitmap> avatarList = new List<Bitmap>(avatar);  // Конвертация массива в список
            List<int> indexes = new List<int>(); //для использованных значений
            Player[] players = new Player[11]; //объекты

            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int index = random.Next(avatarList.Count-1); // Генерация случайного индекса
                while (indexes.Contains(index) == true)
                {
                    index = random.Next(avatarList.Count-1);
                }
                if (indexes.Contains(index) == false)
                {
                    players[i] = new Player(nicknames[index], avatar[index], 3, 0, false, false);
                    indexes.Add(index);
                }
            }
            players[10] = new Player("Lxst", avatar[10], 3, 0, false, false);
            return players;
        }
    }
}
