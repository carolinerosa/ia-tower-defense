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

namespace TowerDefenseIA
{
    public class Input
    {
        private static bool pressed;
        private static KeyboardState _Keyboard { get; set; }
        private static KeyboardState _OldKeyboard { get; set; }
        private static MouseState _Mouse { get; set; }

        private static Game game;

        public Input(Game game)
        {
            Input.game = game;
        }

        public static void Update()
        {
            _Keyboard = Keyboard.GetState();
            _Mouse = Mouse.GetState();
        }

        public static bool GetKey(Keys k)
        {
            if(game.IsActive)
                return _Keyboard.IsKeyDown(k);

            return false;
        }

        public static bool GetKeyDown(Keys k)
        {
            if (game.IsActive)
            {
                return _Keyboard.IsKeyDown(k) &&
                       _OldKeyboard.IsKeyUp(k);
            }

            return false;
        }

        public static bool GetKeyUp(Keys k)
        {
            if (game.IsActive)
            {
                return _Keyboard.IsKeyUp(k) &&
                       _OldKeyboard.IsKeyDown(k);
            }

            return false;
        }

        public static bool LeftMouseButtonDown()
        {
            if (game.IsActive)
            {
                if (_Mouse.LeftButton == ButtonState.Pressed)
                {
                    if (pressed == false)
                    {
                        pressed = true;
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        public static bool RightMouseButtonDown()
        {
            if (game.IsActive)
            {
                if (_Mouse.RightButton == ButtonState.Pressed)
                {
                    if (pressed == false)
                    {
                        pressed = true;
                        return true;
                    }
                }
            }

            return false;
        }

        public static Vector2 MousePosition
        {
            get { return new Vector2(_Mouse.X, _Mouse.Y); }
        }

        public static Vector3 MousePositionInWorld
        {
            get { return game.GraphicsDevice.Viewport.Unproject(new Vector3(Input.MousePosition, 1f), Camera.Projection, Camera.View, Matrix.Identity); }
        }

        public static void LateUpdate()
        {
            _OldKeyboard = _Keyboard;

            if (game.IsActive)
            {
                if (_Mouse.LeftButton == ButtonState.Released)
                {
                    pressed = false;
                }
            }
        }
    }
}