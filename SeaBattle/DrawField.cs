using System;

namespace SeaBattle
{
    public class DrawField
    {
        public void ClearLine(int minusY)
        {
            Console.SetCursorPosition(0, Console.CursorTop-minusY);
            Console.Write(new String(' ', Console.BufferWidth));
        }
    }
}