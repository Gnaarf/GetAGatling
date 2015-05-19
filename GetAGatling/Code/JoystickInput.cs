using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject2D
{
//author Gerd
    public enum GamePadButton { A, B, X, Y, LB, RB, Select, Start, BUTTONNUM, LT,RT };
    
    class GamePadInputManager
    {
        /*
        struct input
        {
            Vector2 leftStick;
            Vector2 rightStick;
            float LTRT;

            bool[] oldButton;
            bool[] currentButton;
        }

        Dictionary<uint, input> padInputs;

        int numSupportedPads = 30;

        static uint numConnectedPads = 0;
        
        public GamePadInputManager()
        {
            padInputs = new Dictionary<uint, input>();

            Joystick.Update();

            for (uint i = 0; i < numSupportedPads; i++)
            {
                if (Joystick.GetAxisPosition(i,Joystick.Axis.U) != 0)
                {
                    numConnectedPads++;
                    padInputs[i] = new input();
                }
            }

            oldButton = new bool[(int)GamePadButton.BUTTONNUM];
            currentButton = new bool[(int)GamePadButton.BUTTONNUM];

            leftStick = new Vector2();

            numConnectedPads++;
        }

        public void update()
        {
            Joystick.Update();
            for (int i = 0; i < (int)GamePadButton.BUTTONNUM; i++)
                oldButton[i] = currentButton[i];


            for (int i = 0; i < (int)GamePadButton.BUTTONNUM; i++)
                currentButton[i] = Joystick.IsButtonPressed(padID, (uint)i);

            rightStick = new Vector2(Joystick.GetAxisPosition(padID, Joystick.Axis.U), -Joystick.GetAxisPosition(padID, Joystick.Axis.R));
            leftStick = new Vector2(Joystick.GetAxisPosition(padID, Joystick.Axis.X), -Joystick.GetAxisPosition(padID, Joystick.Axis.Y));
            LTRT = Joystick.GetAxisPosition(padID, Joystick.Axis.Z);

        }
        public Vector2 getLeftStick(uint padIndex)
        {
            return leftStick;
        }
        public Vector2 getRightStick(uint padIndex)
        {
            return rightStick;
        }
        public bool isClicked(GamePadButton button, uint padIndex)
        {
            return currentButton[(int)button] && !oldButton[(int)button];
        }

        public bool isPressed(GamePadButton button)
        {
            if (button == GamePadButton.LT)
                return LTRT > 50;
            if (button == GamePadButton.RT)
                return LTRT < -50;

            return Joystick.IsButtonPressed((uint)1, (uint)button);
        }
        public bool isReleased(GamePadButton button)
        {
            return oldButton[(int)button] && !Joystick.IsButtonPressed(padID, (uint)button);
        }*/
    }
}
