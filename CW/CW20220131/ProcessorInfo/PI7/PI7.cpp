#include <iostream>
//#include <sysinfoapi.h>
#include <stdio.h>
#include <Windows.h>
#include <tchar.h>

using namespace std;

typedef struct _SYSTEM_INFO {
    union {
        DWORD dwOemId;
        struct {
            WORD wProcessorArchitecture;
            WORD wReserved;
        } DUMMYSTRUCTNAME;
    } DUMMYUNIONNAME;
    DWORD     dwPageSize;
    LPVOID    lpMinimumApplicationAddress;
    LPVOID    lpMaximumApplicationAddress;
    DWORD_PTR dwActiveProcessorMask;
    DWORD     dwNumberOfProcessors;
    DWORD     dwProcessorType;
    DWORD     dwAllocationGranularity;
    WORD      wProcessorLevel;
    WORD      wProcessorRevision;
} SYSTEM_INFO, * LPSYSTEM_INFO;


int main()
{
    //cout << "Hello World!\n";

    void GetSystemInfo(
        [out] LPSYSTEM_INFO lpSystemInfo
    );

}
