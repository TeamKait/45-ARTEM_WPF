using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void ShowInfoButton(object sender, RoutedEventArgs e)
        {
            UtilsWPF.ProgramInfo.Show();
        }
        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Init(
                    author: "Лющенко Артём",
                    name: "45. Проверка правописания",
                    description: "Проверка введённого текста на правописание",
                    instruction: "1. Вводите текст.\n2. Для окончания ввода нажмите ESC.\n3. Введите название файла, в который будет сохранён результат.");
            var label = LogicalTreeHelper.FindLogicalNode(this, "NameLabel") as Label;
            if (label != null)
            {
                label.Content = UtilsWPF.ProgramInfo.name;
            }
        }
    }
}
