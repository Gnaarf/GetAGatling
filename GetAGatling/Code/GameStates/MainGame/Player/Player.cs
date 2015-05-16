using System;
using SFML.Graphics;
using SFML.Window;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;
using System.Collections.Generic;

namespace GameProject2D
{
    public class Player : GameObject, IContactCallback
    {
        Sprite sprite;
        public override Vector2 midPoint { get { return body.GetPosition(); } }
        public Vector2 movement { get { return body.GetLinearVelocity(); } private set { body.SetLinearVelocity(value); } }
        public Vector2 size;
        public Body body { get; private set; }
        List<Box2DX.Collision.Shape> collisionShapes = new List<Box2DX.Collision.Shape>();

        PlayerController controller;

        public Player(World world, Vector2 midPoint)
        {
            sprite = new Sprite(new Texture("Textures/pixel.png"));
            sprite.Origin = ((Vector2)sprite.Texture.Size) / 2F;
            sprite.Color = SFML.Graphics.Color.Black;
            
            this.size = new Vector2(1F, 1F);

            // controller
            controller = new KeyboardController();

            // Physics init
            BodyDef bodydef = new BodyDef();
            bodydef.Position.Set(midPoint.X, midPoint.Y);
            body = world.CreateBody(bodydef);

            CircleDef circleDef = new CircleDef();
            circleDef.Radius = 0.5F;
            circleDef.Density = 1.0F;
            circleDef.Friction = 1.0F;

            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox(this.size.X / 2F, this.size.Y / 2F);
            shapeDef.Density = 1.0f;
            shapeDef.Friction = 1.0f;

            body.SetUserData(this);
            body.CreateShape(circleDef);
            body.SetMassFromShapes();
        }

        public void update()
        {
            // remove outdated collisions
            List<Box2DX.Collision.Shape> cachedShapesToBeDeleted = new List<Box2DX.Collision.Shape>();
            foreach(Box2DX.Collision.Shape shape in collisionShapes)
            {
                Box2DX.Common.Vec2[] contactPoints = new Box2DX.Common.Vec2[2];
                float distance = Collision.Distance(out contactPoints[0], out contactPoints[1], this.body.GetShapeList(), this.body.GetXForm(), shape, shape.GetBody().GetXForm());

                if(distance > 0.1F)
                {
                    cachedShapesToBeDeleted.Add(shape);
                }
                collisionRectangle.Position = ((Vector2)contactPoints[0]).PixelCoord;
            }
            foreach(Box2DX.Collision.Shape shape in cachedShapesToBeDeleted)
            {
                collisionShapes.Remove(shape);
            }

            // movement
            float deltaTime = (float)Program.gameTime.EllapsedTime.TotalSeconds;
            float speedFactor = 20F * deltaTime;

            ControllerInput input = controller.update(this);

            Vector2 inputMovement = Vector2.Zero;
            
            inputMovement.X += input.xMovement;

            if(input.startJumping && collisionShapes.Count != 0)
            {
                inputMovement += Vector2.Up * 250F;
            }

            if(inputMovement != Vector2.Zero)
            {
                movement += inputMovement * speedFactor;
            }

            movement *= (1F - deltaTime * 4F);    // friction
        }

        RectangleShape collisionRectangle = new RectangleShape(new Vector2(5, 5));
                    
        public void draw(RenderWindow win, View view)
        {
            // Draw Player
            sprite.Position = midPoint.PixelCoord;
            sprite.Scale = size.PixelCoord / sprite.Texture.Size;
            sprite.Rotation = body.GetAngle() * Helper.RadianToDegree;
            win.Draw(sprite);

            //
            collisionRectangle.Size = new Vector2(5, 5);
            collisionRectangle.Origin = new Vector2(3, 3);
            collisionRectangle.FillColor = SFML.Graphics.Color.Red;
             
                foreach(Box2DX.Collision.Shape shape in collisionShapes)
                {
                    Box2DX.Common.Vec2[] contactPoints = new Box2DX.Common.Vec2[2];
                    float distance = Collision.Distance(out contactPoints[0], out contactPoints[1], this.body.GetShapeList(), this.body.GetXForm(), shape, shape.GetBody().GetXForm());

                    collisionRectangle.Position = ((Vector2)contactPoints[0]).PixelCoord;
                    win.Draw(collisionRectangle);
                    collisionRectangle.Position = ((Vector2)contactPoints[1]).PixelCoord;
                    //win.Draw(collisionRectangle);
                }
        }

        public void OnContact(Box2DX.Collision.Shape other, ContactPoint point)
        {
            if(!collisionShapes.Contains(other))
            {
                collisionShapes.Add(other);
            }
        }


        public void OnContactRemove(Box2DX.Collision.Shape other, ContactPoint point)
        {
        }
    }
}
