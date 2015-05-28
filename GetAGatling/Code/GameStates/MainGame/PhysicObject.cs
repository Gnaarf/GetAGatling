using Box2DX.Collision;
using Box2DX.Dynamics;
using SFML.Graphics;
using System;

namespace GameProject2D
{
    public abstract class PhysicObject : GameObject
    {
        public override Vector2 midPoint { get { return body.GetPosition(); } protected set { throw new Exception("can't set Position of a PhysicObject"); } }

        public Body body { get; protected set; }

        public Vector2 movement { get { return body.GetLinearVelocity(); } protected set { body.SetLinearVelocity(value); } }
    }
}
