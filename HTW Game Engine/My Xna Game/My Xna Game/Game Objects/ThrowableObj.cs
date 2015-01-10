#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using BaseLogic;
using BaseLogic.Object;

namespace My_Xna_Game.Game_Objects
{
    /// <summary>
    ///
    /// </summary>
    internal class ThrowableObj : HoldableObj
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThrowableObj"/> class.
        /// </summary>
        /// <param name="staticObj">The static object.</param>
        /// <param name="objMgr">The object MGR.</param>
        public ThrowableObj(StaticObj staticObj, BaseLogic.Manager.ObjectMgr objMgr)
            : base(staticObj, objMgr)
        {
        }

        /// <summary>
        /// Called when [drop].
        /// </summary>
        public override void OnDrop()
        {
            base.OnDrop();
        }

        /// <summary>
        /// Called when [throw].
        /// </summary>
        public override float OnThrow()
        {
            base.OnThrow();

            return 30.0f;
        }

        /// <summary>
        /// Called when [interaction].
        /// </summary>
        public override void OnInteraction()
        {
            base.OnInteraction();
        }
    }
}