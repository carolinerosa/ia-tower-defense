using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA.Classes
{
    public class Projectile : GameObject
    {
        private GameObject target;

        public Projectile(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model) : base(game, scale, rotation, position, model)
        {

        }

        public override void Update(GameTime gameTime)
        {
            CheckCollision();
            CheckTarget();

            base.Update(gameTime);
        }

        private void CheckTarget()
        {
            
        }

        private void CheckCollision()
        {
            for (int i = 0; i < this.model.Meshes.Count; i++)
            {
                // Check whether the bounding boxes of the two cubes intersect.
                BoundingSphere c1BoundingSphere = this.model.Meshes[i].BoundingSphere;
                c1BoundingSphere.Center += this.GetPosition();

                for (int j = 0; j < target.model.Meshes.Count; j++)
                {
                    BoundingSphere c2BoundingSphere = target.model.Meshes[j].BoundingSphere;
                    c2BoundingSphere.Center += target.GetPosition();

                    if (c1BoundingSphere.Intersects(c2BoundingSphere))
                    {
                        //Colidiu
                        return;
                    }
                }
            }
        }
    }
}
