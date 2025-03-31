class Program
{
    static void Main(string[] args)
    {
        // Create && Add person
        Person person1 = new Person("Hi");
        Person person2 = new Person("Juliusz");
        Person person3 = new Person("Hello");
        Person person4 = new Person("Too long String");
        
        Person.AddPersons(person1, person2, person3, person4); // EXCEPTION: ATRYBUTY // Eception of limit 10 letters string
        
        // Draw table of persons
        Console.WriteLine(Person.DrawTablePerson()); 
        
        // Create new Group
        OrderedGroup group = new OrderedGroup("MyGroup");
        
        Person person5 = new Person("Don't exist in Person"); // EXCEPTION: SUBSET // don't exist in person but will try to add it on group
        
        // Describing order
        group.AddMemberInOrder(person1, 0, Position.Pracownik); 
        group.AddMemberInOrder(person2, 1, Position.Pracownik);
        group.AddMemberInOrder(person3, 1, Position.Student);
        group.AddMemberInOrder(person4, 2, Position.Dyrektor);
        group.AddMemberInOrder(person5, 0, Position.Dyrektor); // EXCEPTION: SUBSET
        group.AddMember(person1); // Ograniczenie własne
        
        // Draw table in descripted order
        Console.WriteLine(group.DrawOrderedMembers());
        
        // EXCEPTION: XOR
        group.AddMemberInOrder(person3, Position.Pracownik);
        group.AddMemberInOrder(person1, Position.Student);
        group.AddMemberInOrder(person2, Position.Dyrektor);
        Console.WriteLine(group.DrawOrderedMembers());
    }
    
}
