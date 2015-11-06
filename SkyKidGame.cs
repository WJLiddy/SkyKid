using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class SkyKidGame : AD2Game
{
    // Player character.
    public Kid player;
    // List of all the bad guys.
    public LinkedList<Baddie> baddies;
    // The camera X
    public int camX = 2000;
    // This game's level.
    public FlatMap level;

    // A quick a dirty bullet container
    public class Bullet
    {
        public static Texture2D texture;
        public int x;
        public int y;
        public bool left;
    }

    // List of all the bullets.
    public static LinkedList<Bullet> bullets = new LinkedList<Bullet>();

    // Game Dims.
    public static readonly int baseWidth = 288;
    public static readonly int baseHeight = 224;

    public SkyKidGame() : base(baseWidth, baseHeight, 40)
    {
        //lol stub constructor
    }

    public static bool collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2 )
    {
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }

    protected override void AD2Logic(int ms, KeyboardState keyboardState, GamePadState[] gamePadState)
    {
        //move the camera left.
        camX -= 3;
        
        //update the player.
        player.update(this,keyboardState);

        //update the baddies.
        foreach (Baddie b in baddies)
        {
            b.update(this);
        }

        //do bullet logic. move them and collect the ones that are out of bounds.
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

    protected override void AD2Draw(AD2SpriteBatch primarySpriteBatch)
    {
        level.drawBase(primarySpriteBatch,camX, 0);
        player.draw(primarySpriteBatch,camX);

        foreach (Baddie b in baddies)
        {
            b.draw(primarySpriteBatch,camX);
        }

        foreach (Bullet b in bullets)
        {
            primarySpriteBatch.drawTexture(Bullet.texture, b.x + -2 + -camX, b.y + -2);
        }
    }

    protected override void AD2LoadContent()
    {
        baddies = new LinkedList<Baddie>();
        for (int i = 0; i != 50; i++)
        {
            baddies.AddFirst(new Baddie());
        }

        player = new Kid();
        //TODO : should not need to pass screen width or height
        level = new FlatMap("map/map.xml", SkyKidGame.baseWidth, SkyKidGame.baseHeight);
        Bullet.texture = Utils.TextureLoader("bullet.png");
    }
}

