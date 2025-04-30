using Raylib_cs;

namespace GameOfLifeCS
{
    /// <summary>
    /// Represents the cell grid in the game of life.
    /// Allows to initialize, draw, evaluate and change states of cells.
    /// </summary>
    public class World
    {

        State[,] Grid { get; set; }
        int GridSize { get; set; }
        int CellSize { get; set; }

        private readonly int randomChance;
        private readonly Random random = new();

        public enum State
        {
            Dead,
            Alive
        }

        /// <summary>
        /// Initialize new World → a 2-dimensional grid of States (Dead and Alive). It will fill the grid with dead cells and a random amount of alive cells.
        /// </summary>
        /// <param name="GridSize">Size of the grid → [size, size].</param>
        /// <param name="CellSize">Size of the one cell in pixels.</param>
        /// <param name="randomChance">Integer between 0 and 100. The bigger, then bigger the chance for alive cells. Will change values lower than 0 and bigger than 100 to 100.</param>
        public World(int GridSize, int CellSize, int randomChance)
        {

            //Setting new grid
            Grid = new State[GridSize, GridSize];
            this.GridSize = GridSize;
            this.CellSize = CellSize;

            if (randomChance > 100 || randomChance < 0) randomChance = 100;
            this.randomChance = randomChance;

            Grid = SetAllDead(Grid);
            Grid = SetRandAlive(Grid);

        }

        /// <summary>
        /// Takes any grid, sets all cells dead (as metal as it sounds!) and returns this grid.
        /// </summary>
        /// <param name="grid">The grid to change.</param>
        /// <returns>Grid with all cells set as dead.</returns>
        private State[,] SetAllDead(State[,] grid)
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                    grid[i, j] = State.Dead;
            }
            return grid;
        }

        /// <summary>
        /// Takes any grid, sets random amount of them as alive, and returns this grid.
        /// </summary>
        /// <param name="grid">Grid to change</param>
        /// <returns>Grid with some of the cells set as alive.</returns>
        private State[,] SetRandAlive(State[,] grid)
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)

                    if (random.Next(100) < randomChance)
                    {
                        grid[i, j] = State.Alive;
                    }
            }
            return grid;
        }

        /// <summary>
        /// Draws Grid with slight borders around every cell. Uses "switch" to go for every possible cell state.
        /// </summary>
        public void DrawGrid()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    int x = i * CellSize;
                    int y = j * CellSize;

                    switch (Grid[i, j])
                    {
                        case State.Dead:
                            Raylib.DrawRectangle(x, y, CellSize, CellSize, Color.DarkGray);
                            break;
                        case State.Alive:
                            Raylib.DrawRectangle(x, y, CellSize, CellSize, Color.Yellow);
                            break;
                        default:
                            Raylib.DrawRectangle(x, y, CellSize, CellSize, Color.DarkGray);
                            break;
                    }


                    Raylib.DrawRectangleLines(x, y, CellSize, CellSize, Color.Black);
                }
            }
        }
        /// <summary>
        /// Allows for simple change in the cell state → mostly for Mouse Control
        /// </summary>
        /// <param name="x">The x coordinate for the cell.</param>
        /// <param name="y">The y coordinate for the cell.</param>
        public void ChangeStateSimple(int x, int y)
        {
            if (Grid[x, y] == State.Dead) Grid[x, y] = State.Alive;
            else if (Grid[x, y] == State.Alive) Grid[x, y] = State.Dead;
        }
        /// <summary>
        /// The most important function of this class. It creates a copy of the current grid and creates a new one; then performs calculations and sets the new grid as the Grid with updated states of the cells. Skips edge cases because of yes (I don't want to do them).
        /// </summary>
        public void GameOfLife()
        {
            //Copy new grid and crate new one → that will replace the old one after calculating changes
            State[,] currentGrid = (State[,])Grid.Clone();
            State[,] newGrid = SetAllDead(new State[GridSize, GridSize]);

            //Go to every cell and calculate if it need to be changed. Leave edge cases as i don't want to do it.
            for (int i = 1; i < Grid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < Grid.GetLength(1) - 1; j++)
                {

                    (int live, int dead) = CountNeighbours(currentGrid, i, j);

                    if (Grid[i, j] == State.Dead && live == 3)
                    {
                        newGrid[i, j] = State.Alive;
                    }
                    else if (Grid[i, j] == State.Alive && live < 2)
                    {
                        newGrid[i, j] = State.Dead;
                    }
                    else if (Grid[i, j] == State.Alive && (live == 2 || live == 3))
                    {
                        newGrid[i, j] = State.Alive;
                    }
                    else if (Grid[i, j] == State.Alive && live > 3)
                    {
                        newGrid[i, j] = State.Dead;
                    }
                }
            }

            Grid = newGrid;

        }
        /// <summary>
        /// Find Moore Neighbors for a cell and count alive and dead cells in it. 
        /// </summary>
        /// <param name="currentGrid">The grid that is used for the calculations.</param>
        /// <param name="x">The X coordinate of the cell.</param>
        /// <param name="y">The Y coordinate of the cell.</param>
        /// <returns>Tuple of live and dead cells (live, dead)</returns>
        private static (int live, int dead) CountNeighbours(State[,] currentGrid, int x, int y)
        {
            int live = 0;
            int dead = 0;

            //UP - LEFT
            if (currentGrid[x - 1, y - 1] == State.Alive) live++;
            else dead++;

            //UP
            if (currentGrid[x, y - 1] == State.Alive) live++;
            else dead++;

            //UP - RIGHT
            if (currentGrid[x + 1, y - 1] == State.Alive) live++;
            else dead++;

            //LEFT
            if (currentGrid[x - 1, y] == State.Alive) live++;
            else dead++;

            //RIGHT
            if (currentGrid[x + 1, y] == State.Alive) live++;
            else dead++;

            //DOWN - LEFT
            if (currentGrid[x - 1, y + 1] == State.Alive) live++;
            else dead++;

            //DOWN
            if (currentGrid[x, y + 1] == State.Alive) live++;
            else dead++;

            //DOWN - LEFT
            if (currentGrid[x + 1, y + 1] == State.Alive) live++;
            else dead++;

            return (live, dead);
        }

        public void DrawLine((int x, int y) pointA, (int x, int y) pointB)
        {
            
            int deltaX = Math.Abs(pointB.x - pointA.x);
            int deltaY = Math.Abs(pointB.y - pointA.y);

            int stepX = (pointA.x < pointB.x) ? 1 : -1;
            int stepY = (pointA.y < pointB.y) ? 1 : -1;

            int err = deltaX - deltaY;

            int x = pointA.x;
            int y = pointA.y;

            List<(int x,int y)> cells = new List<(int,int)> ();

            while(true)
            {
                cells.Add((x,y));

                if (x == pointB.x && y == pointB.y) break;

                int maxErr = err * 2;
                if (maxErr > -deltaY)
                {
                    err -= deltaY;
                    x += stepX;
                }
                if (maxErr < deltaX)
                {
                    err += deltaX;
                    y += stepY;
                }

            }
            
            foreach (var cell in cells)
            {
                if (Grid[cell.x, cell.y] == State.Dead)
                {
                    ChangeStateSimple(cell.x, cell.y);
                }
            }

        }

    }
}
