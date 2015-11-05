
public class Baddie
{
    int width = 22;
    int height = 17;
    int explodeTime = 3;
    bool dead = false;
    int x = 0;
    int y = 0;
    AnimationSet animation;

    public Baddie()
    {
        x = (int)(Utils.random.NextDouble() * 1300.0);
        y = (int)(Utils.random.NextDouble() * 200.0);

        animation = new AnimationSet(@"baddie\baddie.xml");
        animation.speed = 2;
        animation.autoAnimate("normal", 0);
    }

    public void draw(int camX)
    {
        if (explodeTime > 0)
            animation.draw(x - camX, y);
    }

    public void update()
    {
        if (!dead)
        {

            //    if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) && x > GS.camX)
            //        x -= 5;
            //    else if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) && x < GS.camX + SkyKidGame.baseWidth + -width)
            //        x -= 1;
            //    else 
            x += 1;

      //      if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
      //          y += 3;
      //      else if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
      //          y -= 3;


            if ( (x - GS.player.x) > -300 && Utils.random.NextDouble() < 0.01)
            {
                GS.Bullet b = new GS.Bullet();
                b.x = x + width + 2;
                b.y = y + 5;
                b.left = false;
                GS.bullets.AddFirst(b);
            }

            if (GS.level.collide(x, y) || GS.level.collide(x + width, y) || GS.level.collide(x, y + height) || GS.level.collide(x + width, y + height))
            {
                dead = true;
                //play once??????
                animation.autoAnimate("dead", 0);
            }

            foreach (GS.Bullet b in GS.bullets)
            {
                if (GS.collide(b.x, b.y, 1, 1, x, y, width, height) && b.left)
                {
                    dead = true;
                    //play once??????
                    animation.autoAnimate("dead", 0);
                }
            }
        }
        else
        {
            explodeTime--;

        }
        animation.update();
    }
}
