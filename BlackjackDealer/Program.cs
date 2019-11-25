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
        // Last Modified: 11/20/19
        // ************************************

        static void Main(string[] args)
        {

            DisplayWelcomeScreen("Blackjack Dealer");
            DisplayMainMenu();
            DisplayClosingScreen("Thank you for playing my unoriginal average game!"); // MAKE BETTER

        }

        #region SCREENS

        static void DisplayWelcomeScreen(string message)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t Welcome to my");
            Console.WriteLine("\t\t" + message);
            Console.WriteLine();

            DisplayContinuePrompt("start");
        }

        static void DisplayClosingScreen(string message)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + message);
            Console.WriteLine();

            DisplayContinuePrompt("exit");
        }

        #endregion

        static void DisplayMainMenu()
        {
            // -------------
            // VARIABLES
            //
            ConsoleColor foregroundColor;
            ConsoleColor backgroundColor;

            string cardDeckInfo = @"Data\cardDeckInfo.txt";
            string instructions = @"Data\instructions.txt";
            string defaultRules = @"Data\defaultRules.txt";

            List<PlayingCard> cardDeck = BuildCardDeck(cardDeckInfo);
            List<PlayingCard> discard = new List<PlayingCard>();

            int userResponse;
            bool runApplication = true;

            Random random = new Random();



            while (runApplication)
            {

                DisplayScreenHeader("Main Menu");

                userResponse = ConsoleHelper.MultipleChoice(true, "Play Game", "Display Instructions", "Rules", "Players");
                //userResponse += 1; // Imporoving program readability so that the first options is option #1
                switch (userResponse + 1)
                {
                    case 1:
                        DisplayGame(random, cardDeck, discard);
                        break;
                    case 2:
                        DisplayInstructionsMenu(instructions);
                        break;
                    case 3:
                        DisplayRulesMenu(defaultRules);
                        break;
                    case 4:
                        DisplayPlayerMenu();
                        break;
                    default:
                        break;
                }
            }
        }

        #region PLAY GAME METHODS

        static void DisplayGame(Random random, List<PlayingCard> cardDeck, List<PlayingCard> discard)
        {
            PlayingCard drawnCard;

            DisplayScreenHeader("Blackjack");



            DisplayContinuePrompt("exit");
        }

        static PlayingCard DrawCard(Random random, List<PlayingCard> cardDeck, List<PlayingCard> discard)
        {
            PlayingCard drawnCard;

            int randomNumber = random.Next(cardDeck.Count());

            drawnCard = cardDeck[randomNumber];

            return drawnCard;
        }

        #endregion

        #region INSTRUCTIONS METHODS

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

        static void DisplayInstructionsMenu(string instructions)
        {
            int userResponse;
            string instructionsContent = File.ReadAllText(instructions);
            string[] submenuContent = instructionsContent.Split('|');
            //string[] submenuLineByLine;
            //List<string[]> submenuTextByLine = new List<string[]>();
            bool keepLooping = true;

            //foreach (string line in submenuContent)
            //{
            //    submenuLineByLine = line.Split('*');
            //    submenuTextByLine.Add(submenuLineByLine);
            //}

            do
            {
                DisplayScreenHeader("Instructions");

                userResponse = ConsoleHelper.MultipleChoice(true, "Object of the Game", "Betting", "The Deal", "Getting a Blackjack", "The Play", "The Dealer\'s Play");
                userResponse += 1; // Imporoving program readability so that the first options is option #1
                switch (userResponse)
                {
                    case 1:
                        DisplaySubheadingContent("Object of the Game", submenuContent, 0);
                        break;
                    case 2:
                        DisplaySubheadingContent("Betting", submenuContent, 1);
                        break;
                    case 3:
                        DisplaySubheadingContent("The Deal", submenuContent, 2);
                        break;
                    case 4:
                        DisplaySubheadingContent("Getting a Blackjack", submenuContent, 3);
                        break;
                    case 5:
                        DisplaySubheadingContent("The Play", submenuContent, 4);
                        break;
                    case 6:
                        DisplaySubheadingContent("The Dealer\'s Play", submenuContent, 5);
                        break;
                    default:
                        keepLooping = false;
                        break;
                }
            } while (keepLooping);

            Console.WriteLine("--------------------------------------------------------------------------" +
                              "\nText taken from the Bicycle Cards Website," +
                              "\nEdited https://bicyclecards.com/how-to-play/blackjack/" +
                              "\n--------------------------------------------------------------------------");
        }

        static void DisplaySubheadingContent(string submenuName, string[] submenuContent, int index)
        {
            DisplayScreenHeader(submenuName);
            Console.WriteLine(submenuContent[index].PadRight(2).PadLeft(2));
            DisplayContinuePrompt("return to instructions");
            Console.Clear();
        }

        #endregion

        #region RULES METHODS

        static void DisplayRulesMenu(string defaultRules)
        {
            DisplayScreenHeader("Rules");

            List<string> allRules = GetRules(defaultRules);

            bool keepLooping = true;

            int userResponse;

            userResponse = ConsoleHelper.MultipleChoice(true, "View Rules", "Modify Rules");
            // userResponse += 1;
            do
            {
                switch (userResponse + 1)
                {
                    case 1:
                        DisplayViewRules(allRules);
                        break;

                    case 2:
                        DisplayModifyRules();
                        break;

                    default:
                        keepLooping = false;
                        break;
                }
            } while (keepLooping);
        }

        static List<string> GetRules(string defaultRules)
        {
            string[] rulesTextArray = File.ReadAllLines(defaultRules);
            List<string> allRules = new List<string>();    

            foreach (string rule in rulesTextArray)
            {
                string[] individualRule = rule.Split('*');
                foreach (string finalRule in individualRule)
                {
                    string[] finalRuleSplit = finalRule.Split('|');

                    allRules.Add(finalRuleSplit[0]);
                }
            }

            return allRules; 
        }

        static void DisplayViewRules(List<string> allRules)
        {
            DisplayScreenHeader("Rules");

            foreach (string rule in allRules)
            {
                Console.WriteLine($"\t{rule}");
            }

            DisplayContinuePrompt("exit");
        }

        static void DisplayModifyRules()
        {

        }

        #endregion

        #region PLAYER METHODS

        static void DisplayPlayerMenu()
        {

        }

        #endregion
        // MAKE BETTER need better name for region
        #region CARD STUFF

        static List<PlayingCard> BuildCardDeck(string cardDeckInfo)
        {
            List<PlayingCard> cards = new List<PlayingCard>();

            string[] cardStrings = File.ReadAllLines(cardDeckInfo);
            foreach (string card in cardStrings)
            {
                string[] cardProperties = card.Split('|');

                PlayingCard newCard = new PlayingCard();

                Enum.TryParse(cardProperties[0], out PlayingCard.Suit suit);
                newCard.CardSuit = suit;

                Enum.TryParse(cardProperties[1], out PlayingCard.Rank rank);
                newCard.CardRank = rank;

                int.TryParse(cardProperties[2], out int value);
                newCard.CardValue = value;

                cards.Add(newCard);
            }

            return cards;
        }

        static void DisplayAllCards(List<PlayingCard> cards)
        {
            foreach (PlayingCard card in cards)
            {
                Console.WriteLine(card.CardInfo());
            }

            DisplayContinuePrompt("continue");
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
