using System;
using SFML.Graphics;
using SFML.Window;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;

namespace GameProject2D
{
    public class Player : GameObject
    {
        Sprite sprite;
        public override Vector2 midPoint { get { return body.GetPosition(); } }
        public Vector2 movement { get { return body.GetLinearVelocity(); } private set { body.SetLinearVelocity(value); } }
        public Vector2 size;
        Body body;

        public Player(World world, Vector2 midPoint)
        {
            sprite = new Sprite(new Texture("Textures/pixel.png"));
            sprite.Origin = ((Vector2)sprite.Texture.Size) / 2F;
            sprite.Color = SFML.Graphics.Color.Black;

            this.size = new Vector2(1F, 1F);

            BodyDef bodydef = new BodyDef();
            bodydef.Position.Set(midPoint.X, midPoint.Y);
            body = world.CreateBody(bodydef);
            
            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox(this.size.X / 2F, this.size.Y / 2F);
            shapeDef.Density = 1.0f;
            shapeDef.Friction = 1.0f;


            body.SetUserData(sprite);
            body.CreateShape(shapeDef);
            body.SetMassFromShapes();
        }

        public void update()
        {
            float deltaTime = (float)Program.gameTime.EllapsedTime.TotalSeconds;
            float speedFactor = 20F * deltaTime;
            
            Vector2 inputMovement = new Vector2(0F, 0F);

            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Left) ? -1 : 0F;
            inputMovement.X += Keyboard.IsKeyPressed(Keyboard.Key.Right) ? 1 : 0F;

            if(Keyboard.IsKeyPressed(Keyboard.Key.Up) && body.GetWorld().GetContactCount() != 0)  //HACK
            {
                inputMovement += Vector2.Up * 25F;
                Console.WriteLine("Jump");
            }

            if(inputMovement != Vector2.Zero)
            {
                movement += inputMovement * speedFactor;
            }

            movement *= (1F - deltaTime * 4F);    // friction

            //Console.WriteLine(position);
        }

        public void draw(RenderWindow win, View view)
        {
            sprite.Position = midPoint.PixelCoord;
            sprite.Scale = size.PixelCoord / sprite.Texture.Size;
            sprite.Rotation = body.GetAngle() * Helper.RadianToDegree;
            win.Draw(sprite);
        }
    }
}
