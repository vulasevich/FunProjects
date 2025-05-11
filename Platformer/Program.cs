    #nullable disable
    using System;
    using System.Timers;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;
using System.Numerics;

namespace SimplePlatformer
    {
        public partial class Form1 : Form
        {
            [DllImport("user32.dll")]
            public static extern short GetAsyncKeyState(Keys vKey);

            float speed = 5f;

            int level = 1;

            Vector2 spawnPoint = new Vector2(1, 2);
            
            Vector2 playerPosition = new Vector2(1, 2);
            float jumpForce = 13f; 
            float force = 0f;
            float gravity = 40;
            bool canJump = true;

            float coyoteTime = 0.1f;
            float coyoteTimeCounter = 0f;

            private bool wasJumpPressed = false;
            private System.Timers.Timer gameTimer;

            private DateTime lastUpdateTime;

            List<Block> Blocks = new List<Block>
            {
                new Block(0f, 0f, 1),
                new Block(1f, 0f, 1),
                new Block(2f, 0f, 3),

                new Block(6.5f, 0f, 1),
                new Block(7.5f, 0f, 1),
                new Block(8.5f, 0f, 1),
                new Block(9.5f, 0f, 1),

                new Block(14f, 0f, 1),
                new Block(15f, 0f, 1),
                new Block(15f, 1f, 1),
                new Block(15f, 2f, 1),

                new Block(10.5f, 4.5f, 1),    
                new Block(12f, 4f, 1),

                new Block(7f, 4f, 1),

                new Block(0f, 4f, 1),
                new Block(0f, 5f, 1),
                new Block(0f, 6f, 1),

                new Block(1f, 4f, 1),
                new Block(2f, 4f, 1),
                new Block(3f, 4f, 1),
                new Block(4f, 4f, 1),

                new Block(2f, 6f, 1),
                new Block(2f, 7f, 1),
                new Block(5f, 7f, 1),
                new Block(3f, 7f, 2),
                new Block(4f, 7f, 2),
                
                new Block(7.5f, 7.5f, 1),

                new Block(10f, 8f, 1),
                new Block(14f, 8f, 1),
                new Block(11f, 8f, 2),
                new Block(12f, 8f, 2),
                new Block(13f, 8f, 2),

                new Block(15f, 10f, 3),
            };

            public Form1()
            {
                InitializeComponent();

                this.ClientSize = new Size(800, 600);
                this.DoubleBuffered = true;
                this.KeyPreview = true;
                this.BackColor = Color.FromArgb(151, 59, 97);
                this.Text = "Simple Platformer";


                gameTimer = new System.Timers.Timer(16);
                gameTimer.Elapsed += OnGameUpdate;
                gameTimer.AutoReset = true;
                gameTimer.Start();

                lastUpdateTime = DateTime.Now;
            }

            private void OnGameUpdate(object sender, ElapsedEventArgs e)
            {
                DateTime now = DateTime.Now;
                double deltaTime = (now - lastUpdateTime).TotalSeconds;
                lastUpdateTime = now;

                
                if (canJump){coyoteTimeCounter = coyoteTime;}
                else{coyoteTimeCounter -= (float)deltaTime;}

                bool moveLeft = GetAsyncKeyState(Keys.A) < 0 || GetAsyncKeyState(Keys.Left) < 0;
                bool moveRight = GetAsyncKeyState(Keys.D) < 0 || GetAsyncKeyState(Keys.Right) < 0;
                bool jumpPressed = GetAsyncKeyState(Keys.Space) < 0 || GetAsyncKeyState(Keys.Up) < 0;

                float moveX = 0f;
                if (moveLeft) moveX -= speed * (float)deltaTime;
                if (moveRight) moveX += speed * (float)deltaTime;

                if (jumpPressed && !wasJumpPressed && coyoteTimeCounter > 0f)
                {
                    force = jumpForce;
                    canJump = false;
                    coyoteTimeCounter = 0f;
                }
                wasJumpPressed = jumpPressed;

                force -= gravity * (float)deltaTime;
                float moveY = force * (float)deltaTime;

                playerPosition.X += moveX;
                foreach (var block in Blocks)
                {
                    if (IsColliding(playerPosition, block) && block.type == 1)
                    {
                        if (moveX > 0)
                            playerPosition.X = block.positionX - 1;
                        else if (moveX < 0)
                            playerPosition.X = block.positionX + 1;
                    }
                }

                playerPosition.Y += moveY;
                canJump = false;
                foreach (var block in Blocks)
                {
                    if (IsColliding(playerPosition, block))
                    {
                        if (block.type == 2)
                        {
                            playerPosition = spawnPoint;
                            force = 0;
                            break;       
                        } 
                        else if (block.type == 3)
                        {  
                            NextLevel();    
                        }
                        else if (moveY > 0)
                        {
                            playerPosition.Y = block.positionY - 1;
                            force = 0;
                        }
                        else if (moveY < 0)
                        {
                            playerPosition.Y = block.positionY + 1;
                            force = 0;
                            canJump = true;
                        }
                    }
                }

                if (playerPosition.Y < -2)
                {
                    playerPosition = spawnPoint;
                    force = 0;
                }

                Invalidate();
            }

            private bool IsColliding(Vector2 playerPosition, Block block)
            {
                return Math.Abs(block.positionX - playerPosition.X) < 1 &&
                    Math.Abs(block.positionY - playerPosition.Y) < 1;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;

                using (Brush outlineBrush = new SolidBrush(Color.FromArgb(97, 25, 63)))
                {
                    g.FillRectangle(outlineBrush, playerPosition.X * 50, 550 - playerPosition.Y * 50, 45, 45);
                }

                using (Brush fillBrush = new SolidBrush(Color.FromArgb(246, 126, 125)))
                {
                    g.FillRectangle(fillBrush, playerPosition.X * 50 + 5, 550 - playerPosition.Y * 50 + 5, 35, 35);
                }

                foreach (var block in Blocks)
                {
                    using (Brush outlineBrush = new SolidBrush(Color.FromArgb(97, 25, 63)))
                    {
                        g.FillRectangle(outlineBrush, block.positionX * 50 - 5, 550 - block.positionY * 50 - 5, 55, 55);
                    }

                    using (Brush fillBrush = new SolidBrush(Color.FromArgb(246, 126, 125)))
                    {
                        g.FillRectangle(fillBrush, block.positionX * 50, 550 - block.positionY * 50, 45, 45);
                    }

                    if (block.type == 2)
                    {
                        using (Brush fillBrush = new SolidBrush(Color.FromArgb(195, 14, 89)))
                        {
                            g.FillRectangle(fillBrush, block.positionX * 50, 550 - block.positionY * 50, 45, 45);
                        }
                    }
                    if (block.type == 3)
                    {
                        using (Brush fillBrush = new SolidBrush(Color.FromArgb(164, 180, 101)))
                        {
                            g.FillRectangle(fillBrush, block.positionX * 50, 550 - block.positionY * 50, 45, 45);
                        }
                    }
                }
            }
            
            private void NextLevel()
            {
                level ++;
                if (level ==2)
                {
                    spawnPoint = new Vector2(14, 2);
                    playerPosition = spawnPoint;
                    Blocks.Clear();
                    Blocks.AddRange(new List<Block>
                    {
                    new Block(8.5f, 0.5f, 1), new Block(8.5f, 1.5f, 1), new Block(7.5f, 1.5f, 1), new Block(9.5f, 1.5f, 1),

                    new Block(12.5f, 3.5f, 1), new Block(13.5f, 3.5f, 1), new Block(14.5f, 3.5f, 1), new Block(15.3f, 5.5f, 1),

                    new Block(11.0f, 6.5f, 1),new Block(10.0f, 6.5f, 1),new Block(9.0f, 6.5f, 1),new Block(10.0f, 7.2f, 2),


                    new Block(0.5f, 5.5f, 1), new Block(1.5f, 5.5f, 1),  new Block(2.5f, 5.5f, 1), 

                    new Block(2f, 2.5f, 2), new Block(1.5f, 2.5f, 1), new Block(4f, 2.5f, 1), new Block(2f, 2.5f, 2), new Block(3f, 2.5f, 2), 

                    new Block(5.5f, 7.5f, 1),new Block(5.5f, 6.5f, 1),new Block(5.5f, 5.5f, 1),new Block(5.5f, 4.5f, 1),new Block(5.5f, 3.5f, 1),new Block(5.5f, 2.5f, 1),new Block(5.5f, 1.5f, 1),new Block(5.5f, 0.5f, 1),

                    new Block(2f, 8f, 1),new Block(1.8f, 8f, 2),new Block(2f, 8f, 1),new Block(3f, 8f, 1),new Block(4f, 8f, 1), new Block(5f, 8f, 1), new Block(6f, 8f, 1), new Block(-0.3f, 8f, 2),
                                
                    new Block(-0.5f, -0.5f, 1), new Block(0.5f, -0.5f, 1), new Block(1.5f, -0.5f, 1), new Block(2.5f, -0.5f, 1), new Block(3.5f, -0.5f, 1), new Block(4.5f, -0.5f, 1), 
                    new Block(5.5f, -0.5f, 1),  new Block(6.5f, -0.5f, 1), new Block(7.5f, -0.5f, 1), new Block(8.5f, -0.5f, 1), new Block(9.5f, -0.5f, 1), new Block(10.5f, -0.5f, 1),
                    new Block(11.5f, -0.5f, 1), new Block(12.5f, -0.5f, 1),new Block(13.5f, -0.5f, 1),new Block(14.5f, -0.5f, 1),new Block(15.5f, -0.5f, 1),

                    new Block(-0.5f, 11.5f, 1), new Block(0.5f, 11.5f, 1), new Block(1.5f, 11.5f, 1), new Block(2.5f, 11.5f, 1), new Block(3.5f, 11.5f, 1), new Block(4.5f, 11.5f, 1),
                    new Block(5.5f, 11.5f, 1),  new Block(6.5f, 11.5f, 1), new Block(7.5f, 11.5f, 1), new Block(8.5f, 11.5f, 1), new Block(9.5f, 11.5f, 1), new Block(10.5f, 11.5f, 1),
                    new Block(11.5f, 11.5f, 1), new Block(12.5f, 11.5f, 1),new Block(13.5f, 11.5f, 1),new Block(14.5f, 11.5f, 1),new Block(15.5f, 11.5f, 1),


                    new Block(15.5f, 0.5f, 1), new Block(15.5f, 1.5f, 1), new Block(15.5f, 2.5f, 1), new Block(15.5f, 3.5f, 1),
                    new Block(15.5f, 4.5f, 1), new Block(15.5f, 5.5f, 1), new Block(15.5f, 6.5f, 1), new Block(15.5f, 7.5f, 1),
                    new Block(15.5f, 8.5f, 1), new Block(15.5f, 9.5f, 1), new Block(15.5f, 10.5f, 1),

                    new Block(-0.5f, 0.5f, 1), new Block(-0.5f, 1.5f, 1), new Block(-0.5f, 2.5f, 1), new Block(-0.5f, 3.5f, 1),
                    new Block(-0.5f, 4.5f, 1), new Block(-0.5f, 5.5f, 1), new Block(-0.5f, 6.5f, 1), new Block(-0.5f, 7.5f, 1),
                    new Block(-0.5f, 8.5f, 1), new Block(-0.5f, 9.5f, 1), new Block(-0.5f, 10.5f, 1),

                    new Block(3, 1, 3),
                    });
                }
                
                if (level ==3)
                {
                    
                    Blocks.Clear();
                    Blocks.AddRange(new List<Block>
                    {
                    new Block(2f, 2f, 1), new Block(3f, 2f, 1), new Block(4f, 2f, 1), new Block(5f, 2f, 1), new Block(6f, 2f, 1), new Block(7f, 2f, 1),
                    new Block(8f, 2f, 1), new Block(9f, 2f, 1), new Block(10f, 2f, 1), new Block(11f, 2f, 1), new Block(12f, 2f, 1), new Block(13f, 2f, 1),

                    new Block(1.5f, 1.5f, 1), new Block(2.5f, 1.5f, 1), new Block(3.5f, 1.5f, 1), new Block(4.5f, 1.5f, 1), new Block(5.5f, 1.5f, 1), new Block(6.5f, 1.5f, 1),
                    new Block(7.5f, 1.5f, 1), new Block(8.5f, 1.5f, 1), new Block(9.5f, 1.5f, 1), new Block(10.5f, 1.5f, 1), new Block(11.5f, 1.5f, 1), new Block(12.5f, 1.5f, 1),
                    new Block(13.5f, 1.5f, 1),

                    new Block(1f, 1f, 1), new Block(2f, 1f, 1), new Block(3f, 1f, 1), new Block(4f, 1f, 1), new Block(5f, 1f, 1), new Block(6f, 1f, 1),
                    new Block(7f, 1f, 1), new Block(8f, 1f, 1), new Block(9f, 1f, 1), new Block(10f, 1f, 1), new Block(11f, 1f, 1), new Block(12f, 1f, 1),
                    new Block(13f, 1f, 1), new Block(14f, 1f, 1), 

                    new Block(0.5f, 0.5f, 1), new Block(1.5f, 0.5f, 1), new Block(2.5f, 0.5f, 1), new Block(3.5f, 0.5f, 1), new Block(4.5f, 0.5f, 1), new Block(5.5f, 0.5f, 1),
                    new Block(6.5f, 0.5f, 1), new Block(7.5f, 0.5f, 1), new Block(8.5f, 0.5f, 1), new Block(9.5f, 0.5f, 1), new Block(10.5f, 0.5f, 1), new Block(11.5f, 0.5f, 1),
                    new Block(12.5f, 0.5f, 1), new Block(13.5f, 0.5f, 1), new Block(14.5f, 0.5f, 1),

                    new Block(0f, 0f, 1), new Block(1f, 0f, 1), new Block(2f, 0f, 1), new Block(3f, 0f, 1), new Block(4f, 0f, 1), new Block(5f, 0f, 1),
                    new Block(6f, 0f, 1), new Block(7f, 0f, 1), new Block(8f, 0f, 1), new Block(9f, 0f, 1), new Block(10f, 0f, 1), new Block(11f, 0f, 1),
                    new Block(12f, 0f, 1), new Block(13f, 0f, 1), new Block(14f, 0f, 1), new Block(15f, 0f, 1),

                    new Block(-0.5f, -0.5f, 1), new Block(0.5f, -0.5f, 1), new Block(1.5f, -0.5f, 1), new Block(2.5f, -0.5f, 1), new Block(3.5f, -0.5f, 1), new Block(4.5f, -0.5f, 1),
                    new Block(5.5f, -0.5f, 1), new Block(6.5f, -0.5f, 1), new Block(7.5f, -0.5f, 1), new Block(8.5f, -0.5f, 1), new Block(9.5f, -0.5f, 1), new Block(10.5f, -0.5f, 1),
                    new Block(11.5f, -0.5f, 1), new Block(12.5f, -0.5f, 1), new Block(13.5f, -0.5f, 1), new Block(14.5f, -0.5f, 1), new Block(15.5f, -0.5f, 1)

                    
                    });
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
