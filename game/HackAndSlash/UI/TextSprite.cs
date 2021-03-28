﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace HackAndSlash
{
    class TextSprite : ISprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int totalFrames;
        private int currentFrame;
        private long animeDelay = GlobalSettings.DELAY_TIME;
        private Stopwatch stopwatch = new Stopwatch();
        private long timer;
        private int delayCounter { get; set; }

        public TextSprite(Texture2D texture, int rows, int columns)
        {
            stopwatch.Restart();
            Texture = texture;
            Rows = rows;
            Columns = columns;
            totalFrames = rows * columns;
            currentFrame = 0;
            delayCounter = 0;
        }

        public void Update()
        {
            // Record the time elapsed 
            timer = stopwatch.ElapsedMilliseconds;
            // Every time the time elpased exceeds the designated delay amount,
            // update the frame and restart the timer 
            if (timer > animeDelay)
            {
                currentFrame++;
                stopwatch.Restart();
                timer = 0;
            }
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
        }

        public void Draw(SpriteBatch spriteBatch, String stringInput, Vector2 location, Color color)
        {
            int textPos = 0;
            foreach (char character in stringInput)
            {
                currentFrame = parseCharacterToFrame(character);
                int width = Texture.Width / Columns;
                int height = Texture.Height / Rows;
                int row = (int)((float)currentFrame / (float)Columns);
                int column = currentFrame % Columns;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)location.X + textPos, (int)location.Y, width, height);
                textPos += 8;
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);
            }


        }

        private int parseCharacterToFrame(char currentCharacter)
        {
            int frame = 51;
            if (currentCharacter >= 48 && currentCharacter <= 57)
            {
                frame = currentCharacter - 9;
            } 
            else if (currentCharacter >= 65 && currentCharacter <= 90)
            {
                frame = currentCharacter - 65;
            }
            return frame;
        }
    }
}