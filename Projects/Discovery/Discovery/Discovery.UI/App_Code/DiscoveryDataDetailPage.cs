using System;
using System.Diagnostics;
using System.Web.Compilation;
using System.Web.UI.WebControls;
using ValidationFramework.Web;

namespace Discovery.UI.Web
{
    /*************************************************************************************************
     ** CLASS:	DiscoveryDataPage
     **
     ** OVERVIEW:
     ** This is base class for any pages that view or maintain list or a singular item of data
     **
     ** MODIFICATION HISTORY:
     **
     ** Date:		Version:    Who:	Change:
     ** 19/7/06	    1.0			PJN		Initial Version
     ************************************************************************************************/

    public abstract partial class DiscoveryDataDetailPage : DiscoveryDataPage
    {
        #region Fields

        private ValidatorGenerator validation = null;

        #endregion

        #region Properties

        public FormViewMode LastMode
        {
            get
            {
                if (null != ViewState["LastMode"])
                {
                    return (FormViewMode)ViewState["LastMode"];
                }
                else
                {
                    return FormViewMode.ReadOnly;
                }
            }
            set { ViewState["LastMode"] = value; }
        }

        public ValidatorGenerator Validation
        {
            get
            {
                // Use jit to create the validator
                if (null == validation) validation = new ValidatorGenerator();
                // done
                return validation;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Sets the hidden updated by field.
        /// </summary>
        protected void SetUpdatedBy()
        {
            HiddenField updatedBy = GetControl<HiddenField>("UpdatedBy", PageFormView);
            if (updatedBy != null) updatedBy.Value = User.Identity.Name;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            FormView pageFormView = PageFormView;

            if (pageFormView != null)
            {
                // Used for rendering the validators
                pageFormView.PreRender += new EventHandler(pageFormView_PreRender);
            }

            if (!IsPostBack)
            {
                if (pageFormView != null)
                {
                    int result;
                    if (AllowAutoInsertMode() && ( Request.QueryString["Id"] == null ||
                        (Int32.TryParse(Server.UrlDecode(Request.QueryString["Id"].ToString()), out result) &&
                         result == -1)))
                    {
                        // Force insert mode
                        pageFormView.ChangeMode(FormViewMode.Insert);
                    }
                }
            }

            //set up some event handlers for inserting, updating and deleteing
            ObjectDataSource dataSource = PageDataSource;
            if (dataSource != null)
            {
                dataSource.Inserted += new ObjectDataSourceStatusEventHandler(DataSource_Inserted);
                dataSource.Updated += new ObjectDataSourceStatusEventHandler(DataSource_Updated);
                pageFormView.ItemUpdated += new FormViewUpdatedEventHandler(pageFormView_ItemUpdated);
                pageFormView.ItemInserted += new FormViewInsertedEventHandler(pageFormView_ItemInserted);
                pageFormView.ItemDeleted += new FormViewDeletedEventHandler(PageFormView_ItemDeleted);
            }

            // Call base
            base.Page_Load(sender, e);
        }

        protected virtual bool AllowAutoInsertMode()
        {
            return true;
        }

        /// <summary>
        /// Handles the PreRender event of the pageFormView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void pageFormView_PreRender(object sender, EventArgs e)
        {
            // Check the mode
            if (PageFormView.CurrentMode != FormViewMode.ReadOnly /*&& LastMode != PageFormView.CurrentMode*/)
            {
                // Render validation controls, override this to insert your controls
                SetValidation();
            }

            // Store the last mode
            LastMode = PageFormView.CurrentMode;
        }

        /// <summary>
        /// Handles the ItemInserted event of the pageFormView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.FormViewInsertedEventArgs"/> instance containing the event data.</param>
        protected void pageFormView_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInInsertMode = true;
                e.ExceptionHandled = true;
                //DiscoveryException discoveryException = new DiscoveryException("Your Save was Unsuccessful", e.Exception);
                //ExceptionPolicy.HandleException(discoveryException, "Business Logic");
                DisplayMessage(e.Exception);
            }
        }

