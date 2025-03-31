using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ConsoleApp1
{
    public class MP01
    {
        /* Ekstensja */
        // Lista ze wszystkimi instancjami klasy
        private static List<MP01> DB = new List<MP01>();

        //Trwałość Zapis
        public static void safeToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(DB);
                File.WriteAllText("./ITN.json", json);
                Console.WriteLine("Dane zapisane");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Bląd zapisu: \n{e}");
            }
        }

        //Trwałość Odczyt
        public static void getFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader("./ITN.json"))
                {
                    string json = sr.ReadToEnd();
                    DB = JsonSerializer.Deserialize<List<MP01>>(json);
                }
                Console.WriteLine("Dane wczytane");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Bląd odczytu: \n{e}");
            }
        }

        
        /* Atrybuty */
        //Prosty
        public int Id { get; set; }
        //Złożony
        public string Eska { get; set; }
        public string Group { get; set; }
        //Opcjonalny
        public bool Itn { get; set; }
        //Powtarzalny
        public List<string> Groups { get; set; } = new List<string>();

        //Klasowy
        public static int StudentsCount
        {
            get { return DB.Count; }
        }

        
        /*Konstruktor*/
        public MP01(int id, string eska, string group, bool itn = false)
        {
            Id = id;
            Eska = eska;
            Group = group;
            Itn = itn;
            DB.Add(this);
        }

        
        /*METODY*/
        //Klasowa
        public static void WriteOutAllStudents()
        {
            Console.WriteLine($"░░░▒▒▒▓▓▓▓ Wszystkich studentów: {StudentsCount} ▓▓▓▓▒▒▒░░░");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■ LISTA ■■■■■■■■■■■■■■■■■■■■");
            foreach (var mp01 in DB)
            {
                Console.WriteLine($"ID: {mp01.Id}, Eska: {mp01.Eska}, Grupa: {mp01.Group}, ITN: {mp01.Itn}");
            }
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n\n");
        }
        
        //Pochodna
        public void ChooseYourStudent()
        {
            Console.WriteLine($"Wybrałeś studenta o ID: {Id} i Escce: {Eska} tu jego informacja\n▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
        }
        
        public string FullName
        {
            get { return $"{Eska} - {Group}"; }
        }

        //Przesłonięta
        public override string ToString()
        {
            return $"ID: {Id}, Eska: {Eska}, Grupa: {Group}, ITN: {Itn}\n▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓\n\n";
        }

        //Przeciążenie
        public void SubmitAccept()
        {
            Console.WriteLine($"Student o ID: {Id} i Esce: {Eska}\nZDAJE.\n\n");
        }

        public void SubmitAccept(string argument)
        {
            Console.WriteLine($"Student o ID: {Id} i Esce: {Eska}\nZDAJE, ponieważ:\n\"{argument}\"\n\n");
        }

        public void SubmitDeny()
        {
            Console.WriteLine($"Student o ID: {Id} i Esce: {Eska}\nNIEZDAJE.\n\n");
        }

        public void SubmitDeny(string argument)
        {
            Console.WriteLine($"Student o ID: {Id} i Esce: {Eska}\nNIEZDAJE, ponieważ:\n\"{argument}\"\n\n");
        }
    }

    class MAS01
    {
        static void Main(string[] args)
        {
            MP01 s1 = new MP01(1, "s234566", "28C");
            MP01 s2 = new MP01(2, "s539866", "21C");
            MP01 s3 = new MP01(3, "s133266", "20C", true);
            MP01 s4 = new MP01(4, "s773566", "18C");

            /*Metody*/
            //klasowa
            MP01.WriteOutAllStudents();

            //pochodna
            s1.ChooseYourStudent();
            Console.WriteLine(s1.FullName);

            //przesłonęcie
            Console.WriteLine(s1);

            //przeciążenie
            s1.SubmitAccept();
            s2.SubmitAccept("Jest bardzo dobrym studentem pozdrawiam");
            s4.SubmitDeny();
            s3.SubmitDeny("Student bober nie pomożemy");

            //trwałość ekstensji
            MP01.getFromFile();
            MP01.WriteOutAllStudents();
            MP01.safeToFile();
        }
    }
}
