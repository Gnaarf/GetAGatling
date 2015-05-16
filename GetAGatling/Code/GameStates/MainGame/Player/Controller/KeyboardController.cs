using System;
using SFML.Graphics;
using SFML.Window;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;
using System.Collections.Generic;

namespace GameProject2D
{
    public class KeyboardController : PlayerController
    {
        public KeyboardController()
            : base()
        {
        }

        public override ControllerInput update(Player player)
        {
            // Movement
            input.xMovement = 0F;
            input.xMovement += (Keyboard.IsKeyPressed(Keyboard.Key.Left) || Keyboard.IsKeyPressed(Keyboard.Key.A)) ? -1F : 0F;
            input.xMovement += (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.D)) ? 1F : 0F;

            // Aiming
            input.aimDirection = Vector2.Right * input.xMovement;

            // Jumping
            input.jumpingButtonDown = Keyboard.IsKeyPressed(Keyboard.Key.Up) || Keyboard.IsKeyPressed(Keyboard.Key.W);

            return input;
        }    
    }
}
