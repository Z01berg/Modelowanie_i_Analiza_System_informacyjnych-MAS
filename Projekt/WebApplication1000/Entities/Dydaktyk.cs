using System;
using System.Collections.Generic;

namespace WebApplication1000.Entities;

public partial class Dydaktyk
{
    public string MagisterDegree { get; set; } = null!;
    public int OsobaSIndex { get; set; }
    public string Password { get; set; } = null!;
    public virtual Osoba OsobaSIndexNavigation { get; set; } = null!;
    public virtual ICollection<Grupa> GrupaIdGrupas { get; set; } = new List<Grupa>();
}

