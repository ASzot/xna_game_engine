//-----------------------------------------------------------------------------
// Code from...
//
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;

namespace RenderingSystem.RendererImpl
{
    /// <summary>
    /// Represents an animating mesh.
    /// </summary>
    public class SkinnedMesh :  Mesh
    {
        /// <summary>
        /// The bone transforms of the model.
        /// </summary>
        private Matrix[] _boneMatrixes;
        /// <summary>
        /// The skinning data.
        /// </summary>
        private SkinningData _skinningData;

        public override void SetModel(Model model)
        {
            base.SetModel(model);
        }

        public override void SetMetadata(MeshData metadata)
        {
            base.SetMetadata(metadata);
            _skinningData = metadata.SkinningData;
            _boneMatrixes = _skinningData.BindPose.ToArray();
        }
        
        public SkinningData SkinningData
        {
            get { return _skinningData; }
        }

        public override Matrix[] BoneMatrixes
        {
            get { return _boneMatrixes; }
            set { _boneMatrixes = value; }
        }

    }
}
