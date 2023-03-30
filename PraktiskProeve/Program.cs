using System;

namespace PraktiskProeve;
class Program
{
    //Path addrresen er for en mac, hvis ikke den virker til windows, skal path variablen ændres til "C:/Users/" + "Environment.UserName + "/Desktop/fodbold.txt";
    public static string path = @"/Users/" + Environment.UserName + "/Desktop/fodbold.txt";
    static void Main(string[] args)
    {
        //variabler
        const int børnePris = 30, voksenPris = 65;
        const double usdKurs = 625.45;
        int antalVoksenBilletter = 0, antalBørneBilletter = 0;
        double totalpris, rabat = 0;
        bool godkendt = false;
        string medlem = "";

        //Opretter fil, hvis den ikke findes
        if (!File.Exists(path)) File.WriteAllText(path, "500");

        Title();    

        Console.WriteLine("Maks 10 af hver billettype");
        Console.WriteLine("Der er {0} pladser tilbage\n", GetPladser());
        Console.WriteLine("Priser for billetter:");
        Console.WriteLine($"Børnebillet: {børnePris}");
        Console.WriteLine($"Voksenbillet: {voksenPris}\n");
        Console.WriteLine("Tryk en tast for at købe billetter");
       

        Console.ReadKey();

        //Opretter fil hvis fil ikke ekistere allerede
        if (!File.Exists(path)) File.WriteAllText(path, "500");

        /*do while hvor den afventer userinput på
        hvor mange børnebilleter brugeren ønsker
        Den tjekker efter om man maks ønsker at købe 10
        og om man har indtaset et tal - hvis ikke begge er overholdt beder den en om at prøve igen
        */
        do
        {
            do
            {
                Console.Clear();
                Title();

                Console.Write("Indtast hvor mange børnebilletter du ønsker: ");
                try
                {
                    antalBørneBilletter = int.Parse(Console.ReadLine());
                    godkendt = true;
                }
                catch
                {
                    Console.WriteLine("Du kan kun indtaste tal - Tryk en tast for at prøve igen!");
                    godkendt = false;
                    Console.ReadKey();

                }
                if (antalBørneBilletter > 10 && godkendt)
                {
                    Console.WriteLine("Du kan maks købe 10 børnebilletter - Tryk en tast for at prøve igen!");
                    godkendt = false;
                    Console.ReadKey();
                }

            } while (antalBørneBilletter > 10 || !godkendt);

            //Gør det samme som do whilen ovenfor - Dog med voksenbilletter i stedet
            godkendt = false;
            do
            {
                Console.Clear();
                Title();

                Console.Write("Indtast hvor mange voksenbilletter du ønsker: ");
                try
                {
                    antalVoksenBilletter = int.Parse(Console.ReadLine());
                    godkendt = true;
                }
                catch
                {
                    Console.WriteLine("Du kan kun indtaste tal - Tryk en tast for at prøve igen!");
                    godkendt = false;
                    Console.ReadKey();

                }
                if (antalVoksenBilletter > 10 && godkendt)
                {
                    Console.WriteLine("Du kan maks købe 10 voksenbilletter - Tryk en tast for at prøve igen!");
                    godkendt = false;
                    Console.ReadKey();
                }


            } while (antalBørneBilletter > 10 || !godkendt);

            Console.Clear();
            Title();

            totalpris = antalBørneBilletter * børnePris + antalVoksenBilletter * voksenPris;

            //Tjekker om der er pladser nok
            if (antalBørneBilletter + antalVoksenBilletter > GetPladser())
            {
                Console.WriteLine("Du har valgt for mange billetter i forhold til, hvor mange pladser der er tilbage");
                Console.WriteLine("Hvis du ønsker at rette i dit køb, så tryk på en tast");

            }
            //Hvis der er plads udregner den pris og spørger om man vil rette i købet, eller om man vil fuldføre
            else
            {

                //Spørger om man er medlem, ved at afvente userinput ja eller nej
                godkendt = false;
                do
                {
                    Console.Clear();
                    Title();
                    Console.WriteLine("Du ønsker at købe:");
                    Console.WriteLine("{0} børnebilleter", antalBørneBilletter);
                    Console.WriteLine("{0} voksenbilletter", antalVoksenBilletter);
                    Console.WriteLine("\nPris:");
                    Console.WriteLine("Totalpris: {0:N2} kr. (${1})", totalpris, Math.Round(totalpris / (usdKurs / 100)));
                    Console.Write("\nEr du medlem af klubbens foreningsgruppe (Ja/Nej): ");
                    medlem = Console.ReadLine().ToLower();
                    if (medlem == "ja" || medlem == "j")
                    {
                        medlem = "ja";
                    }
                    else if (medlem == "nej" || medlem == "n")
                    {
                        medlem = "nej";
                    }
                    else
                    {
                        medlem = "";
                        Console.Write("Brug venligst kun ja eller nej - Tryk en tast for at prøve igen!");
                        Console.ReadKey();
                    }

                } while (medlem == "");
                if (medlem == "ja")
                {
                    rabat = totalpris * 0.10;
                    Console.WriteLine("Rabat: {0:N2} kr. (${1})", rabat, Math.Round(rabat / (usdKurs / 100)));            
                    Console.WriteLine("Totalpris m. rabat: {0:N2} kr. (${1})", totalpris - rabat, Math.Round((totalpris - rabat) / (usdKurs / 100)));

                }
                Console.WriteLine("\nHvis du ønsker at rette ved at starte forfra tryk R");
                Console.WriteLine("Ønsker du at fuldføre dit køb tryk en tast");
            }

        } while (Console.ReadKey(true).Key == ConsoleKey.R || antalBørneBilletter + antalVoksenBilletter > GetPladser());

        //Fuldføre købet og udskriver priser og billetter købt
        Console.Clear();
        Title();

        Console.WriteLine("Kvittering");
        Console.WriteLine("{0} børnebilleter", antalBørneBilletter);
        Console.WriteLine("{0} voksenbilletter", antalVoksenBilletter);
        Console.WriteLine("\nEr du medlem: {0}", medlem);
        Console.WriteLine("\nPris:");
        if (medlem == "ja")
        {
            Console.WriteLine("Rabat: {0:N2} kr. (${1})", rabat, Math.Round(rabat / (usdKurs / 100)));
            Console.WriteLine("Pris u. rabat: {0:N2} kr. (${1})", totalpris, Math.Round(totalpris / (usdKurs / 100)));
            Console.WriteLine("Totalpris: {0:N2} kr. (${1})", totalpris - rabat, Math.Round((totalpris - rabat) / (usdKurs / 100)));
        }
        else
        {
            Console.WriteLine("Totalpris: {0:N2} kr. (${1})", totalpris, Math.Round(totalpris / (usdKurs / 100)));
        }
        //Opdatere pladser og pauser
        UpdatePladser(GetPladser() - (antalBørneBilletter + antalVoksenBilletter));
        Console.Read();

       
    }

    //Metode til at sende titlen med rød skrift og gul baggrund
    public static void Title() {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.BackgroundColor = ConsoleColor.Yellow;

        //Sætter teksten i midten
        Console.WriteLine("Fodboldklubben TEC - Billet salg");
        Console.ResetColor();

        //Tilføjer d.d til højre h
        Console.SetCursorPosition(Console.WindowWidth - 10, 0);
        Console.WriteLine(DateTime.Today.ToString("dd/MM/yyyy"));
    }

    //Metode til at få, hvor mange pladser der tilbage
    public static int GetPladser()
    {
        int pladser = 0;
        try
        {
            pladser = int.Parse(File.ReadAllText(path));
        } catch {
            Console.Write("Der er sket en fejl i fodbold.txt - Slet filen og prøv igen");
        }
        return pladser;
    }

    //Metode til at opdatere pladser tilbage
    public static void UpdatePladser(int pladserTilbage) {

        File.WriteAllText(path, pladserTilbage.ToString());
    }
}

