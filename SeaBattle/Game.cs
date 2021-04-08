using System;

namespace SeaBattle
{
    public class Game
    {   // можно сделать управление стрелочками для расстановки кораблей и подсвечивать возможные варианты клеточек 
        // можно сделать проверку на то ходили ли уже в эту клетку
        // ии после попадания ходит в соседние клетки и не ходит в те же
        private Field field = new Field();
        private Draw draw = new Draw();
        
        public bool isPlayer1Turn=true;
        public int player0HitCounter = 0;
        public int player1HitCounter = 0;
        public int shipCellNum=0;
        private int _gameMode;
        private string[] _playerNames;
        
        public Game(GameInitData gameInitData)
        {
            _playerNames = gameInitData.PlayerNames;
            _gameMode = gameInitData.GameMode;
        }

        public void Start()
        {
            Console.SetCursorPosition(0, 0);
            DrawTwoFields();
            SetFlotilia(false);
            SetFlotilia(true);
            Update();
            EndGame();
            PrepareNextGame();
        }

        public void PrepareNextGame()
        {
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
            Console.SetCursorPosition(0, field.Size.y*2+4);
            draw.ClearLine(0);
            Console.SetCursorPosition(0, field.Size.y*2+4);
            Console.WriteLine($"{_playerNames[playerNumber]} turn");
            Console.ResetColor();
        }

        public void WriteScore()
        {
            Console.SetCursorPosition(0, field.Size.y*2+5);
            Console.WriteLine($"{_playerNames[0]} score: {player0HitCounter}");
            Console.WriteLine($"{_playerNames[1]} score: {player1HitCounter}");
        }

        public void EndGame()//bool perepisat
        {
            if (player0HitCounter == shipCellNum/2)
            {

               // lobby.player0WinCounter ++;
                Console.SetCursorPosition(1,field.Size.y*2+2);
                Console.WriteLine($"{_playerNames[0]} wins!");
            }
            else
            {
                player1HitCounter++;
                Console.SetCursorPosition(1,field.Size.y*2+2);
                Console.WriteLine($"{_playerNames[1]} wins!");
            }
        }

        public void Shoot()
        {
            Console.SetCursorPosition(1,field.Size.y*2+2);
            Random rand = new Random();
            int x, y;
            if (isPlayer1Turn)
            {
                if (_gameMode == 2 || _gameMode == 3)
                {
                    x = GetShootCoord(true) + 1;
                    y = GetShootCoord(false) + 1;
                    draw.ClearLine(1);
                    draw.ClearLine(2);
                }
                else
                {
                    x = rand.Next(1, field.Size.x);
                    y = rand.Next(1, field.Size.y);
                }
            }
            else
            { 
                if (_gameMode == 3)
                {
                    x = GetShootCoord(true) + 1;
                    y = GetShootCoord(false) + field.Size.y+2;
                    draw.ClearLine(1);
                    draw.ClearLine(2);
                }
                else
                {
                    x = rand.Next(1, field.Size.x);
                    y = rand.Next(field.Size.y + 2, field.Size.y * 2 + 1);
                }
            }

            if (field.CellCoord[x, y].isFree)
            {
                draw.DrawShootResult(x,y,false);
                isPlayer1Turn = !isPlayer1Turn;
            }
            else
            {
                draw.DrawShootResult(x,y,true);
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
                maxCoord = field.Size.x-2;
            }
            else
            {
                axis = "y";
                maxCoord=field.Size.y-2;
            }
            int shootCoord=10;
            bool isCorrect = false;
            Console.Write($"Write {axis} coordinate of the cell(0-{maxCoord}), you want to shoot: ");
            while (!isCorrect)
            {
                while (!int.TryParse(Console.ReadLine(), out shootCoord))
                {
                    draw.ClearLine(1);
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
                    draw.ClearLine(1);
                    Console.SetCursorPosition(1, Console.CursorTop - 1);
                    Console.Write($"Are you an Idiot? The number between 0 and {maxCoord}: ");
                }
            }
            return shootCoord;
        }

        public void DrawTwoFields()
        {
            draw.DrawField(0);
            Console.WriteLine("");
            Console.WriteLine(_playerNames[0]);
            draw.DrawField(field.Size.y+1);
            Console.WriteLine("");
            Console.WriteLine(_playerNames[1]);
        }


        public void SpawnShips(int length, int amount, bool isSecondField)
        {
            shipCellNum += length * amount;
            Random rand = new Random();
            int dopHeight;
            string shipSymbol;
            if (isSecondField)
            {
                dopHeight = field.Size.y + 1;
                shipSymbol = "#";
            }
            else
            {
                dopHeight = 0;
                shipSymbol = "@";
            }

            for (int i = 0; i < amount; i++)
            {
                Console.SetCursorPosition(rand.Next(2, field.Size.x - length), rand.Next(2, field.Size.y - length) + dopHeight);
                if (rand.Next(1, 3) == 1)
                {
                    if (field.IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, true))
                    {
                        draw.CreateLongRowShip(length,  shipSymbol);
                    }
                    else if (field.IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, false))
                    {
                        draw.CreateLongColumnShip(length, shipSymbol);
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    if (field.IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, false))
                    {
                        draw.CreateLongColumnShip(length, shipSymbol);
                    }
                    else if (field.IsPlaceFree(Console.CursorLeft, Console.CursorTop, length, true))
                    {
                        draw.CreateLongRowShip(length, shipSymbol);
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

    }
}