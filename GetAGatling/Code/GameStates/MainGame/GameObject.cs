using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameProject2D
{
    [DataContract]
    public abstract class GameObject
    {
        public virtual Vector2 midPoint{ get; protected set; }

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
