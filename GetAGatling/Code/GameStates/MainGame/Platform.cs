using Box2DX.Collision;
using Box2DX.Dynamics;
using SFML.Graphics;
using System;

namespace GameProject2D
{
    class Platform
    {
        Body body;
        Sprite sprite;

        public Platform(World world, String texture,float positionX, float positionY, float heightX, float heightY) 
        {
            BodyDef bodydef = new BodyDef();
            bodydef.Position.Set(positionX / 30.0F, positionY / 30.0F);

            body = world.CreateBody(bodydef);
            
            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox( ( heightX )/ 30.0F , ( heightY ) / 30.0F );
            shapeDef.Density = 0.0f;
            shapeDef.Friction = 0.0f;

            Texture tex = new Texture(texture);
            
            sprite = new Sprite(tex) { Origin = new Vector2(heightX , heightY) };

            body.SetUserData(sprite);
            body.CreateShape(shapeDef);
            body.SetMassFromShapes();
        }

        public void update() 
        {
            Sprite sprite = (Sprite)body.GetUserData();
            sprite.Position = new Vector2( body.GetPosition().X,  body.GetPosition().Y);
        }

        public void draw (RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }
}
