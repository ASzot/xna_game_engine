#region About and License
//=======================================================================
// Copyright Andrew Szot 2015.
// Distributed under the MIT License.
// (See accompanying file LICENSE.txt)
//=======================================================================
#endregion

using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace My_Xna_Game.Core
{
    /// <summary>
    /// 
    /// </summary>
    public struct GameProfile
    {
        /// <summary>
        /// The name
        /// </summary>
        public string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameProfile"/> struct.
        /// </summary>
        /// <param name="name">The name.</param>
        public GameProfile(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProfileMgr : RenderingSystem.MgrTemplate<GameProfile>
    {
        private const string PROFILES_FILENAME = "profiles.xml";

        private int i_selectedProfileIndex = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileMgr"/> class.
        /// </summary>
        public ProfileMgr()
        {
        }

        /// <summary>
        /// Gets a value indicating whether [profiles exist].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [profiles exist]; otherwise, <c>false</c>.
        /// </value>
        public bool ProfilesExist
        {
            get { return GetNumberOfDataElements() != 0; }
        }

        /// <summary>
        /// Gets the selected profile.
        /// </summary>
        /// <value>
        /// The selected profile.
        /// </value>
        public GameProfile SelectedProfile
        {
            get
            {
                return _dataElements.ElementAt(i_selectedProfileIndex);
            }
        }

        /// <summary>
        /// Gets or sets the index of the selected profile.
        /// </summary>
        /// <value>
        /// The index of the selected profile.
        /// </value>
        public int SelectedProfileIndex
        {
            get { return i_selectedProfileIndex; }
            set
            {
                i_selectedProfileIndex = value;
            }
        }

        /// <summary>
        /// Adds to list.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void AddToList(GameProfile data)
        {
            base.AddToList(data);

            SaveProfiles();
        }

        /// <summary>
        /// Doeses the profile exist.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool DoesProfileExist(string name)
        {
            foreach (GameProfile gameProfile in _dataElements)
            {
                if (gameProfile.Name == name)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Loads existing profiles into memory.
        /// </summary>
        public void LoadProfiles()
        {
            string dir = Environment.CurrentDirectory;
            string filename = dir + "\\" + PROFILES_FILENAME;
            if (File.Exists(filename))
            {
                Stream stream = File.Open(filename, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(GameProfile[]));
                GameProfile[] gameProfiles = (GameProfile[])serializer.Deserialize(stream);
                _dataElements = gameProfiles.ToList();

                stream.Close();

                if (_dataElements.Count > 0)
                    i_selectedProfileIndex = 0;
            }
        }

        /// <summary>
        /// Removes the data element.
        /// </summary>
        /// <param name="i">The i.</param>
        public override void RemoveDataElement(int i)
        {
            base.RemoveDataElement(i);

            SaveProfiles();
        }

        /// <summary>
        /// Saves the profiles.
        /// </summary>
        /// <exception cref="System.InvalidOperationException"></exception>
        private void SaveProfiles()
        {
            string dir = Environment.CurrentDirectory;
            string filename = dir + "\\" + PROFILES_FILENAME;
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }

            Stream stream = File.Create(filename);

            XmlSerializer serializer = new XmlSerializer(typeof(GameProfile[]));
            if (serializer != null)
                serializer.Serialize(stream, _dataElements.ToArray());
            else
                throw new InvalidOperationException();

            stream.Close();
        }
    }
}