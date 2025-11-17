# Лабораторна робота №5
# Робота з файлами




#include "pch.h"
#include <windows.h>  // Äëÿ SetConsoleOutputCP

using namespace System;

int main(array<System::String^>^ args)
{
   
    SetConsoleOutputCP(CP_UTF8);

    // Êîíñòàíòè
    const int ROWS = 4;  // Ê³ëüê³ñòü ðÿäê³â
    const int COLS = 4;  // Ê³ëüê³ñòü ñòîâïö³â

    // Ñòâîðåííÿ ìàòðèö³
    array<int, 2>^ matrix = gcnew array<int, 2>(ROWS, COLS);

    // Ââåäåííÿ åëåìåíò³â ìàòðèö³ âðó÷íó
    Console::WriteLine("Ââåä³òü åëåìåíòè ìàòðèö³ {0}x{1}:", ROWS, COLS);
    for (int i = 0; i < ROWS; i++)
    {
        for (int j = 0; j < COLS; j++)
        {
            Console::Write("Åëåìåíò [{0},{1}]: ", i, j);
            String^ input = Console::ReadLine();
            matrix[i, j] = Int32::Parse(input);  // Ïåðåòâîðåííÿ ââåäåíîãî ðÿäêà â ö³ëå ÷èñëî
        }
    }

    // Êîï³ÿ ìàòðèö³ äëÿ çáåðåæåííÿ âèõ³äíîãî âèãëÿäó
    array<int, 2>^ originalMatrix = gcnew array<int, 2>(ROWS, COLS);
    for (int i = 0; i < ROWS; i++)
        for (int j = 0; j < COLS; j++)
            originalMatrix[i, j] = matrix[i, j];

    // Ïîøóê íàéìåíøîãî åëåìåíòà òà éîãî ³íäåêñ³â
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

    // Çàì³íà åëåìåíò³â ðÿäêà òà ñòîâïöÿ íà 0
    for (int j = 0; j < COLS; j++)
        matrix[minRow, j] = 0;  // Ðÿäîê
    for (int i = 0; i < ROWS; i++)
        matrix[i, minCol] = 0;  // Ñòîâïåöü

    // Âèâåäåííÿ çàãîëîâê³â
    Console::WriteLine("+----------------------------------------------------+");
    Console::WriteLine("|         Âèõ³äíà ìàòðèöÿ òà ðåçóëüòàò îáðîáêè       |");
    Console::WriteLine("+----------------------------------------------------+");

    // Âèâåäåííÿ âèõ³äíî¿ ìàòðèö³
    Console::WriteLine("| Âèõ³äíà ìàòðèöÿ:                                   |");
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

    // Âèâåäåííÿ çì³íåíî¿ ìàòðèö³
    Console::WriteLine("| Çì³íåíà ìàòðèöÿ (íóë³ â ðÿäêó òà ñòîâïö³):         |");
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

    // Äîäàòêîâå ïîâ³äîìëåííÿ
    Console::WriteLine("\nÍàéìåíøèé åëåìåíò: {0}, çíàéäåíî â ðÿäêó {1}, ñòîâïö³ {2}",
        minValue, minRow + 1, minCol + 1);
    Console::WriteLine("Íàòèñí³òü áóäü-ÿêó êëàâ³øó äëÿ âèõîäó...");
    Console::ReadKey();

    return 0;

}
