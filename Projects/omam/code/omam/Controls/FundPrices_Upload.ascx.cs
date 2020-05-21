using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;

public partial class Controls_FundPrices_Upload : UserControl
{
    /// <summary>
    /// Event handler of the upload button
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void UploadFile(object sender, EventArgs e)
    {
        //price date refers to the date of the funds uploaded. It is used else where to rebind the grid
        //as we are about to upload new data this value should be removed
        ViewState.Remove("PriceDate");

        List<string> validationErrors;
        OMAMFundPricesCollection fundPricesCollection;

        if (isFileValid(FileUploadPrices, out validationErrors, out fundPricesCollection))
        {
            saveFundPrices(fundPricesCollection);
        }
        //display regardless of validity as these methds will blank the grid (if no funds were saved)
        //or show no error (if there were none)
        displayUploadedPrices();
        displayValidationErrors(validationErrors);
    }

    /// <summary>
    /// Displays the uploaded prices in the grid. Funds are retrieved on each postback using the date of the fundprices found in the upload file
    /// </summary>
    private void displayUploadedPrices()
    {
        OMAMFundPricesCollection fundPricesCollection = getFundPricesCollection();
        LabelHeader.Visible = fundPricesCollection.Count > 0;
        GridViewUploadedPrices.DataSource = fundPricesCollection;
        GridViewUploadedPrices.DataBind();
    }


    /// <summary>
    /// Displays the validation errors in a bulleted list.
    /// </summary>
    /// <param name="errors">The errors.</param>
    private void displayValidationErrors(List<string> errors)
    {
        BulletedListErrors.Items.Clear();

        foreach (string error in errors)
        {
            BulletedListErrors.Items.Add(error);
        }
    }

    /// <summary>
    /// Saves the fund prices yo DB, remove any previous ones for the same date/time.
    /// </summary>
    /// <param name="fundPricesToSave">The fund prices to save.</param>
    private void saveFundPrices(OMAMFundPricesCollection fundPricesToSave)
    {
        //if we have any prices to save
        if (fundPricesToSave.Count > 0)
        {
            //select any prices already uploaded with the same date as the set we are about to save
            ViewState.Add("PriceDate", fundPricesToSave[0].UploadDate);
            OMAMFundPricesCollection fundPricesCollection = getFundPricesCollection();

            //NOTE: Perhaps a transaction should be placed around the delete and save?
            //remove any prices retrieved
            if (fundPricesCollection.Count > 0)
            {
                fundPricesCollection.MarkAllAsDeleted();
                fundPricesCollection.Save();
            }

            //insert the uploaded prices
            fundPricesToSave.Save();
        }
    }

    /// <summary>
    /// Gets the fund prices from the db based on the datetime found in the upload.
    /// </summary>
    /// <returns></returns>
    private OMAMFundPricesCollection getFundPricesCollection()
    {
        OMAMFundPricesCollection fundPricesCollection = new OMAMFundPricesCollection();
        DateTime? priceDate = (DateTime?)ViewState["PriceDate"];
        if (priceDate != null)
        {
            fundPricesCollection.Query.Where(fundPricesCollection.Query.UploadDate.Equal(priceDate));
            fundPricesCollection.Query.Load();
        }
        return fundPricesCollection;
    }

    /// <summary>
    /// validates the upload and parses the file
    /// </summary>
    /// <param name="fileUpload">The file upload.</param>
    /// <param name="validationErrors">The validation errors.</param>
    /// <param name="fundPricesCollection">The fund prices collection from the parsed file.</param>
    /// <returns>
    /// 	<c>true</c> if the specified file upload is valid; otherwise, <c>false</c>.
    /// </returns>
    private bool isFileValid(FileUpload fileUpload, out List<string> validationErrors,
                         out OMAMFundPricesCollection fundPricesCollection)
    {
        //set up variables
        List<string> tempValidationErrors = new List<string>();

        //perform upload validation
        HttpPostedFile postedFile;
        if (!performUploadValidation(fileUpload, out postedFile, ref tempValidationErrors))
        {
            fundPricesCollection = new OMAMFundPricesCollection();
            validationErrors = new List<string>();
            return false;
        }

        performContentValidation(postedFile,out fundPricesCollection, ref tempValidationErrors);

        //assign out param
        validationErrors = tempValidationErrors;
        return validationErrors.Count == 0;
    }

