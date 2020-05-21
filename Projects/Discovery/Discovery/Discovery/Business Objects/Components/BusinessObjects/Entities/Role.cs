/*************************************************************************************************
** CLASS:	Rule
**
** OVERVIEW:
** This class is used within the Security Controller to represent a single Role
**
** MODIFICATION HISTORY:
**
** Date:		Version:    Who:	Change:
** 19/7/06	1.0			PJN		Initial Version
************************************************************************************************/
namespace Discovery.BusinessObjects
{
    public class Role
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Role(string name)
        {
            Name = name;
        }

        public Role()
        {
        }
    }
}