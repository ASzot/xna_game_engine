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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RenderingSystem
{

    /// <summary>
    /// The base for all objects represented in game.
    /// </summary>
	public interface GameActor
	{
        /// <summary>
        /// The position of the object in the 3D scene.
        /// </summary>
		Vector3 Position { get; set; }

        /// <summary>
        /// The unique identification of the actor.
        /// </summary>
		string ActorID { get; set; }

        /// <summary>
        /// Setting to true will remove the object from the manager in charge of it.
        /// </summary>
        bool Kill { get; set; }
	}
}
