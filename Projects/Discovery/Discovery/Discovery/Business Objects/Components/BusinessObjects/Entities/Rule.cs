namespace Discovery.BusinessObjects
{
    /*************************************************************************************************
      ** CLASS:	Rule
      **
      ** OVERVIEW:
      ** This class is used within the Security Controller to represent a single Rule
      **
      ** MODIFICATION HISTORY:
      **
      ** Date:		Version:    Who:	Change:
      ** 19/7/06		1.0			PJN		Initial Version
      ************************************************************************************************/

    public class Rule
    {
        private string name;
        private string expression;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }


        public Rule(string name, string expression)
        {
            Name = name;
            Expression = expression;
        }

        public Rule()
        {
        }
    }
}