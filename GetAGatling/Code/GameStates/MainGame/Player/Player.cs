using System;
using SFML.Graphics;
using SFML.Window;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;

namespace GameProject2D
{
    public class Player
    {
        RectangleShape sprite;
        Vector2 position { get { return sprite.Position; } set { sprite.Position = value; } }
        Vector2 movement { get; set; }
        Vector2 size { get { return sprite.Size; } set { sprite.Size = value; } }
        Body body;
        public Player(World world, Vector2 position)
        {
            
            this.sprite = new RectangleShape(new Vector2(1F, 1F));
            this.sprite.FillColor = SFML.Graphics.Color.Black;

            this.position = position;
            this.movement = new Vector2(0F, 0F);
            
            this.size = new Vector2(100F, 100F);


            BodyDef bodydef = new BodyDef();
            bodydef.Position.Set(position.X / 30.0F, position.Y / 30.0F);
            body = world.CreateBody(bodydef);

            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox((this.size.X) / 30.0F, (this.size.Y) / 30.0F);
            shapeDef.Density = 1.0f;
            shapeDef.Friction = 0.0f;


            body.SetUserData(sprite);
            body.CreateShape(shapeDef);
            body.SetMassFromShapes();
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
