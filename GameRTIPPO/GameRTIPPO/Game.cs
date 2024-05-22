using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRTIPPO
{
     public class Game
    {
        private Player[] players;
        private Bank bank;
        public int count { get; set; }

        public Game(Player[] players, int initialBankPoints)
        {
            this.players = players;
            bank = new Bank(initialBankPoints);
        }

/*        public void Play()
        {
            bool gameEnd = false;

            while (!gameEnd)
            {
                for (int i = 0; i < count; i++)
                {

                }
            }
        }

        private GamePlayForm GameRollForm;
        public void HandleButtonRollClicked(object sender, EventArgs e)
        {
            // Обработка нажатия кнопки броска
            // Можно вызвать нужные методы или обновить состояние игры
        }*/
    }
}
