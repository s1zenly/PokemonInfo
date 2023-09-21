using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;


class Program
{
    //Переменные, с областью видимости класса, которые считают количество строк и столбцов в файле
    static int kol_row;
    static int kol_column;

    //Метод, который заполняет массив элементами из файла
    public static string[][] InfoPokemon()
    {
        string [] linesplit;
        string line = "";
        string pokemon = "";
        kol_row = 0;
        
        //Спрашиваем пользователя из какого файла считывать данные
        Console.WriteLine("Введите название файла из которого считываем");
        string path = Console.ReadLine();
        //Сразу же проверяем его существование
        if (!File.Exists(path))
        {
            Console.WriteLine("Такого файла не существует или он пуст, выберите пункт заново и повторите попытку");
            Menu();
        }
        else
            pokemon = File.ReadAllText(path);
        //Создаем массив и заполняем его
        string[][] infopok = new string[pokemon.Length][];
        for (int i = 0; i < pokemon.Length; i++)
        {
            line += pokemon[i];
            if(pokemon[i] == '\n')
            {
                linesplit = line.Split(",");
                infopok[kol_row] = new string[linesplit.Length];
                for (int j = 0; j < linesplit.Length; j++)
                {
                    infopok[kol_row][j] = linesplit[j];
                }
                kol_row++;
                line = "";
            }

        }
        kol_column = infopok[0].Length;
        return infopok;
         
    }

    //Метод, обрабатывающий первый пункт меню
    static void Point_1()
    {
        //Спрашиваем путь какого фала вывести
        Console.WriteLine("Введите название файла из которого считываем");
        string path = Console.ReadLine();
        //Проверяем его существование
        if (!File.Exists(path))
            Console.WriteLine("Такого файла не существует");
        else
            Console.WriteLine(Path.GetFullPath(path));
        
    }

    //Метод, обрабатывающий второй пункт меню
    static void Point_2()
    {try
        {
            int kol_leg = 0;
            int kol_noleg = 0;
            int numberlegendary = 0;
            //Присваиваем наш список из метода
            string[][] infopok = InfoPokemon();
            kol_column = infopok[0].Length;
            // находим номер столбца Legendary
            for (int j = 0; j < infopok[0].Length; j++)
            {
                if (infopok[0][j] == "Legendary\n")
                    numberlegendary = j;
            }
            //Делаем красочное оформление
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Легендарные:");
            Console.ForegroundColor = ConsoleColor.White;

            //Создаем файл и открываем на запись
            using (FileStream file = File.Create("Pokemon-Legendary.csv"))
            {
                using (StreamWriter sw = new StreamWriter(file,Encoding.UTF8))
                {
                    //Выводим первую строку исходного файла с наименованием столбцов, чтобы в будущем можно было к нему обратиться
                    for (int e = 0; e < kol_column; e++)
                    {
                        sw.Write(infopok[0][e]);
                        if (e != kol_column - 1)
                            sw.Write(",");
                    }

                    //находим легендарных, выводим их и записываем в файл
                    for (int i = 0; i < kol_row; i++)
                    {
                        if (infopok[i][numberlegendary] == "True\n")
                        {
                            kol_leg++;

                            for (int k = 0; k < kol_column; k++)
                            {
                                Console.Write(infopok[i][k]);
                                sw.Write(infopok[i][k]);
                                if (k != kol_column - 1)
                                {
                                    Console.Write(",");
                                    sw.Write(',');
                                }
                            }
                            

                        }
                    }
                }
            }

            if (kol_leg == 0)
                Console.WriteLine("Нет таких");
            //Делаем отступ
            Console.WriteLine();
            Console.WriteLine();

            //Делаем красочное оформление
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Нелегендарные:");
            Console.ForegroundColor = ConsoleColor.White;
            
            //Создаем и открываем на запись файл
            using (FileStream file = File.Create("Pokemon-Usual.csv"))
            {
                using (StreamWriter sw = new StreamWriter(file,Encoding.UTF8))
                {
                    //Выводим первую строку исходного файла с наименованием столбцов, чтобы в будущем можно было к нему обратиться
                    for (int e = 0; e < kol_column; e++)
                    {
                        sw.Write(infopok[0][e]);
                        if (e != kol_column - 1)
                            sw.Write(",");
                    }

                    //находим нелегендарных, выводим их и записываем в файл
                    for (int i = 0; i < kol_row; i++)
                    {
                        if (infopok[i][numberlegendary] == "False\n")
                        {
                            kol_noleg++;

                            for (int k = 0; k < kol_column; k++)
                            {
                                Console.Write(infopok[i][k]);
                                sw.Write(infopok[i][k]);
                                if (k != kol_column - 1)
                                {
                                    Console.Write(",");
                                    sw.Write(',');
                                }
                            }
                            
                        }
                    }
                }
            }
            if (kol_noleg == 0)
                Console.WriteLine("Нет таких");
        }
        //проверка на открыт ли файл или нет
        catch
        {
            Console.WriteLine("У вас открыт данный файл, закройте его и повторите попытку!");
            Menu();
        }
    }
    //Переменная номера поколения в видимости класса
    static int el;

