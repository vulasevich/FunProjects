using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Collections.Generic;

namespace SFMLApp
{
    class Program
    {
        static void Main()
        {
            uint cellsize = 50;
            uint gridheight = 12;
            uint gridwidth = 16;
            bool[] grid = new bool[gridwidth * gridheight];

            bool buttonPressed = false;

            Clock clock = new Clock();
            Time delay = Time.FromMilliseconds(150);

            RenderWindow window = new RenderWindow(new VideoMode(gridwidth * cellsize, gridheight * cellsize), "SFML Square");
            List<RectangleShape> blocks = new List<RectangleShape>();

            for (int y = 0; y < gridheight; y++)
            {
                for (int x = 0; x < gridwidth; x++)
                {
                    if (grid[x + y * gridwidth])
                    {
                        RectangleShape block = new RectangleShape(new Vector2f(cellsize, cellsize));
                        block.FillColor = new Color(0, 0, 0);
                        block.Position = new Vector2f(x * cellsize, y * cellsize);
                        blocks.Add(block);
                    }
                }
            }

            while (window.IsOpen)
            {
                window.DispatchEvents();

                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    if (buttonPressed)
                    {
                        buttonPressed = false;
                        Vector2i mousePosition = Mouse.GetPosition(window);
                        int x = mousePosition.X / (int)cellsize;
                        int y = mousePosition.Y / (int)cellsize;

                        if (x >= 0 && x < gridwidth && y >= 0 && y < gridheight)
                        {
                            grid[x + y * gridwidth] = !grid[x + y * gridwidth];
                        }
                    }
                }
                else
                {
                    buttonPressed = true;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    if (clock.ElapsedTime >= delay)
                    {
                        LifeUpdate();
                        clock.Restart();
                    }
                }

                window.Clear(new Color(255, 255, 255));

                blocks.Clear();
                for (int y = 0; y < gridheight; y++)
                {
                    for (int x = 0; x < gridwidth; x++)
                    {
                        if (grid[x + y * gridwidth])
                        {
                            RectangleShape block = new RectangleShape(new Vector2f(cellsize, cellsize));
                            block.FillColor = new Color(0, 0, 0);
                            block.Position = new Vector2f(x * cellsize, y * cellsize);
                            blocks.Add(block);
                        }
                    }
                }

                foreach (var block in blocks)
                {
                    window.Draw(block);
                }

                window.Display();
            }

            void LifeUpdate()
            {
                bool[] oldGrid = (bool[])grid.Clone();

                for (uint y = 0; y < gridheight; y++)
                {
                    for (uint x = 0; x < gridwidth; x++)
                    {
                        int neighbours = CountNeighbours(oldGrid, (int)x, (int)y);
                        int index = (int)(x + y * gridwidth);

                        if (oldGrid[index])
                        {
                            if (neighbours == 2 || neighbours == 3)
                            {
                                grid[index] = true;
                            }
                            else
                            {
                                grid[index] = false;
                            }
                        }
                        else
                        {
                            if (neighbours == 3)
                            {
                                grid[index] = true;
                            }
                            else
                            {
                                grid[index] = false;
                            }
                        }
                    }
                }
            }

            int CountNeighbours(bool[] grid, int x, int y)
            {
                int count = 0;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        int nx = (x + i + (int)gridwidth) % (int)gridwidth;
                        int ny = (y + j + (int)gridheight) % (int)gridheight;

                        if (grid[nx + ny * (int)gridwidth])
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
        }
    }
}