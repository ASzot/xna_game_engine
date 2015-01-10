#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Linq;
using BaseLogic;
using Microsoft.Xna.Framework;

namespace My_Xna_Game.Game_Objects
{
    /// <summary>
    /// The orblet which the user can summon.
    /// </summary>
    public class HelperOrblet : Orblet
    {
        // Dialog references.
        public const string DIALOG_FILE = "OrbletDialog";

        public const string DLG_CHAIN_ARROWS_PURCHASED = "triviaCorrectArrowsDialog";
        public const string DLG_CHAIN_GUIDELINES_PURCHASED = "triviaCorrectGuidelinesDialog";
        public const string DLG_CHAIN_HELP = "helpingDialog";
        public const string DLG_CHAIN_INCORRECT_TRIVIA = "incorrectTriviaDialog";
        public const string DLG_CHAIN_PIT_CORRECT = "pitTriviaCorrectDialog";
        public const string DLG_CHAIN_PIT_INCORRECT = "pitTriviaIncorrectDialog";
        public const string DLG_CHAIN_PIT_TRIVIA = "pitTriviaDialog";
        public const string DLG_CHAIN_SECRET_PURCHASED = "triviaCorrectSecretDialog";
        public const string DLG_CHAIN_SORRY = "orbletSorryDialog";
        public const string ID = "helper orblet";

        // Numeric Constants.
        private const int ARROW_INC_AMOUNT = 3;

        private const string DLG_EVENT_BUY_ARROW = "buy arrow";

        private const string DLG_EVENT_BUY_GUIDELINES = "buy guidelines";

        // Dialog fired events.
        private const string DLG_EVENT_BUY_SECRET = "buy secret";

        private bool b_summoned;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelperOrblet"/> class.
        /// </summary>
        public HelperOrblet()
        {
            b_summoned = false;
        }

