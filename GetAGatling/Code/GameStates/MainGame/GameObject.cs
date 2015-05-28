using Box2DX.Collision;
using Box2DX.Dynamics;
using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace GameProject2D
{
    public abstract class GameObject
    {
        public abstract Vector2 midPoint { get; protected set; }

        public static Vector2 getAveragePosition(IEnumerable<GameObject> gameObjects)
        {
            Vector2 avg = Vector2.Zero;
            int count = 0;
            foreach(GameObject o in gameObjects)
            {
                avg += o.midPoint;
                count++;
            }

            if(count == 0)
            {
                return Vector2.Zero;
            }
            return avg / count;
        }
    }
}
