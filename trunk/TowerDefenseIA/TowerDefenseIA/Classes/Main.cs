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

        Texture2D pathTexture, nonPathTexture, map;
        Texture2D chooseTowersTexture;
        SpriteBatch spriteBatch;

        public Main(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            base.Initialize();

            cam = new Camera(Game, new Vector3(0, 50, 0), new Vector3(90, 0, 0));
            grid = new Grid(Game, new Vector3(0, 0, 0), 10, 14, pathTexture, nonPathTexture, map);
            chooseTowersInterface = new ChooseTowersInterface(Game, spriteBatch, chooseTowersTexture);
            chooseTowersInterface.DrawOrder = 143;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            
            pathTexture = Game.Content.Load<Texture2D>(@"Textures\path");
            nonPathTexture = Game.Content.Load<Texture2D>(@"Textures\nonPath");
            chooseTowersTexture = Game.Content.Load<Texture2D>(@"Textures\chooseTowers");

            map = Game.Content.Load<Texture2D>("map1");
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.LeftMouseButtonDown())
            {
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
                    chooseTowersInterface.InstantiateTower(i);
                }
            }
        }
    }
}