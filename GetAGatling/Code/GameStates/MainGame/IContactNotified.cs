﻿using Box2DX.Collision;
using Box2DX.Dynamics;

namespace GameProject2D
{
    interface IContactNotified
    {
        void OnContact(Shape other, ContactPoint point);
        void OnContactRemove(Shape other, ContactPoint point);
    }
}
