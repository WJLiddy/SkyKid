
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

public class SkyKidGame : AD2Game
{
    public static Texture2D bullet;
    public static Kid player;
    public static LinkedList<Baddie> baddies;
    public static int camX = 2000;
    public static FlatMap level;
    public static readonly int baseWidth = 288;
    public static readonly int baseHeight = 224;

    public class Bullet
    {
        public int x;
        public int y;
        public bool left;
    }

    public static LinkedList<Bullet> bullets = new LinkedList<Bullet>();

    //Need to initalize stuff specific to the game? Do it here!
    public SkyKidGame() : base(baseWidth, baseHeight, 40)
    {

    }

    public static bool collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2 )
    {
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }

    protected override void ad2Logic(int ms, KeyboardState keyboardState, GamePadState[] gamePadState)
    {
        camX -= 3;
        
        player.update(keyboardState);

        foreach (Baddie b in baddies)
        {
            b.update();
        }

        LinkedList<Bullet> outOfBounds = new LinkedList<Bullet>(); ;
        foreach (Bullet b in bullets)
        {
            if (b.left)
                b.x -= 10;
            else
                b.x += 5;

            if (camX > b.x)
                outOfBounds.AddLast(b);
        }


        foreach (Bullet b in outOfBounds)
        {
            bullets.Remove(b);
        }
    }

    protected override void ad2Draw()
    {
        level.drawBase(renderer.primarySpriteBatch,camX, 0);
        player.draw(renderer.primarySpriteBatch,camX);

        foreach (Baddie b in baddies)
        {
            b.draw(renderer.primarySpriteBatch,camX);
        }

        foreach (Bullet b in bullets)
        {
            //bullet.draw
            //whatever
            Renderer.drawTexture(renderer.primarySpriteBatch,bullet, b.x + -2 + -camX, b.y + -2);
        }
    }

    protected override void ad2LoadContent()
    {
        baddies = new LinkedList<Baddie>();
        for (int i = 0; i != 50; i++)
        {
            baddies.AddFirst(new Baddie());
        }

        player = new Kid();
        //TODO : should not need to pass screen width or height
        level = new FlatMap("map/map.xml", SkyKidGame.baseWidth, SkyKidGame.baseHeight);
        bullet = Utils.TextureLoader("bullet.png");
    }
}

