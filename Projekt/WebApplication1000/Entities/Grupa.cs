using System;
using System.Collections.Generic;

namespace WebApplication1000.Entities;

public partial class Grupa
{
    public int IdGrupa { get; set; }

    public int SemestrIdSemestr { get; set; }

    public virtual Semestr SemestrIdSemestrNavigation { get; set; } = null!;

    public virtual ICollection<Zajecium> Zajecia { get; set; } = new List<Zajecium>();

    public virtual ICollection<Dydaktyk> DydaktykOsobaSIndices { get; set; } = new List<Dydaktyk>();

    public virtual ICollection<Student> StudentOsobaSIndices { get; set; } = new List<Student>();
}
