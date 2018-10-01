using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
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

/*
    WPF
    Для вызуализации используется DirectX и аппаратная поддержка

    Спецсобытия самого приложения, которые можно переопределить находятся в App.xaml.cs
    Полный список событий в описании класса Application

*/
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

        private void Button_Click(object sender, RoutedEventArgs e) // object sender - отправитель события, RoutedEventArgs e - аргументы 
        {
            (sender as Button).Background = Brushes.Azure; //  Приводим отправителя к кнопке и работем с ним

            Random rc = new Random();   //  Рандомим цвета =)
            Polly.Fill = new SolidColorBrush(Color.FromRgb((byte)rc.Next(0, 255), (byte)rc.Next(0, 255), (byte)rc.Next(0, 255)));
        }

        private void NewWindowButton_Click(object sender, RoutedEventArgs e)
        {
            NewWindow nw = new NewWindow(); //  Создаем новое окно
            nw.Owner = this;    //  Устанавливаем владельца
            nw.Show();  //  Показываем
        }

        private void ThButton_Click(object sender, RoutedEventArgs e)   //  Кнопка для асинхронного потока
        {
            MessageBox.Text = "Работем..."; //  Владелец данного объекта главный поток
            Thread th = new Thread(SomeLongMethod); //  Кидаем в новый поток задачу
            th.Start();
        }
        public void SomeLongMethod()    //  Долгий метод
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));  // Спим 5 сек
            //  Для доступа другого потока к элементам главного потока нужно костилить 
            MessageBox.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate () { MessageBox.Text = "Не активно"; });  //  Через диспетчер задач потоков создаем анонимный делегат и работаем с объектом
        }
    }
}
