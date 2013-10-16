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
        Model model;
        private bool isFixed = false;

        public Tower(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model) : base(game, scale, rotation, position)
        {
            this.model = model;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (isFixed == false)
            {
                Vector3 mousePosition = new Vector3(Input.MousePosition.X, Input.MousePosition.Y, 0);
                Vector3 pointInWorldSpace = GraphicsDevice.Viewport.Unproject(mousePosition, Camera.Projection, Camera.View, this.world);
                position = new Vector3(pointInWorldSpace.X, 0, pointInWorldSpace.Y);
                Console.WriteLine("position: " + position);
                Console.WriteLine("point: " + pointInWorldSpace);
            }

            world = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z) * Matrix.CreateTranslation(position);

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
    }
}