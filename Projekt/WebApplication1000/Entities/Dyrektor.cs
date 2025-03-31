using System;
using System.Collections.Generic;

namespace WebApplication1000.Entities;

public partial class Dyrektor
{
    public int Year { get; set; }

    public int OsobaSIndex { get; set; }

    public string Password { get; set; } = null!;

    public virtual Osoba OsobaSIndexNavigation { get; set; } = null!;
}
