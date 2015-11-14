using Microsoft.Xna.Framework.Input;

public class Kid : Plane
{
    public Kid()
    {
        Animation = new AnimationSet(@"kid\kid.xml");
        Animation.Speed = 2;
        Animation.AutoAnimate("normal", 0);
        X = 2100;
        Y = 40;
    }

    public void draw(AD2SpriteBatch sb,int camX)
    {
        Animation.Draw(sb, X - camX, Y);
    }

    public void update(SkyKidGame world,KeyboardState ks)
    {
        if (FramesUntilCanShootAgain != 0)
            FramesUntilCanShootAgain--;

        if (!Dead)
        {
            MovePlane(world, ks);
            CheckForShoot(world, ks);
            CheckIfCrashed(world);
            CheckForBulletCollide(world);

        }

        Animation.Update();
    }

    private void MovePlane(SkyKidGame world, KeyboardState ks)
    {
        // X
        if (ks.IsKeyDown(Keys.Left) && X > world.CamX)
            X -= 5;
        else if (ks.IsKeyDown(Keys.Right) && X < world.CamX + SkyKidGame.BaseWidth + -Width)
            X -= 1;
        else X -= 3;

        // Y
        if (ks.IsKeyDown(Keys.Down))
            Y += 3;
        else if (ks.IsKeyDown(Keys.Up))
            Y -= 3;
    }

    private void CheckForShoot(SkyKidGame world, KeyboardState ks)
    {
        if (ks.IsKeyDown(Keys.A) && FramesUntilCanShootAgain == 0)
        {
            FramesUntilCanShootAgain = FramesPerShot;
            SkyKidGame.Bullet b = new SkyKidGame.Bullet();
            //Arbitrarily decide where to spawn bullet
            b.x = X - 2;
            b.y = Y + 5;
            b.left = true;
            SkyKidGame.Bullets.AddFirst(b);
        }
    }
}
