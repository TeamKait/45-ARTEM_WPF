using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace WPF
{
    public static class UtilsWPF
    {
        /// <summary>
        /// Информация о программе
        /// </summary>
        public struct ProgramInfo
        {
            public static string author;
            public static string name;
            public static string description;
            public static string instruction;
            public static void Show()
            {
                Notify("Информация о программе",
                        $"Автор: {author}" +
                        $"\n\nНазвание: {name}" +
                        $"\n\nОписание: {description}" +
                        $"\n\nИнструкции:\n{instruction}");
            }
        }

        /// <summary>
        /// Открыть окно с уведомлением
        /// </summary>
        /// <param name="caption">Заголовок</param>
        /// <param name="text">Содержание</param>
        /// <param name="icon">Иконка (опционально)</param>
        /// <returns>MessageBoxResult</returns>
        public static MessageBoxResult Notify(string caption, string text, MessageBoxImage icon = MessageBoxImage.Information)
        {
            return MessageBox.Show(text, caption, MessageBoxButton.OK, icon);
        }

        /// <summary>
        /// Метод, иницилизирующий полезные функции UtilsWPF
        /// </summary>
        /// <param name="window">Основное окно (можно вызвать из него)</param>
        /// <param name="author">Автор</param>
        /// <param name="name">Название программы</param>
        /// <param name="description">Описание</param>
        /// <param name="instruction">Инструкции</param>
        public static void Init(this Window window, string author, string name, string description, string instruction)
        {
            ProgramInfo.author = author;
            ProgramInfo.name = name;
            ProgramInfo.description = description;
            ProgramInfo.instruction = instruction;

            window.Title = name;

            window.KeyDown += (object sender, KeyEventArgs e) =>
            {
                if (e.Key == Key.F1)
                {
                    ProgramInfo.Show();
                }
            };
        }

        /// <summary>
        /// Асинхронно выполнить переданное действие
        /// </summary>
        /// <param name="callback">действие</param>
        /// <returns>Thread</returns>
        public static Thread StartCoroutine(Action callback)
        {
            Thread coroutine = new Thread(() => callback());
            coroutine.Start();
            return coroutine;
        }
    }
}
