using System;

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
        }
        if (input == (char)50)
        {
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

    public static void AddWords()
    {

    }
  }
}
