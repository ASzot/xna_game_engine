#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using BaseLogic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RenderingSystem;

namespace My_Xna_Game.UI
{
    /// <summary>
    /// If it is the users first time playing this frame will run. This can display an intro movie and some
    /// images for credits or something.
    /// </summary>
    internal class StartMsgUIFrame : UIFrame
    {
        public const string FRAME_ID = "start msg";
        // This is the duration the image is displayed.
        //public const uint MILLISECONDS_DISPLAYED = 100;

        public static bool HasRan = false;

        // This is a image displayed at the begining.
        //private GameTexture _msgTex;

        private Video _introVideo;
        private VideoPlayer _player;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartMsgUIFrame"/> class.
        /// </summary>
        public StartMsgUIFrame()
            : base(FRAME_ID, true, true, false)
        {
            // If you want to display a intro credits screen put it here and uncomment

            //GameSystem.GameSys_Instance.ProcessMgr.AddProcess(new BaseLogic.Process.WaitEventProcess(MILLISECONDS_DISPLAYED, () =>
            //{
            //    _msgTex = new GameTexture("images/UI/GraphicsPoweredBy");
            //    GameSystem.GameSys_Instance.ProcessMgr.AddProcess(new BaseLogic.Process.WaitEventProcess(MILLISECONDS_DISPLAYED, () =>
            //        {
            //            this.s_moveToFrame = MenuUIFrame.FRAME_ID;
            //        }));
            //}));

            //_msgTex = new GameTexture("images/UI/PhysicsPoweredBy");

            _introVideo = ResourceMgr.Content.Load<Video>("videos/IntroSequence");

            _player = new VideoPlayer();
            _player.Play(_introVideo);
        }

        /// <summary>
        /// Draws the frame.
        /// </summary>
        /// <param name="textRenderer">The text renderer.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="game">The game.</param>
        public override void DrawFrame(TextRenderer textRenderer, int width, int height, XnaGame game)
        {
            HasRan = true;

            if (_player.State != MediaState.Stopped)
            {
                Texture2D videoTex;
                videoTex = _player.GetTexture();
                textRenderer.RenderTexture(videoTex, width, height, 0, 0);
            }

            //float fWidth = (float)width;
            //float fHeight = (float)height;
            //Vector2 scalingFact = GetScalingFactor(fWidth, fHeight);

            //textRenderer.RenderTexture(_msgTex.Texture, width, height, 0, 0);
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            //_msgTex.Unload();
        }

        /// <summary>
        /// Updates the frame.
        /// </summary>
        public override void UpdateFrame()
        {
            if (_player.State == MediaState.Stopped)
            {
                this.s_moveToFrame = MenuUIFrame.FRAME_ID;
            }
        }
    }
}