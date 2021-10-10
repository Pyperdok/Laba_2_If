using System;
using System.Windows;
using System.IO;
using Task_If;
using System.Windows.Input;

namespace Lab_2_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();
            TB_TimeArrive.Focus();
        }
        //Сохраняет поля в файл
        private void SaveConfig()
        {
            FileStream Fs = File.Open("cfg.txt", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter Writer = new StreamWriter(Fs);
            Writer.WriteLine(TB_TimeArrive.Text);
            Writer.WriteLine(TB_TimeLeave.Text);
            Writer.WriteLine(TB_TimePass.Text);
            Writer.Close();
        }
        //Загружает поля из файла
        private void LoadConfig()
        {
            FileStream Fs = File.Open("cfg.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader Reader = new StreamReader(Fs);
            TB_TimeArrive.Text = Reader.ReadLine();
            TB_TimeLeave.Text = Reader.ReadLine();
            TB_TimePass.Text = Reader.ReadLine();
            Reader.Close();
        }
        //Срабатывает при нажатии на кнопку
        public void BT_Result_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] time = TB_TimeArrive.Text.Split(':');
                Time Arrive = new Time(int.Parse(time[0]), int.Parse(time[1])); //Время прибытия

                time = TB_TimeLeave.Text.Split(':');
                Time Leave = new Time(int.Parse(time[0]), int.Parse(time[1])); //Время отправления

                time = TB_TimePass.Text.Split(':');
                Time Pass = new Time(int.Parse(time[0]), int.Parse(time[1])); //Время пассажира

                if (Leave < Arrive && sender != null)
                {
                    MessageBoxResult result = MessageBox.Show("Пассажир пришел в день отправления поезда?", "Сообщение", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes) IsLeaveDay = true;
                }

                bool isStandTrain = Program.IsTrainStand(Arrive, Leave, Pass, IsLeaveDay);
                string message = isStandTrain ? "Поезд СТОИТ на платформе" : "Поезд НЕ стоит на платформе";

                L_Result.Content = $"Результат: {message}";

                SaveConfig(); //Сохраняет введенные поля
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                L_Result.Content = "Ошибка: Введены некорректные данные";
            }

            IsLeaveDay = default;
        }

        //Очищает поля
        private void BT_Clear_Click(object sender, RoutedEventArgs e)
        {
            TB_TimeArrive.Text = string.Empty;
            TB_TimeLeave.Text = string.Empty;
            TB_TimePass.Text = string.Empty;
            L_Result.Content = "============================";
        }

        //Выводит задание
        private void BT_Task_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Поезд прибывает на станцию в a часов b минут и отправляется в c часов d минут. Пассажир пришел на платформу в n часов m минут. Будет ли поезд стоять на платформе? Числа a, b, c, d, n, m — целые, 0 < a 23, 0 < b 59, 0 < c 23, 0 < d 59, 0 < n 23, 0 < m 59");
        }

        //Переход на следующий элемент при нажатии на Enter
        private void TB_TimeNextContorl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TB_TimeArrive.Text != string.Empty)
            {
                if (sender == TB_TimeArrive)
                {
                    TB_TimeLeave.Focus();
                    TB_TimeLeave.CaretIndex = TB_TimeLeave.Text.Length;
                }
                else if (sender == TB_TimeLeave)
                {
                    TB_TimePass.Focus();
                    TB_TimePass.CaretIndex = TB_TimePass.Text.Length;
                }
                else if (sender == TB_TimePass)
                {
                    BT_Result.Focus();
                    BT_Result_Click(1, null);
                }
            }
        }

        private bool _isLeaveDay = default;
        public bool IsLeaveDay { private get { return _isLeaveDay; } set { _isLeaveDay = value; } }
        public string TimeArrive { get { return TB_TimeArrive.Text; } set { TB_TimeArrive.Text = value; } }
        public string TimeLeave { get { return TB_TimeLeave.Text; } set { TB_TimeLeave.Text = value; } }
        public string TimePass { get { return TB_TimePass.Text; } set { TB_TimePass.Text = value; } }
        public string Result { get { return (string)L_Result.Content; } }
    }
}
