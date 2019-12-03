﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

// NOTES TO ME TO DELETE WHEN TURN IN
// If any of my search terms are still in the program take care of them then delete this
// * RID = get rid of later, for easy find later
// * MAKE BETTER = flesh out/make pretty, for easy find later
// * IMPLEMENT = need to add a thing, for easy find later
// * HERE = where I left off, for easy find later

// TODO delete options to add player objects and modify rules if can't fix, better than a dysfunctional program.
// TODO player cards total isn't working, fix it

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
        // Last Modified: 11/27/19
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

            // -------------------------------------------------------------------------
            // declaring file paths for easy use later
            string cardDeckInfo = @"Data\cardDeckInfo.txt";
            string instructions = @"Data\instructions.txt";
            string defaultRules = @"Data\defaultRules.txt";
            //string players = @"Data\players.txt";
            List<Player> players = new List<Player>();

            Random random = new Random();

            // -------------------------------------------------------------------------
            // setting the defaults for rules that can be modified by the players
            (int numberDecks, int startingMoney, Dealer.BettingStyle bettingStyle, int numberRounds) modifiableRules;
            modifiableRules.numberDecks = 1;
            modifiableRules.startingMoney = 500;
            modifiableRules.bettingStyle = Dealer.BettingStyle.everyManForHimself;
            modifiableRules.numberRounds = 5;

            int userResponse;
            bool runApplication = true;
            
            while (runApplication)
            {
                DisplayScreenHeader("Main Menu");

                userResponse = ConsoleHelper.MultipleChoice(false, 1, 0, 0, "Play Game", "Instructions"/*, "Rules", "Players", "Help"*/, "Quit");
                switch (userResponse)
                {
                    case 0:
                        DisplayGame(random, cardDeckInfo, modifiableRules/*, players*/);
                        break;
                    case 1:
                        DisplayInstructionsMenu(instructions);
                        //DisplayLongGameInstructions(instructions);
                        break;
                    //case 2:
                    //    DisplayRulesMenu(defaultRules, modifiableRules);
                    //    break;
                    //case 3:
                    //    DisplayPlayerMenu(players);
                    //    break;
                    //case 4:

                    //    break;
                    case 2:
                        runApplication = false;
                        break;
                    default:
                        break;
                }
            }
        }

        #region PLAY GAME METHODS

        /// <summary>
        /// The initial method for displaying the playable game
        /// </summary>
        /// <param name="random">teh Random object</param>
        /// <param name="cardDeckInfo">The text file for the cards</param>
        /// <param name="modifiableRules">the changable rules</param>
        /// <param name="players">the List of players from the player menu</param>
        static void DisplayGame(Random random, string cardDeckInfo, (int numberDecks, int startingMoney, Dealer.BettingStyle bettingStyle, int numberRounds) modifiableRules)
        {
            List<PlayingCard> cardDeck = BuildCardDeck(cardDeckInfo);
            //List<PlayingCard> cardDeckTemp = new List<PlayingCard>();
            List<PlayingCard> discard = new List<PlayingCard>();
            //List<PlayingCard> dealerCards = new List<PlayingCard>();
            List<Player> players = new List<Player>();

            Dealer dealer = new Dealer();

            //int numberDecks = 1;
            int startingMoney = 500;
            int numberRounds = 5;
            //Dealer.BettingStyle bettingStyle = Dealer.BettingStyle.everyManForHimself;

            Player player1 = new Player("Player 1", startingMoney/*, 0, 0*/);
            Player player2 = new Player("Player 2", startingMoney/*, 0, 0*/);

            players.Add(player1);
            players.Add(player2);

            PlayingCard drawnCard;

            bool gameOver = false;

            //// -------------------------------
            //// Build card deck according to the numberDecks
            //if (modifiableRules.numberDecks > 1)
            //{
            //    for (int deck = 0; deck < modifiableRules.numberDecks - 1; deck++)
            //    {
            //        cardDeckTemp = BuildCardDeck(cardDeckInfo);
            //        cardDeck.AddRange(cardDeckTemp);
            //    }
            //}

            DisplayScreenHeader("Blackjack");

            // -------------------------------------------------------------------------
            // asking the user if they would like to see their modifiable rules, to be sure. 
            //AskAboutDisplayModifiedRules(modifiableRules);

            for (int round = 0; round < numberRounds; round++)
            {


                dealer.Cards.Add(DrawCard(random, cardDeck, discard));
                dealer.Cards.Add(DrawCard(random, cardDeck, discard));

                foreach (Player player in players)
                {
                    DisplayPlayerScreen(cardDeck, discard, random, player, dealer);
                }

                DisplayDealerScreen(players, dealer, cardDeck, discard, random);

                ClearAllHands(players, dealer);
            }

            

            //DisplayPlayerScreen(cardDeck, discard, random, player1, dealerCards);

            DisplayContinuePrompt("exit");
        }

        ///// <summary>
        ///// Asks the user if they want to see the modified rules prior to playing
        ///// </summary>
        ///// <param name="modifiableRules">the tuple of rules that can be modified</param>
        //static void AskAboutDisplayModifiedRules((int numberDecks, int startingMoney, Dealer.BettingStyle bettingStyle, int numberRounds) modifiableRules)
        //{
        //    int userResponse;
        //    Console.WriteLine("\tWould you like an overview of the rules? ");

        //    userResponse = ConsoleHelper.MultipleChoice(false, 2, 1, 0, "Yes", "No");
        //    switch (userResponse + 1)
        //    {
        //        case 1:
        //            DisplayModifiedRules(modifiableRules);

        //            Console.WriteLine("Are these rules okay?");
        //            userResponse = ConsoleHelper.MultipleChoice(false, 2, 4, 14, "Yes", "No");
        //            if (userResponse == 1)
        //            {
        //                DisplayModifyRules(modifiableRules);
        //            }

        //            break;

        //        case 2:
        //            break;

        //        default:
        //            break;
        //    }
        //}

        /// <summary>
        /// Displays what the player sees
        /// </summary>
        /// <param name="cardDeck">The deck of cards the gam eis being played with</param>
        /// <param name="discard">the discard for said cards</param>
        /// <param name="random"></param>
        static void DisplayPlayerScreen(List<PlayingCard> cardDeck, List<PlayingCard> discard, Random random, Player player, Dealer dealer)
        {
            PlayingCard drawnCard;
            //drawnCard = DrawCard(random, cardDeck, discard);
            bool turnEnd = false;
            bool validResponse = false;
            int userResponse;
            int playerBet = 0;

            //List<PlayingCard> playerCards = new List<PlayingCard>();

            player.Cards.Add(DrawCard(random, cardDeck, discard));
            player.Cards.Add(DrawCard(random, cardDeck, discard));

            DisplayScreenHeader(player.Name);

            Console.WriteLine($"\tYou have ${player.Money}");
            Console.Write("\tHow much would you like to bet? $");

            do
            {
                bool couldParse;

                couldParse = int.TryParse(Console.ReadLine(), out playerBet);

                if (couldParse)
                {
                    validResponse = true;
                }
                else
                {
                    DisplayErrorMessage("Please enter an integer [ex. 1, 56]");
                }

                if (playerBet > player.Money)
                {
                    validResponse = false;
                    DisplayErrorMessage("You cannot bet more than you have!");
                }

            } while (!validResponse);

            player.Money -= playerBet;
            player.RoundBet = playerBet;


            do
            {
                int playerCardsTotal/* = 0*/;

                Console.Clear();

                playerCardsTotal = GetCardValueTotal(player.Cards);

                DisplayScreenHeader(player.Name);

                //foreach (PlayingCard card in player.Cards)
                //{
                //    playerCardsTotal += card.CardValue;
                //}

                Console.WriteLine($"\t{player.Name}'s bet: ${playerBet}");

                Console.WriteLine($"\tDealer's Cards: {dealer.Cards[0].CardRank} of {dealer.Cards[0].CardSuit}, ???");

                WriteAllCards(player.Name, player.Cards);

                //Console.Write($"\tYour Cards: ");

                //foreach (PlayingCard card in playerCards)
                //{
                //    Console.Write($"{card.CardRank} of {card.CardSuit}, ");
                //}

                Console.WriteLine($"\n\tCurrent total: {playerCardsTotal}");

                if (playerCardsTotal < 21)
                {
                    userResponse = ConsoleHelper.MultipleChoice(false, 2, 5, 0, "Hit", "Stay");
                    switch (userResponse)
                    {
                        case 0:
                            player.Cards.Add(DrawCard(random, cardDeck, discard));
                            break;

                        case 1:
                            player.RoundTotal = playerCardsTotal;
                            Console.WriteLine($"\n\tYou stopped at: {player.RoundTotal}");
                            DisplayContinuePrompt("progress to the next player");
                            turnEnd = true;
                            break;

                        default:
                            DisplayErrorMessage("Faulty Input");
                            break;
                    }
                }
                else if (playerCardsTotal > 21)
                {
                    Console.WriteLine("\nBust!");
                    DisplayContinuePrompt("continue");
                    turnEnd = true;
                }
                else if ((playerCardsTotal == 21) && (player.Cards.Count == 2))
                {
                    Console.WriteLine("\nBlackjack!");
                    player.Money += playerBet;
                    DisplayContinuePrompt("continue");
                    turnEnd = true;
                }

            } while (!turnEnd);

            // DisplayContinuePrompt("continue");
        }

        static void DisplayDealerScreen(List<Player> players, Dealer dealer, List<PlayingCard> cardDeck, List<PlayingCard> discard, Random random)
        {
            bool keepLooping = true;

            do
            {
                int dealersTotal = 0;

                foreach (PlayingCard card in dealer.Cards)
                {
                    dealersTotal += card.CardValue;
                }

                DisplayScreenHeader("Dealer\'s Play");
                WriteAllCards("Dealer", dealer.Cards);
                Console.WriteLine($"\tDealer Total: {dealersTotal}");

                if (dealersTotal <= 15)
                {
                    dealer.Cards.Add(DrawCard(random, cardDeck, discard));
                }
                else if (dealersTotal > 21)
                {
                    Console.WriteLine("\tDealer Busts!");
                }

                DisplayContinuePrompt("continue");

            } while (keepLooping);

            DisplayContinuePrompt("move on to the next round");
        }

        static int GetCardValueTotal(List<PlayingCard> cards)
        {
            int total = 0;

            foreach (PlayingCard card in cards)
            {
                total += card.CardValue;
            }

            return total;
        }

        /// <summary>
        /// Writes out all of the cards in the specifed players or dealers hand
        /// </summary>
        /// <param name="who">the name for the hand that's being written out</param>
        /// <param name="cards">the list of cards in the hand</param>
        static void WriteAllCards(string who, List<PlayingCard> cards)
        {
            Console.Write($"\t{who}'s cards: ");

            foreach (PlayingCard card in cards)
            {
                Console.Write($"{card.CardRank} of {card.CardSuit}, ");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Resets all of the cards in hands to zero
        /// </summary>
        /// <param name="players">the list of players</param>
        static void ClearAllHands(List<Player> players, Dealer dealer)
        {
            dealer.Cards.Clear();

            foreach (Player player in players)
            {
                if (player.Cards.Count > 0)
                {
                    player.Cards.Clear();
                }
            }
        }

        #endregion

        #region INSTRUCTIONS METHODS

        /// <summary>
        /// Takes all the text from the instructions.txt file and prints it out on the screen line by line
        /// </summary>
        /// <param name="instructions">The file path for the instructions.txt file</param>
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

                userResponse = ConsoleHelper.MultipleChoice(true, 1, 0, 0, "Object of the Game", "Betting", "The Deal", "Getting a Blackjack", "The Play", "The Dealer\'s Play");
                userResponse += 1; // Imporoving program readability so that the first options is option #1
                switch (userResponse)
                {
                    case 0:
                        DisplaySubheadingContent("Object of the Game", submenuContent, 0);
                        break;
                    case 1:
                        DisplaySubheadingContent("Betting", submenuContent, 1);
                        break;
                    case 2:
                        DisplaySubheadingContent("The Deal", submenuContent, 2);
                        break;
                    case 3:
                        DisplaySubheadingContent("Getting a Blackjack", submenuContent, 3);
                        break;
                    case 4:
                        DisplaySubheadingContent("The Play", submenuContent, 4);
                        break;
                    case 5:
                        DisplaySubheadingContent("The Dealer\'s Play", submenuContent, 5);
                        break;
                    case -1:
                        RepeatCharacter(80, "-");
                        Console.WriteLine(/*"\n\n\t--------------------------------------------------------------------------" +*/
                              "\n\tText taken from the Bicycle Cards Website," +
                              "\n\tEdited https://bicyclecards.com/how-to-play/blackjack/" /*+
                              "\n\t--------------------------------------------------------------------------"*/);
                        RepeatCharacter(80, "-");

                        DisplayContinuePrompt("exit");

                        keepLooping = false;
                        break;
                    default:
                        DisplayErrorMessage("faulty input");
                        break;
                }
            } while (keepLooping);
        }

        /// <summary>
        /// Displays the text for the selected instruction
        /// </summary>
        /// <param name="submenuName">the name of the instruction</param>
        /// <param name="submenuContent">a list of the in depth text to be written out</param>
        /// <param name="index">used to access the specific item in the submenuContent list</param>
        static void DisplaySubheadingContent(string submenuName, string[] submenuContent, int index)
        {
            DisplayScreenHeader(submenuName);
            Console.WriteLine(submenuContent[index].PadRight(2).PadLeft(2));
            DisplayContinuePrompt("return to instructions");
            Console.Clear();
        }

        #endregion

        #region RULES METHODS

        //static void DisplayRulesMenu(string defaultRules, (int numberDecks, int startingMoney, Dealer.BettingStyle bettingStyle, int numberRounds) modifiableRules)
        //{
        //    DisplayScreenHeader("Rules");
        //    string[] allRules = GetRules(defaultRules);

        //    //string[] allRulesTemp = File.ReadAllLines(defaultRules);
        //    //string[] allRulesSplit;
        //    //List<string> allRules = new List<string>();
        //    //foreach (string rule in allRulesTemp)
        //    //{
        //    //    allRulesSplit = rule.Split('|');
        //    //}

        //    //foreach (string rule in allRulesSplit)
        //    //{
        //    //    allRules.Add(rule);
        //    //}
        //    // List<string> modifiableRulesList = GetModifiableRules(defaultRules);

        //    bool keepLooping = true;

        //    int userResponse;

        //    userResponse = ConsoleHelper.MultipleChoice(true, 1, 0, 0, "View Rules", "Modify Rules");
        //    do
        //    {
        //        switch (userResponse)
        //        {
        //            case 0:
        //                DisplayViewRules(allRules);
        //                break;

        //            case 1:
        //                DisplayModifyRules(modifiableRules);
        //                break;
        //            case -1:
        //                keepLooping = false;
        //                break;

        //            default:
        //                DisplayErrorMessage("faulty input");
        //                break;
        //        }
        //    } while (keepLooping);
        //}

        ///// <summary>
        ///// retrieves the rules from their text file, defaultRules.txt
        ///// </summary>
        ///// <param name="defaultRules">the path for the text file</param>
        ///// <returns>a list of all the rules properly taken apart to be written on the screen</returns>
        //static string[] GetRules(string defaultRules)
        //{
        //    string rulesText = File.ReadAllText(defaultRules);
        //    string[] allRules = rulesText.Split('*');

        //    return allRules;
        //}

        ///// <summary>
        ///// writes out and formats all of the rules for the game
        ///// </summary>
        ///// <param name="allRules">the list of rules that is generated with the GetRules method</param>
        //static void DisplayViewRules(string[] allRules)
        //{
        //    DisplayScreenHeader("Rules");

        //    foreach (string rule in allRules)
        //    {
        //        Console.WriteLine($"\t{rule}");
        //    }

        //    DisplayContinuePrompt("exit");
        //}
        ///// <summary>
        ///// lets the user change a few specific rules
        ///// </summary>
        ///// <param name="modifiableRules">a tuple that holds the rules that can be modified</param>
        //static void DisplayModifyRules((int numberDecks, int startingMoney, Dealer.BettingStyle bettingStyle, int numberRounds) modifiableRules)
        //{
        //    bool validResponse = false;
        //    bool couldParse;
        //    int numberErrorLines = 0;
        //    int yOffset = 12 + numberErrorLines;
        //    int userResponse;

        //    DisplayScreenHeader("Modify Rules");

        //    Console.WriteLine("\tCurrent Rules");
        //    Console.Write("\t");
        //    RepeatCharacter(20, "-");
        //    Console.WriteLine();
        //    Console.WriteLine($"\t Number of decks: {modifiableRules.numberDecks}");
        //    Console.WriteLine($"\t   Starting cash: ${modifiableRules.startingMoney}");
        //    Console.WriteLine($"\tNumber of Rounds: {modifiableRules.numberRounds}");
        //    if (modifiableRules.bettingStyle == Dealer.BettingStyle.everyManForHimself)
        //    {
        //        Console.WriteLine("\t   Betting Style: Every Man For Himself");
        //    }
        //    else if (modifiableRules.bettingStyle == Dealer.BettingStyle.threeMusketeers)
        //    {
        //        Console.WriteLine("\t   Betting Style: Three Musketeers");
        //    }

        //    Console.WriteLine();
        //    Console.WriteLine("\tNew Rules");
        //    Console.Write("\t");
        //    RepeatCharacter(20, "-");
        //    Console.WriteLine();

        //    Console.Write("\t Number of decks: ");
        //    do
        //    {
        //        couldParse = int.TryParse(Console.ReadLine(), out modifiableRules.numberDecks);
        //        if (couldParse)
        //        {
        //            validResponse = true;
        //        }
        //        else
        //        {
        //            //Console.SetCursorPosition();
        //            validResponse = false;
        //            DisplayErrorMessage("Please enter an integer, there can't be partial decks!");
        //            numberErrorLines += 2;
        //        }

        //    } while (!validResponse);

        //    // ------------------------------------------------------------
        //    // resetting the valid response variable for further validation
        //    validResponse = false;

        //    do
        //    {
        //        Console.Write("\t   Starting Cash: $");
        //        couldParse = int.TryParse(Console.ReadLine(), out modifiableRules.startingMoney);
        //        if (couldParse)
        //        {
        //            validResponse = true;
        //        }
        //        else
        //        {
        //            validResponse = false;
        //            DisplayErrorMessage("Please enter an integer, normal Blackjack deals with chips!");
        //            numberErrorLines += 2;
        //        }

        //    } while (!validResponse);

        //    // ------------------------------------------------------------
        //    // resetting the valid response variable for further validation
        //    validResponse = false;

        //    do
        //    {
        //        Console.Write("\tNumber of Rounds: ");
        //        couldParse = int.TryParse(Console.ReadLine(), out modifiableRules.numberRounds);
        //        if (couldParse)
        //        {
        //            validResponse = true;
        //        }
        //        else
        //        {
        //            validResponse = false;
        //            DisplayErrorMessage("Please enter an integer, there can't be half rounds!");
        //            numberErrorLines += 2;
        //        }

        //    } while (!validResponse);

        //    Console.Write("\t   Playing Style: ");
        //    userResponse = ConsoleHelper.MultipleChoice(false, 2, yOffset, 18, "Every Man For Himself", "Three Musketeers");

        //    if (userResponse == 0)
        //    {
        //        modifiableRules.bettingStyle = Dealer.BettingStyle.everyManForHimself;
        //    }
        //    else if (userResponse == 1)
        //    {
        //        modifiableRules.bettingStyle = Dealer.BettingStyle.threeMusketeers;
        //    }

        //    DisplayContinuePrompt("exit");
        //}

        ///// <summary>
        ///// Displays the rules that the player can modify and what they are set to
        ///// </summary>
        ///// <param name="modifiableRules">the tuple that holds the rules' values</param>
        //static void DisplayModifiedRules((int numberDecks, int startingMoney, Dealer.BettingStyle bettingStyle, int numberRounds) modifiableRules)
        //{
        //    DisplayScreenHeader("Player Defined Rules");

        //    Console.WriteLine($"Number of decks: {modifiableRules.numberDecks}");
        //    Console.WriteLine($"Starting cash: ${modifiableRules.startingMoney}");
        //    Console.WriteLine($"Number of Rounds: {modifiableRules.numberRounds}");
        //    if (modifiableRules.bettingStyle == Dealer.BettingStyle.everyManForHimself)
        //    {
        //        Console.WriteLine("Betting Style: Every Man For Himself");
        //    }
        //    else if (modifiableRules.bettingStyle == Dealer.BettingStyle.threeMusketeers)
        //    {
        //        Console.WriteLine("Betting Style: Three Musketeers");
        //    }
        //    //DisplayContinuePrompt("continue");
        //}

        #endregion

        #region PLAYER METHODS

        static void DisplayPlayerMenu(List<Player> players)
        {
            bool keepLooping = true;
            int userResponse;

            do
            {
                DisplayScreenHeader("Players Menu");

                userResponse = ConsoleHelper.MultipleChoice(true, 1, 0, 0, "View Players", "Add Players", "Remove Players");
                switch (userResponse)
                {
                    case 0:
                        DisplayViewPlayers(players);
                        break;

                    case 1:
                        DisplayAddPlayers(players);
                        break;

                    case 2:
                        DisplayRemovePlayers(players);
                        break;

                    case -1:
                        keepLooping = false;
                        break;

                    default:
                        DisplayErrorMessage("faulty keystroke");
                        break;
                }
            } while (keepLooping);

            DisplayContinuePrompt("RID");
        }

        static void DisplayViewPlayers(List<Player> players)
        {
            DisplayScreenHeader("Players");

            foreach (Player player in players)
            {
                Console.WriteLine(player.Name);
            }

            DisplayContinuePrompt("return to the menu");
        }

        static List<Player> DisplayAddPlayers(List<Player> players)
        {
            List<Player> newPlayers = new List<Player>();
            Player newPlayer = new Player();
            string newName;
            bool keepLooping =  true;

            DisplayScreenHeader("Add a New Character");
            Console.WriteLine("\tAdd as many players as you\'d like, leave the field blank and press enter when you want to stop adding.");

            do
            {
                Console.Write("Player Name: ");
                newName = Console.ReadLine();
                if (newName == "")
                {
                    keepLooping = false;
                }
                else
                {
                    newPlayer.Name = newName;
                    newPlayers.Add(newPlayer);
                }

            } while (keepLooping);

            return newPlayers;
        }

        static void DisplayRemovePlayers(List<Player> players)
        {

        }

        #endregion

        #region CARD STUFF

        /// <summary>
        /// Takes card info from a text file, makes each of the cards into a PlayingCard, then creates a deck of PlayingCard
        /// </summary>
        /// <param name="cardDeckInfo">the text file that holds the card info</param>
        /// <returns>a list of PlayingCards</returns>
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

        /// <summary>
        /// Randomly selects a card from the card deck then places it in the discard
        /// </summary>
        /// <param name="random">the Random object</param>
        /// <param name="cardDeck">a List of PlayingCard, the deck</param>
        /// <param name="discard">a list of PlayingCard, discarded from the deck</param>
        /// <returns>the randomly selected PlayingCard</returns>
        static PlayingCard DrawCard(Random random, List<PlayingCard> cardDeck, List<PlayingCard> discard)
        {

            PlayingCard drawnCard;

            int randomNumber = random.Next(cardDeck.Count());

            drawnCard = cardDeck[randomNumber];

            discard.Add(drawnCard);

            return drawnCard;
        }

        /// <summary>
        /// Uses the cardInfo method in the PlayingCard class to write out a list of playing cards
        /// </summary>
        /// <param name="cards">a list of PlayingCard</param>
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

        /// <summary>
        /// A simple method that waits for user input to resume executing the code
        /// </summary>
        /// <param name="action">what the user is pressing the key for, like "continue", "exit", or something more specific</param>
        static void DisplayContinuePrompt(string action)
        {
            //Console.WriteLine();
            Console.WriteLine($"\tPress enter to {action}.");
            Console.ReadLine();
        }

        /// <summary>
        /// Clears the console window then displays a header with the specified text
        /// </summary>
        /// <param name="headerText">what needs to be displayed</param>
        static void DisplayScreenHeader(string headerText)
        {
            int dashRepeat = 74; // a constant throughout the program, when I have a line for a main heading, it's 74 characters long

            Console.Clear();
            Console.Write("\t");
            RepeatCharacter(dashRepeat, "-");
            Console.WriteLine("\n\t\t" + headerText);
            Console.Write("\t");
            RepeatCharacter(dashRepeat, "-");
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Shows a simple error message indicating what the user did wrong via input from the program. Mostly for formatting.
        /// </summary>
        /// <param name="error">The message that is to be displayed, and what the user should do to fix the error</param>
        static void DisplayErrorMessage(string error)
        {
            Console.WriteLine($"\t** {error} **");
            // MessageBox.Show($"\t** {error} **");

            // DisplayContinuePrompt("retry");
        }

        /// <summary>
        /// repeats a specified character
        /// </summary>
        /// <param name="numberTimes">the number of times to repeat the character</param>
        /// <param name="character">the character that needs to be repeated</param>
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
