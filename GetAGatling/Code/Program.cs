using SFML.Graphics;
using SFML.Window;
using System;

namespace GameProject2D
{
    class Program
    {
        public static GameTime gameTime;

        public static GamePadInputManager gamePadInputManager;

        static bool running = true;

        static Font calibriFont = new Font("Fonts/calibri.ttf");   // HACK

        static GameState currentGameState = GameState.MainMenu;
        static GameState prevGameState = GameState.MainMenu;
        static IGameState state;

        static RenderWindow win;
        static View view;
        static GUI gui;
        
        static void Main(string[] args)
        {
            // initialize window and view
            win = new RenderWindow(new VideoMode(800, 600), "2D Game Project");
            view = new View();
            resetView();
            gui = new GUI(win, view);

            Text debugText = new Text("", calibriFont);
            debugText.Color = new Color(252, 143, 80);

            // exit Program, when window is being closed
            win.Closed += (object sender, EventArgs e) => { (sender as Window).Close(); };

            // initialize GamePadInputManager, in case, there are GamePads connected
            gamePadInputManager = new GamePadInputManager();

            // initialize GameState
            handleNewGameState();

            // initialize GameTime
            gameTime = new GameTime();
            gameTime.Start();

            while (running && win.IsOpen())
            {
                gamePadInputManager.update();
                // TODO: reevaluate gamepads every once in a while

                currentGameState = state.update();

                if (currentGameState != prevGameState)
                {
                    handleNewGameState();
                }

                // draw current frame
                win.Clear(new Color(100, 149, 237));    //cornflowerblue ftw!!! 1337
                state.draw(win, view);
                state.drawGUI(gui);
                {   //HACK
                    debugText.DisplayedString = "fps:" + (1.0 / gameTime.EllapsedTime.TotalSeconds);
                    debugText.DisplayedString += "\ntest";
                    gui.draw(debugText);
                }
                win.SetView(view);
                win.Display();

                // check for window-events. e.g. window closed        
                win.DispatchEvents();

                // update GameTime
                gameTime.Update();
            }
        }

        static void handleNewGameState()
        {
            switch (currentGameState)
            {
                case GameState.None:
                    running = false;
                    break;

                case GameState.MainMenu:
                    state = new MainMenu();
                    break;

                case GameState.InGame:
                    state = new InGame();
                    break;
            }

            prevGameState = currentGameState;

            resetView();
        }

        static void resetView()
        {
            view.Center = new Vector2(win.Size.X / 2F, win.Size.Y / 2F);
            view.Size = new Vector2(win.Size.X, win.Size.Y);
        }
    }
}
