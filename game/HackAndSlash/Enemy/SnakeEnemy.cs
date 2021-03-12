using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using HackAndSlash.Collision;

namespace HackAndSlash
{
    public class SnakeEnemy : IEnemy
    {
        private snakeStateMachine snakeState; // to keep track of the current state of the snake
        private Vector2 position; // the position of the enemy on screen
        private SpriteBatch spriteBatch; // the spritebatch used to draw the enemy
        private GraphicsDevice Graphics; // the graphics device used by the spritebatch

        private int timeSinceLastFrame=0; // used to slow down the rate of animation
        private int timeSinceDirectionChange = 0;
        private int deathTimer = 0;
        private int milliSecondsPerFrame=80;
        private int temp = 0; //counter to change states after a certain number of calls to update

        private System.Random random;
        private int randomDirection = 0; //0-left, 1-up, 2-right, 3- down

        private EnemyCollisionDetector enemyCollisionDetector;
        private EnemyBlockCollision enemyBlockCollision;
        private Rectangle hitbox;

        private int bottomBound = GlobalSettings.WINDOW_HEIGHT - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        private int rightBound = GlobalSettings.WINDOW_WIDTH - GlobalSettings.BORDER_OFFSET - GlobalSettings.BASE_SCALAR;
        //Snake position
        private Rectangle rectangle { get; set; }

        //make the constructor for the class
        public SnakeEnemy(Vector2 startPosition, GraphicsDevice graphics, SpriteBatch SB, Game1 game)
        {
            position = startPosition;
            snakeState = new snakeStateMachine();
            Graphics = graphics;
            spriteBatch = SB;
            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

            random = new System.Random();

            enemyCollisionDetector = new EnemyCollisionDetector(game, this);
            enemyBlockCollision = new EnemyBlockCollision();
            hitbox = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

        }

        public Rectangle getRectangle()
        {
            return rectangle;
        }



        public Vector2 GetPos()
        {
            return position;
        }

        public void SetPos(Vector2 pos)
        {
            position = pos;
        }

        //updating the enemy
        public void Update(GameTime gameTime)
        {
            timeSinceDirectionChange += gameTime.ElapsedGameTime.Milliseconds;
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds; //counting elapsed time since last update
            if (timeSinceLastFrame > milliSecondsPerFrame) // executing when milliSecondsPerFrame seconds have passed
            {
                timeSinceLastFrame = 0;

                if (snakeState.state != snakeStateMachine.snakeHealth.Not)// call update on the EnemySprite when not in 'NOT'
                {
                    snakeState.MachineEnemySprite.Update();
                }
            }

            //Boundary collisions
            if (snakeState.state == snakeStateMachine.snakeHealth.MoveUp)
            {
                // Move up
                if (position.Y >= GlobalSettings.BORDER_OFFSET)
                {
                    position.Y--;
                }

                else
                {
                    snakeState.changeToMoveDown();
                }
            }
            else if (snakeState.state == snakeStateMachine.snakeHealth.MoveDown)
            {
                //Move down
                if (position.Y <= bottomBound)
                {
                    position.Y++;
                }

                else
                {
                    snakeState.changeToMoveUp();
                }
            }
            else if (snakeState.state == snakeStateMachine.snakeHealth.MoveLeft)
            {
                //Move left
                if (position.X >= GlobalSettings.BORDER_OFFSET)
                {
                    position.X--;
                }
                else
                {
                    snakeState.changeToRightMove();
                }
            }
            else if (snakeState.state == snakeStateMachine.snakeHealth.MoveRight)
            {
                //Move right
                if (position.X <= rightBound)
                {
                    position.X++;
                }
                else
                {
                    snakeState.changeToLeftMove();
                }
            }

            if (timeSinceDirectionChange > 8000 && snakeState.state != snakeStateMachine.snakeHealth.Not && snakeState.state != snakeStateMachine.snakeHealth.Die)
            {
                timeSinceDirectionChange = 0;
                randomDirection = random.Next(0, 3);
                switch (randomDirection)
                {
                    case 0:
                        snakeState.changeToLeftMove();
                        break;
                    case 1:
                        snakeState.changeToMoveUp();
                        break;
                    case 2:
                        snakeState.changeToRightMove();
                        break;
                    case 3:
                        snakeState.changeToMoveDown();
                        break;
                }
            }

            hitbox.Location = new Point((int)position.X, (int)position.Y);
            enemyBlockCollision.HandleCollision(this, enemyCollisionDetector.CheckBlockCollisions(hitbox));
            if (enemyCollisionDetector.CheckItemCollision(hitbox) != GlobalSettings.CollisionType.None)
            {
                snakeState.changeToDie();
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, GlobalSettings.BASE_SCALAR, GlobalSettings.BASE_SCALAR);

            //Remove bug from screen 3 seconds after death
            if (snakeState.state == snakeStateMachine.snakeHealth.Die)
            {
                deathTimer += gameTime.ElapsedGameTime.Milliseconds;
                //wait 3 seconds
                if (deathTimer > 3000)
                {
                    deathTimer = 0;
                    snakeState.changeToNot();
                }
            }

        }


