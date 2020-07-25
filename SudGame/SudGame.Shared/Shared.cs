using System;
using System.Collections.Generic;
using System.Text;

namespace SudGame
{
    public static class Shared
    {
        public static string sharedMessage()
        {
            #if WINDOWS_APP
                return "Bem-vindo ao Simple Sudoku para Windows!";
            #endif
            #if WINDOWS_PHONE_APP
                return "Bem-vindo ao Simple Sudoku para Windows Phone!";
            #endif
        }
    }
}
