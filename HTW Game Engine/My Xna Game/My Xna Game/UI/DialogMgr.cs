#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FileHelper = BaseLogic.Object.FileHelper;

namespace My_Xna_Game.UI
{
    /// <summary>
    ///
    /// </summary>
    public class AISpeech
    {
        private const int TEXT_BREAK_CHAR_COUNT = 60;

        private List<string> _speeches;
        private string s_speakingID = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AISpeech"/> class.
        /// </summary>
        /// <param name="aiSpeechStr">The ai speech string.</param>
        /// <param name="speakingID">The speaking identifier.</param>
        public AISpeech(string aiSpeechStr, string speakingID)
        {
            s_speakingID = speakingID;
            LoadSpeechStr(aiSpeechStr);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AISpeech"/> class.
        /// </summary>
        /// <param name="aiSpeechEle">The ai speech ele.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public AISpeech(XElement aiSpeechEle)
        {
            if (aiSpeechEle.Name != "AISpeech")
                throw new ArgumentException();

            XAttribute seekingIDAttribute = aiSpeechEle.Attribute("speakingID");
            s_speakingID = seekingIDAttribute.Value;

            string totalSpeech = aiSpeechEle.Value;

            LoadSpeechStr(totalSpeech);
        }

        /// <summary>
        /// Gets the speaking identifier.
        /// </summary>
        /// <value>
        /// The speaking identifier.
        /// </value>
        public string SpeakingID
        {
            get { return s_speakingID; }
        }

        /// <summary>
        /// Gets the speeches.
        /// </summary>
        /// <value>
        /// The speeches.
        /// </value>
        public List<string> Speeches
        {
            get { return _speeches; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Speaking: " + s_speakingID;
        }

        /// <summary>
        /// Loads the speech string.
        /// </summary>
        /// <param name="totalSpeech">The total speech.</param>
        private void LoadSpeechStr(string totalSpeech)
        {
            totalSpeech = totalSpeech.Replace(Environment.NewLine, "");

            int currentStrIndex = 0;
            int arrayCount = totalSpeech.Length / TEXT_BREAK_CHAR_COUNT;
            if (totalSpeech.Length % TEXT_BREAK_CHAR_COUNT != 0)
                arrayCount++;

            string[] speechElements = new string[arrayCount];
            for (int i = 0; i < arrayCount; ++i)
            {
                speechElements[i] = "";
            }

            string[] wordsOfStatement = totalSpeech.Split(' ');
            for (int i = 0; i < wordsOfStatement.Length; ++i)
            {
                string wordOfStatement = wordsOfStatement[i];

                speechElements[currentStrIndex] += wordOfStatement + " ";

                if (speechElements[currentStrIndex].Length > TEXT_BREAK_CHAR_COUNT)
                    currentStrIndex++;
            }

            var usedStatements = from statement in speechElements
                                 where statement != ""
                                 select statement;

            _speeches = usedStatements.ToList();
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class Dialog
    {
        // These will be gone through sequentially.
        private AISpeech _aiSpeech;

        private List<PlayerResponse> _playerResponses = new List<PlayerResponse>();
        private string s_dialogID = null;
        private string s_toDialogID = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog"/> class.
        /// </summary>
        /// <param name="aiSpeechStr">The ai speech string.</param>
        /// <param name="speakingID">The speaking identifier.</param>
        public Dialog(string aiSpeechStr, string speakingID)
        {
            _aiSpeech = new AISpeech(aiSpeechStr, speakingID);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog"/> class.
        /// </summary>
        /// <param name="dialogEle">The dialog ele.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public Dialog(XElement dialogEle)
        {
            if (dialogEle.Name != "Dialog")
                throw new ArgumentException();

            XElement aiSpeechEle = dialogEle.Element("AISpeech");
            _aiSpeech = new UI.AISpeech(aiSpeechEle);

            XAttribute idAttribute = dialogEle.Attribute("id");
            s_dialogID = idAttribute.Value;

            XElement toDialogEle = dialogEle.Element("ToDialog");
            if (toDialogEle != null)
            {
                XAttribute toIDAttribute = toDialogEle.Attribute("id");
                s_toDialogID = toIDAttribute.Value;
                return;
            }

            XElement playerResponsesEle = dialogEle.Element("PlayerResponses");
            if (playerResponsesEle != null)
            {
                IEnumerable<XElement> playerResponseEles = playerResponsesEle.Elements("Response");
                foreach (XElement playerResponseEle in playerResponseEles)
                {
                    _playerResponses.Add(new PlayerResponse(playerResponseEle));
                }
            }
        }

        /// <summary>
        /// Gets the ai speech.
        /// </summary>
        /// <value>
        /// The ai speech.
        /// </value>
        public AISpeech AISpeech
        {
            get { return _aiSpeech; }
        }

        /// <summary>
        /// Gets the dialog identifier.
        /// </summary>
        /// <value>
        /// The dialog identifier.
        /// </value>
        public string DialogID
        {
            get { return s_dialogID; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has to dialog.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has to dialog; otherwise, <c>false</c>.
        /// </value>
        public bool HasToDialog
        {
            get { return s_toDialogID != null; }
        }

        /// <summary>
        /// Gets the player responses.
        /// </summary>
        /// <value>
        /// The player responses.
        /// </value>
        public List<PlayerResponse> PlayerResponses
        {
            get { return _playerResponses; }
        }

        /// <summary>
        /// Gets to dialog identifier.
        /// </summary>
        /// <value>
        /// To dialog identifier.
        /// </value>
        public string ToDialogID
        {
            get { return s_toDialogID; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return s_dialogID;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class DialogChain
    {
        private List<Dialog> _dialogs = new List<Dialog>();
        private string s_dialogChainID;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogChain"/> class.
        /// </summary>
        /// <param name="dialogChainEle">The dialog chain ele.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public DialogChain(XElement dialogChainEle)
        {
            if (dialogChainEle.Name != "DialogChain")
                throw new ArgumentException();

            XAttribute dialogChainIDAttribute = dialogChainEle.Attribute("id");
            s_dialogChainID = dialogChainIDAttribute.Value;

            foreach (XElement dialogEle in dialogChainEle.Elements())
            {
                _dialogs.Add(new Dialog(dialogEle));
            }
        }

        /// <summary>
        /// Gets the dialog chain identifier.
        /// </summary>
        /// <value>
        /// The dialog chain identifier.
        /// </value>
        public string DialogChainID
        {
            get { return s_dialogChainID; }
        }

        /// <summary>
        /// Gets the dialog of identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Dialog GetDialogOfID(string id)
        {
            foreach (Dialog dialog in _dialogs)
            {
                if (dialog.DialogID == id)
                    return dialog;
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return s_dialogChainID;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class DialogFile
    {
        private List<DialogChain> _dialogChains = new List<DialogChain>();
        private string s_filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogFile"/> class.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="filename">The filename.</param>
        public DialogFile(XDocument doc, string filename)
        {
            s_filename = filename;
            XElement dialogData = doc.Element("DialogData");

            foreach (XElement dialogChain in dialogData.Elements())
            {
                if (dialogChain.Name != "DialogChain")
                    continue;

                _dialogChains.Add(new DialogChain(dialogChain));
            }
        }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public string Filename
        {
            get { return s_filename; }
        }

        /// <summary>
        /// Gets the dialog chain.
        /// </summary>
        /// <param name="dialogChainID">The dialog chain identifier.</param>
        /// <returns></returns>
        public DialogChain GetDialogChain(string dialogChainID)
        {
            foreach (DialogChain dialogChain in _dialogChains)
            {
                if (dialogChain.DialogChainID == dialogChainID)
                    return dialogChain;
            }

            return null;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class DialogMgr
    {
        private const string DIALOG_FOLDER_NAME = "dialogs\\";

        private Dialog _currentDialog = null;
        private DialogChain _currentDialogChain = null;
        private List<DialogFile> _dialogFiles = new List<DialogFile>();
        private bool b_allowDlgQuit = true;
        private Action<string> fn_onDlgEventFire;
        private int i_height;
        private int i_width;
        private GameUI p_gameUI;
        private BaseLogic.TextRenderer p_textRenderer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogMgr"/> class.
        /// </summary>
        public DialogMgr()
        {
        }

        /// <summary>
        /// Displays the dialog chain.
        /// </summary>
        /// <param name="gameUI">The game UI.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="chainName">Name of the chain.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="textRenderer">The text renderer.</param>
        /// <param name="fnOnDlgEventFire">The function on dialog event fire.</param>
        /// <param name="allowDlgQuit">if set to <c>true</c> [allow dialog quit].</param>
        /// <exception cref="System.ArgumentException">Invalid dialog xml data in  + filename</exception>
        public void DisplayDialogChain(GameUI gameUI, string filename, string chainName, int width,
            int height, BaseLogic.TextRenderer textRenderer, Action<string> fnOnDlgEventFire, bool allowDlgQuit = true)
        {
            i_width = width;
            i_height = height;
            p_textRenderer = textRenderer;
            p_gameUI = gameUI;

            DialogFile dialogFile = GetDialogFile(filename);

            DialogChain dialogChain = dialogFile.GetDialogChain(chainName);

            Dialog startDialog = dialogChain.GetDialogOfID("startDialog");
            if (startDialog == null)
                throw new ArgumentException("Invalid dialog xml data in " + filename);

            _currentDialog = startDialog;
            _currentDialogChain = dialogChain;

            fn_onDlgEventFire += fnOnDlgEventFire;

            b_allowDlgQuit = allowDlgQuit;

            DisplayCurrentDialog();
        }

        /// <summary>
        /// Displays the dialog chain.
        /// </summary>
        /// <param name="gameUI">The game UI.</param>
        /// <param name="displayDialogStr">The display dialog string.</param>
        /// <param name="speakingAI_ID">The speaking a i_ identifier.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="textRenderer">The text renderer.</param>
        public void DisplayDialogChain(GameUI gameUI, string displayDialogStr, string speakingAI_ID, int width, int height,
            BaseLogic.TextRenderer textRenderer)
        {
            i_width = width;
            i_height = height;
            p_textRenderer = textRenderer;
            p_gameUI = gameUI;

            _currentDialog = new Dialog(displayDialogStr, speakingAI_ID);

            b_allowDlgQuit = true;

            DisplayCurrentDialog();
        }

        /// <summary>
        /// Gets the dialog file.
        /// </summary>
        /// <param name="dialogFilename">The dialog filename.</param>
        /// <returns></returns>
        public DialogFile GetDialogFile(string dialogFilename)
        {
            foreach (DialogFile dialogFile in _dialogFiles)
            {
                string name = FileHelper.RemoveFileType(dialogFile.Filename);
                if (name == dialogFilename)
                    return dialogFile;
            }

            return null;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="filesToLoad">The files to load.</param>
        public void LoadContent(params string[] filesToLoad)
        {
            foreach (string loadFileName in filesToLoad)
            {
                LoadDialogFile(loadFileName);
            }
        }

        /// <summary>
        /// Displays the current dialog.
        /// </summary>
        private void DisplayCurrentDialog()
        {
            string speakingAI_ID = _currentDialog.AISpeech.SpeakingID;

            if (speakingAI_ID != null)
            {
                var game = XnaGame.Game_Instance;
                var gameSystem = game.GameSystem;

                // Either can be input. The game object will be preferred.
                var gameObj = gameSystem.ObjMgr.GetDataElement(speakingAI_ID);
                var playerObj = gameSystem.PlayerMgr.GetPlayerOfId(speakingAI_ID);

                Microsoft.Xna.Framework.Vector3 turnToPos;
                if (gameObj == null)
                    turnToPos = playerObj.Position;
                else
                    turnToPos = gameObj.Position;

                var userPlayer = game.GetGameUserPlayer();
                userPlayer.TurnToPlayer(turnToPos);
            }

            p_gameUI.ImmediateNavigateToFrame(DialogUIFrame.FRAME_ID);
            p_gameUI.LoadDialogData(_currentDialog, i_width, i_height, p_textRenderer, OnResponseSelected, b_allowDlgQuit);
        }

        /// <summary>
        /// Loads the dialog file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        private void LoadDialogFile(string filename)
        {
            string filePath = FileHelper.GetContentSaveLocation() + DIALOG_FOLDER_NAME + filename;

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                XDocument doc = XDocument.Load(streamReader);

                _dialogFiles.Add(new DialogFile(doc, filename));
            }
        }

        /// <summary>
        /// Called when [response selected].
        /// </summary>
        /// <param name="response">The response.</param>
        /// <exception cref="System.ArgumentException">Invalid ToDialog id in  + _currentDialogChain.DialogChainID</exception>
        private void OnResponseSelected(PlayerResponse response)
        {
            // A dialog with just a single message can have the dialog chain be null.
            if (_currentDialogChain == null)
                return;

            string toDialog = null;
            if (response == null && _currentDialog.HasToDialog)
                toDialog = _currentDialog.ToDialogID;
            else if (response != null && response.HasToDialog)
                toDialog = response.ToDialogID;

            if (toDialog != null)
            {
                _currentDialog = _currentDialogChain.GetDialogOfID(toDialog);
                if (_currentDialog == null)
                    throw new ArgumentException("Invalid ToDialog id in " + _currentDialogChain.DialogChainID);
                DisplayCurrentDialog();
            }
            else
            {
                // The dialog is done.
                p_gameUI.NavigateToFrame(PlayerUIFrame.FRAME_ID);
                p_gameUI = null;
                i_height = 0;
                i_width = 0;
                p_textRenderer = null;
            }

            if (response != null && response.HasFiredEvent && fn_onDlgEventFire != null)
            {
                fn_onDlgEventFire(response.FiredEventStr);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class PlayerResponse
    {
        private string s_firedEventStr;
        private string s_optionTxt;
        private string s_toDialogID;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerResponse"/> class.
        /// </summary>
        /// <param name="playerResponseEle">The player response ele.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public PlayerResponse(XElement playerResponseEle)
        {
            if (playerResponseEle.Name != "Response")
                throw new ArgumentException();

            XElement textEle = playerResponseEle.Element("Text");
            s_optionTxt = textEle.Value;

            XElement toDialogEle = playerResponseEle.Element("ToDialog");
            if (toDialogEle != null)
            {
                XAttribute toIDAttribute = toDialogEle.Attribute("id");
                s_toDialogID = toIDAttribute.Value;
            }

            XElement firedEventEle = playerResponseEle.Element("FiredEvent");
            if (firedEventEle != null)
            {
                XAttribute firedEventIDAttribute = firedEventEle.Attribute("id");
                s_firedEventStr = firedEventIDAttribute.Value;
            }
        }

        /// <summary>
        /// Gets the fired event string.
        /// </summary>
        /// <value>
        /// The fired event string.
        /// </value>
        public string FiredEventStr
        {
            get { return s_firedEventStr; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has fired event.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has fired event; otherwise, <c>false</c>.
        /// </value>
        public bool HasFiredEvent
        {
            get { return s_firedEventStr != null; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has to dialog.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has to dialog; otherwise, <c>false</c>.
        /// </value>
        public bool HasToDialog
        {
            get { return s_toDialogID != null; }
        }

        /// <summary>
        /// Gets the option text.
        /// </summary>
        /// <value>
        /// The option text.
        /// </value>
        public string OptionTxt
        {
            get { return s_optionTxt; }
        }

        /// <summary>
        /// Gets to dialog identifier.
        /// </summary>
        /// <value>
        /// To dialog identifier.
        /// </value>
        public string ToDialogID
        {
            get { return s_toDialogID; }
        }
    }
}