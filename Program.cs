using System;
using System.ComponentModel.Design;
using System.Data;
using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Xml.Schema;
namespace Xogame;

class Program
{
    static bool endgame;
    static string x_symbol = "x";
    static string y_symbol = "o";
    static int n = 15;
    static string[,] Caro_Table = new string[n, n];
    static int pos_x;
    static int pos_y;
    static int player = 1;
    static void Main(string[] args)
    {
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;
        Console.ForegroundColor = ConsoleColor.Green;
        //Câu 1: Viết hàm đặt tên là InitCaroTable để tạo 1 mảng 2 chiều đặt tên là caro-table có kích cỡ là n x n (với n = 15):
        InitCaroTable(n);
        DrawTable();
        do
        {

            Playgame();


        } while (true);


    }

    static void InitCaroTable(int n)
    {
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {
                if (row == 0)
                {
                    if (col < 10)
                    {
                        Caro_Table[row, col] = ($"0{col}");
                    }
                    else
                    {
                        Caro_Table[row, col] = ($"{col}");
                    }
                }
                else if (col == 0)
                {
                    if (col == 0)
                    {
                        if (row < 10)
                        {
                            Caro_Table[row, col] = ($"0{row}");
                        }
                        else
                        {
                            Caro_Table[row, col] = ($"{row}");
                        }
                    }
                }
                else
                {
                    Caro_Table[row, col] = ($"-");
                }
            }
        }
    }
    static void DrawTable()
    {
        Console.Clear();
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {
                if (Caro_Table[row, col] == ($"-") || Caro_Table[row, col] == ($"x") || Caro_Table[row, col] == ($"o"))
                {
                    Console.Write($"  {Caro_Table[row, col]} ");
                }
                else
                {
                    Console.Write($" {Caro_Table[row, col]} ");
                }
            }

            Console.WriteLine();
        }
    }

    static void Playgame()
    {
        bool isvalidPosition = true;
        if (player == 1)
        {
            Console.WriteLine("nguoi choi thu 1 :");
        }
        else
        {
            if (!endgame)

                Console.WriteLine("nguoi choi thu 2 :");
        }
        do
        {
            if (!isvalidPosition)
            {
                Console.WriteLine("Invalid Position");
            }
            if (endgame)
            {
                Environment.Exit(0);
            }
            Console.Write("Nhap so dong :");
            pos_x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Nhap so cot :");
            pos_y = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            isvalidPosition = pos_x > 0 || pos_y > 0 || pos_x < n || pos_y < n && Caro_Table[pos_x, pos_y] != "-";
        } while (!isvalidPosition);
        Caro_Table[pos_x, pos_y] = player == 1 ? x_symbol : y_symbol;
        DrawTable();
        endgame = Checkwin(pos_x, pos_y);
        if (endgame)
        {
            if (player == 1)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("nguoi choi 1 win ");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("nguoi choi 2 win ");
                Console.ResetColor();
            }
        }


        player = player == 1 ? 2 : 1;

    }
    static bool Checkwin(int x, int y)
    {
        return Checkhorizontal(x, y) || Checkvertical(x, y) || CheckDiagonal(x, y) || CheckDiagonalSecondDiagonal(x, y);
    }

    static bool Checkhorizontal(int current_x, int current_y)
    {
        int count = 0;
        string count_symbol = (player == 1 ? x_symbol : y_symbol);

        for (int y = 0; y < n; y++)
        {
            if (Caro_Table[current_x, y] == count_symbol)
            {
                count++;
                if (count == 3)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
    static bool Checkvertical(int current_x, int current_y)
    {
        int count = 0;
        string count_symbol = player == 1 ? x_symbol : y_symbol;

        for (int x = 0; x < n; x++)
        {
            if (Caro_Table[x, current_y] == count_symbol)
            {
                count++;
                if (count == 3)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }

    static bool CheckDiagonal(int current_x, int current_y)
    {
        int count = 0;
        string count_symbol = player == 1 ? x_symbol : y_symbol;
        for (int i = -2; i <= 2; i++)
        {
            int x = current_x + i;
            int y = current_y + i;
            if (x >= 0 && x < n && y >= 0 && y < n && Caro_Table[x, y] == count_symbol)
            {
                count++;
                if (count == 3)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
    static bool CheckDiagonalSecondDiagonal(int current_x, int current_y)
    {
        int count = 0;
        string count_symbol = player == 1 ? x_symbol : y_symbol;
        for (int i = -2; i <= 2; i++)
        {
            int x = current_x + i;
            int y = current_y - i;
            if (x >= 0 && x < n && y >= 0 && y < n && Caro_Table[x, y] == count_symbol)
            {
                count++;
                if (count == 3)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }

}











