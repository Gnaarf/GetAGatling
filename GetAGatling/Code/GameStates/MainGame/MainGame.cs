using System;
using SFML;
using SFML.Graphics;
using SFML.Window;
using Box2DX.Collision;
using Box2DX.Dynamics;
using Box2DX.Common;

namespace GameProject2D
{
    class InGame : IGameState
    {
        Player player;
        World PhysicWorld;
        Platform plat;
        AABB worldAABB = new AABB();
        
        public InGame()
        {
            worldAABB.LowerBound.Set( 0.0f, 0.0f);
            worldAABB.UpperBound.Set(600.0f, 550.0f);
            PhysicWorld = new Box2DX.Dynamics.World(worldAABB, new Vec2(0F, 9.81F), false);
            player = new Player(PhysicWorld,new Vector2f(10F, 10F));

//            plat = new Platform(PhysicWorld, "Textures/MainMenu_Background.jpg", 400, 400, 100, 400);
            plat = new Platform(PhysicWorld, "Textures/Ground.png", 400, 500, 50, 50);
        }

        public GameState update()
        {   
            player.update();
            PhysicWorld.Step((float)Program.gameTime.EllapsedTime.TotalSeconds, 10, 10);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) 
            {
                return GameState.MainMenu;
            }
            return GameState.InGame;
        }

        public void draw(RenderWindow win, View view)
        {

            plat.draw(win, view);
            player.draw(win, view);
        }
    }
}
