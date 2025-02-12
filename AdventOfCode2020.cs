﻿using System;
using System.IO;

namespace AdventOfCode2020
{
    class AdventOfCode2020
    {
        static void Main(string[] args)
        {
            /*--- Day 1: Report Repair ---
            After saving Christmas five years in a row, you've decided to take a vacation at a nice resort on a tropical island. Surely, Christmas will go on without you.

            The tropical island has its own currency and is entirely cash-only. The gold coins used there have a little picture of a starfish; the locals just call them stars. None of the currency exchanges seem to have heard of them, but somehow, you'll need to find fifty of these coins by the time you arrive so you can pay the deposit on your room.

            To save your vacation, you need to get all fifty stars by December 25th.

            Collect stars by solving puzzles. Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star. Good luck!

            Before you leave, the Elves in accounting just need you to fix your expense report (your puzzle input); apparently, something isn't quite adding up.

            Specifically, they need you to find the two entries that sum to 2020 and then multiply those two numbers together.

            For example, suppose your expense report contained the following:

            1721
            979
            366
            299
            675
            1456
            In this list, the two entries that sum to 2020 are 1721 and 299. Multiplying them together produces 1721 * 299 = 514579, so the correct answer is 514579.

            Of course, your expense report is much larger. Find the two entries that sum to 2020; what do you get if you multiply them together?

            Your puzzle answer was 889779.

            --- Part Two ---
            The Elves in accounting are thankful for your help; one of them even offers you a starfish coin they had left over from a past vacation. They offer you a second one if you can find three numbers in your expense report that meet the same criteria.

            Using the above example again, the three entries that sum to 2020 are 979, 366, and 675. Multiplying them together produces the answer, 241861950.

            In your expense report, what is the product of the three entries that sum to 2020?

            Your puzzle answer was 76110336.

            Both parts of this puzzle are complete! They provide two gold stars: **
            */

            //Day 1 - Challenge
            string[] day1_DataSet = File.ReadAllLines("..\\Day1.txt");
            int desiredSum = 2020;

            Console.WriteLine("Day 1");
            SumTwoAndMultiply(ConvertStringArrayToInt(day1_DataSet), desiredSum);
            SumThreeAndMultiply(ConvertStringArrayToInt(day1_DataSet), desiredSum);

            /*--- Day 2: Password Philosophy ---
            Your flight departs in a few days from the coastal airport; the easiest way down to the coast from here is via toboggan.

            The shopkeeper at the North Pole Toboggan Rental Shop is having a bad day. "Something's wrong with our computers; we can't log in!" You ask if you can take a look.

            Their password database seems to be a little corrupted: some of the passwords wouldn't have been allowed by the Official Toboggan Corporate Policy that was in effect when they were chosen.

            To try to debug the problem, they have created a list (your puzzle input) of passwords (according to the corrupted database) and the corporate policy when that password was set.

            For example, suppose you have the following list:

            1-3 a: abcde
            1-3 b: cdefg
            2-9 c: ccccccccc
            Each line gives the password policy and then the password. The password policy indicates the lowest and highest number of times a given letter must appear for the password to be valid. For example, 1-3 a means that the password must contain a at least 1 time and at most 3 times.

            In the above example, 2 passwords are valid. The middle password, cdefg, is not; it contains no instances of b, but needs at least 1. The first and third passwords are valid: they contain one a or nine c, both within the limits of their respective policies.

            How many passwords are valid according to their policies?

            Your puzzle answer was 418.

            --- Part Two ---
            While it appears you validated the passwords correctly, they don't seem to be what the Official Toboggan Corporate Authentication System is expecting.

            The shopkeeper suddenly realizes that he just accidentally explained the password policy rules from his old job at the sled rental place down the street! The Official Toboggan Corporate Policy actually works a little differently.

            Each policy actually describes two positions in the password, where 1 means the first character, 2 means the second character, and so on. (Be careful; Toboggan Corporate Policies have no concept of "index zero"!) Exactly one of these positions must contain the given letter. Other occurrences of the letter are irrelevant for the purposes of policy enforcement.

            Given the same example list from above:

            1-3 a: abcde is valid: position 1 contains a and position 3 does not.
            1-3 b: cdefg is invalid: neither position 1 nor position 3 contains b.
            2-9 c: ccccccccc is invalid: both position 2 and position 9 contain c.
            How many passwords are valid according to the new interpretation of the policies?

            Your puzzle answer was 616.
            */

            //Day 2 - Challenge
            string[] day2_DataSet = File.ReadAllLines("..\\Day2.txt");

            Console.WriteLine("Day 2");
            Console.WriteLine($"Valid Password Count (Policy 1): {CountValidPasswordsPart1(day2_DataSet)}");
            Console.WriteLine($"Valid Password Count (Policy 2): {CountValidPasswordsPart2(day2_DataSet)}");



        static int CountValidPasswordsPart1(string[] data)
        {
            int validPasswordCount = 0;
            int minKeyOcurrence = 0;
            int maxKeyOcurrence = 0;
            int passKeyCounter = 0;

            char passKey;


            foreach(string p in data)
            {
                string[] splitData = p.Split(' ');
                string[] splitNum = splitData[0].Split('-');


                minKeyOcurrence = Int32.Parse(splitNum[0]);
                maxKeyOcurrence = Int32.Parse(splitNum[1]);
                passKey = splitData[1][0];

                for(int i = 0; i < splitData[2].Length; i++)
                {
                    if(splitData[2][i] == passKey)
                    {
                        passKeyCounter++;
                    }
                }
                if(passKeyCounter <= maxKeyOcurrence && passKeyCounter >= minKeyOcurrence)
                {
                    validPasswordCount++;
                }

                passKeyCounter = 0;
            }

            return validPasswordCount;
        }
        static int CountValidPasswordsPart2(string[] data)
        {
            int validPasswordCount = 0;
            int keyOcurrenceLocation1 = 0;
            int keyOcurrenceLocation2 = 0;

            char passKey;


            foreach (string p in data)
            {
                string[] splitData = p.Split(' ');
                string[] splitNum = splitData[0].Split('-');


                keyOcurrenceLocation1 = Int32.Parse(splitNum[0]);
                keyOcurrenceLocation2 = Int32.Parse(splitNum[1]);
                passKey = splitData[1][0];


                if (splitData[2][keyOcurrenceLocation1 - 1] == passKey && splitData[2][keyOcurrenceLocation2 - 1] != passKey)
                {
                    validPasswordCount++;
                }
                else if(splitData[2][keyOcurrenceLocation1 - 1] != passKey && splitData[2][keyOcurrenceLocation2 - 1] == passKey)
                {
                    validPasswordCount++;
                }

            }

            return validPasswordCount;
        }
        static int[] ConvertStringArrayToInt(string[] data)
        {
            int[] numData = new int[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                numData[i] = Int32.Parse(data[i]);
            }

            return numData;
        }
        static void SumTwoAndMultiply(int[] newArray, int checkSum)
        {
            int sum;

            for (int i = 0; i < newArray.Length; i++)
            {
                for (int j = 0; j < newArray.Length; j++)
                {
                    sum = newArray[i] + newArray[j];

                    if (sum == checkSum)
                    {
                        Console.WriteLine($"{newArray[i]} * {newArray[j]} = {newArray[i] * newArray[j]}");
                        return;
                    }
                }
            }
        }//end method SumThreeAndMultiply
        static void SumThreeAndMultiply(int[] newArray, int checkSum)
        {
            int sum;

            for (int i = 0; i < newArray.Length; i++)
            {
                for (int j = 0; j < newArray.Length; j++)
                {
                    for (int k = 0; k < newArray.Length; k++)
                    {
                        sum = newArray[i] + newArray[j] + newArray[k];

                        if (sum == checkSum)
                        {
                            Console.WriteLine($"{newArray[i]} * {newArray[j]} * {newArray[k]} = {newArray[i] * newArray[j] * newArray[k]}");
                            return;
                        }

                    }

                }
            }
        }//end method SumThreeAndMultiply




    }//end class Program
}//end namespace
