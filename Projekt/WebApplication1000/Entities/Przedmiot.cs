using System;
using System.Collections.Generic;

namespace WebApplication1000.Entities;

public partial class Przedmiot
{
    public int IdPrzedmiot { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Zajecium> Zajecia { get; set; } = new List<Zajecium>();

    public virtual ICollection<Semestr> SemestrIdSemestrs { get; set; } = new List<Semestr>();
}
