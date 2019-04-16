using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration
{
    public static class Decoding
    {
        //  Метод декодирования генератора 1
        public static ObjectOfConsumption DecodingGenerator1(string stringToDecode)
        {
            //  Проверка на правильность декодера
            if (stringToDecode[0] != 'a') throw new Exception("Строка не соответствует 1-ому декодеру");
            if (stringToDecode.Length != 18) throw new Exception("Сторка содержит неверный формат (неточное число знаков)");
            string dateTime = stringToDecode.Substring(1, 5);
            string personalAccount = stringToDecode.Substring(6, 7);
            string nameID = stringToDecode.Substring(13, 5);
            try
            {
                return new ObjectOfConsumption(DecodingStringHex(nameID), DecodingStringHex(personalAccount).PadLeft(8, '0'), DecodingDateHex(dateTime));
            }catch
            {
                //  Если данные неудалось декодировать, то выдать исключение
                throw new Exception("Данные повреждены");
            }
        }
        
        // Метод, переводящий 16-ричнную строку в дату
        private static DateTime DecodingDateHex (string dateTime)
        {
            //  Перевести дату в число и разделить на составляющие
            int date = int.Parse(dateTime, System.Globalization.NumberStyles.HexNumber);
            return DecodingDate(date);
        }

        // Метод, создающий DateTime
        private static DateTime DecodingDate(int date)
        {
            //  Разделить дату на составляющие
            int year = (date % 100) + 2000;
            int month = (date % 10000) / 100;
            int day = date / 10000;
            return new DateTime(year, month, day);
        }


        //  Метод, переводящий 16-ричную строку в л\с и id обьекта
        private static string DecodingStringHex (string hex)
        {
            //  Перевести строку с номерами в 10-ричную систему и вернуть в виде строки
            int decodingNumber = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return decodingNumber.ToString();
        }


        //  Метод декодирования генератора 2
        public static ObjectOfConsumption DecodingGenerator2(string stringToDecode)
        {
            //  Прверка корректности входной строки
            if (stringToDecode[0] != 'b') throw new Exception("Строка не соответствует 2-ому декодеру");
            if (stringToDecode.Length != 10) throw new Exception("Сторка содержит неверный формат (неточное число знаков)");
            //  Декодировать строку
            string decodingString = DecodingStringAscii(stringToDecode.Remove(0,1));
            return ConvertObjectOfConsumption(decodingString);
        }

        //  Метод декодирует строку в соответствии с таблицей Ascii
        private static string DecodingStringAscii(string stringToDecode)
        {
            try
            {
                string result = "";
                foreach (char decodingChar in stringToDecode)
                {
                    int decodincInt = (int)decodingChar;
                    //  Проверка недопустимые символы
                    if (decodincInt > 127) decodincInt -= 964;
                    decodincInt -= 40;
                    // если получилось однозначное число, нужно дописать 0
                    if ((decodincInt / 10) == 0) result += "0" + decodincInt; else result += decodincInt;
                }
                return result;
            }catch
            {
                throw new Exception("Ошибка декодирования строки");
            }
        }

        //Метод формирует ObjectOfConsumption из строки
        private static ObjectOfConsumption ConvertObjectOfConsumption(string convertString)
        {
            if (convertString.Length != 18) throw new Exception("Дкуодированная строка имеет неверный формат");
            //  Перевод даты
            try
            {
                //  В текущем блоке только дата может вызвать исключение
                int date = Convert.ToInt32(convertString.Substring(0, 6));
                DateTime dateTime = DecodingDate(date);
                //  выборка л\с и id
                string personalAccount = convertString.Substring(6, 8);
                string nameID = convertString.Substring(14, 4);
                nameID = nameID.TrimStart('0');
                return new ObjectOfConsumption(nameID, personalAccount, dateTime);
            }catch
            {
                throw new Exception("Декодированная дата неверного формата");
            }
        }
    }
}
