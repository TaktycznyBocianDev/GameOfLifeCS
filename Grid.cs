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

        enum State
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

            SetAllDead();

        }

        /// <summary>
        /// Sets all cells dead (as metal as it sounds!).
        /// </summary>
        /// <param name="size"></param>
        public void SetAllDead() 
        {
            for (int i = 0; i < GridSize; i++)
            {
                for(int j = 0; j < GridSize; j++)
                    Grid[i,j] = State.Dead;
            }
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

        public void ChangeStateSimple(int x, int y)
        {
            if (Grid[x,y] == State.Dead) Grid[x,y] = State.Alive;
            else if (Grid[x,y] == State.Alive) Grid[x,y] = State.Dead;
        }
    }
}
