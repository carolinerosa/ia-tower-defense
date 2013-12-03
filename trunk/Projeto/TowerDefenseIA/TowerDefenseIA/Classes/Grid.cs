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
        private int xScale = 10;
        private int yScale = 16;

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
            planeScale = new Vector3(xScale, 0, yScale);

            for (int x = 0; x < rowNumber; x++)
            {
                for (int y = 0; y < columnNumber; y++)
                {
                    float positionX = this.position.X + y * xScale;
                    float positionZ = this.position.Z + x * yScale;

                    if (y == 0)
                    {
                        planes[x, y] = new GamePlane(Game, planeScale, Vector3.Zero, new Vector3(positionX, 0, positionZ), nonPath, false, x, y);
                    }
                    else
                    {
                        planes[x, y] = new GamePlane(Game, planeScale, Vector3.Zero, new Vector3(positionX, 0, positionZ), path, true, x, y);
                    }
                }
            }
        }

        public GamePlane CheckPlaneClicked(Vector3 mousePositionInWorld)
        {
            for (int x = 0; x < rowNumber; x++)
            {
                for (int y = 0; y < columnNumber; y++)
                {
                    if (mousePositionInWorld.X >= planes[x, y].GetPosition().X - (xScale / 2) &&
                    mousePositionInWorld.X <= planes[x, y].GetPosition().X + (xScale / 2) &&
                    mousePositionInWorld.Z >= planes[x, y].GetPosition().Z - (yScale / 2) &&
                    mousePositionInWorld.Z <= planes[x, y].GetPosition().Z + (yScale / 2))
                    {
                        return planes[x, y];
                    }
                }           
            }

            return null;
        }

        public GamePlane GetPlane(int x, int y)
        {
            return planes[x, y];
        }
    }
}