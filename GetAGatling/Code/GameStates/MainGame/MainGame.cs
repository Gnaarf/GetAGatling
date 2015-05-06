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

        Sprite RadarSprite = new Sprite(new Texture("Textures/Radar.png"));
        Sprite pixelSprite = new Sprite(new Texture("Textures/pixel.png"));

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
            // mess with view
            // zoom
            if (Keyboard.IsKeyPressed(Keyboard.Key.Subtract))
                view.Size = new Vector2(view.Size.X + 1F, view.Size.Y + (view.Size.Y / view.Size.X));
            if (Keyboard.IsKeyPressed(Keyboard.Key.Add))
                view.Size = new Vector2(view.Size.X - 1F, view.Size.Y - (view.Size.Y / view.Size.X));
            // move
            view.Center = lerp(view.Center, player.position + player.size / 2F, 0.001F);

            // draw the actual entities
            plat.draw(win, view);

            player.draw(win, view);
        }

        Vector2 lerp(Vector2 from, Vector2 to, float t)
        {
            Vector2 result = new Vector2();
            result.X = (1F - t) * from.X + t * to.X;
            result.Y = (1F - t) * from.Y + t * to.Y;
            return result;
        }

        public void drawGUI(GUI gui)
        {
            RadarSprite.Scale = new Vector2(2F, 2F);
            Vector2 textureSize = new Vector2(RadarSprite.Texture.Size.X * RadarSprite.Scale.X, RadarSprite.Texture.Size.Y * RadarSprite.Scale.Y);
            RadarSprite.Position = GameConstants.GUI_RadarCenter - textureSize / 2F;
            gui.draw(RadarSprite);

            Console.WriteLine(player.position);

            // draw Player on Radar
            Vector2 playerRadarPosition = player.position / 100F + GameConstants.GUI_RadarCenter;
            pixelSprite.Color = SFML.Graphics.Color.Red;
            pixelSprite.Position = playerRadarPosition;
            pixelSprite.Scale = new Vector2(3, 3);
            gui.draw(pixelSprite);
        }
    }
}
