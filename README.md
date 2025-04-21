# Game of Life — RaylibCS Edition

This is a simple and interactive implementation of Conway's Game of Life using C# and Raylib-cs.

## 💡 Description

This version of Game of Life features:

- Customizable grid size (default: 100x100)
- Cell editing via mouse clicks
- Pausing and resuming the simulation with the spacebar
- Simple random initialization of the grid (controlled by `randomChance`)

The application uses `Raylib-cs` for graphics rendering and input handling.

## 🕹️ Controls

| Key            | Action                  |
|----------------|--------------------------|
| `SPACE`        | Pause / Resume simulation |
| `Left Mouse`   | Toggle cell alive/dead   |
| `ESC`          | Exit the program          |

## 🧱 Structure

- `Program.cs` — Handles the window, input, and main loop
- `World.cs` — Contains the grid logic, state updates, and rendering

- ## 🙌 Credits

Created with ❤️ using Raylib-cs.

Inspired by the classic [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life).

## 📄 License

MIT — free to use and modify.
