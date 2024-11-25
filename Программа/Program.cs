using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace _45
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BEGINNING
            Utils.info = new Utils.ProgramInfo(
                author: "Лющенко Артём",
                name: "45. Проверка правописания",
                description: "Проверка введённого текста на правописание",
                instruction: "1. Вводите текст.\n2. Для окончания ввода нажмите ESC.\n3. Введите название файла, в который будет сохранён результат.");
            Utils.PrintAuthor();

            //MAIN PART
            /*
             *  string path
             *  string[] validWords
             *  string[] words
             */
            #region Getting text
            string path = @"S:\ИСП211\Дерин Лющенко\Практика Учебная\Лющенко\45\";
            string[] validWords = Array.Empty<string>();
            try
            {
                validWords = File.ReadAllLines(path + @"Программа\RUS.txt", Encoding.GetEncoding(1251));
            }
            catch
            {
                Utils.RestartApp("Файл не найдён", "Проверьте существования файла RUS.txt");
            }
            Utils.ColoredWrite("Введите текст. Нажмите <fg=darkyellow>ESC<r>, чтобы прекратить ввод\n");

            string userText = "";

            while (true)
            {
                var currKey = Console.ReadKey();
                if (currKey.Key == ConsoleKey.Escape) break;
                userText += currKey.KeyChar + Console.ReadLine() + "\n";
            }
            Console.WriteLine();
            List<string> userWords = userText.Split(' ', '\n').ToList();
            userWords.RemoveAt(userWords.Count - 1);
            #endregion

            /*
             *  Dictionary<string, string>() corrections
             */
            #region Processing words
            Utils.ColoredWriteLine("\n<fg=magenta>Обработка...");
            var sw = new Stopwatch();
            sw.Start();
            var corrections = new Dictionary<string, string>();
            foreach (var word in userWords)
            {
                string bestMatch = null;
                int bestDistance = int.MaxValue;

                foreach (var correctWord in validWords)
                {
                    int distance = LevenshteinDistance(word.ToLower(), correctWord);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestMatch = correctWord;
                    }
                }

                if (word != bestMatch && word != "" && !int.TryParse(word, out int _)) corrections[word] = bestMatch;
            }
            sw.Stop();
            if (corrections.Keys.ToArray().Length > 0)
            {
                foreach (var word in corrections.Keys)
                {
                    Utils.ColoredWriteLine("Ошибка в слове <fg=darkred>'" + word + "'");
                }
            }
            else
            {
                Utils.ColoredWriteLine("<fg=darkgreen>Ошибки не обнаружены");
            }
            Utils.ColoredWriteLine($"<fg=magenta>Обработка завершена за {Math.Round(sw.ElapsedMilliseconds / 1000.0, 2)} секунд");
            #endregion

            #region Writing to file
            if (corrections.Keys.ToArray().Length > 0)
            {
                string fileName = Utils.Input<string>("имя текстового файла отчета латинскими буквами");
                int maxWordLength = 0;
                corrections.Keys.Select(item => item.ToString().Length).ToArray().Map(item => maxWordLength = maxWordLength < (int)item ? (int)item : maxWordLength);
                using (StreamWriter outputFile = new StreamWriter(path + fileName + ".txt"))
                {
                    foreach (var correction in corrections)
                    {
                        outputFile.Write(correction.Key);
                        for (int i = 0; i <= maxWordLength - correction.Key.Length + 2; i++) outputFile.Write(" ");
                        outputFile.Write($"исправлено на    {correction.Value}\n");
                    }
                }
                Utils.ColoredWriteLine($"<fg=cyan>Результат успешно сохранён в\n{path + fileName + ".txt"}");
                Thread.Sleep(500);
                Utils.NewWindow(path + fileName + ".txt");
            }
            #endregion

            //ENDING
            Utils.Ending();
        }
        static int LevenshteinDistance(string a, string b)
        {
            int[,] dp = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
                dp[i, 0] = i;

            for (int j = 0; j <= b.Length; j++)
                dp[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = a[i - 1] == b[j - 1] ? 0 : 1;
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
                }
            }

            return dp[a.Length, b.Length];
        }
    }
}
