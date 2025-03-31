public class OrderedGroup : Group
{
    /* For BAG*/
    static Type classType = typeof(OrderedGroup);
    
    public List<Person> _orderedMembers = new List<Person>();

    public OrderedGroup(string id) : base(id)
    {
    }

    public void AddMemberInOrder(Person person, Position role)
    {
        if (PersonExists(person))
        {
            AssignRole(person, role);
        }
    }
    public void AddMemberInOrder(Person person, int index, Position role) // EXCEPTION: ORDERED && SUBSET
    {
        if (index >= 0 && index <= _orderedMembers.Count) 
        {
            if (PersonExists(person))
            {
                _orderedMembers.Insert(index, person);
                AssignRole(person, role);
            }
            else
            {
                Logger.LogException($"SUBSET {classType.Name}: Person with ID '{person.Id}' does not exist in the database."); // BAG
                Console.WriteLine($"EXCEPTION {classType.Name}: Person with ID '{person.Id}' does not exist in the database."); // EXCEPTION: SUBSET
                //throw new Exception("Person with ID does not exist in the database.");
            }
        }
        else
        {
            Logger.LogException($"EXCEPTION {classType.Name}: Invalid index specified: {index}"); // BAG
            Console.WriteLine($"EXCEPTION {classType.Name}: Invalid index specified: {index}"); // Ograniczenie własne??
            //throw new Exception("Invalid index specified.");
        }
    }
    
    public override void AddMember(Person person)
    {
        Logger.LogException($"EXCEPTION {classType.Name}: Cannot add members directly to an ordered group without specifying the index."); // BAG
        Console.WriteLine($"EXCEPTION {classType.Name}: Cannot add members directly to an ordered group without specifying the index."); // Ograniczenie własne??
    }
    
    public void AssignRole(Person person, Position newRole) // EXCEPTION: XOR
    {
        if (person.Role != Position.Student && person.Role != Position.Dyrektor && person.Role != Position.Pracownik
            || person.Role == Position.Student 
            || newRole == Position.Student)
        {
            person.Role = newRole;
        }
        else if (person.Role == Position.Dyrektor || person.Role == Position.Pracownik)
        {
            Logger.LogException($"EXCEPTION {classType.Name}: Person '{person.Id} | {person.Name}' already has a role assigned."); // BAG
            Console.WriteLine($"EXCEPTION {classType.Name}: Person '{person.Id} | {person.Name}' already has a role assigned."); // EXCEPTION: XOR
        }
        else
        {
            person.Role = newRole;
        }
    }
    
    public string DrawOrderedMembers()
    {
        string orderedMembersInfo = $"MEMBERS OF GROUP '{Id}' IN ORDER:\n" +
                                    $"╔══════════════════════════════════════════════════════════════════════════╗\n";
        if (_orderedMembers.Count == 0)
        {
            orderedMembersInfo += $"                                 NOTHING HERE                               \n" +
                                  $"╚══════════════════════════════════════════════════════════════════════════╝\n\n";
        }
        else
        {
            for (int i = 0; i < _orderedMembers.Count; i++)
            {
                orderedMembersInfo += $"■ {i + 1} ▓ {_orderedMembers[i].Id} ▒ {_orderedMembers[i].Name} ░ {_orderedMembers[i].Role}\n";
                
                if (i == _orderedMembers.Count - 1)
                {
                    orderedMembersInfo += $"╚══════════════════════════════════════════════════════════════════════════╝\n\n";
                }
            }
        }
        
        return orderedMembersInfo;
    }
}