# SudokuSharp
A C# library for working with Sudoku puzzles

# Main classes
There are three classes introduced by this library: **Location**, **Grid** and **PencilGrid**.

## Location
**Location** is stored internally as an integer tracking the index of the location on the board (0-81).

### Location Constructors
```c#
Location(int Column, int Row)
Location(int Index)
```

### Location Properties
```c#
int Row
int Column
int Zone
int Index
```
Both the **Index** and **Zone** are ordered from the upper left corner, moving horizontally to the right, and wrapping to the next line after the end. Thus, the first line of **Zones** are number `0,1,2`, the next line `3,4,5`, and the final line `6,7,8`.

The indices are numbered `0-8` on the first line, `9-17` on the second line, etc.

**Location**s include implicit casts both to and from **int**s. This allows swapping them with **int**s, for instance in `for` loops:

```c#
for (Location loc=0; loc<81; loc++)
 { }
```
This will cycle through every location on the board.

## Grid
A **Grid* is actually a grid of 81 **Location**s.

### Grid Constructors
```c#
Grid();
Grid(Puzzle src);
```
The first creates a completely blank **Grid**, the second is to copy an existing **Grid**.

### Grid Factories
```c#
Grid Grid.CreateSolution(int Seed);
Grid Grid.CreatePuzzle(Grid Solution, int Seed);

Task<Grid> Grid.CreateSolutionAsync(int Seed);
Task<Grid> Grid.CreatePuzzleAsync(Grid Solution, int Seed);
```

The **CreateSolution** methods start with a new blank Grid and fill in the board completely (no empty spaces).

The **CreatePuzzle** methods begin with another Grid (either partially or completely filled in), and removes clues to create a **Grid** with a unique solution.

In either case, a new **Random** stream is seeded with the **Seed** value.

In either case, the Seed value is used to create a new random number stream for determinance.

### Grid Properties
```c#
bool IsSolved
bool IsValid
bool ExistsUniqueSolution
```
**IsSolved** makes sure that every **Location** is filled, and that the solution is valid.
**IsValid** checks for some common problems which prohibit a puzzle from having a unique solution: if two rows within any triplet are completely empty, for instance, they may be swapped resulting in two possible solutions. Likewise, it checks for two empty columns within triplets, and it checks that every number is on the board at least once (one number may be missing, but if two are then all cells containing those two digits may be swapped resulting in two possible solutions).
**ExistsUniqueSolution** will attempt to solve the **Puzzle** via bruteforce. When a solution is found, a flag is set. If a second solution is found (or none is found), then **false** will be returned; if only a single solution is found, then **true**.

### Grid Methods
```c#
Grid Grid.Solve();
```
**Solve** returns a solved form of the calling instance. The original instance of **Grid** is unchanged.

### Accessing Grid data
```c#
void Grid.PutCell(Location where, int value);
int Grid.GetCell(Location where);
```
Because of the implicit casting of the **Location** class, you may either use an actual **Location** instance or an **int** representing the Index.

You may also access via [] indexing with a Location value, such as:
```c#
Location where = new Location(x,y);
Grid[where]=value;
int result = Grid[where];
```
The above contain inlined versions of the PutCell and GetCell methods.