    private static void performContentValidation(HttpPostedFile postedFile, out OMAMFundPricesCollection fundPricesCollection,ref List<string> validationErrors)
    {
        //needed as you cannot assign values to an out param
        OMAMFundPricesCollection _fundPricesCollection = new OMAMFundPricesCollection();
        List<string> _tempValidationErrors = validationErrors;

        //set the parsing class to parse the contents
        TextFieldParser textFieldParser = new TextFieldParser();

        setFileSchema(textFieldParser.TextFields);

        textFieldParser.QuoteCharacter = '"';
        textFieldParser.FirstLineIsHeader = false;
        textFieldParser.FileType = TextFieldParser.FileFormat.Delimited;
        textFieldParser.Delimiter = ',';
        textFieldParser.TrimWhiteSpace = true;

        // Set up event handlers for when a row is read and when a row is read but failes to match the expected schema
        textFieldParser.RecordFound += new TextFieldParser.RecordFoundHandler(
            delegate(ref Int32 currentLineNumber, TextFieldCollection textFields, string lineText)
            {
                string priceType = (string)textFields["Price Type"].Value;
                if (priceType.ToUpper() == "ACC" || priceType.ToUpper() == "INC")
                {
                    OMAMFundPrices fundPrices = _fundPricesCollection.AddNew();
                    //populate with values in text field array
                    fundPrices.BidPrice = (double?)textFields["Bid Price"].Value;
                    fundPrices.OfferPrice = (double?)textFields["Offer Price"].Value;
                    fundPrices.Yield = (double?)textFields["Yield"].Value;
                    fundPrices.Description = (string)textFields["Description"].Value;
                    fundPrices.FundCode = (string)textFields["Fund Code"].Value;
                    fundPrices.PriceType = priceType;
                    fundPrices.UploadDate = (DateTime?)textFields["Price Date"].Value;
                    fundPrices.UploadedBy = 1;
                }
                else
                {
                    _tempValidationErrors.Add(
                        string.Format("Line {0}: Unknown Fund Type - '{1}'.", priceType, currentLineNumber));
                }
            });

        textFieldParser.RecordFailed += new TextFieldParser.RecordFailedHandler(
            delegate(ref Int32 CurrentLineNumber, string LineText, string ErrorMessage, ref bool Continue) { _tempValidationErrors.Add(string.Format("Line {0}: {1}", CurrentLineNumber, ErrorMessage)); });

        //// parse the file contents
        textFieldParser.ParseFileContents(postedFile.InputStream);

        //assign temp varables back to the outand ref params
        fundPricesCollection =_fundPricesCollection;
        validationErrors = _tempValidationErrors;
    }

    private bool performUploadValidation(FileUpload fileUpload, out HttpPostedFile postedFile, ref List<string> validationErrors)
    {
        if (!fileUpload.HasFile)
        {
            validationErrors.Add("No File was uploaded.");
            postedFile = null;
            return false;
        }

        postedFile = FileUploadPrices.PostedFile;

        if (postedFile == null)
        {
            validationErrors.Add("No File was uploaded.");
            return false;
        }
        if (string.IsNullOrEmpty(postedFile.FileName))
        {
            validationErrors.Add("The selected File had no name.");
            return false;
        }
        if (postedFile.ContentType != "text/plain")
        {
            validationErrors.Add("The selected File was not a text file.");
            return false;
        }
        if (postedFile.ContentLength == 0)
        {
            validationErrors.Add("The uploaded file had no contents.");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Sets the expected file schema.
    /// </summary>
    /// <param name="fields">The fields.</param>
    private static void setFileSchema(TextFieldCollection fields)
    {
        fields.Add(new TextField("Fund Code", TypeCode.String, 10, true));
        fields.Add(new TextField("Description", TypeCode.String, 100, true));
        fields.Add(new TextField("Price Type", TypeCode.String, 3));
        TextField field = new TextField("Price Date", TypeCode.DateTime, true);
        field.DateTimeFormat = "yyyyMMddHHmm";
        fields.Add(field);
        fields.Add(new TextField("Bid Price", TypeCode.Double, false));
        fields.Add(new TextField("Offer Price", TypeCode.Double, false));
        fields.Add(new TextField("Yield", TypeCode.Double, false));
    }

    /// <summary>
    /// Handles the RowUpdating event of the GridViewUploadedPrices control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
    protected void GridViewUploadedPrices_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = GridViewUploadedPrices.Rows[e.RowIndex];

        if (row != null)
        {
            OMAMFundPrices fundPrice = new OMAMFundPrices();
            fundPrice.LoadByPrimaryKey((int)GridViewUploadedPrices.DataKeys[e.RowIndex].Value);
            fundPrice.BidPrice = double.Parse(((TextBox)row.FindControl("TextBoxBidPrice")).Text);
            fundPrice.OfferPrice = double.Parse(((TextBox)row.FindControl("TextBoxOfferPrice")).Text);
            fundPrice.Yield = double.Parse(((TextBox)row.FindControl("TextBoxYield")).Text);
            fundPrice.Save();
        }

        GridViewUploadedPrices.EditIndex = -1;
        displayUploadedPrices();
    }

    /// <summary>
    /// Handles the RowEditing event of the GridViewUploadedPrices control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void GridViewUploadedPrices_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewUploadedPrices.EditIndex = e.NewEditIndex;
        displayUploadedPrices();
    }

    /// <summary>
    /// Handles the RowCancelingEdit event of the GridViewUploadedPrices control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
    protected void GridViewUploadedPrices_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewUploadedPrices.EditIndex = -1;
        displayUploadedPrices();
    }
}