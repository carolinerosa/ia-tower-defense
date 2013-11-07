using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TowerDefenseIA
{
    public class ChooseTowersInterface : DrawableGameComponent
    {
        Rectangle interfaceRectangle;
        int windowWidth;
        int windowHeight;
        SpriteBatch spriteBatch;
        Texture2D texture;
        Tower currentTower;

        public int numberOfTowers = 5;
        Rectangle[] towersPhotoRectangle;
        Texture2D[] towerTextures;
        Model archerModel, jesterModel, knightModel, mageModel, chosenOneModel;

        public ChooseTowersInterface(Game game, SpriteBatch spriteBatch, Texture2D texture) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;

            Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();

            windowWidth = Game.Window.ClientBounds.Width;
            windowHeight = Game.Window.ClientBounds.Height;

            int rectWidth = windowWidth / 10;
            interfaceRectangle = new Rectangle(windowWidth - rectWidth, 0, rectWidth, windowHeight);

            InstantiateTowerPhotos();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            
            towerTextures = new Texture2D[numberOfTowers];
            towerTextures[0] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\ArcherTexture");
            towerTextures[1] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\JesterTexture");
            towerTextures[2] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\KnightTexture");
            towerTextures[3] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\MageTexture");
            towerTextures[4] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\ChosenOneTexture");

            //archerModel = Game.Content.Load<Model>(@"Models\ArcherModel");
            //jesterModel = Game.Content.Load<Model>(@"Models\JesterModel");
            //knightModel = Game.Content.Load<Model>(@"Models\KnightModel");
            mageModel = Game.Content.Load<Model>(@"Models\MageModel");
            //chosenOneModel = Game.Content.Load<Model>(@"Models\ChosenOneModel");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            spriteBatch.Draw(texture, interfaceRectangle, Color.White);

            for (int i = 0; i < numberOfTowers; i++)
            {
                spriteBatch.Draw(towerTextures[i], towersPhotoRectangle[i], Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void InstantiateTowerPhotos()
        {
            towersPhotoRectangle = new Rectangle[numberOfTowers];

            int rectWidth = windowWidth / 6;
            int rectHeight = windowHeight / numberOfTowers;

            for (int i = 0; i < numberOfTowers; i++)
            {
                towersPhotoRectangle[i] = new Rectangle(windowWidth - rectWidth, windowHeight - (rectHeight * (i + 1)), rectWidth, rectHeight);
            }
        }

        public Tower InstantiateTower(int i)
        {
            switch (i)
            {
                case 0:
                    currentTower = new Tower(Game, Vector3.One, Vector3.Zero, Vector3.Zero, archerModel);
                    break;
                case 1:
                    currentTower = new Tower(Game, Vector3.One, Vector3.Zero, Vector3.Zero, jesterModel);
                    break;
                case 2:
                    currentTower = new Tower(Game, Vector3.One, Vector3.Zero, Vector3.Zero, knightModel);
                    break;
                case 3:
                    currentTower = new Tower(Game, new Vector3(0.1f, 0.1f, 0.1f), new Vector3(-90, 0, 0), Vector3.Zero, mageModel);
                    break;
                case 4:
                    currentTower = new Tower(Game, Vector3.One, Vector3.Zero, Vector3.Zero, chosenOneModel);
                    break;
            }

            return currentTower;
        }

        public Rectangle TowerRectangle(int i)
        {
            return towersPhotoRectangle[i];
        }
    }
}