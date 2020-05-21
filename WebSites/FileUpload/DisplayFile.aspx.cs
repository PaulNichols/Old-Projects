using System;
using System.IO;
using System.Web.UI;

public partial class DisplayFile : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int fileId;

        if (int.TryParse(Request.QueryString["FileId"], out fileId))
        {
            UploadedFile uploadedFile = FileController.GetUploadedFile(fileId);
            FileInfo file = null;
            if (uploadedFile != null)
            {
                file = new FileInfo(uploadedFile.FullFileName);
            }

            if (uploadedFile != null && file.Exists)
            {
                Response.Clear();

                Response.ClearContent();

                Response.AddHeader("Content-Disposition", "attachment; filename=" + uploadedFile.FileName);

                Response.AddHeader("Content-Length", file.Length.ToString());

                Response.ContentType = uploadedFile.MimeType;

                Response.WriteFile(file.FullName);

                Response.End();
            }
            else
            {
                Response.Write(" <font color='Red' size='2'>File not found.</font>");
            }
        }
    }
}