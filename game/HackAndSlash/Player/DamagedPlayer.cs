﻿

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{


    public class DamagedPlayer : IPlayer
    {
        private IPlayer DecoratedPlayer;
        private Game1 game;
        private int timer;
        private Color color;

        private Vector2 relPositionMC; // Relative position. As position in display window 


        public DamagedPlayer(IPlayer decoratedPlayer, Game1 game) 
        {
            timer = 200;
            this.DecoratedPlayer = decoratedPlayer;
            this.game = game;
            this.color = Color.Red;
            this.relPositionMC = decoratedPlayer.GetPos();
        }

        public Vector2 GetPos()
        {
            return relPositionMC;
        }

        public void SetPos(Vector2 pos)
        {
            relPositionMC = pos;
        }

        public void Update()
        {
            timer--;
            if (timer == 0) RemoveDecorator();
            if (timer % 10 > 5) color = Color.White;
            else color = Color.Red;
            DecoratedPlayer.Update();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            DecoratedPlayer.Draw(spriteBatch, location, this.color);
        }

        public void RemoveDecorator()
        {
            game.Player = DecoratedPlayer; //for the purpose of not taking damage again???
        }

        public void Move()
        {
            DecoratedPlayer.Move();
        }

        public void Attack()
        {
            DecoratedPlayer.Attack();
        }

        public void Damaged()
        {
            //Does not take damaged!
        }

        public int GetDir()
        {
            return DecoratedPlayer.GetDir();
        }

        public void ChangeDirection(GlobalSettings.Direction dir)
        {
            DecoratedPlayer.ChangeDirection(dir);
        }
        public void UseItem()
        {
            DecoratedPlayer.UseItem();
        }
    }
    
}
