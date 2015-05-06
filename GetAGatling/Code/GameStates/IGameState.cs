using SFML.Graphics;

namespace GameProject2D
{
    interface IGameState
    {
        GameState update();
        void draw(RenderWindow win, View view);
        void drawGUI(GUI gui);
    }
}
