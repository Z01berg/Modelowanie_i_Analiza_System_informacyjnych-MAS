using System;
using System.Collections.Generic;

namespace WebApplication1000.Entities;

public partial class Osoba
{
    public int SIndex { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public virtual Dydaktyk? Dydaktyk { get; set; }

    public virtual Dyrektor? Dyrektor { get; set; }

    public virtual Student? Student { get; set; }
}
