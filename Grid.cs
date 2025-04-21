using System;
using System.Collections.Generic;
using Raylib_cs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameOfLifeCS
{
    public class World
    {

        State[,] Grid {get; set;}
        int GridSize { get; set;}
        int CellSize { get; set;}

        public enum State
        {
            Dead,
            Alive
        }

        public World(int GridSize, int CellSize) 
        {
        
            //Setting new grid
            Grid = new State[GridSize, GridSize];
            this.GridSize = GridSize;
            this.CellSize = CellSize;

            Grid = SetAllDead(Grid);
            Grid = SetRandAlive(Grid);

        }

        /// <summary>
        /// Sets all cells dead (as metal as it sounds!).
        /// </summary>
        /// <param name="size"></param>
        public State[,] SetAllDead(State[,] grid) 
        {
            for (int i = 0; i < GridSize; i++)
            {
                for(int j = 0; j < GridSize; j++)
                    grid[i,j] = State.Dead;
            }
            return grid;
        }

        Random random = new Random();
        public State[,] SetRandAlive(State[,] grid)
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)

                    if (random.Next(100) < 10)
                    {
                        grid[i, j] = State.Alive;
                    }
            }
            return grid;
        }

        /// <summary>
        /// Draws Grid with slight borders around avery cell. Uses "switch" to go for every posible cell state.
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
        /// Allows for simple change in the cell state → mostly for Mouse Controll
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ChangeStateSimple(int x, int y)
        {
            if (Grid[x,y] == State.Dead) Grid[x,y] = State.Alive;
            else if (Grid[x,y] == State.Alive) Grid[x,y] = State.Dead;
        }

        public void GameOfLife()
        {
            //Copy new grid and crate new one → that will replace the old one after calculating changes
            State[,] currentGrid = (State[,])Grid.Clone();
            State[,] newGrid = SetAllDead(new State[GridSize, GridSize]);

            //Go to every cell and calculate if it need to be changed. Leave edge cases as i dont want to.
            for (int i = 1; i < Grid.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < Grid.GetLength(1) - 1; j++)
                {

                    (int live, int dead) = CountNeighbours(currentGrid, i, j); 

                    if (Grid[i,j] == State.Dead && live == 3)
                    {
                        newGrid[i, j] = State.Alive;
                    }
                    else if(Grid[i,j] == State.Alive && live < 2)
                    {
                        newGrid[i,j] = State.Dead;
                    }
                    else if (Grid[i,j] == State.Alive && (live == 2 || live == 3))
                    {
                        newGrid[i, j] = State.Alive;
                    }
                    else if (Grid[i,j] == State.Alive && live > 3)
                    {
                        newGrid[i, j] = State.Dead;
                    }
                }
            }

            Grid = newGrid;

        }

        /// <summary>
        /// Find Moore Neighbours for cell and count alive and dead cells in it. 
        /// </summary>
        /// <param name="currentGrid"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private (int live, int dead) CountNeighbours(State[,] currentGrid, int x, int y)
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
            if (currentGrid[x -1, y] == State.Alive) live++;
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
    }
}
