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

        public Main(Game game) : base(game)
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();

            //cam = new Camera(Game, new Vector3(0, 50, 10), new Vector3(90, 0, 0), Vector3.Zero, Vector3.Up);
            CameraFPS cam = new CameraFPS(Game);

            //Plane plane = new Plane(Game, Vector3.One * 3, Vector3.Zero, Vector3.Zero, pathTexture);
            Grid grid = new Grid(Game, new Vector3(-70, 0, -50), 14, 10, pathTexture, nonPathTexture, map);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            pathTexture = Game.Content.Load<Texture2D>(@"Textures\path");
            nonPathTexture = Game.Content.Load<Texture2D>(@"Textures\nonPath");
            map = Game.Content.Load<Texture2D>("map1");
        }
    }
}
