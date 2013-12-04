using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class Enemy : GameObject
    {
        protected int speed = 5 ;
        private int row;

        public Enemy(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model, int row) : base(game, scale, rotation, position, model)
        {
            this.row = row;
            SpawnManager.enemyQueues[row].Enqueue(this);
        }

        public override void Update(GameTime gameTime)
        {
            position.X -= speed * (float)gameTime.ElapsedGameTime.Milliseconds/1000;
            world = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(rotation.Y), MathHelper.ToRadians(rotation.X), MathHelper.ToRadians(rotation.Z)) * Matrix.CreateTranslation(position);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            transforms = new Matrix[this.model.Bones.Count];
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

        public override void Destroy()
        {
            SpawnManager.enemyQueues[row].Dequeue();
            Game.Components.Remove(this);   
        }
    }
}