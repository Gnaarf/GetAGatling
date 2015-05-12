﻿using SFML.Graphics;
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
        static GUI gui;
        
        static void Main(string[] args)
        {
            // initialize window and view
            win = new RenderWindow(new VideoMode(800, 600), "2D Game Project");
            view = new View();
            resetView();
            gui = new GUI(win, view);

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
                state.drawGUI(gui);
                win.SetView(view);
                win.Display();

                // check for window-events. e.g. window closed        
                win.DispatchEvents();

                // initialize GameTime
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
