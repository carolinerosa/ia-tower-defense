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
        Tower currentTower;

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
            chooseTowersInterface.DrawOrder = 50;
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
                Console.WriteLine(GraphicsDevice.Viewport.Unproject(new Vector3(Input.MousePosition, 1), Camera.Projection, Camera.View, Matrix.Identity));

                if (currentTower != null)
                {
                    GamePlane plane = grid.CheckPlaneClicked(currentTower.GetPosition());

                    if (plane != null && plane.IsPath && plane.IsOpen)
                    {
                        currentTower.Fix(plane.GetPosition());
                        plane.IsOpen = false;
                    }
                }

                CheckInterfaceClick(Input.MousePosition);
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
                    currentTower = chooseTowersInterface.InstantiateTower(i);
                }
            }
        }
    }
}