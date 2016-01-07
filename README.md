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

###Board.Find
###Board.Cut
###Board.Fill
###Board.Factory

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
foreach (var loc in MyLocation.Blocking())
   { }
```

If you have two Location instances you wish to compare, you may manually do so, or call any of the following:
```
MyLocation.IsSameRow(Location other)
MyLocation.IsSameColumn(Location other)
MyLocation.IsSameZone(Location other)
MyLocation.IsBlockedBy(Location other)
```
