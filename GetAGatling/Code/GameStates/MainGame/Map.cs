using Box2DX.Dynamics;
using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace GameProject2D
{
    class Map
    {
        public Vector2 size = new Vector2(20,20);    //HACK
        List<Platform> platforms;

        public Map(World physicWorld)
        {
            platforms = new List<Platform>();
            platforms.Add(new Platform(physicWorld, "Textures/pixel.png", new Vector2(2F, 3F), new Vector2(10F, 1F), 0F));
            platforms.Add(new MovingPlatform(physicWorld, "Textures/pixel.png", new Vector2(-4F, 5.8F), new Vector2(10F, 1F), 0F, new Vector2(-6F, 8F), 1F));
            platforms.Add(new Platform(physicWorld, "Textures/pixel.png", new Vector2(8F, 4F), new Vector2(10F, 1F), -Helper.PI / 8F));
            platforms.Add(new Platform(physicWorld, "Textures/pixel.png", new Vector2(-5F, 0F), new Vector2(10F, 1F), -Helper.PI / 2F));
        }

        public void update()
        {
            foreach (Platform platform in platforms)
            {
                platform.update();
            }
        }

        public void draw(RenderWindow win, View view)
        {
            foreach (Platform platform in platforms)
            {
                platform.draw(win, view);
            }
        }

        public void addPlatform(Platform platform)
        {
            platforms.Add(platform);
        }
    }
}
