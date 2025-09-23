namespace task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число столбцов:");
            int m = Convert.ToInt32(Console.ReadLine());
            // int m = 3;//Число строк в матрице

            Console.WriteLine("Введите число строк:");
            int n = Convert.ToInt32(Console.ReadLine());
            //int n = 3;//Число столбцов в матрице

            Console.WriteLine("Введите минимальное число диапозона генерации псевдослучайных чисел:");
            int a = Convert.ToInt32(Console.ReadLine());
            //int a = 10;

            Console.WriteLine("Введите максимальное число диапозона генерации псевдослучайных чисел:");
            int b = Convert.ToInt32(Console.ReadLine());
            //int b = 100;

            Console.WriteLine("Матрица 1");
            MyMatrix mx = new MyMatrix(m, n, a, b);//Создаем нашу матрицу
            mx.Show();//Выводим матрицу в консоль

            Console.WriteLine("Перезаполняем матрицу 1");
            mx.Fill(a, b);//Перезаполняем матрицу числами
            mx.Show();//Выводим матрицу в консоль

            Console.WriteLine("Меняем размер матрицы 1 на 2х2");
            mx.ChangeSize(2, 2, a, b);//Меняем размер матрицы
            mx.Show();// Выводим матрицу в консоль

            Console.WriteLine("Меняем размер матрицы 1 на 3х4");
            mx.ChangeSize(3, 4, a, b);//Меняем размер матрицы
            mx.Show();// Выводим матрицу в консоль

            Console.WriteLine("Подматрица матрицы 1 расположенная в центре");
            mx.ShowPartialy(2, 2, 2, 3);//Выводим в консоль подматрицу исходной
        }
    }
}

class MyMatrix
{
    int m = 0;
    int n = 0;
    int[,] mas;
    public MyMatrix(int m, int n, int a, int b)
    {
        this.m = m;
        this.n = n;

        mas = new int[m, n];
        Random rand = new();

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                mas[i, j] = rand.Next(a, b);
            }
        }
    }
    public void Fill(int a, int b)//метод Fill, перезаполняющий матрицу случайными значениями.
    {
        Random rand = new();
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                mas[i, j] = rand.Next(a, b);
            }
        }
    }

    public void ChangeSize(int m1, int n1, int a, int b)//метод ShowPartialy, принимающий в качестве параметров начальные и конечные значения строк и столбцов, значения матрицы внутри которых нужно вывести на консоль.
    {
        int[,] mas1 = new int[m1, n1];
        Random rand = new();

        for (int i = 0; i < m1; i++)
        {
            for (int j = 0; j < n1; j++)
            {
                if ((i < m) && (j < n))
                {
                    mas1[i, j] = mas[i, j];
                }
                else
                {
                    mas1[i, j] = rand.Next(a, b);
                }
            }
        }

        mas = mas1;
        m = m1;
        n = n1;
    }

    public void ShowPartialy(int m1, int m2, int n1, int n2)//метод ShowPartialy, принимающий в качестве параметров начальные и конечные значения строк и столбцов, значения матрицы внутри которых нужно вывести на консоль.
    {
        if (!((((0 <= m1) && (m1 <= m)) && ((0 <= m2) && (m2 <= m))) && (((0 <= n1) && (n1 <= n)) && ((0 <= n2) && (n2 <= n)))))
        {
            throw new ArgumentOutOfRangeException();//Если пытаемся получить подтаблицу вне диапозона исходной, то генерируем  исключение
        }

        for (int i = m1 - 1; i < m2; i++)
        {
            for (int j = n1 - 1; j < n2; j++)
            {
                Console.Write($" {mas[i, j]} ");
            }
            Console.Write("\n");
        }
        Console.Write("\n");
    }

    public void Show()//метод Show, выводящий все значения матрицы на консоль.
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($" {mas[i, j]} ");
            }
            Console.Write($"\n");
        }
        Console.Write("\n");
    }

    public int this[int i, int j]//Индексатор
    {
        get => mas[i, j];
        set => mas[i, j] = value;
    }
}