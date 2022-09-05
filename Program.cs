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
            
            Console.SetWindowSize(81, 24);
            Console.SetBufferSize(81, 24);
            Console.SetWindowPosition(0, 0);

            // Define the frequencies of notes in an octave

            const int A = 220;
            const int G = 392;

            // Define the duration of a note in units of milliseconds.

            const int WHOLE = 1600;
            const int HALF = WHOLE / 2;
            const int QUARTER = HALF / 2;
        
            Random rnd = new();

            const string menu = @"

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
                ------------------------------------------------
                                                                ";

            const string getReady = @"

                -----------------------|------------------------

                                   GET READY
                                   .........



                          _0                       O_ 
                         |/                         \|
                         /\                         /\
                        /  |                       |  \      
                -----------------------|------------------------
                ------------------------------------------------
                                                                ";

            const string fire = @"

                -----------------------|------------------------

                                     FIRE!
                                   .........
                                   [ space ]


                          _0                       O_ 
                         |/                         \|
                         /\                         /\
                        /  |                       |  \      
                -----------------------|------------------------
                ------------------------------------------------
                                                                ";

            const string loseTooSlow = @"
                
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
                      *** Press [space] to try again ***        ";

            const string loseTooFast = @"
                
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
                      *** Press [space] to try again ***        ";

            const string win = @"

                -----------------------|------------------------

                                *** YOU WIN ***
                                   .........



                         0 __⌐  <                         
                       / \                           \\
                         /\                       /\__/O
                        /  |                       /      
                -----------------------|------------------------
                ------------------------------------------------
                       *** Press [space] to continue ***        ";
                        
            while (true)
            {
                //start at the main menu and prompt start
                bool pAlive = false;
                int currentRound = 1;

                static void SetHeader(int _currentRound)
                    {
                    string header = "QUICKDRAW | ROUND " + _currentRound;
                    Console.SetCursorPosition(29, 2);
                    Console.WriteLine(header);
                    }

                static void SetReactionTime(double elapsedTime)
                {
                    string reactionTime = "REACTION TIME " + elapsedTime + "ms";
                    Console.SetCursorPosition(29, 2);
                    Console.WriteLine(reactionTime);
                }

                Console.Clear();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Gray;
                SetHeader(currentRound);                
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
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    SetHeader(currentRound);
                    Console.WriteLine(getReady);
                    Console.Beep(G, QUARTER);
                    cpuReactionTime = TimeSpan.FromMilliseconds(500 / currentRound);
                    readyDur = TimeSpan.FromMilliseconds(rnd.Next(1000, 5000));
                    double reactionTime;
                    Stopwatch stopwatch = new();
                    stopwatch.Reset();
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
                    Console.ForegroundColor = ConsoleColor.Gray;
                    SetHeader(currentRound);
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
                        Console.CursorVisible = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        SetHeader(currentRound);
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
                        Console.CursorVisible = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        SetHeader(currentRound);
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
                        Console.CursorVisible = false;
                        Console.ForegroundColor = ConsoleColor.Green;
                        SetHeader(currentRound);
                        Console.WriteLine(win);
                        reactionTime = stopwatch.Elapsed.TotalMilliseconds;
                        SetReactionTime(reactionTime);
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
        
    



