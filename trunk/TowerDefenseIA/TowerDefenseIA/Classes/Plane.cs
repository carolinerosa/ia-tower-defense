using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class Plane : GameObject
    {
        VertexPositionTexture[] verts;
        VertexBuffer vertexBuffer;
        BasicEffect effect;

        public Plane(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Texture2D texture) : base(game, scale, rotation, position, texture)
        { }

        public override void Initialize()
        {
            base.Initialize();

            verts = new VertexPositionTexture[6];

            verts[0] = new VertexPositionTexture(new Vector3(-1, 0, 1), new Vector2(0, 1));
            verts[1] = new VertexPositionTexture(new Vector3(-1, 0, -1), new Vector2(0, 0));
            verts[2] = new VertexPositionTexture(new Vector3(1, 0, -1), new Vector2(1, 0));

            verts[3] = new VertexPositionTexture(new Vector3(1, 0, -1), new Vector2(1, 0));
            verts[4] = new VertexPositionTexture(new Vector3(1, 0, 1), new Vector2(1, 1));
            verts[5] = new VertexPositionTexture(new Vector3(-1, 0, 1), new Vector2(0, 1));

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionTexture>(verts);

            effect = new BasicEffect(GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            world = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z) * Matrix.CreateTranslation(position); 

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetVertexBuffer(vertexBuffer);
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;

            effect.World = world;
            effect.View = CameraFPS.View;
            effect.Projection = CameraFPS.Projection;
            effect.TextureEnabled = true;
            effect.Texture = texture;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, verts, 0, verts.Length / 3);
            }

            base.Draw(gameTime);
        }
    }
}
