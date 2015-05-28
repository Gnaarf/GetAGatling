using Box2DX.Collision;
using Box2DX.Dynamics;
using SFML.Graphics;
using System;

namespace GameProject2D
{
    class Platform : PhysicObject
    {
        Sprite sprite;

        Vector2 size;

        public Platform(World world, String texture, float midPointX, float midPointY, float width, float height, float angle)
            : this(world, texture, new Vector2(midPointX, midPointY), new Vector2(width, height), angle)
        {
        }

        public Platform(World world, String texture,Vector2 midPoint, Vector2 size, float angle)
        {
            BodyDef bodydef = new BodyDef();
            bodydef.Position = midPoint;
            bodydef.Angle = angle;

            body = world.CreateBody(bodydef);
            
            PolygonDef shapeDef = new PolygonDef();
            this.size = size;

            // SetAsBox expects radius
            shapeDef.SetAsBox( size.X / 2F, size.Y / 2F);
            shapeDef.Density = 0.0f;
            shapeDef.Friction = 1.0f;

            Texture tex = new Texture(texture);
            
            sprite = new Sprite(tex);
            sprite.Origin = ((Vector2)sprite.Texture.Size) / 2F;
            sprite.Position = midPoint.PixelCoord;

            body.CreateShape(shapeDef);
            body.SetMassFromShapes();

        }

        public virtual void update()
        {
        }

        public virtual void draw(RenderWindow win, View view)
        {
            sprite.Position = midPoint.PixelCoord;
            sprite.Scale = size.PixelCoord / sprite.Texture.Size;
            sprite.Rotation = body.GetAngle() * Helper.RadianToDegree;

            win.Draw(sprite);
        }
    }
}
