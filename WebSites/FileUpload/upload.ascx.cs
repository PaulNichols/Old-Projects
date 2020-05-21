using System;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.UI;

public partial class upload : UserControl
{
    protected void UploadFile(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            UploadedFile uploadedFile;
            if (pysicallySaveFile(postedFile, out uploadedFile))
            {
                saveFileDetails(uploadedFile);
            }
        }
    }

    private bool pysicallySaveFile(HttpPostedFile postedFile, out UploadedFile uploadedFile)
    {
        int projectId = 1;
        uploadedFile = new UploadedFile(postedFile, projectId);
        //check file size<2mb
        bool success = false;

        if (uploadedFile.FileSize > 0)
        {
            if (createUploadDirectory(uploadedFile.FilePath))
            {
                postedFile.SaveAs(uploadedFile.FullFileName);
                success = true;
            }
        }
        LabelError.Visible = !success;
        if (!success)
        {
            LabelError.Text = "Uploading of file " + uploadedFile.FileName + " failed";
        }
        return success;
    }

    private static bool createUploadDirectory(string uploadPath)
    {
        if (!Directory.Exists(uploadPath))
        {
            try
            {
                Directory.CreateDirectory(uploadPath);
            }
            catch (Exception)
            {
                return false;
            }
        }

        return true;
    }

    private bool saveFileDetails(UploadedFile uploadedFile)
    {
        //call stored procedure from here
        return true;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Thread.Sleep(3000);
    }
}