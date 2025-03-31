# MP03 - Inheritance Models Implementation

Implementation of various inheritance models in a university personnel system.

## Implemented Concepts
- **Abstract Class**: `Osoba` base class
- **Polymorphism**: Abstract `SayHello()` method
- **Multiple Inheritance**: `IStudjujeIPracuje` interface
- **Multi-aspect Inheritance**: Gender-specific constructors
- **Dynamic Inheritance**: `ZostanDyrektorem()` method promotion
- **Overlapping**: Shared properties between roles

## Business Case
A university system modeling different roles (Student, Employee, Director) with their relationships.

## Demonstration
The `Main()` method demonstrates:
1. Creating instances of different roles
2. Polymorphic method calls
3. Interface implementation
4. Role promotion (Employee → Director)
5. Gender-specific attribute handling

## Technical Details
- **Language**: C#
- **Special Features**: Gender-enum differentiated construction

## Course Requirements Met
- [x] Original business case
- [x] Console application
- [x] Demonstration in main()
- [x] All required inheritance types implemented