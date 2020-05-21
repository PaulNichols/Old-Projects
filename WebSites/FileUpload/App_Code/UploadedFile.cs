using System;
using System.Web;

/// <summary>
/// Summary description for UploadedFile
/// </summary>
public class UploadedFile
{


    public UploadedFile(HttpPostedFile postedFile, int projectId)
    {
        mimeType = postedFile.ContentType;
        fileSize = postedFile.ContentLength;
        fileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("\\") + 1);
        uploadedBy = "paul";
        uploadedDate = DateTime.Now;
        this.projectId = projectId;
        originalFullFileName = postedFile.FileName;
    }

    public UploadedFile(int fileId, DateTime uploadedDate, string mimeType, int fileSize, string fileName, string uploadedBy, int projectId, string originalFullFileName)
    {
        this.fileId = fileId;
        this.uploadedDate = uploadedDate;
        this.mimeType = mimeType;
        this.fileSize = fileSize;
        this.fileName = fileName;
        this.uploadedBy = uploadedBy;
        this.projectId = projectId;
        this.originalFullFileName = originalFullFileName;
    }

    #region Fields

    private readonly int fileId;
    private DateTime uploadedDate;
    private string mimeType;
    private int fileSize;
    private string fileName;
    private string uploadedBy;
    private int projectId;
    private string originalFullFileName;

    #endregion

    public string MimeType
    {
        get { return mimeType; }
    }

    public int FileSize
    {
        get { return fileSize; }
    }

    public string FileName
    {
        get { return fileName; }
    }

    public string UploadedBy
    {
        get { return uploadedBy; }
    }

    public DateTime UploadedDate
    {
        get { return uploadedDate; }
    }

    public string FullFileName
    {
        get
        {
            return FilePath + FileName + FileController.FileNameSuffix;
        }
    }

    public string FilePath
    {
        get
        {
            return HttpContext.Current.Server.MapPath(".") + FileController.UploadDirectoryName + "\\" + projectId + "\\";
        }
    }

    public int FileId
    {
        get { return fileId; }
    }

    public string OriginalFullFileName
    {
        get { return originalFullFileName; }
    }

    //public string FileNameWithoutSuffix
    //{
    //    get
    //    {
    //        return FilePath + FileName;
    //    }
    //}
}