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
using BaseLogic;
using BaseLogic.Core;
using BaseLogic.Manager;
using BaseLogic.Player;
using Microsoft.Xna.Framework;

namespace Xna_Game_AI
{
    /// <summary>
    /// Behaviors for AI to move with. This is just tweaked regular movement to look beleviable for an AI.
    /// </summary>
    public class AIBehaviors
    {
        /// <summary>
        ///
        /// </summary>
        public enum SpeedLevel { Slow = 3, Normal = 2, Fast = 1 }

        /// <summary>
        /// Arrives the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="deceleration">The deceleration.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public static Vector3 Arrive(Vector3 target, SpeedLevel deceleration, GamePlayer player)
        {
            Vector3 toTarget = target - player.Position;

            float distance = toTarget.Length();

            if (distance == 0)
                return Vector3.Zero;

            const float decelFactor = 0.3f;
            float decel = (float)deceleration;
            float speed = distance / (decel * decelFactor);

            speed = Math.Min(player.MaxSpeed, speed);

            Vector3 desiredVel = toTarget * speed / distance;

            Vector3 vel = (desiredVel - player.GetVelocity());
            vel.Y = 0f;
            return vel;
        }

        /// <summary>
        /// Avoids the obstacle.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="obstaclePos">The obstacle position.</param>
        /// <returns></returns>
        public static Vector3 AvoidObstacle(GamePlayer player, Vector3 obstaclePos)
        {
            // Provide a steering force away from the obstacle.
            // Maybe provide a slightly more intricate obstacle avoiding functionality.

            // Just provide a direct steering force away from the obstacle.
            return Flee(obstaclePos, player);
        }

        /// <summary>
        /// Flees the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public static Vector3 Flee(Vector3 target, GamePlayer player)
        {
            Vector3 vel = player.Position - target;
            vel.Normalize();
            vel.Y = 0f;

            return vel;
        }

        /// <summary>
        /// Gets the collision course.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="intersectionPoint">The intersection point.</param>
        /// <param name="intersectionDistance">The intersection distance.</param>
        /// <returns></returns>
        public static Henge3D.Physics.RigidBody GetCollisionCourse(GamePlayer entity, out Vector3 intersectionPoint, out float intersectionDistance)
        {
            intersectionPoint = Vector3.Zero;
            intersectionDistance = 0f;

            Matrix entityRot = entity.GetLookDirRot();
            Vector3 pos = entity.Position;

            float speedFact = 3f * entity.Speed;
            Vector3 baseOffset = new Vector3(0f, 0f, 5f);
            Matrix transformMat = entityRot * Matrix.CreateTranslation(pos);

            Vector3 origRayEnd = new Vector3(0f, 0f, speedFact);
            Vector3 origRayStart = new Vector3(0f, 0f, 0f);
            origRayStart += baseOffset;
            origRayEnd += baseOffset;

            Vector3 rayEnd = Vector3.Transform(origRayEnd, transformMat);
            Vector3 rayStart = Vector3.Transform(origRayStart, transformMat);
            rayEnd.Y = rayStart.Y = 1f;
            var rayHit = GamePlayer.RaytraceFunc(new RaytraceFireInfo(rayStart, rayEnd));
            if (rayHit != null)
            {
                intersectionPoint = rayHit.IntersectionPoint;
                intersectionDistance = rayHit.IntersectionDistance;
                return rayHit.IntersectionBody;
            }

            return null;
        }

        /// <summary>
        /// Gets the collision course.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Henge3D.Physics.RigidBody GetCollisionCourse(GamePlayer entity)
        {
            Vector3 tmpVec;
            float tmpFloat;
            return GetCollisionCourse(entity, out tmpVec, out tmpFloat);
        }

        /// <summary>
        /// Gets the game players in view.
        /// </summary>
        /// <param name="gamePlayer">The game player.</param>
        /// <returns></returns>
        public static List<GamePlayer> GetGamePlayersInView(GamePlayer gamePlayer)
        {
            var allGamePlayers = GamePlayer.p_PlayerMgr.GetDataElements();
            var exclusiveGamePlayers = from gp in allGamePlayers
                                       where gp.ActorID != gamePlayer.ActorID
                                       select gp;

            List<GamePlayer> playersInView = new List<GamePlayer>();
            foreach (GamePlayer currentPlayer in exclusiveGamePlayers)
            {
                if (gamePlayer.CanSee(currentPlayer))
                    playersInView.Add(currentPlayer);
            }

            return playersInView;
        }

