using System;

// AUTHOR: Jiaxin Dong
// FILENAME: encryptWord.cs
// DATE: 4/15/2018
// VERSION: 1 (initial submission)
//
// This contains the definitions for encryptWord.cs.
//
// Interface invariants:
// Only words of length 4 or more can be encrypted.
// Only alphabetical letters are subject to encryption.
//
// preconditions: an int and boolean are given to the constructor.
// postconditions: an EncryptWord object is created with all stats
// set to 0, the Caesar Cipher shift set to input int, and the 'off'
// or 'on' status is set to the input status.
// 
// Use:
// Can initialize like: encryptWord encryptedWord = new encryptWord(1);
// Can see constructor comments for additional notes.
// After construction, can then encrypt like: encryptedWord.encrypt("hello");
// Then can guess the cipher shift like: encryptedWord.guess(2);
// See appropriate method notes for full set of preconditions/postconditions.
//
// Valid states:
// Valid states are when the cipher shift is a positive number, and the
// number of guesses is also positive. Then the guessing game can proceed 
// and the game statistics make sense.
//
// Assumptions:
// We assume that 'off' means there is no cipher shifting, 'on' means
// cipher shift is original value, and 'reset' resets the guess statistics.
// We assume that the state of the object cannot be changed at all. This
// assumption is backed by this direct quote from code review:
// 'User should not be able to change state of the object'. This directly
// implies that 'on' or 'off' status can only be set during initialization.
// We assume that the user, for an EncryptWord object, cannot change the
// state between 'off' and 'on' and we assume that the EncryptWord will
// always be 'on' or 'off' depending on how it was constructed.
// We assume that we can throw an exception in order to reject words less
// than 4 characters long.
// We assume we only shift alphabetical characters, and not say, numbers.
// We assume that we can include different packages for extra functions.
// We assume that when letters are shifted past the end, it wraps around,
// like "z" shifts to "a" if the shift is 1.

namespace cpsc5051 {
    class encryptWord {

        private const int MINIMUM_WORD_LENGTH = 4; // minimum length of a word to encrypt
        private const int ALPHABET_LENGTH = 26; // letters in alphabet
        private const char LOWER_ALPHABET_START = 'a'; // first letter of alphabet, lower case
        private const char LOWER_ALPHABET_END = 'z'; // last letter of alphabet, lower case
        private const char UPPER_ALPHABET_START = 'A'; // first letter of alphabet, upper case
        private const char UPPER_ALPHABET_END = 'Z'; // last letter of alphabet, upper case

        private int cipherShift; // the shift 
        private int maxGuess; // the biggest guess for the cipher shift
        private int minGuess; // the smallest guess for the cipher shift
        private int numberOfGuesses; // the total number of guesses
        private int guessSum; // sum of all guessed values for the cipher shift
        private bool isActive; // true if encryption is on, false if not

        // This is the constructor, which takes a shift value and creates a new
        // EncryptWord object.
        // precondition: given a shift value, and optionally a bool value 
        // for 'off' or 'on'. If not given, defaults to true, "on".
        // postcondition: creates a new EncryptWord object with the input shift
        // value and input 'isActive' value
        public encryptWord(int shift, bool isOn = true) {
            cipherShift = shift;
            maxGuess = 0;
            minGuess = 0;
            numberOfGuesses = 0;
            guessSum = 0;
            isActive = isOn;
        }

        // This method deactivates the encryption
        // precondition: encryptWord object created
        // postcondition: encryptWord's isActive field set to false
        public void deactivate() {
            this.isActive = false;
        }

        // This method activates the encryption
        // precondition: encryptWord object created
        // postcondition: encryptWord's isActive field set to true
        public void activate() {
            this.isActive = true;
        }

