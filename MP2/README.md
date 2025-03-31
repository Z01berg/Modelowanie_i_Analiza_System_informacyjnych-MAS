# MP02 - Associations Implementation

Implementation of various association types between packages and clients.

## Implemented Concepts
- **Regular Association**: `Klient` ↔ `Paczka` bidirectional relationship
- **Association with Attribute**: Priority attribute on package-client relationship
- **Qualified Association**: Package lookup by name within client context
- **Composition**: Automatic reverse field creation (client assignment in package)
- **Multiplicities**: 1-to-many (client to packages)

## Business Case
A package management system where clients can send/receive packages with different priorities.

## Demonstration
The `Main()` method demonstrates:
1. Client creation
2. Package addition with different priority levels
3. Qualified package lookup
4. Client removal with cascading package cleanup
5. Console visualization of relationships

## Technical Details
- **Language**: C#
- **Special Features**: Console color formatting for better visualization

## Course Requirements Met
- [x] Original business case
- [x] Console application
- [x] Demonstration in main()
- [x] All required association types implemented
- [x] Automatic reverse field creation