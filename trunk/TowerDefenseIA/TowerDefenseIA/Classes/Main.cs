using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class Main : DrawableGameComponent
    {
        Camera cam;
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
            Grid grid = new Grid(Game, new Vector3(0, 0, 0), 10, 14, pathTexture, nonPathTexture, map);
            ChooseTowersInterface chooseTowers = new ChooseTowersInterface(Game, spriteBatch, chooseTowersTexture);
            chooseTowers.DrawOrder = 143;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            
            pathTexture = Game.Content.Load<Texture2D>(@"Textures\path");
            nonPathTexture = Game.Content.Load<Texture2D>(@"Textures\nonPath");
            chooseTowersTexture = Game.Content.Load<Texture2D>(@"Textures\chooseTowers");

            map = Game.Content.Load<Texture2D>("map1");
        }
    }
}