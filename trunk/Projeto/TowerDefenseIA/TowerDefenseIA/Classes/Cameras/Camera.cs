using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class Camera : GameComponent
    {
        protected static Vector3 position;
        protected static Vector3 upVector;
        protected Vector3 targetPosition;

        protected static Matrix viewMatrix;
        protected static Matrix projectionMatrix;
        protected Matrix rotationMatrix;
        protected int near, far, fieldOfView = 45;

        public Camera(Game game) : base(game)
        {
            Camera.position = Vector3.Up * 3;
            this.near = 1;
            this.far = 1000;
            this.fieldOfView = 45;

            Game.Components.Add(this);
        }

        public Camera(Game game, Vector3 position) : base(game)
        {
            Camera.position = position;
            this.near = 1;
            this.far = 1000;
            this.fieldOfView = 45;

            Game.Components.Add(this);
        }

        public Camera(Game game, Vector3 position, int near, int far, int fieldOfView) : base(game)
        {
            Camera.position = position;
            this.near = near;
            this.far = far;
            this.fieldOfView = fieldOfView;

            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            viewMatrix = Matrix.CreateLookAt(position, targetPosition, upVector);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(fieldOfView), Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, near, far);
        }

        public override void Update(GameTime gameTime)
        {
            viewMatrix = Matrix.CreateLookAt(position, targetPosition, upVector);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(fieldOfView), Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height, near, far);

            base.Update(gameTime);
        }

        public static Matrix View
        {
            get { return viewMatrix; }
        }

        public static Matrix Projection
        {
            get { return projectionMatrix; }
        }

        public static Vector3 Position
        {
            get { return position; }
        }
    }
}