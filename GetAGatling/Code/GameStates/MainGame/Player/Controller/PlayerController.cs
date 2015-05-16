using System;
using SFML.Graphics;
using SFML.Window;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;
using System.Collections.Generic;

namespace GameProject2D
{
    public abstract class PlayerController
    {
        protected ControllerInput input;

        protected PlayerController()
        {
            input = new ControllerInput();
            input.xMovement = 0F;
            input.aimDirection = Vector2.Right;
            input.jumpingButtonDown = false;
        }

        public abstract ControllerInput update(Player player);
    }
}
