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
        World PhysicWorld = new Box2DX.Dynamics.World(new AABB(), new Vec2(0F, 9.81F), false);

        public InGame()
        {
            player = new Player(new Vector2f(10F, 10F));

            for (int i = 0; i < 100; i++)
            {
                BodyDef body = new BodyDef();
                body.Position = new Vec2(200F, -i * 10F);
                body.MassData.Mass = 0.1F + Rand.Value(0F, 3.1F);
                body.LinearDamping = 0.1F;

                PhysicWorld.CreateBody(body);
            }
            BodyDef plane = new BodyDef();
            plane.Position = new Vec2(200F, 30F);

            PhysicWorld.CreateBody(plane);
        }

        public GameState update()
        {
            Console.WriteLine(PhysicWorld.GetGroundBody());

            player.update();
            PhysicWorld.Step((float)Program.gameTime.EllapsedTime.TotalSeconds, 10, 10);

            return GameState.InGame;
        }

        public void draw(RenderWindow win, View view)
        {
            RectangleShape a = new RectangleShape(new Vector2(10F, 10F));

            Body phyBody = PhysicWorld.GetBodyList();
            while(phyBody.GetNext() != null)
            {
                a.Position = (Vector2)phyBody.GetPosition();
                win.Draw(a);
                phyBody = phyBody.GetNext();
            }

            player.draw(win, view);
        }
    }
}
