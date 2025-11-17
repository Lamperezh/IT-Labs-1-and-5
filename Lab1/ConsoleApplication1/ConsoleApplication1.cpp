#include "pch.h"
using namespace System;

int main(array<System::String^>^ args)
{
    Console::WriteLine("Введіть перший рядок:");
    String^ str1 = Console::ReadLine();
    Console::WriteLine("Введіть другий рядок:");
    String^ str2 = Console::ReadLine();
    int length1 = str1->Length;
    int length2 = str2->Length;
    if (length1 > length2)
    {
        Console::WriteLine("Перший рядок довший. Довжина: {0}", length1);
    }
    else if (length2 > length1)
    {
        Console::WriteLine("Другий рядок довший. Довжина: {0}", length2);
    }
    else
    {
        Console::WriteLine("Рядки мають однакову довжину: {0}", length1);
    }
    Console::ReadLine(); 
    return 0;
}