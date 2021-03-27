using System;

namespace SeaBattle
{
    public class GameLogic
    {
        private Cell size;
        private Cell[,] cellCoord;
        Cell cell;
        public void Start()
        {
            size = new Cell(11, 11);
            cellCoord = new Cell[size.width, size.height*2+1];
            DrawTwoFields();
            SetFlotilia(false);
            SetFlotilia(true);
            Console.SetCursorPosition(1,size.height*2+2);
            //WriteRules();
        }

        public void DrawTwoFields()
        {
            DrawField(0);
            Console.WriteLine("");
            Console.WriteLine("Computer");
            DrawField(size.height+1);
            GetWriteName();
        }

        public void DrawField(int extraDistance)
        {
            Console.Write(" ");
            for (int i = 0; i < size.width - 1; i++)
            {
                Console.Write(i);
            }

            Console.WriteLine(" ");
            for (int i = 0; i < size.height - 1; i++)
            {
                Console.WriteLine(i);
            }
            
            for (int i = 1; i < size.width; i++)
            {
                for (int j = 1+extraDistance; j < size.height+extraDistance; j++)
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
                cellCoord[Console.CursorLeft, Console.CursorTop].isFree = false;
            }
        }
        public void CreateLongColumnShip(int length, ConsoleColor ShipColor, string ShipSymbol)
        {
            for (int i = 0; i < length; i++)
            {
                PaintCell(Console.CursorLeft, Console.CursorTop+1, ConsoleColor.White, ShipSymbol, ShipColor);
                cellCoord[Console.CursorLeft, Console.CursorTop].isFree = false;
            }
        }
        public void SpawnShip(int length, int amount, bool isSecondField)
        {
            Random rand = new Random();
            int dopHeight;
            ConsoleColor shipColor;
            string shipSymbol;
            bool isRow = rand.Next(1, 3)==1;
            if (isSecondField)
            {
                dopHeight = size.height + 1;
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
                Console.SetCursorPosition(rand.Next(2, size.width-length), rand.Next(2, size.height-length)+dopHeight);
                if (IsPlaceFree(Console.CursorLeft, Console.CursorTop,length,isRow))
                {
                    CreateLongRowShip(length,shipColor,shipSymbol);
                }
                else if (IsPlaceFree(Console.CursorLeft, Console.CursorTop, length,isRow))
                {
                    CreateLongColumnShip(length,shipColor,shipSymbol);
                }
                else i--;
            }
        }
        public void SetFlotilia(bool isSecondField)
        {
            
            //4 cell ship
            SpawnShip(4,1,isSecondField);
            //3 cell ship
            SpawnShip(3,2,isSecondField);
            // 2 cell ship
            SpawnShip(2,3,isSecondField);
            // 1 cell ship
            SpawnShip(1,4,isSecondField);
        } 

        public bool IsPlaceFree(int x, int y, int length, bool isRow)
        {
            int counter = 0;
            
            while (counter < length && ISCellFree(x,y))
            {
                if (isRow)
                {
                    x++;
                }
                else y++;
                counter++;
            }
            return counter == length;
        }

        public bool ISCellFree(int x, int y)//does not work needs correct check on bounds
        {
            if (x ==size.width)
            {
                return cellCoord[x, y].isFree  && cellCoord[x - 1, y].isFree && cellCoord[x, y + 1].isFree && cellCoord[x, y - 1].isFree;
            }
            else if (y==(size.height*2+1) || y==size.height)
            {
                return cellCoord[x, y].isFree && cellCoord[x+1,y].isFree && cellCoord[x - 1, y].isFree && cellCoord[x, y - 1].isFree;
            }
            else if (x == 2)
            {
                return cellCoord[x, y].isFree && cellCoord[x+1,y].isFree && cellCoord[x, y + 1].isFree && cellCoord[x, y - 1].isFree;
            }
            else if (y == 2)
            {
                return cellCoord[x, y].isFree && cellCoord[x+1,y].isFree && cellCoord[x - 1, y].isFree && cellCoord[x, y + 1].isFree;
            }
            else return cellCoord[x, y].isFree && cellCoord[x+1,y].isFree && cellCoord[x - 1, y].isFree && cellCoord[x, y + 1].isFree && cellCoord[x, y - 1].isFree;
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