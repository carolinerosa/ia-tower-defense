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
    public class CameraFPS : Camera
    {
        float leftrightRot = MathHelper.PiOver2;
        float updownRot = -MathHelper.Pi / 10.0f;
        const float rotationSpeed = 0.3f;
        const float moveSpeed = 30.0f;

        MouseState originalMouseState;

        public CameraFPS(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            Mouse.SetPosition(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
            originalMouseState = Mouse.GetState();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            ProcessInput(timeDifference);
            
            base.Update(gameTime);
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
                Mouse.SetPosition(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);

                UpdateViewMatrix();
            }

            Vector3 moveVector = Vector3.Zero;

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

        private void UpdateViewMatrix()
        {
            rotationMatrix = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);

            Vector3 cameraRotatedTarget = Vector3.Transform(Vector3.Forward, rotationMatrix);
            targetPosition = position + cameraRotatedTarget;

            upVector = Vector3.Transform(Vector3.Up, rotationMatrix);
        }

        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            rotationMatrix = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, rotationMatrix);
            position += moveSpeed * rotatedVector;

            UpdateViewMatrix();
        }
    }
}