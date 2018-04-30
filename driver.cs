// AUTHOR: Jiaxin Dong
// FILENAME: driver.cs
// DATE: 4/29/2018
// VERSION: 1 (initial submission)
//
// Description:
// This driver demonstrates the usage of the EncryptWord object.
// It tests the chosen encryption method, the Caesar Cipher, with
// various cipher shift values and with various string inputs.
//
// The input is already predetermined and doesn't take user input so that
// demonstration is smooth. The program explains what is being tested at
// each stage.
//
// Assumptions:
// It is assumed that no user input is necessary for a demonstration on how
// the EncryptWord object is used.
// We assume only letters are shifted, no other characters.

using System;

namespace cpsc5051 {
    public class driver {

        public void run() {
            Console.WriteLine("This demonstrates the use of the EncryptWord object.");
            Console.WriteLine("It uses the Caesar Cipher to encrypt words.");
            Console.WriteLine("No user input is needed as we demonstrate the use for you.");
            Console.WriteLine();

            Console.WriteLine("We first test encrypting with a shift of 1:");
            encryptWord encryptedWord = new encryptWord(1);

            string hello = "hello";
            Console.WriteLine("Encrypting '" + hello + "'");
            Console.WriteLine("Result: " + encryptedWord.encrypt(hello));
            string helloWord = "hello WORLD (1 2 3) & go";
            Console.WriteLine("Encrypting '" + helloWord + "'");
            Console.WriteLine("(Only letters are encrypted)");
            Console.WriteLine("Result: " + encryptedWord.encrypt(helloWord));

            encryptedWord.deactivate();
            Console.WriteLine("Encryption is 'off', so original word should be returned.");
            Console.WriteLine("Result: " + encryptedWord.encrypt(hello));

            encryptedWord.activate();
            Console.WriteLine("Encryption is now 'on', so word should be shifted.");
            Console.WriteLine("Result: " + encryptedWord.encrypt(hello));

            Console.WriteLine();
            Console.WriteLine("Now we demonstrate guessing the shift, first a couple wrong guesses,");
            Console.WriteLine("then a right guess and then printing statistics");
            encryptedWord.decode("ifmmp");
            Console.WriteLine("Now we guess the Caesar Cipher shift for the word 'ifmmp': ");
            Console.WriteLine("We guess 2, 3, then 1. 2 wrong guesses and 1 right guess.");
            bool firstGuess = encryptedWord.guess(2);
            bool secondGuess = encryptedWord.guess(3);
            bool thirdGuess = encryptedWord.guess(1);

            Console.WriteLine("First guess: " + (firstGuess ? "Correct" : "Wrong"));
            Console.WriteLine("Second guess: " + (secondGuess ? "Correct" : "Wrong"));
            Console.WriteLine("Third guess: " + (thirdGuess ? "Correct" : "Wrong"));

            Console.WriteLine(encryptedWord.getStatistics());
            encryptedWord.reset();
            Console.WriteLine("Statistics have been reset:");
            Console.WriteLine(encryptedWord.getStatistics());

            Console.WriteLine("Input with less than 4 is rejected");
            Console.WriteLine("Now trying to encrypt the word 'the'");
            try
            {
                encryptedWord.encrypt("the");
            }
            catch (ArgumentException s)
            {
                Console.WriteLine("Exception is: " + s.Message);
            }

            Console.WriteLine();
            encryptWord anotherWord = new encryptWord(3);
            Console.WriteLine("Now using a new EncryptWord object with shift 3");
            string anotherTestCase = "I love programming";
            Console.WriteLine("Now encrypting '" + anotherTestCase + "'");
            Console.WriteLine("Result: " + anotherWord.encrypt(anotherTestCase));
            Console.WriteLine();

            encryptWord alphabetPart = new encryptWord(2);
            Console.WriteLine("Now using a new EncryptWord object with shift 2");
            string lowerCaseTest = "abc xyz";
            Console.WriteLine("Encrypting start and end of lower case alphabet, '"
                 + lowerCaseTest + "'");
            Console.WriteLine("Result: " + alphabetPart.encrypt(lowerCaseTest));
            Console.WriteLine("Now decoding '" + lowerCaseTest + "'");
            Console.WriteLine("Result: " + alphabetPart.decode(lowerCaseTest));

            string upperCaseTest = "ABC XYZ";
            Console.WriteLine("Encrypting start and end of upper case alphabet, "
                 + upperCaseTest + ":");
            Console.WriteLine("Result: " + alphabetPart.encrypt(upperCaseTest));
            Console.WriteLine("Now decoding ABC XYZ");
            Console.WriteLine("Result: " + alphabetPart.decode(upperCaseTest));
            Console.WriteLine();
        }
    }
}
