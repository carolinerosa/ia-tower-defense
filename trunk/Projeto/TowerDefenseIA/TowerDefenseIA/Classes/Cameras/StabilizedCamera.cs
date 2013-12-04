using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseIA
{
    public class StabilizedCamera : Camera
    {
        private Vector3 rotation;

        public StabilizedCamera(Game Game, Vector3 position, Vector3 rotation) : base(Game, position)
        {
            this.rotation = rotation;
        }

        public StabilizedCamera(Game Game, Vector3 position, Vector3 rotation, int near, int far, int fieldOfView) : base(Game, position, near, far, fieldOfView)
        {
            this.rotation = rotation;
        }

        public override void Initialize()
        {
            base.Initialize();

            rotationMatrix = Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(rotation.Y), MathHelper.ToRadians(rotation.X), MathHelper.ToRadians(rotation.Z));
            upVector = Vector3.Transform(Vector3.Up, rotationMatrix);
        }

        public override void Update(GameTime gameTime)
        {
            Vector3 cameraRotatedTarget = Vector3.Transform(Vector3.Zero, rotationMatrix);
            targetPosition = new Vector3(position.X + cameraRotatedTarget.X, 0, position.Z + cameraRotatedTarget.Z);

            base.Update(gameTime);
        }
    }
}