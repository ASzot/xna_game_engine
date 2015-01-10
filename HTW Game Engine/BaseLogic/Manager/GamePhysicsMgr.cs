#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Henge3D;
using Henge3D.Physics;
using Microsoft.Xna.Framework;

namespace BaseLogic.Manager
{
    /// <summary>
    /// Manages all of the physics data in a scene.
    /// </summary>
    public struct PhysicsSaveData
    {
        /// <summary>
        /// The revolute save datas
        /// </summary>
        public RevoluteJointSaveData[] RevoluteSaveDatas;
    }

    /// <summary>
    /// A ray in 3D space.
    /// Direction matters.
    /// </summary>
    public struct RaytraceFireInfo
    {
        /// <summary>
        /// End of ray.
        /// </summary>
        public Vector3 End;

        /// <summary>
        /// Start of ray.
        /// </summary>
        public Vector3 Start;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaytraceFireInfo"/> struct.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        public RaytraceFireInfo(Vector3 start, Vector3 end)
        {
            this.Start = start;
            this.End = end;
        }
    }

    /// <summary>
    /// The joint data for the RevoluteJoint.
    /// </summary>
    public struct RevoluteJointSaveData
    {
        /// <summary>
        /// The actor id of body A.
        /// </summary>
        public string ActorID_A;

        /// <summary>
        /// The actor id of body B.
        /// </summary>
        public string ActorID_B;

        /// <summary>
        /// The axis
        /// </summary>
        public Vector3 Axis;

        /// <summary>
        /// The maximum angle
        /// </summary>
        public float MaxAngle;

        /// <summary>
        /// The minimum angle
        /// </summary>
        public float MinAngle;

        /// <summary>
        /// The world point
        /// </summary>
        public Vector3 WorldPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="RevoluteJointSaveData"/> struct.
        /// </summary>
        /// <param name="grj">The GRJ.</param>
        /// <exception cref="System.ArgumentException">
        /// Not game rigid body!
        /// </exception>
        public RevoluteJointSaveData(GameRevoluteJoint grj)
        {
            this.WorldPoint = grj.WorldPoint;
            this.Axis = grj.Axis;
            this.MinAngle = grj.MinAngle;
            this.MaxAngle = grj.MaxAngle;
            if (grj.BodyA is GameRigidBody)
            {
                GameRigidBody grb = grj.BodyA as GameRigidBody;
                this.ActorID_A = grb.Name;
            }
            else
                throw new ArgumentException("Not game rigid body!");
            if (grj.BodyB is GameRigidBody)
            {
                GameRigidBody grb = grj.BodyB as GameRigidBody;
                this.ActorID_B = grb.Name;
            }
            else
                throw new ArgumentException("Not game rigid body!");
        }

        /// <summary>
        /// Convert to the <see cref="GameRevoluteJoint"/>
        /// </summary>
        /// <param name="physics">The physics.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Error finding linked bodies!</exception>
        public GameRevoluteJoint ToRevoluteJoint(Henge3D.Physics.PhysicsManager physics)
        {
            var gameBodies = from rb in physics.Bodies
                             where rb is GameRigidBody
                             select rb as GameRigidBody;

            RigidBody bodyA = null;
            RigidBody bodyB = null;

            foreach (GameRigidBody grb in gameBodies)
            {
                if (grb.Name == ActorID_A)
                    bodyA = grb;
                if (grb.Name == ActorID_B)
                    bodyB = grb;
            }

            if (bodyA == null || bodyB == null)
            {
                throw new ArgumentException("Error finding linked bodies!");
            }

            GameRevoluteJoint grj = new GameRevoluteJoint(bodyA, bodyB, WorldPoint, Axis, MinAngle, MaxAngle);

            return grj;
        }
    }