    //Метод, обрабатывающий третий пункт меню
    static void Point_3()
    {try
        {
            int kol_pokem_gener = 0;
            int maxpokemon = 0;
            int max;
            int t;
            int attack;
            int generation = 0;
            int numbergeneration = 0;
            int numberattack = 0;
            string[][] infopok = InfoPokemon();

            //Ищем номер столбца Generation в таблице
            for (int i = 0; i < infopok[0].Length; i++)
            {
                if (infopok[0][i] == "Generation")
                {
                    numbergeneration = i;
                    break;
                }
            }
            
            //Ищем номер столбца Attack в таблице
            for (int k = 0; k < infopok[0].Length; k++)
            {
                if (infopok[0][k] == "Attack")
                {
                    numberattack = k;
                    break;
                }
            }


            //цикл распределения покемонов по поколениям
            for (int j = 1; j < kol_row; j++)
            {
                //проверка на корректность данных
                int.TryParse(infopok[j][numbergeneration], out  el);

                //Переключение на новое поколение
                if (el != generation)
                {
                    //Создаем оформление
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Поколение {el}");
                    Console.ForegroundColor = ConsoleColor.White;
                    
                    attack = 0;
                    t = j;
                    max = 0;
                    //Ищем сильнейшего покемона поколения
                    while ((int.Parse(infopok[t][numbergeneration]) == el) & (t != kol_row - 1))
                    {

                        if (int.Parse(infopok[t][numberattack]) > max)
                        {
                            max = int.Parse(infopok[t][numberattack]);
                            maxpokemon = t;
                        }
                        t++;
                    }
                    //Создаем файл и открываем на запись
                    using (FileStream fs = File.Create($"Pokemon-Gen-{el}.csv"))
                    {
                        using (StreamWriter sw = new StreamWriter(fs,Encoding.UTF8))
                        {
                            //Записываем первую строку с наименованием столбцов
                            for (int e = 0; e < kol_column; e++)
                            {
                                sw.Write(infopok[0][e]);
                                if (e != kol_column - 1)
                                    sw.Write(",");
                            }

                            //Создаем оформление
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("Самый мощный покемон поколения: ");
                            Console.ForegroundColor = ConsoleColor.White;


                            //Записываем самого мощного покемона
                            if (infopok[maxpokemon][0] != "#")
                            {
                                for (int q = 0; q < kol_column; q++)
                                {

                                    sw.Write(infopok[maxpokemon][q]);
                                    Console.Write(infopok[maxpokemon][q]);
                                    if (q != kol_column - 1)
                                    {
                                        Console.Write(",");
                                        sw.Write(",");
                                    }
                                }
                            }
                            Console.WriteLine();
                            //Записываем первого покемона поколения
                            for (int z = 0; z < kol_column; z++)
                            {
                                kol_pokem_gener++;
                                sw.Write(infopok[j][z]);
                                Console.Write(infopok[j][z]);
                                if (z != kol_column - 1)
                                {
                                    Console.Write(",");
                                    sw.Write(",");
                                }

                            }
                        }
                    }
                    //Переприсваеваем для сравнения вначале
                    generation = el;

                }
                //Продолжаем записывать в поколение покемонов, пока if не переключит на новое поколение
                else
                {
                    //Продолжаем запись в тот же файл
                    using (StreamWriter fs = new StreamWriter($"Pokemon-Gen-{el}.csv", true,Encoding.UTF8))
                    {

                        for (int w = 0; w < kol_column; w++)
                        {


                            fs.Write(infopok[j][w]);
                            Console.Write(infopok[j][w]);
                            if (w != kol_column - 1)
                            {
                                Console.Write(",");
                                fs.Write(",");
                            }
                        }
                    }

                }
            }
            if (kol_pokem_gener == 0)
                Console.WriteLine("Нет поколений");
        }
        //Проверка на открыт ли файл или нет
        catch
        {
            Console.WriteLine($"У вас открыт файл \"Pokemon-Gen-{el}.csv\", закройте его и повторите попытку!");
            Menu();
        }
    }

