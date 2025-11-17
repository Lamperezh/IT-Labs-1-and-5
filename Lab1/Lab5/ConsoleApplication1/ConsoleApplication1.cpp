#include "pch.h"
#include <windows.h>  // Для SetConsoleOutputCP

using namespace System;

int main(array<System::String^>^ args)
{
    // Встановлення кодування консолі на UTF-8 для коректного відображення кирилиці
    SetConsoleOutputCP(CP_UTF8);

    // Константи
    const double T = 1800.0;  // Обертальний момент, кгс·см
    const int SIZE = 6;       // Кількість значень напружень

    // Масив для напружень τ (кгс/см²)
    array<int>^ tau_values = { 100, 120, 150, 170, 200, 250 };

    // Масив для діаметрів d (см)
    array<double>^ diameters = gcnew array<double>(SIZE);

    // Обчислення діаметрів за формулою d = (16 * T / (π * τ))^(1/3)
    for (int i = 0; i < tau_values->Length; i++)
    {
        double tau = tau_values[i];
        diameters[i] = Math::Pow(16.0 * T / (Math::PI * tau), 1.0 / 3.0);
    }

    Console::WriteLine("+----------------------------------------------------+");
    Console::WriteLine("|         Таблиця розрахунків діаметра вала          |");
    Console::WriteLine("+------+--------------------+--------------------+");

    // Шапка таблиці
    Console::Write("| №    | ");
    Console::Write("{0,-18}", "Напруження τ (кгс/см²)");
    Console::Write(" | ");
    Console::WriteLine("{0,-18}", "Діаметр d (см) |");

    // Роздільна лінія
    Console::WriteLine("+------+--------------------+--------------------+");

    // Виведення рядків таблиці з номерами
    for (int i = 0; i < tau_values->Length; i++)
    {
        Console::Write("| {0,-4} | ", i + 1);  // Номер рядка
        Console::Write("{0,-18}", tau_values[i]);
        Console::Write(" | ");
        Console::WriteLine("{0,-18:F2} |", diameters[i]);
    }

    // Закриття таблиці
    Console::WriteLine("+------+--------------------+--------------------+");
    Console::WriteLine("\nНатисніть будь-яку клавішу для виходу...");
    Console::ReadKey();

    return 0;
}