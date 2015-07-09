# SudokuSharp
A C# library for working with Sudoku puzzles

# Main classes
There are two classes introduced by this library: Location and Puzzle.
Location is essentially an integer (and includes implicit casting, both to and from, standard int).
Essentially, it is an index (0-81) of every possible position on the board.
It also includes the following properties: Row, Column, Zone, and Index.
Each of these returns an int of the appropriate value.

It may be cast to or from an int, or you may use the constructor with syntax
new Location(int Column, int Row)
for convenience.

The SudokuSharp.Puzzle class is essentially a gameboard, with every location (cell) filled in by 0-9 (0 corresponds to an empty cell).
You may call the default construction Puzzle() to create a completely empty board; if you wish to create a filled in puzzle, you may call one of two factory methods:
Puzzle myPuzzle = Puzzle.CreateSolution(int Seed);
Puzzle myPuzzle = Puzzle.CreatePuzzle(Puzzle Solution, int Seed);
In either case, the Seed value is used to create a new random number stream for determinance.
The first begins with an empty board and randomly places all digits; the second begins with a filled (either partially or completely) board and removes pieces to create a game to play. I make no guarantees about the resulting difficulty level (grading will come in a future version), only in determinance (hence the seed value).

There are also async factories available; they are:
Puzzle.CreateSolutionAsync(int Seed);
Puzle.CreatePuzzleAsync(Puzzle Solution, int Seed);

The Puzzle class also contains many helpful properties:
bool IsSolved: returns whether the puzzle is completely solved AND valid.
bool IsValid: returns whether the puzzle MAY be solved, even though it is only partially filled in. There are certain conditions which are known to preclude this, and this method checks for those.
bool ExistsUniqueSolution: Attempts to randomly solve the puzzle every possible way; when the first solution is found, a flag is set. If a second solution is found, it returns false. If no other solution is found, it returns true.

The Puzzle class also contains a solving method:
Puzzle Puzzle.Solve: returns a solved form of the calling instance. The original instance is unchanged.
