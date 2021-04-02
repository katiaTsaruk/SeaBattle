using System;
using System.Collections.Generic;

namespace SeaBattle
{
    public class Game
    {   // можно сделать управление стрелочками для расстановки кораблей и подсвечивать возможные варианты клеточек 
        // можно сделать проверку на то ходили ли уже в эту клетку
        // ии после попадания ходит в соседние клетки и не ходит в те же
        
        private (int x, int y) size= (11, 11);// the actual size of the field is for 1 cell smaller than this number)))
        private Cell[,] cellCoord;
        public bool isPlayer1Turn=true;
        public int player0HitCounter = 0;
        public int player1HitCounter = 0;
        public int player0WinCounter = 0;
        public int player1WinCounter = 0;
        public int shipCellNum=0;
        public int gameMode;
        public string[] playerNames=new string[2];
        public int gameAmount=1;
        public int currentGameNumber = 1;

        public void Start()
        {
            cellCoord = new Cell[size.x, size.y*2+1];
            if (currentGameNumber == 1)
            {
                StartLobby();
            }
            Console.SetCursorPosition(0, 0);
            DrawTwoFields();
            WriteMatchStatus();
            SetFlotilia(false);
            SetFlotilia(true);
            Update();
            EndGame();
            PrepareNextGame();
        }

        public void WriteMatchStatus()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(size.x + 5, 0);
            Console.Write($"Game {currentGameNumber}/{gameAmount}");
            Console.SetCursorPosition(size.x + 5, 1);
            Console.Write($"Amount of {playerNames[0]} wins: {player0WinCounter}");
            Console.SetCursorPosition(size.x + 5, 2);
            Console.Write($"Amount of {playerNames[1]} wins: {player1WinCounter}");
            Console.ResetColor();
        }

        public void PrepareNextGame()
        {
            currentGameNumber++;
            Console.Write("Press ENTER to continue");
            Console.ReadLine();
            Console.Clear();
            isPlayer1Turn = true;
            player0HitCounter = 0;
            player1HitCounter = 0;
        }

        public void Update()
        {
            while (player0HitCounter != shipCellNum/2 && player1HitCounter != shipCellNum/2)
            {
                WriteScore();
                WriteWhoseTurn();
                Shoot();
            }
        }

        public void WriteWhoseTurn()
        {
            int playerNumber;
            if (isPlayer1Turn)
            {
                playerNumber = 1;
            }
            else
            {
                playerNumber = 0;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(0, size.y*2+4);
            ClearLine(0);
            Console.SetCursorPosition(0, size.y*2+4);
            Console.WriteLine($"{playerNames[playerNumber]} turn");
            Console.ResetColor();
        }

        public void WriteScore()
        {
            Console.SetCursorPosition(0, size.y*2+5);
            Console.WriteLine($"{playerNames[0]} score: {player1HitCounter}");
            Console.WriteLine($"{playerNames[1]} score: {player0HitCounter}");
        }

        public void GetGameAmount()
        {
            if (!int.TryParse(Console.ReadLine(), out gameAmount))
            {
                ClearLine(1);
                Console.SetCursorPosition(1, Console.CursorTop - 1);
                Console.Write("Are you an Idiot? Please write a number: ");
            }
        }

        public void StartLobby()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Hello! Welcome into my game");
            Console.WriteLine("You have an opportunity to choose game mode:");
            Console.WriteLine("1- bot+bot game");
            Console.WriteLine("2- bot+human game");
            Console.WriteLine("3- human+human game");
            Console.Write("Write a number(1-3) to choose: ");
            GetGameMode();
            Console.Write("Enter amount of matches, you want to play: ");
            GetGameAmount();
            Console.Write("Press ENTER to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public void GetGameMode()
        {
            bool isCorrect = false;
            while (!isCorrect)
            {
                while (!int.TryParse(Console.ReadLine(), out gameMode))
                {
                    ClearLine(1);
                    Console.SetCursorPosition(1, Console.CursorTop - 1);
                    Console.Write("Are you an Idiot? Please write a number: ");
                }

                if (gameMode >= 1 && gameMode <= 3)
                {
                    isCorrect = true;
                }
                else
                {
                    isCorrect = false;
                    ClearLine(1);
                    Console.SetCursorPosition(1, Console.CursorTop - 1);
                    Console.Write("Are you an Idiot? The number between 1 and 3: ");
                }
            }
        }

        public void EndGame()
        {
            if (player0HitCounter == shipCellNum/2)
            {

                player0WinCounter ++;
                Console.SetCursorPosition(1,size.y*2+2);
                Console.WriteLine($"{playerNames[0]} wins!");
            }
            else
            {
                player1HitCounter++;
                Console.SetCursorPosition(1,size.y*2+2);
                Console.WriteLine($"{playerNames[1]} wins!");
            }
        }

        public void Shoot()
        {
            Console.SetCursorPosition(1,size.y*2+2);
            Random rand = new Random();
            int x, y;
            if (isPlayer1Turn)
            {
                if (gameMode == 2 || gameMode == 3)
                {
                    x = GetShootCoord(true) + 1;
                    y = GetShootCoord(false) + 1;
                    ClearLine(1);
                    ClearLine(2);
                }
                else
                {
                    x = rand.Next(1, size.x);
                    y = rand.Next(1, size.y);
                }
            }
            else
            { 
                if (gameMode == 3)
                {
                    x = GetShootCoord(true) + 1;
                    y = GetShootCoord(false) + size.y+2;
                    ClearLine(1);
                    ClearLine(2);
                }
                else
                {
                    x = rand.Next(1, size.x);
                    y = rand.Next(size.y + 2, size.y * 2 + 1);
                }
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
                    player0HitCounter++;
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
            if (gameMode !=3)
            {
                playerNames[0] = "Bot 1";
                Console.WriteLine(playerNames[0]);
            }
            else
            {
                if (currentGameNumber == 1)
                {
                    Console.WriteLine(GetWriteName(0));
                }
                else
                {
                    Console.WriteLine(playerNames[0]);
                }
            }
            DrawField(size.y+1);
            Console.WriteLine("");
            if (gameMode ==1)
            {
                playerNames[1] = "Bot 2";
                Console.WriteLine(playerNames[1]);
            }
            else
            {
                if (currentGameNumber == 1)
                {
                    Console.WriteLine(GetWriteName(1));
                }
                else
                {
                    Console.WriteLine(playerNames[1]);
                }
            }
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
                cellCoord[Console.CursorLeft-1, Console.CursorTop].isFree = false;
            }
        }
        public void CreateLongColumnShip(int length,  string ShipSymbol)
        {
            int x = Console.CursorLeft;
            for (int i = 0; i < length; i++)
            {
                PaintCell(x, Console.CursorTop+1, ConsoleColor.White, ShipSymbol);
                cellCoord[Console.CursorLeft-1, Console.CursorTop].isFree = false;
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
                shipSymbol = "@";
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
        public void ClearLine(int minusY)
        {
            Console.SetCursorPosition(0, Console.CursorTop-minusY);
            Console.Write(new String(' ', Console.BufferWidth));
        }

        public string GetWriteName(int playerNumber)
        {
            Console.Write("Enter your name: ");
            playerNames[playerNumber]=Console.ReadLine();
            ClearLine(1);
            Console.SetCursorPosition(0, Console.CursorTop-1);
            return playerNames[playerNumber];
        }
        public void PaintCell(int x, int y, ConsoleColor color,string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = color;
            Cell cell = new Cell(true);
            cellCoord[x, y] = cell;
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}