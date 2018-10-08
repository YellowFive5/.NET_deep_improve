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
using System.ComponentModel;
using Microsoft.Win32;

/*
    WPF
    Для вызуализации используется DirectX и аппаратная поддержка

    Спецсобытия самого приложения, которые можно переопределить находятся в App.xaml.cs
    Полный список событий в описании класса Application

    Свойства-зависимости
    Маршрутизация событий:
    1.  Direct  -   прямое событие
    2.  Тунелирование   -   Сверху вниз
    3.  Пузырьковое -   Снизу вверх

    Вложенные дескрипторы

    Модальное окно  -   ShowDialog()
    Немодальное обычное окно    -   Show();

    В проекте есть файл настроек, с ним можно работать
    Properties.Settings.Default.Someparam

    ICommand    -   Интерфайс для создания собственной команды

    Статические и динамические ресурсы
    Ресурс из App.xaml

    Изменение языка через файлы ресурсов 

    Splash-screen в качесте заставки

    Стили в ресурсах
    Триггеры

    Наследования стиля BasedOn
    TargetType="Button" //  Все кнопки используют стиль
    Style={x:Null}  //  Определенная кнопка не использует

    Привязки
    Обновления првязки

    ItemTemplate    -   DataTemplate    //  Готовые шаблоны

    Path    -   сложная геометрия фигур

    ObserableCollection для автообновнения контролов

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

            //if (Settings1.Default.State == true)
            //    Settings1.Default.State = false;
            //else if (Settings1.Default.State == false)
            //    Settings1.Default.State = true;
            //Settings1.Default.Save();
        }

        private void NewWindowButton_Click(object sender, RoutedEventArgs e)
        {
            NewWindow nw = new NewWindow(); //  Создаем новое окно
            nw.Owner = this;    //  Устанавливаем владельца
            nw.ShowDialog();  //  Показываем
        }

        private void ThButton_Click(object sender, RoutedEventArgs e)   //  Кнопка для асинхронного потока
        {
            TextBox.Text = "Работем..."; //  Владелец данного объекта главный поток
            Thread th = new Thread(SomeLongMethod); //  Кидаем в новый поток задачу
            th.Start();

            Button btn = sender as Button;
            btn.Background =(ImageBrush)btn.TryFindResource("JpgBrush");   //  Работа с ресурсом из App.xaml
        }
        public void SomeLongMethod()    //  Долгий метод
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));  // Спим 5 сек
            //  Для доступа другого потока к элементам главного потока нужно костилить 
            TextBox.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)delegate () { TextBox.Text = "Не активно"; });  //  Через диспетчер задач потоков создаем анонимный делегат и работаем с объектом
        }

        private void Polly_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Popup.IsOpen != true)
                Popup.IsOpen = true;
        }

        private void Polly_MouseLeave(object sender, MouseEventArgs e)
        {
            Popup.IsOpen = false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены, что хотите выйти?", "Exit", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
