using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefenseIA.Classes
{
    class RayUtil
    {
        public Ray GetScreenRay(Vector2 screenPosition, Viewport viewport, Matrix projectionMatrix, Matrix viewMatrix, Matrix worldMatrix)
        {
            Vector3 nearPoint = viewport.Unproject(new Vector3(screenPosition, 0f), projectionMatrix, viewMatrix, worldMatrix);
            Vector3 farPoint = viewport.Unproject(new Vector3(screenPosition, 1f), projectionMatrix, viewMatrix, worldMatrix);
            return new Ray(nearPoint, Vector3.Normalize(farPoint - nearPoint));
        }
    }
}
