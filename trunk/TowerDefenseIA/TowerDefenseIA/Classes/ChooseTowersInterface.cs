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

        int numberOfTowers = 5;
        Rectangle[] towersPhotoRectangle;
        Texture2D[] towerTextures;

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

            int rectWidth = windowWidth / 5;
            interfaceRectangle = new Rectangle(windowWidth - rectWidth, 0, rectWidth, windowHeight);

            InstantiateTowerPhotos();
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

        protected override void LoadContent()
        {
            base.LoadContent();
            
            towerTextures = new Texture2D[numberOfTowers];
            towerTextures[0] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\ArcherTexture");
            towerTextures[1] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\JesterTexture");
            towerTextures[2] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\KnightTexture");
            towerTextures[3] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\MageTexture");
            towerTextures[4] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\ChosenOneTexture");          
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
    }
}