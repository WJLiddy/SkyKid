using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class SkyKidGame : AD2Game
{
    // Player character.
    public Kid Player;
    // List of all the bad guys.
    public LinkedList<Baddie> Baddies;
    // The camera X
    public int CamX = 2000;
    // This game's level.
    public FlatMap Level;

    // A quick a dirty bullet container
    public class Bullet
    {
        public static Texture2D texture;
        public int x;
        public int y;
        public bool left;
    }

    // List of all the bullets.
    public static LinkedList<Bullet> Bullets = new LinkedList<Bullet>();

    // Game Dims.
    public static readonly int BaseWidth = 288;
    public static readonly int BaseHeight = 224;

    public SkyKidGame() : base(BaseWidth, BaseHeight, 40)
    {
        //lol stub constructor
        Renderer.Resolution = Renderer.ResolutionType.WindowedLarge;
    }

    public static bool Collide(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2 )
    {
        return !(x1 > x2 + w2 ||
                y1 > y2 + h2 ||
                x2 > x1 + w1 ||
                y2 > y1 + h1);
    }

    protected override void AD2Logic(int ms, KeyboardState keyboardState, GamePadState[] gamePadState)
    {
        //move the camera left.
        CamX -= 3;
        
        //update the player.
        Player.update(this,keyboardState);

        //update the baddies.
        foreach (Baddie b in Baddies)
        {
            b.update(this);
        }

        //do bullet logic. move them and collect the ones that are out of bounds.
        LinkedList<Bullet> outOfBounds = new LinkedList<Bullet>(); ;
        foreach (Bullet b in Bullets)
        {
            if (b.left)
                b.x -= 10;
            else
                b.x += 5;

            if (CamX > b.x)
                outOfBounds.AddLast(b);
        }

        foreach (Bullet b in outOfBounds)
        {
            Bullets.Remove(b);
        }
    }

    protected override void AD2Draw(AD2SpriteBatch primarySpriteBatch)
    {
        Level.drawBase(primarySpriteBatch,CamX, 0);
        Player.draw(primarySpriteBatch,CamX);

        foreach (Baddie b in Baddies)
        {
            b.draw(primarySpriteBatch,CamX);
        }

        foreach (Bullet b in Bullets)
        {
            primarySpriteBatch.DrawTexture(Bullet.texture, b.x + -2 + -CamX, b.y + -2);
        }
    }

    protected override void AD2LoadContent()
    {
        Baddies = new LinkedList<Baddie>();
        for (int i = 0; i != 50; i++)
        {
            Baddies.AddFirst(new Baddie());
        }

        Player = new Kid();
        //TODO : should not need to pass screen width or height
        Level = new FlatMap("map/map.xml", BaseWidth, BaseHeight);
        Bullet.texture = Utils.TextureLoader("bullet.png");
    }
}

