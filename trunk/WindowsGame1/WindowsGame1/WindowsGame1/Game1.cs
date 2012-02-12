using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // Global variables
        enum BState
        {
            HOVER,
            UP,
            JUST_RELEASED,
            DOWN
        }
        const int NUMBER_OF_BUTTONS = 10,
            EASY_BUTTON_INDEX = 0,
            MEDIUM_BUTTON_INDEX = 1,
            HARD_BUTTON_INDEX = 2,
            HANDGUN_BUTTON_INDEX = 3,
            ZOMBIE1_BUTTON_INDEX = 4,
            ZOMBIE2_BUTTON_INDEX = 5,
            VOLUMEPLUS_BUTTON_INDEX = 6,
            VOLUMEMINUS_BUTTON_INDEX = 7,
            PITCHPLUS_BUTTON_INDEX = 8,
            PITCHMINUS_BUTTON_INDEX = 9,
            BUTTON_HEIGHT = 40,
            BUTTON_WIDTH = 88;
        Color background_color;
        Color[] button_color = new Color[NUMBER_OF_BUTTONS];
        Rectangle[] button_rectangle = new Rectangle[NUMBER_OF_BUTTONS];
        BState[] button_state = new BState[NUMBER_OF_BUTTONS];
        Texture2D[] button_texture = new Texture2D[NUMBER_OF_BUTTONS];
        double[] button_timer = new double[NUMBER_OF_BUTTONS];
        //mouse pressed and mouse just pressed
        bool mpressed, prev_mpressed = false;
        //mouse location in window.
        int mx, my;
        double frame_time;
        SFXBank sb;







        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        //Rectangle de sélection dans le fichier du sprite (où est l'image à afficher)
        Rectangle src,src2;
        //Objet texture qui sera chargé
        Texture2D sprite, sprite2;
        //Rectangle de destination : où l'image sera affichée sur l'écran et sa taille
        Rectangle dst,dst2;
        //Si on veut faire tourner le sprite
        float rotation;
        Vector2 location, speed, curMousePos;
        Texture2D blank;
        SpriteEffects orientation = SpriteEffects.FlipHorizontally;
        Color shoot = Color.Yellow;
        Rectangle textBox;
        String parsedText;
        String typedText;
        double typedTextLength;
        int delayInMilliseconds;
        bool isDoneDrawing;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            src = new Rectangle(0, 0, 127, 147);
            src2 = new Rectangle(0, 0, 30, 30);
            rotation = 0.0f;
            dst = new Rectangle();
            dst2 = new Rectangle();
            dst.X = 32;
            dst.Y = 32;
            dst2.X = 32;
            dst2.Y = 32;
            dst.Width = src.Width;
            dst.Height = src.Height;
            dst2.Width = src2.Width;
            dst2.Height = src2.Height;

            //Definition de la taille de jeu maximal, Pour améliorer la qualité il faudrait "strech" les éléments
            this.graphics.PreferredBackBufferWidth = 960;
            this.graphics.PreferredBackBufferHeight = 600;

            //Définit si l'on voit la souris ou non. Nous pouvons la généré.
            //this.IsMouseVisible = true;
            //Mode plein écran, avec la résolution choisi plus haut.
            //graphics.IsFullScreen = true;
            //Appliquer les changements graphiques.
            graphics.ApplyChanges();

            blank = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
            //dst.Width = 540; //On peut changer manuellement les dimensions du sprite affichée





            // starting x and y locations to stack buttons
            // vertically in the middle of the screen
            int x = 10;
            int y = Window.ClientBounds.Height -
                NUMBER_OF_BUTTONS * BUTTON_HEIGHT;
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {
                button_state[i] = BState.UP;
                button_color[i] = Color.White;
                button_timer[i] = 0.0;
                button_rectangle[i] = new Rectangle(x, y, BUTTON_WIDTH, BUTTON_HEIGHT);
                y += BUTTON_HEIGHT;
            }
            //IsMouseVisible = true;
            background_color = Color.CornflowerBlue;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>(@"SpriteFont1");
            sprite = Content.Load<Texture2D>(@"gfx/cactus");
            sprite2 = Content.Load<Texture2D>(@"gfx/mire");
            location = new Vector2(100, 100);
            speed = new Vector2(150, 150);
            textBox = new Rectangle(10, 10, 300, 300);
            parsedText = parseText("Bonjour et bienvenue dans le monde fascinant du petit cactus, ce programme ne fait que faire bouger un cactus avec WASD et QE pour le faire tourner. La souris joue aussi un petit role! <CLICK TO CONTINUE...>");
            delayInMilliseconds = 50;
            isDoneDrawing = false;
            button_texture[EASY_BUTTON_INDEX] =
               Content.Load<Texture2D>(@"gfx/easy");
            button_texture[MEDIUM_BUTTON_INDEX] =
                Content.Load<Texture2D>(@"gfx/medium");
            button_texture[HARD_BUTTON_INDEX] =
                Content.Load<Texture2D>(@"gfx/hard");
            button_texture[HANDGUN_BUTTON_INDEX] =
                Content.Load<Texture2D>(@"gfx/btnsound/handgun");
            button_texture[ZOMBIE1_BUTTON_INDEX] =
                Content.Load<Texture2D>(@"gfx/btnsound/zombie1");
            button_texture[ZOMBIE2_BUTTON_INDEX] =
                Content.Load<Texture2D>(@"gfx/btnsound/zombie2");
            button_texture[VOLUMEPLUS_BUTTON_INDEX] =
                Content.Load<Texture2D>(@"gfx/btnsound/volume+");
            button_texture[VOLUMEMINUS_BUTTON_INDEX] =
                Content.Load<Texture2D>(@"gfx/btnsound/volume-");
            button_texture[PITCHPLUS_BUTTON_INDEX] =
                Content.Load<Texture2D>(@"gfx/btnsound/pitch+");
            button_texture[PITCHMINUS_BUTTON_INDEX] =
                Content.Load<Texture2D>(@"gfx/btnsound/pitch-");

            sb = new SFXBank(new SoundEffect[] {
                Content.Load<SoundEffect>(@"sfx/pistolshoot"), 
                Content.Load<SoundEffect>(@"sfx/zombie1"), 
                Content.Load<SoundEffect>(@"sfx/zombie2")});

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        KeyboardState oldState = Keyboard.GetState();
        protected override void Update(GameTime gameTime)
        {

            if (!isDoneDrawing)
            {
                if (delayInMilliseconds == 0)
                {
                    typedText = parsedText;
                    isDoneDrawing = true;
                }
                else if (typedTextLength < parsedText.Length)
                {
                    typedTextLength = typedTextLength + gameTime.ElapsedGameTime.TotalMilliseconds / delayInMilliseconds;

                    if (typedTextLength >= parsedText.Length)
                    {
                        typedTextLength = parsedText.Length;
                        isDoneDrawing = true;
                    }

                    typedText = parsedText.Substring(0, (int)typedTextLength);
                }
            }















            KeyboardState newState = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || newState.IsKeyDown(Keys.Escape))
                this.Exit();
            
            if (newState.IsKeyDown(Keys.D))
            {
                orientation = SpriteEffects.FlipHorizontally;
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                int deplacementX = (int)(speed.X * elapsedTime);
                location.X += deplacementX;
                dst.X = (int)location.X;
            }
            else if (newState.IsKeyUp(Keys.D) && oldState.IsKeyDown(Keys.D))
            {
                orientation = SpriteEffects.FlipHorizontally;
            }
            if (newState.IsKeyDown(Keys.A))
            {
                orientation = SpriteEffects.None;
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                int deplacementX = (int)(speed.X * elapsedTime);
                location.X -= deplacementX;
                dst.X = (int)location.X;
            }
            else if (newState.IsKeyUp(Keys.A) && oldState.IsKeyDown(Keys.A))
            {
                orientation = SpriteEffects.None;
            }
            if (newState.IsKeyDown(Keys.W))
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                int deplacementY = (int)(speed.Y * elapsedTime);
                location.Y -= deplacementY;
                dst.Y = (int)location.Y;
            }
            else if (newState.IsKeyUp(Keys.W) && oldState.IsKeyDown(Keys.W))
            {
            }
            if (newState.IsKeyDown(Keys.S))
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                int deplacementY = (int)(speed.Y * elapsedTime);
                location.Y += deplacementY;
                dst.Y = (int)location.Y;
            }
            else if (newState.IsKeyUp(Keys.S) && oldState.IsKeyDown(Keys.S))
            {
            }
            if (newState.IsKeyDown(Keys.Q))
            {
                rotation -= 0.01f;
            }
            if (newState.IsKeyDown(Keys.E))
            {
                rotation += 0.01f;
            }
            curMousePos.X = ms.X;
            curMousePos.Y = ms.Y;
            dst2.X = (int)(ms.X - (dst2.Width/2));
            dst2.Y = (int)(ms.Y - (dst2.Height/2));
            if (ms.LeftButton == ButtonState.Pressed)
            {
                shoot = Color.Red;
                if (typedTextLength == parsedText.Length && isDoneDrawing)
                {
                    parsedText = parseText("VOUS AVEZ CLIQUER, N'EST-CE PAS MAGIQUE!?");
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                }
            }
            else
            {
                shoot = Color.Yellow;
            }
            if (ms.RightButton == ButtonState.Pressed)
            {
                shoot = Color.Red;
                if (typedTextLength == parsedText.Length && isDoneDrawing)
                {
                    parsedText = parseText("Vous avez cliquer droit, donc vous aimer les KIKWI!!!");
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                }
            }
            //dst.X = (int)curMousePos.X;
            //dst.Y = (int)curMousePos.Y;
            // get elapsed frame time in seconds
            frame_time = gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            // update mouse variables
            MouseState mouse_state = Mouse.GetState();
            mx = mouse_state.X;
            my = mouse_state.Y;
            prev_mpressed = mpressed;
            mpressed = mouse_state.LeftButton == ButtonState.Pressed;

            update_buttons();
            // TODO: Add your update logic here
            oldState = newState;
            base.Update(gameTime);
        }
        void DrawLine(SpriteBatch batch, Texture2D blank,
              float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);
            batch.Draw(blank, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(background_color);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Joueur 1", new Vector2(location.X + 15, location.Y - 20), Color.Red);
            spriteBatch.Draw(sprite, dst, src, Color.White, rotation, Vector2.Zero, orientation, 1.0f);
            spriteBatch.DrawString(font, typedText, new Vector2(textBox.X, textBox.Y), Color.White);
            this.DrawLine(this.spriteBatch, this.blank, 1, shoot, new Vector2(location.X + (sprite.Width / 2), location.Y + (sprite.Height / 2)), curMousePos);
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
                spriteBatch.Draw(button_texture[i], button_rectangle[i], button_color[i]);
            spriteBatch.Draw(sprite2, dst2, src2, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1.0f);
            
            //spriteBatch.DrawString(font, "Hello World !", new Vector2(), Color.Red);
            
            
            spriteBatch.End();
            // TODO: Add your drawing code here
            

            base.Draw(gameTime);
        }
        //Converti les caractère du Clavier en Texte XNA
        public string ConvertKeyToChar(Keys key, bool shift)
        {
            switch (key)
            {
                case Keys.Space: return " ";

                // Escape Sequences 
                case Keys.Enter: return "\n";                         // Create a new line 
                case Keys.Tab: return "\t";                           // Tab to the right 

                // D-Numerics (strip above the alphabet) 
                case Keys.D0: return shift ? ")" : "0";
                case Keys.D1: return shift ? "!" : "1";
                case Keys.D2: return shift ? "@" : "2";
                case Keys.D3: return shift ? "#" : "3";
                case Keys.D4: return shift ? "$" : "4";
                case Keys.D5: return shift ? "%" : "5";
                case Keys.D6: return shift ? "^" : "6";
                case Keys.D7: return shift ? "&" : "7";
                case Keys.D8: return shift ? "*" : "8";
                case Keys.D9: return shift ? "(" : "9";

                // Numpad 
                case Keys.NumPad0: return "0";
                case Keys.NumPad1: return "1";
                case Keys.NumPad2: return "2";
                case Keys.NumPad3: return "3";
                case Keys.NumPad4: return "4";
                case Keys.NumPad5: return "5";
                case Keys.NumPad6: return "6";
                case Keys.NumPad7: return "7";
                case Keys.NumPad8: return "8";
                case Keys.NumPad9: return "9";
                case Keys.Add: return "+";
                case Keys.Subtract: return "-";
                case Keys.Multiply: return "*";
                case Keys.Divide: return "/";
                case Keys.Decimal: return ".";

                // Alphabet 
                case Keys.A: return shift ? "A" : "a";
                case Keys.B: return shift ? "B" : "b";
                case Keys.C: return shift ? "C" : "c";
                case Keys.D: return shift ? "D" : "d";
                case Keys.E: return shift ? "E" : "e";
                case Keys.F: return shift ? "F" : "f";
                case Keys.G: return shift ? "G" : "g";
                case Keys.H: return shift ? "H" : "h";
                case Keys.I: return shift ? "I" : "i";
                case Keys.J: return shift ? "J" : "j";
                case Keys.K: return shift ? "K" : "k";
                case Keys.L: return shift ? "L" : "l";
                case Keys.M: return shift ? "M" : "m";
                case Keys.N: return shift ? "N" : "n";
                case Keys.O: return shift ? "O" : "o";
                case Keys.P: return shift ? "P" : "p";
                case Keys.Q: return shift ? "Q" : "q";
                case Keys.R: return shift ? "R" : "r";
                case Keys.S: return shift ? "S" : "s";
                case Keys.T: return shift ? "T" : "t";
                case Keys.U: return shift ? "U" : "u";
                case Keys.V: return shift ? "V" : "v";
                case Keys.W: return shift ? "W" : "w";
                case Keys.X: return shift ? "X" : "x";
                case Keys.Y: return shift ? "Y" : "y";
                case Keys.Z: return shift ? "Z" : "z";

                // Oem 
                case Keys.OemOpenBrackets: return shift ? "{" : "[";
                case Keys.OemCloseBrackets: return shift ? "}" : "]";
                case Keys.OemComma: return shift ? "<" : ",";
                case Keys.OemPeriod: return shift ? ">" : ".";
                case Keys.OemMinus: return shift ? "_" : "-";
                case Keys.OemPlus: return shift ? "+" : "=";
                case Keys.OemQuestion: return shift ? "?" : "/";
                case Keys.OemSemicolon: return shift ? ":" : ";";
                case Keys.OemQuotes: return shift ? "\"" : "'";
                case Keys.OemPipe: return shift ? "|" : "\\";
                case Keys.OemTilde: return shift ? "~" : "`";
            }

            return string.Empty;
        }
        //Permet de wrapper le texte dans une zone défini
        private String parseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (font.MeasureString(line + word).Length() > textBox.Width)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
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
        void update_buttons()
        {
            for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
            {

                if (hit_image_alpha(
                    button_rectangle[i], button_texture[i], mx, my))
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

                if (button_state[i] == BState.JUST_RELEASED)
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
                case EASY_BUTTON_INDEX:
                    background_color = Color.Green;
                    parsedText = parseText("Mode de jeu Actif: Easy");
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                case MEDIUM_BUTTON_INDEX:
                    background_color = Color.Yellow;
                    parsedText = parseText("Mode de jeu Actif: Medium");
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                case HARD_BUTTON_INDEX:
                    background_color = Color.Red;
                    parsedText = parseText("Mode de jeu Actif: Hard");
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                case HANDGUN_BUTTON_INDEX:
                    sb.play(0);
                    parsedText = parseText("POW!");
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                case ZOMBIE1_BUTTON_INDEX:
                    sb.play(1);
                    parsedText = parseText("OMG! ZOMBIE!");
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                case ZOMBIE2_BUTTON_INDEX:
                    sb.play(2);
                    parsedText = parseText("OMG! ZOMBIE!");
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                case VOLUMEPLUS_BUTTON_INDEX:
                    sb.setVolume(sb.getVolume() + (float)0.1);
                    parsedText = parseText("Volume: " + sb.getVolume());
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                case VOLUMEMINUS_BUTTON_INDEX:
                    sb.setVolume(sb.getVolume() - (float)0.1);
                    parsedText = parseText("Volume: " + sb.getVolume());
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                case PITCHPLUS_BUTTON_INDEX:
                    sb.setPitch(sb.getPitch() + (float)0.1);
                    parsedText = parseText("Pitch: " + sb.getPitch());
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                case PITCHMINUS_BUTTON_INDEX:
                    sb.setPitch(sb.getPitch() - (float)0.1);
                    parsedText = parseText("Pitch: " + sb.getPitch());
                    typedText = "";
                    isDoneDrawing = false;
                    typedTextLength = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
