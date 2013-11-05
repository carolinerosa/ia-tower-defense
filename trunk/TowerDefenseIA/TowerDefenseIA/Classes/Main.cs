using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefenseIA
{
    public class Main : DrawableGameComponent
    {
        //CameraFPS cam;
        Camera cam;
        Grid grid;
        ChooseTowersInterface chooseTowersInterface;

        Texture2D pathTexture, nonPathTexture;
        Texture2D chooseTowersTexture;
        SpriteBatch spriteBatch;

        public Main(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            base.Initialize();
            //cam = new CameraFPS(Game);
            cam = new Camera(Game, Vector3.Up * 50, Vector3.Right * 90);
            grid = new Grid(Game, Vector3.Zero, 5, 10, pathTexture, nonPathTexture);
            chooseTowersInterface = new ChooseTowersInterface(Game, spriteBatch, chooseTowersTexture);
            chooseTowersInterface.DrawOrder = 143;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            
            pathTexture = Game.Content.Load<Texture2D>(@"Textures\path");
            nonPathTexture = Game.Content.Load<Texture2D>(@"Textures\nonPath");
            chooseTowersTexture = Game.Content.Load<Texture2D>(@"Textures\chooseTowers");
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.LeftMouseButtonDown())
            {
                CheckInterfaceClick(Input.MousePosition);

                Matrix world = Matrix.CreateTranslation(0, 0, 0);

                //Vector3 source = new Vector3((float) Input.MousePosition.X, 1f, (float) Input.MousePosition.Y);
                Vector3 mousePoint = GraphicsDevice.Viewport.Unproject(new Vector3(Input.MousePosition, 1f), Camera.Projection, Camera.View, world);

                System.Console.Out.WriteLine("x: " + mousePoint.X + ", Y: " + mousePoint.Y + ", Z: " + mousePoint.Z);
            }

            
            base.Update(gameTime);
        }


        private void CheckInterfaceClick(Vector2 mousePosition)
        {
            for (int i = 0; i < chooseTowersInterface.numberOfTowers; i++)
            {
                if (mousePosition.X >= chooseTowersInterface.TowerRectangle(i).X && 
                    mousePosition.X <= chooseTowersInterface.TowerRectangle(i).X + chooseTowersInterface.TowerRectangle(i).Width && 
                    mousePosition.Y >= chooseTowersInterface.TowerRectangle(i).Y && 
                    mousePosition.Y <= chooseTowersInterface.TowerRectangle(i).Y + chooseTowersInterface.TowerRectangle(i).Height)
                {
                    chooseTowersInterface.InstantiateTower(i);
                }
            }
        }
    }
}