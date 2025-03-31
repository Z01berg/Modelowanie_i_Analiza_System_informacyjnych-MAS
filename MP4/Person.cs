public class Person
{
    /* For BAG*/
    static Type classType = typeof(Person);
    
    /* Database with Persons */
    public static List<Person> _personDB = new List<Person>();
    
    /* Constructor Person */
    public string Id { get; }

    private string _name;
    public string Name // EXCEPTION: ATRYBUTY
    {
        get { return _name;}
        set
        {
            if (value.Length > 10)
            {
                Logger.LogException($"ATRYBUTY {GetType().Name}: Too Long String found: {Id} "); // BAG
                Console.WriteLine($"EXCEPTION {classType.Name}: Too Long String found: {Id} ");
                //throw new ArgumentOutOfRangeException(nameof(value), "Name cannot be longer than 10 characters.");
            }
            _name = value;
        }
    }

    public Position Role { get; set; } // FOR XOR

    public Person(string name)
    {
        Id = ReturnID();
        Name = name;
    }
    
    public static void AddPersons(params Person[] persons)
    {
        foreach (var p in persons)
        {
            _personDB.Add(p);
        }
    }
    
    /* Show DB */
    public static string DrawTablePerson()
    {
        string a = "PERSONS DB:\n" +
                   "╔══════════════════════════════════════════════════════════════════════════╗\n";

        if (_personDB.Count == 0)
        {
            a += $"                                 NOTHING HERE                               \n" +
                 $"╚══════════════════════════════════════════════════════════════════════════╝\n\n";
        }
        else
        {
            foreach (var person in _personDB)
            {
                a += $"■ {person.Id} ▓ {person.Name} \n";
                
                if (_personDB.IndexOf(person) == _personDB.Count - 1)
                {
                    a += $"╚══════════════════════════════════════════════════════════════════════════╝\n\n";
                }
            }
        }

        return a;
    }
    
    /* Unique ID Generator */
    public static string GenerateID()
    {
        Guid guid = Guid.NewGuid();

        string guidString = guid.ToString("N");
        string firstFiveChars = guidString.Substring(0, 5);

        string code = "";
        foreach (char c in firstFiveChars)
        {
            if (char.IsLetter(c))
            {
                int unicodeValue = (int)c;
                int digitalValue = unicodeValue % 10;
                code += digitalValue.ToString();
            }
            else
            {
                code += c;
            }
        }

        return code;
    }

    public static bool CheckFalseID(string id) // EXCEPTION: UNIQUE
    {
        foreach (var person in _personDB)
        {
            if (person.Id == id)
            {
                Logger.LogException($"UNIQUE {classType.Name}: Duplicate ID found: {id}"); // BAG
                Console.WriteLine($"EXCEPTION {classType.Name}: Duplicate ID found: {id}");
                //throw new Exception("Duplicate ID found.");
                return false;
            }
        }

        return true;
    }
    
    public static string ReturnID()
    {
        string id;

        do
        {
            id = GenerateID();
        } while (!CheckFalseID(id));

        return id;
    }
}