    /// <summary>
    /// Manages all of the physics data.
    /// </summary>
    public class GamePhysicsMgr
    {
        /// <summary>
        /// The _henge physics MGR
        /// </summary>
        private Henge3D.Physics.PhysicsManager _hengePhysicsMgr;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePhysicsMgr"/> class.
        /// </summary>
        /// <param name="game">The game.</param>
        public GamePhysicsMgr(Microsoft.Xna.Framework.Game game)
        {
            _hengePhysicsMgr = new Henge3D.Physics.PhysicsManager(game);
        }

        /// <summary>
        /// Gets the henge physics MGR.
        /// </summary>
        /// <value>
        /// The henge physics MGR.
        /// </value>
        public Henge3D.Physics.PhysicsManager HengePhysicsMgr
        {
            get { return _hengePhysicsMgr; }
        }

        /// <summary>
        /// Adds the constraint.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        public void AddConstraint(Constraint constraint)
        {
            _hengePhysicsMgr.Add(constraint);
        }

        /// <summary>
        /// Adds the rigid body.
        /// </summary>
        /// <param name="rb">The rb.</param>
        public void AddRigidBody(RigidBody rb)
        {
            _hengePhysicsMgr.Add(rb);
        }

        /// <summary>
        /// Get the drawable lines representing the constraints in the scene.
        /// These will be drawn in debug mode only.
        /// </summary>
        /// <returns>
        /// A list of vectors representing the lines to draw. 
        /// Each vector is 2D with the first component being the start
        /// and the second being the end.
        /// </returns>
        public List<Vector3[]> GetConstraintDebugLines()
        {
            List<Vector3[]> lines = new List<Vector3[]>();

            foreach (var rb in _hengePhysicsMgr.Bodies)
            {
                var constraints = rb.Constraints;
                foreach (var constraint in constraints)
                {
                    if (constraint is GameRevoluteJoint)
                    {
                        GameRevoluteJoint rj = constraint as GameRevoluteJoint;
                        const float scalingFactor = 2f;
                        Vector3 axis = rj.Axis * scalingFactor;
                        Vector3 worldAxisEnd = axis + rj.WorldPoint;
                        Vector3 worldAxisStart = rj.WorldPoint;
                        Vector3[] line = { worldAxisStart, worldAxisEnd };
                        lines.Add(line);
                    }
                }
            }

            return lines;
        }

        /// <summary>
        /// Gets the save data.
        /// </summary>
        /// <returns></returns>
        public PhysicsSaveData GetSaveData()
        {
            List<Constraint> constraints = new List<Constraint>();
            foreach (RigidBody rb in _hengePhysicsMgr.Bodies)
            {
                foreach (Constraint constraint in rb.Constraints)
                {
                    constraints.Add(constraint);
                }
            }

            constraints = constraints.Distinct().ToList();

            var revoluteJointSaveData = from c in constraints
                                        where c is GameRevoluteJoint
                                        let gc = c as GameRevoluteJoint
                                        select new RevoluteJointSaveData(gc);

            PhysicsSaveData psd;
            psd.RevoluteSaveDatas = revoluteJointSaveData.ToArray();

            return psd;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent()
        {
            Player.GamePlayer.RaytraceFunc = Raytrace;

            _hengePhysicsMgr.Initialize();
            _hengePhysicsMgr.Enabled = true;
            _hengePhysicsMgr.Gravity = new Microsoft.Xna.Framework.Vector3(0f, -9.8f, 0f);
        }

        /// <summary>
        /// Loads the save data.
        /// </summary>
        /// <param name="psd">The PSD.</param>
        public void LoadSaveData(PhysicsSaveData psd)
        {
            List<Constraint> constraints = new List<Constraint>();

            var revoluteJointConstraints = from rj in psd.RevoluteSaveDatas
                                           select rj.ToRevoluteJoint(_hengePhysicsMgr);

            constraints.AddRange(revoluteJointConstraints);
        }

        /// <summary>
        /// Raytraces given the specified information about the raytrace fire.
        /// </summary>
        /// <param name="rti">The fire information.</param>
        /// <returns>
        /// Information about the intersection
        /// Null is returned if there was no intersection.
        /// </returns>
        public RaytraceIntersectionInfo Raytrace(RaytraceFireInfo rti)
        {
            Segment seg = new Segment(rti.Start, rti.End);
            float scalar;
            Vector3 point;
            Composition intersection = _hengePhysicsMgr.BroadPhase.Intersect(ref seg, out scalar, out point);
            if (intersection != null && intersection is BodySkin)
            {
                RigidBody intersectionBody = ((BodySkin)intersection).Owner;
                return new RaytraceIntersectionInfo(intersectionBody, point, scalar);
            }

            return null;
        }

        /// <summary>
        /// Removes the rigid body.
        /// </summary>
        /// <param name="removeRb">The remove rb.</param>
        public void RemoveRigidBody(RigidBody removeRb)
        {
            _hengePhysicsMgr.Remove(removeRb);
        }

        /// <summary>
        /// Toggles the physics.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public void TogglePhysics(bool enabled)
        {
            _hengePhysicsMgr.Enabled = enabled;
        }
    }