        // This method encrypts the input string with the Caesar Cipher shift value
        // that this object was created with
        // precondition: given a string value to encrypt with size at least 4
        // postcondition: returns the encrypted string value
        public String encrypt(String input) {
            if (input.Length < MINIMUM_WORD_LENGTH) {
                throw new ArgumentException();
            }

            String result = "";

            if (isActive) {
                for (int i = 0; i < input.Length; i++) {
                    char c = input[i];
                    if (c >= LOWER_ALPHABET_START && c <= LOWER_ALPHABET_END) {
                        c = (char) (c + cipherShift);
                        if (c > LOWER_ALPHABET_END) {
                            c = (char) (c - ALPHABET_LENGTH);
                        }
                        result += c;
                    }
                    else if (c >= UPPER_ALPHABET_START && c <= UPPER_ALPHABET_END) {
                        c = (char) (c + cipherShift);
                        if (c > UPPER_ALPHABET_END) {
                            c = (char) (c - ALPHABET_LENGTH);
                        }
                        result += c;
                    }
                    else {
                        result += c;
                    }
                }
            } else {
                result = input;
            }

            return result;
        }

        // This method decodes the given input string with the given Caesar Cipher
        // shift value.
        // precondition: Given an encrypted string
        // postcondition: The decoded string as given by the Caesar Cipher shift
        public string decode(string input) {
            string result = "";

            if (isActive) {
                for (int i = 0; i < input.Length; i++) {
                    char c = input[i];
                    if (c >= LOWER_ALPHABET_START && c <= LOWER_ALPHABET_END) {
                        c = (char) (c - cipherShift);
                        if (c < LOWER_ALPHABET_START) {
                            c = (char) (c + ALPHABET_LENGTH);
                        }
                        result += c;
                    }
                    else if (c >= UPPER_ALPHABET_START && c <= UPPER_ALPHABET_END) {
                        c = (char) (c - cipherShift);
                        if (c < UPPER_ALPHABET_START) {
                            c = (char) (c + ALPHABET_LENGTH);
                        }
                        result += c;
                    }
                    else {
                        result += c;
                    }
                }
            } else {
                result = input;
            }

            return result;
        }

        // This allows a player to guess what the Caesar Cipher shift is.
        // precondition: Given an integer number where a player is guessing what
        // the Caesar Cipher shift value is
        // postcondition: true if correct, else false. Also updates internal
        // statistic values like number of guesses or maximum guess.
        public bool guess(int guess) {
            if (guess > maxGuess) {
                maxGuess = guess;
            }
            if (guess < minGuess || minGuess == 0) {
                minGuess = guess;
            }
            numberOfGuesses++;
            guessSum += guess;

            return guess == cipherShift;
        }

        // This returns the statistics on a game of guessing what the Caesar
        // Cipher shift value is.
        // precondition: creation of EncryptWord object, and invokation of the
        // guess method 0 or more times.
        // postcondition: returns a summary of the game with how many guesses
        // were taken, the average guess, the highest and the lowest guesses.
        public string getStatistics() {
            String stats = "Max guess: " + maxGuess + "\n";
            stats += "Min guess: " + minGuess + "\n";
            stats += "Total guesses: " + numberOfGuesses + "\n";

            double avg = 0.0;
            if (numberOfGuesses > 0) {
                avg = ((double)guessSum / (double)numberOfGuesses);
            }

            stats += "Average guess: " + avg + "\n";
            return stats;
        }

        // This returns the minimum allowed size of a word to encrypt.
        // precondition: Creation of EncryptWord object
        // postcondition: Returns the minimum allowed size for a word to encrypt.
        public int getMinSize() {
            return MINIMUM_WORD_LENGTH;
        }

        // This resets the internal statistics stored by the EncryptWord object
        // that is used to track the guesses for what the Caesar Cipher shift is.
        // precondition: Creation of EncryptWord object
        // postcondition: all internal stat values reset to 0.
        public void reset() {
            numberOfGuesses = 0;
            maxGuess = 0;
            minGuess = 0;
            guessSum = 0;
        }
    }
}
