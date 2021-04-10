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

        public Draw()
        {
            field[0] = new Field();
            field[1] = new Field();
        }
        public void DrawField(int extraDist)
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
            
            for (int i = 0; i < field[0].Size.x; i++)
            {
                for (int j = extraDist; j < field[0].Size.y+extraDist; j++)
                {
                    PaintCell(i+1,j+1,ConsoleColor.White," ");
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
            int playerCheck ;
            int extraDist;
            if (Console.CursorTop > field[0].Size.y)
            {
                playerCheck = 1;
                extraDist= field[0].Size.y+2;
            }
            else
            {
                playerCheck = 0;
                extraDist = 0;
            }
            for (int i = 0; i < length; i++)
            {
                PaintCell(Console.CursorLeft, Console.CursorTop, ConsoleColor.White, ShipSymbol);
                field[playerCheck].CellCoord[Console.CursorLeft-2, Console.CursorTop-1-extraDist].isFree = false;
            }
        }
        public void CreateLongColumnShip(int length,  string ShipSymbol)
        {
            int playerCheck ;
            int extraDist;
            if (Console.CursorTop > field[0].Size.y)
            {
                playerCheck = 1;
                extraDist= field[0].Size.y+2;
            }
            else
            {
                playerCheck = 0;
                extraDist = 0;
            }
            int x = Console.CursorLeft;
            for (int i = 0; i < length; i++)
            {
                PaintCell(x, Console.CursorTop+1, ConsoleColor.White, ShipSymbol);
                field[playerCheck].CellCoord[Console.CursorLeft-2, Console.CursorTop-1-extraDist].isFree = false;
            }
        }
        public void PaintCell(int x, int y, ConsoleColor color,string symbol)
        {
            int playerCheck ;
            int extraDist;
            if (y > field[0].Size.y)
            {
                playerCheck = 1;
                extraDist= field[0].Size.y+2;
            }
            else
            {
                playerCheck = 0;
                extraDist = 0;
            }
            
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Cell cell = new Cell(true);
            field[playerCheck].CellCoord[x-1, y-extraDist-1] = cell;
             Console.Write(symbol);
            Console.ResetColor();
        }
    }
}