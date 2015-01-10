#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Linq;

using BaseLogic;

using Microsoft.Xna.Framework;

namespace My_Xna_Game.Game_Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class OrbletMgr : RenderingSystem.MgrTemplate<Orblet>
    {
        private const string MODEL_FILENAME = "sphere";

        private HelperOrblet p_helperOrblet;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrbletMgr"/> class.
        /// </summary>
        public OrbletMgr()
        {
        }

        /// <summary>
        /// Gets the helper orblet.
        /// </summary>
        /// <value>
        /// The helper orblet.
        /// </value>
        public HelperOrblet HelperOrblet
        {
            get { return p_helperOrblet; }
        }

        /// <summary>
        /// Gets the random orblet.
        /// </summary>
        /// <returns></returns>
        public Orblet GetRandomOrblet()
        {
            int randomIndex = BaseLogic.Core.ThreadSafeRandom.ThisThreadsRandom.Next(0, _dataElements.Count);
            return GetDataElementAt(randomIndex);
        }

        /// <summary>
        /// Gets the random teleporter orblet.
        /// </summary>
        /// <returns></returns>
        public TeleportationOrblet GetRandomTeleporterOrblet()
        {
            var teleporterOrblets = from orblet in _dataElements
                                    where orblet is TeleportationOrblet
                                    select orblet as TeleportationOrblet;
            int randomIndex = BaseLogic.Core.ThreadSafeRandom.ThisThreadsRandom.Next(0, teleporterOrblets.Count());
            return teleporterOrblets.ElementAt(randomIndex);
        }

        /// <summary>
        /// Spawns the orblet.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="orblet">The orblet.</param>
        public void SpawnOrblet(Vector3 position, Orblet orblet)
        {
            orblet = (Orblet)GameSystem.GameSys_Instance.CreateStaticObj(orblet, MODEL_FILENAME);
            if (orblet is HelperOrblet)
                orblet.ActorID = HelperOrblet.ID;
            else
                orblet.ActorID = "orblet:" + (GetNumberOfDataElements() + 1).ToString();
            orblet.BobbleMidline = position.Y + 6f;
            orblet.SetMass(float.PositiveInfinity);
            orblet.Position = position;
            orblet.SubsetMaterials[0].UseDiffuseMat = true;
            orblet.SubsetMaterials[0].DiffuseMat = Color.LightBlue.ToVector4();
            AddToList(orblet);

            if (orblet is HelperOrblet)
                p_helperOrblet = orblet as HelperOrblet;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="userPlayer">The user player.</param>
        public void Update(GameTime gameTime, GameUserPlayer userPlayer)
        {
            if (!b_update)
                return;

            if (userPlayer == null)
                return;

            foreach (Orblet orblet in _dataElements)
            {
                orblet.Update(gameTime, userPlayer);
            }
        }
    }
}