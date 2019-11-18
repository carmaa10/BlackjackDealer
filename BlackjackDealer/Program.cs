using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// NOTES TO ME TO DELETE WHEN TURN IN
// If any of my search terms are still in the program take care of them then delete this
// * RID = get rid of later, for easy find later
// * MAKE BETTER = flesh out/make pretty, for easy find later
// * IMPLEMENT = need to add a thing, for easy find later
// * HERE = where I left off, for easy find later

namespace BlackjackDealer
{
    class Program
    {
        // ************************************
        // Title: Blackjack Dealter
        // Application Type: Console .NET Core
        // Description: MAKE BETTER
        // Author: Carma Aten
        // Date Created: 11/17/19
        // Last Modified: 11/18/19
        // ************************************

        static void Main(string[] args)
        {

            DisplayWelcomeScreen("Blackjack Dealer");
            DisplayMainMenu();
            DisplayClosingScreen("Thank you for playing my unoriginal avaerage game!");

        }

        #region SCREENS

        static void DisplayWelcomeScreen(string message)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + message);
            Console.WriteLine();

            DisplayContinuePrompt("start");
        }

        static void DisplayMainMenu()
        {
            // -------------
            // VARIABLES
            //
            ConsoleColor foregroundColor;
            ConsoleColor backgroundColor;

            string cardDeckInfo = @"Data/cardDeckInfo.txt";
            string instructions = @"Data/instructions.txt";

            // string userResponse;
            int userResponse;
            bool runApplication = true;



            while (runApplication)
            {

                DisplayScreenHeader("Main Menu");

                userResponse = ConsoleHelper.MultipleChoice(true, "Play Game", "Display Instructions", "Rules", "Players");
                userResponse += 1; // Imporoving program readability so that the first options is option #1
                switch (userResponse)
                {
                    case 1:
                        DisplayErrorMessage("Under Development RID");
                        break;
                    case 2:
                        DisplayLongGameInstructions(instructions);
                        break;
                    case 3:
                        DisplayErrorMessage("Under Development RID");
                        break;
                    case 4:
                        DisplayErrorMessage("Under Development RID");
                        break;
                    default:
                        break;
                }
            }
        }

        #region MAIN MENU SCREENS

        /// <summary>
        /// Takes all the text from the instructions.txt file and prints it out on the screen line by line
        /// </summary>
        /// <param name="instructions">The file path for the instructions.txt file</param>
        // IDEAS
        // * Have menu for everything other than object of game -- use an array in an array??
        static void DisplayLongGameInstructions(string instructions)
        {
            Console.Clear();

            string[] instructionsText = File.ReadAllLines(instructions);
            foreach (string line in instructionsText)
            {
                Console.WriteLine("\t" + line);
            }

            DisplayContinuePrompt("return to the main menu");
        }

        #endregion // MAIN MENU SCREENS

        static void DisplayClosingScreen(string message)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + message);
            Console.WriteLine();

            DisplayContinuePrompt("exit");
        }

        #endregion // SCREENS

        // MAKE BETTER need better name for region
        #region CARD STUFF

        static (string suit, string rank, int value)[] BuildCardDeck(string cardDeckInfo)
        {
            (string suit, string rank, int value) cardInfo;

            // HERE is where I left off

            return cardInfo;
        }

        #endregion

        #region HELPER METHODS

        static void DisplayContinuePrompt(string action)
        {
            Console.WriteLine();
            Console.WriteLine($"Press enter to {action}.");
            Console.ReadLine();
        }

        static void DisplayScreenHeader(string headerText)
        {
            int dashRepeat = 74; // a constant throughout the program, when I have a line for a main heading, it's 74 characters long

            Console.Clear();
            Console.WriteLine();
            RepeatCharacter(dashRepeat, "-");
            Console.WriteLine("\n\t\t" + headerText);
            RepeatCharacter(dashRepeat, "-");
            Console.WriteLine();
        }

        static string DisplayGetUserResponse(string question)
        {
            string userResponse;

            Console.WriteLine(question);

            userResponse = Console.ReadLine().Trim();

            return userResponse;
        }

        static bool CheckParseWorked(bool parse)
        {
            if (parse)
            {
                parse = true;
            }
            else
            {
                DisplayErrorMessage("Please input a number (10, 2, etc.)");
                parse = false;
            }
            return parse;
        }

        static void DisplayErrorMessage(string error)
        {
            Console.WriteLine($"\n** {error} **");

            DisplayContinuePrompt("retry");
        }

        static void RepeatCharacter(int numberTimes, string character)
        {
            for (int repeat = 0; repeat < numberTimes; repeat++)
            {
                Console.Write(character);
            }
        }

        #endregion // HELPER METHODS
    }
}
