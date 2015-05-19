using SFML.Window;
using System.Collections.Generic;

namespace GameProject2D
{
    public enum GamePadButton { A, B, X, Y, LB, RB, Select, Start, BUTTONNUM, LT, RT };
    
    class GamePadInputManager
    {
        struct Input
        {
            public Vector2 leftStick;
            public Vector2 rightStick;
            public float LTRT;

            public bool[] oldButton;
            public bool[] currentButton;
        }

        Dictionary<uint, Input> padInputs;

        public readonly int numSupportedPads = 8;

        public int numConnectedPads {get; private set;}

        public GamePadInputManager()
        {
            registerConnectedGamePads();
        }

        public void registerConnectedGamePads()
        {
            padInputs = new Dictionary<uint, Input>();

            Joystick.Update();

            numConnectedPads = 0;

            for (uint i = 0; i < numSupportedPads; i++)
            {
                if (Joystick.IsConnected(i))
                {
                    numConnectedPads++;

                    Input input = new Input();

                    input.oldButton = new bool[(int)GamePadButton.BUTTONNUM];
                    input.currentButton = new bool[(int)GamePadButton.BUTTONNUM];

                    input.leftStick = new Vector2(0, 0);
                    input.rightStick = new Vector2(0, 0);

                    padInputs[i] = input;
                }
            }
        }

        public void update()
        {
            Joystick.Update();

            foreach(KeyValuePair<uint, Input> indexInputPair in padInputs)
            {
                uint index = indexInputPair.Key;
                Input input = indexInputPair.Value;

                for (uint i = 0; i < (uint)GamePadButton.BUTTONNUM; i++)
                {
                    input.oldButton[i] = input.currentButton[i];
                    input.currentButton[i] = Joystick.IsButtonPressed(index, i);
                }

                input.rightStick = 0.01F * new Vector2(Joystick.GetAxisPosition(index, Joystick.Axis.U), -Joystick.GetAxisPosition(index, Joystick.Axis.R));
                input.leftStick = 0.01F * new Vector2(Joystick.GetAxisPosition(index, Joystick.Axis.X), -Joystick.GetAxisPosition(index, Joystick.Axis.Y));
                input.LTRT = Joystick.GetAxisPosition(index, Joystick.Axis.Z);

                padInputs[index] = input;
            }
        }

        public bool isConnected(uint padIndex)
        {
            return padInputs.ContainsKey(padIndex);
        }

        public Vector2 getLeftStick(uint padIndex)
        {
            return padInputs[padIndex].leftStick;
        }
        
        public Vector2 getRightStick(uint padIndex)
        {
            return padInputs[padIndex].rightStick;
        }

        public bool isClicked(GamePadButton button, uint padIndex)
        {
            return padInputs[padIndex].currentButton[(int)button] && !padInputs[padIndex].oldButton[(int)button];
        }

        public bool isPressed(GamePadButton button, uint padIndex)
        {
            if (button == GamePadButton.LT)
                return padInputs[padIndex].LTRT > 50;
            if (button == GamePadButton.RT)
                return padInputs[padIndex].LTRT < -50;

            return padInputs[padIndex].currentButton[(int)button];
        }

        public bool isReleased(GamePadButton button, uint padIndex)
        {
            return !padInputs[padIndex].currentButton[(int)button] && padInputs[padIndex].oldButton[(int)button];
        }
    }
}
