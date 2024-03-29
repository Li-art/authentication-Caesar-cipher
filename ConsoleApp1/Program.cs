﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Для регистрации нового пользователя нажмите 1, для аутентификации пользователя нажмите 2\n");
            switch (Console.ReadLine())
            {
                case "1":
                    Reg();
                    break;
                case "2":
                    Pruf();
                    break;
            }

            void Reg()
            {
                StreamWriter sw = new StreamWriter("Test.txt");

                Console.Title = "Регистрация";
                Console.Clear();

                Console.WriteLine("Введите логин:");
                String log = Console.ReadLine();
                sw.WriteLine(log);

                {                    
                    Console.WriteLine("Введите пароль:");
                    ConsoleKeyInfo key;
                    string code = "";
                    int n = 0;
                    do
                    {
                        key = Console.ReadKey(true);
                        if (Char.IsLetter(key.KeyChar))
                        {
                            Console.Write("*");
                            n++;
                        }
                        code += key.KeyChar;
                    }

                    while (key.Key != ConsoleKey.Enter);
                    if (n < 10)
                    {
                        Console.WriteLine("\n Пароль должен быть минимум 10 символов!");
                        Console.ReadKey();
                    }
                            
                    int k = 3;
                   
                    char[] result = code.ToCharArray();
                    char kBig = (char)((k % 26) - 26); // 26 это количество букв в англ. алфавите. Если смещение сдвигает букву
                    for (int i = 0; i < n; i++)      // за область 'A' - 'Z'. нужно вернуть чтобы буква оставалась буквой.
                    {
                        if (result[i] >= 'A' && result[i] <= 'Z')
                        {
                            if (result[i] + k % 26 > 'Z') result[i] += kBig;
                            else if (result[i] + k % 26 <= 'Z') result[i] += (char)(k % 26);
                        }

                        if (result[i] >= 'a' && result[i] < 'z')
                        {
                            if (result[i] + k % 26 > 'z') result[i] += kBig;
                            else if (result[i] + k % 26 <= 'z') result[i] += (char)(k % 26);
                        }
                    }

                    sw.WriteLine(result);

                }

                sw.Close();                
            }
            void Pruf()
            {               
                if (File.Exists("Test.txt"))
                {
                    using (StreamReader read = File.OpenText("Test.txt"))
                    {
                        Console.Clear();
                        Console.WriteLine("Ведите логин и пароль");
                        String log = Console.ReadLine();
                        string line1 = File.ReadLines("Test.txt").First();
                        if (line1 != log)
                        {
                            Console.WriteLine("Такого пользователя не существует!");
                            Console.ReadKey();
                        }

                        String pass = Console.ReadLine();
                        string line2 = File.ReadLines("Test.txt").Skip(1).First();

                        int k = 3;
                        char[] result = pass.ToCharArray();
                        char kBig = (char)((k % 26) - 26); // 26 это количество букв в англ. алфавите. Если смещение сдвигает букву 
                        for (int i = 0; i < result.Length; i++)      // за область 'A' - 'Z'. нужно вернуть чтобы буква оставалась буквой.
                        {
                            if (result[i] >= 'A' && result[i] <= 'Z')
                            {
                                if (result[i] + k % 26 > 'Z') result[i] += kBig;
                                else if (result[i] + k % 26 <= 'Z') result[i] += (char)(k % 26);
                            }

                            if (result[i] >= 'a' && result[i] < 'z')
                            {
                                if (result[i] + k % 26 > 'z') result[i] += kBig;
                                else if (result[i] + k % 26 <= 'z') result[i] += (char)(k % 26);
                            }
                        }
                        String str = new String(result);
                        if (line2 != str)
                        {
                            Console.WriteLine("Пароль неверный!");
                            Console.ReadKey();
                        }

                        if (line2 == str && line1 == log)
                        {
                            Console.WriteLine("Добро пожаловать!");
                            Console.ReadKey();
                        }                       
                    }
                }
            }
        }
    }
}
