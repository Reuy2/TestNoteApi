using System;

namespace Test1
{
    public static class ErrorHandler
    {

        public static void Log(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
