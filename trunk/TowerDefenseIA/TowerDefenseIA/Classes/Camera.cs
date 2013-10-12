using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerDefenseIA
{
    public class Camera : GameComponent
    {
        Game game;
        Vector3 position, rotation, targetPosition;
        Vector3 upVector;

        static Matrix viewMatrix;
        static Matrix projectionMatrix;
        float fieldOfView = 45;
        float near = 1, far = 1000;

        public Camera(Game game, Vector3 position, Vector3 rotation, Vector3 targetPosition, Vector3 upVector) : base(game)
        {
            this.game = game;
            this.position = position;
            this.rotation = rotation;
            this.targetPosition = targetPosition;
            this.upVector = upVector;

            game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            viewMatrix = Matrix.CreateLookAt(position, targetPosition, upVector);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(fieldOfView), game.Window.ClientBounds.Width / (float)game.Window.ClientBounds.Height, near, far);
        }

        public override void Update(GameTime gameTime)
        {
            viewMatrix = Matrix.CreateLookAt(position, targetPosition, upVector);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(fieldOfView), game.Window.ClientBounds.Width / (float)game.Window.ClientBounds.Height, near, far);

            base.Update(gameTime);
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
