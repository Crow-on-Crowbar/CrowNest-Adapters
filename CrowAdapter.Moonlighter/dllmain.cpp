// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "TemplateAdapter.h"

TemplateAdapter templateAdapter;

BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        templateAdapter.Attach();
        break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        templateAdapter.Detach();
        break;
    }
    return TRUE;
}

extern "C" __declspec(dllexport) IAdapter* CreateAdapter()
{
    return new TemplateAdapter();
}