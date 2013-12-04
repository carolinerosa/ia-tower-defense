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
        SpriteFont interfaceFont;

        int rectWidth;
        int rectHeight;

        public int numberOfElements = 8;
        Rectangle[] interfaceElementsRectangle;
        Texture2D[] elementTextures;
        Model altarModel, apprenticeModel, sentinelModel, hereticModel, trapModel;
        Tower currentTower;

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

            rectWidth = windowWidth / 8;
            rectHeight = windowHeight / 7;
            interfaceRectangle = new Rectangle(0, 0, windowWidth, rectHeight);

            InstantiateTowerPhotos();
        }

        private void InstantiateTowerPhotos()
        {
            interfaceElementsRectangle = new Rectangle[numberOfElements];

            for (int i = 0; i < numberOfElements; i++)
            {
                interfaceElementsRectangle[i] = new Rectangle(rectWidth * i, 0, rectWidth, rectHeight);
            }

            interfaceElementsRectangle[0] = new Rectangle(0, 0, rectWidth, rectHeight / 2);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            elementTextures = new Texture2D[numberOfElements];
            elementTextures[0] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\None");
            elementTextures[1] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\Altar");
            elementTextures[2] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\Apprentice");
            elementTextures[3] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\Sentinel");
            elementTextures[4] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\Heretic");
            elementTextures[5] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\Trap");
            elementTextures[6] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\None");
            elementTextures[7] = Game.Content.Load<Texture2D>(@"Textures\TowerPhotos\None");

            altarModel = Game.Content.Load<Model>(@"Models\Altar");
            //apprenticeModel = Game.Content.Load<Model>(@"Models\JesterModel");
            //sentinelModel = Game.Content.Load<Model>(@"Models\KnightModel");
            hereticModel = Game.Content.Load<Model>(@"Models\MageModel");
            trapModel = Game.Content.Load<Model>(@"Models\coco_bazoca");

            interfaceFont = Game.Content.Load<SpriteFont>(@"Fonts\InterfaceFont");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(texture, interfaceRectangle, Color.White);

            for (int i = 0; i < numberOfElements; i++)
            {
                spriteBatch.Draw(elementTextures[i], interfaceElementsRectangle[i], Color.White);
            }

            spriteBatch.DrawString(interfaceFont, "Fé: ", new Vector2(interfaceElementsRectangle[0].X, rectHeight - 30), Color.Black);

            spriteBatch.End();

            DepthStencilState zState = new DepthStencilState();
            zState.DepthBufferEnable = true;
            GraphicsDevice.DepthStencilState = zState;

            base.Draw(gameTime);
        }

        public Tower InstantiateTower(int i)
        {
            switch (i)
            {
                case 1:
                    currentTower = new Tower(Game, new Vector3(0.1f, 0.1f, 0.1f), Vector3.Zero, Vector3.Zero, altarModel);
                    break;
                case 2:
                    currentTower = new Tower(Game, Vector3.One, Vector3.Zero, Vector3.Zero, apprenticeModel);
                    break;
                case 3:
                    currentTower = new Tower(Game, Vector3.One, Vector3.Zero, Vector3.Zero, sentinelModel);
                    break;
                case 4:
                    currentTower = new HereticTower(Game, Vector3.One * 0.1f, new Vector3(-90, 0, 0), Vector3.Zero, hereticModel);
                    break;
                case 5:
                    currentTower = new Tower(Game, Vector3.One, Vector3.Zero, Vector3.Zero, trapModel);
                    break;
                default:
                    return null;
            }

            return currentTower;
        }

        public Rectangle ElementRectangle(int i)
        {
            return interfaceElementsRectangle[i];
        }
    }
}