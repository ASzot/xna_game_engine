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
using BaseLogic.Camera;
using BaseLogic.Object;
using BaseLogic.Player;
using Microsoft.Xna.Framework;
using My_Xna_Game.Game_Objects;
using RenderingSystem;

namespace My_Xna_Game
{

    internal class HTW_Game : XnaGame
    {
        private bool _useDebugCamera;

        public static int ScreenWidth;
        public static int ScreenHeight;
        public static bool MultiSample = true;
        public static bool VSync = true;

        public HTW_Game(bool useDebugCamera)
        {
            _useDebugCamera = useDebugCamera;
        }


        /// <summary>
        /// Creates the test scene.
        /// Used for Debugging purposes.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void CreateTestScene(GameTime gameTime)
        {

        }

        /// <summary>
        /// Gets the high scores.
        /// </summary>
        /// <returns></returns>
        public override string[] GetHighScores()
        {
            // Get the highscores and format the highscores converting them to a array of strings.
            var highscores = _highscoreMgr.GetHighScores();
            var highscoreStrs = from highscore in highscores
                                select highscore.ToString();

            return highscoreStrs.ToArray();
        }

        /// <summary>
        /// Draws the screen. This is called everyframe. As it is drawing the frame itself...
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Set this if you want the debug text to always be rendered in debug mode.
            // I personally find the text to be annoying. It can always be toggled in the 
            // scene settings form. However, if the next line is uncommented it cannot be 
            // toggled in the scene settings form.

            // Only have the render text (which is like the debug text) if we are in edit mode.
            //(_gameSystem.Renderer as RendererAccess).RenderText = DebugMode;
            
            // XnaGame is in charge of all the drawing.
            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets the setup camera.
        /// </summary>
        /// <returns></returns>
        protected override FreeLookCamera GetCamera()
        {
            if (_useDebugCamera)
            {
                FreeLookCamera cam = (FreeLookCamera)_gameSystem.CreateCamera(new FreeLookCamera());
                cam.Position = new Vector3(0f, 5f, 0f);
                cam.Yaw = MathHelper.PiOver2;
                return cam;
            }
            else
            {
                FirstPersonCamera cam = (FirstPersonCamera)_gameSystem.CreateCamera(new FirstPersonCamera(UserPlayer));
                cam.EyeOffset = new Vector3(0f, 3f, 2f);
                return cam;
            }
        }

        /// <summary>
        /// Gets the graphics settings.
        /// </summary>
        /// <returns></returns>
        protected override GraphicsSettings GetGraphicsSettings()
        {
			// Static graphics settings can be set here too.
            return new GraphicsSettings(ScreenWidth, ScreenHeight, MultiSample, VSync);
        }

        /// <summary>
        /// Gets the player weapon object.
        /// </summary>
        /// <returns></returns>
        protected override WeaponObj GetPlayerWeaponObj()
        {
            StaticObj bolt = new StaticObj(Guid.NewGuid().ToString());
            bolt.LoadContent(Content, "Models/bolt");

            bolt.SubsetMaterials[0].DiffuseMap = ResourceMgr.LoadGameTexture("DiffuseMaps/CrossbowDiffuse2");

            BaseLogic.Object.Holdable.ProjectileWeaponObj crossbow =
                new BaseLogic.Object.Holdable.ProjectileWeaponObj(UserPlayer, bolt, 50f, 100, 4f,
                    Quaternion.CreateFromYawPitchRoll(0f, 0f, 0f));
            crossbow.FireRate = TimeSpan.FromSeconds(2.83);
            crossbow.Automatic = false;
            crossbow.FireOffset = new Vector3(0f, 0f, 10f);
            crossbow.TotalAmmo = 10;
            crossbow.ClipSize = crossbow.CurrentAmmo = 1;
            crossbow.Damage = 5f;

            return crossbow;
        }

        /// <summary>
        /// Gets the random trivia question.
        /// </summary>
        /// <returns></returns>
        protected override Trivia.TriviaQuestion GetRndTriviaQuestion()
        {
            Trivia.TriviaQuestion triviaQuestion = _triviaMgr.GetRandomTriviaQuestion();

            return triviaQuestion;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// Called when the scene is selected and loaded.
        /// </summary>
        /// <param name="loadedFilename">The loaded filename.</param>
        protected override void OnSceneLoad(string loadedFilename)
        {
            base.OnSceneLoad(loadedFilename);

            // Set up the water beneath all the rooms.
            WaterObject waterObj = new WaterObject("wa wa");
            waterObj.Position = new Vector3(-60f, -4f, 50f);
            waterObj.TexScaleX = 4f;
            waterObj.TexScaleY = 4f;
            waterObj.WaveLength = 0.6f;
            waterObj.WaveHeight = 0.2f;
            waterObj.TransSpeed = 0.001f;
            waterObj.Scale = 200f;
            _gameSystem.AddRenderObj(waterObj);

            // Create the wumpus player setting the attack damage and health.
            if (_shouldLoadHTWGameComps)
            {
                // Create the wumpus's animated object itself.
                AnimatedObj animObj2 = _gameSystem.CreateAnimatedObjAbsoluteFilename("Models/Wumpus/wumpus", "dude actor", "ArmatureAction",
                    new Vector3(-0.5f, 0f, -1f), new Vector3(0.5f, 6f, 1f));

                // Load the wumpus's material informatio.
                animObj2.SubsetMaterials[0].DiffuseMap = ResourceMgr.LoadGameTextureAbsoluteFilename("Models/Wumpus/act_bibliotekar");
                animObj2.SubsetMaterials[0].NormalMap = ResourceMgr.LoadGameTextureAbsoluteFilename("Models/Wumpus/act_bibliotekar_norm");

                // Set rotations and scales appropriately.
                animObj2.Scale = 1.5f;
                animObj2.AppliedRot = Quaternion.CreateFromYawPitchRoll(0f, -MathHelper.PiOver2, MathHelper.Pi);
                animObj2.Position = Vector3.Zero;
                animObj2.Rotation = Quaternion.CreateFromYawPitchRoll(MathHelper.Pi, 0, 0);
                animObj2.SerilizeObj = false;

                WumpusGameObj wumpusGameObj = new WumpusGameObj(animObj2, _mapMgr);
                wumpusGameObj.Health = wumpusGameObj.MaxHealth = 60;
                wumpusGameObj.AttackDamage = 5f;

                _gameSystem.PlayerMgr.AddToList(wumpusGameObj);

                // Spawn the wumpus and player in the location specified in the map file. ( Which was already loaded)
                _mapMgr.SpawnPlayer(this);
                _mapMgr.SpawnWumpus(this);

                // Spawn the orblets in the locations specified in the map file.
                _orbletMgr.SpawnOrblet(HelperOrblet.DismissedPosition, new HelperOrblet());

                // Determines if there is culling in debug mode.
                _mapMgr.AlwaysCull = false;
            }
            

            // Set the health of the player.
            UserPlayer.Health = UserPlayer.MaxHealth = 100;
        }

        /// <summary>
        /// Updates the entire game.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // All the updating is in XnaGame.
            base.Update(gameTime);
        }
    }
}