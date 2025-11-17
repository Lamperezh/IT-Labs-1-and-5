#include "pch.h"
using namespace System;

int main(array<System::String^>^ args)
{
    Console::WriteLine("Р-1: Введіть рядок:");
    String^ str1 = Console::ReadLine();
    String^ result1 = "begin " + str1 + " end";
    int length1 = result1->Length;
    Console::WriteLine("Результат: {0}", result1);
    Console::WriteLine("Довжина нового рядка: {0}", length1);
    Console::WriteLine();
    Console::WriteLine("Р-2: Введіть перший рядок:");
    String^ str2a = Console::ReadLine();
    Console::WriteLine("Введіть другий рядок:");
    String^ str2b = Console::ReadLine();
    String^ result2 = str2a + str2b;
    Console::WriteLine("Об’єднаний рядок: {0}", result2);
    Console::WriteLine();

    Console::WriteLine("Р-3: Введіть перший рядок:");
    String^ str3a = Console::ReadLine();
    Console::WriteLine("Введіть другий рядок:");
    String^ str3b = Console::ReadLine();
    if (String::Compare(str3a, str3b) == 0)
    {
        Console::WriteLine("Рядки однакові.");
    }
    else
    {
        Console::WriteLine("Рядки різні.");
    }
    Console::WriteLine();

    Console::WriteLine("Р-4: Введіть рядок:");
    String^ str4 = Console::ReadLine();
    array<wchar_t>^ chars = str4->ToCharArray();
    Array::Reverse(chars);
    String^ result4 = gcnew String(chars);
    Console::WriteLine("Перевернутий рядок: {0}", result4);

    Console::ReadLine(); 
    return 0;
}