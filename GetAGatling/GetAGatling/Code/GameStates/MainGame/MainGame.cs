using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class InGame : IGameState
    {
        Player player;
        
        public InGame()
        {
            player = new Player(new Vector2f(10F, 10F));
        }

        public GameState update()
        {
            player.update();
            return GameState.InGame;
        }

        public void draw(RenderWindow win, View view)
        {
            player.draw(win, view);
        }
    }
}
