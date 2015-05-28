using Box2DX.Collision;
using Box2DX.Dynamics;
using SFML.Graphics;
using System;

namespace GameProject2D
{
    class MovingPlatform : Platform
    {
        Vector2 startPosition;
        Vector2 movementDirection;
        float movementDistance;
        float speed;

        public MovingPlatform(World world, String texture, Vector2 midPoint, Vector2 size, float angle, Vector2 movementEndPoint, float speed)
            : base(world, texture, midPoint, size, angle)
        {
            this.startPosition = midPoint;
            this.movementDistance = Vector2.distance(midPoint, movementEndPoint);
            this.movementDirection = (movementEndPoint - midPoint).normalize();
            this.speed = speed;

            // bodies without mass cant be applied forces. Therefore MovingPlatform gets mass
            MassData massData = new MassData();
            massData.Center = midPoint;
            massData.Mass = 10000F;
            body.SetMass(massData);
        }

        public override void update()
        {
            if ((speed > 0F && Vector2.dot(midPoint - startPosition, movementDirection) > movementDistance)
                ||(speed < 0F && Vector2.dot(midPoint - startPosition, movementDirection) < 0F))
            {
                speed *= -1F;
            }

            body.SetLinearVelocity(speed * movementDirection);
        }
    }
}
