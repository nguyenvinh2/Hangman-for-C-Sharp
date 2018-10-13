using System;
using System.IO;

namespace Lab03WordGuessGame
{
  public class Program
  {
    /// <summary>
    /// just some into and calls up the titlescreen
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      Console.WriteLine("Welcome to Word Guess");
      TitleScreen();
      Console.ReadKey();
    }
    /// <summary>
    /// Displays titlescreen selection options
    /// checks user input for invalid selection
    /// otherwise redirects user based on option selection
    /// </summary>
    public static void TitleScreen()
    {
      string path = "words.txt";
      Console.WriteLine();
      Console.WriteLine("Main Menu");
      Console.WriteLine("Enter your selection number:");
      Console.WriteLine("1. Play Game");
      Console.WriteLine("2. Settings");
      Console.WriteLine("3. Exit");
      string input = Console.ReadLine();
      try
      {
        CheckSelectionInput(input, "main");
        if (input == "1")
        {
          Console.WriteLine();
          Console.WriteLine("You selected Play Game. Press any key to continue");
          Console.ReadKey();
          PlayGame(path);
        }
        if (input == "2")
        {
          SettingsScreen(path);
        }
        if (input == "3")
        {
          Environment.Exit(0);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine();
        Console.WriteLine(e.Message);
        Console.WriteLine("Press any key to return to Main Menu");
        Console.ReadKey();
        TitleScreen();
      }
    }
    /// <summary>
    /// another selection screen for settings
    /// gives options, checks for valid input
    /// and redirects users
    /// the finally block probably isn't needed
    /// in this case
    /// </summary>
    /// <param name="path">tells where to create word.txt file
    ///in this case in the place of execution.
    /// </param>
    public static void SettingsScreen(string path)
    {
      Console.WriteLine();
      Console.WriteLine("Welcome to the Settings Menu");
      Console.WriteLine("1. View Words");
      Console.WriteLine("2. Add Word");
      Console.WriteLine("3. Delete Word");
      Console.WriteLine("4. Clear Word List");
      Console.WriteLine("5. Back to Main");
      string input = Console.ReadLine();
      try
      {
        CheckSelectionInput(input, "settings");
        if (input == "1")
        {
          ViewWords(path);
          Console.WriteLine("Press any key to return to the Settings menu...");
          Console.ReadKey();
          Console.WriteLine();
        }
        if (input == "2")
        {
          AskForWord(path, "add");
        }
        if (input == "3")
        {
          AskForWord(path, "remove");
        }
        if (input == "4")
        {
          DeleteWordFile(path);
          Console.WriteLine("All your stored words have successfully been deleted");
        }
        if (input == "5")
        {
          TitleScreen();
        }
      }
      catch (Exception e)
      {
        Console.WriteLine();
        Console.WriteLine(e.Message);
        Console.WriteLine("Press any key to return to Settings");
        Console.ReadKey();
      }
      finally
      {
        SettingsScreen(path);
      }
    }
    /// <summary>
    /// throws catch for invalid user input
    /// actually for both titlescreen and settings
    /// based on parameter passed
    /// </summary>
    /// <param name="input">user input - very dynamic</param>
    /// <param name="menu">checks from what menu the user 
    /// is giving the input
    /// </param>
    public static void CheckSelectionInput(string input, string menu)
    {
      if (menu == "main" && input != "1" && input != "2" && input != "3")
      {
        throw new Exception("Sorry, but that is an invalid selection. Your options are 1, 2, or 3");
      }
      if (menu == "settings" && input != "1" && input != "2" && input != "3" && input!= "4" && input != "5")
      {
        throw new Exception("Sorry, but that is an invalid selection. Your options are 1, 2, 3, 4, or 5");
      }

    }
    /// <summary>
    /// asks user for input
    /// </summary>
    /// <param name="path">same variable as before
    /// </param>
    public static void AskForWord(string path, string action)
    {
      string wordInput;
      Console.WriteLine();
      if (action == "add")
      {
        Console.WriteLine("What word would you like to add (capital letters not required)?");
        wordInput = Console.ReadLine().ToLower();
        AddWordstoFile(path, wordInput);
      }
      else if (action == "remove")
      {
        Console.WriteLine("What word would you like to delete (capital letters not required)?");
        wordInput = Console.ReadLine().ToLower();
        DeleteWords(path, wordInput);
      }
    }
    /// <summary>
    /// uses streamwriter to add in text to words.text file
    /// everything will be brought down to lowercasing
    /// does reject empty inputs and throws nullexception
    /// also have generic exception to catch any error that is missed
    /// </summary>
    /// <param name="path"></param>
    /// <param name="wordInput">user input</param>
    public static void AddWordstoFile(string path, string wordInput)
    {
      try
      {
        CheckEmptyAnswer(wordInput);
        using (StreamWriter sw = File.AppendText(path))
        {
          sw.WriteLine(wordInput);
          sw.Close();

          Console.WriteLine($"Your input of \"{wordInput}\" has been accepted.");
        }
      }
      catch (ArgumentNullException)
      {
        Console.WriteLine("Please provide an input");
      }
      catch (Exception e)
      {
        Console.WriteLine($"Your input cannot be accepted because of:");
        Console.WriteLine(e.Message);
      }
    }
    /// <summary>
    /// gets input from user about what word to delete from "path"
    /// Uses streamwriter and stream reader 
    /// doesn't really delete but creates a temp files and copies everything from the original file except for the word being delete.
    /// then deletes the original file and renames the temp file to the original file
    /// does input check similar to add
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteWords(string path, string wordInput)
    {
      try
      {
        int count = 0;
        CheckEmptyAnswer(wordInput);
        string tempFile = Path.GetTempFileName();
        using (StreamReader sr = File.OpenText(path))
        using (StreamWriter sw = File.AppendText(tempFile))
        {
          string currentWordSelected;
          while (!sr.EndOfStream)
          {
            currentWordSelected = sr.ReadLine();
            if (currentWordSelected != wordInput)
            {
              sw.WriteLine(currentWordSelected);
            }
            if (currentWordSelected == wordInput)
            {
              count = 1;
            }
          }
          sw.Close();
        }
        File.Delete(path);
        File.Move(tempFile, path);
        if (count == 0)
        {
          Console.WriteLine($"The word \"{wordInput}\" could not be found");
        }
        else
        {
          Console.WriteLine($"Your word of \"{wordInput}\" has successfully been deleted");
        }
      }
      catch (ArgumentNullException)
      {
        Console.WriteLine("Please provide an input");
      }
      catch (Exception e)
      {
        Console.WriteLine($"Your input cannot be accepted because of:");
        Console.WriteLine(e.Message);
      }
    }
    /// <summary>
    /// this is a simple file delete for stored words
    /// wipes out memory of all words kept
    /// </summary>
    /// <param name="path"></param>
    public static void DeleteWordFile(string path)
    {
      try
      {
        File.Delete(path);
      }
      catch(Exception e)
      {
        Console.WriteLine(e.Message);
      }

    }
    /// <summary>
    /// this one just reads the text file from "path" using StreamReader
    /// no user input
    /// </summary>
    /// <param name="path"></param>
    public static void ViewWords(string path)
    {
      Console.WriteLine("The list of words kept are:");
      using (StreamReader sr = File.OpenText(path))
      {
        while (!sr.EndOfStream)
        {
          Console.WriteLine(sr.ReadLine());
        }
      }
    }
    /// <summary>
    /// simple input check for error throw
    /// no empty inputs
    /// </summary>
    /// <param name="answer">user input in string format</param>
    public static void CheckEmptyAnswer(string answer)
    {
      if (answer.Length < 1)
      {
        throw new ArgumentNullException();
      }
    }
    /// <summary>
    /// this is called from main menu
    /// just provides the intro
    /// selects a random word
    /// and sends it to the method that actually handles
    /// the game "gameOverallSequence"
    /// </summary>
    /// <param name="path">the location of the words.txt file that is being
    /// chained passed through these methods</param>
    public static void PlayGame(string path)
    {
      Console.WriteLine();
      Console.WriteLine("Let's play Bamboozle");
      Console.WriteLine("Enter any character from your keyboard to guess the word hidden below:");
      int randomLine = GetRandomLine(path);
      string chosenWord = GetActualWord(randomLine, path);
      gameOverallSequence(path, chosenWord);
    }
    /// <summary>
    /// count how many words there are in the words.txt file so randomizer can be handled
    /// </summary>
    /// <param name="path"></param>
    /// <returns>rnumber of words in text file</returns>
    public static int GetRandomLine(string path)
    {
      int countLines = 0;
      try
      {
        using (StreamReader sr = File.OpenText(path))
        {
          while (sr.ReadLine() != null)
          {
            countLines++;
          }
          Random rand = new Random();
          int selectWordNumber = rand.Next(1, countLines + 1);
          return selectWordNumber;
        }
      }
      catch(Exception)
      {
        throw;
      }
    }
    /// <summary>
    /// creates a random int within the range of the number of words in the text file. selects word based on that number
    /// </summary>
    /// <param name="wordLineCount">how many words avaiable to choose</param>
    /// <param name="path"></param>
    /// <returns>the chosen word for the game</returns>
    public static string GetActualWord(int selectWordNumber, string path)
    {
      string chosenWord = "";
      try
      {
        using (StreamReader sr = File.OpenText(path))
        {
          for (int i = 0; i<selectWordNumber; i++)
          {
            chosenWord = sr.ReadLine();
          }
          return chosenWord;
        }
      }
      catch (Exception)
      {
        throw;
      }
    }
    /// <summary>
    /// handles the main game logic
    /// breaks the chosen word to character array
    /// creates new array with length to match chosen word array
    /// renders the new array initial filed with "-" by calling up another method
    /// accepts user input for letter guess
    /// compares the input to the chosen word array
    /// replaces the "-" with correct letter guess
    /// checks logic if user won game 
    /// repeats if user did not win game
    /// also stores characters user inputs
    /// creates new array of 50 characters to account for all keys on keyboard
    /// adds it to the array after user inputs that specific key
    /// if user wins, runs postgameoptions
    /// </summary>
    /// <param name="path"></param>
    /// <param name="chosenWord">the word the user has to guess
    /// </param>
    public static void gameOverallSequence(string path, string chosenWord)
    {
      int guessCount = 0;
      bool winGame = false;
      char[] guessedLetterArray = new char[50]; 
      char[] chosenWordArray = chosenWord.ToCharArray();
      char[] displayWordArray = new char[chosenWord.Length];

      while (winGame == false)
      {
        Console.WriteLine();
        WordRendering(displayWordArray, guessCount);
        Console.WriteLine("Guess a Letter");
        char guessLetter = Console.ReadKey().KeyChar;
        displayWordArray = CompareGuess(chosenWordArray, guessLetter, displayWordArray);
        LetterGuessRendering(guessedLetterArray, guessLetter);
        winGame = DidWinGame(displayWordArray, chosenWordArray);
        guessCount++;
      }
      PostGameOptions(path);
    }
    /// <summary>
    /// compares input to the answer array
    /// if matches, updates the displayWord with that letter
    /// to display onto the console
    /// </summary>
    /// <param name="chosenWord">the answer</param>
    /// <param name="guessLetter">the input</param>
    /// <param name="displayWord">what is being displayed</param>
    /// <returns></returns>
    public static char[] CompareGuess(char[] chosenWord, char guessLetter, char[] displayWord)
    {
      for (int i = 0; i < chosenWord.Length; i++)
      {
        if (char.ToLowerInvariant(guessLetter) == chosenWord[i])
        {
          displayWord[i] = char.ToLowerInvariant(guessLetter);
        }
      }
      return displayWord;
    }
    /// <summary>
    /// reveals the word onto the console as the user starts guessing the right letters
    /// initially will be all "-"
    /// </summary>
    /// <param name="displayWordArray">what is being rendered onto console</param>
    /// <param name="count">how many times the user guessed</param>
    public static void WordRendering(char[] displayWordArray, int count)
    {
      for (int i = 0; i < displayWordArray.Length; i++)
      {
        if (count == 0)
        {
          displayWordArray[i] = (char) 45;
          Console.Write(displayWordArray[i]);
        }
        else
        {
          Console.Write(displayWordArray[i]);
        }
      }
      Console.WriteLine();
    }
    /// <summary>
    /// checks if the array has any dashes left in it
    /// if not, the user has fully guess the word
    /// </summary>
    /// <param name="wordArray">the arrray correctly guess letters</param>
    /// <returns></returns>
    public static bool DidWinGame(char[] wordArray, char[] answerArray)
    {
      int trackWord = 0;
      for (int i = 0; i < wordArray.Length; i++)
      {
        if (wordArray[i] == answerArray[i])
        {
          trackWord++;
        }

        if (trackWord == answerArray.Length)
        {
          Console.WriteLine();
          WordRendering(wordArray, 1);
          Console.WriteLine("You Won!");
          Console.WriteLine("For a full feature game, go to:");
          Console.WriteLine("https://nguyenvinh2.github.io/Sandman/");
          return true;
        }
      }
      return false;
    }
    /// <summary>
    /// Without loops, this method is a little too convoluted
    /// updates the array for already guessed letters 
    /// so it can be rendered onto the console
    /// </summary>
    /// <param name="letterArray">an already of letters the user already guess</param>
    /// <param name="letterGuess">the character the user guess</param>
    public static void LetterGuessRendering(char[] letterArray, char letterGuess)
    {
      int tracker = 0;
      //checks if what the user inputted matches any of the already stored letters
      for (int i = 0; i < letterArray.Length; i++)
      {
        if (char.ToLowerInvariant(letterGuess) == letterArray[i])
        {
          tracker++;
          Console.WriteLine();
          Console.WriteLine("You already guessed this letter");
        }
      }
      //if not, finds the next null space to store that letter in the array
      //then breaks opertaion
      if (tracker == 0)
      {
        for (int i = 0; i < letterArray.Length; i++)
        {
          if (letterArray[i] == (char) 0)
          {
            letterArray[i] = char.ToLowerInvariant(letterGuess);
            break;
          }
        }
      }
      Console.WriteLine();
      //renders out onto the console letters the user has already guess
      Console.Write("Letters guessed are: ");
      for (int i = 0; i < letterArray.Length; i++)
      {
        Console.Write($"{letterArray[i]} ");
      }
    }
    /// <summary>
    /// simply asks if the user wants to play again
    /// and makes it so
    /// if not, goes back to the titlescreen
    /// </summary>
    /// <param name="path"></param>
    public static void PostGameOptions(string path)
    {
      Console.WriteLine("Play Again? (Y/N)");
      char input = Console.ReadKey().KeyChar;
      if (char.ToLowerInvariant(input) == (char)121)
      {
        PlayGame(path);
      }
      else if (char.ToLowerInvariant(input) == (char)110)
      {
        Console.WriteLine();
        Console.WriteLine("Returning to Main Menu");
        TitleScreen();
      }
      else
      {
        Console.WriteLine();
        Console.WriteLine("Please pick a proper option.");
        PostGameOptions(path);
      }
    }
  }
}
