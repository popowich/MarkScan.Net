using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkScan.Tools
{
    /// <summary>
    /// Преобразователь символов между различными раскладками клавиатуры
    /// </summary>
    public static class KeyboardMapper
    {
        private static Dictionary<char, char> mapping = new Dictionary<char, char>
        {
            { 'Й','Q'},
            { 'Ц','W'},
            { 'У','E'},
            { 'К','R'},
            { 'Е','T'},
            { 'Н','Y'},
            { 'Г','U'},
            { 'Ш','I'},
            { 'Щ','O'},
            { 'З','P'},
            { 'Х','{'},
            { 'Ъ','}'},
            { 'Ф','A'},
            { 'Ы','S'},
            { 'В','D'},
            { 'А','F'},
            { 'П','G'},
            { 'Р','H'},
            { 'О','J'},
            { 'Л','K'},
            { 'Д','L'},
            { 'Ж',':'},
            { 'Э','\"'},
            { 'Я','Z'},
            { 'Ч','X'},
            { 'С','C'},
            { 'М','V'},
            { 'И','B'},
            { 'Т','N'},
            { 'Ь','M'},
            { 'Б','<'},
            { 'Ю','>'},
            { 'Ё','~'},

            { 'й','q'},
            { 'ц','w'},
            { 'у','e'},
            { 'к','r'},
            { 'е','t'},
            { 'н','y'},
            { 'г','u'},
            { 'ш','i'},
            { 'щ','o'},
            { 'з','p'},
            { 'х','['},
            { 'ъ',']'},
            { 'ф','a'},
            { 'ы','s'},
            { 'в','d'},
            { 'а','f'},
            { 'п','g'},
            { 'р','h'},
            { 'о','j'},
            { 'л','k'},
            { 'д','l'},
            { 'ж',';'},
            { 'э','\''},
            { 'я','z'},
            { 'ч','x'},
            { 'с','c'},
            { 'м','v'},
            { 'и','b'},
            { 'т','n'},
            { 'ь','m'},
            { 'б',','},
            { 'ю','.'},
            { 'ё','/'}
        };

        /// <summary>
        /// Преобразует строку содержающую кириллические символы в строку 
        /// с соответвующими символами в латинской раскладке
        /// </summary>
        /// <param name="line">Исходная строка</param>
        /// <returns>Преобразованная строка</returns>
        public static string TranslateFromCyrillic(string line)
        {
            var outLine = new char[line.Length];
            char tmpChar;

            for (var i = 0; i < line.Length; i++)
            {
                if (mapping.TryGetValue(line[i], out tmpChar))
                    outLine[i] = tmpChar;
                else
                    outLine[i] = line[i];
            }

            return new string(outLine);
        }
    }
}
