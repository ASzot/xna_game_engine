#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseLogic
{
    /// <summary>
    ///
    /// </summary>
    public enum MouseButton
    {
        /// <summary>
        /// The left button
        /// </summary>
        LeftButton,

        /// <summary>
        /// The middle button
        /// </summary>
        MiddleButton,

        /// <summary>
        /// The right button
        /// </summary>
        RightButton,

        /// <summary>
        /// The x button1
        /// </summary>
        XButton1,

        /// <summary>
        /// The x button2
        /// </summary>
        XButton2
    }

    /// <summary>
    ///
    /// </summary>
    public interface IInput
    {
        /// <summary>
        /// Gets or sets a value indicating whether [capture mouse].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [capture mouse]; otherwise, <c>false</c>.
        /// </value>
        bool CaptureMouse { get; set; }

        /// <summary>
        /// Gets or sets the controller sensitivity.
        /// </summary>
        /// <value>
        /// The controller sensitivity.
        /// </value>
        float ControllerSensitivity { get; set; }

        /// <summary>
        /// Gets the state of the game pad.
        /// </summary>
        /// <value>
        /// The state of the game pad.
        /// </value>
        GamePadState GamePadState { get; }

        /// <summary>
        /// Gets the state of the keyboard.
        /// </summary>
        /// <value>
        /// The state of the keyboard.
        /// </value>
        KeyboardState KeyboardState { get; }

        /// <summary>
        /// Gets the keys pressed.
        /// </summary>
        /// <value>
        /// The keys pressed.
        /// </value>
        IEnumerable<Keys> KeysPressed { get; }

        /// <summary>
        /// Gets the mouse delta.
        /// </summary>
        /// <value>
        /// The mouse delta.
        /// </value>
        Vector2 MouseDelta { get; }

        /// <summary>
        /// Gets or sets the mouse sensitivity.
        /// </summary>
        /// <value>
        /// The mouse sensitivity.
        /// </value>
        float MouseSensitivity { get; set; }

        /// <summary>
        /// Gets the state of the mouse.
        /// </summary>
        /// <value>
        /// The state of the mouse.
        /// </value>
        MouseState MouseState { get; }

        /// <summary>
        /// Gets a value indicating whether [using game pad].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [using game pad]; otherwise, <c>false</c>.
        /// </value>
        bool UsingGamePad { get; }

        /// <summary>
        /// Gets if a button was pressed.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns></returns>
        bool WasPressed(Buttons button);

        /// <summary>
        /// Gets if a mouse button was pressed.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns></returns>
        bool WasPressed(MouseButton button);
    }
}