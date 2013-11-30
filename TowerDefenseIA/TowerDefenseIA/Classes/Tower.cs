using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class Tower : GameObject
    {
        protected bool isFixed = false;
        protected int price;
        public int row;

        public Tower(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model) : base(game, scale, rotation, position, model)
        {

        }

        public override void Initialize()
        {
            position = Input.MousePositionInWorld;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (isFixed == false)
            {
                position = Input.MousePositionInWorld;
                
            }

            world = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(rotation.Y), MathHelper.ToRadians(rotation.X), MathHelper.ToRadians(rotation.Z)) * Matrix.CreateTranslation(position);
            
            base.Update(gameTime);
        }

        public void Fix(Vector3 position)
        {
            isFixed = true;
            this.position = position;
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
    }
}