        /// <summary>
        /// Gets the dismissed position.
        /// </summary>
        /// <value>
        /// The dismissed position.
        /// </value>
        public static Vector3 DismissedPosition
        {
            get { return new Vector3(0f, 100f, 0f); }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="HelperOrblet"/> is summoned.
        /// </summary>
        /// <value>
        ///   <c>true</c> if summoned; otherwise, <c>false</c>.
        /// </value>
        public bool Summoned
        {
            get { return b_summoned; }
        }

        /// <summary>
        /// Dismisses the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Dismiss(XnaGame game)
        {
            b_summoned = false;
            f_bobbleMidline = DismissedPosition.Y;

            var lookAtProcs = from proc in game.GameSystem.ProcessMgr.GameProcesses
                              where proc is BaseLogic.Process.CameraRotationProcess
                              select proc;
            foreach (var lookAtProc in lookAtProcs)
                lookAtProc.Kill = true;
        }

        /// <summary>
        /// Speaks the with player.
        /// </summary>
        /// <param name="userPlayer">The user player.</param>
        /// <param name="dialogChainName">Name of the dialog chain.</param>
        /// <param name="roomPos">The room position.</param>
        /// <param name="onEventStrFired">The on event string fired.</param>
        public void SpeakWithPlayer(GameUserPlayer userPlayer, string dialogChainName, Vector3 roomPos, Action<string> onEventStrFired = null)
        {
            Vector3 spawnPos = roomPos;
            spawnPos.Y = 0f;
            f_bobbleMidline = spawnPos.Y;
            this.Position = spawnPos;
            b_summoned = true;

            XnaGame.Game_Instance.DisplayDialog(DIALOG_FILE, dialogChainName, onEventStrFired, false);
        }

        /// <summary>
        /// Summons the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Summon(XnaGame game)
        {
            Map.MapMgr mapMgr = game.MapMgr;
            GameUserPlayer userPlayer = game.GetGameUserPlayer();
            int playerRoomIndex = mapMgr.GetRoomNumOf(userPlayer) - 1;

            Vector3 roomPos = mapMgr.GetRoomPos(playerRoomIndex);
            f_bobbleMidline = roomPos.Y + 5f;
            Vector3 orbletPos = roomPos;
            orbletPos.Y = f_bobbleMidline;
            this.Position = orbletPos;

            if (mapMgr.RoomContainsHazards(playerRoomIndex, game.OrbletMgr))
            {
                game.DisplayDialog(DIALOG_FILE, DLG_CHAIN_SORRY);
                return;
            }

            b_summoned = true;

            Vector2 userPlayerPos = new Vector2(userPlayer.Position.X, userPlayer.Position.Z);
            Vector2 thisPos = new Vector2(Position.X, Position.Z);

            if (Vector2.Distance(thisPos, userPlayerPos) < 5f)
            {
                userPlayer.Position = new Vector3(userPlayer.Position.X, userPlayer.Position.Y, userPlayer.Position.Z + 5f);
                var camPos = XnaGame.Game_Instance.Camera.Position;
                XnaGame.Game_Instance.Camera.Position = new Vector3(camPos.X, camPos.Y, camPos.Z + 5f);
            }

            game.DisplayDialog(DIALOG_FILE, DLG_CHAIN_HELP, (string eventFiredID) =>
                {
                    if (eventFiredID == DLG_EVENT_BUY_SECRET)
                    {
                        game.DisplayTriviaQuestion(3, (int questionsCorrect) =>
                            {
                                if (questionsCorrect >= 2)
                                {
                                    game.DisplayDialog(DIALOG_FILE, DLG_CHAIN_SECRET_PURCHASED, (string eventFiredId2) =>
                                        {
                                            if (eventFiredId2 == "dialog finished")
                                            {
                                                // Now display the secret.
                                                string secretMsgStr = game.GetGameSecretStr();
                                                game.DisplayDialogMsg(secretMsgStr, ActorID);
                                            }
                                        });
                                }
                                else
                                {
                                    game.DisplayDialog(DIALOG_FILE, DLG_CHAIN_INCORRECT_TRIVIA);
                                }
                                Dismiss(game);
                            });
                    }
                    else if (eventFiredID == DLG_EVENT_BUY_ARROW)
                    {
                        game.DisplayTriviaQuestion(3, (int questionsCorrect) =>
                            {
                                if (questionsCorrect >= 2)
                                {
                                    game.DisplayDialog(DIALOG_FILE, DLG_CHAIN_ARROWS_PURCHASED);
                                    userPlayer.WeaponObj.TotalAmmo += ARROW_INC_AMOUNT;
                                }
                                else
                                {
                                    game.DisplayDialog(DIALOG_FILE, DLG_CHAIN_INCORRECT_TRIVIA);
                                }
                                Dismiss(game);
                            });
                    }
                    else if (eventFiredID == DLG_EVENT_BUY_GUIDELINES)
                    {
                        game.DisplayTriviaQuestion(7, (int questionsCorrect) =>
                            {
                                if (questionsCorrect >= 5)
                                {
                                    game.MapMgr.IsDisplayingGuidelines = true;
                                    game.DisplayDialog(DIALOG_FILE, DLG_CHAIN_GUIDELINES_PURCHASED);
                                    BaseLogic.Process.WaitEventProcess waitEventProc = new BaseLogic.Process.WaitEventProcess(120 * 1000, () =>
                                        {
                                            XnaGame.Game_Instance.MapMgr.IsDisplayingGuidelines = false;
                                        });
                                    game.MapMgr.DisableGuidelinesProc = waitEventProc;
                                    game.GameSystem.AddGameProcess(waitEventProc);
                                }
                                else
                                {
                                    game.DisplayDialog(DIALOG_FILE, DLG_CHAIN_INCORRECT_TRIVIA);
                                }
                                Dismiss(game);
                            });
                    }
                });
        }

        /// <summary>
        /// Toggles the summon.
        /// </summary>
        /// <param name="game">The game.</param>
        public void ToggleSummon(XnaGame game)
        {
            if (Summoned)
                Dismiss(game);
            else
                Summon(game);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="userPlayer">The user player.</param>
        public override void Update(GameTime gameTime, GameUserPlayer userPlayer)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            const float driftUpSpeed = 2f;

            float ds = driftUpSpeed * dt;

            if (Position.Y < GetMinY())
            {
                Position = new Vector3(Position.X, Position.Y + ds, Position.Z);
            }
            else
            {
                base.Update(gameTime, userPlayer);
            }
        }
    }

