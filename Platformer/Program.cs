using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SFMLApp
{
    class Program
    {
        static void Main()
        {
            byte[] grid = new byte[16 * 12];
            Clock clock = new Clock();

            float playerX = 1.1f;
            float playerY = 3;
            float speed = 4f;

            float jumpForce = 10f;
            float force = 0f;
            float gravity = 25f;
            bool canJump = true;

            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "SFML Square");

            RectangleShape player = new RectangleShape(new Vector2f(35, 35));
            player.FillColor = new Color(246, 126, 125);
            player.OutlineColor = new Color(97, 25, 63);
            player.OutlineThickness = 5;

            player.Position = new Vector2f(playerX * 50, 600 - (playerY * 50));

            List<RectangleShape> blocks = new List<RectangleShape>();

            grid[0] = 1; grid[1] = 1; grid[2] = 1;

            grid[6] = 1; grid[7] = 1; grid[8] = 1;

            grid[13] = 1;
            grid[14] = 1;
            grid[30] = 1;
            grid[46] = 1;


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
                playerY += force * deltaTime;
                force -= gravity * deltaTime;
                

                for (int y = 0; y < 12; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        if (grid[x + y * 16] == 1)
                        {
                            RectangleShape block = new RectangleShape(new Vector2f(45, 45));
                            block.FillColor = new Color(246, 126, 125);
                            block.OutlineColor = new Color(97, 25, 63);
                            block.OutlineThickness = 5;

                            block.Position = new Vector2f(x * 50, 550 - (y * 50));
                            blocks.Add(block);
                            if (playerX - x < 0.9f && playerX - x > -0.9f)
                            {
                                if (playerY < y + 0.9f)
                                {
                                    playerY = MathF.Max(y + 0.9f, playerY);
                                    canJump = true;
                                    force = 0;
                                }
                                if (y > playerY)
                                {
                                    playerY = MathF.Min(y - 0.9f, playerY);
                                }
                            }
                            if (playerY - y < 0.9f && playerY - y > -0.9f)
                            {
                                if (x < playerX)
                                {
                                    playerX = MathF.Max(x + 0.9f, playerX);
                                }
                                if (x > playerX)
                                {
                                    playerX = MathF.Min(x - 0.9f, playerX);
                                }
                            }
                            
                        }
                    }
                }

                player.Position = new Vector2f(playerX * 50, 550 - (playerY * 50));

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
