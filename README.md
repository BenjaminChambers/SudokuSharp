# Welcome to the SudokuSharp 2.0

SudokuSharp is a .NET Portable Class Library intended for working with Sudoku puzzles. It is available from the NuGet repository at NuGet.org, or in source form from GitHub.

It contains both low level classes for working with Sudoku boards, and an example higher level class for building a complete Sudoku game.

## Getting Started

The main classes that you will work with, when using SudokuSharp, are:
&nbsp;<ul>
<li>Location</li>
<li>Board</li>
</ul>&nbsp;

## Board

Most of the work you do with Sudoku puzzles will be via the Board class.

Internally, it is represented as an array of 81 integers; values 1-9 correspond to filled cells, and a value of 0 designates an empty cell.

You may access the board with either the GetCell / PutCell methods, or via an array accessor (Board[Location]).

For instance,
```
Board.PutCell(0, 5);
Board[0]=5;

int value = Board.GetCell(15);
int value = Board[15];
```

(Usually, instead of referencing cells by their index (as above), you will use an instance of the Location class... see below)

Each Board also provides a number of bool properties to make evaluating them easier: IsSolved, IsValid, and ExistsUniqueSolution.

IsSolved tells you if the puzzle has no empty spaces, no duplicate values, and each value appearing once in every row, column, or 3x3 zone.

IsValid checks whether an incomplete puzzle MAY be solved. Specifically, it checks that:
<ul>
<li>there are no duplicate values,</li>
<li>you have at least one of every digit except one present, (If two digits are entirely absent from the Board, then you may swap those sets of digits in the solution, resulting in at least two possible solutions)</li>
<li>you cannot swap any rows or columns (resulting in at least two possible solutions)</li>
</ul>
More checks may be added in the future if necessary.

ExistsUniqueSolution is straightforward: it attempts to solve the puzzle multiple times via brute force. As soon as a second solution is found, it quits and returns false. If no solution is found, it returns false. It will only return true if a single correct solution is found.

###Board Helper Classes
Additionally, there are several member classes which contain helpful functions. These are accessed via members of Board instances, but will usually leave the original Board untouched.

###Board.Fill
```
Board Board.Fill.Sequential()
Board Board.Fill.Randomized()
```
Both of these functions return new Board instances. They attempt to fill every empty location in the Board; Sequential tries digits in order, whereas Randomized shuffles the digits before trying them.

If the calling instance cannot be solved successfully (ie there is no combination of digits which will result in a completely filled Board), then null is returned.

Internally, simple constraint tracking is used, so performance is very good. Although I don't implement a heap, my tests show performance on par with, or better than, heap-based constraint solvers for solving any random puzzle via this method.

###Board.Find
```
Board.Find.EmptyLocations()
Board.Find.FilledLocations()
```
These both return lists of Location values.

```
Board.Find.Candidates(Location Where)
```
Returns a list of digits which may be placed here.

```
Board.Find.AllCandidates()
```
Returns a dictionary. The key is an empty Location, the value is a list of digits.

```
Board.Find.NakedSingles()
Board.Find.HiddenSingles()
Board.Find.AllSingles()
Board.Find.LockedCandidates()
```
Attempts to solve for these patterns, and return IEnumerable<KeyValuePair<Location, int>>.
The generic form of the answer is left so as to avoid unnecessary conversions.


###Board.Cut
```
Board Board.Cut.Quad(Random Stream)
Board Board.Cut.Pair(Random Stream)
Board Board.Cut.Single(Random Stream)
```
These functions are intended to aid in puzzle generation. Each one attempts to cut a [number of] single[s] from a board and, if the Board still only has one solution, will return that Board. If the resulting Board has multiple solutions, then the original instance will be returned as a result.

## Location
The Location class is a helpful utility for dealing with Boards; most functions require a Location rather than an integer index. However, the class may be freely cast back-and-forth with Int32.

For example,
```
Location where = new Location(0); // Create via index
Location where = new Location(3,5); // Create via Column, Row (x, y like in mathematics)
Location where = 8;
int i = new Location(47);
```

You may get specific information about a Location via the Row, Column, and Zone, or Index properties.

Zones are ordered as:
```
012
345
678
```

Location.Index is what is used when casting to Int32.

There are also some predeclared lists of Locations you may iterate over. The most common one is All:
```
foreach (var loc in Location.All)
  { }
```
All is a static member of the Location class, thus called without an instance. Another list exists which requires an instance: Blocking

```
Location myLoc = new Location(17);
foreach (var loc in myLoc.Blocking)
   { }
```

If you have two Location instances you wish to compare, you may manually do so, or call any of the following:
```
MyLocation.IsSameRow(Location other)
MyLocation.IsSameColumn(Location other)
MyLocation.IsSameZone(Location other)
MyLocation.IsBlockedBy(Location other)
```

## Factory

The Factory class contains static members to generate Board instances.
```
Board Factory.Solution(int Seed)
Board Factory.Solution(Random Stream)
```
Both these methods create a new Sudoku Board completely randomized.

```
Board Factory.Puzzle(int Seed, int QuadsToCut, int PairsToCut, int SinglesToCut)
Board Factory.Puzzle(Board Source, Random Stream, int QuadsToCut, int PairsToCut, int SinglesToCut)
```
Both these methods return a Board instance with several cells removed, such that you may use the result as a game.
In each case, they make the specific number of calls to Board.Cut.* (Quad, Pair, Single) and return the result.

I'm investigating puzzle generation which would target specific grades of difficulty, but I have not yet implemented anything I like.
