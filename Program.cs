using Raylib_cs;
using System.Numerics;

class Program
{
    static void Main()
    { 
        const int screenWidth = 800;
        const int screenHeight = 600;

        Raylib.InitWindow(screenWidth, screenHeight, "Raylib-CS: Starter Template");
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            // LOGIKA GRY TU
            Update();

            // RYSOWANIE TU
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGray);

            Draw();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    static void Update()
    {
        // np. porusz obiekt
    }

    static void Draw()
    {
        Raylib.DrawText("Hello Raylib-CS!", 10, 10, 20, Color.RayWhite);
    }
}
