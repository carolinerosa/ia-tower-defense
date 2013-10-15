using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA
{
    public enum TowerType { Knight, Archer, Mage, Jester, TheChosenOne };

    public class Tower : GameObject
    {
        Model model;
        TowerType type;

        public Tower(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Texture2D texture, TowerType type) : base(game, scale, rotation, position, texture)
        {
            this.type = type;
        }

        public override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {
            base.LoadContent();

            switch (type)
	        {
                case TowerType.Archer:
                    model = Game.Content.Load<Model>(@"Models\ArcherModel");
                    break;
                case TowerType.Jester:
                    model = Game.Content.Load<Model>(@"Models\JesterModel");
                    break;
                case TowerType.Knight:
                    model = Game.Content.Load<Model>(@"Models\KnightModel");
                    break;
                case TowerType.Mage:
                    model = Game.Content.Load<Model>(@"Models\MageModel");
                    break;
                case TowerType.TheChosenOne:
                    model = Game.Content.Load<Model>(@"Models\ChosenOneModel");
                    break;
	        }
        }

        public override void Update(GameTime gameTime)
        {
            world = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z) * Matrix.CreateTranslation(position);

            base.Update(gameTime);
        }
    }
}