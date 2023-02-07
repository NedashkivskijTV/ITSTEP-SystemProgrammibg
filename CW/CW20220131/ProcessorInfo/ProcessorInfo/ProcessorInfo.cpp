/*// ProcessorInfo.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
//#include <sysinfoapi.h>
#include <stdio.h>
#include <Windows.h>
#include <tchar.h>

using namespace std;

//typedef struct _SYSTEM_INFO {
//    union {
//        DWORD dwOemId;
//        struct {
//            WORD wProcessorArchitecture;
//            WORD wReserved;
//        } DUMMYSTRUCTNAME;
//    } DUMMYUNIONNAME;
//    DWORD     dwPageSize;
//    LPVOID    lpMinimumApplicationAddress;
//    LPVOID    lpMaximumApplicationAddress;
//    DWORD_PTR dwActiveProcessorMask;
//    DWORD     dwNumberOfProcessors;
//    DWORD     dwProcessorType;
//    DWORD     dwAllocationGranularity;
//    WORD      wProcessorLevel;
//    WORD      wProcessorRevision;
//} SYSTEM_INFO, * LPSYSTEM_INFO;


int main()
{
    cout << "Hello World!\n";

    //void GetSystemInfo(
    //    [out] LPSYSTEM_INFO lpSystemInfo
    //);

}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
*/




#include <stdio.h>
#include <Windows.h>
#include <tchar.h>

// SMBIOS Table Type numbers
#define SMB_TABLE_BIOS              0
#define SMB_TABLE_SYSTEM            1
#define SMB_TABLE_BASEBOARD         2
#define SMB_TABLE_CHASSIS           3
#define SMB_TABLE_PROCESSOR         4
#define SMB_TABLE_MEMCTRL           5
#define SMB_TABLE_MEMMODULES        6
#define SMB_TABLE_PORTS             8
#define SMB_TABLE_SLOTS             9
#define SMB_TABLE_OEM_STRINGS       11
#define SMB_TABLE_SYS_CFG_OPTIONS   12
#define SMB_TABLE_MEM_ARRAY         16
#define SMB_TABLE_MEM_DEVICE        17
#define SMB_TABLE_END_OF_TABLE      127

// 64bit Word type
typedef unsigned long long QWORD;

/*
* Structures
*/
typedef struct _RawSmbiosData
{
    BYTE    Used20CallingMethod;
    BYTE    SMBIOSMajorVersion;
    BYTE    SMBIOSMinorVersion;
    BYTE    DmiRevision;
    DWORD   Length;
    BYTE    SMBIOSTableData[1];
} RAW_SMBIOS_DATA, * PRAW_SMBIOS_DATA;

typedef struct _SmbiosStructHeader
{
    BYTE Type;
    BYTE Length;
    WORD Handle;
} SMBIOS_STRUCT_HEADER, * PSMBIOS_STRUCT_HEADER;

// Structures
typedef struct _NODE {
    wchar_t* Name;                      // Name of the node
    struct _NODE_ATT_LINK* Attributes;  // Array of attributes linked to the node
    struct _NODE* Parent;               // Parent node
    struct _NODE_LINK* Children;        // Array of linked child nodes
    int Flags;                          // Node configuration flags
} NODE, * PNODE;

typedef struct _NODE_LINK {
    struct _NODE* LinkedNode;           // Node attached to this node
} NODE_LINK, * PNODE_LINK;

typedef struct _NODE_ATT {
    wchar_t* Key;                       // Attribute name
    wchar_t* Value;                     // Attribute value string (may be null separated multistring if NAFLG_ARRAY is set)
    int Flags;                          // Attribute configuration flags
} NODE_ATT, * PNODE_ATT;

typedef struct _NODE_ATT_LINK {
    struct _NODE_ATT* LinkedAttribute;  // Attribute linked to this node
} NODE_ATT_LINK, * PNODE_ATT_LINK;

/********************************************************************/

PRAW_SMBIOS_DATA GetSmbiosData()
{
    DWORD bufferSize = 0;

    PRAW_SMBIOS_DATA smbios = NULL;

    // Get required buffer size
    bufferSize = GetSystemFirmwareTable('RSMB', 0, NULL, 0);
    if (bufferSize) {
        smbios = (PRAW_SMBIOS_DATA)LocalAlloc(LPTR, bufferSize);
        bufferSize = GetSystemFirmwareTable('RSMB', 0, (PVOID)smbios, bufferSize);
    }

    return smbios;
}

