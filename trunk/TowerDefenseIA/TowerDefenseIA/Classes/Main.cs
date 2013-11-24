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
        Camera cam;
        Grid grid;
        ChooseTowersInterface chooseTowersInterface;
        Tower currentTower;

        Texture2D pathTexture, nonPathTexture;
        Texture2D chooseTowersTexture;
        SpriteBatch spriteBatch;

        SpawnManager spawnManager;

        bool changeCamera = false;

        public Main(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            base.Initialize();
            cam = new StabilizedCamera(Game, new Vector3(45, 50, 25.5f), Vector3.Right * 90, 1, 50, 87);
            grid = new Grid(Game, Vector3.Zero, 5, 10, pathTexture, nonPathTexture);
            chooseTowersInterface = new ChooseTowersInterface(Game, spriteBatch, chooseTowersTexture);

            spawnManager = new SpawnManager(Game, grid);
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
            Input.Update();

            chooseTowersInterface.DrawOrder = Game.Components.Count;

            if (Input.GetKeyDown(Keys.Escape))
            {
                Game.Exit();
            }

            if (Input.LeftMouseButtonDown())
            {
                Console.WriteLine(Input.MousePositionInWorld);

                if (currentTower != null)
                {
                    GamePlane plane = grid.CheckPlaneClicked(currentTower.GetPosition());

                    if (plane != null && plane.IsPath && plane.IsOpen)
                    {
                        currentTower.Fix(plane.GetPosition());
                        currentTower.row = plane.Row; 
                        plane.IsOpen = false;
                        currentTower = null;
                    }
                }
                else
                {
                    CheckInterfaceClick(Input.MousePosition);
                }
            }

            if (Input.GetKeyDown(Keys.Delete))
            {
                changeCamera = !changeCamera;

                if (changeCamera)
                {
                    Game.Components.Remove(cam);
                    cam = new CameraFPS(Game);
                }
                else
                {
                    Game.Components.Remove(cam);
                    cam = new StabilizedCamera(Game, new Vector3(45, 50, 25.5f), Vector3.Right * 90, 1, 50, 87);
                }
            }

            base.Update(gameTime);

            Input.LateUpdate();
        }

        private void CheckInterfaceClick(Vector2 mousePosition)
        {
            for (int i = 0; i < chooseTowersInterface.numberOfElements; i++)
            {
                if (mousePosition.X >= chooseTowersInterface.ElementRectangle(i).X && 
                    mousePosition.X <= chooseTowersInterface.ElementRectangle(i).X + chooseTowersInterface.ElementRectangle(i).Width && 
                    mousePosition.Y >= chooseTowersInterface.ElementRectangle(i).Y && 
                    mousePosition.Y <= chooseTowersInterface.ElementRectangle(i).Y + chooseTowersInterface.ElementRectangle(i).Height)
                {
                    currentTower = chooseTowersInterface.InstantiateTower(i);
                }
            }
        }
    }
}