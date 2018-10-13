# Lab03-Word-Guess-Game

## Word Guess Game

This is a basic C# Console Application for a generic hang man game call Bamboozle. Basic functions randomly selects a word from a stored list for the user to guess. User also has the ability to view the list of words, add or delete a specify word, or delete the entire list entirely.

## Version

V1.0 - 10/12/2018 Basic Functionality w/ Testing Unit

## Requirements

Visual Studios 2017 or equivalent C# IDE

.NET Core 2.1 SDK

## Instructions

Clone this repo to local storage and open it up using Visual Studios 2017.

Open the Lab03WordGuessGAme.sln solution located in the Lab02-Unit-Testing folder.

Compile the Program.cs and run the application.

A console command should appear to prompt you for inputs. 

Select options as they appear.

## Additional Notes

While playing game, console will automatically accept and process only one character input. 

Outside the game, user will have to specify the option and confirm it via the enter key.


The location of the stored Word List assumes the location of program execution is @ Lab03-Word-Guess-Game\Lab03WordGuessGame\bin\Debug\netcoreapp2.1\

The name of the store Word List is words.txt

Thus the path is specified as ../../../../words.txt, which is in the same location as the Lab03WordGuessGAme.sln.

If the user's settings is completely different (i.e. not using Visual Studios 2017), the path variable can be adjusted in the TitleScreen() method in Program.cs. Change ../../../../words.txt to just word.txt to relocate the path back to the location of program execution.

If the words.txt does not exist, it can be initiated by Adding a Word!

## Testing

XUnit is included and tests the following:

Test that a file can be created

Test that a file can be updated

Test that a file can be deleted

Test that a word can be added to a file

Test that all words can be retrieved from file

Test that the letter guessed will be compared to the answer and be rendered appropriately

## Result

![Console](Capture.PNG?raw=true "Output")
