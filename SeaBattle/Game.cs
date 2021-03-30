using System;

namespace SeaBattle
{
    public class Game
    {
        private Cell size;
        private Cell[,] cellCoord;
        Cell cell;
        public void Start()
        {
            size = new Cell(11, 11);// the actual size of the field is for 1 cell smaller than this number
            cellCoord = new Cell[size.x, size.y*2+1];
            DrawTwoFields();
            SetFlotilia(false);
            SetFlotilia(true);
            Console.SetCursorPosition(1,size.y*2+2);
            //WriteRules();
        }

        public void DrawTwoFields()
        {
            DrawField(0);
            Console.WriteLine("");
            Console.WriteLine("Computer");
            DrawField(size.y+1);
            GetWriteName();
        }

        public void DrawField(int extraDistance)
        {
            Console.Write(" ");
            for (int i = 0; i < size.x - 1; i++)
            {
                Console.Write(i);
            }

            Console.WriteLine(" ");
            for (int i = 0; i < size.y - 1; i++)
            {
                Console.WriteLine(i);
            }
            
            for (int i = 1; i < size.x; i++)
            {
                for (int j = 1+extraDistance; j < size.y+extraDistance; j++)
                {
                    PaintCell(i,j,ConsoleColor.White," ",ConsoleColor.White);
                }
            }
        }

        public void CreateLongRowShip(int length, ConsoleColor ShipColor, string ShipSymbol)
        {
            for (int i = 0; i < length; i++)
            {
                PaintCell(Console.CursorLeft, Console.CursorTop, ConsoleColor.White, ShipSymbol, ShipColor);
                cellCoord[Console.CursorLeft-1, Console.CursorTop].isFree = false;
            }
        }
        public void CreateLongColumnShip(int length, ConsoleColor ShipColor, string ShipSymbol)
        {
            int x = Console.CursorLeft;
            for (int i = 0; i < length; i++)
            {
                PaintCell(x, Console.CursorTop+1, ConsoleColor.White, ShipSymbol, ShipColor);
                //x--;
                cellCoord[Console.CursorLeft-1, Console.CursorTop].isFree = false;
            }
        }

        public void SpawnShips(int length, int amount, bool isSecondField)
        {
            Random rand = new Random();
            int dopHeight;
            ConsoleColor shipColor;
            string shipSymbol;
            if (isSecondField)
            {
                dopHeight = size.y + 1;
                shipColor = ConsoleColor.Black;
                shipSymbol = "#";
            }
            else
            {
                dopHeight = 0;
                shipColor = ConsoleColor.White;
                shipSymbol = " ";
            }

            for (int i = 0; i < amount; i++)
            {
                Console.SetCursorPosition(rand.Next(2, size.x - length), rand.Next(2, size.y - length) + dopHeight);
                if (rand.Next(1, 3) == 1)
                {
                    if (IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, true))
                    {
                        CreateLongRowShip(length, shipColor, shipSymbol);
                    }
                    else if (IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, false))
                    {
                        CreateLongColumnShip(length, shipColor, shipSymbol);
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    if (IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, false))
                    {
                        CreateLongColumnShip(length, shipColor, shipSymbol);
                    }
                    else if (IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, true))
                    {
                        CreateLongRowShip(length, shipColor, shipSymbol);
                    }
                    else
                    {
                        i--;
                    }
                }
            }
        }

        public void SetFlotilia(bool isSecondField)
        {
            
            //4 cell ship
            SpawnShips(4,1,isSecondField);
            //3 cell ship
            SpawnShips(3,2,isSecondField);
            // 2 cell ship
            SpawnShips(2,3,isSecondField);
            // 1 cell ship
            SpawnShips(1,4,isSecondField);
        } 

        public bool IsPlaceFree(int x, int y, int length, bool isRow)
        {
            int counter = 0;
            
            while (counter < length && IsCellFree(x,y))
            {
                if (isRow)
                {
                    x++;
                }
                else
                {
                    y++;
                }
                counter++;
            }
            return counter == length;
        }
        public bool IsCellFree(int x, int y)
        {
            bool isCellFree=cellCoord[x, y].isFree;;
            if (x < size.x && isCellFree)
            {
                isCellFree = cellCoord[x+1, y].isFree;
            }
            if (x > 2 && isCellFree)
            {
                isCellFree = cellCoord[x-1, y].isFree;
            }
            if (y != 2 && y != size.y + 3 && isCellFree) 
            {
                isCellFree = cellCoord[x, y-1].isFree;
            }
            if (y != size.y && y != size.y * 2 + 1 && isCellFree) 
            {
                isCellFree = cellCoord[x, y+1].isFree;
            }
            return isCellFree;
        }
        public void WriteRules() // need to be finished
        {
            Console.WriteLine("");
        }

        public void ClearLine(int minusY)
        {
            Console.SetCursorPosition(0, Console.CursorTop-minusY);
            Console.Write(new String(' ', Console.BufferWidth));
        }

        public void GetWriteName()
        {
            Console.WriteLine("");
            Console.Write("Enter yor name: ");
            string name = Console.ReadLine();
            ClearLine(1);
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.Write(name);
        }
        public void PaintCell(int x, int y, ConsoleColor color,string symbol,ConsoleColor symbolColor)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Console.ForegroundColor = symbolColor;
            cell = symbol == " " ? new Cell(true) : new Cell(false);
            cellCoord[x, y] = cell;
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}