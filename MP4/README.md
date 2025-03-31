# MP04 - Constraints Implementation

Implementation of various constraints in a group management system.

## Implemented Concepts
- **Attribute Constraint**: Name length ≤ 10 characters
- **Unique Constraint**: Auto-generated unique IDs
- **Subset Constraint**: Group members must exist in Person DB
- **Ordered Constraint**: Explicit member ordering in groups
- **XOR Constraint**: Role assignment restrictions
- **Custom Constraint**: Index validation for ordered insertion
- **Bag**: Logging constraint violations

## Business Case
A system for managing university groups with strict membership rules and role assignments.

## Demonstration
The `Main()` method demonstrates:
1. Person creation with name validation
2. Ordered group membership
3. Role assignment constraints
4. Subset validation
5. Constraint violation logging

## Technical Details
- **Language**: C#
- **Dependencies**: Custom `Logger` class for constraint violations
- **Special Features**: Console table formatting for visualization

## Course Requirements Met
- [x] Original business case
- [x] Console application
- [x] Demonstration in main()
- [x] All required constraint types implemented