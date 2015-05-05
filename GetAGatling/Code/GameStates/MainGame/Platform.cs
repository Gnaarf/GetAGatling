using Box2DX.Collision;
using Box2DX.Dynamics;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject2D
{
    class Platform
    {
        BodyDef bodydef;
        Body body;
        PolygonDef shapeDef;
        Sprite sprite;
        public Platform(World world, String texture,float positionX, float positionY, float heightX, float heightY) 
        {
            bodydef = new BodyDef();
            bodydef.Position.Set(positionX / 30.0F, positionY / 30.0F);

            body = world.CreateBody(bodydef);
            shapeDef = new PolygonDef();
            shapeDef.SetAsBox((heightX / 2) / 30.0f, (heightY / 2) / 30.0f);
            shapeDef.Density = 0.0f;

            //sprite = new Sprite(new Texture(texture)) { Origin = new Vector2(heightX / 2, heightY / 2) };

//            body.SetUserData(sprite);

            body.CreateShape(shapeDef);
            body.SetMassFromShapes();
        }

        public void update() { }
        public void draw (RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }
}
