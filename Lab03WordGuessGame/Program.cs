using System;
using System.IO;

namespace Lab03WordGuessGame
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      Console.WriteLine("Welcome to Word Guess");
      TitleScreen();
      Console.ReadKey();
    }
    public static void TitleScreen()
    {
      string path = "words.txt";
      Console.WriteLine();
      Console.WriteLine("Main Menu");
      Console.WriteLine("Enter your selection number:");
      Console.WriteLine("1. Play Game");
      Console.WriteLine("2. Settings");
      Console.WriteLine("3. Exit");
      char input = Console.ReadKey().KeyChar;
      try
      {
        CheckSelectionInput(input, "main");
        if (input == (char) 49)
        {
          PlayGame(path);
        }
        if (input == (char) 50)
        {
          SettingsScreen(path);
        }
        if (input == (char) 51)
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

    public static void SettingsScreen(string path)
    {
      Console.WriteLine();
      Console.WriteLine("Welcome to the Settings Menu");
      Console.WriteLine("1. View Words");
      Console.WriteLine("2. Add Word");
      Console.WriteLine("3. Delete Word");
      Console.WriteLine("4. Back to Main");
      char input = Console.ReadKey().KeyChar;
      try
      {
        CheckSelectionInput(input, "settings");
        if (input == (char)49)
        {
          ViewWords(path);
        }
        if (input == (char)50)
        {
          AddWords(path);
        }
        if (input == (char)51)
        {
          DeleteWords(path);
        }
        if (input == (char)52)
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
        Console.WriteLine();
        SettingsScreen(path);
      }
    }
    public static void CheckSelectionInput(char input, string menu)
    {
      if (menu == "main" && input != (char)49 && input != (char)50 && input != (char)51)
      {
        throw new Exception("Sorry, but that is an invalid selection. Your options are 1, 2, or 3");
      }
      if (menu == "settings" && input != (char) 49 && input != (char) 50 && input != (char) 51 && input!= (char) 52)
      {
        throw new Exception("Sorry, but that is an invalid selection. Your options are 1, 2, 3, or 4");
      }

    }
    public static void AddWords(string path)
    {
      Console.WriteLine();
      Console.WriteLine("What word would you like to add (capital letters not required)?");
      string wordInput = Console.ReadLine().ToLower();
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
      finally
      {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        SettingsScreen(path);
      }
    }
    public static void DeleteWords(string path)
    {
      Console.WriteLine();
      Console.WriteLine("What word would you like to delete (capital letters not required)?");
      string wordInput = Console.ReadLine().ToLower();
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
          Console.WriteLine($"You word of \"{wordInput}\" has successfully been deleted");
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
      finally
      {
        Console.WriteLine("Press any key to return to Settings");
        Console.ReadKey();
        SettingsScreen(path);
      }
    }
    public static void ViewWords(string path)
    {
      Console.WriteLine();
      Console.WriteLine("The list of words kept are:");
      using (StreamReader sr = File.OpenText(path))
      {
        while (!sr.EndOfStream)
        {
          Console.WriteLine(sr.ReadLine());
        }
      }
      Console.WriteLine("Press any key to return to Settings");
      Console.ReadKey();
      SettingsScreen(path);
    }
    public static void CheckEmptyAnswer(string answer)
    {
      if (answer.Length < 1)
      {
        throw new ArgumentNullException();
      }
    }

    public static void PlayGame(string path)
    {
      Console.WriteLine();
      Console.WriteLine("Let's play Bamboozle");
      int wordLineCount = CountStoredLines(path);
      string chosenWord = GetActualWord(wordLineCount, path);
      gameOverallSequence(path, chosenWord);
    }
    public static int CountStoredLines(string path)
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
          return countLines;
        }
      }
      catch(Exception)
      {
        throw;
      }
    }

    public static string GetActualWord(int wordLineCount, string path)
    {
      string chosenWord = "";
      Random rand = new Random();
      int selectWordNumber = rand.Next(1, wordLineCount+1);
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
        char guessLetter = Console.ReadKey().KeyChar;
        for (int i = 0; i < chosenWordArray.Length; i++)
        {
          if (char.ToLowerInvariant(guessLetter) == chosenWordArray[i])
          {
            displayWordArray[i] = char.ToLowerInvariant(guessLetter);
          }
        }
        LetterGuessRendering(guessedLetterArray, guessLetter);
        winGame = didWinGame(displayWordArray);
        guessCount++;
      }
      PostGameOptions(path);
    }

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

    public static bool didWinGame(char[] wordArray)
    {
      int trackWord = 0;
      for (int i = 0; i < wordArray.Length; i++)
      {
        if (wordArray[i] == (char)45)
        {
          trackWord++;
        }

        if (i == wordArray.Length - 1 && trackWord == 0)
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

    public static void LetterGuessRendering(char[] letterArray, char letterGuess)
    {
      int tracker = 0;
      for (int i = 0; i < letterArray.Length; i++)
      {
        if (letterGuess == letterArray[i])
        {
          tracker++;
        }
      }
      if (tracker == 0)
      {
        for (int i = 0; i < letterArray.Length; i++)
        {
          if (letterArray[i] == (char) 0)
          {
            letterArray[i] = letterGuess;
            break;
          }
        }
      }
      Console.WriteLine();
      Console.Write("Letters guessed are: ");
      for (int i = 0; i < letterArray.Length; i++)
      {
        Console.Write($"{letterArray[i]} ");
      }
    }
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
      }
    }
  }
}
