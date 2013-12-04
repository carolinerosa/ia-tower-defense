using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class SunSystem : DrawableGameComponent
    {
        private float interval = 10;
        private float timer;
        private Random rand;
        private Model sunModel;
        private Grid grid;

        public SunSystem(Game game, Grid grid) : base(game)
        {
            this.grid = grid;
            rand = new Random();
            game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (timer >= interval)
            {
                int positionX = rand.Next((int)grid.GetPlane(0,1).GetPosition().X, (int)grid.GetPlane(0,9).GetPosition().X);
                int positionZ = rand.Next((int)grid.GetPlane(0, 9).GetPosition().Z, (int)grid.GetPlane(4, 1).GetPosition().Z);

                new Sun(Game, Vector3.One * 0.1f, Vector3.Zero, new Vector3(positionX, 50, positionZ), sunModel);
                timer = 0;
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            sunModel = Game.Content.Load<Model>(@"Models\Altar");
        }
    }
}