    //Обработка четвертого пункта меню
    static void Point_4()
    {
        try
        {
            
            int numberspeed = 0;
            uint maxspeed = 0;
            string[][] infopok = InfoPokemon();
            int ch = 0;
            //Ищем номер столбца Speed
            for (int i = 0; i < kol_column; i++)
            {
                if (infopok[0][i] == "Speed")
                {
                    numberspeed = i;
                    break;
                }
            }
            //Ищем максимальную скорость
            for (int j = 0; j < kol_row; j++)
            {
                uint.TryParse(infopok[j][numberspeed], out uint speed);
                if (speed > maxspeed)
                    maxspeed = speed;
            }
            //Создаем файл и открываем на запись
            using (FileStream fs = File.Create("Speed-Pokemon.csv"))
            { }
            for (int k = 0; k < kol_row; k++)
            {
                //Проверяем выполнение условия    
                uint.TryParse(infopok[k][numberspeed], out uint speed);
                if (maxspeed - speed <= 10)
                {
                    for (int p = 0; p < kol_column; p++)
                    {
                        using (StreamWriter sw = new StreamWriter("Speed-Pokemon.csv", true, Encoding.UTF8))
                        {
                            //Счетчик для вывода первой строки файла с номенованием столбцов
                            ch++;
                            if(ch == 1)
                            {
                                //Заполняем первую строку с наименованием столбцов
                                for (int e = 0; e < kol_column; e++)
                                {
                                    sw.Write(infopok[0][e]);
                                    if (e != kol_column - 1)
                                        sw.Write(",");
                                }
                            }
                            sw.Write(infopok[k][p]);
                            Console.Write(infopok[k][p]);
                            if (p != kol_column - 1)
                            {
                                Console.Write(",");
                                sw.Write(",");
                            }
                        }
                    }
                }
            }
            if (ch == 0)
                Console.WriteLine("Нет таких");
        }
        //Проверяем открыт ли файл или нет
        catch
        {
            Console.WriteLine("Открыт файл \"Speed-Pokemon.csv\", закройте его и повторите попытку!");
            Menu();
        }
    }

    //Обработчик пятого пункта меню
    static void Point_5()
    {
        try
        {
            int kol = 0;
            int generation = 0;
            int numbergeneration = 0;
            string[][] infopok = InfoPokemon();

            //Ищем номер столбца Generation в файле
            for (int i = 0; i < kol_column; i++)
            {
                if (infopok[0][i] == "Generation")
                {
                    numbergeneration = i;
                }
            }
            //Создаем оформление
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("a)");
            Console.ForegroundColor = ConsoleColor.White;

            //Считаем по поколением количсетво покемонов
            for (int j = 1; j < kol_row; j++)
            {
                int.TryParse(infopok[j][numbergeneration], out el);
                if (el != generation)
                {
                    if (el - 1 != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"Поколение {el - 1}:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(kol);
                    }
                    generation = el;
                    kol = 0;
                    kol++;

                }
                else
                {
                    kol++;
                }


            }
            //Создаем оформление
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Поколение {el}:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(kol);


            int numberattack = 0;
            int max = 0;
            int min = 0;

            //Создаем оформление
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("b)");
            Console.ForegroundColor = ConsoleColor.White;

            //Ищем номер столбца Attack в файле
            for (int z = 0; z < kol_column; z++)
            {
                if (infopok[0][z] == "Attack")
                {
                    numberattack = z;
                }
            }
            int.TryParse(infopok[1][numberattack], out int attackmin);
            int.TryParse(infopok[1][numberattack], out int attackmax);

            //Ищем индексы самого мощного и самого слабого покемонов
            for (int m = 1; m < kol_row; m++)
            {
                if (int.Parse(infopok[m][numberattack]) >= attackmax)
                {
                    attackmax = int.Parse(infopok[m][numberattack]);
                    max = m;
                }
                if (int.Parse(infopok[m][numberattack]) <= attackmin)
                {
                    attackmin = int.Parse(infopok[m][numberattack]);
                    min = m;
                }
            }

            //Создаем оформление
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Самый мощный покемон: ");
            Console.ForegroundColor = ConsoleColor.White;

            //Выводим самого сильного покемона
            if (infopok[max][0] != "#")
            {
                for (int y = 0; y < kol_column; y++)
                {

                    Console.Write(infopok[max][y]);
                    if (y != kol_column - 1)
                        Console.Write(",");
                }
            }
            else
                Console.WriteLine("Нет такого");

            //Создаем оформление
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Самый слабый покемон: ");
            Console.ForegroundColor = ConsoleColor.White;

            //Выводим самого слабого покемона
            if (infopok[min][0] != "#")
            {
                for (int o = 0; o < kol_column; o++)
                {

                    Console.Write(infopok[min][o]);
                    if (o != kol_column - 1)
                        Console.Write(",");
                }
            }
            else
                Console.WriteLine("Нет такого");

            //Создаем оформление
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("c)");
            Console.ForegroundColor = ConsoleColor.White;

            int numbertype1 = 0;
            int numbertype2 = 0;
            int kolbugpoison = 0;

            //Создаем оформление
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Количество ядовитых жуков: ");
            Console.ForegroundColor = ConsoleColor.White;

            //ищем индексы столбов Type 1 и Type 2
            for (int g = 0; g < kol_column; g++)
            {
                if (infopok[0][g] == "Type 1")
                    numbertype1 = g;
                if (infopok[0][g] == "Type 2")
                    numbertype2 = g;
            }

            //Считаем количество ядовитых жуков
            for (int b = 1; b < kol_row; b++)
            {
                if (((infopok[b][numbertype1] == "Poison") && (infopok[b][numbertype2] == "Bug")) || ((infopok[b][numbertype2] == "Poison") && (infopok[b][numbertype1] == "Bug")))
                    kolbugpoison++;
            }

            Console.WriteLine(kolbugpoison);

            int kolgeneration2 = 0;
            int numberdefense = 0;

            //Создаем оформление
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("d)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Количество покемонов второго поколения с защитой < 50: ");
            Console.ForegroundColor = ConsoleColor.White;

            //Ищем индекс столбца Defense
            for (int x = 0; x < kol_column; x++)
            {
                if (infopok[0][x] == "Defense")
                    numberdefense = x;
            }

            //Ищем покемонов второго поколения с защитой < 50
            for (int d = 1; d < kol_row; d++)
            {
                if ((infopok[d][numbergeneration] == "2") && (int.Parse(infopok[d][numberdefense]) < 50))
                    kolgeneration2++;
            }
            Console.WriteLine(kolgeneration2);
        }
        catch
        {
            Console.WriteLine("Открыт файл, закройте его и выберите вариант из списка заново!");
            Menu();
        }
    }

