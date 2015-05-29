using System;
using SFML.Graphics;
using SFML.Window;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;
using System.Collections.Generic;

namespace GameProject2D
{
    public class Player : PhysicObject, IContactNotified
    {
        AnimatedSprite sprite;
        public Vector2 size;
        List<Box2DX.Collision.Shape> collisionShapes = new List<Box2DX.Collision.Shape>();
        List<Vector2> collisionPoints = new List<Vector2>();
            
        PlayerController controller;

        public Player(World world, Vector2 midPoint, PlayerController controller)
        {
            // sprite for rendering
            sprite = new AnimatedSprite(new Texture("Textures/Character/idle_00.png"), 0.05F, 20, new Vector2i(62, 50));
            sprite.Origin = ((Vector2)sprite.spriteSize) / 2F;
            sprite.Scale = Vector2.One * 1.4F;
            sprite.restartAnimation(Program.gameTime);

            // controller
            this.controller = controller;

            // set properties
            this.size = new Vector2(1F, 1F);

            // Physics init
            BodyDef bodydef = new BodyDef();
            bodydef.Position.Set(midPoint.X, midPoint.Y);
            body = world.CreateBody(bodydef);

            CircleDef circleDef = new CircleDef();
            circleDef.Radius = 0.5F;
            circleDef.Density = 1.0F;
            circleDef.Friction = 1.0F;
            circleDef.Restitution = 0.0F;

            body.SetUserData(this);
            body.CreateShape(circleDef);
            body.SetMassFromShapes();

            Console.WriteLine(body.GetMass());
        }

        public void update()
        {
            // remove outdated collisions and get current collisionPoints
            collisionPoints.Clear();
            List<Box2DX.Collision.Shape> cachedShapesToBeDeleted = new List<Box2DX.Collision.Shape>();
            foreach(Box2DX.Collision.Shape shape in collisionShapes)
            {
                Box2DX.Common.Vec2[] contactPoints = new Box2DX.Common.Vec2[2];
                float distance = Collision.Distance(out contactPoints[0], out contactPoints[1], this.body.GetShapeList(), this.body.GetXForm(), shape, shape.GetBody().GetXForm());

                if(distance > 0.1F)
                {
                    cachedShapesToBeDeleted.Add(shape);
                }
                collisionPoints.Add(contactPoints[0]);
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

            if(inputMovement != Vector2.Zero)
            {
                movement += inputMovement * speedFactor;
            }

            if (input.startJumping && collisionShapes.Count != 0)
            {
                Vector2 avgCollisionPoint = Vector2.average(collisionPoints.FindAll((p) => (p.Y >= midPoint.Y)).ToArray());

                Vector2 normal = (midPoint - avgCollisionPoint).normalized;

                Vector2 jumpMovement = (Vector2.Up * 2 + normal).normalized * 6F;

                movement = new Vector2(movement.X + jumpMovement.X, jumpMovement.Y);
            }

            float friction = 2F;
            movement = new Vector2(movement.X * (1F - deltaTime * friction), movement.Y);    // friction
        }

        RectangleShape collisionRectangle = new RectangleShape();
                    
        public void draw(RenderWindow win, View view)
        {
            // Draw Player
            sprite.Position = midPoint.PixelCoord;
            sprite.Texture.Smooth = true;
            if ((movement.X > 0.1F && sprite.Scale.X > 0F) || (movement.X < -0.1F && sprite.Scale.X < 0F))
            {
                sprite.Scale = new Vector2(sprite.Scale.X * -1F, sprite.Scale.Y);
            }

            win.Draw(sprite.updateFrame(Program.gameTime));

            //
            collisionRectangle.Size = new Vector2(3, 3);
            collisionRectangle.Origin = new Vector2(1, 1);
            collisionRectangle.FillColor = SFML.Graphics.Color.Red;
             
                foreach(Box2DX.Collision.Shape shape in collisionShapes)
                {
                    Box2DX.Common.Vec2[] contactPoints = new Box2DX.Common.Vec2[2];
                    float distance = Collision.Distance(out contactPoints[0], out contactPoints[1], this.body.GetShapeList(), this.body.GetXForm(), shape, shape.GetBody().GetXForm());
                    
                    collisionRectangle.Position = ((Vector2)contactPoints[0]).PixelCoord;
                    win.Draw(collisionRectangle);
                    collisionRectangle.Position = ((Vector2)contactPoints[1]).PixelCoord;
                    win.Draw(collisionRectangle);
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
