using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nepomnyaschy.Nsudotnet.NumberGuesser
{
    class Program
    {
        public static string[] humilition = {
                "Oh you so stupid, {0}",
                "Wonderful, here a test's result: You are a stupid human, {0}. I'm serious, here what he says: 'stupid human'. Even we didnt test this",
                "LOOOOOOOOOOSEEEEEEER",
                "U so dumb, {0}. I hope u cant have children",
                "Lets try it in Spanish: El dumbass mas grande en el mundo"
            };


        private static int userNumber;
        private static int totalTries;
        private static Random random = new Random();
        private static string[] userTries = new string[1000];



        static void Main(string[] args)
        {
            Console.WriteLine("Hello, u need to guess number from 0 to 100, write ur name, when u ready");
            var username = Console.ReadLine();
            Console.WriteLine($"Okay, {username}, Let start. Ur time starts NOW");
            var startTime = DateTime.Now.Minute;


            var randomNumber = random.Next(0, 100);

            while (true)
            {
                var readLine = Console.ReadLine();
                if (int.TryParse(readLine, out userNumber))
                {
                    if (userNumber > 100 || userNumber < 0)
                    {
                        foreach (var s in humilition)
                        {
                            Console.WriteLine(s, username);
                        }
                    }
                    if (userNumber == randomNumber)
                    {
                        Console.WriteLine("K R A C U B O");
                        var time = DateTime.Now.Minute - startTime;
                        Console.WriteLine($"It takes u {time} minutes and {totalTries} tries");
                        Console.WriteLine("History:");
                        for (int i = 0; i < totalTries; i++)
                        {
                            Console.WriteLine(userTries[i]);
                        }
                        Console.WriteLine("Bye!");
                        Console.ReadLine();
                        return;
                    }
                    else if (userNumber != randomNumber)
                    {
                        string lessOrMore;
                        lessOrMore = userNumber > randomNumber ? "more" : "less";

                        Console.WriteLine($"Ur answer {lessOrMore} then need");
                        userTries[totalTries] = lessOrMore;
                        totalTries++;
                        if (totalTries % 4 == 0)
                        {
                            Console.WriteLine(humilition[random.Next(0, 3)], username);
                        }

                    }
                }
                else if (readLine == "q")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Enter q or number");
                }
            }
        }
    }
}
