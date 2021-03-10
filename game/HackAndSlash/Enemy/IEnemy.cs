using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    interface IEnemy
    {
        
         void Update(GameTime gametime);
        //gametime passed in for frame rate

        //Draw method for each of the enemies
        void Draw();

        //changing the state of the enemy to be idle
        void changeToIdle();

        //changing the state of the enemy to Not - meaning the enemy is not currently updated or drawn
        void changeToNot();

        Vector2 GetPos();
        void SetPos(Vector2 pos);
    }
}
