using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Xml;

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
        const int NUMBER_OF_BUTTONS = 18,

            MVOLUMEPLUS_BUTTON_INDEX = 0,
            MVOLUMEMINUS_BUTTON_INDEX = 1,
            SVOLUMEPLUS_BUTTON_INDEX = 2,
            SVOLUMEMINUS_BUTTON_INDEX = 3,
            PITCHPLUS_BUTTON_INDEX = 4,
            PITCHMINUS_BUTTON_INDEX = 5,
            PANPLUS_BUTTON_INDEX = 6,
            PANMINUS_BUTTON_INDEX = 7,
            BRIGHTNESSPLUS_BUTTON_INDEX = 8,
            BRIGHTNESSMINUS_BUTTON_INDEX = 9,
            CONTRASTPLUS_BUTTON_INDEX = 10,
            CONTRASTMINUS_BUTTON_INDEX = 11,
            DEFAULT_BUTTON_INDEX = 12,
            APPLY_BUTTON_INDEX = 13,
            TEXTBOX_INDEX = 14,
            FULLSCREEN_BUTTON_INDEX = 15,
            EXIT_BUTTON_INDEX = 16,
            MAINMENU_BUTTON_INDEX = 17,
            BUTTON_HEIGHT = 40,
            BUTTON_WIDTH = 88;

        GraphicButton[] buttons = new GraphicButton[NUMBER_OF_BUTTONS];
        SpriteFont font;
        Texture2D whiteTexture;

        int brightness;
        int contrast;
        int width = 0;
        int height = 0;

        public bool closeProgram = false, mainMenu = false, changes = false;

        BlendState brightnessBlend;
        BlendState contrastBlend;

        SoundEffectInstance Sound_Preview;
        SoundEffectInstance Music_Preview;
        GraphicsDevice GraphicsDevice;
        GraphicsDeviceManager graphics;

        public Options(int X, int Y)
        {
            brightness = 255;
            contrast = 128;

            brightnessBlend = new BlendState();
            brightnessBlend.ColorSourceBlend = brightnessBlend.AlphaSourceBlend = Blend.Zero;
            brightnessBlend.ColorDestinationBlend = brightnessBlend.AlphaDestinationBlend = Blend.SourceColor;

            contrastBlend = new BlendState();
            contrastBlend.ColorSourceBlend = contrastBlend.AlphaSourceBlend = Blend.DestinationColor;
            contrastBlend.ColorDestinationBlend = contrastBlend.AlphaDestinationBlend = Blend.SourceColor;

            InitializeButton(X, Y);
        }

        public void InitializeButton(int X, int Y)
        {
            height = Y * 2;
            width = X * 2;
            int x = X - BUTTON_WIDTH;
            int y = Y - ((NUMBER_OF_BUTTONS / 2) * BUTTON_HEIGHT) / 2;
            if(buttons[TEXTBOX_INDEX] != null)
                if (x == buttons[TEXTBOX_INDEX].rectangle.X && y == buttons[TEXTBOX_INDEX].rectangle.Y)
                    return;


            buttons[TEXTBOX_INDEX] = new GraphicButton(x, y, BUTTON_WIDTH * 2, BUTTON_HEIGHT);
            y += BUTTON_HEIGHT;

            buttons[FULLSCREEN_BUTTON_INDEX] = new GraphicButton(x, y, BUTTON_WIDTH * 2, BUTTON_HEIGHT);
            y += BUTTON_HEIGHT;

            for (int i = 0; i < NUMBER_OF_BUTTONS; i = i + 2)
            {
                if (i != TEXTBOX_INDEX && i != FULLSCREEN_BUTTON_INDEX)
                {
                    buttons[i + 1] = new GraphicButton(x, y, BUTTON_WIDTH, BUTTON_HEIGHT);
                    buttons[i] = new GraphicButton(x + BUTTON_WIDTH, y, BUTTON_WIDTH, BUTTON_HEIGHT);
                    y += BUTTON_HEIGHT;
                }
            }

            buttons[APPLY_BUTTON_INDEX].oneClick = true;
            buttons[DEFAULT_BUTTON_INDEX].oneClick = true;
            buttons[MAINMENU_BUTTON_INDEX].oneClick = true;
            buttons[EXIT_BUTTON_INDEX].oneClick = true;
        }
        
        public void setButtonData(int X, int Y)
        {
            height = Y * 2;
            width = X * 2;
            int x = X - BUTTON_WIDTH;
            int y = Y - ((NUMBER_OF_BUTTONS / 2) * BUTTON_HEIGHT) / 2;
            if (buttons[TEXTBOX_INDEX] != null)
                if (x == buttons[TEXTBOX_INDEX].rectangle.X && y == buttons[TEXTBOX_INDEX].rectangle.Y)
                    return;


            buttons[TEXTBOX_INDEX].UpdateData(x, y, BUTTON_WIDTH * 2, BUTTON_HEIGHT);
            y += BUTTON_HEIGHT;

            buttons[FULLSCREEN_BUTTON_INDEX].UpdateData(x, y, BUTTON_WIDTH * 2, BUTTON_HEIGHT);
            y += BUTTON_HEIGHT;

            for (int i = 0; i < NUMBER_OF_BUTTONS; i = i + 2)
            {
                if (i != TEXTBOX_INDEX && i != FULLSCREEN_BUTTON_INDEX)
                {
                    buttons[i + 1].UpdateData(x, y, BUTTON_WIDTH, BUTTON_HEIGHT);
                    buttons[i].UpdateData(x + BUTTON_WIDTH, y, BUTTON_WIDTH, BUTTON_HEIGHT);
                    y += BUTTON_HEIGHT;
                }
            }

            buttons[APPLY_BUTTON_INDEX].oneClick = true;
            buttons[DEFAULT_BUTTON_INDEX].oneClick = true;
            buttons[MAINMENU_BUTTON_INDEX].oneClick = true;
            buttons[EXIT_BUTTON_INDEX].oneClick = true;
        }


        public void LoadTexture(ContentManager Content, GraphicsDevice GraphicsDevice, GraphicsDeviceManager graphics)
        {
            this.GraphicsDevice = GraphicsDevice;
            this.graphics = graphics;

            //Ligne de la saisie du nom du joueur
            buttons[TEXTBOX_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/textbox"));

            //Ligne du mode Plein Écran
            buttons[FULLSCREEN_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/fullscreen"));
            
            //Ligne du Volume de la musique de fond
            buttons[MVOLUMEMINUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/mvolume-"));
            buttons[MVOLUMEPLUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/mvolume+"));
            
            //Ligne du Volume des effets sonores
            buttons[SVOLUMEMINUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/svolume-"));
            buttons[SVOLUMEPLUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/svolume+"));

            //Ligne du Volume des effets sonores
            buttons[SVOLUMEMINUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/svolume-"));
            buttons[SVOLUMEPLUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/svolume+"));

            //Ligne du Pitch des effets sonores
            buttons[PITCHMINUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/pitch-"));
            buttons[PITCHPLUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/pitch+"));

            //Ligne de la Balance des effets sonores
            buttons[PANMINUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>( @"Button/pan-"));
            buttons[PANPLUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/pan+"));

            //Ligne du Brightness
            buttons[BRIGHTNESSMINUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/bright-"));
            buttons[BRIGHTNESSPLUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/bright+"));

            //Ligne de la Contraste
            buttons[CONTRASTMINUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/contrast-"));
            buttons[CONTRASTPLUS_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/contrast+"));

            //Ligne de validation des paramètres ou remise à zéro
            buttons[APPLY_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/apply"));
            buttons[DEFAULT_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/default"));

            //Ligne pour quitter la partie en cours ou le programme
            buttons[MAINMENU_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/mainmenu"));
            buttons[EXIT_BUTTON_INDEX].LoadContent(Content.Load<Texture2D>(@"Button/exit"));

            font = Content.Load<SpriteFont>(@"Fonts/TextFont");



            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });

            Sound_Preview = (Content.Load<SoundEffect>(@"SoundFX/pistolshoot")).CreateInstance();
            Music_Preview = (Content.Load<SoundEffect>(@"Music/bgmusic")).CreateInstance();

            LoadPreferenceData();
        }

        

        DateTime update = DateTime.Now;
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.Immediate, brightnessBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, width, height), new Color(brightness, brightness, brightness, 255));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, contrastBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(0, 0, width, height), new Color(contrast, contrast, contrast, 255));
            spriteBatch.End();

            GraphicsDevice.BlendState = BlendState.Opaque;

            spriteBatch.Begin(SpriteSortMode.Immediate, brightnessBlend);
            spriteBatch.Draw(whiteTexture, new Rectangle(buttons[TEXTBOX_INDEX].rectangle.X, buttons[TEXTBOX_INDEX].rectangle.Y - BUTTON_HEIGHT, BUTTON_WIDTH * 2, BUTTON_HEIGHT), new Color(100, 100, 100, 255));
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(font, "PAUSE", new Vector2(buttons[TEXTBOX_INDEX].rectangle.X + BUTTON_WIDTH - (font.MeasureString("PAUSE").Length() / 2), buttons[TEXTBOX_INDEX].rectangle.Y - BUTTON_HEIGHT + 5), Color.White);

            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                spriteBatch.Draw(buttons[i].texture, buttons[i].rectangle, buttons[i].color);
                if (i == 1)
                {
                    spriteBatch.DrawString(font, ((int)(Music_Preview.Volume * 100)).ToString() + "%", new Vector2(buttons[i].rectangle.X + BUTTON_WIDTH * 2 + 50, buttons[i].rectangle.Y), Color.White);
                }else if (i == 3)
                {
                    spriteBatch.DrawString(font, ((int)(Sound_Preview.Volume * 100)).ToString() + "%", new Vector2(buttons[i].rectangle.X + BUTTON_WIDTH * 2 + 50, buttons[i].rectangle.Y), Color.White);
                }
                else if (i == 5)
                {
                    spriteBatch.DrawString(font, ((int)(Sound_Preview.Pitch * 100)).ToString() + "%", new Vector2(buttons[i].rectangle.X + BUTTON_WIDTH * 2 + 50, buttons[i].rectangle.Y), Color.White);
                }
                else if (i == 7)
                {
                    spriteBatch.DrawString(font, ((int)(Sound_Preview.Pan * 100)).ToString() + "%", new Vector2(buttons[i].rectangle.X + BUTTON_WIDTH * 2 + 50, buttons[i].rectangle.Y), Color.White);
                }
                else if (i == 11)
                {
                    spriteBatch.DrawString(font, brightness.ToString(), new Vector2(buttons[i].rectangle.X + BUTTON_WIDTH * 2 + 50, buttons[i].rectangle.Y), Color.White);
                }
                else if (i == 13)
                {
                    spriteBatch.DrawString(font, contrast.ToString(), new Vector2(buttons[i].rectangle.X + BUTTON_WIDTH * 2 + 50, buttons[i].rectangle.Y), Color.White);
                }
                else if (i == FULLSCREEN_BUTTON_INDEX)
                {
                    spriteBatch.DrawString(font, graphics.IsFullScreen.ToString(), new Vector2(buttons[i].rectangle.X + BUTTON_WIDTH * 2 + 50, buttons[i].rectangle.Y), Color.White);
                }
            }
            if (buttons[TEXTBOX_INDEX].input)
            {
                if ((DateTime.Now - update).Milliseconds >= 500 && (DateTime.Now - update).Milliseconds < 1000)
                    spriteBatch.DrawString(font, buttons[TEXTBOX_INDEX].Text + "|", new Vector2(buttons[TEXTBOX_INDEX].rectangle.X + BUTTON_WIDTH - (font.MeasureString(buttons[TEXTBOX_INDEX].Text).Length() / 2), buttons[TEXTBOX_INDEX].rectangle.Y + 5), Color.Black);
                else if ((DateTime.Now - update).Milliseconds >= 0 && (DateTime.Now - update).Milliseconds < 500)
                    spriteBatch.DrawString(font, buttons[TEXTBOX_INDEX].Text, new Vector2(buttons[TEXTBOX_INDEX].rectangle.X + BUTTON_WIDTH - (font.MeasureString(buttons[TEXTBOX_INDEX].Text).Length() / 2), buttons[TEXTBOX_INDEX].rectangle.Y + 5), Color.Black);
                else
                    update = DateTime.Now;
            }
            else
            {
                spriteBatch.DrawString(font, buttons[TEXTBOX_INDEX].Text, new Vector2(buttons[TEXTBOX_INDEX].rectangle.X + BUTTON_WIDTH - (font.MeasureString(buttons[TEXTBOX_INDEX].Text).Length() / 2), buttons[TEXTBOX_INDEX].rectangle.Y + 5), Color.Black);
            }
            spriteBatch.End();

        }

        // Logic for each button click goes here
        void take_action_on_button(int i)
        {
            //take action corresponding to which button was clicked
            switch (i)
            {
                case MVOLUMEPLUS_BUTTON_INDEX:
                    Music_Preview.Play();
                    if (Music_Preview.Volume + (float)0.01 <= 1)
                        Music_Preview.Volume += (float)0.01;
                    else if (Music_Preview.Volume + (float)0.01 >= 1)
                        Music_Preview.Volume = 1;
                    break;
                case MVOLUMEMINUS_BUTTON_INDEX:
                    Music_Preview.Play();
                    if (Music_Preview.Volume - (float)0.01 >= 0)
                        Music_Preview.Volume -= (float)0.01;
                    else if (Music_Preview.Volume - (float)0.01 < 0)
                        Music_Preview.Volume = 0;
                    break;
                case SVOLUMEPLUS_BUTTON_INDEX:
                    Sound_Preview.Play();
                    if (Sound_Preview.Volume + (float)0.01 <= 1)
                        Sound_Preview.Volume += (float)0.01;
                    else if (Sound_Preview.Volume + (float)0.01 >= 1)
                        Sound_Preview.Volume = 1;
                    break;
                case SVOLUMEMINUS_BUTTON_INDEX:
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
                    if(brightness < 255)
                        brightness++;
                    break;
                case BRIGHTNESSMINUS_BUTTON_INDEX:
                    if (brightness > 50)
                        brightness--;
                    break;
                case CONTRASTPLUS_BUTTON_INDEX:
                    if (contrast < 255)
                        contrast++;
                    break;
                case CONTRASTMINUS_BUTTON_INDEX:
                    if (contrast > 0)
                        contrast--;
                    break;
                case APPLY_BUTTON_INDEX:
                    ApplyChanges();
                    break;
                case DEFAULT_BUTTON_INDEX:
                    DefaultConfig();
                    break;
                case TEXTBOX_INDEX:
                    buttons[TEXTBOX_INDEX].input = true;
                    break;
                case FULLSCREEN_BUTTON_INDEX:
                    graphics.IsFullScreen = !graphics.IsFullScreen;
                    graphics.ApplyChanges();
                    break;
                case EXIT_BUTTON_INDEX:
                    closeProgram = true;
                    break;
                case MAINMENU_BUTTON_INDEX:
                    mainMenu = true;
                    break;
                default:
                    break;
            }
        }

        DateTime timeMusic = DateTime.Now;
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                if (buttons[i].update_buttons(gameTime))
                {
                    take_action_on_button(i);
                }
            }
            if ((DateTime.Now - timeMusic).TotalMilliseconds >= 5000)
            {
                timeMusic = DateTime.Now;
                Music_Preview.Stop();
            }
        }

        public void LoadPreferenceData()
        {
            //Load from Database.sdf
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenRead("Preference.xml");

            buttons[TEXTBOX_INDEX].Text = file.ReadNextTextNode();
            brightness = Int32.Parse(file.ReadNextTextNode());
            contrast = Int32.Parse(file.ReadNextTextNode());
            Music_Preview.Volume = float.Parse(file.ReadNextTextNode());
            Sound_Preview.Volume = float.Parse(file.ReadNextTextNode());
            Sound_Preview.Pitch = float.Parse(file.ReadNextTextNode());
            Sound_Preview.Pan = float.Parse(file.ReadNextTextNode());
            graphics.IsFullScreen = Boolean.Parse(file.ReadNextTextNode());

            file.ReadClose();
            graphics.ApplyChanges();
        }

        void ApplyChanges()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenWrite("Preference.xml");

            file.WriteCategory("Preference");

            file.WriteNextTextNode("username", buttons[TEXTBOX_INDEX].Text);
            file.WriteNextTextNode("brightness", brightness.ToString());
            file.WriteNextTextNode("contrast", contrast.ToString());
            file.WriteNextTextNode("Mvolume", Music_Preview.Volume.ToString());
            file.WriteNextTextNode("SFXvolume", Sound_Preview.Volume.ToString());
            file.WriteNextTextNode("SFXpitch", Sound_Preview.Pitch.ToString());
            file.WriteNextTextNode("pan", Sound_Preview.Pan.ToString());
            file.WriteNextTextNode("fullscreen", graphics.IsFullScreen.ToString());
            file.WriteNextTextNode("width", this.graphics.PreferredBackBufferWidth.ToString());
            file.WriteNextTextNode("height", this.graphics.PreferredBackBufferHeight.ToString());

            file.WriteEndCategory();

            file.WriteClose();
            changes = true;
        }

        void DefaultConfig()
        {
            XmlReaderWriter file = new XmlReaderWriter();
            file.OpenWrite("Preference.xml");

            file.WriteCategory("Preference");

            file.WriteNextTextNode("username", buttons[TEXTBOX_INDEX].Text);
            file.WriteNextTextNode("brightness", "255");
            file.WriteNextTextNode("contrast", "128");
            file.WriteNextTextNode("Mvolume", "1");
            file.WriteNextTextNode("SFXvolume", "1");
            file.WriteNextTextNode("SFXpitch", "0");
            file.WriteNextTextNode("pan", "0");
            file.WriteNextTextNode("fullscreen", graphics.IsFullScreen.ToString());
            file.WriteNextTextNode("width", this.graphics.PreferredBackBufferWidth.ToString());
            file.WriteNextTextNode("height", this.graphics.PreferredBackBufferHeight.ToString());

            file.WriteEndCategory();

            file.WriteClose();
            LoadPreferenceData();
        }


        


    }
}
