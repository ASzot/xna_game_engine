#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

using Microsoft.Xna.Framework;

using RM = RenderingSystem.ResourceMgr;

namespace BaseLogic.Object
{
    /// <summary>
    /// An object which the user can hold.
    /// </summary>
    public class HoldableObj : StaticObj
    {
        /// <summary>
        /// Whether sights are being held down with this object.
        /// Only applies to the user player.
        /// </summary>
        private bool b_AimDownSights = false;

        /// <summary>
        /// The holder of the object.
        /// </summary>
        private Player.GamePlayer p_holder;

        /// <summary>
        /// The offset aiming down the sights has on the object.
        /// </summary>
        private Vector3 v_AimDownSightsOffset = Vector3.Zero;

        /// <summary>
        /// The v_holding offset
        /// </summary>
        private Vector3 v_holdingOffset = Vector3.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="HoldableObj"/> class.
        /// </summary>
        public HoldableObj()
            : base(Guid.NewGuid().ToString())
        {
            b_AimDownSights = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HoldableObj"/> class.
        /// </summary>
        /// <param name="staticObj">The static object.</param>
        /// <param name="objMgr">The object MGR.</param>
        public HoldableObj(StaticObj staticObj, Manager.ObjectMgr objMgr)
            : base(staticObj.ActorID)
        {
            objMgr.RemoveDataElement(staticObj);

            this.LoadContent(RM.Content, staticObj.Filename);
            this.Position = staticObj.Position;
            this.Rotation = staticObj.Rotation;
            this.Scale = staticObj.Scale;
            this.SubsetMaterials = staticObj.SubsetMaterials;
            this.SerilizeObj = staticObj.SerilizeObj;
            this.b_massModified = staticObj.MassModified;
            this.SetMass(staticObj.RigidBody.MassProperties);
        }

        /// <summary>
        /// Gets or sets the ads offset.
        /// </summary>
        /// <value>
        /// The ads offset.
        /// </value>
        public Vector3 ADSOffset
        {
            get { return v_AimDownSightsOffset; }
            set { v_AimDownSightsOffset = value; }
        }

        /// <summary>
        /// Gets or sets the holding offset.
        /// </summary>
        /// <value>
        /// The holding offset.
        /// </value>
        public Vector3 HoldingOffset
        {
            get { return v_holdingOffset; }
            set { v_holdingOffset = value; }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <returns></returns>
        public virtual string GetIdentifier()
        {
            return "Holdable";
        }

        /// <summary>
        /// Called when [drop].
        /// </summary>
        public virtual void OnDrop()
        {
            p_holder = null;
            this._rigidBody.IsActive = true;
        }

        /// <summary>
        /// Called when [interaction].
        /// </summary>
        public virtual void OnInteraction()
        {
        }

        /// <summary>
        /// Called when [throw].
        /// </summary>
        /// <returns>The speed at which to throw the object.</returns>
        public virtual float OnThrow()
        {
            p_holder = null;
            this._rigidBody.IsActive = true;

            return 5.0f;
        }

        /// <summary>
        /// Sets the ads.
        /// </summary>
        /// <param name="ads">if set to <c>true</c> [ads].</param>
        public void SetADS(bool ads)
        {
            b_AimDownSights = ads;
        }

        /// <summary>
        /// Sets the game player.
        /// </summary>
        /// <param name="holder">The holder.</param>
        public void SetGamePlayer(Player.GamePlayer holder)
        {
            p_holder = holder;
        }

        /// <summary>
        /// Update the object.
        /// Called every frame by the manager.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (p_holder == null)
                return;

            Vector3 localPos;
            if (b_AimDownSights)
                localPos = v_AimDownSightsOffset;
            else
                localPos = v_holdingOffset;

            // Don't forget to take into account the players offset.
            Matrix trans = p_holder.GetWorldMatrix();
            Vector3 worldPos = Vector3.Transform(localPos, trans);

            this.Position = worldPos;
            this.Rotation = p_holder.GetRotation();
        }
    }
}