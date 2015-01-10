#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using Microsoft.Xna.Framework;

using RenderingSystem;

namespace BaseLogic.Editor.Forms
{
    /// <summary>
    /// This never really worked out but it was intended to allow the user to edit objects relative to other objects.
    /// So a given object could be the origin of the scene. This could really speed things up with the scene editing process.
    /// </summary>
    public static class OriginMgr
    {
        /// <summary>
        /// The p_origin object
        /// </summary>
        private static GameObj p_originObj;

        /// <summary>
        /// Gets or sets the origin object.
        /// </summary>
        /// <value>
        /// The origin object.
        /// </value>
        public static GameObj OriginObj
        {
            get { return p_originObj; }
            set { p_originObj = value; }
        }

        /// <summary>
        /// Transform the transformation local to the set origin to global space.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static Vector3 LocalToWorld(Vector3 point)
        {
            if (p_originObj == null)
                return point;

            Vector3 transformedPoint = Vector3.Transform(point, p_originObj.Rotation);
            transformedPoint += p_originObj.Position;

            return transformedPoint;
        }

        /// <summary>
        /// Transform the world transformation of an object to the local transformation relative to the set origin.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static Vector3 WorldToLocal(Vector3 point)
        {
            if (p_originObj == null)
                return point;
            Quaternion rot = p_originObj.Rotation;
            rot = Quaternion.Inverse(rot);
            Vector3 transformedPoint = point;
            transformedPoint -= p_originObj.Position;
            transformedPoint = Vector3.Transform(transformedPoint, rot);

            return transformedPoint;
        }
    }
}