
#include <iostream>
#include <sysinfoapi.h>
#include <stdio.h>
#include <Windows.h>

//using namespace std;


int main()
{
    std::cout << "Hello World!\n";

    SYSTEM_INFO sysInfo;
    GetSystemInfo(&sysInfo);

    int i = 0;

    //switch (sysInfo.wProcessorArchitecture)
    //{
    //case 6:
    //    //return i = 64;
    //    i = 64;
    //    break;
    //case 9:
    //    //return i = 64;
    //    i = 64;
    //    break;
    //case 0:
    //    //return i = 32;
    //    i = 32;
    //    break;
    //default:
    //    //return i = -1;
    //    i = -1;
    //}

    printf("i = %i ", i);

}