        /// <summary>
        /// Hits the target.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetID">The target identifier.</param>
        /// <param name="castEnd">The cast end.</param>
        /// <returns></returns>
        public static bool HitTarget(GamePlayer entity, string targetID, Vector3 castEnd)
        {
            Vector3 start = new Vector3(0, entity.YRayFireOffset, entity.ZRayStart);

            Matrix rot = Matrix.CreateFromQuaternion(entity.GetRotation()) * Matrix.CreateRotationY(MathHelper.Pi);

            Matrix transformMat = rot * Matrix.CreateTranslation(entity.Position);

            start = Vector3.Transform(start, transformMat);

            RaytraceIntersectionInfo raytraceIntersection = GamePlayer.RaytraceFunc(new RaytraceFireInfo(start, castEnd));
            if (raytraceIntersection != null)
            {
                GameRigidBody gameRigidBody = raytraceIntersection.IntersectionBody as GameRigidBody;
                if (gameRigidBody.Name == targetID)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Ins the distance squared.
        /// </summary>
        /// <param name="gamePlayer1">The game player1.</param>
        /// <param name="gamePlayer2">The game player2.</param>
        /// <param name="distanceTolerance">The distance tolerance.</param>
        /// <returns></returns>
        public static bool InDistanceSquared(GamePlayer gamePlayer1, GamePlayer gamePlayer2, float distanceTolerance)
        {
            return InDistanceSquared(gamePlayer1.Position, gamePlayer2.Position, distanceTolerance);
        }

        /// <summary>
        /// Ins the distance squared.
        /// </summary>
        /// <param name="gamePlayer1">The game player1.</param>
        /// <param name="pos">The position.</param>
        /// <param name="distanceTolerance">The distance tolerance.</param>
        /// <returns></returns>
        public static bool InDistanceSquared(GamePlayer gamePlayer1, Vector3 pos, float distanceTolerance)
        {
            return InDistanceSquared(gamePlayer1.Position, pos, distanceTolerance);
        }

        /// <summary>
        /// Ins the distance squared.
        /// </summary>
        /// <param name="pos1">The pos1.</param>
        /// <param name="pos2">The pos2.</param>
        /// <param name="distanceTolerance">The distance tolerance.</param>
        /// <returns></returns>
        public static bool InDistanceSquared(Vector3 pos1, Vector3 pos2, float distanceTolerance)
        {
            float distance = Vector3.DistanceSquared(pos1, pos2);

            return distance <= distanceTolerance;
        }

        /// <summary>
        /// Picks the best target.
        /// </summary>
        /// <param name="gamePlayersInSight">The game players in sight.</param>
        /// <returns></returns>
        public static GamePlayer PickBestTarget(List<GamePlayer> gamePlayersInSight)
        {
            return gamePlayersInSight.ElementAt(0);
        }

        /// <summary>
        /// Seeks the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        public static Vector3 Seek(Vector3 target, GamePlayer player)
        {
            Vector3 playerPos = player.Position;
            playerPos.Y = 0f;
            Vector3 vel = target - playerPos;
            vel.Normalize();

            vel.Y = 0f;

            return vel;
        }

        /// <summary>
        /// Wanders the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="currentTheta">The current theta.</param>
        /// <param name="jitter">The jitter.</param>
        /// <returns></returns>
        public static Vector3 Wander(GamePlayer player, ref float currentTheta, float jitter)
        {
            currentTheta += (float)GameMath.NextDoubleInRange(-1, 1f) * jitter;
            Vector3 seekPos = new Vector3((float)Math.Cos(currentTheta), 0f, (float)Math.Sin(currentTheta));
            seekPos.Z += 10f;
            seekPos += player.Position;

            return Seek(seekPos, player);
        }
    }
}