    //Метод меню , который в зависимости от пожеланий пользователя напрвавляет его в нужный метод обработки пункта
    static void Menu()
    {
       
       
        if (!int.TryParse(Console.ReadLine(), out int number))
        {
            Console.WriteLine("Вы ввели недопустимое значение, повторите попытку");
            Menu();
        }
        else
        {
            switch (number)
            {
                case 1:
                    Point_1();
                    End();
                    break;
                case 2:
                    Point_2();
                    End();
                    break;
                case 3:
                    Point_3();
                    End();
                    break;
                case 4:
                    Point_4();
                    End();
                    break;
                case 5:
                    Point_5();
                    End();
                    break;
                default:
                    Console.WriteLine("Вы ввели неверную цифру, повторите попытку");
                    Menu();
                    break;
            }

        }
    }

    //Метод, который спрашивает пользователя о его желании закончить
    static void End()
    {
        string f;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Если хотите закончить, то введите \"1\", если хотите продолжить, то введите \"2\"");
        Console.ForegroundColor = ConsoleColor.White;
        f = Console.ReadLine();
        switch (f)
        {
            case "1":
                Console.WriteLine("Всего доброго!");
                break;
            case "2":
                Console.WriteLine("Введите номер пункта из списка");
                Menu();
                break;
            default:
                Console.WriteLine("Вы ввели некорректные данные, повторите попытку!");
                End();
                break;
        }
    }

    //Основной метод, выдающий список с функциями программы
    static void Main()
    {
        


        Console.WriteLine("Выберите пункт из меню:");
        Console.WriteLine("1. Вывести адрес файла");
        Console.WriteLine("2. Вывести на экран информацию о группах покемонов, являющихся легендарными и нелегендарными");
        Console.WriteLine("3. Вывести на экран список покемонов, относящихся к одному и тому же поколению.Вначале вывести информацию о самом мощном покемона в поколении");
        Console.WriteLine("4. Вывести на экран выборку покемонов, скорость которых не более, чем на 10 единиц меньше максимального значения скорости среди всех покемонов");
        Console.WriteLine("5. Вывести на экран: a)общее количество покемонов по поколениям;" +
                  "\n                     b)полные данные о самом слабом и самом сильном покемоне" +
                  "\n                     c)количество ядовитых жуков" +
                  "\n                     d)количество покемонов второго поколения, у которых защита меньше 50");
        Menu();
        
    }
}