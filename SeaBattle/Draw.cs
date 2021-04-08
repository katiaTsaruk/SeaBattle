using System;

namespace SeaBattle
{
    public class Draw
    {
        private Field field = new Field();
        public void ClearLine(int minusY)
        {
            Console.SetCursorPosition(0, Console.CursorTop-minusY);
            Console.Write(new String(' ', Console.BufferWidth));
        }
       
        public void DrawField(int extraDistance)
        {
            Console.Write(" ");
            for (int i = 0; i < field.Size.x - 1; i++)
            {
                Console.Write(i);
            }

            Console.WriteLine(" ");
            for (int i = 0; i < field.Size.y - 1; i++)
            {
                Console.WriteLine(i);
            }
            
            for (int i = 1; i < field.Size.x; i++)
            {
                for (int j = 1+extraDistance; j < field.Size.y+extraDistance; j++)
                {
                    PaintCell(i,j,ConsoleColor.White," ");
                }
            }
        }

        public void DrawShootResult(int x, int y,bool isHit)
        {
            string shootResult;
            ConsoleColor resultColor;
            if (isHit)
            {
                shootResult = "Hit";
                resultColor = ConsoleColor.Red;
            }
            else
            {
                shootResult = "Miss";
                resultColor = ConsoleColor.Green;
            }
            PaintCell(x, y, resultColor, "#");
            Console.SetCursorPosition(1, field.Size.y * 2 + 2);
            Console.WriteLine(shootResult);
            Console.Write("Press ENTER to continue");
            Console.ReadLine();
            ClearLine(1);
            ClearLine(2);
        }

        public void CreateLongRowShip(int length, string ShipSymbol)
        {
            for (int i = 0; i < length; i++)
            {
                PaintCell(Console.CursorLeft, Console.CursorTop, ConsoleColor.White, ShipSymbol);
                field.CellCoord[Console.CursorLeft-1, Console.CursorTop].isFree = false;
            }
        }
        public void CreateLongColumnShip(int length,  string ShipSymbol)
        {
            int x = Console.CursorLeft;
            for (int i = 0; i < length; i++)
            {
                PaintCell(x, Console.CursorTop+1, ConsoleColor.White, ShipSymbol);
                field.CellCoord[Console.CursorLeft-1, Console.CursorTop].isFree = false;
            }
        }
        public void PaintCell(int x, int y, ConsoleColor color,string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Cell cell = new Cell(true);
            field.CellCoord[x, y] = cell;
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}