        public void Draw()
        {
            if (snakeState.state == snakeStateMachine.snakeHealth.Die)
            {
                snakeState.MachineEnemySprite.Draw(spriteBatch, position, Color.Red);
            }

            else if ((snakeState.state != snakeStateMachine.snakeHealth.Not) && (snakeState.state != snakeStateMachine.snakeHealth.Die))
            {
                snakeState.MachineEnemySprite.Draw(spriteBatch, position, Color.White);
            }

        }

       
        //Functions to switch the states
        public void changeToIdle()
        {
            snakeState.changeToIdle();
        }

        public void changeToMoveRight()
        {
            snakeState.changeToRightMove();
        }

        public void changeToMoveLeft()
        {
            snakeState.changeToLeftMove();
        }

        public void changeToMoveUp()
        {
            snakeState.changeToMoveUp();
        }

        public void changeToMoveDown()
        {
            snakeState.changeToMoveDown();
        }

        public void changeToDie()
        {
            snakeState.changeToDie();
        }

        public void changeToNot()
        {
            snakeState.changeToNot();
        }
    }

    
    public class snakeStateMachine
    {
        public enum snakeHealth { Idle,MoveUp, MoveDown, MoveLeft, MoveRight, Die, Not }; // the different possible states
        public snakeHealth state; // the current health state of the bug
        public EnemySprite MachineEnemySprite; // The EnemySprite implementing ISprite

        //constructor for the class
        public snakeStateMachine()
        {
            state = snakeHealth.MoveUp;
            MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
        }
        public void changeToIdle()
        {
            //change to idle if not already Idle
            if (state != snakeHealth.Idle)
            {
                state = snakeHealth.Idle;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
                //get appropriate sprite from sprite factory
            }

        }

        public void changeToRightMove()
        {
            //change to Attack if not already in Attack
            if (state != snakeHealth.MoveRight)
            {
                state = snakeHealth.MoveRight;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeRightMoving();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToLeftMove()
        {
            //change to Move if not already Move
            if (state != snakeHealth.MoveLeft)
            {
                state = snakeHealth.MoveLeft;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeLeftMoving();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveUp()
        {
            //change to Move if not already Move
            if (state != snakeHealth.MoveUp)
            {
                state = snakeHealth.MoveUp;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToMoveDown()
        {
            //change to Move if not already Move
            if (state != snakeHealth.MoveDown)
            {
                state = snakeHealth.MoveDown;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeIdle();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToDie()
        {
            //change to Die if not already Die
            if (state != snakeHealth.Die)
            {
                state = snakeHealth.Die;
                MachineEnemySprite = (EnemySprite)SpriteFactory.Instance.CreateSnakeDie();
                //get appropriate sprite from sprite factory
            }
        }

        public void changeToNot()
        {
            //change to Not if not already Not
            if (state != snakeHealth.Not)
            {
                state = snakeHealth.Not;
            }
        }

    }
}
