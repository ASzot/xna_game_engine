#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;

using BaseLogic.Editor.Forms;

namespace BaseLogic.Manager
{
    /// <summary>
    ///
    /// </summary>
    public enum EditorFormType
    {
        /// <summary>
        /// The game object editor form
        /// </summary>
        GameObjEditorForm,

        /// <summary>
        /// The game light editor form
        /// </summary>
        GameLightEditorForm,

        /// <summary>
        /// The ssao settings form
        /// </summary>
        SSAOSettingsForm,

        /// <summary>
        /// The shadow settings form
        /// </summary>
        ShadowSettingsForm,

        /// <summary>
        /// The particle settings form
        /// </summary>
        ParticleSettingsForm,

        /// <summary>
        /// The renderer settings form
        /// </summary>
        RendererSettingsForm,

        /// <summary>
        /// The ai graph editor form
        /// </summary>
        AIGraphEditorForm,

        /// <summary>
        /// The scene settings form
        /// </summary>
        SceneSettingsForm,

        /// <summary>
        /// The real time event editor form
        /// </summary>
        RealTimeEventEditorForm
    };

    /// <summary>
    ///
    /// </summary>
    internal class EditorFormMgr
    {
        /// <summary>
        /// The _form update funcs
        /// </summary>
        private List<Action> _formUpdateFuncs = new List<Action>();

        /// <summary>
        /// The _updating forms
        /// </summary>
        private List<RenderingSystem.UpdatingForm> _updatingForms = new List<RenderingSystem.UpdatingForm>();

        /// <summary>
        /// The b_load form open
        /// </summary>
        private bool b_loadFormOpen = false;

        /// <summary>
        /// The b_renderer settings form open
        /// </summary>
        private bool b_rendererSettingsFormOpen = false;

        /// <summary>
        /// The b_save form open
        /// </summary>
        private bool b_saveFormOpen = false;

        /// <summary>
        /// The b_shadow settings form open
        /// </summary>
        private bool b_shadowSettingsFormOpen = false;

