using static System.Console;

public class Matrix
{
    // private - переменные, которые нельзя использовать (получать доступ, модифицировать и тд) вне класса 
    private static string text = "\nВыберите опцию";
    private static string[] rules = {
        "Во время любого ввода вы можете ввести слово \"стоп\" и программа завершится",
        "Во время любого ввода вы можете ввести слово \"правила\" и будут показаны правила программы",
        "При завершении ввода данных нажимайте клавишу Enter",
        "При сообщении \"Введите: да/нет\" если вы согласны, введите слово \"да\", иначе введите любое другое слово или символ"
    };

    // public - переменные, которые можно использовать вне класса
    public static string tab = "\n\t";
    public static string tryAgainText = ". Попробуйте еще раз";

    private static List<int[,]> matrixes = new List<int[,]>(); // здесь будут храниться все матрицы

    public static void Stopper(string str)
    {
        if (str == "стоп")
        {
            Environment.Exit(0); // завершить программу
        }
        else if (str == "правила")
        {
            Write("Практикум по программированию. Практическая 1, задание 1. Матрицы\nПРАВИЛА");

            foreach (string i in rules) // вывести все правила в консоль
            {
                WriteLine(tab + i);
            }

            Menu(); // после вывода правил возвращаем пользователя в меню
        }
    }

