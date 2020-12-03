using System;

namespace Angeles_Scott_GuessingGame
{
    class GameManager
    {
        /*
        Project Name: Interactivity: Guessing Game
        Contribution: Scott Angeles
        Feature: Game Manager - Guessing Game
        Start & End dates: 11/25/2020 - 11/30/2020
        References: N/A
        Links: N/A
        */

        /*
        Rules: Create a program that explains the rules of the game.

        Player 1 Picks a number between 0 and 100
        Player 2 gets 5 guesses to find Player 1’s number.
        After Player 2 guesses correctly or loses the game, the game will provide the option to start over.
        At any time, the users can opt to exit out of the game by typing “quit” or “exit”

        Player 1: User will input a number between 0 and 100.
        The program should validate that:
        Player 1 did input a number and not anything else
        If the number is less than 0, make it 0.
        If the number is greater than 100, make it 100.
        If Player 1 did not input a number, ask the player to please stick to only numbers.
        The program will then ask Player 2 for their guess.

        Player 2: User will input a number between 0 and 100
        The program should validate this input the exact same way using the same criteria

        Assessment:
        If the guess is correct, the game will tell the player how many guesses it took to get it right.
        If this is the last guess, the game will tell the player that they have run out of guesses and have lost the game.
        If the guess is too high or too low, the game will relay that information to the player and also provide how many guesses are left.
        */

