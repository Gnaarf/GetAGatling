using Box2DX.Collision;
using Box2DX.Dynamics;
using SFML.Graphics;
using System;

namespace GameProject2D
{
    public abstract class GameObject
    {
        public abstract Vector2 midPoint { get; protected set; }
    }
}
