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

           // MyList<int> myList1Int = new MyList<int>() { 10, 15, 20 };//Доработать IEnumerator
        }
    }
}

class MyList<T> //:IEnumerable<T>
{
    public Element<T> first;

    private int _Count;
    public int Count { get => _Count; }//Свойство получения общего количества элементов

    public MyList(params T?[] elements)
    {
        first = new Element<T>(0);

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


    public class Element<K>
    {
        public Element<K>? Next;
        public int Number { get; }

        private K? _value;

        public K? Value
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
    }//Класс элементов

    //class ListEnumerator : IEnumerator<Element<T>> {
    //{
    //    public bool MoveNext()
    //    {
    //        if 
    //    }
    //}

    //public IEnumerator<T> GetEnumerator() => days.GetEnumerator();

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

    public T? this[int index]
    {
        get
        {
            Element<T>? temp = first;
            if (index >= Count) { throw new IndexOutOfRangeException(); }
            for (int i = 0; i < index; i++)
            {
                temp = first.Next;
            }
            return temp.Value;
        }
        set 
        {
            Element<T>? temp = first;
            if (index >= Count) { throw new IndexOutOfRangeException(); }
            for (int i = 0; i < index; i++)
            {
                temp = first.Next;
            }
            temp?.Value = value;
        }
    }//Индексатор, возвращает значение элемента списка

    private Element<T>? GetElement(int index)
    {
        Element<T>? temp = first;
        if (index >= Count) { throw new IndexOutOfRangeException(); }
        for (int i = 0; i < index; i++)
        {
            temp = first.Next;
        }
        return temp;
    }

    public void Print()
    {
        Console.WriteLine("\nВесь список:");
        for (int i = 0; i < this.Count; i++)
        {
            Console.WriteLine(this[i]);
        }
        Console.WriteLine();
    }
}
