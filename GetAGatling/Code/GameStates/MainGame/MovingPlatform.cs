using Box2DX.Collision;
using Box2DX.Dynamics;
using SFML.Graphics;
using System;

namespace GameProject2D
{
    class MovingPlatform : Platform
    {
        Vector2 startPosition;
        Vector2 endPosition;
        float speed;
        bool moveForward;

        static float targetRadiusSqr = 0.1F * 0.1F;

        public MovingPlatform(World world, String texture, Vector2 midPoint, Vector2 size, float angle, Vector2 movementEndPoint, float speed)
            : base(world, texture, midPoint, size, angle)
        {
            this.startPosition = midPoint;
            this.endPosition = movementEndPoint;
            this.speed = speed;
            this.moveForward = true;

            // bodies without mass cant be applied forces. Therefore MovingPlatform gets mass
            MassData massData = new MassData();
            massData.Center = midPoint;
            massData.Mass = 0.01F;
            body.SetMass(massData);
        }

        public override void update()
        {
            Vector2 target = getTarget();

            if(Vector2.distanceSqr(midPoint, target) <= targetRadiusSqr)
            {
                moveForward = !moveForward;
                target = getTarget();
            }

            Vector2 toTarget = target - midPoint;

            body.SetLinearVelocity(speed * toTarget.normalize());
        }

        Vector2 getTarget()
        {
            return moveForward ? endPosition : startPosition;
        }
    }
}