    /// <summary>
    /// A general orblet.
    /// </summary>
    public class Orblet : StaticObj
    {
        protected float f_bobbleAmplitude = 1f;
        protected float f_bobbleMidline = 5f;

        /// <summary>
        /// Initializes a new instance of the <see cref="Orblet"/> class.
        /// </summary>
        public Orblet()
            : base(Guid.NewGuid().ToString())
        {
            SerilizeObj = false;
        }

        /// <summary>
        /// Gets or sets the bobble amplitude.
        /// </summary>
        /// <value>
        /// The bobble amplitude.
        /// </value>
        public float BobbleAmplitude
        {
            get { return f_bobbleAmplitude; }
            set { f_bobbleAmplitude = value; }
        }

        /// <summary>
        /// Gets or sets the bobble midline.
        /// </summary>
        /// <value>
        /// The bobble midline.
        /// </value>
        public float BobbleMidline
        {
            get { return f_bobbleMidline; }
            set { f_bobbleMidline = value; }
        }

        /// <summary>
        /// Gets the minimum y.
        /// </summary>
        /// <returns></returns>
        public float GetMinY()
        {
            float y = f_bobbleAmplitude * (float)Math.Sin(MathHelper.Pi + MathHelper.PiOver2);
            return y + f_bobbleMidline;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="userPlayer">The user player.</param>
        public virtual void Update(GameTime gameTime, GameUserPlayer userPlayer)
        {
            Update(gameTime);

            float y = f_bobbleAmplitude * (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds);
            y += f_bobbleMidline;

            var pos = Position;
            Position = new Vector3(pos.X, y, pos.Z);
        }
    }

    /// <summary>
    /// Orblet which rests in the room and teleports the user upon entering.
    /// </summary>
    public class TeleportationOrblet : Orblet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeleportationOrblet"/> class.
        /// </summary>
        public TeleportationOrblet()
        {
        }

        /// <summary>
        /// Repositions the in random room.
        /// </summary>
        /// <param name="mapMgr">The map MGR.</param>
        public void RepositionInRandomRoom(Map.MapMgr mapMgr)
        {
            int safeIndex = mapMgr.GetRandomSafeEmptyIndex(XnaGame.Game_Instance.OrbletMgr);
            Vector3 safePos = mapMgr.GetRoomPos(safeIndex);
            this.Position = new Vector3(safePos.X, Position.Y, safePos.Z);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="userPlayer">The user player.</param>
        public override void Update(GameTime gameTime, GameUserPlayer userPlayer)
        {
            base.Update(gameTime, userPlayer);

            if (userPlayer.IsTeleporting)
                return;

            Map.MapMgr mapMgr = XnaGame.Game_Instance.MapMgr;

            this.Enabled = true;

            int playerRoomIndex = mapMgr.GetRoomNumOf(userPlayer);
            int orbletRoomIndex = mapMgr.GetRoomNumOf(this);

            if (playerRoomIndex == orbletRoomIndex)
            {
                int safeIndex = mapMgr.GetRandomSafeEmptyIndex(XnaGame.Game_Instance.OrbletMgr);
                Vector3 safePos = mapMgr.GetRoomPos(safeIndex);
                safePos.Y = userPlayer.Position.Y;
                userPlayer.StartTeleportation(safePos, this);
            }
        }
    }
}