using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class SpawnManager : DrawableGameComponent
    {
        Model model;
        Grid grid;
        float timer;
        float interval = 5;

        public static Queue<Enemy>[] enemyQueues = new Queue<Enemy>[5];

        public SpawnManager(Game game, Grid grid) : base(game)
        {
            this.grid = grid;

            for (int i = 0; i < enemyQueues.Length; i++)
            {
                enemyQueues[i] = new Queue<Enemy>();
            }

            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            
            Vector3 enemyPosition = new Vector3(grid.GetPlane(0, 9).GetPosition().X, 1, grid.GetPlane(0, 9).GetPosition().Z);

            new Enemy(Game, Vector3.One, new Vector3(-90, 0, 90), enemyPosition, model, 0);
        }

        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

            if (timer >= interval)
            {
                Vector3 enemyPosition = new Vector3(grid.GetPlane(0, 9).GetPosition().X, 1, grid.GetPlane(0, 9).GetPosition().Z);
                new Enemy(Game, Vector3.One, new Vector3(-90, 0, 90), enemyPosition, model, 0);

                timer = 0;
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            model = Game.Content.Load<Model>(@"Models\coco_bazoca");
        }
    }
}