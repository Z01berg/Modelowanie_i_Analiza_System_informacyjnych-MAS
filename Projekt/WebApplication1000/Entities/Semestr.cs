using System;
using System.Collections.Generic;

namespace WebApplication1000.Entities;

public partial class Semestr
{
    public int IdSemestr { get; set; }

    public virtual ICollection<Grupa> Grupas { get; set; } = new List<Grupa>();

    public virtual ICollection<Przedmiot> PrzedmiotIdPrzedmiots { get; set; } = new List<Przedmiot>();
}
