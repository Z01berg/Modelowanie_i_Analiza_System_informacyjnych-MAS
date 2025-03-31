public abstract class Group
{
    /* For BAG*/
    static Type classType = typeof(Group);
    
    public string Id { get; }
    public List<Person> Members { get; } = new List<Person>();
    
    public Group(string id)
    {
        Id = id;
    }

    public virtual void AddMember(Person person) // EXCEPTION: OLD VIRTUAL SUBSET
    {
        if (PersonExists(person))
        {
            Members.Add(person);
        }
        else
        {   // Grupa nie może być wypewniona ludzmi których nie mamy w firmie
            Console.WriteLine($"EXCEPTION {classType.Name}: Person with ID '{person.Id}' does not exist in the database.");
            //throw new Exception("Person with ID does not exist in the database.");
        }
    }

    public bool PersonExists(Person person)
    {
        foreach (var p in Person._personDB)
        {
            if (p.Id == person.Id)
            {
                return true;
            }
        }

        return false;
    }
}