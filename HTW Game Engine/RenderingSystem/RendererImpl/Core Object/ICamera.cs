#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem
{
	/// <summary>
	/// The base functionality for the camera.
	/// </summary>
    public interface ICamera : GameActor
    {
        float TanFovy { get; }
        Matrix Proj { get; }
        Matrix View { get; }
        Matrix Transform { get; }
        Matrix ViewProj { get; }
        float Aspect { get; }

        float NearClip { get; }
        float FarClip { get; }

        float Pitch { get; set; }
        float Yaw { get; set; }
        float MaxPitch { get; set; }
        float MinPitch { get; set; }

        void SetProjection(float planeNear, float planeFar, float fov, float aspectRatio);

        void Update(GameTime gameTime);
        void Move(Vector3 delta, float speed, float dt);

        void SetDefault(Viewport vp);

        Vector3 UpAxis { get; set; }
        Vector3 ForwardAxis { get; set; }
        Vector3 SideAxis { get; }
        Vector3 Direction { get; set; }

        BoundingFrustum Frustum { get; }
    }
}
