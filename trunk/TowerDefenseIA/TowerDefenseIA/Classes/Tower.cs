﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class Tower : GameObject
    {
        private bool isFixed = false;
        protected int price;

        public Tower(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model) : base(game, scale, rotation, position, model)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (isFixed == false)
            {
                Vector3 mousePoint = GraphicsDevice.Viewport.Unproject(new Vector3(Input.MousePosition, 1f), Camera.Projection, Camera.View, Matrix.Identity);
                position = mousePoint;
            }

            world = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z) * Matrix.CreateTranslation(position);
            
            base.Update(gameTime);
        }

        public void Fix(Vector3 position)
        {
            isFixed = true;
            this.position = position;
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
    }
}