namespace Discovery
{
    namespace Utility
    {
        public class StringManipulation
        {
            public static string ProperCase(string s)
            {
                s = s.ToLower();
                string sProper = "";

                char[] seps = new char[] { ' ' };
                foreach (string ss in s.Split(seps))
                {
                    sProper += char.ToUpper(ss[0]);
                    sProper +=
                        (ss.Substring(1, ss.Length - 1) + ' ');
                }
                return sProper;
            }

            public static string SplitStringOnUpperCase(string originalValue)
            {
                string newValue = "";
                string currentWord = "";
                foreach (char character in originalValue)
                {
                    if (char.IsUpper(character))
                    {
                        if (currentWord != "")
                        {
                            //we've come to the beginning of a new word
                            newValue = AddNewWord(currentWord, newValue);
                            //blank the current word ready to start on the next
                            currentWord = "";
                        }
                        currentWord = string.Concat(currentWord, character);
                    }
                    else
                    {
                        //add to the current word
                        currentWord = string.Concat(currentWord, character);
                    }
                }
                return  AddNewWord(currentWord, newValue);
            }

            private static string AddNewWord(string currentWord, string newValue)
            {
                if (newValue == "")
                {
                    //either add the new word (prefixed by a space) to the description
                    newValue = string.Concat(newValue,currentWord);
                }
                else
                {
                    //or set the description to contain this first word
                    newValue = string.Concat(newValue, " ", currentWord);
                }
                return newValue;
            }
        }
    }
}