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
      Console.WriteLine(chosenWord);
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
  }
}
