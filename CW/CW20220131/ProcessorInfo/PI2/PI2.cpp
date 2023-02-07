//// PI2.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
////
//
//#include <iostream>
//
//int main()
//{
//    std::cout << "Hello World!\n";
//}
//
//// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
//// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"
//
//// Советы по началу работы 
////   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
////   2. В окне Team Explorer можно подключиться к системе управления версиями.
////   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
////   4. В окне "Список ошибок" можно просматривать ошибки.
////   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
////   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.

#include <windows.h>
#include <stdio.h>
#include <iostream>

using namespace std;

void main()
{
    SYSTEM_INFO sisInfo;

    // Copy the hardware information to the SYSTEM_INFO structure. 

    GetSystemInfo(&sisInfo);

    // Display the contents of the SYSTEM_INFO structure. 

    printf("Hardware information: \n");
    printf("  OEM ID: %u\n", 
        sisInfo.dwOemId);
    printf("  Number of processors: %u\n",
        sisInfo.dwNumberOfProcessors);
    printf("  Page size: %u\n", 
        sisInfo.dwPageSize);
    printf("  Processor type: %u\n", 
        sisInfo.dwProcessorType);
    printf("  Minimum application address: %lx\n",
        sisInfo.lpMinimumApplicationAddress);
    printf("  Maximum application address: %lx\n",
        sisInfo.lpMaximumApplicationAddress);
    printf("  Active processor mask: %u\n",
        sisInfo.dwActiveProcessorMask);

    cout << "=======================================================";

    printf("Processor info: \n");
    printf("  ID = (dwActiveProcessorMask) %i\n", sisInfo.dwActiveProcessorMask);
    printf("  Socket = (dwAllocationGranularity) %u\n", sisInfo.dwAllocationGranularity);
    printf("  Cores = (dwNumberOfProcessors) %u\n", sisInfo.dwNumberOfProcessors);
    printf("  dwProcessorType = %u\n", sisInfo.dwProcessorType);
    printf("  lpMaximumApplicationAddress = %u\n", sisInfo.lpMaximumApplicationAddress);
    printf("  Architecture = (wProcessorArchitecture) %u\n", sisInfo.wProcessorArchitecture);
    printf("  wProcessorLevel = () %u\n", sisInfo.wProcessorLevel);
    printf("  wProcessorRevision = () %u\n", sisInfo.wProcessorRevision);
    printf("  wReserved = () %u\n", sisInfo.wReserved);
    



    cout << "\nNumber of Cores: \n" << sisInfo.dwNumberOfProcessors << endl;
    
    //cpuinfo_initialize();
    //printf("Running on %s CPU\n", cpuinfo_get_package(0)->name);



}