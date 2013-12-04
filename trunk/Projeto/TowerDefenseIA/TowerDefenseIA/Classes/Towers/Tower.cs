using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;
using TowerDefenseIA.Classes;

namespace TowerDefenseIA
{
    public class Tower : GameObject
    {
        protected bool isFixed = false;
        protected int price;
        public int row;
        private bool isAnimated = false;

        private AnimationPlayer animationPlayer;

        // Non-animated
        public Tower(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model) : base(game, scale, rotation, position, model)
        {

        }

        // One Animation
        public Tower(Game game, Vector3 scale, Vector3 rotation, Vector3 position, AnimatedModel animatedModel) : base(game, scale, rotation, position, animatedModel)
        {
            this.isAnimated = true;

            this.animatedModel = animatedModel;

            // Look up our custom skinning information.
            SkinningData skinningData = animatedModel.model.Tag as SkinningData;

            if (skinningData == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            // Create an animation player, and start decoding an animation clip.
            this.animationPlayer = new AnimationPlayer(skinningData);

            AnimationClip clip = skinningData.AnimationClips[this.animatedModel.animationName];

            this.animationPlayer.StartClip(clip);
        }

        // More than one animation
        public Tower(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Dictionary<string, AnimatedModel> animatedModels) : base(game, scale, rotation, position, animatedModels)
        {
            this.isAnimated = true;

            this.animatedModels = animatedModels;

            // Look up our custom skinning information.
            AnimatedModel am;
            animatedModels.TryGetValue("stand", out am);
            SkinningData skinningData = am.model.Tag as SkinningData;

            if (skinningData == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            // Create an animation player, and start decoding an animation clip.
            this.animationPlayer = new AnimationPlayer(skinningData);

            AnimationClip clip = skinningData.AnimationClips[am.animationName];
        }

        public void ChangeAnimation(string AnimationName)
        {
            // Look up our custom skinning information.
            AnimatedModel am;
            animatedModels.TryGetValue(AnimationName, out am);
            if (am == null)
            {
                throw new ArgumentNullException("Não existe uma animação com esse nome");
            }

            SkinningData skinningData = am.model.Tag as SkinningData;

            if (skinningData == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            // Create an animation player, and start decoding an animation clip.
            this.animationPlayer = new AnimationPlayer(skinningData);

            AnimationClip clip = skinningData.AnimationClips[am.animationName];
        }

        public override void Initialize()
        {
            position = Input.MousePositionInWorld;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (isFixed == false)
            {
                position = Input.MousePositionInWorld;

            }

            if (this.isAnimated)
            {
                this.animationPlayer.UpdateUsingTime(gameTime.ElapsedGameTime, true, world);
                //this.animationPlayer.UpdateUsingKeyframes(gameTime.ElapsedGameTime, true, world);
            }

            world = Matrix.CreateScale(scale) * Matrix.CreateFromYawPitchRoll(MathHelper.ToRadians(rotation.Y), MathHelper.ToRadians(rotation.X), MathHelper.ToRadians(rotation.Z)) * Matrix.CreateTranslation(position);

            base.Update(gameTime);
        }

        public void Fix(Vector3 position)
        {
            isFixed = true;
            this.position = position;
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.isAnimated)
            {
                Matrix[] bones = animationPlayer.GetSkinTransforms();

                transforms = new Matrix[this.animatedModel.model.Bones.Count];
                this.animatedModel.model.CopyAbsoluteBoneTransformsTo(transforms);

                // Render the skinned mesh.
                foreach (ModelMesh mesh in this.animatedModel.model.Meshes)
                {
                    foreach (SkinnedEffect effect in mesh.Effects)
                    {
                        effect.SetBoneTransforms(bones);

                        effect.EnableDefaultLighting();
                        effect.World = world;
                        effect.View = Camera.View;
                        effect.Projection = Camera.Projection;
                    }

                    mesh.Draw();
                }
            }
            else
            {
                transforms = new Matrix[this.model.Bones.Count];

                this.model.CopyAbsoluteBoneTransformsTo(transforms);

                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = world;
                        effect.View = Camera.View;
                        effect.Projection = Camera.Projection;
                    }

                    mesh.Draw();
                }
            }

            base.Draw(gameTime);
        }
    }
}