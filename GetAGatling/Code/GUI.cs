using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace GameProject2D
{
    class GUI
    {
        Queue<Sprite> cachedSprites = new Queue<Sprite>();
        RenderWindow win;
        View view;

        public GUI(RenderWindow win, View view)
        {
            this.win = win;
            this.view = view;
        }

        public void draw(Sprite sprite)
        {
            // work on a copy, instead of the original, for the original could be reused outside this scope
            Sprite spriteCopy = new Sprite(sprite);

            // modify sprite, to fit it in the gui
            float viewScale = (float)view.Size.X / win.Size.X;

            spriteCopy.Scale *= viewScale;
            spriteCopy.Position = view.Center - view.Size / 2F + spriteCopy.Position * viewScale;

            // draw the sprite
            win.Draw(spriteCopy);
        }
    }
}