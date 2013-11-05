using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public class Grid : GameComponent
    {
        private Vector3 position;
        private int rowNumber, columnNumber;
        private Texture2D path, nonPath;
        private int scale = 5;

        private GamePlane[,] planes;
        private Vector3 planeScale;

        public Grid(Game game, Vector3 position, int rowNumber, int columnNumber, Texture2D path, Texture2D nonPath) : base(game)
        {
            this.position = position;
            this.rowNumber = rowNumber;
            this.columnNumber = columnNumber;

            this.path = path;
            this.nonPath = nonPath;

            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            planes = new GamePlane[rowNumber, columnNumber];
            planeScale = new Vector3(scale, 0, scale);

            for (int x = 0; x < rowNumber; x++)
            {
                for (int y = 0; y < columnNumber; y++)
                {
                    float positionX = this.position.X + y * (scale * 2);
                    float positionZ = this.position.Z + x * (scale * 2);

                    if (y == 0)
                    {
                        planes[x, y] = new GamePlane(Game, planeScale, Vector3.Zero, new Vector3(positionX, 0, positionZ), nonPath);
                    }
                    else
                    {
                        planes[x, y] = new GamePlane(Game, planeScale, Vector3.Zero, new Vector3(positionX, 0, positionZ), path);
                    }
                }
            }
        }
    }
}