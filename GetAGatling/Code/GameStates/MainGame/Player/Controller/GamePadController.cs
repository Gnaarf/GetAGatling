
using System;
namespace GameProject2D
{
    public class GamePadController{/* : PlayerController
    {
        
        GamePadInputManager pad = new GamePadInputManager();
        float deadZone = 0.1F;

        public GamePadController()
            : base()
        {
        }

        public override ControllerInput update(Player player)
        {
            pad.update();

            // Movement
            input.xMovement = pad.getLeftStick().X / 100F;
            if(Math.Abs(input.xMovement) < deadZone)
            {
                input.xMovement = 0F;
            }

            // Aiming
            input.aimDirection = pad.getRightStick();

            // Jumping
            input.jumpingButtonDown = pad.isPressed(GamePadButton.A);

            return input;
        } */
    }
}