PSMBIOS_STRUCT_HEADER GetNextStructure(PRAW_SMBIOS_DATA smbios, PSMBIOS_STRUCT_HEADER previous)
{
    PSMBIOS_STRUCT_HEADER next = NULL;
    PBYTE c = NULL;

    // Return NULL is no data found
    if (NULL == smbios)
        return NULL;

    // Return first table if previous was NULL
    if (NULL == previous)
        return (PSMBIOS_STRUCT_HEADER)(&smbios->SMBIOSTableData[0]);

    // Move to the end of the formatted structure
    c = ((PBYTE)previous) + previous->Length;

    // Search for the end of the unformatted structure (\0\0)
    while (true) {
        if ('\0' == *c && '\0' == *(c + 1)) {
            /* Make sure next table is not beyond end of SMBIOS data
             * (Thankyou Microsoft for ommitting the structure count
             * in GetSystemFirmwareTable
             */
            if ((c + 2) < ((PBYTE)smbios->SMBIOSTableData + smbios->Length))
                return (PSMBIOS_STRUCT_HEADER)(c + 2);
            else
                return NULL; // We reached the end
        }

        c++;
    }

    return NULL;
}

PSMBIOS_STRUCT_HEADER GetNextStructureOfType(PRAW_SMBIOS_DATA smbios, PSMBIOS_STRUCT_HEADER previous, DWORD type)
{
    PSMBIOS_STRUCT_HEADER next = previous;
    while (NULL != (next = GetNextStructure(smbios, next))) {
        if (type == next->Type)
            return next;
    }

    return NULL;
}

PSMBIOS_STRUCT_HEADER GetStructureByHandle(PRAW_SMBIOS_DATA smbios, WORD handle)
{
    PSMBIOS_STRUCT_HEADER header = NULL;

    while (NULL != (header = GetNextStructure(smbios, header)))
        if (handle == header->Handle)
            return header;

    return NULL;
}

void GetSmbiosString(PSMBIOS_STRUCT_HEADER table, BYTE index, LPWSTR output, int cchOutput)
{
    DWORD i = 0;
    DWORD len = 0;
    //wcscpy(output, L"");

    if (0 == index) return;

    char* c = NULL;

    for (i = 1, c = (char*)table + table->Length; '\0' != *c; c += strlen(c) + 1, i++) {
        if (i == index) {
            len = MultiByteToWideChar(CP_UTF8, 0, c, -1, output, cchOutput);
            break;
        }
    }
}

//вывод значения числового параметра таблицы SMBIOS по указанному смещению
void PrintBiosValue(PRAW_SMBIOS_DATA smbios, DWORD type, DWORD offset, DWORD size)
{
    PSMBIOS_STRUCT_HEADER head = NULL;
    PBYTE cursor = NULL;

    head = GetNextStructureOfType(smbios, head, type);
    if (NULL == head) { printf("PrintBiosValue Error!\n"); return; }

    cursor = ((PBYTE)head + offset);

    //value           
    for (int i = 0; i < size; i++) {
        printf("%02x", (unsigned int)*cursor);
        cursor++;
    }
    printf("\n");
}

//вывод значения строкового параметра таблицы SMBIOS по указанному смещению
void PrintBiosString(PRAW_SMBIOS_DATA smbios, DWORD type, DWORD offset)
{
    PSMBIOS_STRUCT_HEADER head;
    head = NULL;
    PBYTE cursor = NULL;
    WCHAR buf[1024];

    head = GetNextStructureOfType(smbios, head, type);
    if (NULL == head) { printf("PrintString Error!\n"); return; }
    cursor = ((PBYTE)head + offset);
    BYTE val = *cursor;

    GetSmbiosString((head), *cursor, buf, 1024);
    //  value           
    wprintf(L"%s\n", buf);
}

int main() {

    PRAW_SMBIOS_DATA data = GetSmbiosData();

    if (data == NULL) {
        printf("Can't get SMBIOS data!");
        return 1;
    }

    printf("System UUID: ");
    PrintBiosValue(data, SMB_TABLE_SYSTEM, 8, 16);

    printf("Chassis serial: ");
    PrintBiosString(data, SMB_TABLE_CHASSIS, 7);

    printf("Motherboard serial: ");
    PrintBiosString(data, SMB_TABLE_BASEBOARD, 7);

    printf("CPUID: ");
    PrintBiosValue(data, SMB_TABLE_PROCESSOR, 8, 8);  //Таблица SMBIOS содержит только 2 DWORD-значения CPUID из 4, но этого обычно достаточно

    getchar();
}
