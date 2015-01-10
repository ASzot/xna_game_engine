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

namespace My_Xna_Game.Core
{
    /// <summary>
    /// 
    /// </summary>
    public struct HighScoreData : IComparable<HighScoreData>
    {
        public int ArrowCount;
        public int GoldCount;
        public string LevelName;
        public string Name;
        public int Score;
        public int TurnCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighScoreData"/> struct.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="score">The score.</param>
        /// <param name="levelName">Name of the level.</param>
        /// <param name="arrowCount">The arrow count.</param>
        /// <param name="goldCount">The gold count.</param>
        /// <param name="turnCount">The turn count.</param>
        public HighScoreData(string name, int score, string levelName, int arrowCount, int goldCount, int turnCount)
        {
            Name = name;
            Score = score;
            LevelName = levelName;
            ArrowCount = arrowCount;
            GoldCount = goldCount;
            TurnCount = turnCount;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="hsd1">The HSD1.</param>
        /// <param name="hsd2">The HSD2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(HighScoreData hsd1, HighScoreData hsd2)
        {
            return !(hsd1 == hsd2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="hsd1">The HSD1.</param>
        /// <param name="hsd2">The HSD2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(HighScoreData hsd1, HighScoreData hsd2)
        {
            return (hsd1.Name == hsd2.Name) && (hsd1.Score == hsd2.Score) && (hsd1.TurnCount == hsd2.TurnCount) &&
                (hsd1.GoldCount == hsd2.GoldCount) && (hsd1.ArrowCount == hsd2.ArrowCount) && (hsd1.LevelName == hsd2.LevelName);
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. 
        /// The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> 
        /// parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public int CompareTo(HighScoreData other)
        {
            return this.Score.CompareTo(other.Score);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string formatedData = "{0}: {1} on {2}";
            formatedData = String.Format(formatedData, Name, Score, LevelName);
            return formatedData;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HighscoreMgr
    {
        private const string HIGH_SCORE_FILENAME = "HighScores.txt";

        // ceate a list to keep track of players
        // and make defualt players.
        private List<HighScoreData> _scores = new List<HighScoreData>();

        private string s_saveAbsoluteFilename;

        /// <summary>
        /// Computes the score.
        /// </summary>
        /// <param name="numberOfTurns">The number of turns.</param>
        /// <param name="goldCount">The gold count.</param>
        /// <param name="arrowCount">The arrow count.</param>
        /// <returns></returns>
        public static int ComputeScore(int numberOfTurns, int goldCount, int arrowCount)
        {
            return (100 - numberOfTurns + goldCount + (10 * arrowCount));
        }

        /// <summary>
        /// Adds the score.
        /// </summary>
        /// <param name="highscore">The highscore.</param>
        /// <returns></returns>
        public bool AddScore(HighScoreData highscore)
        {
            // when the game ends, game control will ask to add the
            // score to the list and will return if added(bool)(Saved).

            //			if(Hs.Name.Length != 0)

            //
            // 2. Add current score to list
            // 3. sort list by score.
            //+/ truncate ToString 10

            _scores.Add(highscore);

            _scores.Sort();

            _scores.Reverse();

            if (_scores.Count > 10)
            {
                _scores.RemoveAt(10);
            }

            WriteToFile(s_saveAbsoluteFilename);

            bool isHighscore = false;
            foreach (HighScoreData hsd in _scores)
            {
                if (hsd == highscore)
                    isHighscore = true;
            }

            return isHighscore;
        }

        /// <summary>
        /// Gets the high scores.
        /// </summary>
        /// <returns></returns>
        public List<HighScoreData> GetHighScores()
        {
            // game controll calls to load (display) existing score and
            // my object will return the saved or existing players and
            // display the name and the user will click it and will see their
            // score.
            return _scores;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent()
        {
            string contentLocation = BaseLogic.Object.FileHelper.GetContentSaveLocation();
            string completeFilename = contentLocation + HIGH_SCORE_FILENAME;
            s_saveAbsoluteFilename = completeFilename;

            StreamReader streamReader = new StreamReader(completeFilename);

            string inputStr = streamReader.ReadLine();

            while (inputStr != null)
            {
                string[] dataScore = inputStr.Split(',');
                if (dataScore.Count() != 6)
                    break;

                string name = dataScore[0];
                int score = int.Parse(dataScore[1]);
                string levelName = dataScore[2];
                int goldCount = int.Parse(dataScore[3]);
                int arrowCount = int.Parse(dataScore[4]);
                int turnCount = int.Parse(dataScore[5]);

                HighScoreData highscoreData = new HighScoreData(name, score, levelName, arrowCount, goldCount, turnCount);

                _scores.Add(highscoreData);

                inputStr = streamReader.ReadLine();
            }

            streamReader.Close();
        }

        /// <summary>
        /// Writes to file.
        /// </summary>
        /// <param name="saveLoc">The save loc.</param>
        public void WriteToFile(string saveLoc)
        {
            StreamWriter wumpusStreamWriter = new StreamWriter(saveLoc);

            foreach (HighScoreData hsd in _scores)
            {
                string output = "{0},{1},{2},{3},{4},{5}";
                output = String.Format(output, hsd.Name, hsd.Score, hsd.LevelName, hsd.GoldCount, hsd.ArrowCount, hsd.TurnCount);
                wumpusStreamWriter.WriteLine(output);
            }

            wumpusStreamWriter.Close();
        }
    }
}