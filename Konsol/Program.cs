﻿using System;

bool run = true;
while (run)
{
    PrintMenu();
    string valg = Console.ReadLine();

    switch (valg)
    {
        case "1":
            // Kald GuessANumber
            var guessANumber = new Hjemmet.GuessANumber();
            guessANumber.Start();
            break;
        case "2":
            // Kald RockPaperScissors
            var rockPaperScissors = new Hjemmet.RockPaperScissors();
            rockPaperScissors.Start();
            break;
        case "3":
            // Kald TicTacToe
            var ticTacToe = new Hjemmet.TicTacToe();
            ticTacToe.Start();
            break;
        case "4":
            // Kald BinaryConverter
            var binaryConverter = new Kontoret.BinaryConverter();
            binaryConverter.Start();
            break;
        case "5":
            // Kald ADService
            var adService = Konsol.Enterprice.ADService.FromConfigFile();
            adService.Start();
            break;
        case "?":
            PrintMenu();
            break;
        case "exit":
            run = false;
            break;
        default:
            Console.WriteLine("Ugyldigt valg. Prøv igen.");
            Console.ReadKey();
            break;
    }
}

void PrintMenu()
{
    Console.Clear();
    Console.WriteLine("Vælg et program:");
    Console.WriteLine("1. Gæt et tal");
    Console.WriteLine("2. Sten, Saks, Papir");
    Console.WriteLine("3. Tic-Tac-Toe");
    Console.WriteLine("4. Binærkodeomformer");
    Console.WriteLine("5. AD Brugeroversigt");
    Console.WriteLine("?. Help");
    Console.WriteLine("exit. Afslut");
    Console.Write("Indtast valg: ");
}
