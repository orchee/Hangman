namespace Hangman
{
    public class Solution
    {
        /// <summary>
        /// The word that a user has to guess.
        /// </summary>
        public string Word { get; private set; }
        public string Hint { get; private set; }
        public string Category { get; private set; }

        public Solution(string word, string hint, string category)
        {
            Word = word.ToUpper();
            Hint = hint.ToUpper();
            Category = category.ToUpper();
        }
    }
}