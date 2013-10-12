using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace TowerDefenseIA
{
    public class CameraFPS : GameComponent
    {
        Game game;
        GraphicsDevice device;

        static Vector3 cameraPosition = new Vector3(0, 3, 0);
        float leftrightRot = MathHelper.PiOver2;
        float updownRot = -MathHelper.Pi / 10.0f;
        float near, far, fieldOfView = 45;
        const float rotationSpeed = 0.3f;
        const float moveSpeed = 30.0f;
        
        static Matrix viewMatrix;
        static Matrix projection;
        Matrix cameraRotation = Matrix.Identity;

        static Vector3 cameraRotatedUpVector;

        MouseState originalMouseState;

        public CameraFPS(Game game) : base(game)
        {
            this.game = game;
            this.near = 1;
            this.far = 1000;

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(fieldOfView), game.Window.ClientBounds.Width / (float)game.Window.ClientBounds.Height, near, far);
            device = game.GraphicsDevice;
           
            game.Components.Add(this);
        }

        public override void Initialize()
        {
            Mouse.SetPosition(device.Viewport.Width / 2, device.Viewport.Height / 2);
            originalMouseState = Mouse.GetState();

            UpdateViewMatrix();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            ProcessInput(timeDifference);
            
            base.Update(gameTime);
        }

        private void UpdateViewMatrix()
        {
            cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            
            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = cameraPosition + cameraRotatedTarget;

            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);
            cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);
            
            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraFinalTarget, cameraRotatedUpVector);
        }

        private void ProcessInput(float amount)
        {
            MouseState currentMouseState = Mouse.GetState();

            if (currentMouseState != originalMouseState)
            {
                float xDifference = currentMouseState.X - originalMouseState.X;
                float yDifference = currentMouseState.Y - originalMouseState.Y;
                leftrightRot -= rotationSpeed * xDifference * amount;
                updownRot -= rotationSpeed * yDifference * amount;
                Mouse.SetPosition(device.Viewport.Width / 2, device.Viewport.Height / 2);

                UpdateViewMatrix();
            }

            Vector3 moveVector = new Vector3(0, 0, 0);

            if (Input.GetKey(Keys.W))
            {
                moveVector += new Vector3(0, 0, -1);
            }

            if (Input.GetKey(Keys.S))
            {
                moveVector += new Vector3(0, 0, 1);
            }

            if (Input.GetKey(Keys.D))
            {
                moveVector += new Vector3(1, 0, 0);
            }

            if (Input.GetKey(Keys.A))
            {
                moveVector += new Vector3(-1, 0, 0);
            }

            if (Input.GetKey(Keys.Q))
            {
                moveVector += new Vector3(0, 1, 0);
            }

            if (Input.GetKey(Keys.Z))
            {
                moveVector += new Vector3(0, -1, 0);
            }

            AddToCameraPosition(moveVector * amount);
        }

        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            cameraPosition += moveSpeed * rotatedVector;

            UpdateViewMatrix();
        }

        public static Matrix View
        {
            get { return viewMatrix; }
        }

        public static Matrix Projection
        {
            get { return projection; }
        }

        public static Vector3 Postion
        {
            get { return cameraPosition; }
        }

        public static Vector3 UpVector
        {
            get { return cameraRotatedUpVector; }
        }

        public float Near
        {
            get { return near; }
            set { near = value; }
        }

        public float Far
        {
            get { return far; }
            set { far = value; }
        }

        public float FieldOfView
        {
            get { return fieldOfView; }
            set { fieldOfView = value; }
        }
    }
}