using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TowerDefenseIA
{
    public abstract class GameObject : DrawableGameComponent
    {
        protected Matrix world;
        protected Vector3 scale;
        protected Vector3 rotation;
        protected Vector3 position;
        protected Texture2D texture;

        public Model model;
        protected Matrix[] transforms;

        public BoundingBox boundingBox;

        public GameObject(Game game, Vector3 scale, Vector3 rotation, Vector3 position)
            : base(game)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;

            game.Components.Add(this);
        }

        public GameObject(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Texture2D texture)
            : base(game)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;
            this.texture = texture;

            game.Components.Add(this);
        }

        public GameObject(Game game, Vector3 scale, Vector3 rotation, Vector3 position, Model model)
            : base(game)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.position = position;
            this.model = model;

            game.Components.Add(this);
        }

        public Vector3 GetPosition() { return position; }

        public bool CheckCollision(GameObject go1, GameObject go2)
        {
            BoundingBox aabb1 = CalculateBoundingBox(go1);
            BoundingBox aabb2 = CalculateBoundingBox(go2);

            Vector3[] obb1 = new Vector3[8];
            Vector3[] obb2 = new Vector3[8];

            aabb1.GetCorners(obb1);
            aabb1.GetCorners(obb2);

            Vector3.Transform(obb1, ref go1.world, obb1);
            Vector3.Transform(obb2, ref go2.world, obb2);

            BoundingBox worldAABB1 = BoundingBox.CreateFromPoints(obb1);
            BoundingBox worldAABB2 = BoundingBox.CreateFromPoints(obb2);

            go1.boundingBox = worldAABB1;
            go2.boundingBox = worldAABB2;

            bool hasCollided = worldAABB1.Intersects(worldAABB2);
            return hasCollided;
        }

        public BoundingBox CalculateBoundingBox(GameObject go)
        {
            // Create variables to hold min and max xyz values for the model. Initialise them to extremes
            Vector3 modelMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            Vector3 modelMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

            foreach (ModelMesh mesh in go.model.Meshes)
            {
                //Create variables to hold min and max xyz values for the mesh. Initialise them to extremes
                Vector3 meshMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
                Vector3 meshMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

                // There may be multiple parts in a mesh (different materials etc.) so loop through each
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    // The stride is how big, in bytes, one vertex is in the vertex buffer
                    // We have to use this as we do not know the make up of the vertex
                    int stride = part.VertexBuffer.VertexDeclaration.VertexStride;

                    byte[] vertexData = new byte[stride * part.NumVertices];
                    part.VertexBuffer.GetData(part.VertexOffset * stride, vertexData, 0, part.NumVertices, 1);

                    // Find minimum and maximum xyz values for this mesh part
                    // We know the position will always be the first 3 float values of the vertex data
                    Vector3 vertPosition = new Vector3();

                    for (int ndx = 0; ndx < vertexData.Length; ndx += stride)
                    {
                        vertPosition.X = BitConverter.ToSingle(vertexData, ndx);
                        vertPosition.Y = BitConverter.ToSingle(vertexData, ndx + sizeof(float));
                        vertPosition.Z = BitConverter.ToSingle(vertexData, ndx + sizeof(float) * 2);

                        // update our running values from this vertex
                        meshMin = Vector3.Min(meshMin, vertPosition);
                        meshMax = Vector3.Max(meshMax, vertPosition);
                    }
                }

                // transform by mesh bone transforms
                meshMin = Vector3.Transform(meshMin, go.transforms[mesh.ParentBone.Index]);
                meshMax = Vector3.Transform(meshMax, go.transforms[mesh.ParentBone.Index]);

                // Expand model extents by the ones from this mesh
                modelMin = Vector3.Min(modelMin, meshMin);
                modelMax = Vector3.Max(modelMax, meshMax);
            }

            // Create and return the model bounding box
            return new BoundingBox(modelMin, modelMax);
        }

        public void DrawBoundingBox(BoundingBox bb)
        {
            short[] bbIndices = {0, 1, 1, 2, 2, 3, 3, 0,
                                4, 5, 5, 6, 6, 7, 7, 4,
                                0, 4, 1, 5, 2, 6, 3, 7};

            Vector3[] corners = bb.GetCorners();

            VertexPositionColor[] verts = new VertexPositionColor[corners.Length];
            BasicEffect effect = new BasicEffect(GraphicsDevice);

            effect.World = world;
            effect.View = Camera.View;
            effect.Projection = Camera.Projection;
            effect.VertexColorEnabled = true;
            effect.TextureEnabled = false;
            
            for (int i = 0; i < corners.Length; i++)
            {
                verts[i] = new VertexPositionColor(corners[i], Color.AliceBlue);
            }

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.LineList, verts, 0, verts.Length, bbIndices, 0, 12);
            }
        }
    }
}