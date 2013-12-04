using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;


namespace TowerDefenseIA.Classes
{
    public class AnimatedModel
    {
        public Model model = null;
        public string animationName = null;

        public AnimatedModel(Model model, string animationName) 
        {
            this.model = model;
            this.animationName = animationName;
        }
    }
}
