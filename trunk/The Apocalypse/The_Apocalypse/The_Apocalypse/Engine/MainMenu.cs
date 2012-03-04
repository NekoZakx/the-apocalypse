using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace The_Apocalypse
{
    class MainMenu
    {
        enum BState
        {
            HOVER,
            UP,
            JUST_RELEASED,
            DOWN
        }
        const int NUMBER_OF_BUTTONS = 2,

            PLAY_BUTTON_INDEX = 0,
            EXIT_BUTTON_INDEX = 1,
            BUTTON_HEIGHT = 40,
            BUTTON_WIDTH = 88;

        GraphicButton[] buttons = new GraphicButton[NUMBER_OF_BUTTONS];
        SpriteFont font;

        public bool closeProgram = false, play = false;

        public MainMenu()
        {
            Initialize();
        }
        public void Initialize()
        {
            int x = 10;
            int y = 50;

            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                buttons[i] = new GraphicButton(x, y, BUTTON_WIDTH, BUTTON_HEIGHT);
                buttons[i].oneClick = true;
                y += BUTTON_HEIGHT + 10;
            }
        }
        public void setButtonData()
        {
            int x = 10;
            int y = 50;

            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                buttons[i].UpdateData(x, y, BUTTON_WIDTH, BUTTON_HEIGHT);
                y += BUTTON_HEIGHT + 10;
            }
        }


        public void LoadTexture(ContentManager Content)
        {
            buttons[EXIT_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/exit"));
            buttons[PLAY_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/play"));

            font = Content.Load<SpriteFont>(@"Fonts/TextFont");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Main Menu", new Vector2(10, 5), Color.White);

            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                spriteBatch.Draw(buttons[i].texture, buttons[i].rectangle, buttons[i].color);
            }
            spriteBatch.End();

        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                if (buttons[i].update_buttons(gameTime))
                {
                    take_action_on_button(i);
                }
            }
        }
        // Logic for each button click goes here
        void take_action_on_button(int i)
        {
            //take action corresponding to which button was clicked
            switch (i)
            {
                case EXIT_BUTTON_INDEX:
                    closeProgram = true;
                    break;
                case PLAY_BUTTON_INDEX:
                    play = true;
                    break;
                default:
                    break;
            }
        }


    }
}