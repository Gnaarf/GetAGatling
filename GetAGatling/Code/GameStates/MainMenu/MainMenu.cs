using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class MainMenu : IGameState
    {
        Sprite background;

        public MainMenu()
        {
            background = new Sprite(new Texture("Textures/MainMenu_Background2.jpg"));
        }

        public GameState update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return GameState.InGame;
            }

            return GameState.MainMenu;
        }

        public void draw(RenderWindow win, View view)
        {
            win.Draw(background);
        }

        public void drawGUI(GUI gui)
        {

        }
    }
}
