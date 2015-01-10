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
using System.Xml.Serialization;
using BaseLogic.Core;

namespace My_Xna_Game.Trivia
{
    public struct TriviaQuestion
    {
        public int CorrectAnswerIndex;
        public bool IsMathQuestion;
        public string MathInputStr;
        public int NumAnswers;
        public string[] PossibleAnswers;
        public string Question;

        /// <summary>
        /// Initializes a new instance of the <see cref="TriviaQuestion"/> struct.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <param name="answers">The answers.</param>
        /// <param name="correctAnswerIndex">Index of the correct answer.</param>
        /// <param name="isMathQuestion">if set to <c>true</c> [is math question].</param>
        /// <param name="mathInputStr">The math input string.</param>
        public TriviaQuestion(string question, string[] answers, int correctAnswerIndex, bool isMathQuestion, string mathInputStr)
        {
            Question = question;
            NumAnswers = answers.Count();
            PossibleAnswers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
            IsMathQuestion = isMathQuestion;
            MathInputStr = mathInputStr;
        }

        /// <summary>
        /// Sets the correct answer.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SetCorrectAnswer(int index)
        {
            string correctAns = PossibleAnswers[CorrectAnswerIndex];
            PossibleAnswers[CorrectAnswerIndex] = PossibleAnswers[index];
            PossibleAnswers[index] = correctAns;
            CorrectAnswerIndex = index;
        }

        /// <summary>
        /// Shuffles the correct answer.
        /// </summary>
        /// <param name="rand">The rand.</param>
        public void ShuffleCorrectAnswer(Random rand)
        {
            string correctAns = PossibleAnswers[CorrectAnswerIndex];
            var ansList = PossibleAnswers.ToList();
            ansList.Shuffle();
            for (int i = 0; i < ansList.Count; ++i)
            {
                if (ansList[i] == correctAns)
                    CorrectAnswerIndex = i;
            }
            PossibleAnswers = ansList.ToArray();
        }
    }

    public class TriviaMgr
    {
        private const string TRIVIA_FOLDER_NAME = "Trivia\\";
        private List<TriviaQuestion> _questions = new List<TriviaQuestion>();
        private int indexCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="TriviaMgr"/> class.
        /// </summary>
        public TriviaMgr()
        {
        }

        /// <summary>
        /// Gets the random trivia question.
        /// </summary>
        /// <returns></returns>
        public TriviaQuestion GetRandomTriviaQuestion()
        {
            if (indexCount > _questions.Count - 1)
            {
                indexCount = 0;
                _questions.Shuffle();
            }

            TriviaQuestion UsedQuestion = _questions[indexCount];

            indexCount++;

            return UsedQuestion;
        }

        /// <summary>
        /// Loads the trivia.
        /// </summary>
        /// <param name="triviaFilename">The trivia filename.</param>
        /// <returns></returns>
        public bool LoadTrivia(string triviaFilename)
        {
            string contentLocation = BaseLogic.Object.FileHelper.GetContentSaveLocation();

            string completeTriviaFilename = contentLocation + TRIVIA_FOLDER_NAME + triviaFilename;

            Stream stream = File.Open(completeTriviaFilename, FileMode.Open);

            XmlSerializer serializer = new XmlSerializer(typeof(List<TriviaQuestion>));
            _questions = (List<TriviaQuestion>)serializer.Deserialize(stream);
            _questions.Shuffle();

            return true;
        }
    }
}