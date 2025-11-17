#include "pch.h"
#include <windows.h>  // Для SetConsoleOutputCP

using namespace System;

int main(array<System::String^>^ args)
{
   
    SetConsoleOutputCP(CP_UTF8);

    // Константи
    const int ROWS = 4;  // Кількість рядків
    const int COLS = 4;  // Кількість стовпців

    // Створення матриці
    array<int, 2>^ matrix = gcnew array<int, 2>(ROWS, COLS);

    // Введення елементів матриці вручну
    Console::WriteLine("Введіть елементи матриці {0}x{1}:", ROWS, COLS);
    for (int i = 0; i < ROWS; i++)
    {
        for (int j = 0; j < COLS; j++)
        {
            Console::Write("Елемент [{0},{1}]: ", i, j);
            String^ input = Console::ReadLine();
            matrix[i, j] = Int32::Parse(input);  // Перетворення введеного рядка в ціле число
        }
    }

    // Копія матриці для збереження вихідного вигляду
    array<int, 2>^ originalMatrix = gcnew array<int, 2>(ROWS, COLS);
    for (int i = 0; i < ROWS; i++)
        for (int j = 0; j < COLS; j++)
            originalMatrix[i, j] = matrix[i, j];

    // Пошук найменшого елемента та його індексів
    int minValue = matrix[0, 0];
    int minRow = 0;
    int minCol = 0;
    for (int i = 0; i < ROWS; i++)
    {
        for (int j = 0; j < COLS; j++)
        {
            if (matrix[i, j] < minValue)
            {
                minValue = matrix[i, j];
                minRow = i;
                minCol = j;
            }
        }
    }

    // Заміна елементів рядка та стовпця на 0
    for (int j = 0; j < COLS; j++)
        matrix[minRow, j] = 0;  // Рядок
    for (int i = 0; i < ROWS; i++)
        matrix[i, minCol] = 0;  // Стовпець

    // Виведення заголовків
    Console::WriteLine("+----------------------------------------------------+");
    Console::WriteLine("|         Вихідна матриця та результат обробки       |");
    Console::WriteLine("+----------------------------------------------------+");

    // Виведення вихідної матриці
    Console::WriteLine("| Вихідна матриця:                                   |");
    Console::WriteLine("+------+------+------+------+");
    for (int i = 0; i < ROWS; i++)
    {
        Console::Write("| ");
        for (int j = 0; j < COLS; j++)
        {
            Console::Write("{0,4} ", originalMatrix[i, j]);
        }
        Console::WriteLine("|");
    }
    Console::WriteLine("+------+------+------+------+");

    // Виведення зміненої матриці
    Console::WriteLine("| Змінена матриця (нулі в рядку та стовпці):         |");
    Console::WriteLine("+------+------+------+------+");
    for (int i = 0; i < ROWS; i++)
    {
        Console::Write("| ");
        for (int j = 0; j < COLS; j++)
        {
            Console::Write("{0,4} ", matrix[i, j]);
        }
        Console::WriteLine("|");
    }
    Console::WriteLine("+------+------+------+------+");

    // Додаткове повідомлення
    Console::WriteLine("\nНайменший елемент: {0}, знайдено в рядку {1}, стовпці {2}",
        minValue, minRow + 1, minCol + 1);
    Console::WriteLine("Натисніть будь-яку клавішу для виходу...");
    Console::ReadKey();

    return 0;
}