using System;
using SFML.Graphics;
using SFML.Window;
using Box2DX;

namespace GameProject2D
{
    public class Player
    {
        RectangleShape sprite;
        Vector2 position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2 movement { get; set; }
        Vector2 size { get { return sprite.Size; } set { sprite.Size = value; } }

        public Player(Vector2 position)
        {
            this.sprite = new RectangleShape(new Vector2(1F, 1F));
            this.sprite.FillColor = Color.Black;

            this.position = position;
            this.movement = new Vector2(0F, 0F);
            
            this.size = new Vector2(100F, 100F);
        }

        public void update()
        {
            float deltaTime = (float)Program.gameTime.EllapsedTime.TotalSeconds;
            float speed = 1F * deltaTime;
            
            Vector2 inputMovement = new Vector2(0F, 0F);

            inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Down) ? speed : 0F;
            inputMovement.Y += Keyboard.IsKeyPressed(Keyboard.Key.Up) ? -speed : 0F;

            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Left) ? -speed : 0F;
            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Right) ? speed : 0F;

            if(inputMovement.Y != 0F || inputMovement.X != 0F)
            {
                movement += inputMovement * speed / (float)Math.Sqrt(inputMovement.X * inputMovement.X + inputMovement.Y * inputMovement.Y);
            }

            movement *= (1F - deltaTime * 4F);    // friction

            position += movement;
        }

        public void draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }
}