        /// <summary>
        /// Handles the ItemUpdated event of the pageFormView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.FormViewUpdatedEventArgs"/> instance containing the event data.</param>
        protected void pageFormView_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            // Stay in edit mode if we have an exception
            if (e.Exception != null)
            {
                if (!e.ExceptionHandled)
                {
                    // Display the exception if we have one
                    DisplayMessage("Your save was unsuccessful.", DiscoveryMessageType.Error);
                    DisplayMessage(e.Exception);
                }
                // Don't change mode, we failed
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
            }
            else
            {
                // Display message
                DisplayMessage("Your save was successful.", DiscoveryMessageType.Success);
            }
        }

        /// <summary>
        /// Handles the Updated event of the DataSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs"/> instance containing the event data.</param>
        protected void DataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            // See if we have an exception
            if (e.Exception == null)
            {
                // See if we have a return value
                if (e.ReturnValue is Int32 && (int)e.ReturnValue == -1)
                {
                    // Throw exception
                    throw new Exception("Failed to save the entity to the database, the return value was -1.");
                }
            }
        }


        /// <summary>
        /// Gets the page form view.
        /// </summary>
        /// <value>The page form view.</value>
        private FormView pageFormView = null;

        protected FormView PageFormView
        {
            get
            {
                if (null == pageFormView)
                {
                    // Attempt to find the first form view control
                    pageFormView = GetControlByType<FormView>(ThisContentPlaceHolder);
                    // Make sure we found the form view
                    Debug.Assert(null != pageFormView);
                }
                // Done
                return pageFormView;
            }
        }

        /// <summary>
        /// Gets the page data source.
        /// </summary>
        /// <value>The page data source.</value>
        private ObjectDataSource pageDataSource = null;

        protected ObjectDataSource PageDataSource
        {
            get
            {
                if (PageFormView != null)
                {
                    if (null == pageDataSource)
                    {
                        // Attempt to find the object data source
                        pageDataSource = GetControl<ObjectDataSource>(PageFormView.DataSourceID, ThisContentPlaceHolder);
                        // Make sure we found the data source
                        Debug.Assert(null != pageDataSource);
                    }
                    // Done
                    return pageDataSource;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            SetUpdatedBy();
        }


        /// <summary>
        /// Handles the Inserted event of the DataSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs"/> instance containing the event data.</param>
        protected virtual void DataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                // Return to the specified url using the return value
                Response.Redirect(String.Format(BackUrl, (null == e.ReturnValue) ? "-1" : e.ReturnValue.ToString()));
            }
            //else if (!e.ExceptionHandled)
            //{
            //    // Display error message
            //    DiscoveryException discoveryException=new DiscoveryException("Your Save was Unsuccessful",e.Exception);
            //    ExceptionPolicy.HandleException(discoveryException, "Business Logic");
            //    DisplayMessage(discoveryException.Message);

            //}
        }

        /// <summary>
        /// Handles the Click event of the InsertButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            SetUpdatedBy();
        }

        /// <summary>
        /// Handles the Click event of the InsertCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(BackUrl);
        }

        /// <summary>
        /// Handles the ItemDeleted event of the PageFormView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Web.UI.WebControls.FormViewDeletedEventArgs"/> instance containing the event data.</param>
        protected virtual void PageFormView_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                // Display message
                DisplayMessage(e.Exception);
                e.ExceptionHandled = true;
            }
            else
            {
                Response.Redirect(BackUrl);
            }
        }

        /// <summary>
        /// Sets the validation.
        /// </summary>
        protected virtual void SetValidation()
        {
            // Make sure we have a form view
            if (PageFormView != null)
            {
                Type typeToValidate = null;
                if (null != PageFormView.DataItem)
                {
                    typeToValidate = PageFormView.DataItem.GetType();
                }
                else
                {
                    typeToValidate = BuildManager.GetType(PageDataSource.DataObjectTypeName, false, true);
                }
                
                // Make sure we have a type
                Debug.Assert(null != typeToValidate);

                // Add controls for validataion
                Validation.GenerateValidators(PageFormView.Row, typeToValidate);
            }
        }

        #region Protected Methods

        protected override void SetEnabledStateOfButtons()
        {
            WebControl editButton = PageFormView.FindControl("ButtonEdit") as WebControl;
            WebControl deleteButton = PageFormView.FindControl("ButtonDelete") as WebControl;
            if (editButton != null)
            {
                editButton.Enabled = CanUpdate;
            }
            if (deleteButton != null)
            {
                deleteButton.Enabled = CanDelete;
            }
        }

        #endregion

        protected override bool HasPageLoadAuthorisation()
        {
            return CanRead;
        }
    }
}