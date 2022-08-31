// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
using System.Threading;



namespace QuickDraw
{

[System.Runtime.Versioning.SupportedOSPlatform("windows")]

    class Program
    {

        static void Main()
        {

            //TODO: best reaction times. formatting improvements

            // Define the frequencies of notes in an octave, as well as
            // silence (rest).

            const int A = 220;
            const int G = 392;

            // Define the duration of a note in units of milliseconds.

            const int WHOLE = 1600;
            const int HALF = WHOLE / 2;
            const int QUARTER = HALF / 2;
        
            Random rnd = new();

            const string menu = @"
            
                Quick Draw
                -----------------------|------------------------                    
                    Wait for the signal and press [spacebar] to 
                    shoot your opponent before they shoot you. 
                                    .........
                           [ press [space] to start ]                                                            
                                   
                                                                        
                          _0                       O_ 
                         |/                         \|
                         /\                         /\
                        /  |                       |  \      
                -----------------------|------------------------
                ------------------------------------------------";

            const string getReady = @"

                Quick Draw
                -----------------------|------------------------
                     
                                    GET READY
                                    .........
                 

                                                                        
                          _0                       O_ 
                         |/                         \|
                         /\                         /\
                        /  |                       |  \      
                -----------------------|------------------------
                ------------------------------------------------";

            const string fire = @"

                Quick Draw
                -----------------------|------------------------
                     
                                     FIRE!
                                   .........
                                   [ space ]                                                            

                                                                        
                          _0                       O_ 
                         |/                         \|
                         /\                         /\
                        /  |                       |  \      
                -----------------------|------------------------
                ------------------------------------------------";

            const string loseTooSlow = @"

                Quick Draw
                -----------------------|------------------------
                     
                                   TOO SLOW!
                                   .........
                                [ you're dead ]                                                            

                                                                        
                                               >  ¬__ O 
                         //                           / \
                        0/__/\                       /\
                            \                       |  \      
                -----------------------|------------------------
                ------------------------------------------------
                      *** Press [space] to try again ***";

            const string loseTooFast = @"

                Quick Draw
                -----------------------|------------------------
                     
                              TOO FAST, YOU MISSED!
                                   .........
                                [ you're dead ]                                                            

                                                                        
                                               >  ¬__ O 
                         //                           / \
                        0/__/\                       /\
                            \                       |  \      
                -----------------------|------------------------
                ------------------------------------------------
                      *** Press [space] to try again ***";

            const string win = @"

                Quick Draw
                -----------------------|------------------------
                     
                                *** YOU WIN ***
                                   .........


                                                                        
                         0 __⌐  <                         
                       / \                           \\
                         /\                       /\__/O
                        /  |                       /      
                -----------------------|------------------------
                ------------------------------------------------
                       *** Press [space] to continue ***";
                        
            while (true)
            {
                //start at the main menu and prompt start
                bool pAlive = false;
                int currentRound = 1;

                Console.Clear();
                Console.WriteLine(menu);
                Console.Beep(G, QUARTER);
                TimeSpan? cpuReactionTime;
                TimeSpan? readyDur;

                while (pAlive == false) 
                {
                    Console.CursorVisible = false;
                    if (Console.KeyAvailable && Console.ReadKey(true).Key is ConsoleKey.Spacebar)
                    {
                        pAlive = true;
                    }
                }

                //start round            
                while (pAlive == true)
                {
                    Console.Clear();
                    cpuReactionTime = TimeSpan.FromMilliseconds(2000 / currentRound);
                    readyDur = TimeSpan.FromMilliseconds(rnd.Next(1000, 5000));
                    Console.WriteLine(getReady);
                    Console.WriteLine("ROUND " + currentRound);
                    Console.Beep(G, QUARTER);
                    Stopwatch stopwatch = new();
                    stopwatch.Restart();
                    bool tooFast = false;

                    while (stopwatch.Elapsed < readyDur && !tooFast)
                    {
                        if (Console.KeyAvailable && Console.ReadKey(true).Key is ConsoleKey.Spacebar)
                        {
                            tooFast = true;
                        }
                    }

                    Console.Clear();
                    Console.CursorVisible = false;
                    Console.WriteLine(fire);
                    Console.Beep(A, QUARTER);
                    stopwatch.Restart();
                    bool tooSlow = true;

                    while (!tooFast && stopwatch.Elapsed < cpuReactionTime && tooSlow)
                    {
                        if (Console.KeyAvailable && Console.ReadKey(true).Key is ConsoleKey.Spacebar)
                        {
                            tooSlow = false;
                        }
                    }
                    if (tooFast == true)
                    {
                        Console.Clear();
                        Console.WriteLine(loseTooFast);
                        Console.Beep(A, WHOLE);
                        if (Console.ReadKey(true).Key is ConsoleKey.Spacebar)
                        {
                            break;
                        }
                    }
                    else if (tooSlow == true)
                    {
                        Console.Clear();
                        Console.WriteLine(loseTooSlow);
                        Console.Beep(A, WHOLE);
                        if (Console.ReadKey(true).Key is ConsoleKey.Spacebar)
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(win);
                        Console.Beep(G, HALF);
                        if (Console.ReadKey(true).Key is ConsoleKey.Spacebar)
                        {
                            currentRound++;
                        }
                    }
                }
            }

        }
    }
}
        
    



