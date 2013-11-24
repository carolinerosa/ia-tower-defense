using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class Projectile : GameObject
    {
        private GameObject target;
        private int row;
        private int speed = 5;

        public Projectile(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model, int row) : base(game, scale, rotation, position, model)
        {
            this.row = row;
        }

        public override void Update(GameTime gameTime)
        {
            CheckTarget();

            if (target != null)
            {
                CheckCollision();
            }

            position.X += speed * (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
            world = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(rotation.Z, rotation.X, rotation.Y) * Matrix.CreateTranslation(position);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix[] transforms = new Matrix[this.model.Bones.Count];
            this.model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = Camera.View;
                    effect.Projection = Camera.Projection;
                }

                mesh.Draw();
            }

            base.Draw(gameTime);
        }

        private void CheckTarget()
        {
            target = SpawnManager.enemyQueues[row].Peek();
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
                        Console.WriteLine("Colidi");
                        return;
                    }
                }
            }
        }
    }
}
