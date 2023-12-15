using System;

namespace MazeGeneratingByBacktracking
{
    internal static class Input
    {
        public static uint ReadUnsignedInt(string message)
        {
            bool parsed = false;
            string input = string.Empty;
            uint value = 0;

            while (!parsed) 
            {
                Console.WriteLine(message);
                input = Console.ReadLine();

                if(uint.TryParse(input, out value))
                {
                    parsed = true;
                }
                else 
                {
                    Console.WriteLine("Неверный ввод. Попробуйте еще раз.");
                }
            }

            return value;
        }
    }
}
