using System;
using Xunit;
using System.IO;
using Lab03WordGuessGame;
using static Lab03WordGuessGame.Program;

namespace XUnitTestLab03WordGuessGame
{
  public class UnitTest1
  {
    /// <summary>
    /// makes sure the dummy text file "wordTest.txt" does not exist
    /// yet by deleting any known instance of it
    ///Simple test to create a text file with the word "Something"
    ///by using method being tested
    ///then checks to see if that file actually exists
    ///thus proven that the method
    ///exist
    /// </summary>
    [Fact]
    public void FileCanBeCreated()
    {
      File.Delete("wordTest.txt");
      AddWordstoFile("wordTest.txt", "Something");
      Assert.True(File.Exists("wordTest.txt"));
    }
    /// <summary>
    /// Tests if file can be updated with custom method
    /// creates file "wordTestUpdate.txt"
    /// updates it 5 times by add text "Something"
    /// this means text file should contain 5 lines
    /// counts number of lines until it does not equal null
    /// expects count to be 5
    /// </summary>
    [Fact]
    public void FileCanBeUpdated()
    {
      int count = 0;
      File.Delete("wordTestUpdate.txt");
      for (int i = 0; i < 5; i++)
      {
        AddWordstoFile("wordTestUpdate.txt", "Something");
      }
      using (StreamReader sr = File.OpenText("wordTestUpdate.txt"))
      {
        while (sr.ReadLine() != null)
        {
          count++;
        }
      }
      Assert.Equal(5, count);
    }
    /// <summary>
    ///makes sure the dummy text file "wordTestDelete.txt" does not exist
    /// yet by deleting any known instance of it
    ///create a text file with the word "Something"
    ///by using the add method
    ///then runs DeleteWordFile method to test that file
    ///can be deleted by removing the word
    ///then checks to see if that file doesn't exists
    /// </summary>
    [Fact]
    public void FileCanBeDeleted()
    {
      File.Delete("wordTestDelete.txt");
      AddWordstoFile("wordTestDelete.txt", "Something");
      DeleteWordFile("wordTestDelete.txt");
      Assert.False(File.Exists("wordTestDelete.txt"));
    }
    /// <summary>
    /// creates string variable "testWord"
    /// Makes sure wordAdd.txt doesn't exist
    /// creates wordAdd.txt with the first word of "Something"
    /// using AddWordstoFile method
    /// uses same method to add the second word of "Something Cool"
    /// reads the linr of the text file
    /// assign that value to "testWord"
    /// verifies the method works if the testWord is equal to
    /// "Something", the first word to be added to test file.
    /// verifies the method words if the testWord is equal to
    /// "Something Cool", the second word to be added to test file.
    /// </summary>
    [Theory]
    [InlineData("Something", 1)]
    [InlineData("Something Cool", 2)]
    public void WordsCanBeAdded(string expected, int input)
    {
      string testWord = "";
      File.Delete("wordAdd.txt");
      AddWordstoFile("wordAdd.txt", "Something");
      AddWordstoFile("wordAdd.txt", "Something Cool");
      using (StreamReader sr = File.OpenText("wordAdd.txt"))
      {
        for (int i = 0; i < input; i++)
        {
          testWord = sr.ReadLine();
        }
      }
      Assert.Equal(expected, testWord);
    }
    /// <summary>
    /// Test adds two words to dummy text file
    /// then tests the DeleteWords method to successfully delete
    /// the second line by specifying that word exactly
    /// then when streamreader tries to read the second line
    /// the expected value should be null
    /// file is verified to be updated
    /// </summary>
    [Fact]
    public void WordsCanBeDeleted()
    {
      string testWord = "";
      File.Delete("wordDelete.txt");
      AddWordstoFile("wordDelete.txt", "Something");
      AddWordstoFile("wordDelete.txt", "Something Cool");
      DeleteWords("wordDelete.txt", "Something Cool");
      using (StreamReader sr = File.OpenText("wordDelete.txt"))
      {
        for (int i = 0; i < 2; i++)
        {
          testWord = sr.ReadLine();
        }
      }
      Assert.Null(testWord);
    }
    /// <summary>
    /// this tests adds five dummy words to a text file tested called
    /// "wordRetriveFile.txt"
    /// Then it tests the method that retrieves the word on each
    /// line by specifying what line its on
    /// </summary>
    /// <param name="expected">what the test outcome expects</param>
    /// <param name="input">selects what line of the text
    /// file to output</param>
    [Theory]
    [InlineData("Something", 1)]
    [InlineData("Nothing", 2)]
    [InlineData("Everything", 3)]
    [InlineData("Always", 4)]
    [InlineData("Never", 5)]
    public void CanGetAllWords(string expected, int input)
    {
      File.Delete("wordRetrieveFile.txt");
      AddWordstoFile("wordRetrieveFile.txt", "Something");
      AddWordstoFile("wordRetrieveFile.txt", "Nothing");
      AddWordstoFile("wordRetrieveFile.txt", "Everything");
      AddWordstoFile("wordRetrieveFile.txt", "Always");
      AddWordstoFile("wordRetrieveFile.txt", "Never");
      Assert.Equal(expected, GetActualWord(input, "wordRetrieveFile.txt"));
    }
    /// <summary>
    /// checks if method can match user guess letter to the answer word
    /// and replaces the testCharacterArray with said letter
    /// initital testCharacterArray is filled with '-'
    /// if input is 'a', and as the answer array has an 'a'
    /// in the zero index, the '-' in the zero index of 
    /// testCharacterArray should be replaced with 'a'
    /// </summary>
    [Fact]
    public void DetectLetterInAnswer()
    {
      char input = 'a';
      char[] testCharacterArray = { '-', '-', '-', '-', '-', '-' };
      char[] answer = { 'a', 'b', 'd', 'e', 'f', 'g' };
      CompareGuess(answer, input, testCharacterArray);
      Assert.Equal('a', testCharacterArray[0]);
    }
    /// <summary>
    /// if the array to be displayed matches the answer
    /// array, returns true for win
    /// </summary>
    [Fact]
    public void GameIsWonTest()
    {
      char[] answerArray = { 'a', 'b', 'c' };
      char[] inputCharArray = { 'a', 'b', 'c' };
      Assert.True(DidWinGame(inputCharArray, answerArray));
    }
    /// <summary>
    /// if array doesn't match answer, returns false
    /// for win 
    /// </summary>
    [Fact]
    public void GameIsNotWonTest()
    {
      char[] answerArray = { 'a', 'b', 'c' };
      char[] inputCharArray = { 'a', 'e', 'c' };
      Assert.False(DidWinGame(inputCharArray, answerArray));
    }
  }
}
