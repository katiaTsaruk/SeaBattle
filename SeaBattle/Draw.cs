using System;

namespace SeaBattle
{
    public class Draw
    {
        public Field[] field = new Field[2];
        public void ClearLine(int minusY)
        {
            Console.SetCursorPosition(0, Console.CursorTop-minusY);
            Console.Write(new String(' ', Console.BufferWidth));
        }
       
        public void DrawField(int extraDistance)
        {
            Console.Write(" ");
            for (int i = 0; i < field[1].Size.x ; i++)
            {
                Console.Write(i);
            }

            Console.WriteLine(" ");
            for (int i = 0; i < field[0].Size.y; i++)
            {
                Console.WriteLine(i);
            }
            
            for (int i = 1; i <= field[0].Size.x; i++)
            {
                for (int j = 1+extraDistance; j <= field[0].Size.y+extraDistance; j++)
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
            Console.SetCursorPosition(1, field[0].Size.y * 2 + 4);
            Console.WriteLine(shootResult);
            Console.Write("Press ENTER to continue");
            Console.ReadLine();
            ClearLine(1);
            ClearLine(2);
        }
        public void DrawTwoFields(string[] name)
        {
            DrawField(0);
            Console.WriteLine("");
            Console.WriteLine(name[0]);
            DrawField(field[0].Size.y+2);
            Console.WriteLine("");
            Console.WriteLine(name[1]);
        }
        public void CreateLongRowShip(int length, string ShipSymbol)
        {
            int playerCheck = Console.CursorTop > field[0].Size.y ? 1 : 0;
            for (int i = 0; i < length; i++)
            {
                PaintCell(Console.CursorLeft, Console.CursorTop, ConsoleColor.White, ShipSymbol);
                field[playerCheck].CellCoord[Console.CursorLeft-1, Console.CursorTop].isFree = false;
            }
        }
        public void CreateLongColumnShip(int length,  string ShipSymbol)
        {
            int playerCheck = Console.CursorTop > field[0].Size.y ? 1 : 0;
            int x = Console.CursorLeft;
            for (int i = 0; i < length; i++)
            {
                PaintCell(x, Console.CursorTop+1, ConsoleColor.White, ShipSymbol);
                field[playerCheck].CellCoord[Console.CursorLeft-1, Console.CursorTop].isFree = false;
            }
        }
        public void PaintCell(int x, int y, ConsoleColor color,string symbol)
        {
            int playerCheck = y > field[0].Size.y ? 1 : 0;
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Cell cell = new Cell(true);
            field[playerCheck].CellCoord[x, y] = cell;
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}