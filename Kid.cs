using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Kid
{
    int width = 22;
    int height = 17;
    int explodeTime = 3;
    public int x = -50;
    int y = 10;
    int shootCoolDown = 0;
    int shootCoolDownMax = 7;
    bool dead = false;
    AnimationSet animation;
    public Kid()
    {
        animation = new AnimationSet(@"kid\kid.xml");
        animation.speed = 2;
        animation.autoAnimate("normal", 0);
        x = 2100;
        y = 40;
    }

    public void draw(AD2SpriteBatch sb,int camX)
    {
        if (explodeTime > 0)
            animation.draw(sb, x - camX, y);
    }

    public void update(SkyKidGame world,Microsoft.Xna.Framework.Input.KeyboardState ks)
    {
        if (shootCoolDown != 0)
            shootCoolDown--;
        if (!dead)
        {

            if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) && x > world.camX)
                x -= 5;
            else if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) && x < world.camX + SkyKidGame.baseWidth + -width)
                x -= 1;
            else x -= 3;

            if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                y += 3;
            else if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                y -= 3;

            if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A) && shootCoolDown == 0)
            {
                shootCoolDown = shootCoolDownMax;
                SkyKidGame.Bullet b = new SkyKidGame.Bullet();
                b.x = x - 2;
                b.y = y + 5;
                b.left = true;
                SkyKidGame.bullets.AddFirst(b);
            }

            if (world.level.collide(x, y) || world.level.collide(x + width, y) || world.level.collide(x, y + height) || world.level.collide(x + width, y + height))
            {
                dead = true;
                //play once??????
                animation.autoAnimate("dead", 0);
            }

            foreach(SkyKidGame.Bullet b in SkyKidGame.bullets )
            {
                if (SkyKidGame.collide(b.x, b.y, 1, 1, x, y, width, height))
                {
                    dead = true;
                    //play once??????
                    animation.autoAnimate("dead", 0);
                }
            }


        } else
        {
            explodeTime--;

        }
            animation.update();
    }
}
