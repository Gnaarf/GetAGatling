using System;
using SFML.Graphics;
using SFML.Window;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;
using System.Collections.Generic;

namespace GameProject2D
{
    public struct ControllerInput
    {
        public float xMovement;
        public Vector2 aimDirection;

        /// <summary>will be set automatically when </summary>
        public bool startJumping { get; private set; }
        bool _jumpingButtonDown;
        /// <summary>needs to be set exactly once per update, to accuratly manage startJumping</summary>
        public bool jumpingButtonDown
        {
            get
            {
                return _jumpingButtonDown;
            }
            set
            {
                startJumping = !_jumpingButtonDown && value;
                _jumpingButtonDown = value;
            }
        }
    }
}
