public enum Sex_E
{
    Male,
    Female
}

public abstract class Osoba // Abstrakcyjna klasa
{
    public string Name { get; set; }
    public string SecondName { get; set; }
    public Sex_E Sex { get; set; }
    public string FemaleName;
    public int ManMilitary;

    // Dziedziczenie wieloaspektowe 
    public Osoba(string name, string secondName, string fName)
    {
        Name = name;
        SecondName = secondName;
        Sex = Sex_E.Female;
        FemaleName = fName;
    }

    public Osoba(string name, string secondName, int MMilitary)
    {
        Name = name;
        SecondName = secondName;
        Sex = Sex_E.Male;
        ManMilitary = MMilitary;
    }
    
    public void ZamienMiejsce(Osoba osoba)
    {
        string tempName = this.Name;
        string tempSecondName = this.SecondName;
        string tempFemaleName = this.FemaleName;
        int tempManMilitary = this.ManMilitary;
        Sex_E tempSex = this.Sex;

        this.Name = osoba.Name;
        this.SecondName = osoba.SecondName;
        this.FemaleName = osoba.FemaleName;
        this.ManMilitary = osoba.ManMilitary;
        this.Sex = osoba.Sex;

        osoba.Name = tempName;
        osoba.SecondName = tempSecondName;
        osoba.FemaleName = tempFemaleName;
        osoba.ManMilitary = tempManMilitary;
        osoba.Sex = tempSex;
    }

    public abstract void SayHello(); // Polimorfizm
    public string whoAmI()
    {
        return $"Jestem {Sex}.\n\n";
    }
}

// Wielodziedziczenie
public interface IStudjujeIPracuje
{
    void StudyAndWork(string nazwaPracy);
}

public class Student : Osoba, IStudjujeIPracuje
{
    public string Eska { get; set; }

    public override void SayHello() // Polimorfizm
    {
        Console.WriteLine($"Cześć! Jestem studentem o imieniu {Name} {SecondName}\nFNAME: {FemaleName} MILLITARY: {ManMilitary}.");
    }

    public void StudyAndWork(string nazwaPracy) // Wielodziedziczenie
    {
        Console.WriteLine($"Studiuję PJATK i pracuje w {nazwaPracy}");
    }

    public Student(string name, string secondName, string eska, string fName) : base(name, secondName, fName)
    {
        Eska = eska;
    }

    public Student(string name, string secondName, string eska, int MMilitary) : base(name, secondName, MMilitary)
    {
        Eska = eska;
    }
}

public class Pracownik : Osoba, IStudjujeIPracuje
{
    public string OwnedGroup { get; set; }

    public override void SayHello() // Polimorfizm
    {
        Console.WriteLine($"Witaj! Jestem pracownikiem o imieniu {Name} {SecondName}\nFNAME: {FemaleName} MILLITARY: {ManMilitary}.");
    }

    public void StudyAndWork(string nazwaPracy) // Wielodziedziczenie
    {
        Console.WriteLine($"Pracuję w {nazwaPracy} i studiuję PJATK");
    }
    
    public Dyrektor ZostanDyrektorem(int years) //TODO this one
    {
        if (this.Sex == Sex_E.Female)
        {
            return new Dyrektor(this.Name, this.SecondName, this.OwnedGroup, years, this.FemaleName);
        }
        else
        {
            return new Dyrektor(this.Name, this.SecondName, this.OwnedGroup, years, this.ManMilitary);
        }
    }


public Pracownik(string name, string secondName, string ownedGroup, string fName) : base(name, secondName, fName)
    {
        OwnedGroup = ownedGroup;
    }

    public Pracownik(string name, string secondName, string ownedGroup, int mMilitary) : base(name, secondName, mMilitary)
    {
        OwnedGroup = ownedGroup;
    }
}

public class Dyrektor : Pracownik
{
    public int Years { get; set; }

    public override void SayHello()
    {
        Console.WriteLine($"Dzień dobry! Jestem dyrektorem o imieniu {Name} {SecondName}\nFNAME: {FemaleName} MILLITARY: {ManMilitary}.\n");
    }

    public Dyrektor(string name, string secondName, string ownedGroup, int years, string fName) : base(name, secondName, ownedGroup, fName)
    {
        Years = years;
    }

    public Dyrektor(string name, string secondName, string ownedGroup, int years, int mMilitary) : base(name, secondName, ownedGroup, mMilitary)
    {
        Years = years;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Student student = new Student("Jan", "Kowalski", "ABC123", "Kowalska");
        student.SayHello();
        student.StudyAndWork("Domena Developerska");
        Console.WriteLine(student.whoAmI());

        Pracownik pracownik = new Pracownik("Anna", "Nowak", "Grupa A", "Kowalska");
        
        pracownik.SayHello();
        pracownik.StudyAndWork("PJATK");
        Console.WriteLine(pracownik.whoAmI());

        Dyrektor dyrektor = new Dyrektor("Adam", "Wiśniewski", "Grupa B", 5, 5);
        dyrektor.SayHello();
        Console.WriteLine(dyrektor.whoAmI());

        pracownik.ZostanDyrektorem(6);
        Console.WriteLine("Swapped Dyrektor");
        pracownik.SayHello();
        dyrektor.SayHello();
    }
}

