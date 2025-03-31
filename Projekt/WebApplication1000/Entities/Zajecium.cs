using System;
using System.Collections.Generic;

namespace WebApplication1000.Entities;

public partial class Zajecium
{
    public int IdZajecia { get; set; }

    public string? Theme { get; set; }

    public TimeOnly Duration { get; set; }

    public DateTime StartZajecia { get; set; }

    public DateTime EndZajecia { get; set; }

    public int GrupaIdGrupa { get; set; }

    public int Type { get; set; }

    public int NumberAuditory { get; set; }

    public int PrzedmiotIdPrzedmiot { get; set; }

    public virtual Grupa GrupaIdGrupaNavigation { get; set; } = null!;

    public virtual Przedmiot PrzedmiotIdPrzedmiotNavigation { get; set; } = null!;
}