    public static void ErrorMessage(string err)
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine(err);
        ForegroundColor = ConsoleColor.Gray;
        Beep(3084, 200); // :)
    }

    public static void ResultMessage(string text)
    {
        ForegroundColor = ConsoleColor.Green;
        WriteLine(text);
        ForegroundColor = ConsoleColor.Gray;
    }

    // меню, где пользователь выбирает то, что ему нужно
    // а также тут осуществляется проверка на правильность введенных данных и существование матриц, если выбрана опция для произведения операций
    public static void Menu()
    {
        WriteLine("\n\nГЛАВНОЕ МЕНЮ" + text);
        string[] options = { "0 - завершить программу", "1 - создать матрицу", "2 - удалить матрицу", "3 - операция над матрицей/матрицами" };
        foreach (string i in options) // все элементы
        {
            WriteLine(tab + i); // вывести в консоль
        }

        string optionStr = ReadLine();
        Stopper(optionStr); // завершить программу если введено слово стоп или вывести правила

        int option = 0;
        try
        {
            option = Convert.ToInt32(optionStr);
        }
        catch (FormatException) // проверка ввода (если не число, то вывести ошибку и попросить ввести данные снова
        {
            ErrorMessage("Введено не число" + tryAgainText);
            Menu(); // вернуться на предыдущую ступень, туда, где была ошибка
        }
        finally
        {
            switch (option) // исполнить выбранную опцию
            {
                case 0:
                    Environment.Exit(0); break;
                case 1:
                    MatrixCreationMenu();
                    Menu();
                    break;
                case 2:
                    if (matrixes.Count > 0) // если есть матрицы, то продолжить. Иначе вывести ошибку и вернуть в меню
                    {
                        MatrixDeletionMenu();
                    }
                    else
                    {
                        ErrorMessage("Нет матриц для удаления");
                    }
                    Menu();
                    break;
                case 3:
                    if (matrixes.Count == 0) // check if list is empty
                    {
                        ErrorMessage("Нет матриц для операций. Может вы хотели создать матрицу? Введите: да/нет");
                        string str = ReadLine();

                        if (str == "да")
                        {
                            MatrixCreationMenu();
                        }
                    }
                    else
                    {
                        MatrixOperationMenu();
                    }
                    Menu();
                    break;
                default:
                    ErrorMessage("Такой опции нет" + tryAgainText);
                    Menu();
                    break;
            }
        }

        Exception ex = new Exception();
        if (ex is IndexOutOfRangeException)
        {
            ErrorMessage("Что-то пошло не так" + tryAgainText);
        }
    }

    // подменю, где пользователь выбирает какую матрицу хочет создать
    // а также тут осуществляется проверка на правильность введенных данных
    public static void MatrixCreationMenu()
    {
        WriteLine("\n\nМЕНЮ СОЗДАНИЯ МАТРИЦЫ" + text);
        string[] options = { "0 - вернуться в главное меню", "1 - создать матрицу и задать числа", "2 - создать матрицу случайных чисел" };
        foreach (string i in options)
        {
            WriteLine(tab + i);
        }

        string optionStr = ReadLine();
        Stopper(optionStr); // завершить программу если введено слово стоп или вывести правила

        int option = 0;
        try
        {
            option = Convert.ToInt32(optionStr);
        }
        catch (FormatException) // проверка ввода (если не число, то вывести ошибку и попросить ввести данные снова
        {
            ErrorMessage("Введено не число" + tryAgainText);
            MatrixCreationMenu();
        }
        finally
        {
            switch (option)
            {
                case 0:
                    Menu(); break;
                case 1:
                    MatrixCreation(); break;
                case 2:
                    RandomMatrixCreation(); break;
                default:
                    ErrorMessage("Такой опции нет" + tryAgainText);
                    MatrixCreationMenu();
                    break;
            }
        }
    }

    private static int GetInt()
    {
        string str = ReadLine();
        Stopper(str); // завершить программу если введено слово стоп или вывести правила

        int num = 0;

        try
        {
            num = Convert.ToInt32(str);
        }
        catch (FormatException) // проверка ввода (если не число, то вывести ошибку и попросить ввести данные снова
        {
            ErrorMessage("Введено не число" + tryAgainText);
            Menu();
        }

        return num;
    }

    private static int GetPositiveInt()
    {
        string str = ReadLine();
        Stopper(str); // завершить программу если введено слово стоп или вывести правила

        int num = 0;
        try
        {
            num = Convert.ToInt32(str);
        }
        catch (Exception ex) // проверка ввода (если не число, то вывести ошибку и попросить ввести данные снова
        {
            if (ex is FormatException)
            {
                ErrorMessage("Введено не число" + tryAgainText);
                GetPositiveInt(); // вернуться на предыдущую ступень, туда, где была ошибка
            }
        }

        if (num < 0) // проверка ввода (если число не положительное, то вывести ошибку и попросить ввести данные снова
        {
            ErrorMessage("Число должно быть положительным" + tryAgainText);
            GetPositiveInt(); // вернуться на предыдущую ступень, туда, где была ошибка
        }

        return num;
    }

    private static void SetElements(int rows, int cols)
    {
        int[,] array = new int[rows, cols];
        WriteLine("\nВведите элементы матрицы: ");
        string elementStr;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = GetInt();
            }
        }

        matrixes.Add(array);
    }

    private static void SetRandElements(int rows, int cols, int min, int max)
    {
        int[,] array = new int[rows, cols];
        Random rand = new Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = rand.Next(min, max);
            }
        }

        matrixes.Add(array);
    }

    private static void ShowMatrix(int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Write(array[i, j] + " ");
            }
            WriteLine();
        }
        WriteLine();
    }

    private static void MatrixCreation()
    {
        // Что нужно сделать
        // - получить кол-во строк и столбцов
        // - получить элементы и записать в матрицу
        // - провести проверку правильности введенных данных для каждого шага выше
        // - показать готовую матрицу

        WriteLine("\nЗапущен процесс создания матрицы!\nКол-во строк: ");
        int rows = GetPositiveInt();
        WriteLine("\nКол-во столбцов: ");
        int cols = GetPositiveInt();
        SetElements(rows, cols);

        ResultMessage("\nРезультат");
        ShowMatrix(matrixes[matrixes.Count - 1]); // показать последнюю созданную матрицу

        // если пользователь не хочет больше создавать матрицы, вернуть в главное меню. Иначе продолжить создавать матрицы
        WriteLine("Продолжить создавать матрицы? да/нет");
        if (ReadLine() == "да")
        {
            MatrixCreation();
        }
        else
        {
            Menu();
        }
    }

    private static void RandomMatrixCreation()
    {
        // Что нужно сделать
        // - получить кол-во строк и столбцов
        // - получить минимальное и максимальное число для генерации случайных чисел
        // - провести проверку правильности введенных данных для каждого шага выше
        // - сгенерировать случайные числа и записать в матрицу
        // - показать готовую матрицу

        WriteLine("\nЗапущен процесс создания матрицы!\nКол-во строк: ");
        int rows = GetPositiveInt();
        WriteLine("\nКол-во столбцов: ");
        int cols = GetPositiveInt();
        WriteLine("\nМинимальное число элемента: ");
        int min = GetInt();
        WriteLine("\nМаксимальное число элемента: ");
        int max = GetInt();

        if (max < min)
        {
            ErrorMessage("\nМаксимальное число не может быть меньше минимального!");
            WriteLine("Вы ввели:\n\tМинимальное: " + min + "\n\tМаксимальное: " + max);
            RandomMatrixCreation();
        }

        SetRandElements(rows, cols, min, max);
        ResultMessage("\nРезультат");
        ShowMatrix(matrixes[matrixes.Count - 1]); // показать последнюю созданную матрицу

        // если пользователь не хочет больше создавать матрицы, вернуть в главное меню. Иначе продолжить создавать матрицы
        WriteLine("Создать ещё матрицу? Да/нет");
        if (ReadLine() == "да")
        {
            RandomMatrixCreation();
        }
        else
        {
            Menu();
        }
    }

    // подменю, где пользователь выбирает какую матрицу хочет удалить
    // а также тут осуществляется проверка на правильность введенных данных
    public static void MatrixDeletionMenu()
    {
        WriteLine("\n\nМЕНЮ УДАЛЕНИЯ МАТРИЦЫ" + text);
        string[] options = { "0 - вернуться в главное меню", "1 - удалить определенную матрицу", "2 - удалить все матрицы" };
        foreach (string i in options)
        {
            WriteLine(tab + i);
        }

        int option = GetPositiveInt();
        switch (option)
        {
            case 0:
                Menu(); break;
            case 1:
                MatrixDeletion(); break;
            case 2:
                MatrixDeletionAll(); break;
            default:
                ErrorMessage("Такой опции нет" + tryAgainText);
                MatrixDeletionMenu();
                break;
        }
    }

    private static void ShowAllMatrixes()
    {
        int count = 0;

        ResultMessage("\nВсе матрицы");
        foreach (int[,] i in matrixes)
        {
            count++;
            WriteLine("___Матрица номер " + count + "___");
            ShowMatrix(i);
        }
    }

    private static void MatrixDeletion()
    {
        ShowAllMatrixes();

        Write("\nВведите номер матрицы для удаления: ");
        int option = GetPositiveInt();

        if (option - 1 >= matrixes.Count)
        {
            ErrorMessage("Матрицы под таким номером не существует");
            MatrixDeletionMenu();
        }
        else
        {
            matrixes.RemoveAt(option - 1);
            ResultMessage("Матрица под номером " + option + " удалена");
        }
    }

    private static void MatrixDeletionAll()
    {
        matrixes.Clear();
        ResultMessage("Все матрицы удалены");
    }

    // подменю, где пользователь выбирает какую операцию хочет осуществить
    // а также тут осуществляется проверка на правильность введенных данных
    public static void MatrixOperationMenu()
    {
        WriteLine("\n\nМЕНЮ ОПЕРАЦИЙ НАД МАТРИЦАМИ" + text);
        string[] options = { "0 - вернуться в главное меню", "1 - транспонировать матрицу", "2 - получить сумму матриц", "3 - получить произведение матриц", "4 - умножить матрицу на константу" };
        foreach (string i in options)
        {
            WriteLine(tab + i);
        }

        int option = GetPositiveInt();
        switch (option)
        {
            case 0:
                Menu(); break;
            case 1:
                Transposition(); break;
            case 2:
                Sum(); break;
            case 3:
                Multiplication(); break;
            case 4:
                MultiplicationByNumber(); break;
            default:
                ErrorMessage("Такой опции нет" + tryAgainText);
                Menu();
                break;
        }
    }

    private static int ChooseMatrix()
    {
        WriteLine("Ваш выбор: ");
        int index = GetPositiveInt() - 1;

        if (index < 0 || index >= matrixes.Count)
        {
            ErrorMessage("Такой матрицы нет" + tryAgainText); // ошибка, если пользователь ввел индекс несуществующей матрицы
            ChooseMatrix();
        }

        return index;
    }

    private static void Transposition()
    {
        int index = 0;

        if (matrixes.Count == 1)
        {
            // если матриц лишь 1 штука, то пользователю ничего не надо вводить
        }
        else
        {
            ShowAllMatrixes();
            index = ChooseMatrix();
        }

        int[,] matrix = matrixes[index];

        int w = matrix.GetLength(0); // width
        int h = matrix.GetLength(1); // height
        int[,] result = new int[h, w]; // transposed matrix has swapped dimensions

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                result[j, i] = matrix[i, j];
            }
        }

        ResultMessage("\nРезультат");
        // Iterate over the elements of the transposed matrix and print them
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                Write(result[i, j] + " ");
            }
            WriteLine();
        }

    }

    private static void Sum()
    {
        int index1 = 0, index2 = 0, temp;

        if (matrixes.Count == 1)
        {
            // если матриц лишь 1 штука, то пользователю ничего не надо вводить
        }
        else
        {
            ShowAllMatrixes();// показ всех матриц
            WriteLine("Выберите первую матрицу");
            index1 = ChooseMatrix();// и выбор пользователем матрицы
            WriteLine("Выберите вторую матрицу");
            index2 = ChooseMatrix();
        }

        bool areEqual = (matrixes[index1].GetLength(0) == matrixes[index2].GetLength(0)) && (matrixes[index1].GetLength(1) == matrixes[index2].GetLength(1));

        ResultMessage("\nРезультат");
        if (areEqual)// проверка на совместимость матриц (стороны равны)
        {
            for (int i = 0; i < matrixes[index1].GetLength(0); i++)
            {
                for (int j = 0; j < matrixes[index1].GetLength(1); j++)
                {
                    temp = matrixes[index1][i, j] + matrixes[index2][i, j];// складываем
                    Write(temp + " ");
                }
                WriteLine();
            }
        }
        else
        {
            ErrorMessage("Для сложения размеры матриц должны совпадать" + tryAgainText);
            Sum();
        }
    }

    private static void Multiplication()
    {
        int index1 = 0, index2 = 0;

        if (matrixes.Count == 1)
        {
            // если матриц лишь 1 штука, то пользователю ничего не надо вводить
        }
        else
        {
            ShowAllMatrixes();// показ всех матриц
            WriteLine("Выберите первую матрицу");
            index1 = ChooseMatrix();// и выбор пользователем матрицы
            WriteLine("Выберите вторую матрицу");
            index2 = ChooseMatrix();
        }

        // проверка на совместимость матриц (ширина и высота одной матрицы должна быть равна высоте и ширине другой матрицы)
        int h1 = matrixes[index1].GetLength(0);
        int w1 = matrixes[index1].GetLength(1);
        int h2 = matrixes[index2].GetLength(0);
        int w2 = matrixes[index2].GetLength(1);

        int[,] newMatrix = { };

        ResultMessage("\nРезультат");
        if (h1 == w2 || w1 == h2)
        {
            for (int i = 0; i < h1; i++)
            {
                for (int j = 0; j < w2; j++)
                {
                    int temp = 0;// reset
                    for (int z = 0; z < w1; z++)// пробег по эл.-ам строки первой матрицы и пробег по эл.-ам столбца второй матрицы
                    {

                        temp += matrixes[index1][i, z] * matrixes[index2][z, j];
                    }
                    Write(temp + " ");
                }
                WriteLine();
            }
        }
        else
        {
            ErrorMessage("Для умножения кол-во строк и кол-во столбцов 1-ой матрицы должны совпадать с кол-вом строк и кол-вом столбцов 2-ой матрицы" + tryAgainText);
        }

        for (int i = 0; i < h1; i++)
        {
            for (int j = 0; j < h1; j++)
            {

            }

        }
    }

    private static void MultiplicationByNumber()
    {
        int index = 0;

        if (matrixes.Count == 1)
        {
            // если матриц лишь 1 штука, то пользователю ничего не надо вводить
        }
        else
        {
            ShowAllMatrixes();
            index = ChooseMatrix();
        }

        Write("Число:");
        int num = GetInt();

        ResultMessage("\nРезультат");
        for (int i = 0; i < matrixes[index].GetLength(0); i++)
        {
            for (int j = 0; j < matrixes[index].GetLength(1); j++)
            {
                Write(matrixes[index][i, j] * num + " ");
            }
            WriteLine();
        }
    }
}

public class Start
{
    public static void Main(string[] args)
    {
        ForegroundColor = ConsoleColor.Gray;
        Matrix.Stopper("правила");
        Matrix.Menu();
    }
}