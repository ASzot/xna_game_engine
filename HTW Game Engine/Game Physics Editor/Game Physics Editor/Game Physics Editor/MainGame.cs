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
using System.IO;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Henge3D;
using Henge3D.Physics;

namespace Game_Physics_Editor
{
    /// <summary>
    /// The main game responsible for rendering everything to the screen, 
    /// managing user interaction through the camera, user input, and the 
    /// bounding box data.
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private LineRenderer _lineRenderer;

        private Camera _cam;
        private Input _input;

        /// <summary>
        /// The model filename of the model the user has opened.
        /// </summary>
        private string s_modelFilename;

        /// <summary>
        /// The save folder for the physics data.
        /// </summary>
        private string s_saveLocation;
        private Model _model;
        private bool b_renderWireframe = false;
        private RasterizerState _wireframe;

        private List<BoundingBox> _gameBBs = new List<BoundingBox>();

        public List<BoundingBox> GameBBs
        {
            get { return _gameBBs; }
            set { _gameBBs = value; }
        }

        public bool RenderWireFrame
        {
            get { return b_renderWireframe; }
            set { b_renderWireframe = value; }
        }


        public MainGame(string modelFilename, string saveLocation)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.SynchronizeWithVerticalRetrace = true;

            s_modelFilename = modelFilename;
            s_saveLocation = saveLocation;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _cam = new Camera();
            _cam.SetDefault(GraphicsDevice.Viewport);
            _cam.Pitch = MathHelper.PiOver4;
            _cam.Yaw = 0f;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _model = Content.Load<Model>(s_modelFilename);

            _input = new Input(this);
            _lineRenderer = new LineRenderer();

            _wireframe = new RasterizerState();
            _wireframe.FillMode = FillMode.WireFrame;
        }

        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Save all of the physics bounding box data to the save location specified earlier by the user.
        /// </summary>
        /// <returns>Whether the save was successful.</returns>
        public bool SaveData()
        {
            string filename = Path.Combine(s_saveLocation, s_modelFilename + "_Physics.xml");
            try
            {
                // This writes out the data the same way the Wumpus Game Engine Reads in the data.
                using (FileStream fs = File.Create(filename))
                {
                    using (XmlWriter writer = XmlWriter.Create(fs))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("BoundingBoxes");

                        foreach (BoundingBox bb in _gameBBs)
                        {
                            writer.WriteStartElement("BoundingBox");

                            writer.WriteStartElement("Min");
                            writer.WriteElementString("X", bb.Min.X.ToString());
                            writer.WriteElementString("Y", bb.Min.Y.ToString());
                            writer.WriteElementString("Z", bb.Min.Z.ToString());
                            writer.WriteEndElement();

                            writer.WriteStartElement("Max");
                            writer.WriteElementString("X", bb.Max.X.ToString());
                            writer.WriteElementString("Y", bb.Max.Y.ToString());
                            writer.WriteElementString("Z", bb.Max.Z.ToString());
                            writer.WriteEndElement();

                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Update the camera movement and whether the user wants to create the physics editor form.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector3 moveVec = Vector3.Zero;

            // Camera movement.
            if (_input.KeyboardState.IsKeyDown(Keys.W))
                moveVec += Vector3.UnitZ;
            if (_input.KeyboardState.IsKeyDown(Keys.A))
                moveVec += Vector3.UnitX;
            if (_input.KeyboardState.IsKeyDown(Keys.D))
                moveVec += -Vector3.UnitX;
            if (_input.KeyboardState.IsKeyDown(Keys.S))
                moveVec += -Vector3.UnitZ;
            if (_input.KeyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            // Form creation input.
            foreach (var key in _input.KeysPressed)
            {
                switch (key)
                {
                    case Keys.B:
                        PhysicsEditorForm pef = new PhysicsEditorForm(this);
                        pef.Show();
                        break;
                }
            }

            // Camera update.
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _cam.Move(moveVec, 10f, dt);

            _cam.Update(gameTime);

            Vector2 yawPitch = new Vector2(_input.MouseDelta.X, -_input.MouseDelta.Y);
            yawPitch *= _input.MouseSensitivity;
            if (yawPitch != Vector2.Zero)
            {
                _cam.Yaw -= yawPitch.X;
                _cam.Pitch += yawPitch.Y;
            }

            if (_input.MouseState.RightButton == ButtonState.Pressed)
                _input.CaptureMouse = true;
            else
                _input.CaptureMouse = false;
        }

        /// <summary>
        /// Draw the model and the bounding boxes to the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            // Changing this value will change the background color in the scene.
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // The light direction can also be changed for more pleasant viewing.
            Vector3 lightDir = new Vector3(-1f, -1f, -1f);
            lightDir.Normalize();

            base.Draw(gameTime);

            if (b_renderWireframe)
                GraphicsDevice.RasterizerState = _wireframe;
            else
                GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;

            // Render the mesh.
            foreach (var mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = Matrix.Identity;
                    effect.View = _cam.View;
                    effect.Projection = _cam.Proj;
                    effect.DiffuseColor = new Vector3(0.6f);
                    effect.EnableDefaultLighting();
                }

                mesh.Draw();
            }

            // Draw the bounding boxes.
            Vector3 color = Color.Red.ToVector3();
            foreach (BoundingBox bb in _gameBBs)
            {
                Vector3 mx = bb.Max;
                Vector3 mn = bb.Min;

                Vector3 p0 = mn;
                Vector3 p1 = new Vector3(mn.X, mn.Y, mx.Z);
                Vector3 p2 = new Vector3(mn.X, mx.Y, mn.Z);
                Vector3 p3 = new Vector3(mx.X, mn.Y, mn.Z);
                Vector3 p4 = mx;
                Vector3 p5 = new Vector3(mx.X, mx.Y, mn.Z);
                Vector3 p6 = new Vector3(mx.X, mn.Y, mx.Z);
                Vector3 p7 = new Vector3(mn.X, mx.Y, mx.Z);

                _lineRenderer.Draw(GraphicsDevice, p0, p1, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p0, p2, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p0, p3, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p1, p6, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p1, p7, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p2, p7, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p2, p5, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p3, p5, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p3, p6, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p4, p6, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p4, p7, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p4, p5, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p3, p5, _cam, color);
                _lineRenderer.Draw(GraphicsDevice, p0, p1, _cam, color);

            }
        }
    }
}
