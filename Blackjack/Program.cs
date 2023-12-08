// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel.Design;
using System.Security.Cryptography;

class Program
{

    static void Main()
    {
        int minPlayers = 1;
        int maxPlayers = 7;
        int num_players = 8;
        int active_count = 0;
        string[] playerNames = new string[num_players];
        int[] chips = new int[num_players];
        int[] bets = new int[num_players];
        int[] cards = new int[num_players];
        bool[] active_player = new bool[num_players];
        bool[] winner = new bool[num_players];
        bool[] bust = new bool[num_players];


        Console.Write("Skriv in mängden spelare (1 - 7 spelare): ");







        while (num_players > maxPlayers || num_players < minPlayers)

        {
            num_players = int.Parse(Console.ReadLine());

            if (num_players >= minPlayers && num_players <= maxPlayers)
            {
                Console.WriteLine("Du har valt " + num_players + " spelare");
                
            }


            else
            {
                Console.WriteLine("Nu vart det tokigt det måste vara 1-7 spelare, skriv in mändden spelare (1-7 st): ");
            }

        }

        

        bool playagain = true;
        for (int i = 0; i < num_players; i++)
        {
            Thread.Sleep(1000);
            Console.Write($"Skriv in namet för spelare {i + 1}: ");
            playerNames[i] = Console.ReadLine();
            chips[i] = 500;

        }
        Thread.Sleep(500);
        Console.WriteLine("Alla startar med chip mängden 500 ");
        Thread.Sleep(500);
        // Visar namn
        Console.WriteLine("namnen på spelarna :");
        for (int i = 0; i < num_players; i++)
        {
            Thread.Sleep(500);
            Console.WriteLine(playerNames[i]);
        }

        // kollar hur mycket det vill satsa
        Thread.Sleep(500);
        Console.WriteLine("Nu är vi reado att spela, hur mycket vill ni satsa? ");
        Thread.Sleep(500);
        while (playagain == true)
        {
            active_count = num_players;
            for (int i = 0; i < num_players; i++)
            {
                bust[i] = false;
                if (chips[i] > 0)
                {
                    Console.Write("hur mycket vill " + playerNames[i] + " satsa? du har " + chips[i] + " chips: ");
                    bets[i] = int.Parse(Console.ReadLine());
                }
                else if (chips[i] <1)
                {
                    Console.WriteLine(" Du har inga pengar kvar, här ta ett sms lån på 500 chips" + "(du har " + chips[i] + " chips");
                    bets[i] = 500;

                }

                if (bets[i] > chips[i] && chips[i] > 0)
                {
                    while (bets[i] > chips[i])
                    {
                        Console.WriteLine("Nää det där kan du inte satsa, sataa det du faktiskt har!");
                        Console.Write("hur mycket vill " + playerNames[i] + " satsa?: ");
                        bets[i] = int.Parse(Console.ReadLine());
                    }
                }

                chips[i] = chips[i] - bets[i];
                Thread.Sleep(500);
            }

            Console.WriteLine("nu kör vi!");


            //sätter i gång spelet
            bool game_status = true;

            Random random = new Random();
            for (int i = 0; i < num_players; i++)
            {
                active_player[i] = true;
                int x = random.Next(1, 12);
                cards[i] = x;
                Console.WriteLine(playerNames[i] + " du har " + cards[i]);
                Thread.Sleep(2000);
            }

            Console.WriteLine("Och dealern har!");
            Thread.Sleep(3000);
            int dealer = random.Next(1, 12);
            Console.WriteLine(dealer);
            Thread.Sleep(300);
            Console.WriteLine("Nu får ni ett till kort!");
            // frågar om de vill ta eller stanan
            for (int i = 0; i < num_players; i++)
            {
                Thread.Sleep(1000);
                cards[i] = cards[i] + random.Next(1, 12);
                Console.WriteLine(playerNames[i] + " du har nu " + cards[i]);

                if (cards[i] > 21)
                {
                    Console.WriteLine("Ajdå " + playerNames[i] + " du gick bust");
                    active_player[i] = false;
                    winner[i] = false;
                    bust[i] = true;
                    active_count = active_count - 1;

                }
                else if (cards[i] == 21)
                {

                    active_player[i] = false;
                    winner[i] = true;
                    Console.WriteLine(" Grattis du Vann och får " + bets[i] * 2);
                    chips[i] = chips[i] + bets[i] * 2;
                    active_count=active_count - 1;

                }
            }

            bool[] hit = new bool[num_players];
           

            for (int i = 0; i < num_players; i++)
            {
                hit[i] = true;
            }



            while (game_status == true)
            {

                

                for (int i = 0; i < num_players; i++)
                {
            
                    Thread.Sleep(1000);
                    
                    
                    
                    if (active_player[i] == true && hit[i] == true)
                    {
                     
                        Console.WriteLine(playerNames[i] + " du har " + cards[i] + " vill du stanna eller ta kort eller stanna? (1 för ta 0 för stanna)");

                        string answer = "";    // checkar för gilttigt svar och kollar om spelarn vill ta eller stanna.

                    
                        answer = Console.ReadLine();
                        answer.ToLower();
                        if (answer == "1")
                        {
                            hit[i] = true;
                            cards[i] = cards[i] + random.Next(1, 12);
                            Console.WriteLine(playerNames[i] + " du fick " + cards[i]);
                            if (cards[i] > 21)
                            {
                                Console.WriteLine("Adjå du gick bust");
                                active_player[i] = false;
                                winner[i] = false;
                                bust[i] = true;
                                active_count = active_count - 1;
                            }

                        }
                        else if (answer == "0")
                        {
                            hit[i] = false;
                            Console.WriteLine(playerNames[i] + " stannar på " + cards[i]);

                            active_count = active_count - 1;
                        }
                        else
                        {
                            Console.WriteLine("Nu vart det något tåkig");



                        }
                        if (cards[i] == 21)
                        {
                            active_player[i] = false;
                            hit[i] = false;
                            Console.WriteLine(playerNames[i] + " har 21 så du stannar automatiskt");
                            active_count = active_count - 1;


                        }

                    }


                }



                if (active_count == 0)
                {
                    game_status = false;

                }
            }
            dealer = random.Next(14, 24); //riggad dealer
            Console.WriteLine("Och delaern har!!! " + dealer);
            Thread.Sleep(1000);
            for (int i = 0; i < num_players; i++)
            {
                Thread.Sleep(1000);

                //kollar om spelarn van eller förlora
                if (winner[i] != true && cards[i] > dealer && cards[i] < 22 || dealer > 21 && cards[i] < 22)
                {
                    Console.WriteLine("Grattis " + playerNames[i] + " du vann och får " + 2 * bets[i]);
                    chips[i] = chips[i] + 2 * bets[i];
                }
                else if (dealer == cards[i] && winner[i] != true && bust[i] != true)
                {
                    Console.WriteLine("Nu vart det lika " + playerNames[i] + "! du får tillbaka dina chip" + "(" + bets[i] + ")" + " chips");
                    chips[i] = chips[i] + bets[i];
                }
                else if (winner[i] == true)
                {
                    Console.WriteLine(playerNames[i] + " Du vann ju också och har redan fått " + 2 * bets[i] + " Chips");
                }
                else
                {
                    Console.WriteLine(playerNames[i]+ " du förlora :(");
                }
            }
            Thread.Sleep(1000);
            Console.WriteLine("vill ni köra igen? (skriv: ja eller nej");
            string playagain_ans = "";
            
             // kollar om spelaren vill köra igen
            while (playagain_ans != "ja")   
            {

                playagain_ans = playagain_ans.ToLower();
                playagain_ans = Console.ReadLine();
                if (playagain_ans == "ja")
                {
                    playagain = true;
                    Console.WriteLine("Då kör vi igen! (skriv ja om du vill eller nej om du inte vill forsätta");
                }
                else if (playagain_ans == "nej")
                {
                    Console.WriteLine("Okej då avslutar vi här,tack för ni spela!!!");
                    playagain = false;
                    Thread.Sleep(500);
                    for (int i = 0; i < num_players; i++)
                    {
                        Console.WriteLine(playerNames[i] + " du slutar med " + chips[i] + " chips");
                        Thread.Sleep(500);


                    }
                    Environment.Exit(0);



                }
                else
                {
                    Console.WriteLine("Nu var det konstigt prova en gång till");
                }

            }

        }
    }
}
 
