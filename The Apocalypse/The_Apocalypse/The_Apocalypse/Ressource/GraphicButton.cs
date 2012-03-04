using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace The_Apocalypse
{
    class GraphicButton
    {
        enum BState
        {
            HOVER,
            UP,
            JUST_RELEASED,
            DOWN
        }

        string text = "";
        int x, y, width, height;

        Color button_color;
        Rectangle button_rectangle;
        BState button_state;
        bool button_oneClick = false;
        Texture2D button_texture;
        double button_timer;
        bool inputBool = false;
        bool mpressed, prev_mpressed = false;
        double frame_time;


        /// <summary>
        /// Create the object GraphicButton
        /// </summary>
        /// <param name="x">The X position at the screen.</param>
        /// <param name="y">The Y position at the screen.</param>
        /// <param name="width">The width of the button.</param>
        /// <param name="height">The height of the button.</param>
        /// 
        public GraphicButton(int x, int y, int width, int height)
        {
            UpdateData(x, y, width, height);
        }
        /// <summary>
        /// Create the object GraphicButton
        /// </summary>
        /// <param name="x">The X position at the screen.</param>
        /// <param name="y">The Y position at the screen.</param>
        /// <param name="width">The width of the button.</param>
        /// <param name="height">The height of the button.</param>
        /// <param name="text">Set the text of the button.</param>
        /// 
        public GraphicButton(int x, int y,int width, int height,string text)
        {
            this.text = text;
            UpdateData(x, y, width, height);
        }
        public void LoadContent(Texture2D texture)
        {
            button_texture = texture;
        }

        public void UpdateData(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            button_state = BState.UP;
            button_color = Color.White;
            button_timer = 0.0;
            button_rectangle = new Rectangle(this.x, this.y, this.width, this.height);
        }

        /*public BState state
        {
            get { return button_state; }
            set { button_state = value; }
        }*/

        public Color color 
        {
            get{return button_color;}
            set { button_color = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public double timer
        {
            get { return button_timer; }
            set { button_timer = value; }
        }

        public bool oneClick
        {
            get { return button_oneClick; }
            set { button_oneClick = value; }
        }

        public Rectangle rectangle
        {
            get { return button_rectangle; }
            set { button_rectangle = value; }
        }

        public bool input
        {
            get { return inputBool; }
            set { inputBool = value; }
        }
        public Texture2D texture
        {
            get { return button_texture; }
            set { button_texture = value; }
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
        public bool update_buttons(GameTime gameTime)
        {
            if (inputBool)
            {
                inputKeyboard();
            }
            MouseState mouse_state = Mouse.GetState();
            prev_mpressed = mpressed;
            mpressed = mouse_state.LeftButton == ButtonState.Pressed;
            frame_time = gameTime.ElapsedGameTime.Milliseconds / 1000.0;

            if (hit_image_alpha(
                button_rectangle, button_texture, mouse_state.X, mouse_state.Y))
            {
                button_timer = 0.0;
                if (mpressed)
                {
                    // mouse is currently down
                    button_state = BState.DOWN;
                    button_color = Color.Red;
                }
                else if (!mpressed && prev_mpressed)
                {
                    // mouse was just released
                    if (button_state == BState.DOWN)
                    {
                        // button i was just down
                        button_state = BState.JUST_RELEASED;
                    }
                }
                else
                {
                    button_state = BState.HOVER;
                    button_color = Color.LightBlue;
                }
            }
            else
            {
                button_state = BState.UP;
                if (button_timer > 0)
                {
                    button_timer = button_timer - frame_time;
                }
                else
                {
                    button_color = Color.White;
                }
            }
            if ((!button_oneClick && button_state == BState.DOWN) || (button_oneClick && button_state == BState.JUST_RELEASED))
            {
                return true;
            }
            return false;
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

            if (text.Length >= 15 && key != Keys.Back)
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
                    if (text.Length != 0)
                        text = text.Remove(text.Length - 1);
                    return;
            }
            if (currentKeyboardState.IsKeyDown(Keys.RightShift) ||
                currentKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                newChar = newChar.ToUpper();
            }
            text += newChar;
        }

        private bool CheckKey(Keys theKey)
        {
            return lastKeyboardState.IsKeyDown(theKey) && currentKeyboardState.IsKeyUp(theKey);
        }
    }
}
