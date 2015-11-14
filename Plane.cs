public class Plane
{
    public int X { get; protected set; }
    public int Y { get; protected set; }
    public bool Dead { get; protected set; }

    protected int FramesUntilCanShootAgain = 0;

    public readonly int Width = 22;
    public readonly int Height = 17;
    public int FramesPerShot { get; protected set; } = 7;

    protected AnimationSet Animation;

    protected void CheckForBulletCollide(SkyKidGame world)
    {
        foreach (SkyKidGame.Bullet b in SkyKidGame.Bullets)
        {
            if (SkyKidGame.Collide(b.x, b.y, 1, 1, X, Y, Width, Height))
            {
                Dead = true;
                Animation.AutoAnimateOnce("dead", 0);
            }
        }
    }

    protected void CheckIfCrashed(SkyKidGame world)
    {
        //check to see if corners of sprite collide with world.
        if (world.Level.collide(X, Y) || world.Level.collide(X + Width, Y) || world.Level.collide(X, Y + Height) || world.Level.collide(X + Width, Y + Height))
        {
            Dead = true;
            // TODO: play once??????
            Animation.AutoAnimateOnce("dead", 0);
        }
    }
}