        /// <summary>
        /// The b_ssao settings form open
        /// </summary>
        private bool b_ssaoSettingsFormOpen = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorFormMgr"/> class.
        /// </summary>
        public EditorFormMgr()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether [load form open].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [load form open]; otherwise, <c>false</c>.
        /// </value>
        public bool LoadFormOpen
        {
            get { return b_loadFormOpen; }
            set { b_loadFormOpen = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [save form open].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [save form open]; otherwise, <c>false</c>.
        /// </value>
        public bool SaveFormOpen
        {
            set { b_saveFormOpen = value; }
            get { return b_saveFormOpen; }
        }

        /// <summary>
        /// Adds the form for updating.
        /// </summary>
        /// <param name="updatingForm">The updating form.</param>
        public void AddFormForUpdating(RenderingSystem.UpdatingForm updatingForm)
        {
            _updatingForms.Add(updatingForm);
        }

        /// <summary>
        /// Adds the update function.
        /// </summary>
        /// <param name="updateFunc">The update function.</param>
        public void AddUpdateFunc(Action updateFunc)
        {
            _formUpdateFuncs.Add(updateFunc);
        }

        /// <summary>
        /// Creates the editor form.
        /// </summary>
        /// <param name="formType">Type of the form.</param>
        /// <param name="gameSystem">The game system.</param>
        /// <param name="modalWindow">if set to <c>true</c> [modal window].</param>
        /// <returns></returns>
        public System.Windows.Forms.Form CreateEditorForm(Type formType, GameSystem gameSystem, bool modalWindow)
        {
            var form = (System.Windows.Forms.Form)Activator.CreateInstance(formType);

            SetPerFormData(form, gameSystem);

            if (modalWindow)
                form.ShowDialog();
            else
                form.Show();

            return form;
        }

        /// <summary>
        /// Creates the load level form.
        /// </summary>
        /// <param name="onFormSubmit">The on form submit.</param>
        public void CreateLoadLevelForm(Action<string> onFormSubmit)
        {
            LoadLevelForm loadLevelForm = new Editor.Forms.LoadLevelForm();
            loadLevelForm.OnLoadAccepted += onFormSubmit;

            b_loadFormOpen = true;

            loadLevelForm.ShowDialog();
        }

        /// <summary>
        /// Creates the particle MGR form.
        /// </summary>
        /// <param name="particleMgr">The particle MGR.</param>
        /// <param name="device">The device.</param>
        /// <param name="content">The content.</param>
        public void CreateParticleMgrForm(Manager.ParticleMgr particleMgr, Microsoft.Xna.Framework.Graphics.GraphicsDevice device,
            Microsoft.Xna.Framework.Content.ContentManager content)
        {
            ParticleMgrForm pmf = new ParticleMgrForm(particleMgr, device, content);
            pmf.Show();
        }

        /// <summary>
        /// Creates the renderer settings form.
        /// </summary>
        /// <param name="gameRenderer">The game renderer.</param>
        public void CreateRendererSettingsForm(RenderingSystem.GameRenderer gameRenderer)
        {
            if (b_rendererSettingsFormOpen)
                return;
            b_rendererSettingsFormOpen = true;
            gameRenderer.CreateRendererSettingsDlg(() =>
                {
                    b_rendererSettingsFormOpen = false;
                });
        }

        /// <summary>
        /// Creates the save level form.
        /// </summary>
        /// <param name="onFormSubmit">The on form submit.</param>
        public void CreateSaveLevelForm(Action<string> onFormSubmit)
        {
            SaveLevelForm saveLevelForm = new Editor.Forms.SaveLevelForm();
            saveLevelForm.OnSaveAccepted += onFormSubmit;

            b_saveFormOpen = true;

            saveLevelForm.ShowDialog();
        }

        /// <summary>
        /// Creates the shadow settings form.
        /// </summary>
        /// <param name="renderAccess">The render access.</param>
        public void CreateShadowSettingsForm(RenderingSystem.RendererAccess renderAccess)
        {
            if (b_shadowSettingsFormOpen)
                return;
            b_shadowSettingsFormOpen = true;
            ShadowSettingsForm shadowSettingsForm = new ShadowSettingsForm(renderAccess);
            Action onFormClose = () =>
                {
                    b_shadowSettingsFormOpen = false;
                };
            shadowSettingsForm.OnDlgClose += onFormClose;
            shadowSettingsForm.ShowDialog();
        }

        /// <summary>
        /// Creates the ssao settings form.
        /// </summary>
        /// <param name="renderAccess">The render access.</param>
        public void CreateSSAOSettingsForm(RenderingSystem.RendererAccess renderAccess)
        {
            if (b_ssaoSettingsFormOpen)
                return;
            b_ssaoSettingsFormOpen = true;
            SSAOSettingsForm ssaoSettingsForm = new SSAOSettingsForm(renderAccess);
            Action onFormClose = () =>
                {
                    b_ssaoSettingsFormOpen = false;
                };
            ssaoSettingsForm.OnDlgClose += onFormClose;
            ssaoSettingsForm.ShowDialog();
        }

        /// <summary>
        /// Determines whether [has active windows].
        /// </summary>
        /// <returns></returns>
        public bool HasActiveWindows()
        {
            return (_formUpdateFuncs.Count != 0 || b_loadFormOpen || b_saveFormOpen || b_ssaoSettingsFormOpen || b_shadowSettingsFormOpen);
        }

        /// <summary>
        /// Updates the forms.
        /// </summary>
        public void UpdateForms()
        {
            foreach (Action updateAction in _formUpdateFuncs)
            {
                updateAction();
            }

            for (int i = 0; i < _updatingForms.Count; ++i)
            {
                var updatingForm = _updatingForms[i];
                if (updatingForm.IsFormClosed)
                    _updatingForms.RemoveAt(i++);
                else
                    updatingForm.UpdateUI();
            }
        }

        /// <summary>
        /// Sets the per form data.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="gameSystem">The game system.</param>
        private void SetPerFormData(System.Windows.Forms.Form form, GameSystem gameSystem)
        {
            if (form is GameObjEditorForm)
            {
                GameObjEditorForm goef = form as GameObjEditorForm;
                goef.SetObjMgr(gameSystem);
                goef.FormUpdateFuncs = _formUpdateFuncs;

                _formUpdateFuncs.Add(goef.UpdateUI);
            }
            else if (form is GameLightEditorForm)
            {
                GameLightEditorForm glef = form as GameLightEditorForm;
                glef.SetLightMgr(gameSystem.LightMgr);
                glef.FormUpdateFuncs = _formUpdateFuncs;

                _formUpdateFuncs.Add(glef.UpdateUI);
            }
            else if (form is SceneSettingsForm)
            {
                SceneSettingsForm ssf = form as SceneSettingsForm;
                ssf.SetRenderAccess(gameSystem.Renderer as RenderingSystem.RendererAccess);
                AddFormForUpdating(ssf);
            }
            else if (form is Editor.Forms.Object_Editors.RealTimeEventEditorForm)
            {
                Editor.Forms.Object_Editors.RealTimeEventEditorForm rteef = form as
                    Editor.Forms.Object_Editors.RealTimeEventEditorForm;
                rteef.SetRTETriggers(gameSystem.GetRealTimeEventTriggers());
            }
        }
    }
}