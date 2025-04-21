using GameOfLifeCS;
using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    { 
        const int screenSize = 800;

        int gridSize = 800;
        int cellSize = screenSize/gridSize;

        World world = new World(gridSize, cellSize);

        Raylib.InitWindow(screenSize, screenSize, "Game Of Life");
        Raylib.SetTargetFPS(20);

        bool paused = true;

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGray);

            if (Raylib.IsKeyPressed(KeyboardKey.Space))
                paused = !paused;


            
            if (paused) MouseClickOnGrid(world, cellSize);

            world.DrawGrid();
            Raylib.DrawText(paused ? "PAUSED (SPACE to run)" : "RUNNING (SPACE to pause)", 10, 10, 20, Color.RayWhite);
            
            if (!paused) world.GameOfLife();
           
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

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


    static void Draw()
    {
        Raylib.DrawText("Hello Raylib-CS!", 10, 10, 20, Color.RayWhite);
    }
}
