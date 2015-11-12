public class Baddie : Plane
{

    public Baddie()
    {
        X = (int)(Utils.Random.NextDouble() * 1300.0);
        Y = (int)(Utils.Random.NextDouble() * 200.0);
        FramesPerShot = 20;

        Animation = new AnimationSet(@"baddie\baddie.xml");
        Animation.Speed = 2;
        Animation.AutoAnimate("normal", 0);
    }

    public void draw(AD2SpriteBatch sb,int camX)
    {
        if (ExplodeTime > 0)
            Animation.Draw(sb,X - camX, Y);
    }

    public void update(SkyKidGame world)
    {
        if (FramesUntilCanShootAgain != 0)
            FramesUntilCanShootAgain--;

        if (!Dead)
        {

            X++;
            CheckIfCrashed(world);
            CheckForBulletCollide(world);
            ShootBullets(world);
        }
        else
        {
            ExplodeTime--;
        }
        Animation.Update();
    }

    public void ShootBullets(SkyKidGame world)
    {
        if(FramesUntilCanShootAgain == 0)
        {
            FramesUntilCanShootAgain = FramesPerShot;
            SkyKidGame.Bullet b = new SkyKidGame.Bullet();
            b.x = X + Width + 2;
            b.y = Y + 5;
            b.left = false;
            SkyKidGame.Bullets.AddFirst(b);
        }
    }
}
