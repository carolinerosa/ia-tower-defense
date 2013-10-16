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
        Vector3 position;
        int rowNumber, columnNumber;
        Texture2D path, nonPath, map;
        int scale = 5;

        string[,] map1 = {{"x","x","x","o","o","o","o","o","o","o","o","o","o","o"},
                          {"o","o","x","o","o","o","x","x","x","x","x","x","o","o"},
                          {"o","o","x","o","o","o","x","o","o","o","o","x","o","o"},
                          {"o","o","x","o","o","o","x","o","o","o","o","x","o","o"},
                          {"o","o","x","o","o","o","x","o","o","o","o","x","o","o"},
                          {"o","o","x","x","x","x","x","o","o","o","o","x","o","o"},
                          {"o","o","o","o","o","o","o","o","o","o","o","x","o","o"},
                          {"o","o","o","o","o","o","o","o","o","o","o","x","o","o"},
                          {"o","o","o","o","o","o","o","o","o","o","o","x","o","o"},
                          {"o","o","o","o","o","o","o","o","o","o","o","x","x","x"}};

        public Grid(Game game, Vector3 position, int rowNumber, int columnNumber, Texture2D path, Texture2D nonPath, Texture2D map) : base(game)
        {
            this.position = position;
            this.rowNumber = rowNumber;
            this.columnNumber = columnNumber;

            this.path = path;
            this.nonPath = nonPath;
            this.map = map;

            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            for (int x = 0; x < rowNumber; x++)
            {
                for (int y = 0; y < columnNumber; y++)
                {
                    float positionX = this.position.X + y * (scale * 2);
                    float positionZ = this.position.Z + x * (scale * 2);

                    if (map1[x, y] == "x")
                    {
                        Plane plane = new Plane(Game, new Vector3(scale, 0, scale), Vector3.Zero, new Vector3(positionX, 0, positionZ), path);
                    }
                    else
                    {
                        Plane plane = new Plane(Game, new Vector3(scale, 0, scale), Vector3.Zero, new Vector3(positionX, 0, positionZ), nonPath);
                    }
                }
            }

            /*Color[,] mapPixelsColor = GetMapPixels();

            for (int x = 0; x < columnNumber; x++)
            {
                for (int y = 0; y < rowNumber; y++)
                {
                    float positionX = this.position.X + x * (scale * 2);
                    float positionZ = this.position.Z + y * (scale * 2);

                    if (mapPixelsColor[x, y] == Color.Black)
                    {
                        Plane plane = new Plane(Game, new Vector3(scale, 0, scale), Vector3.Zero, new Vector3(positionX, 0, positionZ), path);
                    }
                    else
                    {
                        Plane plane = new Plane(Game, new Vector3(scale, 0, scale), Vector3.Zero, new Vector3(positionX, 0, positionZ), nonPath);
                    }
                }
            }*/
        }

        private Color[,] GetMapPixels()
        {
            Color[] map1D = new Color[map.Width * map.Height];
            Color[,] map2D = new Color[map.Width, map.Height];

            map.GetData<Color>(map1D);

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    map2D[x, y] = map1D[x + (y * map.Width)];
                }
            }

            return map2D;
        }
    }
}