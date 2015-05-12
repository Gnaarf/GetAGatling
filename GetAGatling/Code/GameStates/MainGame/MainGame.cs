using System;
using SFML;
using SFML.Graphics;
using SFML.Window;
using Box2DX.Collision;
using Box2DX.Dynamics;
using Box2DX.Common;
using System.Collections.Generic;

namespace GameProject2D
{
    class InGame : IGameState
    {
        Player player;

        World PhysicWorld;
        Map map = new Map();
        List<Platform> platforms;
        AABB worldAABB = new AABB();

        Sprite RadarSprite = new Sprite(new Texture("Textures/Radar.png"));
        Sprite pixelSprite = new Sprite(new Texture("Textures/pixel.png"));

        public InGame()
        {
            worldAABB.LowerBound.Set(-1000.0f, -1000.0f);
            worldAABB.UpperBound.Set(1000.0f, 1000.0f);
            PhysicWorld = new Box2DX.Dynamics.World(worldAABB, new Vec2(0F, 9.81F), false);
            player = new Player(PhysicWorld,new Vector2f(0F, 0F));

            platforms = new List<Platform>();
            platforms.Add(new Platform(PhysicWorld, "Textures/pixel.png", 0F, 3F, 10F, 1F, 0F));
            platforms.Add(new Platform(PhysicWorld, "Textures/pixel.png", 8F, 4F, 10F, 1F, -Helper.PI / 8F));
        }

        public GameState update()
        {   
            player.update();
            foreach(Platform platform in platforms)
            {
                platform.update();
            }

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
            view.Center = Vector2.lerp(view.Center, player.midPoint.PixelCoord + player.size / 2F, 0.001F);

            // draw coordinate-system
            float stepSize = 1F;
            pixelSprite.Color = SFML.Graphics.Color.Black;
            for (int i = -100; i <= 100; ++i)
            {
                pixelSprite.Position = new Vector2(0F, i * stepSize).PixelCoord;
                win.Draw(pixelSprite);
                pixelSprite.Position = new Vector2(i * stepSize, 0F).PixelCoord;
                win.Draw(pixelSprite);
            }


            // draw the actual entities
            foreach (Platform platform in platforms)
            {
                platform.draw(win, view);
            }

            player.draw(win, view);
        }

        public void drawGUI(GUI gui)
        {
            RadarSprite.Scale = new Vector2(2F, 2F);
            Vector2 textureSize = new Vector2(RadarSprite.Texture.Size.X * RadarSprite.Scale.X, RadarSprite.Texture.Size.Y * RadarSprite.Scale.Y);
            RadarSprite.Position = GameConstants.GUI_RADAR_CENTER - textureSize / 2F;
            gui.draw(RadarSprite);

            // draw Player on Radar
            Vector2 playerRadarPosition = player.midPoint.PixelCoord / map.size + GameConstants.GUI_RADAR_CENTER;
            pixelSprite.Color = SFML.Graphics.Color.Red;
            pixelSprite.Position = playerRadarPosition;
            pixelSprite.Scale = new Vector2(3, 3);
            gui.draw(pixelSprite);
        }
    }
}
