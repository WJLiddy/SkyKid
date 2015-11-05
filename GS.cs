
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class GS
{

    public static Texture2D bullet;

    public static GameTime lastDelta;

    public static ControllerManager controllerManager;

    public static Kid player;

    public static LinkedList<Baddie> baddies;

    public static int camX = 2000;

    public static FlatMap level;

    public class Bullet
    {
        public int x;
        public int y;
        public bool left;
    }

    public static LinkedList<Bullet> bullets = new LinkedList<Bullet>();

    //Need to initalize stuff specific to the game? Do it here!
    public GS()
    {
        baddies = new LinkedList<Baddie>();
        for (int i = 0; i != 50; i++)
        {
            baddies.AddFirst(new Baddie());
        }

        //required for controllers.
        controllerManager = new ControllerManager();
        player = new Kid();
        //TODO : should not need to pass screen width or height
        level = new FlatMap("map/map.xml", SkyKidGame.baseWidth, SkyKidGame.baseHeight);
        bullet = Utils.TextureLoader("bullet.png");
    }


    public static void update(GameTime delta, Microsoft.Xna.Framework.Input.KeyboardState ks, GamePadState[] gs)
    {
        camX -= 3;
        lastDelta = delta;

        SlimDX.DirectInput.JoystickState[] joyStates = controllerManager.getState();

        player.update(ks);


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
    public static void draw()
    {
        level.drawBase(camX, 0);       
        player.draw(camX);

        foreach(Baddie b in baddies)
        {
            b.draw(camX);
        }
        Utils.drawString(lastDelta.IsRunningSlowly ? "SLOW!" : "", 100, 1, Color.BlanchedAlmond, 1, true);

        foreach (Bullet b in bullets)
        {
            Utils.drawTexture(bullet, b.x +-2 +- camX, b.y +- 2);
        }
    }

    public static bool collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2 )
    {
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }
}

