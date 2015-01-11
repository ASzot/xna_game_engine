#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;

namespace Game_Physics_Editor
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // Prompt the user to enter the model load file and the 
            // physics data save location.
            ModelSelectorClass msc = new ModelSelectorClass();
            string modelName = "";
            string saveLocation = "";
            msc.OnSubmission += (string mnStr, string slStr) =>
            {
                // Fired when the user accepted the information.
                modelName = mnStr;
                saveLocation = slStr;
            };
            msc.ShowDialog();

            using (MainGame game = new MainGame(modelName, saveLocation))
            {
                game.Run();
            }
        }
    }
#endif
}

