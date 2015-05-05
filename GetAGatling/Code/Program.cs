using SFML.Graphics;
using SFML.Window;
using System;

namespace GameProject2D
{
    class Program
    {
        public static GameTime gameTime;

        static bool running = true;

        static GameState currentGameState = GameState.MainMenu;
        static GameState prevGameState = GameState.MainMenu;
        static IGameState state;

        static RenderWindow win;
        static View view;
        
        static void Main(string[] args)
        {
            // initialize window and view
            win = new RenderWindow(new VideoMode(800, 600), "2D Game Project");
            view = new View(new Vector2(win.Size.X / 2, win.Size.Y / 2), new Vector2(win.Size.X, win.Size.Y));

            // exit Program, when window is being closed
            win.Closed += (object sender, EventArgs e) => { (sender as Window).Close(); };

            handleNewGameState();

            // initialize GameTime
            gameTime = new GameTime();
            gameTime.Start();

            while (running && win.IsOpen())
            {
                currentGameState = state.update();

                if (currentGameState != prevGameState)
                {
                    handleNewGameState();
                }

                // draw current frame
                win.Clear(new Color(100, 149, 237));    //cornflowerblue ftw!!! 1337
                state.draw(win, view);
                win.SetView(view);
                win.Display();

                // check for window-events. e.g. window closed        
                win.DispatchEvents();

                // initialize GameTime
                gameTime.Update();
            }
        }

        private static void handleNewGameState()
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
        }
    }
}
