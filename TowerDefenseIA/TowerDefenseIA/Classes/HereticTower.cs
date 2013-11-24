using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class HereticTower : Tower
    {
        private float attackCooldown = 3;
        private float timer;
        private Model projectileModel;

        public HereticTower(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model) : base(game, scale, rotation, position, model)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (isFixed)
            {
                if (SpawnManager.enemyQueues[this.row].Count > 0)
                {
                    timer += (float)gameTime.ElapsedGameTime.Milliseconds / 1000;

                    if (timer > attackCooldown)
                    {
                        new Projectile(Game, Vector3.One, Vector3.Zero, position, projectileModel, this.row);
                        timer = 0;
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            projectileModel = Game.Content.Load<Model>(@"Models\coco_bazoca");
        }
    }
}
