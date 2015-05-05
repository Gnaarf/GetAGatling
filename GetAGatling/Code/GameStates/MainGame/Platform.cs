using Box2DX.Collision;
using Box2DX.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAGatling.Code.GameStates.MainGame
{
    class Platform
    {
        BodyDef bodydef;
        Body body;
        PolygonDef shapedef;

        public Platform(World world, float positionX, float positionY, float heightX, float heightY) 
        {
            bodydef = new BodyDef();
            bodydef.Position.Set(positionX / 30.0F, positionY / 30.0F);

            body = world.CreateBody(bodydef);
            shapedef.SetAsBox(heightX,heightY);
        }
    }
}
