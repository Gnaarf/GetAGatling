
using System;
namespace GameProject2D
{
    public class GamePadController : PlayerController
    {
        float deadZone = 0.1F;

        uint? padIndex = null;

        static bool[] occupied;

        public GamePadController()
            : base()
        {
            if (occupied == null)
            {
                // initialized with false, for no Controller is registered to a pad yet
                occupied = new bool[Program.gamePadInputManager.numSupportedPads];
            }

            if (!padIndex.HasValue)
            {
                for (int i = 0; i < Program.gamePadInputManager.numConnectedPads; ++i)
                {
                    if(!occupied[i])
                    {
                        padIndex = (uint)i;
                        occupied[i] = true;
                    }
                }
            }
        }

        public override ControllerInput update(Player player)
        {
            // Movement
            input.xMovement = Program.gamePadInputManager.getLeftStick(padIndex.Value).X;
            if(Math.Abs(input.xMovement) < deadZone)
            {
                input.xMovement = 0F;
            }

            // Aiming
            input.aimDirection = Program.gamePadInputManager.getRightStick(padIndex.Value);

            // Jumping
            input.jumpingButtonDown = Program.gamePadInputManager.isPressed(GamePadButton.A, padIndex.Value);

            return input;
        } 
    }
}
