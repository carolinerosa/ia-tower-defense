﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TowerDefenseIA
{
    public abstract class GameObject : DrawableGameComponent
    {
        protected Matrix world = Matrix.Identity;
        protected Vector3 scale;
        protected Vector3 rotation;
        protected Vector3 position;
        protected Texture2D texture;
        public Model model;


        public GameObject(Game game, Vector3 scale, Vector3 rotation, Vector3 position) : base(game)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;

            game.Components.Add(this);
        }

        public GameObject(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Texture2D texture) : base(game)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;
            this.texture = texture;
            
            game.Components.Add(this);
        }

        public GameObject(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model) : base(game)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;
            this.model = model;

            game.Components.Add(this);
        }

        public Vector3 GetPosition() { return position; }
    }
}
