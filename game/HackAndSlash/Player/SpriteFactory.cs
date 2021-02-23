﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace HackAndSlash
{   /// <summary>
    /// This is a class for all sprites including player sprite, enemy sprite, and obstacle sprite.
    /// Every sprite is required to implement ISprite Interface.
    /// </summary>
    class SpriteFactory
    {
        ImageDatabase IMDB;

        private Texture2D ZeldaDown;
        private Texture2D ZeldaUp;
        private Texture2D ZeldaLeft;
        private Texture2D ZeldaRight;

        private Texture2D ZeldaAttackDown;
        private Texture2D ZeldaAttackUp;
        private Texture2D ZeldaAttackLeft;
        private Texture2D ZeldaAttackRight;

        private Texture2D ZeldaUseItemDown;
        private Texture2D ZeldaUseItemUp;
        private Texture2D ZeldaUseItemLeft;
        private Texture2D ZeldaUseItemRight;

        private Texture2D BGSprite;

        private Texture2D SnakeIdleSprite;
        private Texture2D SnakeMoveSprite;
        private Texture2D SnakeAttackSprite;
        private Texture2D SnakeDieSprite;

        private Texture2D BugIdleSprite;
        private Texture2D BugMoveUpSprite;
        private Texture2D BugMoveDownSprite;
        private Texture2D BugDieSprite;

        private Texture2D FirewallSprite;
        private Texture2D BombSprite;
        private Texture2D ExplosionSprite;

        private Texture2D ChipBlock;
        private Texture2D SmoothBlock;


        private static SpriteFactory instance = new SpriteFactory();

        public static SpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            IMDB = new ImageDatabase();         

            ZeldaDown = content.Load<Texture2D>(IMDB.zeldaDown.pathName);
            ZeldaUp = content.Load<Texture2D>(IMDB.zeldaUp.pathName);
            ZeldaLeft = content.Load<Texture2D>(IMDB.zeldaLeft.pathName);
            ZeldaRight = content.Load<Texture2D>(IMDB.zeldaRight.pathName);

            ZeldaAttackDown = content.Load<Texture2D>(IMDB.zeldaAttackDown.pathName);
            ZeldaAttackUp = content.Load<Texture2D>(IMDB.zeldaAttackUp.pathName);
            ZeldaAttackLeft = content.Load<Texture2D>(IMDB.zeldaAttackLeft.pathName);
            ZeldaAttackRight = content.Load<Texture2D>(IMDB.zeldaAttackRight.pathName);

            ZeldaUseItemDown = content.Load<Texture2D>(IMDB.zeldaUseItemDown.pathName);
            ZeldaUseItemUp = content.Load<Texture2D>(IMDB.zeldaUseItemUp.pathName);
            ZeldaUseItemLeft = content.Load<Texture2D>(IMDB.zeldaUseItemLeft.pathName);
            ZeldaUseItemRight = content.Load<Texture2D>(IMDB.zeldaUseItemRight.pathName);

            // Original image from https://opengameart.org/content/animated-snake
            // Edited in Adobe Fresco to align specific states

            SnakeIdleSprite = content.Load<Texture2D>(IMDB.snakeIdle.pathName);
            SnakeMoveSprite = content.Load<Texture2D>(IMDB.snakeMoveLeft.pathName);
            SnakeAttackSprite = content.Load<Texture2D>(IMDB.snakeAttackLeft.pathName);
            SnakeDieSprite = content.Load<Texture2D>(IMDB.snakeDie.pathName);

            //Original image sourced from 
            //Edited in Adobe fresco to align specific states

            BugMoveUpSprite = content.Load<Texture2D>(IMDB.bugMoveUp.pathName);
            BugMoveDownSprite = content.Load<Texture2D>(IMDB.bugMoveDown.pathName);
            BugDieSprite = content.Load<Texture2D>(IMDB.bugDie.pathName);
            BugIdleSprite = content.Load<Texture2D>(IMDB.bugIdle.pathName);

            //Item Sprites 
            FirewallSprite = content.Load<Texture2D>(IMDB.fireWall.pathName);
            BombSprite = content.Load<Texture2D>(IMDB.bomb.pathName);
            ExplosionSprite = content.Load<Texture2D>(IMDB.explosion.pathName);

            // More Content.Load calls follow
            BGSprite = content.Load<Texture2D>(IMDB.BG.pathName);

            //Blocks
            ChipBlock = content.Load<Texture2D>(IMDB.ChipBlock.pathName);
            SmoothBlock = content.Load<Texture2D>(IMDB.SmoothBlock.pathName);

        }

        public Texture2D CreateBG()
        {
            return BGSprite;
        }

        public Texture2D CreatePlayer()
        {
            return ZeldaRight; //initial face right
        }
            
        //***********Below are Player movement***************
    
        public void SetUpPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUp.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUp.C;
            DrawPlayer.Instance.SetTexture(ZeldaUp);
        }

        public void SetRightPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaRight.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaRight.C;
            DrawPlayer.Instance.SetTexture(ZeldaRight);
        }
        public void SetLeftPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaLeft.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaLeft.C;
            DrawPlayer.Instance.SetTexture(ZeldaLeft);
        }

        public void SetDownPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaDown.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaDown.C;
            DrawPlayer.Instance.SetTexture(ZeldaDown);
        }

        //*************Below are Player attack*********************
        public void SetUpAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackUp.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackUp.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackUp);
        }

        public void SetRightAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackRight.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackRight.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackRight);
        }
        public void SetLeftAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackLeft.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackLeft.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackLeft);
        }

        public void SetDownAttackPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaAttackDown.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaAttackDown.C;
            DrawPlayer.Instance.SetTexture(ZeldaAttackDown);
        }

        //*************Below are Player use item*********************
        public void SetUpUseItemPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUseItemUp.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUseItemUp.C;
            DrawPlayer.Instance.SetTexture(ZeldaUseItemUp);
        }

        public void SetRightUseItemPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUseItemRight.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUseItemRight.C;
            DrawPlayer.Instance.SetTexture(ZeldaUseItemRight);
        }

        public void SetLeftUseItemPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUseItemLeft.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUseItemLeft.C;
            DrawPlayer.Instance.SetTexture(ZeldaUseItemLeft);
        }

        public void SetDownUseItemPlayer()
        {
            DrawPlayer.Instance.Rows = IMDB.zeldaUseItemDown.R;
            DrawPlayer.Instance.Columns = IMDB.zeldaUseItemDown.C;
            DrawPlayer.Instance.SetTexture(ZeldaUseItemDown);
        }
        //*****************Below are enemies sprites******************

        public ISprite CreateSnakeIdle()
        {
            return new EnemySprite(SnakeIdleSprite, IMDB.snakeIdle.C, IMDB.snakeIdle.R);
        }

        public ISprite CreateSnakeMoving()
        {
            return new EnemySprite(SnakeMoveSprite, IMDB.snakeMoveLeft.C, IMDB.snakeMoveLeft.R);
        }

        public ISprite CreateSnakeAttack()
        {
            return new EnemySprite(SnakeAttackSprite, IMDB.snakeAttackLeft.C, IMDB.snakeAttackLeft.R);
        }

        public ISprite CreateSnakeDie()
        {
            return new EnemySprite(SnakeDieSprite, IMDB.snakeDie.C, IMDB.snakeIdle.R);
        }

        public ISprite CreateBugIdle()
        {
            return new EnemySprite(BugIdleSprite, IMDB.bugIdle.C, IMDB.bugIdle.R);
        }

        public ISprite CreateBugMoveUp()
        {
            return new EnemySprite(BugMoveUpSprite, IMDB.bugMoveUp.C, IMDB.bugMoveUp.R);
        }

        public ISprite CreateBugMoveDown()
        {
            return new EnemySprite(BugMoveDownSprite, IMDB.bugMoveDown.C, IMDB.bugMoveDown.R);
        }

        public ISprite CreateBugDie()
        {
            return new EnemySprite(BugDieSprite, IMDB.bugDie.C, IMDB.bugDie.R);
        }

        public  ISprite CreateFirewall()
        {
            return new ItemSprite(FirewallSprite, IMDB.fireWall.C, IMDB.fireWall.R);
        }
        public ISprite CreateBomb()
        {
            return new ItemSprite(BombSprite, IMDB.bomb.C, IMDB.bomb.R);
        }
        public ISprite CreateExplosion()
        {
            return new ItemSprite(ExplosionSprite, IMDB.explosion.C, IMDB.explosion.R);
        }

        public IBlock CreateChipBlock(SpriteBatch spriteBatch)
        {
            return new ChipBlock(ChipBlock, new Vector2(100, 300), spriteBatch);
        }

        public IBlock CreateSmoothBlock(SpriteBatch spriteBatch)
        {
            return new SmoothBlock(SmoothBlock, new Vector2(175, 300), spriteBatch);
        }
    }
}
