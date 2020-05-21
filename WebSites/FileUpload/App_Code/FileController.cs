using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Files
/// </summary>
public static class FileController
{
    public  const string UploadDirectoryName = "\\uploads\\";
    public const string FileNameSuffix = ".resource";

    public static List<UploadedFile> GetUploadedFiles(int projectId)
    {
        //get all from db for project name, ordered
        List<UploadedFile> files = new List<UploadedFile>();
        files.Add(new UploadedFile(1, DateTime.Now, "application/ms-word", 500, "test.doc", "Paul", projectId,""));
        files.Add(new UploadedFile(2, DateTime.Now, "application/ms-word", 100, "physicallymissing.doc", "Paul", projectId,""));
        files.Add(new UploadedFile(3, DateTime.Now, "image/pjpeg", 50, "Sunset.jpg", "Marcus", projectId,""));
        return files;
    }


    public static UploadedFile GetUploadedFile(int fileId)
    {
        //load from db by fileid
        UploadedFile uploadedFile = null;
        switch (fileId)
        {
            case 1:
                uploadedFile = new UploadedFile(1,DateTime.Now, "application/ms-word", 500, "test.doc", "paul", 1,"");
                break;
            case 3:
                uploadedFile =
                    new UploadedFile(3, DateTime.Now, "image/pjpeg", 50, "Sunset.jpg", "Marcus", 1,"");
                break;
        }

        return uploadedFile;
    }
}