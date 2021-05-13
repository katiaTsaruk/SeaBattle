using System;
using System.Security.Policy;

namespace SeaBattle
{
    public class Lobby
    {
        private Draw draw = new Draw();
        private int gameAmount = 1;
        private int gameMode;
        private PlayerProfile player0;
        private PlayerProfile player1;
        public AllProfiles allProfiles = new AllProfiles();
        
        private PlayerProfile GetProfile()
        {
            string name;
            string password;
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
            Console.Write("Enter your password: ");
            password = Console.ReadLine();
            PlayerProfile profile =allProfiles.SetProfile(name, password);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            return profile;
        }

        private void SetProfile()
        {
            if (gameMode == 1)
            {
                player0=allProfiles.SetProfile("Bot 1", "password");
                player1=allProfiles.SetProfile("Bot 2", "password");
            }
            else if (gameMode == 2)
            {
                player0 = allProfiles.SetProfile("Bot 1", "password");
                player1 = GetProfile();
            }
            else
            {
                player0 = GetProfile();
                player1 = GetProfile();
            }
        }

        private void WriteMatchStatus(int currentGameNumber)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(15, 0);
            Console.Write($"Game {currentGameNumber}/{gameAmount}");
            Console.SetCursorPosition(15, 1);
            Console.Write($"Amount of {player0.name} wins: {player0.wins}");
            Console.SetCursorPosition(15, 2);
            Console.Write($"Amount of {player1.name} wins: {player1.wins}");
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
            allProfiles.Load();
            SetProfile();
            allProfiles.Save();
            Console.Write("Press ENTER to continue");
            Console.ReadLine();
            Console.Clear();

            GameInitData gameInitData;
            gameInitData.player0 = player0;
            gameInitData.player1 = player1;
            gameInitData.GameMode = gameMode;

            for (int i = 1; i <= gameAmount; i++)
            {
                WriteMatchStatus(i);
                Game game = new Game(gameInitData);
                game.Start();
                GameResult(game.player0HitCounter, game.shipCellNum);
                PrepareScreen();
                allProfiles.Save();
            }
        }

        private void PrepareScreen()
        {
            Console.Write("Press ENTER to continue");
            Console.ReadLine();
            Console.Clear();
        }

        private void GameResult(int player0HitCounter, int shipCellNum)
        {
            if (player0HitCounter == shipCellNum / 2)
            {
                player0.wins++;
                Console.SetCursorPosition(1, draw.field[0].Size.y * 2 + 4);
                Console.WriteLine($"{player0.name} wins!");
            }
            else
            {
                player1.wins++;
                Console.SetCursorPosition(1, draw.field[0].Size.y * 2 + 4);
                Console.WriteLine($"{player1.name} wins!");
            }
        }

        private void GetGameAmount()
        {
            while (!int.TryParse(Console.ReadLine(), out gameAmount))
            {
                draw.ClearLine(1);
                Console.SetCursorPosition(1, Console.CursorTop - 1);
                Console.Write("Are you an Idiot? Please write a number: ");
            }
        }

        private void GetGameMode()
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