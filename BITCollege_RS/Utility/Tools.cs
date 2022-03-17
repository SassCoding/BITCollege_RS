using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// A class for a collection of useful tools.
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Returns a string of words included before the given term. The given term and everything after are removed.
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="term"></param>
        /// <returns>A string including all words that come before the given term.</returns>
        public static string StringEditor(string sentence, string term)
        {
            //Begins at 0, keeps all values up until the given term is found and stops.
            string result = sentence.Substring(0, sentence.IndexOf(term));

            return result;
        }
    }
}
