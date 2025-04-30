using GameOfLifeCS;
using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace GameOfLifeCS
{
    class Program
    {
        static void Main()
        {
            const int screenSize = 1000;
            const int gridSize = 250;
            const int cellSize = screenSize / gridSize;
            const int randomChance = 10; //ONLY from 0 to 100 (Will be changed to 100 if outside this clamps).
            World world = new World(gridSize, cellSize, randomChance);
            bool paused = true;

            bool firstPointMarked = false;
            Vector2 firstLinePointPos = Vector2.Zero;
            Vector2 secondLinePointPos = Vector2.Zero;

            bool debugMode = true;

            Raylib.InitWindow(screenSize, screenSize, "Game Of Life");
            Raylib.SetTargetFPS(60);


            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DarkGray);

                if (Raylib.IsKeyPressed(KeyboardKey.Space))
                    paused = !paused;

                if (paused) MouseClickOnGrid(world, cellSize);
                if (paused && Raylib.IsMouseButtonReleased(MouseButton.Right))
                {
                    if (!firstPointMarked) 
                    {
                        firstLinePointPos = Raylib.GetMousePosition();
                        Vector2 mousePos = Raylib.GetMousePosition();

                        int x = (int)(mousePos.X / cellSize);
                        int y = (int)(mousePos.Y / cellSize);

                        world.ChangeStateSimple(x, y);

                        firstPointMarked = true;
                    }
                    else 
                    {
                        secondLinePointPos = Raylib.GetMousePosition();
                        CreateLine(firstLinePointPos, secondLinePointPos, world, cellSize);

                        firstPointMarked = false;

                    }
                }

                world.DrawGrid();


                Raylib.DrawText(paused ? "PAUSED (SPACE to run)" : "RUNNING (SPACE to pause)", 10, 10, 20, Color.RayWhite);

                if (!paused) world.GameOfLife();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        /// <summary>
        /// Allows changing State of the cell by clicking on it. 
        /// </summary>
        /// <param name="world">Current world of the game of life.</param>
        /// <param name="cellSize">It is necessary to convert pixel coordinates to grid coordinates.</param>
        public static void MouseClickOnGrid(World world, int cellSize)
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Vector2 mousePos = Raylib.GetMousePosition();

                int x = (int)(mousePos.X / cellSize);
                int y = (int)(mousePos.Y / cellSize);

                world.ChangeStateSimple(x, y);

                Console.WriteLine("Click: " + x + ", " + y);

            }
        }

        public static void CreateLine(Vector2 startPos, Vector2 endPos, World world, int cellSize)
        {
            int startPosX= (int)(startPos.X / cellSize);
            int startPosY = (int)(startPos.Y / cellSize);

            int endPosX = (int)(endPos.X / cellSize);
            int endPosY = (int)(endPos.Y / cellSize);

            world.DrawLine((startPosX, startPosY), (endPosX, endPosY));

            Console.WriteLine("Line → From:" + startPosX + ", " + startPosY + " to " + endPosX + ", " + endPosY);
        }


    }
}