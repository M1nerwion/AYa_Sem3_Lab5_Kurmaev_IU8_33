using System.Collections;

namespace task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyDictionary<string, string> myDictionary = new MyDictionary<string, string>();

            myDictionary.Add("Первый элемент", "Элемент №1");
            Console.WriteLine(myDictionary.Count);

            myDictionary.Add("Бауманка", "МГТУ им. Н.Э.Баумана");
            Console.WriteLine(myDictionary.Count);

            myDictionary.Add("Собака", "Кошка");
            Console.WriteLine(myDictionary.Count);

            myDictionary.Print();

            Console.Write("Элемент со значением 'Бауманка': ");
            Console.WriteLine(myDictionary["Бауманка"]);

            myDictionary.Add("Бауманка", "Измайлово");
            Console.WriteLine(myDictionary.Count);

            Console.WriteLine("\nВесь словарь с помощью foreach");
            foreach (Para<string, string> temp in myDictionary)
            {
                Console.WriteLine($"Ключ: {temp.key}; Значение: {temp.Value}");
            }

            //Console.WriteLine("\nНахождение всех элементов с ключом 'Бауманка' с помощью foreach");
            //foreach (Para<string, string> temp in myDictionary)
            //{
            //    if (temp.key == "Бауманка") Console.WriteLine($"Ключ: {temp.key}; Значение: {temp.Value}");
            //}
        }
    }
}

public class Para<KKey, KValue>//Класс элементов
{
    public Para<KKey, KValue>? Next;//Указатель на следующий элемент списка
    public int Number { get; } //Номер элемента в списке

    public KKey? key;
    private KValue? _value; //Значение элемента

    public KValue? Value //Свойство задающее Значение элемента
    {
        get { return _value; }
        set { _value = value; }
    }

    public Para(int Number)
    {
        Next = default;
        this.Number = Number;
        key = default;
        _value = default;
    }
    public Para(int Number, KKey? key, KValue? _value)
    {
        Next = default;
        this.Number = Number;
        this.key = key;
        this._value = _value;
    }
}

class MyDictionary<TKey,TValue> : IEnumerable
{
    public Para<TKey, TValue> first;

    private int _Count;
    public int Count { get => _Count; }//Свойство получения общего количества элементов

    public MyDictionary()
    {
        _Count = 0;
        first = new Para<TKey, TValue>(_Count, default, default);
    }

    
    class DictionaryEnumerator : IEnumerator //Описание Enumerator для элементов класса
    {
        private Para<TKey, TValue>? elem;
        private Para<TKey, TValue>? ifirst;
        public DictionaryEnumerator(Para<TKey, TValue>? first)
        {
            this.ifirst = new Para<TKey, TValue>(0);
            ifirst.Next = first;//Нам необъходимо сделать начальную позицию -1
            elem = ifirst;
        }

        public bool MoveNext()//Метод MoveNext() перемещает указатель на текущий элемент на следующую позицию в последовательности.
        {
            if (elem?.Next is not null) 
            {
                elem = elem.Next; 
            }
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

    public IEnumerator GetEnumerator() => new DictionaryEnumerator(first);//Описание GetEnumerator


    public void Add(TKey? key, TValue? _value)//Добавление элемента в список
    {
        Para<TKey, TValue> Para = new Para<TKey, TValue>(Count, key, _value);

        if (Count == 0) { first = Para; _Count++; Console.WriteLine($"Добавлен объект: Ключ: {key}; Значение: {_value}"); }
        else
        {
            int pos = contain(key);
            if (pos >= 0) 
            {
                Para<TKey, TValue> i = first;
                for (; i.Number != pos; i = i.Next) { }
                i.Value = _value;
                Console.WriteLine($"В процессе добавления изменен объект на: Ключ: {key}; Значение: {_value}");
            }
            else
            {
                Para<TKey, TValue>? PreviusPara = GetPara(Count - 1);
                PreviusPara?.Next = Para;
                _Count++;
                Console.WriteLine($"Добавлен объект: Ключ: {key}; Значение: {_value}");
            }
        }
    }

    private int contain(TKey key)//Проверяет есть ли элемент с данным ключом в словаре, если да, то возвращает номер элемента, если нет, то -1
    {

        for (Para<TKey, TValue> i = first; i.Next is not null; i = i.Next)
        {
            if (i.key.Equals(key)) return i.Number;
        }
        return -1;
    }

    public TValue? this[TKey? index]//Индексатор, возвращает значение элемента списка
    {
        get
        {
            Para<TKey, TValue>? temp = first;
            TValue? result = default;
            //if (index >= Count) { throw new IndexOutOfRangeException(); }
            for (int i = 0; i < Count; i++)
            {
                if (temp.key.Equals(index)) { return temp.Value; }
                temp = temp.Next;
            }
            return default;//Если объект не найден, то возвращаем null
        }
        set
        {
            Para<TKey, TValue>? temp = first;
            TValue? result = default;
            //if (index >= Count) { throw new IndexOutOfRangeException(); }
            for (int i = 0; i < Count; i++)
            {
                if (temp.key.Equals(index)) { temp.Value = value; }
                temp = temp.Next;
            }
            throw new IndexOutOfRangeException("Не существует объекта с таким ключом");//Если объекта с таким ключом не существует
        }
    }

    private Para<TKey, TValue>? GetPara(int index)//Метод - Индексатор, возвращает элемента 
    {
        Para<TKey, TValue>? temp = first;
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
            Console.WriteLine($"Ключ: {GetPara(i).key}; Значение: {GetPara(i).Value}");
        }
        Console.WriteLine();
    }
}