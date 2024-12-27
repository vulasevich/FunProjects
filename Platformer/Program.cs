using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Collections.Generic;

namespace SFMLApp
{
    class Program
    {
        static void Main()
        {
            byte[] grid = new byte[16 * 12];
            Clock clock = new Clock();

            float playerX = 1f;
            float playerY = 2;
            float speed = 5f;

            float jumpForce = 11f;
            float force = 0f;
            float gravity = 30;
            bool canJump = true;

            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "SFML Square");
            window.Closed += (sender, e) => window.Close();

            RectangleShape player = new RectangleShape(new Vector2f(35, 35))
            {
                FillColor = new Color(246, 126, 125),
                OutlineColor = new Color(97, 25, 63),
                OutlineThickness = 5
            };
            player.Position = new Vector2f(playerX * 50, 600 - (playerY * 50));

            grid[0] = 1; grid[1] = 1; grid[2] = 1;
            grid[6] = 1; grid[7] = 1; grid[8] = 1;
            grid[13] = 1; grid[14] = 1; grid[30] = 1; grid[46] = 1;
            grid[75] = 1; grid[54] = 1;
            grid[65] = 1; grid[66] = 1; grid[67] = 1;
            grid[98] = 1; grid[115] = 1; grid[116] = 1; grid[117] = 1;
            grid[138] = 1; grid[139] = 1; grid[140] = 1; grid[141] = 1; grid[142] = 1;

            List<RectangleShape> blocks = new List<RectangleShape>();
            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    if (grid[x + y * 16] == 1)
                    {
                        RectangleShape block = new RectangleShape(new Vector2f(45, 45))
                        {
                            FillColor = new Color(246, 126, 125),
                            OutlineColor = new Color(97, 25, 63),
                            OutlineThickness = 5,
                            Position = new Vector2f(x * 50, 550 - (y * 50))
                        };
                        blocks.Add(block);
                    }
                }
            }

            while (window.IsOpen)
            {
                window.DispatchEvents();

                float deltaTime = clock.Restart().AsSeconds();

                if (Keyboard.IsKeyPressed(Keyboard.Key.A)) playerX -= speed * deltaTime;
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)) playerX += speed * deltaTime;

                if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && canJump)
                {
                    force = jumpForce;
                    canJump = false;
                }

                force -= gravity * deltaTime;
                playerY += force * deltaTime;
                if (playerY < -2)
                {
                playerX = 1f;
                playerY = 2;
                }
                foreach (var block in blocks)
                {
                    float blockX = block.Position.X / 50;
                    float blockY = (550 - block.Position.Y) / 50;

                    if (Math.Abs(playerY - blockY) < 0.89f)
                    {
                        if (playerX < blockX && playerX + 1 > blockX)
                        {
                            playerX = MathF.Min(blockX - 1, playerX);
                        }
                        if (playerX > blockX && playerX - 1 < blockX)
                        {
                            playerX = MathF.Max(blockX + 1, playerX);
                        }
                    }

                    if (Math.Abs(playerX - blockX) < 0.89f)
                    {
                        if (playerY < blockY + 1 && playerY > blockY)
                        {
                            playerY = MathF.Max(blockY + 1, playerY); 
                            force = 0;
                            canJump = true;
                        }
                        if (playerY > blockY - 1 && playerY < blockY)
                        {
                            playerY = MathF.Min(blockY - 1, playerY);

                            force = 0;
                        }
                    }

                }
                player.Position = new Vector2f((playerX + 0.1f) * 50, 550 - ((playerY - 0.1f) * 50));

                window.Clear(new Color(151, 59, 97));

                foreach (var block in blocks)
                {
                    window.Draw(block);
                }

                window.Draw(player);
                window.Display();
            }
        }
    }
}
