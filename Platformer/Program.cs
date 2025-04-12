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

            Vector2f startPosition = new Vector2f(1, 2);

            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Platformer");
            window.Closed += (sender, e) => window.Close();

            List<Block> Blocks = new List<Block>
{
    new Block(0f, 0f, 1),
    new Block(1f, 0f, 1),
    new Block(2f, 0f, 1),

    new Block(7f, 0f, 1),
    new Block(8f, 0f, 1),
    new Block(9f, 0f, 1),

    new Block(14f, 0f, 1),
    new Block(15f, 0f, 1),
    new Block(15f, 1f, 1),
    new Block(15f, 2f, 1),
    
    new Block(10.5f, 4.5f, 1),    
    new Block(12f, 4f, 1),

    new Block(6f, 4f, 1),

    new Block(0f, 4f, 1),
    new Block(0f, 5f, 1),
    new Block(0f, 6f, 1),
    
    new Block(1f, 4f, 1),
    new Block(2f, 4f, 1),
    new Block(3f, 4f, 1),

    new Block(2f, 7f, 1),
    new Block(3f, 7f, 2),
    new Block(4f, 7f, 2),
    new Block(5f, 7f, 1),

    new Block(10f, 8f, 1),
    new Block(11f, 8f, 2),
    new Block(12f, 8f, 2),
    new Block(13f, 8f, 2),
    new Block(14f, 8f, 1),
};


            RectangleShape player = new RectangleShape(new Vector2f(35, 35))
            {
                FillColor = new Color(246, 126, 125),
                OutlineColor = new Color(97, 25, 63),
                OutlineThickness = 5
            };
            player.Position = new Vector2f(playerX * 50, 600 - (playerY * 50));


            List<RectangleShape> shapes = new List<RectangleShape>();
            foreach (var block in Blocks)
            {
                Color blockColor = new Color (246, 126, 125);
                if (block.type == 2)
                {blockColor = new Color (195, 14, 89);}
                        RectangleShape shape = new RectangleShape(new Vector2f(45, 45))
                        {
                            FillColor = blockColor, 
                            OutlineColor = new Color(97, 25, 63),
                            OutlineThickness = 5,
                            Position = new Vector2f(block.positionX * 50, 550 - (block.positionY * 50))
                        };
                        shapes.Add(shape);
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
                foreach (var block in Blocks)
                {
                    float blockX = block.positionX;
                    float blockY = block.positionY;

                    if (block.type == 2) 
                    {
                      if (Math.Abs(playerY - blockY) < 0.98f)
                        {  
                            if (Math.Abs(playerX - blockX) < 0.98f)
                            {
                                playerX = startPosition.X;
                                playerY = startPosition.Y;
                            }
                        }
                    }
                    else
                    {
                        if (Math.Abs(playerY - blockY) < 0.99f)
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

                        if (Math.Abs(playerX - blockX) < 0.99f)
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
                }
                player.Position = new Vector2f((playerX + 0.1f) * 50, 550 - ((playerY - 0.1f) * 50));

                window.Clear(new Color(151, 59, 97));

                foreach (var shape in shapes)
                {
                    window.Draw(shape);
                }

                window.Draw(player);
                window.Display();
            }
        }
    }
    class Block
    {
        public float positionX;
        public float positionY;     
        public byte type;

        public Block(float x, float y, byte thisType)
        {
            positionX = x;
            positionY = y;
            type = thisType;
        }
    }
}
