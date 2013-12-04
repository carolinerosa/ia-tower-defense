using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class Sun : GameObject
    {
        private float timer;
        private float timeOfLife = 5;

        public Sun(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model) : base(game, scale, rotation, position, model)
        {

        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (timer > timeOfLife)
            {
                Destroy();
            }

            base.Update(gameTime);
        }
    }
}
