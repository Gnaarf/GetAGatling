
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
