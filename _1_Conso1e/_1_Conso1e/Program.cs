using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;
using System.Xml.XPath;

namespace _1_Conso1e
{


    class Program
    {
        #region С# Essentials
        /*

        /// <summary>
        /// АВТООПИСАНИЕ МЕТОДА через тройной слеш
        /// </summary>
        /// <param name="a">...описание...</param>
        /// <param name="b">...описание...</param>
        /// <returns>...описание...</returns>
        int Foo(int a,string b) { return a; }

        static void Foo2(ref int x) { } //  Передача переметра по ссылке
        int x=5;
        Foo2(ref x);

        static int Foo3(out int x)  //  OUT параметры
        {
        x = 1;
        return 2; 
        } 
        int y = 0;
        int res=Foo3(out y);

        static void Foo4(string value1 = "default", int value2 = 5, double value3 = 3.5)    //  Аргументы по умолчанию
        {
            Console.WriteLine("{0},{1},{2}", value1, value2, value3);
        }
        Foo4(value1:"some");    //  Использованин именованных аргументов

        //  Допустима перегрузака Main, однако возвращаемое значение Main может быть только int или void
        
        //------------------------------------------------------------------------
        //  КЛАССЫ
        class MyClass   //  Просто класс объекта
        {
            public MyClass() { }    //  Конструктор по умолчанию
            public string name;
            public readonly int ID = 32324; //  Поля только для чтения, могут изменяться только при создании и в конструкторе
            private string profession;  //  Скрытое поле 
            public string Profession
            {  //  Искуственная конструкция "свойство" сет и гет для скрытого поля
                set { profession = value; }
                get { return profession; }
            }
            public double Money { get; set; }   //  Автосвойство, само по себе создает поле с геттером и сеттером
            public void Do() { }
        }
        
        //  Main
        new MyClass().Do(); //  Обращение по слабой ссылке 
        MyClass obj_1 = new MyClass();
        obj_1.name = "Jack";
        obj_1.Do();
        obj_1.Profession = "Driver";  //  Работа через свойство
        Console.WriteLine(obj_1.Profession);
        obj_1.Money = 4.55;
        Console.WriteLine(obj_1.Money);

        //------------------------------------------------------------------------
        //  ЧАСТИЧНАЯ РЕАЛИЗАЦИЯ КЛАССА в разных местах
        partial class ParClass {    //  Часть реализаций класса
            public int x = 1;
            partial void Foo(); //  Объявление частичного метода
        }
        partial class ParClass {    //  Может находится в разных частях программы
            partial void Foo() { }      //  Реализация частичного метода
        }
        //------------------------------------------------------------------------
        //  НАСЛЕДОВАНИЕ
        class BaseClass //   Базовый класс
        {
            public int Public_A = 1;    //  Доступ отовсюду
            private int Private_A = 2;  //  Доступ только внутри класса
            protected int Protected_A = 3;  //  Доступ изнутри или в наследнике
            public void Say() { Console.WriteLine("HI from base"); }    //  Чтобы вызвать родительский замещенный метод нужно сделать UPCAST
            public virtual void Print() { Console.WriteLine("HI from base"); }  //  Виртуальный метод родительского класса
        }
        sealed class DerivedClass : BaseClass  //  Класс наследник, sealed запрещено наследование от него
        {
            public DerivedClass() : base(x,y) //  вызов конструктора базового класса
            {
                this.z=z;
                base.Print();   //  Доступ к базе
            }
            public void Say() { Console.WriteLine("HI from derive"); }  //  Замещаемый метод наследника
            sealed public override void Print() { Console.WriteLine("HI from derive"); }   //  Перезаписываемый метод класса наследника, sealed
        }
        //  Main
        DerivedClass D = new DerivedClass();
        BaseClass B = (BaseClass)D; //  UPCAST, приведение к базовому типу
        D.Print();
        new BaseClass().Print();
        B.GetHashCode() //  Получение hash элемента

        (B is Object)   //  Проверка на класс, true/false ответ
        B as Object //  Приведение к классу с проверкой на возможность приведение, false в случае неудачи

        //------------------------------------------------------------------------
        //  АБСТРАКТНЫЕ классы, методы
        abstract class AbstractClass   //  Абстрактный класс, объект создать нельзя, но неявно он создается при создании объекта конкретного класса
        {
            public abstract void Method();  //  с абстракнтным методом
        }
        class ConcreteClass : AbstractClass //  Конкретный класс
        {
            public override void Method() { }   //  с перезаписываемым  методом
        }
        //  Main
        AbstractClass obj = new ConcreteClass();    //  Создание абстракции с приведением ее к конкретике
        obj.Method();

        
        //------------------------------------------------------------------------
        //  ИНТЕРФЕЙСЫ
        interface IInt  //  Интерфейс
        {
            void Method();  //  Виртуальный метод интерфейса
            void Print();   //  Метод интерфейса с идентичным именем
        }
        interface IInt_2    //  Второй интерфейс
        {
            void Method_2();    //  Виртуальный метод интерфейса
            void Print();   //  Метод интерфейса с идентичным именем
        }
        interface IInt_3 : IInt_2   {   }

        class MyClass : IInt,IInt_2,IInt_3    //  Класс, множественный наследник интерфейсов
        {
            public void Method() { }    //  Реализация интерфейсов
            public void Method_2() { }
            void IInt.Print() { }    //  Реализация метода конкретного интерфейса без модификатора доступа
            void IInt_2.Print() { }    //  Реализация метода конкретного интерфейса без модификатора доступа
        }
        //  Main
        IInt m = new MyClass(); //  UPCAST объекта к интерфейсу
        m.Print();  //  и вызов метода интерфейса
        MyClass n = m as MyClass;   //  
        n.Method_2();

        //  UPCAST  -    явное приведение объекта к родительскому классу
        //  DOWNCAST    -   явное ВОЗВРАЩЕНИЕ элемента от родительского класса к наследнику, возможно только после UPCASTа
        //  Ковариантность  -   неявный UPCAST всех элементов массива [структурный тип элементов (OFF) , ссылочный тип элементов (ON)]
        //  Контрвариантность   -   неявный DOWNCAST всех элементов массива[структурный тип элементов(OFF) , ссылочный тип элементов (OFF) ]
        //------------------------------------------------------------------------
        //  СТАЧИЧЕСКИЕ поля, методы, классы
        class NonStaticClass    //  Обычный класс
        {
            public static int x = 56;   //  Статическое поле
            public static void Foo() { }    //  Статический метод
            static NonStaticClass() { } //  Статический конструктор, выполняется при обращении в класс за методом или полем без создания объекта класса

        }
        //  Main
        NonStaticClass.Foo();   //  Доступ к статическому методу через класс, выполнится статический конструктор

        static class StaticClass    //  Статический класс, не наследует и не наследуем, нельзя создать экземпляр, не реализует интерфейсы, содержит только статические члены, не может содержать конструктора экземпляра.
        {
            public static void Foo() { }
            private static int y = 6;
            static StaticClass() { }    //  Статический конструктор
        }
        //  Main
        StaticClass.Foo();  //  Доступ к статическому классу

        //------------------------------------------------------------------------
        //  РАСШИРЕНИЯ
        static class ExtensionClass //  Класс расширения, создается вне классов
        {
            public static void ExtensionMethod(this string value)   //  Метод расширения
            {
                Console.WriteLine(value + "some_extension");
            }
            public static void ExtensionMethod(this int value)   //  Перегрузка метода расширения
            {
                Console.WriteLine(value + "some_extension");
            }
        }
        //  Main
        string txt = "some text";
        ExtensionClass.ExtensionMethod(txt);    //  Использование класса расширения
        txt.ExtensionMethod();  //  Еще более удобное использование класса расширения
        ExtensionClass.ExtensionMethod(2);  //  Использование перегруженного метода расширения
        "text".ExtensionMethod();   //  Литеральное прямое использование
            
        //------------------------------------------------------------------------
        //  СТРУКТУРНЫЕ ТИПЫ
        struct MyStruct //  Структура
        {
            public int field;   //  Только инициализация поля (1)
        }
        //  Отличия класса и структуры:
        //  1.  У структуры структурный тип данных, а у объекта ссылочный
        //  2.  Структура неявно наследуется от абстрактного класса Value type, а классы от класса Object
        //  3.  Все пременные(структурые типы) хранятся в стеке, а объект это ссылочный тип и он хранится в куче, в стеке от него только ссылка на него
        //  4.  При копировании структуры создается полная копия полей, при копировании ссылочного типа создается ссылка на объект
        //  5.  В теле структуры возможна только инициализация поля
        //  6.  Присвоение значения полю, только после этого можно использовать
        //  7.  Нельзя задавать конструктор ПО УМОЛЧАНИЮ
        //  8.  Если ПОЛЬЗОВАТЕЛЬСКИЙ конструктор есть, то в нем должны быть проинициализированны все поля структуры
        //  9.  НЕ МОЖЕТ быть статической, но может содержать статические члены 
        //  10. Статический конструктор срабатывает только вместе с пользовательским конструктором
        //  11. Если экземпляр структуры создается в массиве или классе, то он создается на куче, а в стеке только тогда, когда экземпляр представлен локальной переменной.
        //  12. Наследуют ТОЛЬКО интерфейсы и реализовывают их.
        //  13. Не могут быть родителями, поэтому нет protected членов, виртуальных и абстрактных методов. 
        //  14. Структура существует только в boxed и unboxed, ссылочные типы существуют только в boxed виде.
        //  15. 
        //  16.
        //  17.
        //  18.
        //  19.
        //  20.
        //  Main
        MyStruct instance;  //  Создание объекта структуры как напрямую,
        MyStruct instance2 = new MyStruct();    //  так и через конструктор по умолчанию.
        instance.field = 5; //  Присвоение значения полю, только после этого можно использовать (2)
            
        //  Боксинг/Анбоксинг - перевод из структурного типа в ссылочный. (перевод из стека в кучу)
        int item = 10;  //  Структурный тип данных, обычная переменная, хранится в стеке
        object obj = item;  //  Боксинг структуры в кучу
        int item2 = (int)obj;   //  Анбоксинг обратно в стек к родному типу
            
        //   Структура упаковывается в Value type, Object и в наследуемый интерфейс
        //  Мелкие объекты лучше делать структурами(в стеке), а большие объекты классами(в куче, где регулируется жизнь)(Самосвал с мышами)

        //------------------------------------------------------------------------
        //  ПЕРЕЧИСЛЕНИЯ
        enum EnumType : byte    //  Перечисление, константы явного типа byte с некоторыми значениями
        {
            Zero = 0,   //  Алиас / значение
            One = 1,
            Two = 2,
            Three = 3
        }
        //  Main
        Console.WriteLine(EnumType.One);    //  Вывод алиаса
        Console.WriteLine((byte)EnumType.One);  //  Привидение алиаса к типу 
           
        //------------------------------------------------------------------------
        //  ДЕЛЕГАТЫ
        class MyClassForDel
        {
            public static void Method() { }    //  Статический методы для делегата
            public void Method_2() { }  //  Обычный метод
            public string Method_3(string x) { return x + "Beep!"; }  //  Строковый метод
        }
        public delegate void MyDelegate();  //  Объявление делегата, спецобъект который хранит в себе указатель на метод
        public delegate string MyDelegate_2(string some);   //  Делегат для строки
        public delegate string MyDelegate_3(string x, string y);
        //  Main
        MyClassForDel mcfd = new MyClassForDel();   //  Объект класса
        MyDelegate Del_1 = new MyDelegate(MyClassForDel.Method);    //  Создание объекта делегата и передача ему метода класса
        MyDelegate Del_1_ = MyClassForDel.Method;   //  Более быстрая конструкция создания
        MyDelegate Del_2 = new MyDelegate(mcfd.Method_2);   //  Передали через объект класса
        Del_1.Invoke();   //  Выполнение
        Del_2();   //  Выполнение

        MyDelegate_2 Del_3 = new MyDelegate_2(mcfd.Method_3);
        string str = Del_3.Invoke("Some string");    //  Вызвали делегат со строкой
        Console.WriteLine(str);

        MyDelegate Del_4 = delegate { Console.WriteLine("Del funct"); };    //  Анонимный делегат, техника предположения делегата
        Del_4.Invoke();

        MyDelegate_3 Del_5 = delegate (string x, string y) { return y + x; };   //  Лямбда метод
        Console.WriteLine(Del_5("1","2"));

        MyDelegate_2 Del_6 = x => x + " World!";    //  Полноценное лямбда выражение 
        Console.WriteLine(Del_6("Hello"));

        MyDelegate DelSum = null;   //  Пустой делегат для арифметики делегатов
        DelSum = Del_1 + Del_2; //  В пустой сложили два делегата
        DelSum -= Del_2;    //  Удалили один делегат
        
        Если методы составного делегата возвращают значения, то в составном делегате вернется только последний return

        //------------------------------------------------------------------------
        //  ПАРАМЕТРИЗИРОВАННЫЙ КЛАСС
        class MyClass<T1,T2>    //  Параметризированный класс GENERIC. Класс с параметризированным указателем местазаполнения Т,
        {
            public T1 field;
            public T2 field_2;

            public MyClass(T1 x, T2 y)
            {
                this.field = x;
                this.field_2 = y;
            }

            public void Method()
            {
                Console.WriteLine(field.GetType());
                Console.WriteLine(field_2.GetType());
            }
            public void ParMethod<T3>(T3 arg)   //  Параметризированный метод
            {
                T3 z = arg;
            }
        }
        //  Main
        MyClass<int,double> M = new MyClass<int,double>(5,6.77);    //  Инстанцировали класс и закрыли его параметром типа int
        M.Method();

        M.ParMethod<string>("str");

        //  КОвариантность обобщения -   Отношение сверху вниз, UPCAST параметра типа <out T>
        //  КОТНТРвариантность  -   Отношенин снизу вверх, DOWNCAST параметра типа <in T>
        //  ИНвариантность  -   Отношения в обе стороны, могут быть интерфейсы или делегаты

        //------------------------------------------------------------------------
        //  ОГРАНИЧЕНИЯ НА ПАРАМЕТРИЗИРОВАННЫЙ КЛАСС   
        class MyClass<T> where T : class // Ограничение на тип как класс 
        {
        }
        class MyClass2<T> where T : struct //   Ограничение на тип как сруктура
        {
        }
        class MyClass3<T> where T: MyClass<string> //  Ограничение на определенный класс
        {
        }
        class MyClass4<T,U,R> where T : U  //  Ограничение, где два типа совпадают
        {
        }
        //  Main
        MyClass<string> x = new MyClass<string>();
        MyClass2<int> y = new MyClass2<int>();
        MyClass3<MyClass<string>> z = new MyClass3<MyClass<string>>();
        MyClass4<int, object, int> c = new MyClass4<int, object, int>();

        //------------------------------------------------------------------------
        //  СОБЫТИЯ
        public delegate void EventDelegate();   //  Делегат, который будет переделан в событие
        public class MyClass    //  Класс
        {
            public event EventDelegate MyEvent = null;  //  Делаем из делегата событие, и зануляем его для дальнейшей арифметики
            public void InvokeEvent()   //  Метод выполнения события
            {
                MyEvent.Invoke();
            }
            EventDelegate MyEvent2 = null;  //  Еще одно событие  
            public event EventDelegate MyEvent2//  Методы доступа с контролем подписки и отписки обработчиков
            {
                add { MyEvent2 += value; }  //  Можно дописать проверку
                remove { MyEvent2 -= value; }
            }
        }
        static private void Handler1()  //  Обработчик события 1
        {
            Console.WriteLine("Обработчик 1");
        }
        static private void Handler2()  //  Обработчик события 2
        {
            Console.WriteLine("Обработчик 2");
        }
        //  События могут быть абстрактными, виртуальными и соответственно переопределяемыми
        //  Main
        MyClass instance = new MyClass();
        instance.MyEvent += new EventDelegate(Handler1);    //  Создаем экземпляр класса делегата, сообщаем с ним метод обработчик и подписываем этот обрабочик на событие
        instance.MyEvent += Handler2;   // Добавление еще одного обработчика
        instance.MyEvent -= Handler2;   // Открепление обработчика
        instance.MyEvent += delegate { Можно подписывать анонимный метод, однако отписать его не получится };
        instance.InvokeEvent(); //  Вызов метода вызова события

        //------------------------------------------------------------------------
        //  МНОГОПОТОК
        static void SecondThreadMethod()  //  Метод, который планируется выполнять асинхронно
        {
            while (true)
            {
                Console.WriteLine("         :second thread work");
            }
        }

        static void STMethod_withPar(object some)   //  Метод для потока, который ожидает аргумент
        {
            Console.WriteLine(some);
        }
        //  Main
        ThreadStart ThreadDel = new ThreadStart(SecondThreadMethod);    //  Создали делегата, которому подписали на метод
        Thread Th = new Thread(ThreadDel);  //  Создали поток, которому передали делегата
        Thread Th = new Thread(SecondThreadMethod);  //  То же самое, только через предположение делегата(напрямую)
        Th.Start(); //  Запускаем поток асинхронно главному
        while (true)   //  Задача выполняется в певичном потоке
        {
            Console.WriteLine("primary thread work");
        }
        //CLR каждому потоку назначает свой стек в 1МБ, в каждом потоке создаются свои экземпляры переменных, поэтому даже статические методы выполняются как два разных метода
        ParameterizedThreadStart TD = new ParameterizedThreadStart(STMethod_withPar);   //  Делелгат для параметризированного потока
        Thread Th_2 = new Thread(STMethod_withPar);  //  То же самое, только через предположение делегата(напрямую)
        Th_2.Start(5);
        //  Если мы хотим передать много аргумнтов в поток, надо создать класс или структуру с набором аргументов и в выполняемом методе ее разложить и обработать
        //  На построенин потоков затрачивается какое-то время
        Thread Th_3 = new Thread(delegate () { Console.WriteLine("Вложеный анонимныq метод в асинхронном потоке"); });
        Th_3.Start();
        //  Программа по умолчанию ждет завершния всех потоков, поэтому если какой-то поток бесконечен, и в определенный момент работу программы надо завершить
        //  ВСЕ ВСЕХ ЖДУТ
        Th_3.IsBackground = true;   //  По завершении работы первичного потока, вторичный вырубается

        //  Критические секции для эксклюзивного доступа для потока , все оставльные потоки ждут конца выполнения секции 
        object locker = new object();    //  Объект синхронизации доступа к разделяемому ресурсу
        lock (locker)  //  Критическая секция, безопасный вариант кода
        {
            //---Работа с разделяемым ресурсом(консоль, файл, база данных)
        }
        Monitor.Enter(locker);   //  Аналог
        //-------
        Monitor.Exit(locker);

        //------------------------------------------------------------------------
        //  ОБРАБОТКА ИСКЛЮЧЕНИЙ
        class ExceptionDerived : Exception
        {
            //  Пользовательский класс исключения, наследник от базового класса исключения
        }
        //  Main
        ExceptionDerived ex = new ExceptionDerived();
        try
        {
            throw ex;   // Кинуть исключение
            throw new Exception("Исключение по слабой ссылке");
            int x = 9 / 0;  //  Ошибка
            Console.WriteLine("{0}, Этот выводе не выполнится, так как после ошибки код пойдет сразу в catch", x);

            Action ol = null;    //  Лямбда рекурсия для StackOverflowExeption
            ol = () =>{
                Console.WriteLine("Lambda recursion");
                ol.Invoke();
            };
            ol.Invoke();

            Action me = null;   //  Лямбда рекурсия для OutOfMemoryException
            me = () => {
                int[] arr = new int[1000000];
                Console.WriteLine(arr);
                me.Invoke();
            };
            me.Invoke();
        }
        catch (ExceptionDerived ed) //  Блоки для отлова исключений нужно размещать по убывающей вниз от класса наследника к базовому
        {
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Finally");
            //  Блок выполнится всегда после обработки исключения,
            //  1.  НО только если ошибка не StackOverflowExeption, в этом случае не сработает даже блок catch
            //  2.  Или если в catch бросить еще одно исключение или попасть в бесконечный цикл
        }
        //------------------------------------------------------------------------
        //  ГЛУБОКОЕ И ПОВЕРХНОСТНОЕ КЛОНИРОВАНИЕ
        object a = new object();
        Object b = new Object();

        Console.WriteLine("{0}-{1}",a.GetHashCode(),b.GetHashCode());
        Console.WriteLine(a.Equals(b)); //  У двух одинаковых объектов разные хэши, поэтому они не идентичны

        a = b;
        Console.WriteLine(a.Equals(b)); //  А теперь идентичны

        //  Переопределять методы Object.Equals и GetHashCode нужно вместе
        //  Глубокое клонирование, это когда создается отдельный аналогичный экземпляр a.MemberwiseClone(),
        //  Поверхностное копирование (ассоциация) это когда просто создается ссылка на объект
        //  Копирование объекта происходит в разы быстрее, так как конструктор нового объекта не вызывается, а просто копируется область памяти копируемого объекта

        Stopwatch timer = new Stopwatch();  // Таймер 
        timer.Start();
        //  some code to ,measure
        timer.Stop();
        Console.WriteLine(timer.ElapsedMilliseconds);
        
        //------------------------------------------------------------------------
        //  ПЕРЕГРУЗКА ОПЕРАТОРОВ
        class Point
        {
            private int x;
            private int y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public string Show() { return "x: " + x + " y: " + y; }

            public static Point operator +(Point a, Point b)    //  Перегрузка оператора для класса (открытый и статический метод)
            {
                return new Point(a.x + b.x, a.y + b.y);
            }
        }
        //  Main
        Point p = new Point(5, 4);
        Point p2 = new Point(5, 6);
        Point p_p2 = p + p2;    //  Использование перегруженного оператора
        Console.WriteLine(p_p2.Show());

        //------------------------------------------------------------------------
        //  АНОНИМНЫЕ ТИПЫ ДАННЫХ
        class MyClass
        {
            public int field;
            public void Method()
            {
                Console.WriteLine(field);
            }
        }
        //  Main
        var inst = new { Name = "Alex", Age = 26 }; //  Анонимный тип
        Console.WriteLine(inst.Name + " " + inst.Age);
        Type type = inst.GetType();
        Console.WriteLine(type.ToString());

        var inst2 = new { My = new MyClass() };  //  Анонимый тип с экземпляром объекта класса
        inst2.My.Method();  //  Доступ к методу класса через анонимную сущность

        new { My = new MyClass { field = 1 }}.My.Method();  //  Через анонимный тип создали по слабой ссылке объект класса, изменили его поле, и выполнили метод класса

        //  LINQ    -   SQL подобный язык, который на уровне компиляции трансформируется в последовательность вызовов расширяющих методов
        var col_1 = new int[] { 1, 2, 3, 4, 5 };
        var col_2 = new int[] { 1, 2, 3, 4, 5 };

        var query = from x in col_1 //  (!!!) Выраженин запроса начинает отрабатывать ТОГДА, когда к query переменной запроса идет обращение в foreach (.), все для того чтобы выполнять запрос только тогда, когда точно нужен его результат
                    from y in col_2
                    select new  //  Операция проэкции. Получаем коллекцию перемноженных между собой элементов двух коллекций
                            {
                                X = x,
                                Y = y,
                                Prod = x * y
                            };
        foreach (var item in query) //  Полученную коллекцию перебираем, ...тут  (.) 
        {
            Console.WriteLine("{0}*{1}={2}",item.X,item.Y,item.Prod);
        }

        //------------------------------------------------------------------------
        //  ДИНАМИЧЕСКИЙ ТИП ДАННЫХ, изменяется в любой момент в любой тип. Используется везде как обычый тип, кроме:
        //  1.  Собития не поддерживают динамической типизации
        //  2.  default(dynamic)=null
        //  3.  При перегрузке операторов, хотя бы один из параметров НЕ должен быть динамическим
        //      public static Point operator +(Point p, dynamic d) { }
        //  4. Реализуется идея Ad hock полиморфизма классом dynamic, т.е. привидение к типу без инкапсюляции, как в обычном UPCAST и DOWNCAST

        dynamic variable = 1;    //  Динамическая переменная, INTELLI_SENCE не работает
        variable = "Change to string";  //  Изменили int на string

        dynamic x = 1;
        dynamic y = "3";
        dynamic c = x + y;

        //------------------------------------------------------------------------
        //  ПРОСТРАНСТВО ИМЕН
        //  namespaces
        using AliasforClass = Another.Class1;   //  Создание псевдонима для элемента из другого пространсва имен
        //  Main
        Another.Class1 n = new Another.Class1();    //  Доступ к элементам в другом пространстве имен через полный адрес, если не подключено в using
        //  Простанства имен могут быть вложенными друг в друга, при этом маленькая матрешка видит изнутри всех, а большая снаружи не видит внутрь =)
        AliasforClass n2 = new AliasforClass(); //  Использование псевдонима
        //  Создание DLL библиотек и их подключение двумя способами, как сторонний файл или свой проект
        //  Если подключаются две разные библиотеки, но с одинаковыми пространствами имен и стереотипов, то в папке References>ПКМ>свойства> присваем псевдоним вместо global
        //  Internal модификатор доступа, доступ возможен только из данной сборки
        //  Internal protected модификатор доступа, из данной сборки или из производного класса в другой сборке
        //------------------------------------------------------------------------
        //  ДЕРЕКТИВЫ ПРЕПРОЦЕССОРА
        //top of sheet
        #define some_name

        #region NAME
        #endregion  //  То toggle region

        #if DEBUG   //  Отдельно выполняемый участок кода в зависимости от дерективы
        Console.WriteLine("Do some, only when DEBUG mod is on");
        #endif

        #if (DEBUG && some_name)    //  Сложная логика усовий препроцессора
        Console.WriteLine("Do some, only when DEBUG and some_name");
            #elif (!DEBUG && some_name)
            Console.WriteLine("Do some, only when NOT DEBUG and some_name");
            #else
            Console.WriteLine();
        #endif

        #warning Пользовательское предупреждение
        #error Прямой запрет на билд
        //------------------------------------------------------------------------
        */
        #endregion
        //------------------------------------------------------------------------
        #region Algorithms and Data Structures
        /*
        //------------------------------------------------------------------------
        //  АЛГОРИТМЫ И СТРУКТУРЫ ДАННЫХ
        //------------------------------------------------------------------------
        Алгоритм    -   описание последовательности действий, строгое выполнение которых приводит к решению поставленной задачи за конечное число шагов
        Свойства: Дискретность, Детерминированность, Конечность, Массовость, Результативность
        Виды: Линейный, Разветвляющий, Циклический
        Временная сложность алгоритма
        Асимтотическая сложность
        O(n) время выполнения алгоритма
        Структура данных    -   программная единица, которая позоляет хранить и обрабатывать множество логически связанных данных
        
        АЛГОРИТМЫ СОРТИРОВКИ

        1.  Пузырьковая -   простой алгоритм, каждый проход по коллекции наибольший элемент "всплывает" в конец. Проход - лесенка.
        static void Swap(int [] items, int left, int right) //  Метод замены местами двух элементов
        {
            if (left!=right)
            {
                int temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
        }
        2.  Вставками   -   элементы входной последовательности просматриваются по одному, и каждый новый поступивший элемент размещается в подходящее место среди ранее упорядоченных элементов
        3.  Выбором -   на каждом шаге отыскивается наименьший элемент в неотсортированной части и устанавливается в соответствующую позицию
        4.  Слиянием    -   массива разделяется и каждая часть сортируется оттдельно
        5.  Быстрая -   случайный выбор опорного элемента, все что больше переметить вправо, что меньше влево, посторить для кажной части

        //------------------------------------------------------------------------
        1.  СПИСОК  -   последовательность элементов определенного типа разделенных запятыми
            Двусвязный и односвязный
        +   Быстрая ставка
        +   Неограниченное количество элементов
        -   Доступ друг за другом
        -   Обязательная ссылка на соседние элементы
        -   Добавленин нового элемента только в начало или в конец
        LinkedList<int> list = new LinkedList<int>();
        LinkedListNode<string> node= new LinkedListNode<string>("Hello");   //  Один узел

        //------------------------------------------------------------------------
        2.  ДИНАМИЧЕСКИЕ МАССИВЫ
        Позволяет добавлять элементы массива в любой момент, и по любому индексу
        При переполнении внутренней коллекции динамического массива происходит увеличение размера в 2 раза. Политика роста
        ArrayList arrlist = new ArrayList();

        //------------------------------------------------------------------------
        3.  СТЕК
        LIFO    -   Первый вошел, последний вышел
        Стек часто используется в рекурсии.
        PUSH - POP - PEEK
        Stack<int> stack = new Stack<int>();
        stack.Push(5);

        //------------------------------------------------------------------------
        4.  ОЧЕРЕДЬ
        FIFO    -   Первый вошел, первый вешел
        Двусвязная deque и односвязная queue
        Queue<int> queue = new Queue<int>();

        //------------------------------------------------------------------------
        5.  ДЕРЕВО
        Бинарное дерево. Каждый узел имеет не более двух потомков

        //------------------------------------------------------------------------
        6.  МНОЖЕСТВО   -   структура данных, представляет коллекцию элементов и является реализацией математического множества
        Возможность включительного(общие элементы) и исключительного(уникальная коллекция) слияния множеств, разность множеств, симметрическая разность, подмножество, круги Эйлера
        HashSet<int> many = new HashSet<int>();

        //------------------------------------------------------------------------
        7.  ХЭШ ТАБЛИЦА -   хранит пары "ключ"-"значение", возможны быстрый поиск, вставку и удаление элементов
        Hashtable hash = new Hashtable();
        hash.Add(1,"Bibo");
        Console.WriteLine(hash[1]);

        //------------------------------------------------------------------------
        8.  АВЛ-ДЕРЕВО  -   сбалансированное по высоте двоичное дерево

        //------------------------------------------------------------------------
        */
        #endregion
        //------------------------------------------------------------------------
        #region C# Professional
        /* 
        //------------------------------------------------------------------------
        //  ПОЛЬЗОВАТЕЛЬСКИЕ КОЛЛЕКЦИИ
        Итератор    -   патерн, который позволяет работать с коллекцией
        Все коллекции в .NET основаны на интерфейсах IEnumerable и IEnumerator
        
        IEnumerable:
            IEnumerator GetEnumerator() -   возвращает ссылку на объект интерфейса
        IEnumerator:
            object Current{}    -   Возвращает текущий элемент коллекции
            bool MoveNext() -   Перемещает курсор на следующий элемент коллекции
            void Reset()    -   Возврат курсора в начало коллекции
        IEnumerable<T>  -   для универсальности 

        foreach =   //  Для его работы нужно в коллекции либо определить вышесказанные интерфейсы, или создать одноименные открытые методы.
                    //  Компилятор преобразует вот в это:
            var enumerator = ((IEnumerable)collection).GetEnumerator();
            while(enumerator.MoveNext())
            {
                var element = (Element)enumerator.Current;
                Console.WriteLine(element.FieldA, element.FieldB);
            }

            ИЛИ можно использовать ключевое слово yield, переписав один интерфейс
            public IEnumerator GetEnumerator()
            {
                while (true)
                {
                    if (position < elements.Length - 1)
                    {
                        position++;
                        yeild return elements[position];
                    }
                    else
                    {
                        position = -1;
                        yeild break;
                    }
                }
            }
            
        ICollection:    //  Дополнительные удобства для пользовательской коллекции
            int Count {}    -   Скажет сколько элементов в коллекции
            bool IsSynchronized {}  -   Скажет возможна ли синхронизация для многопотока
            object SyncRoot {}  -   Объект синхронизации доступа
            void CopyTo()   -   Копирование элементов коллекции в массив начиная с индекса
        IColection<T>   -   параметризированный интерфейс, позволяющий добавлять, очищать, искать, копировать и удалять элементы коллекции

        IList   -   Списочный интерфейс
            
        //------------------------------------------------------------------------
        //  СИСТЕМНЫЕ КОЛЛЕКЦИИ
        1.  ARRAYLIST   -   Это object [] Может хранить любой объект, но при итерации все упаковывается в object
            var arrlist = new ArrayList() { "Peu!", "Mew!" };
            arrlist.Add("Chponk!");
            foreach (var item in arrlist)   //  При попытке изменить коллекцию через foreach будет исключение, так как при переборе коллекции IEnumerator проверяет свою версию и версию коллекции
            {
                //arrlist.Remove(item);   //  Ошибка
                Console.WriteLine(item);
            }
            for (int i = 0; i < arrlist.Count; i++) //  Для изменения коллекции надо пользоваться for
            {
                arrlist[i] = "b";
            }
            arrlist.Sort(); //  Qick sort
            arrlist.Sort(new CustomComparer()); //  Custom sort
            class CustomComparer : IComparer { }  //  Создаем класс наследник интерфейса сортировки и реализуем нужную логику
        
        2.  QUEUE   -   Очередь FIFO
            Queue qu = new Queue();
            qu.Enqueue("First");
            qu.Enqueue("Second");
            qu.Enqueue("Third");
            while (qu.Count > 0)
            {
                Console.WriteLine(qu.Dequeue());  //  Перебор с удалением
            }
            var el = qu.Peek(); //  Просмотр первого в очереди без его удаления
            Console.WriteLine(el);

        3.  STACK   -   Стек LIFO
            Stack st = new Stack();
            st.Push(1);
            st.Push(2);
            st.Push(3);
            while (st.Count > 0)
            {
                Console.WriteLine(st.Pop());    //  Просмотр с удалением
            }
            var el = st.Peek();
            Console.WriteLine(el);  //  Просмотр без удаления

        4.  HASHTABLE   -   словарь, ключ-значение
            Hashtable ht = new Hashtable();
            ht.Add("abc@msi.com","Bob Thornton");
            //ht.Add("abc@msi.com","Matt Daimon");   //  Ошибка, ключи должны быть уникальные
            ht["abc@msi.com"] = "Clod Jirough"; //  Если ключ сущетвует-изменение значения...
            ht["orrr@msi.com"] = "Ben Stievens";    //  ...если ключа нет-создание нового ключа-значения
            foreach (DictionaryEntry item in ht)    //  Приводим к объекту и вызываем свойство
            {
                Console.WriteLine(item.Value);
            }
            foreach (var item in ht.Values) //  Аналогично
            {
                Console.WriteLine(item);
            }
            foreach (var item in ht.Keys) //  По ключам
            {
                Console.WriteLine(item);
            }
            //  Если в таблицу добавлять два экземпляра класса, даже если из поля и состояния идентичны, все равно это будут два разных объекта, поэтому, если нужна сложная логика сравнения, следует в данных классах переписать методы Equals() и GetHashCode()
            MyClass m = new MyClass("John");
            MyClass m2 = new MyClass("John");
            (m==m2) //  false

        5.  LIST DICTIONARY -   принцип обчного массива, подходит для хранения небольшого количества элементов (до 10)
            ListDictionary ld = new ListDictionary();
            
        6.  HYBRID DICTIONARY    -   гибрид между hashtable и listdictionary, когда неизвестно сколько будет элеметов. Сама подстрваивается
            HybridDictionary hd = new HybridDictionary();

        7.  ORDERED DICTIONARY  -   словарь с размещением в порядке добавления
            OrderedDictionary od = new OrderedDictionary {
                {"avb","BOB" },
                {"ere","MIKE" },
                {"zzs","TOM" },
            };
            foreach (DictionaryEntry item in od)
            {
                Console.WriteLine(item.Key+" - "+item.Value);
            }

        7.  SORTED LIST -   словарь, отсортированный по ключу
            SortedList sl = new SortedList();
            sl[40] ="Qutie";
            sl[56] = "Wizz";
            sl[5] = "Bob";
            sl[1] = "Ann";
            foreach (DictionaryEntry item in sl)
            {
                Console.WriteLine(item.Key+" - "+item.Value);
            }

        *Specialized*
        *8. BIT ARRAY   -   битовый массив для битовых данных. Можно мутить побитовые операции с массивом
            BitArray b = new BitArray(3);
            b[0] = true;
            b[1] = false;
            b[2] = true;
            b.Length = 4;   //  Можно менять длину
            b[3] = false;
            foreach (var item in b)
            {
                Console.WriteLine(item);
            }
            BitArray b2 = new BitArray (4);
            b2[0] = false;
            b2[1] = false;
            b2[2] = true;
            b2[3] = false;

            var xorb_b2 = b.Xor(b2);    //  Побитовые операции с двумя массивами

        *9. BITVECTOR32 -  для работы с единичным 32 битным числом. Хорош для создания побитовых масок и для упаковки битов
        
        *10.NAMEVALUECOLLECTION -   как словарь, только можно под ОДНИМ ключом хранить НЕСКОЛЬКО значений
            NameValueCollection nvc = new NameValueCollection {
                { "Key","Info"},
                { "Key","Info with same key"}
            };
            foreach (var item in nvc.GetValues("Key"))
            {
                Console.WriteLine(item);
            }

        //------------------------------------------------------------------------
        //  ПОТОКИ ВВОДА-ВЫВОДА
        1.  Директории и файлы
            var dir = new DirectoryInfo(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects");  //  Связали с директорией
            if (dir.Exists) //  Если директория существует
            {
                Console.WriteLine(dir.FullName);    //  Разное инфо о деректории
            }
            var dircur = new DirectoryInfo(@".");   //  Директория в которой выполнеяется EXE
            FileInfo[] files = dircur.GetFiles("*.txt");    //  Получаем массив с файлами расширения txt
            dir.CreateSubdirectory("#Subdir");   //  Создали поддиректорию в текущем месте
            dir.CreateSubdirectory(@"#Subdir\Anothersubdir");   //  Создали поддиректорию по адресу
            Directory.Delete(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir\Anothersubdir");   //  Удалить директорию
            Directory.Delete(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir", true);   //  Удалить директорию со влеженными подкаталогами

            var file = new FileInfo(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir\file.txt");  //  По адресу прявязались к файлу
            FileStream stream = file.Create();  //  Открыли поток и закинули
            FileStream stream = file.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);  //  Создание с параметрами
            stream.Close(); //  Закрыли поток
            Console.WriteLine(file.FullName + " " + file.CreationTime);
            file.Delete();  //  Удалили файл

            StreamWriter writer = file.CreateText();    //  Создали поток записи и сзязали с файлом
            writer.WriteLine("First string");   //  Пишем...
            writer.WriteLine("Second string");
            for (int i = 0; i < 5; i++)
            {
                writer.Write(i.ToString());
                writer.Write(writer.NewLine);   //  Новая строка
            }
            writer.Close(); //  Закрыли поток
            Writer.WriteLine("text",true);  //  Дописываем

            FileStream file_2 = new FileStream(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir\file_2.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite); //  Еще один файл параметризированным способом
            StreamWriter writer_2 = new StreamWriter(file_2, Encoding.GetEncoding(1251));    //  Еще один параметризированный поток
            writer_2.WriteLine("Hi!");  //  Пишем в файл
            writer_2.Close();
            File.WriteAllText(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir\file_2.txt","Hello World");   //  Быстрый способ записи в файл
            File.AppendAllText(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir\file_2.txt", "\nHello World add+");   //  Дозапись в файл

            StreamReader reader = File.OpenText(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir\file.txt");   //  Читаем файл
            string input;
            while ((input = reader.ReadLine()) != null) //  Читваем пока не пусто
            {
                Console.WriteLine(input);
            }
            reader.Close();

            var file = new FileInfo(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir\file.txt"); //  Берем файл
            file.CopyTo(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir\Anothersubdir\filecopy.txt");   //  И копируем его

        2.  Статический класс Path для работы с путями файловой системы.
            string path = @"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir\file.txt";
            Console.WriteLine(path);
            Console.WriteLine(Path.GetExtension(path)); //  Вернет толко расширение файла

        3.  FileSystemWatcher    -   отслеживание изменения в системе
            var watcher = new FileSystemWatcher {Path = @"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\#Subdir" }; //  Создали наблюдателя
            watcher.Created += new FileSystemEventHandler(delegate { Console.WriteLine("Добавлен файл!"); });   //  Подписали лямбда-обработчик на событие создания в директории нового файла
            watcher.EnableRaisingEvents = true; //  Включили наблюдение

        4.  MemoryStream    -   поток-накопитель, в который накидываем данный, а потом все вместе можно обрабатывать
            var memory = new MemoryStream();    //  Создали поток-накопитель
            var writer = new StreamWriter(memory);  //  Поток записи
            writer.Write("[{0}] ","info");  // Через поток записи накидываем в память
            writer.Write("[{0}] ", "info++");   //  Параметризированный ввод
            writer.Write("[{0}] ", "info***");
            writer.Flush(); //  Подчищаем
            memory.Position = 0;    //  Курсор в начало
            string res = System.Text.Encoding.UTF8.GetString(memory.ToArray()); //  Поток в массив и в строку
            Console.WriteLine(res);
            writer.Close();
            memory.Close();
        
        5.  BufferStream    -   буферизованный поток-накопитель, накапливает данные и при заполнении буфера возможны действия(запись в файл)
            FileStream file = File.Create(@"Dir\log.txt");
            BufferedStream buffer = new BufferedStream(file);
            StreamWriter write = new StreamWriter(buffer);
            write.WriteLine("some...");
            //...
            buffer.Position = 0;
            write.Close();
            buffer.Close();

        6.  GZipStream  -   компрессор-упаковщик в zip файлы

        //------------------------------------------------------------------------
        //  РАБОТА С ТЕКСТОМ
        1.  System.String
            //String s = new string('-',20);
            //string s2 = String.Format("{0}*{1}","aB","bA"); //  Форматированный вид
            //string s3 = @"C:\folder";   //  Говорим компилятору игнорировать управляющие символы
            //string s4 = String.Intern(Console.ReadLine());  //  Если данная строка существует в таблице интернирования, возвращается ссылка на нее, чтобы не создавать новую строку
            //  Можно в пользовательском классе перпопределить метод ToString();
            //  Или реализовать интерфейс IFormatable

            //  Региональные культуры форматирования даты, времени, температуры, мер и весов.
            //  RegionInfo.
            //  CultureInfo.
            //  IFormattable    -   интерфейс переопределения пользовательского формата вывода

            //  Encoding    -   перекодирование

            //  StringBuilder   -   фабрика строк
            var builder = new StringBuilder();
            builder.Append("One-").Append("Two-").Append("Three-"); //  В разы быстрее обычной конкетинации
            string str = builder.ToString();

        2.  Резулярные выражения    -   формальный язык поиска и манупуляции со строками, основанный на метасимволах
            System.Text.ReqularExpressions
            
            МЕТАСИМВОЛЫ - это символы для составления Шаблона поиска.
            \d  -   Определяет символы цифр. 
            \D  -   Определяет любой символ, который не является цифрой. 
            \w  -   Определяет любой символ цифры, буквы или подчеркивания. 
            \W  -   Определяет любой символ, который не является цифрой, буквой или подчеркиванием. 
            \s  -   Определяет любой непечатный символ, включая пробел. 
            \S  -   Определяет любой символ, кроме символов табуляции, новой строки и возврата каретки.
            .   -   Определяет любой символ кроме символа новой строки. 
            \.  -   Определяет символ точки.
 
            КВАНТИФИКАТОРЫ - это символы которые определяют, где и сколько раз необходимое вхождение символов может встречаться.
            ^() -   c начала строки. 
            ()$ -   с конца строки. 
            ()+ -   одно и более вхождений подшаблона в строке.  
            *() -   любое количество

            bool issucess = false;
            const string pattern = @"\d";   //  Должна быть цифра
            var reg = new Regex(pattern);   //  Создаем регулярное выражение
            while (!issucess)   //  Пока не введена цифра не выходим
            {
                string input = Console.ReadKey().KeyChar.ToString();    //  Нажимаем клавишу
                issucess = reg.IsMatch(input);  //  Проверяем
                if (issucess)
                    Console.WriteLine("\nВведена цифра, выходим");
                else
                    Console.WriteLine("\nВведена НЕ цифра, еще раз...");
            }

            const string pattern = @"\d+";   //  В строке должна быть хотя бы одна цифра
            var reg = new Regex(pattern);   //  Регулярное выражение
            var arr = new[] { "1", "rer4", "ddf" };    //  Массив
            foreach (var item in arr)   //  Перебираем
            {
                if (reg.IsMatch(item))  //  Проходит по условию
                {
                    Console.WriteLine("Строки с символами:{0}", item);   //  Выводим
                }
            }

            Console.WriteLine(Regex.Replace("334224gfddg",@"\d","+"));  //  Заменяем цифры на +
            Console.WriteLine(Regex.Replace("Fuck YOU !!!",@"Fuck","****"));  //  Заменяем фрагмент
            Console.WriteLine(Regex.Replace("12345",@"\d",m=>(int.Parse(m.Value)+1).ToString()));   //  Нашли, в лямбду, прибавили 1

            //  "^[a-z0-9]+$"   -   строка только из букв и цифр
            //  "fuck|bitch" -   ИЛИ

        //------------------------------------------------------------------------
        //  КОНФИГУРАЦИЯ, XML, РЕЕСТР
            //  В App.config прописали настройки в виде ключ-значение
            NameValueCollection allsettings = ConfigurationManager.AppSettings; //  Создали объект настроек
            Console.WriteLine(allsettings["First"]);    //  Читаем настройки
            Console.WriteLine(allsettings[1]);
            Console.WriteLine(allsettings.Count);

            //  Изменение файлов настроек из приложения
            var confchange = new XmlDocument(); //  Создали объект документа
            confchange.Load(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\ITVDN\.NET_deep_improve\_1_Conso1e\_1_Conso1e\App.config"); //  Прописали путь к файлу настроек
            foreach (var el in confchange.DocumentElement)  //  Пребираем корень
            {
                if (!el.GetType().ToString().Equals("System.Xml.XmlComment")) //  Тут костыли от комментария, отсеиваем комментарии, так как они почему-то не приводятся к XmlElement
                {
                    XmlElement element = (XmlElement)el;    //  Кастим к XmlElement
                    if (element.Name.Equals("appSettings")) //  Зашли в ветку настроек
                    {
                        foreach (var node in element.ChildNodes)    //  Перебираем настройки
                        {
                            if (!node.GetType().ToString().Equals("System.Xml.XmlComment")) //  Повторные кастыли
                            {
                                XmlElement X = (XmlElement)node;    //  Кастим к XmlElement
                                if (X.Attributes[0].Value.Equals("First"))  //  Нашли ключ настройки
                                {
                                    X.Attributes[1].Value = "off";  //  Меняем значение настройки
                                }
                            }
                        }
                    }
                }
            }
            confchange.Save(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\ITVDN\.NET_deep_improve\_1_Conso1e\_1_Conso1e\App.config"); //  Сохраняем файл настройки
            ConfigurationManager.RefreshSection("appSettings");
            
        //------------------------------------------------------------------------
        //------------------------------------------------------------------------
        */
        #endregion
        //------------------------------------------------------------------------

