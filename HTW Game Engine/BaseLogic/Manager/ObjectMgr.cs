#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;
using System.Linq;
using BaseLogic.Object;
using Microsoft.Xna.Framework;
using RenderingSystem;

namespace BaseLogic.Manager
{
    /// <summary>
    /// Manages all of the objects in the scene.
    /// </summary>
    public class ObjectMgr : MgrTemplate<GameObj>
    {
        // The complete list of animation spans for all the models.
        /// <summary>
        /// The _anim spans
        /// </summary>
        private List<AnimationSpan> _animSpans = new List<AnimationSpan>();

        /// <summary>
        /// The p_physics MGR
        /// </summary>
        private Manager.GamePhysicsMgr p_physicsMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectMgr"/> class.
        /// </summary>
        public ObjectMgr()
        {
        }

        /// <summary>
        /// Actors the identifier exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool ActorIDExists(string id)
        {
            foreach (GameObj gameObj in _dataElements)
            {
                if (gameObj.ActorID == id)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the animation span.
        /// </summary>
        /// <param name="animationSpan">The animation span.</param>
        public void AddAnimationSpan(AnimationSpan animationSpan)
        {
            _animSpans.Add(animationSpan);
        }

        /// <summary>
        /// Adds to list.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void AddToList(GameObj data)
        {
            // Check to see if there is an animation which has a corresponding filename.
            if (data is AnimatedObj)
            {
                AnimatedObj animObj = data as AnimatedObj;

                foreach (AnimationSpan animSpan in _animSpans)
                {
                    if (animSpan.TargetedModel == animObj.Filename)
                    {
                        animObj.AddAnimationSpan(animSpan);
                    }
                }
            }
            else if (data is PhasedAnimObj)
            {
                PhasedAnimObj phasedAnimObj = data as PhasedAnimObj;
                foreach (AnimationSpan animSpan in _animSpans)
                {
                    if (animSpan.TargetedModel == phasedAnimObj.Filename)
                    {
                        phasedAnimObj.AddAnimationSpan(animSpan);
                    }
                }
            }
            base.AddToList(data);
        }

        /// <summary>
        /// Gets the data element.
        /// </summary>
        /// <param name="actorID">The actor identifier.</param>
        /// <returns></returns>
        public GameObj GetDataElement(string actorID)
        {
            foreach (GameObj gameObj in _dataElements)
            {
                if (gameObj.ActorID == actorID)
                    return gameObj;
            }
            return null;
        }

        /// <summary>
        /// Gets the index of object.
        /// </summary>
        /// <param name="gameObj">The game object.</param>
        /// <returns></returns>
        public int GetIndexOfObj(GameObj gameObj)
        {
            for (int i = 0; i < _dataElements.Count; ++i)
            {
                GameObj go = _dataElements[i];
                if (go.ActorID == gameObj.ActorID)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent()
        {
        }

        /// <summary>
        /// Removes the data element.
        /// </summary>
        /// <param name="gameObj">The game object.</param>
        public override void RemoveDataElement(GameObj gameObj)
        {
            if (gameObj is PhysObj)
            {
                PhysObj physObj = gameObj as PhysObj;
                // Remove the physics object as well.
                p_physicsMgr.RemoveRigidBody(physObj.RigidBody);
            }
            _dataElements.Remove(gameObj);
        }

        /// <summary>
        /// Removes the data element.
        /// </summary>
        /// <param name="i">The i.</param>
        public override void RemoveDataElement(int i)
        {
            GameObj gameObj = _dataElements.ElementAt(i);

            if (gameObj is PhysObj)
            {
                PhysObj physObj = gameObj as PhysObj;
                // Remove the physics object as well.
                p_physicsMgr.RemoveRigidBody(physObj.RigidBody);
            }

            base.RemoveDataElement(i);
        }

        /// <summary>
        /// Sets the physics MGR.
        /// </summary>
        /// <param name="pPhysicsMgr">The p physics MGR.</param>
        public void SetPhysicsMgr(Manager.GamePhysicsMgr pPhysicsMgr)
        {
            p_physicsMgr = pPhysicsMgr;
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {
            foreach (var data in _dataElements)
            {
                if (data is PhysObj)
                    (data as PhysObj).UnloadContent();
            }
        }

        /// <summary>
        /// Updates the objs.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void UpdateObjs(GameTime gameTime)
        {
            if (!b_update)
                return;

            for (int i = 0; i < _dataElements.Count; ++i)
            {
                GameObj gameObj = _dataElements.ElementAt(i);
                if (gameObj.Kill)
                {
                    // Terminate the existence of this object.
                    RemoveDataElement(i);
                    // So we don't skip one of the elements in the array.
                    --i;
                    continue;
                }

                gameObj.Update(gameTime);
            }
        }
    }
}