        static void Main(string[] args)
        {

            bool isGameOver = false;

            //Game Loop
            do
            {
                //Title
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                Console.WriteLine("%%    A Number Guessing Game by Scott Angeles    %%");
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%\n\n");


                DisplayRules();

                ResetConsole();
                
                isGameOver = PlayGame();

            } while (!isGameOver);
   
            //Close Console Window
            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }
        static bool PlayGame()
        {
            //Initialize variables
            bool isValidNumber = false;
            bool isValidCommand = false;

            int magicNumber = 0;
            int guessNumber = 0;
            int guessCounter = 1;
            int maxGuesses = 5;
            int minRange = 0;
            int maxRange = 100;

            string playerInput = string.Empty;

            //Display valid commands at top of console
            DisplayCommands();

            //Start Player 1's Turn
            Console.WriteLine("Ready PLAYER 1? It's time to pick a magic number!\n");

            //Player 1 Turn Validation
            while (!isValidNumber)
            {
                playerInput = RequestPlayerInput();

                if (playerInput.ToLower() == "exit" || playerInput.ToLower() == "quit")
                {
                    return true;
                }
                else if (playerInput.ToLower() == "retry")
                {
                    ResetConsole();
                    return false;
                }
                else if (playerInput.ToLower() == "help" || playerInput == "?") 
                {
                    isValidCommand = true;
                    DisplayRules();

                }
                else if (playerInput.ToLower() == "random")
                {
                    Random randomNum = new Random();

                    magicNumber = randomNum.Next(0, 100);
                    ResetConsole();
                    break;
                }

                isValidNumber = int.TryParse(playerInput, out magicNumber);

                if (isValidNumber && magicNumber > maxRange)
                {
                    magicNumber = 100;
                    Console.WriteLine($"\nThat number is a bit too high, let's set your magic number to {magicNumber} and see if Player 2 can guess it.");
                    ResetConsole();
                }
                else if (isValidNumber && magicNumber < minRange)
                {
                    magicNumber = 0;
                    Console.WriteLine($"\nThat number is a bit too low, let's set your magic number to {magicNumber} and see if Player 2 can guess it.");
                    ResetConsole();
                }
                else if (isValidNumber)
                {
                    Console.WriteLine($"\nPerfect! Your magic number is {magicNumber}, let's see if Player 2 can guess it.");
                    ResetConsole();
                }
                else
                {
                    if (!isValidCommand)
                    {
                        Console.WriteLine("\nCome on Player 1, that's not even a whole number! Please try again.\n");
                    }

                    isValidCommand = false;
                }

            }
            //Start Player 2's Turn
            isValidNumber = false;

            //Display valid commands at top of console
            DisplayCommands();

            Console.WriteLine("Ready PLAYER 2! It's time to guess the magic number!\n");

            //Player 2 Turn Validation
            while (!isValidNumber || guessCounter <= maxGuesses)
            {
                playerInput = RequestPlayerInput();

                if (playerInput.ToLower() == "exit" || playerInput.ToLower() == "quit")
                {
                    return true;
                }
                else if (playerInput.ToLower() == "retry")
                {
                    ResetConsole();
                    return false;

                }
                else if (playerInput.ToLower() == "help" || playerInput == "?")
                {
                    isValidCommand = true;
                    DisplayRules();
                }

                isValidNumber = int.TryParse(playerInput, out guessNumber) && ValidateNumberInRange(guessNumber, minRange, maxRange);

                if (isValidNumber && guessNumber > magicNumber && guessCounter < maxGuesses)
                {

                    Console.WriteLine($"\n\tThat number is a bit too high, guess lower than {guessNumber}. Guesses Remaining: {maxGuesses - guessCounter}\n");
                    guessCounter++;

                }
                else if (isValidNumber && guessNumber < magicNumber && guessCounter < maxGuesses)
                {

                    Console.WriteLine($"\n\tThat number is a bit too low, guess higher than {guessNumber}. Guesses Remaining: {maxGuesses - guessCounter}\n");
                    guessCounter++;

                }
                else if (isValidNumber && guessNumber == magicNumber && guessCounter <= maxGuesses)
                {

                    Console.WriteLine($"\n\tYou got it! The magic number was {magicNumber}. Guesses Required: {guessCounter}\n\n\t***Player 2 Wins!***\n\nType \"retry\" to play again or exit out of the game by typing \"quit\" or \"exit\"\n");

                    playerInput = Console.ReadLine();

                    if (playerInput.ToLower() == "exit" || playerInput.ToLower() == "quit")
                    {
                        return true;
                    }
                    else if (playerInput.ToLower() == "retry"|| playerInput.ToLower() == "help" || playerInput == "?")
                    {
                        ResetConsole();
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("That isn't one of your choices, but I'm going to assume you want to play again!\n");
                        ResetConsole();
                        break;
                    }

                }
                else if (isValidNumber && guessCounter == maxGuesses)
                {
                    Console.WriteLine($"\n\tSorry! That was your last guess. The magic number was {magicNumber}.\n\n\t***Player 1 Wins!***\n\nType \"retry\" to play again or exit out of the game by typing \"quit\" or \"exit\"\n");

                    playerInput = Console.ReadLine();

                    if (playerInput.ToLower() == "exit" || playerInput.ToLower() == "quit")
                    {
                        return true;
                    }
                    else if (playerInput.ToLower() == "retry"|| playerInput.ToLower() == "help" || playerInput == "?")
                    {
                        ResetConsole();
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("That isn't one of your choices, but I'm going to assume you want to play again!\n");
                        ResetConsole();
                        return false;
                    }
                }
                else
                {
                    if (!isValidCommand)
                    {
                        Console.WriteLine("\nCome on Player 2, that's not a valid attempt! I won't count that as an official guess. Please try again.\n");
                    }

                    isValidCommand = false;
                }
            }

            return false;
        }
        static void DisplayRules()
        {
            //Game Rules
            Console.WriteLine("\tHow to Play...\n");
            Console.WriteLine("\t1. The game begins with Player 1, who will enter a whole number between 0 and 100. This is the magic number!");
            Console.WriteLine("\t2. Next, Player 2 will attempt to guess the magic number by entering a whole number between 0 and 100.");
            Console.WriteLine("\t3. After each guess, Player 2 will receive a clue indicating whether their guess was too high or too low.");
            Console.WriteLine("\t4. Make the guesses count! Player 2 only gets a maximum of 5 attempts to guess the magic number.");
            Console.WriteLine("\t5. If Player 2 can guess the magic number in 5 guesses or less, they are the winner! If not, Player 1 wins!\n");

            //Exit Instructions
            Console.WriteLine("\t*Either player can restart the game by typing \"retry\" at any time or exit out of the game by typing \"quit\" or \"exit\"\n");
        }
        static void DisplayCommands()
        {
            Console.WriteLine("--[help]--[retry]--[quit]--[exit]--\n\n");
        }
        static string RequestPlayerInput()
        {   
            Console.Write("Enter a whole number between 0 and 100: ");

            return Console.ReadLine();
        }
        static bool ValidateNumberInRange(int num, int min, int max)
        {
            return num >= min && num <= max;
        }
        static void ResetConsole()
        {
            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();

            Console.Clear();

        }
    }
}
