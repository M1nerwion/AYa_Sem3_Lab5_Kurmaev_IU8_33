using System.Collections;


namespace task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyList<string> myList = new MyList<string>();

            myList.Add("Первый элемент");
            Console.WriteLine(myList.Count);

            myList.Add("Второй элемент");
            Console.WriteLine(myList.Count);

            Console.Write("Первый элемент списка: ");
            Console.WriteLine(myList[0]);

            myList.Print();

            Console.Write("Инициализация списка:");
            MyList<int> myList1Int = new MyList<int>() { 10, 15, 20 };

            myList1Int.Print();

            Console.Write("Третий элемент списка myList1Int: ");
            Console.WriteLine(myList1Int[2]);
        }
    }
}

class MyList<T> :IEnumerable
{
    public Element<T> first;

    private int _Count;
    public int Count { get => _Count; }//Свойство получения общего количества элементов

    public MyList(params T?[] elements)
    {
        first = new Element<T>(0); //Создаем Элемент по Умолчанию

        _Count = 0;
        for (int i = 0; i < elements.Length; i++)
        {
            if ((i == 0) && (Count == 0))
            {
                first.Value = elements[i];//Если первый элемент в списке, то достаточно поменять значение по умолчанию у созданного по умолчанию первого элемента
            }
            else
            {
                Add(elements[i]);//Если не первый элемент, то просто добавить в список
            }
        }
        _Count = elements.Length;
    }


    public class Element<K>//Класс элементов
    {
        public Element<K>? Next;//Указатель на следующий элемент списка
        public int Number { get; } //Номер элемента в списке

        private K? _value; //Значение элемента

        public K? Value //Свойство задающее Значение элемента
        {
            get { return _value; }
            set { _value = value; }
        }

        public Element(int Number)
        {
            Next = default;
            this.Number = Number;
            _value = default;
        }
        public Element(int Number, K? _value)
        {
            Next = default;
            this.Number = Number;
            this._value = _value;
        }
    }

    
    class ListEnumerator : IEnumerator //Описание Enumerator для элементов класса
    {
        private Element<T>? elem;
        private Element<T>? ifirst;
        public ListEnumerator(Element<T>? ifirst)
        {
            this.ifirst = ifirst;
            elem = ifirst;
        }

        public bool MoveNext()//Метод MoveNext() перемещает указатель на текущий элемент на следующую позицию в последовательности.
        {
            if (elem?.Next is not null) { elem = elem.Next; }
            else { return false; }
            return true;
        }

        public object Current //Свойство Current возвращает объект в последовательности, на который указывает указатель.
        { 
            get 
            { 
                if (elem is null) throw new ArgumentException(); 
                else return elem; 
            } 
        }

        public void Reset()//Метод Reset() сбрасывает указатель позиции в начальное положение.
        {
            elem = ifirst;
        }

        public void Dispose() { } //Требовал наличие, но функционал не обязателен
    }

    public IEnumerator GetEnumerator() => new ListEnumerator(first);//Описание GetEnumerator


    public void Add(T? _value)//Добавление элемента в список
    {
        Element<T> element = new Element<T>(Count, _value);

        if (Count == 0) { first = element; }
        else
        {
            Element<T>? PreviusElement = GetElement(Count - 1);
            PreviusElement?.Next = element;
        }
        _Count++;
    }

    public T? this[int index]//Индексатор, возвращает значение элемента списка
    {
        get
        {
            Element<T>? temp = first;
            if (index >= Count) { throw new IndexOutOfRangeException(); }
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }
            return temp.Value;
        }
        set 
        {
            Element<T>? temp = first;
            if (index >= Count) { throw new IndexOutOfRangeException(); }
            for (int i = 0; i < index; i++)
            {
                temp = temp.Next;
            }
            temp?.Value = value;
        }
    }

    private Element<T>? GetElement(int index)//Метод - Индексатор, возвращает элемента 
    {
        Element<T>? temp = first;
        if (index >= Count) { throw new IndexOutOfRangeException(); }
        for (int i = 0; i < index; i++)
        {
            temp = temp.Next;
        }
        return temp;
    }

    public void Print()//Метод, который выводит весь список на экран
    {
        Console.WriteLine("\nВесь список:");
        for (int i = 0; i < this.Count; i++)
        {
            Console.WriteLine(this[i]);
        }
        Console.WriteLine();
    }
}
