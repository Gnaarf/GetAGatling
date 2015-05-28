using System;
using SFML.Graphics;
using SFML.Window;
using Box2DX;
using Box2DX.Dynamics;
using Box2DX.Collision;
using System.Collections.Generic;

namespace GameProject2D
{
    public class PlayerManager
    {
        enum ControllDevice
        {
            Keyboard,
            GamePad,
            Network
        }

        struct ControllerKey
        {
            public uint index;
            public ControllDevice device;

            public ControllerKey(uint index, ControllDevice device)
            {
                this.index = index;
                this.device = device;
            }
        }

        Dictionary<ControllerKey, Player> players = new Dictionary<ControllerKey, Player>();

        public Player[] currentPlayers 
        { 
            get 
            { 
                Player[] p = new Player[players.Count];
                players.Values.CopyTo(p, 0);
                return p;
            } 
        }

        public PlayerManager()
        {
        }

        public void update(World world)
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                ControllerKey controllerKey = new ControllerKey(0, ControllDevice.GamePad);
                if (!players.ContainsKey(controllerKey))
                {
                    players[controllerKey] = new Player(world, Vector2.Zero, new KeyboardController());
                }
            }


            GamePadInputManager padInputManager = Program.gamePadInputManager;

            foreach(uint padIndex in padInputManager.connectedPadIndices)
            {
                if(padInputManager.isClicked(GamePadButton.Start, padIndex))
                {
                    ControllerKey controllerKey = new ControllerKey(padIndex, ControllDevice.GamePad);
                    if(!players.ContainsKey(controllerKey))
                    {
                        players[controllerKey] = new Player(world, Vector2.Zero, new GamePadController(padIndex));
                    }
                }
            }

            foreach(Player player in players.Values)
            {
                player.update();
            }
        }

        public void draw(RenderWindow win, View view)
        {
            foreach (Player player in players.Values)
            {
                player.draw(win, view);
            }
        }
    }
}
