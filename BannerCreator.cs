using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBannerBot
{
    public class BannerCreator
    {
        #region Private Properties
        private Dictionary<string, List<string>> characterDictionary;
        #endregion

        #region Public Properties
        #endregion

        #region Constructors
        public BannerCreator()
        {
            characterDictionary = new Dictionary<string, List<string>>();
            FillCharacterDictionary();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Fetches the hardcoded letters from the Characters class and fills up the characterDicionary with
        /// usable string lists
        /// </summary>
        private void FillCharacterDictionary()
        {
            Type type = typeof(Characters);
            foreach (var c in type.GetFields())
            {
                List<string> charStrings = c.GetValue(null).ToString().Split(Environment.NewLine).ToList();
                characterDictionary.Add(c.Name, charStrings);
            }
            //special case for space (need to find a nicer solution for this^^)
            characterDictionary[" "] = characterDictionary["space"];
            characterDictionary.Remove("space");
        }
        #endregion

        #region Public Methods
        public string CreateBannerMessage(string message)
        {
            string banner = "";

            if (message.Length > 10)
            {
                banner = "It is too long for me to take in! (That's what she said, hahahah!)";
                return banner;
            }
           

            for (int i = 0; i < 7; i++)
            {
                foreach (var character in message)
                {
                    if (characterDictionary.ContainsKey(character.ToString()))
                    {
                        banner += characterDictionary[character.ToString()][i] + " ";
                    }
                    else
                    {
                        banner = "Could not create banner from given input!";
                        return banner;
                    }
                }
                banner += "\n";
            }

            return banner;
        }
        #endregion
    }
}