        static void Main(string[] args)
        {
            #region //Main comments
            /*
            
            double x = 5.545;
            int y = (int)x;   //  Явное приведение типов EXPLICIT, в том случае, когда возможна потеря информации

            int z = 6;
            double c = z;   //  Неявное приведение типов IMPLICIT, в том случае, когда потери данных не будет
            Convert.ToInt32(string x);

            string word1 = "Hello";
            string word2 = "Word!";
            string word3 = word1 + word2;   //  Конкатенация строк
            Console.WriteLine("{0} {1}",word1,word2);   //  Маркеры подстановки
            Console.WriteLine("{0:C}",word1);   //    Маркеры форматирования

            sizeof(int);    //  Возвращает размер

            var x = 5.5;    //  Неявно типизированная переменная, сама распознается, нужна для сокрытия типов данных

            max = (true) ? (return this) : (else return)    //  Тернарный оператор

            int bool = 5; // Ошибка
            int @bool = 5; // Нет ошибки

            //  Побитовые операции
            & И
            | ИЛИ
            ^ исключающее ИЛИ
            ~ НЕ
            ~(x)+1 = -x // изменение знака
            (>>) и (<<) //  логический сдвиг
            << 1    Сдвиг на один влево это умножение на 2 в десятичной
            byte port = 0xF0;   //  1111 0000 порты
            byte mask = 0x02;   //  0000 0010 маска включения порта для работы с прибором
            port = (byte)(port | mask); //  дизъюнкция, ВКЛ прибор
            mask = 0xFD;    //  1111 1101 маска выключения порта
            port = (byte)(port & mask); //  конъюнкция, ВЫКЛ прибор

            Теорема Де Моргана 
            Исходное выражение = Эквивалентное выражение
            !A & !B =   !(A | B)
            !A & B  =   !(A | !B)
            A & !B  =   !(!A | B)   
            A & B   =   !(!A | !B)
            !A | !B =   !(A & B)
            !A | B  =   !(A & !B)
            A | !B  =   !(!A & B) 
            A | B   =   !(!A & !B) 

            //  Функции возвращают значения
            //  Процедуры на возвращают

            byte[] arr = new byte[3] { 1, 2, 6 };   //  Создали массив
            byte[] arr2 = { 4, 2, 6, 6, 5 };
            Console.WriteLine(arr.Length);
            int[][] jagged = new int[3][];  //  Зубчатый массив

            Console.ForegroundColor = ConsoleColor.Magenta; //  Цвет консоли

            static int Foo(params int[] x) {    //  Множественность аргументов, передаваемых в функцию. Создается одномерный массив
            int sum=0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += x[i];
            }
            return sum;
            }
            Foo(2,3,4,6,6,6,6,6,6,6,299);

            //  Индексатор - перегруженные через properties [квадратные скобки] для класса с массивом для удобного доступа
            class MyClass
            {
                private int[] arr = new int[10];
                public int this[int index]
                {
                    get { return arr[index]; }
                    set { arr[index] = value; }
                }
            }
            //  Main
            MyClass my = new MyCLass();
            my[5] = 6;
            my[7];

            const int x = 5;    //  Константа

            int? a = null;  //  NULLABLE переменная, отдельный тип данных, не число а просто пустота

            yield   //  оператор автоматической генерации программного кода итераторов коллекции
            yield return

            // TODO: закладка в списке задач внизу окна

            checked //  Блок контроля переполнения
            {
            //....
            }

            string a = "Ab";
            string b = "Ab";
            Console.WriteLine(a.GetHashCode()+"  "+b.GetHashCode());    //  Одинаковые хеши
            object A = new object();
            object B = new object();
            Console.WriteLine(A.GetHashCode().ToString()+"  "+B.GetHashCode().ToString());  //  Два разных объекта - два разных хэша
            object C = new object();
            A = B = C;  //  Скопировали по ссылке на С
            Console.WriteLine(A.GetHashCode().ToString()+"  "+B.GetHashCode().ToString());  //  Два одинаковых объкта - два одинаковых хеша
            //  В более сложных классах и структурах возможно переопределение методов класса object Equals() и GetHashCode() для переопределения логики сравнения объектов



            */
            #endregion
            //------------------------------------------------------------------------


            //------------------------------------------------------------------------
            //  Main
            //  Main
            //  Main
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
