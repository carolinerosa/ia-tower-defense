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
        private static KeyboardState _Keyboard { get; set; }
        private static KeyboardState _OldKeyboard { get; set; }

        public static void Update()
        {
            _Keyboard = Keyboard.GetState();
        }

        public static bool GetKey(Keys k)
        {
            return _Keyboard.IsKeyDown(k);
        }

        public static bool GetKeyDown(Keys k)
        {
            return _Keyboard.IsKeyDown(k) &&
                   _OldKeyboard.IsKeyUp(k);
        }

        public static bool GetKeyUp(Keys k)
        {
            return _Keyboard.IsKeyUp(k) &&
                   _OldKeyboard.IsKeyDown(k);
        }

        public static void LateUpdate()
        {
            _OldKeyboard = _Keyboard;
        }
    }
}