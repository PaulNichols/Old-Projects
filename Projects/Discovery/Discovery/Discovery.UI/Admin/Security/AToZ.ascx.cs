using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Discovery.UI.Web.Security
{
    /*************************************************************************************************
    ** CLASS:	AToZ
    **
    ** OVERVIEW:
    ** A user control designed to be used with any list of items that can be filtered alphabetically,
    ** for example a grid of users
    **
    ** MODIFICATION HISTORY:
    **
    ** Date:		Version:    Who:	Change:
    ** 19/7/06		1.0			PJN		Initial Version
    ************************************************************************************************/

    public partial class AToZ : UserControl
    {
        private string currentLetter;

        /// <summary>
        /// Gets the currently selected letter.
        /// </summary>
        /// <value>The current letter.</value>
        public string CurrentLetter
        {
            get
            {
                if (string.IsNullOrEmpty(currentLetter))
                    return null;
                else
                    return currentLetter;
            }
          
        }
        
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
          //go from a to z adding in the letters as linkbuttons
            for (int counter = 'A'; counter <= 'Z';counter++)
            {
                string text = ((Char) counter).ToString();
                AddLetter(text, text);
                Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            }

            AddLetter("All",null);
        }

        /// <summary>
        /// Adds a letter or enter to the control.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="commandArgument">The command argument.</param>
        private void AddLetter(string text,string commandArgument)
        {
            LinkButton letter;
            letter = new LinkButton();
            letter.Text = text;
            letter.CommandName = "Filter";
            letter.CommandArgument = commandArgument;
            letter.CausesValidation = false;
            //letter.CssClass = "CommandButton";
            letter.Command += new CommandEventHandler(letter_Command);
            Controls.Add(letter); 
        }

        /// <summary>
        /// Handles the Command event of the letter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.CommandEventArgs"/> instance containing the event data.</param>
        void letter_Command(object sender, CommandEventArgs e)
        {
            currentLetter = e.CommandArgument.ToString();
        }


    }
}