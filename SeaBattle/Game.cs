using System;

namespace SeaBattle
{
    public class Game
    { // можно сделать управление стрелочками для расстановки кораблей и подсвечивать возможные варианты клеточек 
        private (int x, int y) size= (11, 11);// the actual size of the field is for 1 cell smaller than this number)))
        private Cell[,] cellCoord;
        public bool isPlayer1Turn=true;
        public int player2HitCounter = 0;
        public int player1HitCounter = 0;
        public int shipCellNum=0;
        
        public void Start()
        {
            cellCoord = new Cell[size.x, size.y*2+1];
            DrawTwoFields();
            SetFlotilia(false);
            SetFlotilia(true);
            while (player2HitCounter != shipCellNum/2 || player1HitCounter != shipCellNum/2)
            {
                Shoot();
            }
            EndGame();
            //WriteRules();
        }

        public void EndGame()
        {
            if (player2HitCounter == shipCellNum/2)
            {
                Console.SetCursorPosition(1,size.y*2+2);
                Console.WriteLine("Player 2 wins!");
            }
            else
            {
                Console.SetCursorPosition(1,size.y*2+2);
                Console.WriteLine("Player 1 wins!");
            }
        }

        public void Shoot()
        {
            Console.SetCursorPosition(1,size.y*2+2);
            Random rand = new Random();
            int x, y;
            if (isPlayer1Turn)
            {
                x = GetShootCoord(true)+1;
                y = GetShootCoord(false)+1;
                ClearLine(1);
                ClearLine(2);
            }
            else
            { 
                x = rand.Next(2, 11);
                y = rand.Next(size.y + 3, size.y * 2 + 1);
            }

            if (cellCoord[x, y].isFree)
            {
                PaintCell(x, y, ConsoleColor.Green, " ");
                Console.SetCursorPosition(1,size.y*2+2);
                Console.WriteLine("Miss");
                Console.Write("Press ENTER to continue");
                Console.ReadLine();
                ClearLine(1);
                ClearLine(2);
                isPlayer1Turn = !isPlayer1Turn;
            }
            else
            {
                PaintCell(x, y, ConsoleColor.Red, "#");
                Console.SetCursorPosition(1, size.y * 2 + 2);
                Console.WriteLine("Hit");
                Console.Write("Press ENTER to continue");
                Console.ReadLine();
                ClearLine(1);
                ClearLine(2);
                if (!isPlayer1Turn)
                {
                    player2HitCounter++;
                }
                else
                {
                    player1HitCounter++;
                }
            }
        }
        public int GetShootCoord(bool isXAxis)
        {
            string axis;
            int maxCoord;
            if (isXAxis)
            {
                axis = "x";
                maxCoord = size.x-2;
            }
            else
            {
                axis = "y";
                maxCoord=size.y-2;
            }
            int shootCoord=10;
            bool isCorrect = false;
            Console.Write($"Write {axis} coordinate of the cell(0-{maxCoord}), you want to shoot: ");
            while (!isCorrect)
            {
                while (!int.TryParse(Console.ReadLine(), out shootCoord))
                {
                    ClearLine(1);
                    Console.SetCursorPosition(1, Console.CursorTop - 1);
                    Console.Write("Are you an Idiot? Please write a number: ");
                }

                if (shootCoord >= 0 && shootCoord <= maxCoord)
                {
                    isCorrect = true;
                }
                else
                {
                    isCorrect = false;
                    ClearLine(1);
                    Console.SetCursorPosition(1, Console.CursorTop - 1);
                    Console.Write($"Are you an Idiot? The number between 0 and {maxCoord}: ");
                }
            }
            return shootCoord;
        }

        public void DrawTwoFields()
        {
            DrawField(0);
            Console.WriteLine("");
            Console.WriteLine("Player 2");
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
                    PaintCell(i,j,ConsoleColor.White," ");
                }
            }
        }

        public void CreateLongRowShip(int length, string ShipSymbol)
        {
            for (int i = 0; i < length; i++)
            {
                PaintCell(Console.CursorLeft, Console.CursorTop, ConsoleColor.White, ShipSymbol);
                cellCoord[Console.CursorLeft-2, Console.CursorTop-1].isFree = false;
            }
        }
        public void CreateLongColumnShip(int length,  string ShipSymbol)
        {
            int x = Console.CursorLeft;
            for (int i = 0; i < length; i++)
            {
                PaintCell(x, Console.CursorTop+1, ConsoleColor.White, ShipSymbol);
                cellCoord[Console.CursorLeft-2, Console.CursorTop-1].isFree = false;
            }
        }

        public void SpawnShips(int length, int amount, bool isSecondField)
        {
            shipCellNum += length * amount;
            Random rand = new Random();
            int dopHeight;
            string shipSymbol;
            if (isSecondField)
            {
                dopHeight = size.y + 1;
                shipSymbol = "#";
            }
            else
            {
                dopHeight = 0;
                shipSymbol = " ";
            }

            for (int i = 0; i < amount; i++)
            {
                Console.SetCursorPosition(rand.Next(2, size.x - length), rand.Next(2, size.y - length) + dopHeight);
                if (rand.Next(1, 3) == 1)
                {
                    if (IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, true))
                    {
                        CreateLongRowShip(length,  shipSymbol);
                    }
                    else if (IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, false))
                    {
                        CreateLongColumnShip(length, shipSymbol);
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
                        CreateLongColumnShip(length, shipSymbol);
                    }
                    else if (IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, true))
                    {
                        CreateLongRowShip(length, shipSymbol);
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
            bool isCellFree=cellCoord[x-1, y-1].isFree;;
            if (x < size.x && isCellFree)
            {
                isCellFree = cellCoord[x, y-1].isFree;
            }
            if (x > 2 && isCellFree)
            {
                isCellFree = cellCoord[x-2, y-1].isFree;
            }
            if (y != 2 && y != size.y + 3 && isCellFree) 
            {
                isCellFree = cellCoord[x-1, y-2].isFree;
            }
            if (y != size.y && y != size.y * 2 + 1 && isCellFree) 
            {
                isCellFree = cellCoord[x-1, y].isFree;
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
            Console.Write("Player 1, enter yor name: ");
            string name = Console.ReadLine();
            ClearLine(1);
            Console.SetCursorPosition(0, Console.CursorTop-1);
            Console.Write(name);
        }
        public void PaintCell(int x, int y, ConsoleColor color,string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Cell cell = symbol == " " ? new Cell(true) : new Cell(false);
            cellCoord[x, y] = cell;
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}