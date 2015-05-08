using Box2DX.Collision;
using Box2DX.Dynamics;
using SFML.Graphics;
using System;

namespace GameProject2D
{
    class Platform : GameObject
    {
        Body body;
        Sprite sprite;

        public override Vector2 position { get { return body.GetPosition(); } }

        public Platform(World world, String texture, float positionX, float positionY, float height, float width)
            : this(world, texture, new Vector2(positionX, positionY), new Vector2(height, width))
        {
        }

        public Platform(World world, String texture,Vector2 position, Vector2 size)
        {
            BodyDef bodydef = new BodyDef();
            bodydef.Position = position;

            body = world.CreateBody(bodydef);
            
            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox( size.X, size.Y);
            shapeDef.Density = 0.0f;
            shapeDef.Friction = 0.0f;

            Texture tex = new Texture(texture);
            
            sprite = new Sprite(tex) { Position = position.PixelCoord };

            body.CreateShape(shapeDef);
            body.SetMassFromShapes();
        }

        public void update() 
        {
        }

        public void draw (RenderWindow win, View view)
        {
            sprite.Position = new Vector2(body.GetPosition().X, body.GetPosition().Y);

            AABB aabb;
            body.GetShapeList().ComputeAABB(out aabb, body.GetXForm());
            //Vector2 size = aabb.UpperBound - aabb.LowerBound;
            //sprite.Scale = size.PixelCoord / (Vector2)sprite.Texture.Size;
            
            win.Draw(sprite);
        }
    }
}
