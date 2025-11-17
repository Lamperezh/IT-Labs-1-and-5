# Лабораторна робота №1
# Автор: [Шкурко Дмитро]
# Мета: Навчитися працювати з [циклами/масивами/функціями]
# Опис: Програма обчислює суму чисел від 1 до N

#include "pch.h"
using namespace System;

int main(array<System::String^>^ args)
{
    Console::WriteLine("Ââåä³òü ïåðøèé ðÿäîê:");
    String^ str1 = Console::ReadLine();
    Console::WriteLine("Ââåä³òü äðóãèé ðÿäîê:");
    String^ str2 = Console::ReadLine();
    int length1 = str1->Length;
    int length2 = str2->Length;
    if (length1 > length2)
    {
        Console::WriteLine("Ïåðøèé ðÿäîê äîâøèé. Äîâæèíà: {0}", length1);
    }
    else if (length2 > length1)
    {
        Console::WriteLine("Äðóãèé ðÿäîê äîâøèé. Äîâæèíà: {0}", length2);
    }
    else
    {
        Console::WriteLine("Ðÿäêè ìàþòü îäíàêîâó äîâæèíó: {0}", length1);
    }
    Console::ReadLine(); 
    return 0;

}
