
using System;
namespace GameProject2D
{
    public class GamePadController : PlayerController
    {
        uint padIndex;

        public GamePadController(uint padIndex)
            : base()
        {
            this.padIndex = padIndex;
        }

        public override ControllerInput update(Player player)
        {
            // Movement
            input.xMovement = Program.gamePadInputManager.getLeftStick(padIndex).X;

            // Aiming
            input.aimDirection = Program.gamePadInputManager.getRightStick(padIndex);

            // Jumping
            input.jumpingButtonDown = Program.gamePadInputManager.isPressed(GamePadButton.A, padIndex);

            return input;
        } 
    }
}
