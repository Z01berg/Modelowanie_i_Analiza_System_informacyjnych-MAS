using WebApplication1000.Entities;


public partial class Student
{
    public int OsobaSIndex { get; set; }
    public int? Itn { get; set; }
    public DateOnly YearOfStudy { get; set; }
    public string Password { get; set; } = null!;
    public virtual Osoba OsobaSIndexNavigation { get; set; } = null!;
    public virtual ICollection<Grupa> GrupaIdGrupas { get; set; } = new List<Grupa>();
}