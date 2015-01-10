#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SkinnedModel;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// Where the information is stored about the processed mesh.
    /// </summary>
    public class MeshData
    {
        /// <summary>
        /// The bounding box of the loaded mesh.
        /// </summary>
        private BoundingBox _boundingBox;

        /// <summary>
        /// The animation skinning data if there is any.
        /// </summary>
        private SkinningData _skinningData;

        /// <summary>
        /// All of the sub mesh data. (Where the bulk of the information is).
        /// </summary>
        private List<SubMeshData> _subMeshesMetadata = new List<SubMeshData>();

        /// <summary>
        /// The type of rendering this mesh when in game must recieve.
        /// </summary>
        public enum RenderQueueType
        {
            Default,
            SkipGbuffer,
            Blend,
            Count
        }

        /// <summary>
        /// The bounding box of this mesh.
        /// </summary>
        public BoundingBox BoundingBox
        {
            get { return _boundingBox; }
            set { _boundingBox = value; }
        }

        /// <summary>
        /// The skinning animation data if any of this mesh.
        /// </summary>
        public SkinningData SkinningData
        {
            get { return _skinningData; }
            set { _skinningData = value; }
        }

        /// <summary>
        /// The list of all submeshes for this mesh.
        /// </summary>
        public List<SubMeshData> SubMeshesMetadata
        {
            get { return _subMeshesMetadata; }
        }

        /// <summary>
        /// Add a new sub mesh
        /// </summary>
        /// <param name="subMeshData">The sub mesh data to add.</param>
        public void AddSubMeshData(SubMeshData subMeshData)
        {
            _subMeshesMetadata.Add(subMeshData);
        }

        /// <summary>
        /// The sub mesh which has just been loaded by the model processor.
        /// </summary>
        public class SubMeshData
        {
            /// <summary>
            /// The bounding box for the sub mesh.
            /// </summary>
            private BoundingBox _boundingBox;
            /// <summary>
            /// The rendering this sub mesh must receive can differ throughout the overall mesh.
            /// </summary>
            private RenderQueueType _renderQueue = RenderQueueType.Default;
            /// <summary>
            /// Whether this sub mesh participates in the construction of the shadow map.
            /// </summary>
            private bool b_castShadows = true;

            /// <summary>
            /// The bounding box for this sub mesh.
            /// </summary>
            public BoundingBox BoundingBox
            {
                get { return _boundingBox; }
                set { _boundingBox = value; }
            }

            /// <summary>
            /// Whether this sub mesh participates in the contruction of the shadow map.
            /// </summary>
            public bool CastShadows
            {
                get { return b_castShadows; }
                set { b_castShadows = value; }
            }

            /// <summary>
            /// The rendering this sub mesh must receive.
            /// Can differ throughout the overall mesh.
            /// </summary>
            public RenderQueueType RenderQueue
            {
                get { return _renderQueue; }
                set { _renderQueue = value; }
            }
        }
    }
}