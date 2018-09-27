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
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.Remoting.Messaging;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;

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
        //  СТАТИЧЕСКИЕ поля, методы, классы
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
        //  ДИНАМИЧЕСКИЙ ТИП ДАННЫХ
        //  Изменяется в любой момент в любой тип. Используется везде как обычый тип, кроме:
        //  1.  Собития не поддерживают динамической типизации
        //  2.  default(dynamic)=null
        //  3.  При перегрузке операторов, хотя бы один из параметров НЕ должен быть динамическим
        //      public static Point operator +(Point p, dynamic d) { }
        //  4.  Реализуется идея Ad hock полиморфизма классом dynamic, т.е. привидение к типу без инкапсюляции, как в обычном UPCAST и DOWNCAST
        //  5.  Выполняется намного дольше обычной переменной
        //  6.  IntelliSense не работает
        //  7.  Нельзя закрывать динамиком параметризированный интерфейс
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
        //  КОНФИГУРАЦИИ, XML, РЕЕСТР
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
        //  REFLECTIONS
        //  Процесс, во время которого программа может отслеживать и модифицировать собственную структуру во время выполнения
            
        1.  class MyClass   //  Класс для рефлексии
            {
                private int cash = 15;  //  Поля
                public string name = "Bob";  //  Поля
                public MyClass() { }    //  Конструкторы
                public MyClass(string name) { this.name = name; }    //  Конструкторы
                public string Property { get; set; }   //  Свойства
                public void Foo() { }   //  Методы
                private void Foo2() { Console.WriteLine("Закрытый метод"); }   //  Закрытые методы
                private void Foo3(string message) { Console.WriteLine(message); }   //  Закрытые методы
            }
            MyClass my = new MyClass();
            Type t = my.GetType(); //  Объект класса "волшебника", все про всех знает
            //  "Расскажи про этого!"
            //  Рассказываю...

            MethodInfo[] mi = t.GetMethods();   //  В массив получаем все методы
            MethodInfo[] mi = t.GetMethods(BindingFlags.DeclaredOnly);   //  Или определенные
            foreach (MethodInfo item in mi)
            {
                Console.WriteLine(item.Name);
            }
            FieldInfo[] fi = t.GetFields(); //  Аналогично с полями
            PropertyInfo[] pi = t.GetProperties();  //  Аналогично со свойствами
            Type[] ty = t.GetInterfaces();  //  Аналогично с интерфейсами
            ConstructorInfo[] ci = t.GetConstructors(); //  Аналогично с конструкторами

            //  H TRICS =)
            //  Как хакнуть PRIVATE метод объекта
            MethodInfo method = t.GetMethod("Foo2", BindingFlags.Instance | BindingFlags.NonPublic); //  Объект для вызова метода
            method.Invoke(my, new object[] { }); //  Вызов метода на объекте

            MethodInfo method2 = t.GetMethod("Foo3", BindingFlags.Instance | BindingFlags.NonPublic); //  Еще один объект для вызова метода
            method2.Invoke(my, new object[] { "Sayonara bustards!" });    //  Вызов метода с передачей параметров в него
            //  Изменить PRIVATE поле
            FieldInfo changer = t.GetField("cash", BindingFlags.Instance | BindingFlags.NonPublic); //  Объект доступа к полю
            Console.WriteLine(changer.GetValue(my));    //  Было денег...
            changer.SetValue(my,0); //  Меняем поле
            Console.WriteLine(changer.GetValue(my));    //  Стало денег

        2.  //  Позднее связывание сборок   -   прием, когда мы пишем приложение, которое не знает с какой DLL библиотекой оно будет работать, и соостветственно, какие в ней классы
            Assembly assembly = null;
            assembly = Assembly.Load("Libary_name");    //  Указали имя сборки для загрузки
            Type[] ty = assembly.GetTypes();    //  Получаем все типы в сборке

            Type t = assembly.GetType("Libary_name.Account");   //  Подписываемся на интересующий класс
            MemberInfo[] mi = t.GetMembers();   //  Получаем всех членов класса
            //  Далее используем для получения нужной информации

        //------------------------------------------------------------------------
        //  АТРИБУТЫ
        //  Атрибут -   класс-декоратор.
        //  Top
        [assembly: AssemblyVersion("1.0.0.0.7767")] //  Глобальные атрибуты сборки
        #define TRIAL
        #define PREMIUM

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]  //  Начальный атрибут к пользовательскому атрибуту, устанавливает возможности пользовательского атрибута
        public class MyAttrubute : Attribute { public int Count { get; set; } } //  Пользовательский атрибут с переменной

        [MyAttrubute(Count = 5)]    //  Кастомизируем класс атрибутом со значением 5
        public class MyClass
        {
            public static void Print()
            {
                int count = 1;
                var type = typeof(MyClass); //  Передаем "волшебнику" класс
                if (Attribute.IsDefined(type, typeof(MyAttrubute))) //  Если класс кастомизирован атрибутом
                {
                    var attr = Attribute.GetCustomAttribute(type, typeof(MyAttrubute)) as MyAttrubute;  //  Получаем объект атрибута
                    count = attr.Count; //  Получаем значение атрибута
                }
                string s = new string('-', count);  //  Используем значение атрибута
                Console.WriteLine(s);
            }
            //
            [Conditional("TRIAL")]  //  Атрибут работает с DEFINE 
            public static void DoSome() { Console.WriteLine("TRIAL version works"); }
            [Conditional("PREMIUM")]  //  Атрибут работает с DEFINE 
            public static void DoSome2() { Console.WriteLine("PREMIUM version works"); }
        }
        //  Main
        //MyClass.Print();  //    Метод выполнится со значением атрибута

        //MyClass.DoSome();  //  Будут работать в зависимости от DEFINE
        //MyClass.DoSome2();  //  Будут работать в зависимости от DEFINE

        //------------------------------------------------------------------------
        //  SERIALIZATION - DESERIALIZATION
        //  Процесс преобозования объекта в поток байтов, с целью сохранить его в памяти, а потом воссаздать его состояние в случае необходимости
        1.  В XML формат:
            1.  Записываются только открытые типы и члены
            2.  Класс должен иметь конструктор по умолчанию
            3.  Читаем визуально
            4.  Универсален для разных языков

        2.  В двоичный формат
            1.  Десериализация возможна только в .NET среде
            2.  Абсолютно все запаковывается
        
            [Serializable]  //  Доступен для сериализации
            class Person : IDeserializationCallback
            {
                private string name;
                private int money;

                [NonSerialized] //  Запрет на сериализацию определенному члену
                private int dna = new Random().Next();  //  Генерит одной строкой рандомный int, и НЕ сериализуется

                public string Name { get { return name; } }
                public int Money { get { return money; } }
                public int Dna { get { return dna; } }
                public Person(string name, int money)
                {
                    this.name = name;
                    this.money = money;
                }
                public void OnDeserialization(object sender)    //  Реалилзоваваем интерфейс десерализации
                {
                    this.dna = 99999999;    //  Несериализованное поле после десериализации
                }
                //  Методы  -   индикаторы паковки-распаковки
                [OnSerializing] //  Метод выполнится перед паковкой
                public void Info(StreamingContext s) { Console.WriteLine("Готовы паковать объект"); }   //  StreamingContext на вход ОБЯЗАТЕЛЕН
                [OnSerialized]  //  Метод выполнится после паковки
                public void Info2(StreamingContext s) { Console.WriteLine("Запаковали"); }
                [OnDeserializing]   //  Метод выполнится перед распаковкой
                public void Info3(StreamingContext s) { Console.WriteLine("Готовы распаковать"); }
                [OnDeserialized]    //  Метод выполнится после распаковки
                public void Info4(StreamingContext s) { Console.WriteLine("Распаковали"); }
            }
            //  Main
            Person pers1 = new Person("Bob", 100);
            Person pers2 = new Person("Jack", 549);   //  Создали два разных объекта

            FileStream stream = File.Create(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\ITVDN\.NET_deep_improve\_1_Conso1e\_1_Conso1e\Serialize.dat");  //  Создали поток в файл
            BinaryFormatter formatter = new BinaryFormatter();  //  Создали двоичный сериализатор
            formatter.Serialize(stream, pers1);  //  В поток бросаем объект
            stream.Close(); //  Закрываем поток

            stream = File.OpenRead(@"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\ITVDN\.NET_deep_improve\_1_Conso1e\_1_Conso1e\Serialize.dat");   //  Открываем поток для чтения
            pers2 = formatter.Deserialize(stream) as Person;    //  Распаковываем в подобный объект
            //  Объект восстанавливает состояния из файла

            //  Можно осуществить интерфейс ISerializable для тонкой настройки процесса сериализации
            //  IDeserializationCallback   -   Интерфейс, в котором определяется, что делать, когда из файла восстанавливается объект, у которого на определенных полях стоял запрет не сериализацию, то-есть он скопирован не полностью

            //  [OptionalField] -   атрибут,решает проблему: если мы сериализовали объект, сменилась версия библиотеки, в которой сменился член класса, то в новой версии приложения при десериализации может возникнуть исключение, так как в старой версии объекта член отсутстует.

        //------------------------------------------------------------------------
        //  GARBAGE COLLECTOR
        //  Во время работы сборщика мусоры работа всех потоков приложения останавливается
        //  Присваивание объектам поколения жизни (0,1,2) от молодых до старых. По внутренней логике чем раньше создан объект тем он нужнее и, поэтому, дольше живет
        //  Куча для маленьких объектов и куча для больших объектов 65КВ+. Куча для больших объектов не дефрагментируется
        1.  Маркировка -   Сборщик проходит по стеку потока и маркирует объекты, на которые есть ссылки извне, то есть они еще используются
        2.  Сжатие  -   Сборщик проходит линейно в поисках непрерывных блоков немаркированных объектов, т.е. мусора. Маленькие блоки он не трогает, а в больших он перемещает вниз маркированные объекты, сжимая тем самым кучу
        
        //  Не считается хорошей практикой вручную управлять сборщиком, однако, иногда возникает необходимость в гарантированной беспрерывной работе приложения в определенный момент времени, поэтому перед отвестсвенным моментом возможно вручную запустить сборщик, чтобы он сам не запустился и не повесил приложение в ответственный момент
        //  В режиме DEBUG коллектор ждет завершения приложения для очистки, искуственно продлевая жизнь объектам, даже если ссылка по факту уже потеряна, в режиме RELEASE сразу после потери ссылки работает сборщик.
        
        class MyClass
        {
            ~MyClass()  //  Финализатор - деструктор, вызавется при удалении объекта сборщиком мусора
            {
                Console.WriteLine(this.GetHashCode() + "\t-\t" + "Удален");
            }
        }

        //  Main
        for (int i = 0; i < 10; i++)    //  Создаем 10 объектов в цикле, так как в каждом новом цикле мы затираем ссылку на предыдущий объект, то все, кроме последнего созданного в цикле объекта попадает в мусор
        {
            MyClass n = new MyClass();
            Console.WriteLine(n.GetHashCode() + "\t-\t" + "Создан");
        }
        Thread.Sleep(1000);
        GC.Collect();   //  Принудительно собираем мусор
        GC.WaitForPendingFinalizers();  //  Ждем до завершения финлизаторов всех объектов

        Console.WriteLine(GC.GetTotalMemory(false));    //  Занятых байт памяти в куче
        Console.WriteLine(GC.MaxGeneration + 1);    //  Поколений поддерживается

        //  IDisposable -   интерфейс, реализующий освобождение ресурсов объектом
        //  Один метод  -   Dispose(){};
        MyClass my = new MyClass();
        using (my)  //  Конструкция, которая гарантирует вызов Dispose() объекта, даже при исключении
        {
        }

        //------------------------------------------------------------------------
        //  ВЕРСИОННОСТЬ
        1.  Шаблон проэктирования NVI   -   Non Virtual Interface
            -   Задача  -   разделить представления интерфейса и его реализацию. Виртуальные функции объявляются protected, а их вызов происходит внутри обычных функций, которые представляются пользователю. Так, при изменении библиотеки базовых классов, пользователям библиотеки ничего не надо менять в коде.
        2.  Полиморфизм
            class Base
            {
                public virtual void Foo() { Console.WriteLine("Base Foo"); }
                public virtual void Foo_2() { Console.WriteLine("Base Foo_2"); }
            }
            class Derrived : Base
            {
                public new void Foo() { Console.WriteLine("Derrived Foo"); }    //  Перекрытие метода
                public override void Foo_2() { Console.WriteLine("Derrived Foo_2"); }   //  Переопределение метода
            }
            //  Main
            Base b = new Base();    //  Чистый базовый класс
            b.Foo();    //  "Base Foo"
            b.Foo_2();  //  "Base Foo_2"
            Derrived d = new Derrived();    //  Чистый наследник
            d.Foo();    //  "Derrived Foo"
            d.Foo_2();  //  "Derrived Foo_2"
            Base bd = d;    //  Приведенный к базе наследник
            bd.Foo();   //  Выполнится оригинальная версия из базового класса "Base Foo"
            bd.Foo_2(); //  Выполнится переопределенная версия "Derrived Foo_2" !!!
       
        3.  AdHoc полиморфизм
            class Class1
            {
                public void Method()
                {
                    Console.WriteLine("Class1");
                }
            }
            class Class2
            {
                public void Method()
                {
                    Console.WriteLine("Class2");
                }
            }
            interface IInterface    //  Воздаем связной интерфейс
            {
                void Method();
            }
            class MyClass1 : Class1, IInterface { } //  Наследуеся от классов и интерфейса, автоматически определяя методы интерфейса
            class MyClass2 : Class2, IInterface { }
            //  Main
            IInterface[] arr = { new MyClass1(),new MyClass2()};    //  Работаем как с однородными объектами 
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].Method();
            }

        4.  Позднее свызывание
            //  Main
            dynamic[] darr = { new Class1(),new Class2()};  //  Аналог предыдущего примера, только просто с dynamic структурой
            foreach (var item in darr)
            {
                item.Method();
            }
            
        //------------------------------------------------------------------------
        //  МНОГОПОТОК
        1.  Из Essential
        static void SecondThreadMethod()  //  Метод, который планируется выполнять асинхронно
        {
            while (true)
            {
                Console.WriteLine(":second thread work");
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
        //  Если мы хотим передать много аргументов в поток, надо создать класс или структуру с набором аргументов и в выполняемом методе ее разложить и обработать
        //  На построенин потоков затрачивается какое-то время
        Thread Th_3 = new Thread(delegate () { Console.WriteLine("Вложеный анонимный метод в асинхронном потоке"); });
        Th_3.Start();
        //  Программа по умолчанию ждет завершения всех потоков, поэтому если какой-то поток бесконечен, и в определенный момент работу программы надо завершить
        //  ВСЕ ВСЕХ ЖДУТ
        Th_3.IsBackground = true;   //  По завершении работы первичного потока, вторичный вырубается

        //  Критические секции для эксклюзивного доступа для потока , все остальные потоки ждут конца выполнения секции 
        object locker = new object();    //  Объект синхронизации доступа к разделяемому ресурсу
        lock (locker)  //  Критическая секция, безопасный вариант кода
        {
            //---Работа с разделяемым ресурсом(консоль, файл, база данных)
        }

        Monitor.Enter(locker);   //  Аналог
        //-------
        Monitor.Exit(locker);

        2.  Из Professional

        th.Join();  //  Главный поток останавливается и ждет завершения потока th
        public static void Print()  //  Бесконечный метод для потока
        {
            while (true)    //  Блок обработки для потока
            {
                try
                {
                    Console.Write("Second thread work ");   //  Поток работает
                }
                catch (ThreadAbortException ex) //   Из Main пришел Abort()
                {
                    Console.WriteLine("Thread aborted");    // Делаем что нужно и поток умирает
                    //Thread.ResetAbort();  //  Либо не умерает и продолжает работу
                }
            }
        }
        //  Main
        Thread th = new Thread(Print);
        th.Start();
        Thread.Sleep(3000);
        th.Abort(); //  Кидаем исключение в поток

        th.IsBackground = false;    //  Основной поток ждет завершения th потока. По умолчанию
        th.IsBackground = true; //  th мрёт с основным потоком

        th.Priority = ThreadPriority.Highest;   //  Изменение приоритета для потока, для распределения процессорного времени больше или меньше, по возможности

        Thread.CurrentThread.ManagedThreadId    //  Порядковый номер потока

        3.  Разделяемый ресурс

        static public long counter; //  Счетчик
        static public void Counter()    //  Прибавитель счетчика
        {
            for (int i = 0; i < 100; i++)
            {
                //Program.counter++;    //  Обычный инкремент
                Interlocked.Increment(ref counter); //  Инкремент с разделением ресурса

                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " " + counter);
                Thread.Sleep(10);
            }
        }
        //  Main
        //  10 потоков одновременно обращаются к одному ресурсу в обычном режиме, поэтому может возникнуть ситуация одновременного использования ресурса счетчика, и поэтому неправильной работы
        //  Для правильного разделения можно использовать Interlocked.
        var tharr = new Thread[10];
        for (int i = 0; i < tharr.Length; i++)
        {
            tharr[i] = new Thread(Counter);
            tharr[i].Start();
        }
        Console.WriteLine(counter); //  При неразделении ресурса ожидаемой "1000" мы не увидим
        
        4.  
        static int stop;    //  Флаг работы потока
        //  Main
        Console.WriteLine("Main:");
        Thread.Sleep(2000);

        Thread th = new Thread(delegate ()
        {
            while (Thread.VolatileRead(ref stop)!=1)    //  Пока флаг горит, работает поток
            {
                Console.WriteLine("Thread");
            }
        });
        th.Start();

        Thread.Sleep(1500);
        Thread.VolatileWrite(ref stop, 1);  //  Спецобращение к переменной для видимости ее всеми потоками. Изменяем флаг работы
        Console.WriteLine("Thread остановлен");

        5.  Пул потоков
        public static void ShowPoolInfo()   //  Инфа о пуле потоков
        {
            int AvailableworkerThreads, AvailablecompletionPortThreads, MaxworkerThreads, MaxcompletionPortThreads;
            ThreadPool.GetAvailableThreads(out AvailableworkerThreads, out AvailablecompletionPortThreads);
            ThreadPool.GetMaxThreads(out MaxworkerThreads, out MaxcompletionPortThreads);
            Console.WriteLine(
                "Доступно потоков в пуле: {0} из {1}\n" +
                "Доступно потоков ввода-вывода в пуле: {2} из {3}",
                AvailableworkerThreads, AvailablecompletionPortThreads,
                MaxworkerThreads, MaxcompletionPortThreads);
        }
        public static void Task(Object state)   //  Задача для отдельного потока. Объект в аргумент обязательно
        {
            Thread.Sleep(3000);
        }
        //  Main
        ShowPoolInfo();
        ThreadPool.QueueUserWorkItem(Task); //  Закидываем метод в пул потоков, он начинает выполняться в отдельном потоке.
        ThreadPool.QueueUserWorkItem(Task); //  Еще один...
        ThreadPool.QueueUserWorkItem(Task); //  И еще один...
        ShowPoolInfo(); //  По мере выполнения, менеджер потоков сам освобождает и распределяет потоки

        6.  Объекты синхронизации уровня ядра
            1.  MUTEX   -   блокировка критической секции на уровне потока ядра
            private static readonly Mutex mutex = new Mutex(false, "Mutex_hash_0450054");    //  Создали межпроцессовый объект синхронизации
            public static void Method() //  Метод работы с критической секцией
            {
                bool mymutex = mutex.WaitOne(); //  Закрываем критическую секцию для одного потока ядра
                //  Критическая секция
                Console.WriteLine("Поток " + Thread.CurrentThread.GetHashCode() + " получил доступ к ресурсу и работает с ним");
                Thread.Sleep(1500);
                Console.WriteLine("Поток " + Thread.CurrentThread.GetHashCode() + " освобождает секцию");
                //  Критическая секция
                mutex.ReleaseMutex();   //  Снимаем блокировку секции
            }
            //  Main
            var tharr = new Thread[5];  //  Создали потоки
            for (int i = 0; i < tharr.Length; i++)
            {
                tharr[i] = new Thread(Method);  //  Запустили все потоки для работы с критической секцией
                tharr[i].Start();
            }
            //  Mutex-блокировка разрешает работу с секцией только одному потоку на уровне системы, т.е даже при многократном запуске приложения гарантируется правильный доступ к секции только одному потоку на уровне ядра
        
            2.  SEMAPHORE   -   Предоставляет пулл из потоков
            static Semaphore pool;
            static void Work(Object number)
            {
                pool.WaitOne(); //  Точка доступа семафора, разрешает вход определенному числу потоков
                Console.WriteLine("Поток " + number + " занял слот семафора и работает с ним");
                Thread.Sleep(2000);
                Console.WriteLine("Поток " + number + " освободил слот семафора");
                pool.Release(); //  Точка выхода для потока семафора
            }
            //  Main
            pool = new Semaphore(1, 10, "Semaphore_hash_034495");  //  Создали семафор, с изначально одним рабочим потоком из пулла 10 потоков
            for (int i = 0; i < 10; i++)    //  Генерим 10 потоков
            {
                Thread th = new Thread(Work);   //  Запускаемся
                th.Start(i);
            }
            Thread.Sleep(6000);
            //  В течении 6 секунд семафор будет разрешать работу только одному потоку
            pool.Release(9); //  Сбрасываем ограничение на остальные 9 потока
                             //  После чего все 10 симафорных потоков работают одновременно

            //  SEMAPHORE slim  -   легковесная реализация, без блокировки на уровне ядра, но в остальном аналогична

            3.  AUTORESETEVENT  -   Уведомление ОДНИМ потоком ДРУГОГО потока о неком собитии
            static readonly AutoResetEvent auto = new AutoResetEvent(false);
            static void Function()
            {
                while (true)
                {
                    auto.WaitOne(); //  Ждет сигнального состояния 
                    Console.WriteLine("Пришел сигнал, поток выполняет");
                }
            }
            //  Main
            Console.WriteLine("Нажмите кнопку для перевода AutoReset в сигнальное состояние");
            var th = new Thread(Function);  //  Запускается паралельный поток
            th.Start();

            while (true)
            {
                Console.ReadKey();  //  Нажимается любая кнопка
                auto.Set(); //  Сигнал, пралелельный поток ловит сигнал и продолжает работу
            }

            4.  MANUALRESETEVENT
            //  Один сигнал, много ожидающих
            static ManualResetEvent manual = new ManualResetEvent(false);
            static void Function_1()
            {
                while (true)
                {
                    Console.WriteLine("Поток 1 запущен и ждет сигнала");
                    manual.WaitOne();   //  Ждем сигнала
                    Console.WriteLine("Поток 1 отработал");
                    Thread.Sleep(20);
                    manual.Reset(); //  Переводим сигнал в ВЫКЛ
                }
            }
            static void Function_2()
            {
                while (true)
                {
                    Console.WriteLine("Поток 2 запущен и ждет сигнала");
                    manual.WaitOne();   //  Ждем сигнала
                    Console.WriteLine("Поток 2 отработал");
                    Thread.Sleep(20);
                    manual.Reset(); //  Переводим сигнал в ВЫКЛ
                }
            }
            //  Main
            Thread[] tharr = { new Thread(Function_1), new Thread(Function_2) };
            foreach (Thread item in tharr)
            {
                item.Start(); 
            }
            while (true)
            {
                Console.WriteLine("Нажмите кнопку чтобы послать сигнал");
                Console.ReadKey();
                manual.Set();  //  Посылаем сигнал
            }

            //  MANUALRESETEVENTslim -  облегченный аналог, не обращающийся к блокировке на уровне ядра

            5.  EVENTWAITHANDLE -   глобальное событие
            static EventWaitHandle globalmanual = null;
            //  Main
            globalmanual = new EventWaitHandle(false, EventResetMode.ManualReset, "GlobalEvent_hash");  //  Создаем глобальное событие
            Thread th = new Thread(delegate(){  //  Поток
                globalmanual.WaitOne(); //  Ждет события
                while (true)
                {
                    Console.WriteLine("Hello!");
                }
            });
            th.IsBackground = true;
            th.Start();
            Console.WriteLine("Нажмите кнопку чтобы отправить глобальное событие");
            Console.ReadKey();
            globalmanual.Set(); //  Отправляет глобальное событие
            //  Даже если в системе работает несколько приложений, создается глобальный сигнал ядра, и все кто его ожидает начинают работу

            6.  Множественность событий
            static WaitHandle[] events = new WaitHandle[] { new AutoResetEvent(false), new AutoResetEvent(false), };    //  Массив событий
            static void Task_1(Object state)
            {
                var auto = state as AutoResetEvent;
                Console.WriteLine("Метод 1 выполняется...");
                Thread.Sleep(2000);
                Console.WriteLine("Метод 1 готов!");
                auto.Set(); //  Сигналим готовым первым методом
            }
            static void Task_2(Object state)
            {
                var auto = state as AutoResetEvent;
                Console.WriteLine("Метод 2 выполняется...");
                Thread.Sleep(5000);
                Console.WriteLine("Метод 2 готов!");
                auto.Set(); //  Сигналим готовым вторым методом
            }
            //  Main
            Console.WriteLine("Главный поток ждет завершения обеих задач");
            ThreadPool.QueueUserWorkItem(Task_1, events[0]);    //  В пулл кидаем первую задачу с первым событием
            ThreadPool.QueueUserWorkItem(Task_2, events[1]);    //  и вторую задачу со вторым событием
            //  Задачи выполняются...
            WaitHandle.WaitAll(events); //  Главный поток ждет пока ОБА события не сработают
            Console.WriteLine("Обе задачи завершены, главный поток идет дальше");

            WaitHandle.WaitAny(events); //  Главный поток ждет пока ЛЮБОЕ ИЗ событий не сработет
            Console.WriteLine("Какая-то из задач готова, главный поток идет дальше");

        7.  Асинхронная модель
        static void AsyncMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Работает асинхронный метод_");
                Thread.Sleep(500);
            }
        }
        //  Main
        var del = new Action(AsyncMethod);  //  Создаем делегат с методом
        IAsyncResult asres = del.BeginInvoke(null, null);   //  Через объект интерфейса запускаем делегата в асинхронном потоке
        //  Метод начинает выполняться асинхронно с главным потоком
        for (int i = 0; i < 5; i++)    //  Работа главного потока
        {
            Console.WriteLine("Работает main_");
            Thread.Sleep(500);
        }   //  Главный поток заканчивает работу быстрее
        Console.WriteLine("Main завершил работу");
        while (!asres.IsCompleted)  // и пока асинхронный поток еще работает
        {
            Console.WriteLine("Main ждет асинхронного потока");
            Thread.Sleep(500);
        }
        del.EndInvoke(asres);   //  Главный поток ждет окончания асинхронного потока

        //  Аналог с обработкой результата выполнения асинхронного метода

        static int AsyncMethod(int x, int y)
        {
            Console.WriteLine("Работает асинхронный метод ");
            Thread.Sleep(5000);
            Console.WriteLine("Асинхронный метод отработал ");
            return x * y;
        }
        static void AsyncComplete(IAsyncResult Res) //  Метод, который запуститься после отработки асинхронного метода в том же асинхронном потоке
        {
            //  Получение экземпляра делегата, на котором была вызвана асинхронная операция
            var result = Res as AsyncResult;
            var caller = (Func<int, int, int>)result.AsyncDelegate;
            //  Получение результата из асинхронного метода
            int mult = caller.EndInvoke(result);
            //  Получение строкового аргумента из вызова асинхрона и привидение его в маску вывода
            string resmask = string.Format(Res.AsyncState.ToString(), mult);
            Console.WriteLine("Метод-обработчик. Результат работы асинхроного метода: " + resmask);
        }
        //  Main
        var del = new Func<int, int, int>(AsyncMethod);  //  Создаем делегат с методом и тремя генериками, два на аргументы, один на вывод
        var callback = new AsyncCallback(AsyncComplete);    //  Создаем делегат метода-обработчика
        var asres = del.BeginInvoke(7, 8, callback, "a * b = {0}"); //  Запускаемся в асинхроне, передавая аргументы в метод, делегат обработчика и строковую маску вывода как аргумент.
        //  Метод начинает выполняться асинхронно с главным потоком
        Console.WriteLine("Работает main ");
        Thread.Sleep(2000);
        Console.WriteLine("Main завершил работу");

        //  Пример использования для чтения-записи
        static void Confirmation(IAsyncResult Res)  //  Метод-обработчик 
        {
            Console.WriteLine("Файл записан");
            var stream = Res.AsyncState as FileStream;
            if (stream != null)
                stream.Close(); //  Закрываем поток
        }
        //  Main
        var stream = new FileStream("File.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);   //  Создаем поток
        byte[] arr = { 1, 2, 3, 4, 5, 6, 7 };   //  Создаем массив, который будем записывать
        stream.BeginWrite(arr,0,arr.Length,Confirmation,stream);    //  В асинхроне начинаем писать и передаем обработчик

        8.  Task parallel library   -   современная удобная реализация многопотока
        static void Method()
        {
            Console.WriteLine("Метод " + Task.CurrentId + " в паралельном потоке начал работать");
            Thread.Sleep(1000);
            Console.WriteLine("Метод " + Task.CurrentId + " в паралельном потоке закончил работать");
        }
        static void NextMethod(Task t)  //  Следующая задача, или задача-обработчик
        {
            Console.WriteLine("Следующая задача в паралельном потоке начала работу");
            Thread.Sleep(500);
            Console.WriteLine("Следующая задача в паралельном потоке закончила работу");
        }
        static int Mult5(object x)   //  Задача с входными-выходными параметрами
        {
            int y = (int)x; //  Распаковываем 
            Thread.Sleep(1000);
            return y * 5;
        }
        static void CancelMethod(object o)  //  Метод, который можно отменить в поцессе выполнения
        {
            var cancel = (CancellationToken)o;
            cancel.ThrowIfCancellationRequested();
            for (int i = 0; i < 10; i++)
            {
                if (cancel.IsCancellationRequested)
                {
                    Console.WriteLine("Метод отменен");
                    cancel.ThrowIfCancellationRequested();
                }
                Thread.Sleep(100);
                Console.WriteLine("Метод работает");
            }
            Console.WriteLine("Метод завершен");
        }
        static void SomeMethod()    //  Метод для паралельного потока
        {
            Console.WriteLine("Метод в потоке " + Thread.CurrentThread.GetHashCode() + " начал выполняться");
            for (int i = 0; i < new Random().Next(1, 10); i++)
            {
                Console.WriteLine("Метод в потоке " + Thread.CurrentThread.GetHashCode() + " работает");
                Thread.Sleep(new Random().Next(200, 800));
            }
            Console.WriteLine("Метод в потоке " + Thread.CurrentThread.GetHashCode() + " закончил выполняться");
        }
        //  Main
        Task T = new Task(Method);
        Task L = new Task(() => { Console.WriteLine("Lambda задача"); });
        Task T2 = new Task(Method);
        Task C = T.ContinueWith(NextMethod);    //  Задача-продолжение начинает выполняться после выполнения основной
        T.Start();
        T2.Start(); //  Так называемая холодная задача. Сначала создали а потом запустили

        T.Wait();   //  Main ждет завершения
        T2.Wait();
        C.Wait();

        Task.WaitAll(T, T2); //  Аналог
        int whofirst = Task.WaitAny(T, T2);    //  Ждем первую из, возвращает индекс первой завершенной задачи

        Task TF = Task.Factory.StartNew(Method);    //  Горячая задача, сразу запускается

        Task<int> TF2 = Task<int>.Factory.StartNew(Mult5, 2);    //  Запуск задачи с аргументами
        Console.WriteLine("Результат выполнения задачи с параметрами: " + TF2.Result);    //  Получаем результат

        var canceltok = new CancellationTokenSource();  //  Создали объект отмены
        Task TF3 = Task.Factory.StartNew(CancelMethod, canceltok.Token, canceltok.Token); //  Запустились
        Thread.Sleep(500);  //  Дали поработать мэину
        canceltok.Cancel(); //  Отправили запрос на отмену
        TF3.Wait();

        Parallel.Invoke(SomeMethod, SomeMethod, SomeMethod); //  Выполнение методов в паралельных потоках
        var options = new ParallelOptions   //  Настройки количества потоков паралельного запуска
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount > 2 ? Environment.ProcessorCount - 1 : 1
        };  //  Если процессоров в системе больше двух, включаем на 1 меньше, иначе просто один
        Parallel.Invoke(options, SomeMethod, SomeMethod, SomeMethod, SomeMethod);   //  Запуск с настройками
        Parallel.Invoke(() => { Console.WriteLine("Lambda 1"); }, () => { Console.WriteLine("Lambda 2"); });    //  Лямбды в паралельный поток

        Parallel.For(); //  Выполнение цикла for в многопотоке

        9. Async & Await
        //  Когда выполняется долгая работа, процесс простаивает ожидая конца ее выполнения. Если таких задач много, то, рано или поздно, свободные процессы все будут заняты и приложение будет простаивать.
        //  Поэтому данная конструкция, перекидывает ресурсы с бесполезного ожидания на полезную работу
        static void Method()    //  Обычный синхронный метод
        {
            Console.WriteLine("Метод начал работу");
            Thread.Sleep(3000);
            Console.WriteLine("Метод закончил работу");
        }
        async static Task AsyncMethod() //  Асинхронный метод
        {
            Console.WriteLine("Async метод начал работу");
            await Task.Factory.StartNew(Method);    //  Вызывает обычный метод
            //  Так как возвращаемое значение типа Task, return можно не указывать, оно само генерится awaitом
            Console.WriteLine("Async метод закончил работу");
        }
        //  Main
        Method(); //  Если запустить обычный синхронный метод, главный поток подвиснет до конца выполнения метода
        Task t = AsyncMethod(); //  Запускаем асинхронный метод.
        while (!t.IsCompleted)  //  Главный поток продолжает работу и не блокируется, до тех пор пока не отработает асинхронный поток
        {
            Thread.Sleep(500);
            Console.WriteLine("Main активен");
        }
        //------------------------------------------------------------------------
        //  ФУНКЦИОНАЛЬНОЕ ПРОГРАММИРОВАНИЕ

        1.  Замыкание   -   когда переменные объявлены вне тела функции, которая работает с ними
        int x = 5;  //  Внешняя переменная
        Func<int, int> fu = (y) => y + x;   //  Лямбда
        fu.Invoke(5);   //  10

        static void Foo(Func<int> counter)  //  Метод принимает лямбду
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(counter.Invoke());    //  И вызывает ее 10 раз
            }
        }
        //  Main
        int x = 0;
        Foo(() => x++); //  Передаем в метод лямбду
        Console.WriteLine(x);   //  Лямда меняет внешнюю переменную

        //  Main
        int[] arr = new int[10];    //  Массив
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(20);
            arr[i] = new Random().Next(1, 100); //  Набиваем рандомом
        }
        foreach (var item in arr)
        {
            if (new Func<int, bool>(x => x > 50).Invoke(item)) //  Одной строкой создаем лямбда-предикат.Кидаем туда элемент массива.
            {
                Console.WriteLine(item);
            }
        }
        //  Есть возможность создавать блоки из лямбда выражений и из использовать

        2.  Мемоизация  -   оптимизационная методика для исключения повторного вычисления результатов предыдущих вызовов, тем самым повышения производительности программы в геометрической прогрессии
        public static class Memoizer    //  Метод расширения для мемоизации лямбды фибоначи
        {
            public static Func<T, R> Memoize<T, R>(this Func<T, R> func)    //  Метод принимает функцию, которая один аргумент принимает и один возвращает, и такую же функцию возвращает. То есть модифицирует обычную функцию в мемоизованную
            {
                var cache = new Dictionary<T, R>(); //  Создаем словарь для кеширования
                return x =>
                {
                    R result = default(R);
                    if (cache.TryGetValue(x, out result))   //  Если в словаре уже есть рассчитанное значение для числа, возвращаем его
                        return result;
                    result = func(x);   //  Если нет, вычисляем
                    cache[x] = result;  //  Записываем
                    return result;  //  И возвращаем
                };
            }
        }
        //  Main
        Func<Int32, Int64> fibo = null;
        fibo = (x) => x > 1 ? fibo(x - 1) + fibo(x - 2) : x;    //  Рекурсивная функция рассчета числа фибоначи.
        //  Каждый раз при вызове на очередном числе происходит повторный рассчет предыдущий чисел, т.е. однообразная работа
        fibo = fibo.Memoize();    //  Перезаписываем функцию расширяющим методом

        for (int i = 0; i < 99; i++)    //  Вычисляем фибоначи
        {
            Console.WriteLine(i+" "+fibo(i));
        }

        3.  Каррирование и частичное применение функции

        //------------------------------------------------------------------------
        //  МЕЖПРОЦЕССОРНОЕ ВЗАИМОДЕЙСТВИЕ
        Inter Process Communication
        //------------------------------------------------------------------------
        */
        #endregion
        //------------------------------------------------------------------------
        #region ADO.NET
        /* 
        //------------------------------------------------------------------------
        //  ADO.NET
        //  Предоставляет различные службы для доступа к базам данных
        
        1.  Строка подключения, открытие-закрытие соединения.
        //В проэкт добавляем базу данных. Через ПКМ-Свойства смотрим строку подключения
        string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True; Pooling=true"; //  Явно прописываем строку соединения
        SqlConnection connection = new SqlConnection(connectionstring); //  Создаем объект соединения по строке
        Console.WriteLine(connection.Database); //  Местоположение базы
        try
        {
            connection.Open();
            Console.WriteLine(connection.State);
        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
            Console.WriteLine(connection.State);
        }
        //  Если предполагается взаимоедйствие с пользователем при построении строки соединения, то явно встраивать введенные пользователем данные опасно, нужно использовать 
        SqlConnectionStringBuilder CSB = new SqlConnectionStringBuilder();
        CSB.DataSource = @"(LocalDB)\MSSQLLocalDB";
        CSB.AttachDBFilename = @"C:\Users\YellowFive\Dropbox\MY\Visual Studio\Projects\ITVDN\.NET_deep_improve\_1_Conso1e\_1_Conso1e\Database.mdf";
        CSB.UserID = Console.ReadLine();
        CSB.Password = Console.ReadLine();
        SqlConnection connection = new SqlConnection(CSB.ConnectionString);

        Pooling=true;   //  Добавка в строку подключения, включающая пулл соединения, что позволяет не закрывать соединение когда много циклов открытия-закрытия, координально сокращая время обращения к базе данных

        2.  Команды
        DBNull.Value  //  Класс NULL из ДБ
        SqlCommand cmd_select = new SqlCommand("SELECT Name FROM Players WHERE Id=1", connection);  //  Создали команду выбора
        string res = (string)cmd_select.ExecuteScalar();   //  Выполнили команду, возвращающую один объект
        Console.WriteLine(res);

        SqlCommand cmd_insert = new SqlCommand("INSERT INTO Players VALUES (4,'Anna',NULL)", connection);   //  Команда вставки
        int rowaffected = cmd_insert.ExecuteNonQuery(); //  Выполняем команду вставки, возвращается количество измененных полей
        Console.WriteLine(rowaffected + " Rows_Affected");

        SqlCommand cmd_delete = new SqlCommand("DELETE FROM Players WHERE Id=4",connection);    //  Команда удаления
        int rowaffected = cmd_delete.ExecuteNonQuery(); //  Выполняем команду удаления
        Console.WriteLine(rowaffected+" Rows_Affected");

        SqlCommand cmd_selectall = new SqlCommand("SELECT * FROM Players", connection); //  Создаем команду выбора всего
        SqlDataReader reader = cmd_selectall.ExecuteReader();   //  Создаем ридер на выполненной команде
        while (reader.Read())   //  Пока ридер читает
        {
            for (int i = 0; i < reader.FieldCount; i++) //  Либо через for выводим все
            {
                Console.WriteLine(reader.GetName(i) + " : " + reader[i]);
            }
            Console.WriteLine("ID: {0}\tNAME: {1}\tGAMES PLAYED: {2}", reader.GetFieldValue<int>(0), reader.GetString(1), reader.GetValue(2)); // Либо через каждую запись
            Console.WriteLine(reader[0].ToString() + reader["Name"].ToString() + reader[2].ToString());  //  Либо через индексатор

        }
        reader.Close(); //  Закрываем ридер

        SqlCommand cmd_asyncdelay = new SqlCommand("WAITFOR DELAY '00:00:05'",connection);  //  Имитация длительной работы с БД
        var DBTask = cmd_asyncdelay.ExecuteNonQueryAsync(); //  Запуск в асинхроне
        while (!DBTask.IsCompleted) //  Пока работем с БД
        {
            Console.WriteLine("Main активен");
            Thread.Sleep(100);
        }

        SqlCommand cmd_transact = new SqlCommand("INSERT INTO Players VALUES(4, 'Anna', NULL);" +
                                                "UPDATE Players SET GamesPlayed=5 WHERE Name='Anna'", connection);  //  Много команд
        cmd_transact.Transaction = connection.BeginTransaction(IsolationLevel.);   //  Открываем соединение c настройкой изоляции 
        cmd_transact.ExecuteNonQuery(); //  Выполняем транзакцией все команды
        cmd_transact.Transaction.Commit();  //  Фиксируем транзакцию
        cmd_transact.Transaction.Rollback();    //  Или отменяем изменения

        3.  Data Table, Data Set  -   объектно-ориентированное представление таблицы
        SqlCommand cmd_schema = new SqlCommand("SELECT * FROM Players", connection);    //  Выбираем все
        SqlDataReader reader = cmd_schema.ExecuteReader();  //  Ридер
        DataTable schema = reader.GetSchemaTable(); //  Получаем схему таблицы из ДБ в объект локальной таблицы
        foreach (DataRow row in schema.Rows)    //  Перебираем всю схему
        {
            foreach (DataColumn col in schema.Columns)
            {
                Console.WriteLine(col.ColumnName + "\t-\t" + row[col]);
            }
            Console.WriteLine();
        }

        //  Копирование в локальный объект таблицы
        DataTable filled = new DataTable("AllDataTable");   //  Создали объект таблицы
        filled.Load(reader);    //  Скопировали из ридера все
        reader.Close();
        foreach (DataRow row in filled.Rows)    //  Перебираем  всю таблицу
        {
            foreach (DataColumn col in filled.Columns)
            {
                Console.WriteLine(col.ColumnName + "\t-\t" + row[col]);
            }
            Console.WriteLine();
        }

        DataTable data = new DataTable("Data");
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Players", connection);   //  Аналог с помощью адаптера
        adapter.Fill(data); //  Заполняем через с помощью адаптера
        foreach (DataRow row in data.Rows)
        {
            foreach (DataColumn col in data.Columns)
            {
                Console.WriteLine(col.ColumnName + "\t-\t" + row[col]);
            }
        }

        4.  Data View
        DataTable data = new DataTable("Data");
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Players", connection);   //  Аналог с помощью адаптера
        adapter.Fill(data); //  Заполняем через с помощью адаптера
        //DataView dataview = new DataView(data); //  Объект вида обычный
        DataView dataview = new DataView(data,"Id>1","Name",DataViewRowState.Unchanged); //  Объект вида с параметрами
        foreach (DataRowView viewrow in dataview)   //  Перебираем 
        {
            Console.WriteLine(viewrow["Id"].ToString() + viewrow["Name"] + viewrow[2]);
        }
        */
        #endregion
        //------------------------------------------------------------------------
        #region ENTITY FRAMEWORK 6
        /*
        //------------------------------------------------------------------------
        //  ENTITY FRAMEWORK 6
        ORM
        //  Полностью развертывает базу данных в ООП сущности для работы с ними 
        1.  DataBase First  -   Все развертывается на основе готовой БД
        //  ПКМ по проекту-добавить элемент-модель ADO.NET EDM-Подключится к нужной БД-дальше пакет все сформирует сам
        2.  Model First -   Сначала моделируем схему, а потом фрейм все развертывает на основе схемы
        3.  Code First  -   Сначала пишется код всех классов, потом на его основе генерится база 
        
        DbEntities entities = new DbEntities(); //  Создали объект развертки БД

        using (entities)  //  Блок для корректного закрытия соединения
        {
            entities.Players.Add(new Player { Name = "Gogic", GamesPlayed = 9 }); //  Добавляем поля в кеш
            entities.SaveChanges();   //  Сохраняем в БД

            Player one = new Player { Name = "Huan", GamesPlayed = 45 };
            Player two = new Player { Name = "Herule", GamesPlayed = 25 };
            entities.Players.AddRange(new List<Player> { one, two });   //  Добавить несколько записей

            var change = entities.Players.Find(9);  //  Ищем по ключу
            change.Name = "Jorick"; //  Меняем найденому объекту поле
            entities.Players.AddOrUpdate(change);   //  Апдейтим
            //entities.Entry(change).State = EntityState.Modified;  //  Еще один выриант апдейта
            entities.SaveChanges();   //  Сохраняем в БД

            var del = entities.Players.Find(9); //  Удаление
            entities.Players.Remove(del);
            entities.SaveChanges();
        }

        List<Player> list = entities.Players.ToList();  //  В лист набиваем все поля таблицы
        foreach (Player item in list)   //  Перебираем
        {
            Console.WriteLine(item.Id + item.Name + item.GamesPlayed);
        }
        
        //  LINQ
        class Person    //  Класс с именем и возрастом
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        var people = new List<Person>   //  Набиваем коллекцию
        {
            new Person { Name="Bill", Age=35},
            new Person { Name="William", Age=25},
            new Person { Name="Jenn", Age=15},
            new Person { Name="Ann", Age=5}
        };
        var result = from res in people //  В результирующую коллекцию в объект res из коллекции
                        where res.Age > 18 //  где у объекта поле по условию
                        select res.Name;   //  проэцируем объект или его определенные поля 
        foreach (var r in result)   //  Пребираем результирующую коллекцию
        {
            Console.WriteLine(r);
        }

        using (DbEntities entities = new DbEntities())
        {
            IQueryable<Player> col = entities.Players;  //  Формируем запрос
            col = col.Where(x => x.GamesPlayed > 1).OrderBy(x =>x.GamesPlayed);   //  Конфигурируем запрос

            var col = from q in entities.Players    //  То же самое только LINQ
                      orderby q.GamesPlayed
                      select q;
            foreach (var item in col)   //  И только при обращении к запросу он идет в БД
            {
                Console.WriteLine("{0}-{1}", item.Name, item.GamesPlayed);
            }

            Player p = entities.Players.FirstOrDefault(x=>x.Name=="Anna");  // Если найдет то вернет, если нет вернет null
            Console.WriteLine(p.Name+p.GamesPlayed);

            int num = entities.Players.Count(); //  Всего записей
            int num2 = entities.Players.Count(x => x.GamesPlayed > 20); //  C условием
            int sum = entities.Players.Sum(x => x.GamesPlayed.HasValue ? x.GamesPlayed.Value : 0);  //  Сумма, с проверкой на null
            int mingp = entities.Players.Min(x => x.Id);  //  Min
            int maxgp = entities.Players.Max(x => x.Id);  //  Max
            double gpavr = entities.Players.Average(x => x.Id);   //  Avarage

            int rowaffected = entities.Database.ExecuteSqlCommand("INSERT INTO Players VALUES ('Phill',44)");    //  чистый SQL запрос к базе
            var query = entities.Database.SqlQuery<Player>("Select * FROM Players");    //  чистый SQL запрос к базе
        }

        //  FLUENT API & DATA ANNOTATIONS   -   конфигурирование БД

        DbEntities entities = new DbEntities(); //  Создали объект развертки БД
        entities.Configuration.LazyLoadingEnabled = true;   // Lazy load. Отложенная загрузка, обращение к БД каждый раз при обращении к строке, не подходит для большого объема выборки

        entities.Configuration.LazyLoadingEnabled = false;
        entities.Players.Load();    //  Strict load.  Загрузка локально по требованию

        Возможно организовать автоматическую инициализацию полей при создании базы
        Возможно организовать миграцию при добавлении новой сущности в БД
        
        //------------------------------------------------------------------------
        */
        #endregion
        //------------------------------------------------------------------------
        #region DESIGN PATTERNS
        //------------------------------------------------------------------------
        /*
        //  DESIGN PATTERNS
        Патерн  -   абстрактный пример правильного использования небольшого числа комбинаций простых техник ООП
        Простые примемы, показыающие правильные способы организации взаимодействия между классами или объектами
        
            23 штуки
        5   Пораждающих
        7   Структурных
        11  Поведенческих

        ООП
        1.  Инакпсуляция
        2.  Наследование
        3.  Полиморфизм
        4.  Абстракция
        5.  Посылка сообщений
        6.  Повторное использование

        Принципы SOLID
        Single responsibility
        Open-close  -   открыт для расширения, закрыт для модификации
        Liskov  -   поведение класснов неследников не должно противоречить базовому классу
        Interface segregation   -   узкий-широкий интерфейс
        Dependency incversion   -   абстрагирование вариантов использования

        КОНЕЧНЫЙ АВТОМАТ    -   STATE MACHINE
        Мили \ Мура

        Начальное состояние
        Текущее состояние
        Входные сигналы
        Выходные сигналы
        Функции переходов
        Функции выходов

        1.  ABSTRACT FACTORY
        Oject порождающий
        Порождение семейств взаимодействующий продуктов

            Client
            Absstract Factory
            Abstract product A
            Abstract product B
            Concrete product A
            Concrete product B

        2.  BUILDER
        Oject порождающий
        Пошаговое построение сложных продуктов

            Director
            Abstract builder
            Concrete builder
            Product

        3.  FACTORY METHOD
        Class порождающий
        Основа для всех пораждающих патернов

            Abstract creator
            Concrete creator
            Abstract product
            Concrete product

        4.  PROTOTYPE
        Oject порождающий
        Клонирование объектов
        
            Abstract prototype
            Concrete prototype.clone()

        5.  SINGLETON
        Oject порождающий
        Гарантирует создание только одного экземпляра класса, или опциональный контроль за количеством

        6.  ADAPTER
        Уровня класса или объекта
        Обеспечивает совместную работу классов с несовместимыми интерфейсами

            Сonsumer    -   пользователь продукта
            Abstract provider   -   провайдер от продукта к пользователю
            Concrete provider A -   продукт
            Concrete provider B -   продукт
            Earned Provider -   новый, напрямую несовместимый продукт
            Adapt provider C to connect -   адаптер для совмещения нового продукта с пользователем   

        7. BRIDGE
        Отделение абстракции от реализации таким образом, чтобы их обе можно было изменять независимо друг от друга

            Concrete drawmachine    -   готовое приложение с возможным расширяемым функционалом
            Abstract draw process   -   абстрактные методы работы приложения
            Abstract draw shapes    -   абстрактные методы работы приложения
            Abstract draw instruments   -   абстрактные методы работы приложения
            Concrete instruments    -   конкретные инструменты для работы приложения
            Concrete shapes -   конкретные инструменты для работы приложения
            .... добавляем новые инструменты независимо от основной реализации программы

        8.  COMPOSITE 
        Организация древовидной структуры
            
            Abstract Component  -   абстрактный компонент с полями и методами
            Concrete Leaf   -   конечный компонент, не может иметь наследников
            Concrete Branche    -   промежуточный полноценный компонент

        9.  DECORATOR
        Динамическое добавлнение объекту новые состояния и поведения
        Альтернатива наследственному порождению новых классов для расшинения функционала

            Abstract component
            Concrete component  -   стандартный объект
            Abstract decorator  -   абстрактный расширитель
            Concrete decorator A    -   добавление нового функционала к объекту
            Concrete decorator B    -   добавление нового функционала к объекту

        10. FACADE 
        Предоставляет унифицированный интерфейс для доступа к подсистеме

            Client  -   Общается с подсистемами через фасад
            Facade  -   Предоставляет доступ к подсистемам
            Subsystem_1 -   Функционал подсистемы
            Subsystem_2 -   Функционал подсистемы
            Subsystem_3 -   Функционал подсистемы

        11. FLYWEIGNT
        Организует работу с разделяемыми объектами

            Actor   -   разделяемый объект
            Role_1  -   Действие на основе объекта
            Role_2  -   Другое действие на основе объекта

        12. PROXY
        Предостваляет объект заместитель

            Abstract subject
            Real subject    -   Конкретный объект
            Surogate    -   Сурогат конкретного объекта
            
        13. CHAIN OF RESPONSIBILITY
        Создает цепочки из обработчиков запросов
            
            Client
            First   -   Первый обработчик
            Second  -   Второй обработчик
            ...

        14. COMMAND
        Позвляет представить запрос в виде объекта, позаволяя клиенту конфигурировать запрос, ставить запрос в очередь и отменять операции
 
            Invoker -   Запоминает команду и запускает ее
            Abstract command    -   
            Concrete command    -   Конкретная команда
            Reciever    -   Исполнитель конкретной команды

        15. INTERPRETER
        Формирует объектно-ориентированное представлелние грамматики для заданного языка
            
            Contex
            Abstract expression
            Terminal expression
            Non terminal expression

        16. ITERATOR
        Предоставляет способ последоветельного удобного доступ ко всем элементам коллекции не раскрывая структуры коллекции

            Inumarable
            Inumerator

        17. MEDIATOR
        Предоставление объекта-посредника, скрывающий способ взаимадействия множества других коллег

            Abstract mediator
            Abstract Collegue
            Concrete mediator   -   Конкретный посредник между коллегами, на него ложиться вся работа по передаче объекта между коллегами
            Concrete collegue   -   Конкретные коллеги, друг о друге не знают
            Concrete collegue_2 -   Конкретные коллеги, друг о друге не знают

        18. MEMENTO
        Предоставляет объект для хранения состояния

            Originator  -   Хозяин состояния
            Memento -   Хранитель состояния
            Caretaker   -   Посыльный, держит состояние и возвращает его хозяину

       19.  OBSERVER
       Описывает технику Издатель-Подписчик

            Push model  -   проталкивание, принудительное обновление
            Pull model  -   вытягивание, только оповещение

            Abstract subject
            Abstract observer
            Concrete subject    -   Конкретный издатель, уведомляет при наступлении события каждого подписчика
            Concrete observer   -   Конкретный подписчик, в зависимости от модели оповещения принимает информацию от издателя

        20. STATE
        Описывает способы построения конечных автоматов
        Позволяет объекту изменять свое поведение в зависимости от состояния

            Object  -   Конкретный объект, находится в одном из состояний и меняет их
            Abstract state
            Concrete state_1    -   Состояние объекта
            Concrete state_2    -   Состояние объекта

        21. STRATEGY
        Определяет набор алгоритмов схожих действий и делает их взаимозаменяемыми

            Context -   Исполнитель методов стратегии
            Abstract strategy
            Concrete strategy_1 -   Методы стратегии
            Concrete strategy_2 -   Методы стратегии

        22. TEMPLATE METHOD
        Задает структуру алгоритма

                Abstract class  -   Вызывает метод абстрактного класса, который исполняет
                Concrete class  -   переопределенные методы конктртного класса

        23. VISITOR
        Организует обход набора элементов с разнородными интерфейсами
        
            Abstract visitor
            Concrete visitor    -   Конкретный посетитель, которого вызывает структра
            Element structure   -   Структура с коллекцией конкретных элементов 
            Abstract element
            Concrete element_1  -   Конкретный элемент структры
            Concrete element_2  -   Конкретный элемент структры
        //------------------------------------------------------------------------
        */
        #endregion
        //------------------------------------------------------------------------
        #region ARCHITECTURE 
        //------------------------------------------------------------------------
        /*
        //  ARCHITECTURE
        Триада Ветрувия:
        1.  Прочность конструкции
        2.  Польза
        3.  Красота

        Слой    -   логическая группировка функциональности
            UML -   Packets
            Namespaces

        Уровни  -   Физическое распределение функциональности по разным машинам

        Multilager design:

                Presentation layer
                |
                V
                Service layer       --> Cross-cutting layer (Microsoft Enterprise library)
                |
                V                       
                Buiseness layer     -->      
                |
                V
                Data layer          --> 

        HIGH    Связанность -   мера самодостаточности сушности для обладания функциональной полнотой
        LOW     Сязность    -   мера зависимости одной сущности от другой

        1.  Продумка разделения слоев
        2.  Выбор слоев
        3.  Решение о распределении слоев и компонентов
        4.  Возможно ли сворачивание слоев
        5.  Определение правил взаимодействия со слоями
        6.  Определение сквозной функциональность
        7.  Определение интерфейсоа между слоями
        8.  Выбор стратегии развертывания
        9.  Выбор протоколов связи

        */
        #endregion
        //------------------------------------------------------------------------

        //------------------------------------------------------------------------

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

            //  ПОБИТОВЫЕ ОПЕРАЦИИ
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

            var timer = new Timer(Print,"Tick-Tack",0,200); //  Таймер, передает в него аргумент и выполняет метод интервалом
            void Print(object state)  //  Метод для таймера
            {
                Console.WriteLine(state);
            }

            Console.WriteLine(new Random (Guid.NewGuid().GetHashCode()).Next());  //  Качественный рандом

            ОБЪЕКТ  -   область памяти в управляемой куче, которая содержит в себе методы и статические поля
            ЭКЗЕМПЛЯР   -   область памяти, которая содержит в себе только нестатические поля

            */
        #endregion
        //------------------------------------------------------------------------


        //------------------------------------------------------------------------
        //  Main
        //  Main
        //  Main
        Console.WriteLine("Main завершен");
            Console.ReadKey();
        }
    }
}
