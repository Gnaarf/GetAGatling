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
            // work on a copy, instead of the original
            Sprite spriteCopy = new Sprite(sprite);

            // modify sprite, to fit it in the gui
            float winViewRatio = view.Size.X / ((float)win.Size.X);
            sprite.Scale *= winViewRatio;
            sprite.Position = ((view.Center - view.Size / 2F) + sprite.Position * winViewRatio);

            // draw the sprite
            win.Draw(sprite);
        }
    }
}