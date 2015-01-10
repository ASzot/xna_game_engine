#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using BaseLogic;
using BaseLogic.Player;
using Microsoft.Xna.Framework;
using RenderingSystem;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    internal class PlayerUIFrame : UIFrame
    {
        public const string FRAME_ID = "player ui";

        private GameTexture _arrowUnitTex;
        private GameTexture _crosshairsTex;
        private GameTexture _flashlightTex;
        private GameTexture _goldUnitTex;
        private GameTexture _healthBarOutlineTex;
        private GameTexture _radarBleepTex;
        private GameTexture _radarTex;
        private GameTexture _solidColorTex;
        private float f_pixelsPerUnits = 0f;
        private Vector2 v_radarTexCenter = Vector2.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUIFrame"/> class.
        /// </summary>
        /// <param name="exclusiveRender">if set to <c>true</c> [exclusive render].</param>
        /// <param name="exclusiveInput">if set to <c>true</c> [exclusive input].</param>
        /// <param name="exclusiveUpdate">if set to <c>true</c> [exclusive update].</param>
        public PlayerUIFrame(bool exclusiveRender, bool exclusiveInput, bool exclusiveUpdate)
            : base(FRAME_ID, exclusiveRender, exclusiveInput, exclusiveUpdate)
        {
            _radarTex = new GameTexture("images/UI/GameRadar");
            _radarBleepTex = new GameTexture("images/UI/RadarBleep");
            _arrowUnitTex = new GameTexture("images/UI/ArrowUnit");
            _goldUnitTex = new GameTexture("images/UI/GoldUnit");
            _solidColorTex = new GameTexture("images/UI/SolidColor");
            _healthBarOutlineTex = new GameTexture("images/UI/HealthBarOutline");
            _crosshairsTex = new GameTexture("images/UI/crosshairs");
            _flashlightTex = new GameTexture("images/UI/FlashlightSymbol");

            f_pixelsPerUnits = 26f / Map.MapMgr.HEX_APEX;

            v_radarTexCenter = new Vector2(0.5f * (float)_radarTex.Texture.Width,
                0.5f * (float)_radarTex.Texture.Height);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerUIFrame"/> class.
        /// </summary>
        public PlayerUIFrame()
            : this(false, false, false)
        {
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
            if (game.MapMgr == null && game.ShouldLoadHTWGameComps)
            {
                DrawLoadingFrame(textRenderer, width, height, game);
                return;
            }

            if (game.DebugMode)
                return;

            string inputHint = null;
            if (game.MapMgr != null)
                inputHint = game.MapMgr.GetButtonHint(game.Input, game);

            float fWidth = (float)width;
            float fHeight = (float)height;
            Vector2 scalingFactor = GetScalingFactor(fWidth, fHeight);
            Vector2 centerPos = new Vector2(fWidth / 2f, fHeight / 2f);
            float txtWidth;
            float txtHeight;
            Vector2 renderPos;
            const float txtSize = 0.5f;
            Vector2 txtScalingFactor = scalingFactor * txtSize;

            textRenderer.SetRenderFont(TextType.UI);

            if (inputHint != null)
            {
                txtWidth = textRenderer.GetLengthOfStr(inputHint) * txtScalingFactor.X;
                txtHeight = textRenderer.GetHeightOfStr(inputHint) * txtScalingFactor.Y;

                renderPos = centerPos;
                renderPos.Y = 100f * scalingFactor.Y;
                renderPos -= new Vector2(txtWidth / 2f, txtHeight / 2f);
                textRenderer.RenderText(inputHint, renderPos, Color.Blue, txtScalingFactor);
            }

            var userPlayer = game.GetGameUserPlayer();
            if (userPlayer == null)
                return;

            Vector3 userPos = game.Camera.Position;
            Vector2 gameProjCoords = new Vector2(-userPos.Z, userPos.X);
            Vector2 radarProjCoordsFromCenter = gameProjCoords * f_pixelsPerUnits;
            Vector2 radarProjCoords = radarProjCoordsFromCenter + new Vector2(165, 162);

            Vector2 samplingSize = new Vector2(200f, 200f);

            Rectangle sourceRectangle = new Rectangle((int)(radarProjCoords.X - (samplingSize.X / 2f)),
                (int)(radarProjCoords.Y - (samplingSize.Y / 2f)),
                (int)samplingSize.X, (int)samplingSize.Y);

            const float actualRadarTexWidth = 300f;
            const float actualRadarTexHeight = 300f;

            float destRectangleWidth = scalingFactor.X * actualRadarTexWidth;
            float destRectangleHeight = scalingFactor.Y * actualRadarTexHeight;

            float x = fWidth - destRectangleWidth;
            float y = 0f;

            Rectangle destRectangle = new Rectangle((int)x, (int)y, (int)destRectangleWidth, (int)destRectangleHeight);

            textRenderer.RenderTexture(_radarTex.Texture, sourceRectangle, destRectangle, Color.White);

            const float radarBleepActualWidth = 20f;
            const float radarBleepActualHeight = 20f;
            float radarBleepWidth = radarBleepActualWidth * scalingFactor.X;
            float radarBleepHeight = radarBleepActualHeight * scalingFactor.Y;

            Vector2 radarBleepScaling = new Vector2(radarBleepActualWidth / (float)_radarBleepTex.Texture.Width,
                radarBleepActualHeight / (float)_radarBleepTex.Texture.Height);

            x = fWidth - (destRectangleWidth / 2f) - (radarBleepWidth - 10f * scalingFactor.X);
            y = (destRectangleHeight / 2f) - (radarBleepHeight - 10f * scalingFactor.Y);

            textRenderer.RenderTexture(_radarBleepTex.Texture, (int)radarBleepWidth, (int)radarBleepHeight, (int)x, (int)y);

            // Player ammunition.
            int totalAmmo = userPlayer.WeaponObj.TotalAmmo;

            string ammoStr = totalAmmo.ToString();
            string goldStr = userPlayer.GoldCount.ToString();

            float ammoStrWidth = textRenderer.GetLengthOfStr(ammoStr) * txtScalingFactor.X;
            float ammoStrHeight = textRenderer.GetHeightOfStr(ammoStr) * txtScalingFactor.Y;

            float goldStrWidth = textRenderer.GetLengthOfStr(goldStr) * txtScalingFactor.X;
            float goldStrHeight = textRenderer.GetHeightOfStr(goldStr) * txtScalingFactor.Y;

            const float crossbowUnitActualWidth = 200f;
            const float crossbowUnitActualHeight = 200f;
            const float goldUnitActualWidth = 200f;
            const float goldUnitActualHeight = 200f;
            const float flashUnitActualWidth = 100f;
            const float flashUnitActualHeight = 100f;

            float crossbowUnitWidth = crossbowUnitActualWidth * scalingFactor.X;
            float crossbowUnitHeight = crossbowUnitActualHeight * scalingFactor.Y;
            float goldUnitWidth = goldUnitActualWidth * scalingFactor.X;
            float goldUnitHeight = goldUnitActualHeight * scalingFactor.Y;

            float flashUnitWidth = flashUnitActualWidth * scalingFactor.X;
            float flashUnitHeight = flashUnitActualHeight * scalingFactor.Y;

            Vector2 backingPos = Vector2.Zero;
            float backingWidth = Math.Max(goldUnitWidth + goldStrWidth, crossbowUnitWidth + ammoStrWidth);
            float backingHeight = goldUnitHeight + crossbowUnitHeight;

            if (userPlayer.UsingFlashLight)
            {
                backingHeight += flashUnitHeight;
            }

            backingPos.X = fWidth - backingWidth;
            backingPos.Y = fHeight - backingHeight;

            textRenderer.RenderTexture(_solidColorTex.Texture, (int)backingWidth, (int)backingHeight, (int)backingPos.X, (int)backingPos.Y, UIBackingColor);

            float rightMargin = 10f * scalingFactor.X;

            x = fWidth - (crossbowUnitWidth + ammoStrWidth + rightMargin);
            y = fHeight - (crossbowUnitHeight);

            textRenderer.RenderTexture(_arrowUnitTex.Texture, (int)crossbowUnitWidth, (int)crossbowUnitHeight, (int)x, (int)y);

            float arrowCountMarginDown = 100f * txtScalingFactor.Y;
            y = fHeight - arrowCountMarginDown - ammoStrHeight;

            float marginLeft = 20f * scalingFactor.X;
            x += crossbowUnitWidth - marginLeft;
            textRenderer.RenderText(ammoStr, new Vector2(x, y), Color.White, txtScalingFactor);

            // Player Gold.

            x = fWidth - (goldUnitWidth + goldStrWidth + rightMargin);
            y = fHeight - (crossbowUnitHeight + goldUnitHeight);

            textRenderer.RenderTexture(_goldUnitTex.Texture, (int)goldUnitWidth, (int)goldUnitHeight, (int)x, (int)y);

            float goldCountMarginDown = arrowCountMarginDown + goldUnitHeight;

            y = fHeight - goldCountMarginDown - goldStrHeight;
            x += goldUnitWidth - marginLeft;

            textRenderer.RenderText(goldStr, new Vector2(x, y), Color.White, txtScalingFactor);

            if (userPlayer.UsingFlashLight)
            {
                y -= (20f * scalingFactor.Y) + flashUnitHeight;
                x = fWidth - (flashUnitWidth + rightMargin);

                textRenderer.RenderTexture(_flashlightTex.Texture, (int)flashUnitWidth, (int)flashUnitHeight, (int)x, (int)y, Color.White);
            }

            float health;
            float maxHealth;

            // Wumpus health bar.
            GamePlayer gamePlayer = GameSystem.GameSys_Instance.PlayerMgr.GetPlayerOfId("wumpus");
            if (gamePlayer == null)
            {
                health = maxHealth = 1f;
            }
            else
            {
                health = gamePlayer.Health;
                maxHealth = gamePlayer.MaxHealth;
            }

            float filledPercent = health / maxHealth;

            const float healthBarActualWidth = 500f;
            const float healthBarActualHeight = 20f;

            float healthBarWidth = healthBarActualWidth * scalingFactor.X;
            float healthBarHeight = healthBarActualHeight * scalingFactor.Y;

            float topMargin = 20f * scalingFactor.Y;

            x = 20f * scalingFactor.X;
            y = topMargin;

            float barWidth = healthBarWidth * filledPercent;
            float barHeight = healthBarHeight;

            textRenderer.RenderTexture(_solidColorTex.Texture, (int)barWidth, (int)barHeight, (int)x, (int)y, Color.Red);

            textRenderer.RenderTexture(_healthBarOutlineTex.Texture, (int)healthBarWidth, (int)healthBarHeight, (int)x, (int)y);

            // Player health
            filledPercent = userPlayer.Health / userPlayer.MaxHealth;

            marginLeft = 10f * scalingFactor.X;
            float bottomMargin = 5f * scalingFactor.Y;

            barWidth = healthBarWidth * filledPercent;

            x = marginLeft;
            y = fHeight - bottomMargin - barHeight;

            string playerHealthLabelStr = "Player Health";
            const float lblSize = 0.5f;
            txtHeight = textRenderer.GetHeightOfStr(playerHealthLabelStr) * txtScalingFactor.Y * lblSize;
            Vector2 playerHealthLblPos = new Vector2(x, y - txtHeight);

            backingPos = playerHealthLblPos;
            backingPos.X = 0f;
            backingPos.Y -= txtHeight / 4f;
            backingWidth = healthBarWidth + 40f * scalingFactor.X;
            backingHeight = fHeight - backingPos.Y;

            textRenderer.RenderTexture(_solidColorTex.Texture, (int)backingWidth, (int)backingHeight, (int)backingPos.X, (int)backingPos.Y, UIBackingColor);
            textRenderer.RenderText(playerHealthLabelStr, playerHealthLblPos, Color.White, txtScalingFactor * lblSize);
            textRenderer.RenderTexture(_solidColorTex.Texture, (int)barWidth, (int)barHeight, (int)x, (int)y, Color.Green);
            textRenderer.RenderTexture(_healthBarOutlineTex.Texture, (int)healthBarWidth, (int)healthBarHeight, (int)x, (int)y, Color.Green);

            // Crosshairs.
            const float crosshairActualWidth = 100f;
            const float crosshairActualHeight = 100f;

            float crosshairWidth = crosshairActualWidth * scalingFactor.X;
            float crosshairHeight = crosshairActualHeight * scalingFactor.Y;

            textRenderer.RenderTexture(_crosshairsTex.Texture, (int)crosshairWidth, (int)crosshairHeight,
                (int)centerPos.X, (int)centerPos.Y);

            // Guideines timer text.
            if (game.MapMgr != null && game.MapMgr.DisableGuidelinesProc != null)
            {
                uint currentTime = game.MapMgr.DisableGuidelinesProc.CurrentWait;
                uint totalTime = game.MapMgr.DisableGuidelinesProc.WaitTime;

                uint milliRemaining = totalTime - currentTime;

                TimeSpan timeRemaining = TimeSpan.FromMilliseconds(milliRemaining);

                string timeRemainingStr = String.Format("{0}:{1:00}", timeRemaining.Minutes, timeRemaining.Seconds);

                txtWidth = textRenderer.GetLengthOfStr(timeRemainingStr) * txtScalingFactor.X;
                txtHeight = textRenderer.GetHeightOfStr(timeRemainingStr) * txtScalingFactor.Y;

                renderPos = new Vector2(fWidth / 2f, fHeight - txtHeight);
                renderPos.X -= (txtWidth / 2f);

                textRenderer.RenderText(timeRemainingStr, renderPos, Color.Red, txtScalingFactor);
            }
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            _arrowUnitTex.Unload();
            _crosshairsTex.Unload();
            _goldUnitTex.Unload();
            _healthBarOutlineTex.Unload();
            _solidColorTex.Unload();
            _radarBleepTex.Unload();
            _radarTex.Unload();
            _flashlightTex.Unload();
        }

        /// <summary>
        /// Draws the loading frame.
        /// </summary>
        /// <param name="textRenderer">The text renderer.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="game">The game.</param>
        private void DrawLoadingFrame(TextRenderer textRenderer, int width, int height, XnaGame game)
        {
            float fWidth = (float)width;
            float fHeight = (float)height;
            Vector2 scalingFactor = GetScalingFactor(fWidth, fHeight);

            textRenderer.RenderTexture(_solidColorTex.Texture, width, height, 0, 0, Color.Black);

            Vector2 txtRenderPos = new Vector2(fWidth / 2f, fHeight / 2f);
            string loadingStr = "Loading";
            float strLength = textRenderer.GetLengthOfStr(loadingStr) * scalingFactor.X;
            float strHeight = textRenderer.GetHeightOfStr(loadingStr) * scalingFactor.Y;
            txtRenderPos.X -= strLength / 2f;
            txtRenderPos.Y -= strHeight / 2f;

            textRenderer.RenderText("Loading", txtRenderPos, Color.White, scalingFactor);
        }
    }
}