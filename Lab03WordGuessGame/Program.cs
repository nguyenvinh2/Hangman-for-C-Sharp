using System;
using System.IO;

namespace Lab03WordGuessGame
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      Console.WriteLine("Welcome to ");
      Console.WriteLine();
      TitleScreen();
    }
    public static void TitleScreen()
    {
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
        }
        if (input == (char) 50)
        {
          SettingsScreen();
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
      }
      finally
      {
        Console.WriteLine();
        TitleScreen();
      }
    }

    public static void SettingsScreen()
    {
      string path = "words.text";
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
        }
        if (input == (char)52)
        {
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
        SettingsScreen();
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
      Console.WriteLine("What word would you like to add?");
      string wordInput = Console.ReadLine();
      try
      {
        CheckEmptyAnswer(wordInput);
        using (StreamWriter sr = File.AppendText(path))
        {
          sr.WriteLine(wordInput);
          sr.Close();

          Console.WriteLine($"Your input of \"{wordInput}\" has been accepted.");
          Console.WriteLine(File.ReadAllText(path));
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
        SettingsScreen();
      }
    }
    public static void ViewWords(string path)
    {
      Console.WriteLine("The list of words kept are:");
      using (StreamReader sr = File.OpenText(path))
      {
        string words;
        while ((words = sr.ReadLine()) != null)
        {
          Console.WriteLine(words);
        }
      }
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
      SettingsScreen();
    }

    public static void CheckEmptyAnswer(string answer)
    {
      if (answer.Length < 1)
      {
        throw new ArgumentNullException();
      }
    }
  }
}
