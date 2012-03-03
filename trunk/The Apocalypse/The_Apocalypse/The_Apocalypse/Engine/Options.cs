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
    class Options
    {
        enum BState
        {
            HOVER,
            UP,
            JUST_RELEASED,
            DOWN
        }
        const int NUMBER_OF_BUTTONS = 16,
            
            VOLUMEPLUS_BUTTON_INDEX = 0,
            VOLUMEMINUS_BUTTON_INDEX = 1,
            PITCHPLUS_BUTTON_INDEX = 2,
            PITCHMINUS_BUTTON_INDEX = 3,
            PANPLUS_BUTTON_INDEX = 4,
            PANMINUS_BUTTON_INDEX = 5,
            BRIGHTNESSPLUS_BUTTON_INDEX = 6,
            BRIGHTNESSMINUS_BUTTON_INDEX = 7,
            CONTRASSPLUS_BUTTON_INDEX = 8,
            CONTRASSMINUS_BUTTON_INDEX = 9,
            DEFAULT_BUTTON_INDEX = 10,
            APPLY_BUTTON_INDEX = 11,
            TEXTBOX_INDEX = 12,
            FULLSCREEN_BUTTON_INDEX = 13,
            EXIT_BUTTON_INDEX = 14,
            MAINMENU_BUTTON_INDEX = 15,
            BUTTON_HEIGHT = 40,
            BUTTON_WIDTH = 88;

        Color[] button_color = new Color[NUMBER_OF_BUTTONS];
        Rectangle[] button_rectangle = new Rectangle[NUMBER_OF_BUTTONS];
        BState[] button_state = new BState[NUMBER_OF_BUTTONS];
        Texture2D[] button_texture = new Texture2D[NUMBER_OF_BUTTONS];
        double[] button_timer = new double[NUMBER_OF_BUTTONS];
        SpriteFont font;
        Texture2D whiteTexture;

        //mouse pressed and mouse just pressed
        bool mpressed, prev_mpressed = false;
        double frame_time;

        int brightness;
        int contrast;
        int width = 0;
        int height = 0;

        bool inputBool = false;
        public bool closeProgram = false;

        BlendState brightnessBlend;
        BlendState contrastBlend;

        SoundEffectInstance Sound_Preview;
        GraphicsDevice GraphicsDevice;
        GraphicsDeviceManager graphics;

        public Options(int X,int Y)
        {
            brightness = 255;
            contrast = 128;

            brightnessBlend = new BlendState();
            brightnessBlend.ColorSourceBlend = brightnessBlend.AlphaSourceBlend = Blend.Zero;
            brightnessBlend.ColorDestinationBlend = brightnessBlend.AlphaDestinationBlend = Blend.SourceColor;

            contrastBlend = new BlendState();
            contrastBlend.ColorSourceBlend = contrastBlend.AlphaSourceBlend = Blend.DestinationColor;
            contrastBlend.ColorDestinationBlend = contrastBlend.AlphaDestinationBlend = Blend.SourceColor;

            setButtonData(X, Y);            
        }

        public void setButtonData(int X, int Y)
        {
            height = Y*2;
            width = X*2;
            int x = X - BUTTON_WIDTH;
            int y = Y - ((NUMBER_OF_BUTTONS / 2) * BUTTON_HEIGHT) / 2;

            if (x == button_rectangle[TEXTBOX_INDEX].X && y == button_rectangle[TEXTBOX_INDEX].Y)
                return;

            button_state[TEXTBOX_INDEX] = BState.UP;
            button_color[TEXTBOX_INDEX] = Color.White;
            button_timer[TEXTBOX_INDEX] = 0.0;
            button_rectangle[TEXTBOX_INDEX] = new Rectangle(x, y, BUTTON_WIDTH * 2, BUTTON_HEIGHT);
            y += BUTTON_HEIGHT;
            button_state[FULLSCREEN_BUTTON_INDEX] = BState.UP;
            button_color[FULLSCREEN_BUTTON_INDEX] = Color.White;
            button_timer[FULLSCREEN_BUTTON_INDEX] = 0.0;
            button_rectangle[FULLSCREEN_BUTTON_INDEX] = new Rectangle(x, y, BUTTON_WIDTH * 2, BUTTON_HEIGHT);
            y += BUTTON_HEIGHT;

            for (int i = 0; i < NUMBER_OF_BUTTONS; i = i + 2)
            {
                if (i != TEXTBOX_INDEX && i != FULLSCREEN_BUTTON_INDEX)
                {
                    button_state[i + 1] = BState.UP;
                    button_color[i + 1] = Color.White;
                    button_timer[i + 1] = 0.0;
                    button_rectangle[i + 1] = new Rectangle(x, y, BUTTON_WIDTH, BUTTON_HEIGHT);

                    button_state[i] = BState.UP;
                    button_color[i] = Color.White;
                    button_timer[i] = 0.0;
                    button_rectangle[i] = new Rectangle(x + BUTTON_WIDTH, y, BUTTON_WIDTH, BUTTON_HEIGHT);
                    y += BUTTON_HEIGHT;
                }
            }
        }


        public void LoadTexture(ContentManager Content, GraphicsDevice GraphicsDevice, GraphicsDeviceManager graphics)
        {
            this.GraphicsDevice = GraphicsDevice;
            this.graphics = graphics;

            //Ligne de la saisie du nom du joueur
            button_texture[TEXTBOX_INDEX] =              Content.Load<Texture2D>(@"Button/textbox");
            
            //Ligne du mode Plein Écran
            button_texture[FULLSCREEN_BUTTON_INDEX] =    Content.Load<Texture2D>(@"Button/fullscreen");
            
            //Ligne du Volume
            button_texture[VOLUMEMINUS_BUTTON_INDEX] = Content.Load<Texture2D>(@"Button/volume-");
            button_texture[VOLUMEPLUS_BUTTON_INDEX] =    Content.Load<Texture2D>(@"Button/volume+");

            //Ligne du Pitch
            button_texture[PITCHMINUS_BUTTON_INDEX] = Content.Load<Texture2D>(@"Button/pitch-");
            button_texture[PITCHPLUS_BUTTON_INDEX] =     Content.Load<Texture2D>(@"Button/pitch+");

            //Ligne de la Balance
            button_texture[PANMINUS_BUTTON_INDEX] = Content.Load<Texture2D>(@"Button/pan-");
            button_texture[PANPLUS_BUTTON_INDEX] =       Content.Load<Texture2D>(@"Button/pan+");

            //Ligne du Brightness
            button_texture[BRIGHTNESSMINUS_BUTTON_INDEX] = Content.Load<Texture2D>(@"Button/bright-");
            button_texture[BRIGHTNESSPLUS_BUTTON_INDEX]= Content.Load<Texture2D>(@"Button/bright+");

            //Ligne de la Contraste
            button_texture[CONTRASSMINUS_BUTTON_INDEX] = Content.Load<Texture2D>(@"Button/contrass-");
            button_texture[CONTRASSPLUS_BUTTON_INDEX] =  Content.Load<Texture2D>(@"Button/contrass+");

            //Ligne de validation des paramètres ou remise à zéro
            button_texture[APPLY_BUTTON_INDEX] =         Content.Load<Texture2D>(@"Button/apply");
            button_texture[DEFAULT_BUTTON_INDEX] =       Content.Load<Texture2D>(@"Button/default");

            //Ligne pour quitter la partie en cours ou le programme
            button_texture[MAINMENU_BUTTON_INDEX] =      Content.Load<Texture2D>(@"Button/mainmenu");
            button_texture[EXIT_BUTTON_INDEX] =          Content.Load<Texture2D>(@"Button/exit");

            font = Content.Load<SpriteFont>(@"Fonts/TextFont");

            

            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });

            Sound_Preview = (Content.Load<SoundEffect>(@"SoundFX/pistolshoot")).CreateInstance();

            LoadPreferenceData();
        }

        // wrapper for hit_image_alpha taking Rectangle and Texture
        Boolean hit_image_alpha(Rectangle rect, Texture2D tex, int x, int y)
        {
            return hit_image_alpha(0, 0, tex, tex.Width * (x - rect.X) /
                rect.Width, tex.Height * (y - rect.Y) / rect.Height);
        }

        // wraps hit_image then determines if hit a transparent part of image
        Boolean hit_image_alpha(float tx, float ty, Texture2D tex, int x, int y)
        {
            if (hit_image(tx, ty, tex, x, y))
            {
                uint[] data = new uint[tex.Width * tex.Height];
                tex.GetData<uint>(data);
                if ((x - (int)tx) + (y - (int)ty) *
                    tex.Width < tex.Width * tex.Height)
                {
                    return ((data[
                        (x - (int)tx) + (y - (int)ty) * tex.Width
                        ] &
                                0xFF000000) >> 24) > 20;
                }
            }
            return false;
        }

        // determine if x,y is within rectangle formed by texture located at tx,ty
        Boolean hit_image(float tx, float ty, Texture2D tex, int x, int y)
        {
            return (x >= tx &&
                x <= tx + tex.Width &&
                y >= ty &&
                y <= ty + tex.Height);
        }

        // determine state and color of button
        public void update_buttons(GameTime gameTime)
        {
            if (inputBool)
            {
                inputKeyboard();
            }
            MouseState mouse_state = Mouse.GetState();
            prev_mpressed = mpressed;
            mpressed = mouse_state.LeftButton == ButtonState.Pressed;
            frame_time = gameTime.ElapsedGameTime.Milliseconds / 1000.0;
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {

                if (hit_image_alpha(
                    button_rectangle[i], button_texture[i], mouse_state.X, mouse_state.Y))
                {
                    button_timer[i] = 0.0;
                    if (mpressed)
                    {
                        // mouse is currently down
                        button_state[i] = BState.DOWN;
                        button_color[i] = Color.Red;
                    }
                    else if (!mpressed && prev_mpressed)
                    {
                        // mouse was just released
                        if (button_state[i] == BState.DOWN)
                        {
                            // button i was just down
                            button_state[i] = BState.JUST_RELEASED;
                        }
                    }
                    else
                    {
                        button_state[i] = BState.HOVER;
                        button_color[i] = Color.LightBlue;
                    }
                }
                else
                {
                    button_state[i] = BState.UP;
                    if (button_timer[i] > 0)
                    {
                        button_timer[i] = button_timer[i] - frame_time;
                    }
                    else
                    {
                        button_color[i] = Color.White;
                    }
                }

                if (button_state[i] == BState.DOWN)
                {
                    take_action_on_button(i);
                }
            }
            
        }

        string inputText = "Player";

        DateTime update = DateTime.Now;
        public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            
            spriteBatch.Begin(SpriteSortMode.Immediate, brightnessBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, width, height), new Color(brightness, brightness, brightness, 255));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, contrastBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, width, height), new Color(contrast, contrast, contrast, 255));
            spriteBatch.End();

            GraphicsDevice.BlendState = BlendState.Opaque;

            spriteBatch.Begin(SpriteSortMode.Immediate, brightnessBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(button_rectangle[TEXTBOX_INDEX].X, button_rectangle[TEXTBOX_INDEX].Y - BUTTON_HEIGHT, BUTTON_WIDTH * 2, BUTTON_HEIGHT), new Color(100, 100, 100, 255));
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "PAUSE", new Vector2(button_rectangle[TEXTBOX_INDEX].X + BUTTON_WIDTH - (font.MeasureString("PAUSE").Length() / 2), button_rectangle[TEXTBOX_INDEX].Y - BUTTON_HEIGHT + 5), Color.White);

            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                spriteBatch.Draw(button_texture[i], button_rectangle[i], button_color[i]);
                if (i == 1)
                {
                    spriteBatch.DrawString(font, ((int)(Sound_Preview.Volume * 100)).ToString() + "%", new Vector2(button_rectangle[i].X + BUTTON_WIDTH * 2 + 50, button_rectangle[i].Y), Color.White);
                }
                else if (i == 3)
                {
                    spriteBatch.DrawString(font, ((int)(Sound_Preview.Pitch * 100)).ToString() + "%", new Vector2(button_rectangle[i].X + BUTTON_WIDTH * 2 + 50, button_rectangle[i].Y), Color.White);
                }
                else if (i == 5)
                {
                    spriteBatch.DrawString(font, ((int)(Sound_Preview.Pan * 100)).ToString() + "%", new Vector2(button_rectangle[i].X + BUTTON_WIDTH * 2 + 50, button_rectangle[i].Y), Color.White);
                }
                else if (i == 7)
                {
                    spriteBatch.DrawString(font, brightness.ToString(), new Vector2(button_rectangle[i].X + BUTTON_WIDTH * 2 + 50, button_rectangle[i].Y), Color.White);
                }
                else if (i == 9)
                {
                    spriteBatch.DrawString(font, contrast.ToString(), new Vector2(button_rectangle[i].X + BUTTON_WIDTH * 2 + 50, button_rectangle[i].Y), Color.White);
                }
                else if (i == FULLSCREEN_BUTTON_INDEX)
                {
                    spriteBatch.DrawString(font, graphics.IsFullScreen.ToString(), new Vector2(button_rectangle[i].X + BUTTON_WIDTH * 2 + 50, button_rectangle[i].Y), Color.White);
                }
            }
            if (inputBool)
            {
                if ((DateTime.Now - update).Milliseconds >= 500 && (DateTime.Now - update).Milliseconds < 1000)
                    spriteBatch.DrawString(font, inputText + "|", new Vector2(button_rectangle[TEXTBOX_INDEX].X + BUTTON_WIDTH - (font.MeasureString(inputText).Length() / 2), button_rectangle[TEXTBOX_INDEX].Y + 5), Color.Black);
                else if ((DateTime.Now - update).Milliseconds >= 0 && (DateTime.Now - update).Milliseconds < 500)
                    spriteBatch.DrawString(font, inputText, new Vector2(button_rectangle[TEXTBOX_INDEX].X + BUTTON_WIDTH - (font.MeasureString(inputText).Length() / 2), button_rectangle[TEXTBOX_INDEX].Y + 5), Color.Black);
                else
                    update = DateTime.Now;
            }
            else
            {
                spriteBatch.DrawString(font, inputText, new Vector2(button_rectangle[TEXTBOX_INDEX].X + BUTTON_WIDTH - (font.MeasureString(inputText).Length() / 2), button_rectangle[TEXTBOX_INDEX].Y + 5), Color.Black);
            }
            spriteBatch.End();
            
        }
        
        // Logic for each button click goes here
        void take_action_on_button(int i)
        {
            //take action corresponding to which button was clicked
            switch (i)
            {
                case VOLUMEPLUS_BUTTON_INDEX:
                    Sound_Preview.Play();
                    if (Sound_Preview.Volume + (float)0.01 <= 1)
                        Sound_Preview.Volume += (float)0.01;
                    else if (Sound_Preview.Volume + (float)0.01 >= 1)
                        Sound_Preview.Volume = 1;
                    break;
                case VOLUMEMINUS_BUTTON_INDEX:
                    Sound_Preview.Play();
                    if (Sound_Preview.Volume - (float)0.01 >= 0)
                        Sound_Preview.Volume -= (float)0.01;
                    else if (Sound_Preview.Volume - (float)0.01 < 0)
                        Sound_Preview.Volume = 0;
                    break;
                case PITCHPLUS_BUTTON_INDEX:
                    Sound_Preview.Play();
                    if (Sound_Preview.Pitch + (float)0.01 <= 1)
                        Sound_Preview.Pitch += (float)0.01;
                    else if (Sound_Preview.Pitch + (float)0.01 >= 1)
                        Sound_Preview.Pitch = 1;
                    break;
                case PITCHMINUS_BUTTON_INDEX:
                    Sound_Preview.Play();
                    if (Sound_Preview.Pitch - (float)0.01 >= -1)
                        Sound_Preview.Pitch -= (float)0.01;
                    else if (Sound_Preview.Pitch - (float)0.01 <= -1)
                        Sound_Preview.Pitch = -1;
                    break;
                case PANPLUS_BUTTON_INDEX:
                    Sound_Preview.Play();
                    if (Sound_Preview.Pan + (float)0.01 <= 1)
                        Sound_Preview.Pan += (float)0.01;
                    else if (Sound_Preview.Pan + (float)0.01 >= 1)
                        Sound_Preview.Pan = 1;
                    break;
                case PANMINUS_BUTTON_INDEX:
                    Sound_Preview.Play();
                    if (Sound_Preview.Pan - (float)0.01 >= -1)
                        Sound_Preview.Pan -= (float)0.01;
                    else if (Sound_Preview.Pan - (float)0.01 <= -1)
                        Sound_Preview.Pan = -1;
                    break;
                case BRIGHTNESSPLUS_BUTTON_INDEX:
                    brightness++;
                    break;
                case BRIGHTNESSMINUS_BUTTON_INDEX:
                    brightness--;
                    break;
                case CONTRASSPLUS_BUTTON_INDEX:
                    contrast++;
                    break;
                case CONTRASSMINUS_BUTTON_INDEX:
                    contrast--;
                    break;
                case APPLY_BUTTON_INDEX:
                    ApplyChanges();
                    break;
                case DEFAULT_BUTTON_INDEX:
                    DefaultConfig();
                    break;
                case TEXTBOX_INDEX:
                    inputBool = true;
                    break;
                case FULLSCREEN_BUTTON_INDEX:
                    graphics.IsFullScreen = !graphics.IsFullScreen;
                    graphics.ApplyChanges();
                    break;
                case EXIT_BUTTON_INDEX:
                    closeProgram = true;
                    break;
                default:
                    break;
            }
        }
        public void LoadPreferenceData()
        {
            //Load from Database.sdf
            brightness = 255;
            contrast = 128;
            Sound_Preview.Volume = 1;
            Sound_Preview.Pitch = 0;
            Sound_Preview.Pan = 0;
        }
        void ApplyChanges()
        {
            //Save to Database.sdf
        }
        void DefaultConfig()
        {
            brightness = 255;
            contrast = 128;
            Sound_Preview.Volume = 1;
            Sound_Preview.Pitch = 0;
            Sound_Preview.Pan = 0;
            inputText = "Player";
            graphics.IsFullScreen = false;
            //Save to Database.sdf
        }


        Keys[] keysToCheck = new Keys[] { 
    Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
    Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
    Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
    Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
    Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
    Keys.Z, 
    Keys.D0, Keys.D1, Keys.D2, Keys.D3, 
    Keys.D4, Keys.D5, Keys.D6, Keys.D7, 
    Keys.D8, Keys.D9, 
    Keys.Back, Keys.Space, Keys.Enter };

        KeyboardState currentKeyboardState;
        KeyboardState lastKeyboardState;
        void inputKeyboard()
        {
            currentKeyboardState = Keyboard.GetState();

            foreach (Keys key in keysToCheck)
            {
                if (CheckKey(key))
                {
                    AddKeyToText(key);
                    break;
                }
            }
            lastKeyboardState = currentKeyboardState;
        }

        private void AddKeyToText(Keys key)
        {
            string newChar = "";

            if (inputText.Length >= 15 && key != Keys.Back)
                return;

            switch (key)
            {
                case Keys.D0:
                    newChar += "0";
                    break;
                case Keys.D1:
                    newChar += "1";
                    break;
                case Keys.D2:
                    newChar += "2";
                    break;
                case Keys.D3:
                    newChar += "3";
                    break;
                case Keys.D4:
                    newChar += "4";
                    break;
                case Keys.D5:
                    newChar += "5";
                    break;
                case Keys.D6:
                    newChar += "6";
                    break;
                case Keys.D7:
                    newChar += "7";
                    break;
                case Keys.D8:
                    newChar += "8";
                    break;
                case Keys.D9:
                    newChar += "9";
                    break;
                case Keys.A:
                    newChar += "a";
                    break;
                case Keys.B:
                    newChar += "b";
                    break;
                case Keys.C:
                    newChar += "c";
                    break;
                case Keys.D:
                    newChar += "d";
                    break;
                case Keys.E:
                    newChar += "e";
                    break;
                case Keys.F:
                    newChar += "f";
                    break;
                case Keys.G:
                    newChar += "g";
                    break;
                case Keys.H:
                    newChar += "h";
                    break;
                case Keys.I:
                    newChar += "i";
                    break;
                case Keys.J:
                    newChar += "j";
                    break;
                case Keys.K:
                    newChar += "k";
                    break;
                case Keys.L:
                    newChar += "l";
                    break;
                case Keys.M:
                    newChar += "m";
                    break;
                case Keys.N:
                    newChar += "n";
                    break;
                case Keys.O:
                    newChar += "o";
                    break;
                case Keys.P:
                    newChar += "p";
                    break;
                case Keys.Q:
                    newChar += "q";
                    break;
                case Keys.R:
                    newChar += "r";
                    break;
                case Keys.S:
                    newChar += "s";
                    break;
                case Keys.T:
                    newChar += "t";
                    break;
                case Keys.U:
                    newChar += "u";
                    break;
                case Keys.V:
                    newChar += "v";
                    break;
                case Keys.W:
                    newChar += "w";
                    break;
                case Keys.X:
                    newChar += "x";
                    break;
                case Keys.Y:
                    newChar += "y";
                    break;
                case Keys.Z:
                    newChar += "z";
                    break;
                case Keys.Space:
                    newChar += " ";
                    break;
                case Keys.Enter:
                    inputBool = false;
                    break;
                case Keys.Back:
                    if (inputText.Length != 0)
                        inputText = inputText.Remove(inputText.Length - 1);
                    return;
            }
            if (currentKeyboardState.IsKeyDown(Keys.RightShift) ||
                currentKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                newChar = newChar.ToUpper();
            }
            inputText += newChar;
        }

        private bool CheckKey(Keys theKey)
        {
            return lastKeyboardState.IsKeyDown(theKey) && currentKeyboardState.IsKeyUp(theKey);
        }


    }
}
