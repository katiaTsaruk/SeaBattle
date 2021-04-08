using System;

namespace SeaBattle
{
    public class Lobby
    {
        private Draw draw = new Draw();
        public int gameAmount=1;
        public int gameMode;
        public int player0WinCounter = 0;
        public int player1WinCounter = 0;
        public string[] playerNames=new string[2];
        
        
        public string GetName()
        {
            string name;
            Console.Write("Enter your name: ");
            name=Console.ReadLine();
            draw.ClearLine(1);
            Console.SetCursorPosition(0, Console.CursorTop-1);
            return name;
        }

        public void SetName()
        {
            if (gameMode ==1)
            {
                playerNames[0] = "Bot 1";
                playerNames[1] = "Bot 2";
            }
            else if (gameMode == 2)
            {
                playerNames[0] = "Bot 1";
                playerNames[1] = GetName();
            }
            else
            {
                playerNames[0] = GetName();
                playerNames[1] = GetName();
            }
        }
        
        public void WriteMatchStatus(int currentGameNumber)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(15, 0);
            Console.Write($"Game {currentGameNumber}/{gameAmount}");
            Console.SetCursorPosition(15, 1);
            Console.Write($"Amount of {playerNames[0]} wins: {player0WinCounter}");
            Console.SetCursorPosition(15, 2);
            Console.Write($"Amount of {playerNames[1]} wins: {player1WinCounter}");
            Console.ResetColor();
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
            SetName();
            Console.Write("Press ENTER to continue");
            Console.ReadLine();
            Console.Clear();

            GameInitData gameInitData;
            gameInitData.PlayerNames = playerNames;
            gameInitData.GameMode = gameMode;
            
            for (int i = 1; i <= gameAmount; i++)
            {
                WriteMatchStatus(i);
                Game game = new Game(gameInitData);
                game.Start();
            }
        }

        public void GetGameAmount()
        {
            if (!int.TryParse(Console.ReadLine(), out gameAmount))
            {
                draw.ClearLine(1);
                Console.SetCursorPosition(1, Console.CursorTop - 1);
                Console.Write("Are you an Idiot? Please write a number: ");
            }
        }

        public void GetGameMode()
        {
            bool isCorrect = false;
            while (!isCorrect)
            {
                while (!int.TryParse(Console.ReadLine(), out gameMode))
                {
                    draw.ClearLine(1);
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
                    draw.ClearLine(1);
                    Console.SetCursorPosition(1, Console.CursorTop - 1);
                    Console.Write("Are you an Idiot? The number between 1 and 3: ");
                }
            }
        }
    }
}