    /// <summary>
    /// The game implementation of the Henge RevoluteJoint
    /// </summary>
    public class GameRevoluteJoint : RevoluteJoint
    {
        /// <summary>
        /// The axis
        /// </summary>
        public Vector3 Axis;

        /// <summary>
        /// The maximum angle
        /// </summary>
        public float MaxAngle;

        /// <summary>
        /// The minimum angle
        /// </summary>
        public float MinAngle;

        /// <summary>
        /// The world point
        /// </summary>
        public Vector3 WorldPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRevoluteJoint"/> class.
        /// </summary>
        /// <param name="bodyA">The first constrained body.</param>
        /// <param name="bodyB">The second constrained body.</param>
        /// <param name="worldPoint">The point in world coordinates shared by both bodies at the time the constraint is created.</param>
        /// <param name="axis">The axis around which to allow relative rotation. Rotation around the other axes will be prevented.</param>
        /// <param name="minAngle">The minimum angle specifies (in radians) the most that the first body can rotate clockwise relative
        /// to the second body around the axis. This number should be zero or negative.</param>
        /// <param name="maxAngle">The maximum angle specifies (in radians) the most that the first body can rotate counter-clockwise relative
        /// to the second body around the axis. This number should be zero or positive.</param>
        public GameRevoluteJoint(RigidBody bodyA, RigidBody bodyB, Vector3 worldPoint, Vector3 axis, float minAngle, float maxAngle)
            : base(bodyA, bodyB, worldPoint, axis, minAngle, maxAngle)
        {
            WorldPoint = worldPoint;
            Axis = axis;
            MinAngle = minAngle;
            MaxAngle = maxAngle;
        }
    }

    /// <summary>
    /// Information about a ratrace intersection.
    /// </summary>
    public class RaytraceIntersectionInfo
    {
        /// <summary>
        /// The intersection body
        /// </summary>
        public RigidBody IntersectionBody = null;

        /// <summary>
        /// The intersection distance
        /// </summary>
        public float IntersectionDistance = 0f;

        /// <summary>
        /// The intersection point
        /// </summary>
        public Vector3 IntersectionPoint = Vector3.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="RaytraceIntersectionInfo"/> class.
        /// </summary>
        /// <param name="rb">The rb.</param>
        /// <param name="intersectionPoint">The intersection point.</param>
        /// <param name="intersectionDistance">The intersection distance.</param>
        public RaytraceIntersectionInfo(RigidBody rb, Vector3 intersectionPoint, float intersectionDistance)
        {
            IntersectionBody = rb;
            IntersectionPoint = intersectionPoint;
            IntersectionDistance = intersectionDistance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RaytraceIntersectionInfo"/> class.
        /// </summary>
        public RaytraceIntersectionInfo()
        {
        }
    }
}