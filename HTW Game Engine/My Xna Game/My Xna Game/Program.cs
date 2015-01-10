#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

namespace My_Xna_Game
{
    internal static class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        [STAThread]
        private static void Main(string[] args)
        {
            BaseLogic.Editor.Forms.Core.ResolutionSelectorForm resSelectorForm = new BaseLogic.Editor.Forms.Core.ResolutionSelectorForm();
            resSelectorForm.ShowDialog();

            HTW_Game.ScreenWidth = resSelectorForm.Width;
            HTW_Game.ScreenHeight = resSelectorForm.Height;
            HTW_Game.VSync = true;
            HTW_Game.MultiSample = true;

            Input.KeyboardInteractionKey = 'X';
            Input.XboxInteractionBtn = 'X';
            Input.CameraUpdateSpeed = 7.5f;

            BaseLogic.Player.UserPlayer.JUMP_POWER = 5f;

            using (HTW_Game game = new HTW_Game(false))
            {
                game.Run();
            }
        }
    }
}