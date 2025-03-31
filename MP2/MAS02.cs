using System;
using System.Collections.Generic;

public class Klient
{
    public string Nazwa { get; private set; }
    public Dictionary<string, List<Paczka>> Paczki { get; private set; } = new Dictionary<string, List<Paczka>>();

    public Klient(string nazwa)
    {
        Nazwa = nazwa;
    }
    
    // Asocjacja Zwykła 
    public void DodajPaczke(Paczka paczka, string priorytet = "")
    {
        if (!Paczki.ContainsKey(priorytet))
        {
            Paczki[priorytet] = new List<Paczka>();
        }
        Paczki[priorytet].Add(paczka);
        paczka.PrzypiszKlienta(this);
    }
    
    // Asocjacja Z atrybutem
    public void DodajPaczke(string name, string opis, string priorytet = "")
    {
        Paczka paczka = new Paczka(name, priorytet, opis);
        DodajPaczke(paczka, priorytet);
    }

    // Asocjacja Kwalifikowana
    public void DodajPaczke(string name, string priorytet)
    {
        Paczka paczka = new Paczka(name, priorytet);
        DodajPaczke(paczka, priorytet);
    }
    
    // Wyrzucenie klienta
    public void WyrzucKlienta()
    {
        foreach (var priorytet in Paczki.Keys)
        {
            foreach (Paczka paczka in Paczki[priorytet])
            {
                // Usuń klienta z paczki
                paczka.UsunKlienta();
            }
        }
        // Wyczyść listę paczek klienta
        Paczki.Clear();
    }
}

public class Paczka
{
    public string Nazwa { get; private set; }
    public string Opis { get; private set; }
    public string Priorytet { get; private set; }
    public Klient Klient { get; private set; }

    public Paczka(string nazwa, string priorytet, string opis = "")
    {
        Nazwa = nazwa;
        Priorytet = priorytet;
        Opis = opis;
    }

    // Powiązanie zwrotne
    public void PrzypiszKlienta(Klient klient)
    {
        Klient = klient;
    }
    
    // Wyrzucenie klienta
    public void UsunKlienta()
    {
        Klient = null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Klient klient = new Klient("Jan Kowalski");
        Klient klient2 = new Klient("Juliusz Kowalski");
        
        // Asocjacja Kwalifikowana
        klient.DodajPaczke("Paczka 1", "Ta paczka jest bardzo ważna", "wysoki");
        klient.DodajPaczke("Paczka 2", "wysoki");
        klient.DodajPaczke("Paczka 3", "niski");
        
        klient2.DodajPaczke("Paczka 1", "Ta paczka jest nieważna", "wysoki");
        klient2.DodajPaczke("Paczka 2", "wysoki");
        klient2.DodajPaczke("Paczka 3", opis:"Happy Birthday");

        pokazPaczki(klient);
        pokazPaczki(klient2);
        
        klient.WyrzucKlienta();
        pokazPaczki(klient);
        
        // Decoration
        void pokazPaczki(Klient klient)
        {
            Console.WriteLine($"{ColorText($"{klient.Nazwa}", ConsoleColor.Red)} posiada paczki:");
            foreach (var priorytet in klient.Paczki.Keys)
            {
                if (priorytet == "")
                {
                    Console.WriteLine($"* Paczki bez pryriotetu:");
                }
                else
                {
                    Console.WriteLine($"* Paczki o priorytecie {priorytet}:");
                }
            
                foreach (Paczka paczka in klient.Paczki[priorytet])
                {
                    Console.WriteLine($"  - {paczka.Nazwa}");
                    if (paczka.Opis != "")
                    {
                        Console.WriteLine($"      ▓{paczka.Opis}");
                    }
                }
            }
        }
        
        static string ColorText(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = oldColor;

            return null;
        }
    }
}

