using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRTIPPO
{
    public class Dice
    {
        //Сохранение количества выпадений каждого номера
        long[] rolls = { 0, 0, 0, 0, 0, 0 };
        //Рандом для случаных чисел
        readonly Random roller = new Random();
        //Последний выпавший номер
        int lastRoll = 0;
        //Сохранение ранее выпавшего номера
        int previousRoll = 0;

        //Выполнение броска
        public int RollNumber()
        {
            previousRoll = lastRoll;
            //Бросок
            lastRoll = roller.Next(0, 5) + 1;

            //Обновление кол-ва выпадения номера
            rolls[lastRoll]++;
            return lastRoll;
        }

        //Последнее выпавшее число
        public int LastRoll => lastRoll;
        //Итоговые значения
        public long[] Rolls => rolls;
        //Общее значение всех выпавших
        public long[] TotalsOfRolledNumbers => rolls.Select((number, index) => number * (index + 1)).ToArray();
        public long TotalValues => TotalsOfRolledNumbers.Sum();
    }
}
