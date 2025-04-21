using GameOfLifeCS;
using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    { 
        const int screenSize = 800;

        int gridSize = 10;
        int cellSize = screenSize/gridSize;

        World world = new World(gridSize, cellSize);

        Raylib.InitWindow(screenSize, screenSize, "Game Of Life");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            // LOGIKA GRY TU

            MouseClickOnGrid(world, cellSize);

            // RYSOWANIE TU
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGray);

            world.DrawGrid();

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

        }
    }


    static void Draw()
    {
        Raylib.DrawText("Hello Raylib-CS!", 10, 10, 20, Color.RayWhite);
    }
}
