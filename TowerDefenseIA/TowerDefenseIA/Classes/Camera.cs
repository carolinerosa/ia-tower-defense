using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseIA
{
    public class Camera : GameComponent
    {
        Vector3 position, rotation, targetPosition;
        Vector3 currentUpVector;
        Matrix rotationMatrix;

        static Matrix viewMatrix;
        static Matrix projectionMatrix;
        float fieldOfView = 45;
        float near = 1, far = 1000;

        public Camera(Game Game, Vector3 position, Vector3 rotation) : base(Game)
        {
            this.position = position;
            this.rotation = rotation;

            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            rotationMatrix = Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);
            currentUpVector = Vector3.Transform(Vector3.Up, rotationMatrix);

            viewMatrix = Matrix.CreateLookAt(position, targetPosition, currentUpVector);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(fieldOfView), Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, near, far);
        }

        public override void Update(GameTime GameTime)
        {
            if (Input.GetKey(Keys.Right))
            {
                position.X -= 1;
            }

            if (Input.GetKey(Keys.Left))
            {
                position.X += 1;
            }

            if (Input.GetKey(Keys.Down))
            {
                position.Z -= 1;
            }

            if (Input.GetKey(Keys.Up))
            {
                position.Z += 1;
            }
            
            Vector3 cameraRotatedTarget = Vector3.Transform(Vector3.Zero, rotationMatrix);
            targetPosition = new Vector3(position.X + cameraRotatedTarget.X, 0, position.Z + cameraRotatedTarget.Z);

            viewMatrix = Matrix.CreateLookAt(position, targetPosition, currentUpVector);

            base.Update(GameTime);
        }

        public static Matrix Projection
        {
            get { return projectionMatrix; }
        }

        public static Matrix View
        {
            get { return viewMatrix; }
        }
    }
}
