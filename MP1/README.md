# MP01 - Classes and Attributes Implementation

Implementation of class structures and various attribute types for a student management system.

## Implemented Concepts
- **Class Extension**: Static `DB` list maintains all instances of `MP01` class
- **Persistence**: JSON serialization for saving/loading student data (`safeToFile()`, `getFromFile()`)
- **Attribute Types**:
  - Simple: `Id` (integer identifier)
  - Composite: `Eska` + `Group` (student ID + group)
  - Optional: `Itn` (boolean flag)
  - Repeatable: `Groups` (list of strings)
  - Class-level: `StudentsCount` (static property)
  - Derived: `FullName` (computed from `Eska` and `Group`)
- **Method Types**:
  - Class method: `WriteOutAllStudents()`
  - Instance method: `ChooseYourStudent()`
  - Overridden: `ToString()`
  - Overloaded: `SubmitAccept()` and `SubmitDeny()` (with/without reason)

## Business Case
A system for managing student records at PJATK, tracking basic student information, group assignments, and academic status.

## Demonstration
The `Main()` method demonstrates:
1. Creating student instances
2. Class method invocation
3. Instance method usage
4. Method overriding and overloading
5. Persistence operations

## Technical Details
- **Language**: C#
- **Dependencies**: `System.Text.Json` for JSON serialization
- **Persistence**: Local JSON file storage (`ITN.json`)

## Course Requirements Met
- [x] Original business case
- [x] Console application
- [x] Demonstration in main()
- [x] All required attribute types implemented
- [x] Persistence implementation