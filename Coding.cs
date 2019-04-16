using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    public static class Coding
    {
        /*
         * Первый алгоритм построения цифро-буквенного кода.
         * Первая буква в обоих кодах - это номер алгоритма кодироваия.
         * Алгоритм конвентирует предоставленные ему данные в 16-причную систему счисления, и добавляет
         * недостоющие нули для фиксированной длины каждого участка, а именно:
         * дата - 5 символов, л\с - 7 символов, номер id - 5 символов.
         * При декодировании, строка разделяется на эти же самые отрезки, и фекодируется обратно.
         * 
         * Второй алгоритм основан на таблице Unicode
         * Алгоритм состовляет строку из предложенных ему данных, дополняя недостающую длину нулями:
         * дата - 6 символов, л\с - 8 символов, номер id - 4 символа.
         * Затем, алгоритм разделяет строку на 2-значные числа и присваевает им символ Unicode.
         * При декодировании, эти символы переводятся обратно в строку, состоящую из чисел.
         * Так как известны длины каждых сегментов, строка делится на нужные значения.
         */

        //  реализация генератора номер 1
        public static string CodingGenerator1 (ObjectOfConsumption objectOfConsumption)
        {
            try
            {
                //  Первая буква - тип генератора (a - Первый генератор, b - второй генератор)
                string result = "a";
                //  Добавить к строке кодированные в 16-ричной системе последовательно дату, л\с и id обьекта
                result = result + ConversionDate(objectOfConsumption.dateTime);
                result = result + ConversionPersinalAccount(objectOfConsumption.personalAccount);
                result = result + ConversionName(objectOfConsumption.nameID);
                return result;
            }catch
            {
                throw new Exception("Возникла ошибка кодирования");
            }
        }

        //  Метод принимает объект даты и возвращает дату в виде строки без точек
        private static string ConversionDate (DateTime date)
        {
            // Удалить из даты точки
            string encodedString = RemovePoint(date.ToShortDateString());
            int encodedNumber = Convert.ToInt32(encodedString);

            return encodedNumber.ToString("X").PadLeft(5, '0');
        }

        //  Метод удаления точек в дате
        private static string RemovePoint (string date)
        {
            return date.Remove(2, 1).Remove(4, 3);
        }

        //  Метод принимает номер лицевого счёта, и конвентирует его в 16-ричную систему счисления. доводит общее кол-во символов до 7
        private static string ConversionPersinalAccount (string personalAccount)
        {
                int encodedNumber = Convert.ToInt32(personalAccount);
            string result = encodedNumber.ToString("X");
            result = result.PadLeft(7, '0');
            return result;
        }

        //  Метод, переводящий в 16-ричную систему счисления id объекта
        private static string ConversionName (string nameID)
        {
            return Convert.ToInt32(nameID).ToString("X").PadLeft(5, '0');
        }


        //  реализация генератора номер 2
        public static string CodingGenerator2 (ObjectOfConsumption objectOfConsumption)
        {
            string codingString = "";
            //  добавить в шифруемую строку дату, л\с и id обьекта
            codingString += RemovePoint(objectOfConsumption.dateTime.ToShortDateString());
            codingString += objectOfConsumption.personalAccount.PadLeft(8, '0') + objectOfConsumption.nameID.PadLeft(4, '0');

            //  Шифровать с помощью таблицы и вернуть с добовлением кода генератора
            return "b" + CodingString(codingString);
        }

        //  Метод шифрует строку. строка делится на 2-значное число и кодируется исходя из таблицы
        private static string CodingString (string codingString)
        {
            if (codingString.Length != 18) throw new Exception("Неверноый формат строки");
            string result = "";
            for (int i = 0; i <= 16; i += 2)
            {
                result += CodingNumber (codingString.Substring(i, 2));
            }
            return result;
        }

        //  Зашифровать число 
        private static char CodingNumber (string number)
        {
            try
            {
                int intNumber = Convert.ToInt32(number);
            //  Шифруем числа по таблице ascii
            // прибавляем 33 чтобы пропустить спец символы
            intNumber += 40;
                //  если номер юольше 127 заменяем на другие символы (чтобы исключить спец символы)
                if (intNumber >= 127) intNumber += 964;
            return (char)intNumber;
            }
            catch
            { throw new Exception("Неверный формат кодируемой строки"); }
        }
    }
}
