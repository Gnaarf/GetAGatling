using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class MainMenu : IGameState
    {
        Sprite background;
        Texture startButton;
        Texture startButtonHover;
        Texture quitButton;
        Texture quitButtonHover;
        Sprite start;
        Sprite quit;
        SelectedButton currentSelected;
        int indexButton;
        enum SelectedButton 
        {
            None,
            Start,
   //         Controlles,
   //         Credits,
            Quit,
            Size
        }
        public MainMenu()
        {
            background = new Sprite(new Texture("Textures/MainMenu_Background.jpg"));
            startButton = new Texture("Textures/MainMenuButton/StartButton.jpg");
            startButtonHover = new Texture("Textures/MainMenuButton/StartButtonHover.jpg");
            start = new Sprite(startButtonHover);
            quitButton = new Texture("Textures/MainMenuButton/QuitButton.jpg");
            startButtonHover = new Texture("Textures/MainMenuButton/QuitButtonHover.jpg");
            quit = new Sprite(quitButton);
            currentSelected = SelectedButton.Start;
            indexButton = 0;
        }

        public GameState update()
        {
            selectButton();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && SelectedButton.Start == currentSelected)
            {
                return GameState.InGame;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && SelectedButton.Quit == currentSelected)
            {
                return GameState.None;
            }

            return GameState.MainMenu;
        }
        void selectButton() 
        {
            if( currentSelected > (SelectedButton)1 && upInputPressed())
            {
                indexButton--;
                currentSelected = (SelectedButton)indexButton;
                start = new Sprite(startButtonHover);
                quit = new Sprite(quitButton);
            }
            else if( downInputPressed() && currentSelected < SelectedButton.Size-1 )
            {
                indexButton++;
                currentSelected = (SelectedButton)indexButton; 
                start = new Sprite(startButton);
                quit = new Sprite(quitButtonHover);
            }
        }

        bool upInputPressed() 
        {
            return (Keyboard.IsKeyPressed(Keyboard.Key.W)
                || Keyboard.IsKeyPressed(Keyboard.Key.D)
                || Keyboard.IsKeyPressed(Keyboard.Key.Up)
                || Keyboard.IsKeyPressed(Keyboard.Key.Right)
                //||  Program.gamePadInputManager.getLeftStick(0).X >0 
                //||  Program.gamePadInputManager.getLeftStick(0).Y > 0
                );
        }
        bool downInputPressed() 
        {
            return (Keyboard.IsKeyPressed(Keyboard.Key.S)
                || Keyboard.IsKeyPressed(Keyboard.Key.A)
                || Keyboard.IsKeyPressed(Keyboard.Key.Down)
                || Keyboard.IsKeyPressed(Keyboard.Key.Left)
                //||  Program.gamePadInputManager.getLeftStick(0).X < 0
                ||  Program.gamePadInputManager.getLeftStick(0).Y < 0 
                );
        }
        public void draw(RenderWindow win, View view)
        {
            start.Position = new Vector2(100, 100);
            quit.Position = new Vector2(100,200);
            win.Draw(background);
            win.Draw(start);
            win.Draw(quit);
        }

        public void drawGUI(GUI gui)
        {

        }
    }
}
