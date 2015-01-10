//-----------------------------------------------------------------------------
// Adapted from the code by...
// Jorge Adriano Luna
// http://jcoluna.wordpress.com
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna_Game_Model
{
    public enum MeshType
    {
        Both,
        Physical,
        Visual
    }

    public enum PhysicalShape
    {
        Mesh,
        Polyhedron,
        Sphere,
        Capsule
    }

    public enum WindingOrder
    {
        Clockwise,
        CounterClockwise
    }
}
