//  Modifications
//  Feb 8, 2005:  - Added parameter checking to the TextFieldParser.Delimiter property.
//                - Added constructor overloads to the TextField class to eliminate the
//                  need to pass unnecessary parameters.
//  Feb 9, 2005:  - Added the TextFieldParser.LineNumber property and exposed
//                  it as a ByRef argument of the RecordFound and RecordFailed events.
//                - Added parameter checking to the TextField.Name and TextField.Length
//                  property setters and altered the constructor to use the properties
//                  instead of the private fields.
//  Feb 13, 2005: - Removed the regular expression functionality for FileFormat.FixedWidth
//                  and FileFormat.Delimitted and replaced it with new functionality based 
//                  on the String.Split() for delimitted files and String.Substring() for 
//                  fixed-width files.  All external interfaces remain unchanged and all 
//                  previous functionality remains.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Discovery.BusinessObjects;
using Discovery.Utility.DataAccess;

namespace Discovery.ComponentServices.Parsing
{
    /// <summary>
    /// A class is to be used to parse the text field
    /// </summary>
    public class TextFieldParser
    {
        #region '" Enumerations "' 

        /// <summary>
        /// An enumeration to include file format in fixed width and delimitor
        /// </summary>
        public enum FileFormat
        {
            /// <summary>
            /// FixedWidth of the FileFormat
            /// </summary>
            FixedWidth,
            /// <summary>
            /// Delimited of the FileFormat
            /// </summary>
            Delimited,
        }

        #endregion 

        #region '" Delegates & Events "' 

        /// <summary>
        /// A delegate to handle that a record is found
        /// </summary>
        /// <param name="currentLineNumber"></param>
        /// <param name="textFields"></param>
        /// <param name="lineText"></param>
        public delegate void RecordFoundHandler(ref Int32 currentLineNumber, TextFieldCollection textFields, string lineText);

        /// <summary>
        /// An event to handle whether a record is found
        /// </summary>
        public event RecordFoundHandler RecordFound;

        /// <summary>
        /// A delegate to handle a failure record
        /// </summary>
        /// <param name="CurrentLineNumber"></param>
        /// <param name="LineText"></param>
        /// <param name="ErrorMessage"></param>
        /// <param name="Continue"></param>
        public delegate void RecordFailedHandler(
            ref Int32 CurrentLineNumber, string LineText, string ErrorMessage, ref bool Continue);
        
        /// <summary>
        /// A delegate to handle that a record is read
        /// </summary>
        /// <param name="CurrentLineNumber"></param>
        /// <param name="LineText"></param>
        public delegate void RecordReadHandler(Int32 CurrentLineNumber, string LineText);

        /// <summary>
        /// An event to handle whether a failure record
        /// </summary>
        public event RecordFailedHandler RecordFailed;

        /// <summary>
        /// An event to handle whether a record is read
        /// </summary>
        public event RecordReadHandler RecordRead;

        #endregion 

        #region '" Private Fields "' 

        private FileFormat fileType = FileFormat.Delimited;
        private string fileName = "";
        private TextFieldCollection textFields;
        private char delimiter = Convert.ToChar(",");
        private RecordFoundHandler callBack;
        private Int32 currentLineNumber = 0;
        private char quoteChar = Convert.ToChar(@"""");

        #endregion 

        #region '" Constructors "' 

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextFieldParser"/> class.
        /// </summary>
        public TextFieldParser() : this("", FileFormat.FixedWidth)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextFieldParser"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public TextFieldParser(string fileName) : this(fileName, FileFormat.FixedWidth)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextFieldParser"/> class.
        /// </summary>
        /// <param name="fileType">Type of the file.</param>
        public TextFieldParser(FileFormat fileType) : this("", fileType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextFieldParser"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileType">Type of the file.</param>
        public TextFieldParser(string fileName, FileFormat fileType)
        {
            this.fileName = fileName;
            this.fileType = fileType;
            textFields = new TextFieldCollection();
        }

        #endregion 

        #region '" Public Properties "' 

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        /// <summary>
        /// Gets or sets the type of the file.
        /// </summary>
        /// <value>The type of the file.</value>
        public FileFormat FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }

        /// <summary>
        /// Gets or sets the delimiter.
        /// </summary>
        /// <value>The delimiter.</value>
        public char Delimiter
        {
            get { return Convert.ToChar(delimiter); }
            set
            {
                if (value.ToString().Length == 0 && fileType == FileFormat.Delimited)
                {
                    throw new ApplicationException(
                        @"You must specify a Delimiter when the FileType is ""FileFormat.Delimited""");
                }
                else
                {
                    delimiter = value;
                }
            }
        }

        private bool trimWhiteSpace = false;
        private bool firstLineIsHeader;

        /// <summary>
        /// Gets or sets a value indicating whether [trim white space].
        /// </summary>
        /// <value><c>true</c> if [trim white space]; otherwise, <c>false</c>.</value>
        public bool TrimWhiteSpace
        {
            get { return trimWhiteSpace; }
            set { trimWhiteSpace = value; }
        }

        /// <summary>
        /// Gets the text fields.
        /// </summary>
        /// <value>The text fields.</value>
        public TextFieldCollection TextFields
        {
            get { return textFields; }
        }

        /// <summary>
        /// Gets or sets the current line number.
        /// </summary>
        /// <value>The current line number.</value>
        public Int32 CurrentLineNumber
        {
            get { return currentLineNumber; }
            set
            {
                if (value < currentLineNumber)
                {
                    throw new ApplicationException("You can not decriment the LineNumber.");
                }
                currentLineNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets the quote character.
        /// </summary>
        /// <value>The quote character.</value>
        public char QuoteCharacter
        {
            get { return quoteChar; }
            set { quoteChar = value; }
        }

        public bool FirstLineIsHeader
        {
            get { return firstLineIsHeader; }
            set { firstLineIsHeader = value; }
        }

        #endregion 

        #region '" Public Methods "' 

        //public bool ParseLine(string line)
        //{
        //    //  fill the fields array based on file type
        //    Array fields = GetFieldArray(line);
        //    //  make sure we found a match
        //    if (fields.Length == textFields.Count)
        //    {
        //        //  the record matches our pattern
        //        try
        //        {
        //            //  loop through the fields and assign the values
        //            for (int x = 0; x <= textFields.Count - 1; x++)
        //            {
        //                textFields[x].Value = fields.GetValue(x);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //  pass the unhandled error back to the caller
        //            //  the most likely problem is a conversion/cast error
        //            bool continueIdent = true;
        //            int refActualLineNumber=0;
        //            if (null != RecordFailed)
        //                RecordFailed(ref refActualLineNumber, line, ex.Message, ref continueIdent);
        //            if (!(continueIdent))
        //            {
        //                // No more lines
        //                return false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //  the number of fields located doesn't match the configuration
        //        bool continueIdent = true;
        //        Int32 refActualLineNumber = 0;
        //        if (null != RecordFailed)
        //            RecordFailed(ref refActualLineNumber, line,
        //                         "The number of fields identified in the file record does not match the number of TextField objects specified.",
        //                         ref continueIdent);
        //        if (!(continueIdent))
        //        {
        //            // No more lines
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        /// <summary>
        /// Parses the line.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="actualLineNumber">The actual line number.</param>
        /// <returns></returns>
        public bool ParseLine(TextReader reader, ref int actualLineNumber)
        {
            string fileRecord;

            //  get the data and execute the pattern match
            fileRecord = reader.ReadLine();

            actualLineNumber++;

            if (null != RecordRead)
                RecordRead( actualLineNumber, fileRecord);
            
            //  don't process lines that we are supposed to skip
            if (actualLineNumber >= currentLineNumber)
            {
                //  make sure the line number property stays in sync
                CurrentLineNumber = actualLineNumber;
                //  fill the fields array based on file type
                try
                {
                    Array fields = GetFieldArray(fileRecord);
                    //  make sure we found a match
                    if (fields.Length == textFields.Count)
                    {
                        //  the record matches our pattern
                        try
                        {
                            //  loop through the fields and assign the values
                            for (int x = 0; x <= textFields.Count - 1; x++)
                            {
                                // See if we need to trim string data
                                if (trimWhiteSpace && textFields[x].DataType == TypeCode.String)
                                {
                                    textFields[x].Value = fields.GetValue(x).ToString().Trim();
                                }
                                else
                                {
                                    textFields[x].Value = fields.GetValue(x);
                                }
                            }
                            //  let the caller know what we found
                            if (null != RecordFound) RecordFound(ref actualLineNumber, textFields, fileRecord);
                            CurrentLineNumber = actualLineNumber;
                        }
                        catch (Exception ex)
                        {
                            //  pass the unhandled error back to the caller
                            //  the most likely problem is a conversion/cast error
                            //bool continueIdent = true;
                            //Int32 refActualLineNumber = actualLineNumber;
                            //if (null != RecordFailed)
                            //    RecordFailed(ref refActualLineNumber, fileRecord, ex.Message, ref continueIdent);
                            //if (!(continueIdent))
                            //{
                            //    // No more lines
                            //    return false;
                            //}
                            //CurrentLineNumber = refActualLineNumber;
                            
                            // Simply re throw the exception, will be caught later
                            throw ex;
                        }
                    }
                    else
                    {
                        //  the number of fields located doesn't match the configuration
                        //bool continueIdent = true;
                        //Int32 refActualLineNumber = actualLineNumber;
                        //if (null != RecordFailed)
                        //    RecordFailed(ref refActualLineNumber, fileRecord,
                        //                 "The number of fields identified in the file record does not match the number of TextField objects specified.",
                        //                 ref continueIdent);
                        //if (!(continueIdent))
                        //{
                        //    // No more lines
                        //    return false;
                        //}
                        //CurrentLineNumber = refActualLineNumber;

                        // Simply throw an exception, will be caught later
                        throw new Exception(string.Format("The number of fields identified in the file '{0}' do not match the expected number.",FileName));
                    }
                }
                catch (Exception ex)
                {
                    
                    //  pass the unhandled error back to the caller
                    bool continueIdent = true;
                    Int32 refActualLineNumber = actualLineNumber;
                    if (null != RecordFailed)
                        RecordFailed(ref refActualLineNumber, fileRecord, ex.Message, ref continueIdent);
                    if (!(continueIdent))
                    {
                        // No more lines
                        return false;
                    }
                    CurrentLineNumber = refActualLineNumber;
                }
                // More lines
                return true;
            }
            // No more lines
            return false;
        }

        /// <summary>
        /// Parses the file.
        /// </summary>
        public void ParseFile()
        {
            ParseFile(fileName);
        }

        /// <summary>
        /// Parses the file contents.
        /// </summary>
        /// <param name="fileContents">The file contents.</param>
        public void ParseFileContents(string fileContents)
        {
            StringReader reader = new StringReader(fileContents);
            int actualLineNumber = 0;
            bool moreToRead = true;

            if (FirstLineIsHeader)
            {
                reader.ReadLine();
            }


            //  loop through the text file
            while (reader.Peek() != -1 && moreToRead)
            {
                moreToRead = ParseLine(reader, ref actualLineNumber);
            }
            //  clean up our stream
            reader.Close();
        }

        /// <summary>
        /// Parses the file.
        /// </summary>
        /// <param name="fileNameToParse">The file name to parse.</param>
        public void ParseFile(string fileNameToParse)
        {
            StreamReader reader = new StreamReader(fileNameToParse);
            int actualLineNumber = 0;
            bool moreToRead = true;
            //  set the properties
            FileName = fileNameToParse;
            //  loop through the text file
            while (reader.Peek() != -1 && moreToRead)
            {
                moreToRead = ParseLine(reader, ref actualLineNumber);
            }
            //  clean up our stream
            reader.Close();
        }

        /// <summary>
        /// Gets the property info.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        private static List<PropertyInfo> GetPropertyInfo(Type t)
        {
            //  Use the cache because the reflection used later is expensive
            List<PropertyInfo> properties = (List<PropertyInfo>)DataCache.GetCache(t.FullName);
            // Were the properties in the cache?
            if (properties == null)
            {
                properties = new List<PropertyInfo>();
                foreach (PropertyInfo property in t.GetProperties())
                {
                    // Add property to properties collection
                    properties.Add(property);
                }

                // Add to cache
                DataCache.SetCache(t.FullName, properties);
            }
            return properties;
        }

        /// <summary>
        /// Fills the object.
        /// </summary>
        /// <param name="objectToPopulate">The object to populate.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="entityPrefix">The entity prefix.</param>
        public static void FillObject(DiscoveryBusinessObject objectToPopulate, TextFieldCollection fields, string entityPrefix)
        {
            // Qualified property name
            string qualifiedProperty = "";

            // Iterate over the properties of the type
            List<PropertyInfo> objectProperties = TextFieldParser.GetPropertyInfo(objectToPopulate.GetType());
            
            foreach (PropertyInfo objectProperty in objectProperties)
            {
                // Build the qualified name
                qualifiedProperty = ((string.IsNullOrEmpty(entityPrefix))?"":entityPrefix + ".") + objectProperty.Name;

                // See if we can write to the property
                if (objectProperty.CanWrite)
                {
                    // See if it's a complex type
                    if (objectProperty.PropertyType.IsSubclassOf(typeof(DiscoveryBusinessObject)))
                    {
                        // It's a complext type
                        FillObject(
                                    (DiscoveryBusinessObject)objectProperty.GetValue(objectToPopulate, null), 
                                    fields,
                                    qualifiedProperty);
                    }
                    else if (null != fields[qualifiedProperty])
                    {
                        // See if the property is in the text fields collection
                        objectProperty.SetValue(
                                    objectToPopulate, 
                                    Convert.ChangeType(fields[qualifiedProperty].Value, objectProperty.PropertyType), null);
                    }
                }
            }
        }

        /// <summary>
        /// Fills the object.
        /// </summary>
        /// <param name="objectToPopulate">The object to populate.</param>
        /// <param name="fields">The fields.</param>
        public static void FillObject(DiscoveryBusinessObject objectToPopulate, TextFieldCollection fields)
        {
            // Call default implementation to fill base object
            FillObject(objectToPopulate, fields, "");
        }

        #endregion 

        #region '" Private Methods "' 

        private Array GetFieldArray(string fileRecord)
        {
            Array fields = null;
            switch (FileType)
            {
                case FileFormat.Delimited:
                    {
                        //  split the fields
                        string[] rawFields = fileRecord.Split(Convert.ToChar(Delimiter));
                        //  recombine any with quotes
                        RecombineQuotedFields(ref rawFields);
                        //  remove the extra elements
                        ExtractNullArrayElements(ref rawFields, ref fields);
                        break;
                    }
                case FileFormat.FixedWidth:
                    {
                        ArrayList rawFields = new ArrayList();
                        Int32 mark = 0;
                        for (int x = 0; x <= textFields.Count - 1; x++)
                        {
                            //  extract the value and move the book mark
                            rawFields.Add(fileRecord.Substring(mark, textFields[x].Length));
                            mark += textFields[x].Length;
                        }
                        fields = rawFields.ToArray();
                        break;
                    }
                default:
                    throw new ApplicationException("The specified FileType is not valid.");
            }

            return fields;
        }


        private void RecombineQuotedFields(ref string[] fields)
        {
            char firstChar;
            char lastChar;
            Int32 maxFieldIndex = fields.GetLength(0) - 1;
            //  start seaching
            for (int x = 0; x <= maxFieldIndex; x++)
            {
                //  get the potential delimitters
                if (fields[x].Length > 0)
                {
                    firstChar = fields[x][0];
                    lastChar = fields[x][fields[x].Length - 1];
                }
                else
                {
                    // Done
                    continue;
                }
                //  start the comparisons
                if (firstChar == quoteChar)
                {
                    //  we started with a valid quote character
                    if ((firstChar == lastChar))
                    {
                        //  strip off the matched quotes
                        fields.SetValue(fields[x].Substring(1, fields[x].Length - 2), x);
                    }
                    else
                    {
                        Int32 startIndex = x;
                        char theChar = firstChar;
                        do
                        {
                            //  skip to the next item
                            x += 1;
                            //  get the new "endpoints"
                            firstChar = fields[x][0];
                            lastChar = fields[x][fields[x].Length - 1];
                            //  this field better not start with a quote
                            if (firstChar == quoteChar && fields[x].Length > 1)
                            {
                                throw new ApplicationException("There was an unclosed quotation mark.");
                            }
                            //  recombine the items
                            fields.SetValue(
                                string.Concat(fields[startIndex].ToString(), Convert.ToString(Delimiter),
                                              fields[x].ToString()), startIndex);
                            //  flush the unused array element
                            Array.Clear(fields, x, 1);
                        } while (! ((lastChar == theChar)));
                        //  strip off the outer quotes
                        fields.SetValue(fields[startIndex].Substring(1, fields[startIndex].Length - 2), startIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Extracts the null array elements.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        private void ExtractNullArrayElements(ref string[] input, ref Array output)
        {
            Int32 x;
            Int32 maxInputIndex = input.Length - 1;
            Int32 count = 0;
            Int32 mark = 0;
            //  get the actual field count
            for (x = 0; x <= maxInputIndex; x++)
            {
                if (!(input[x] == null))
                {
                    count += 1;
                }
            }
            //  resize the output array
            output = Array.CreateInstance(typeof (string), count);
            for (x = 0; x <= maxInputIndex; x++)
            {
                if (!((input[x] == null)))
                {
                    //  save the value and incriment the book mark
                    output.SetValue(input[x], mark);
                    mark += 1;
                }
            }
        }

        #endregion
    }

    #region '" TextField "' 

    /// <summary>
    /// A field to be parsed from the text file.
    /// </summary>
    public class TextField
    {
        private string name;
        private TypeCode dataType;
        private Int32 length;
        private bool quoted;
        private object fieldValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextField"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="DataType">Type of the data.</param>
        ///// <param name="Quoted">if set to <c>true</c> [quoted].</param>
        public TextField(string Name, TypeCode DataType)
            : this(Name, DataType, 0XFFF, false)
        {
            //  this constructor would be used with delimited fields
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextField"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="DataType">Type of the data.</param>
        /// <param name="Quoted">if set to <c>true</c> [quoted].</param>
        public TextField(string Name, TypeCode DataType, bool Quoted) : this(Name, DataType, 0XFFF, Quoted)
        {
            //  this constructor would be used with delimited fields
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextField"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="DataType">Type of the data.</param>
        /// <param name="Length">The length.</param>
        public TextField(string Name, TypeCode DataType, Int32 Length) : this(Name, DataType, Length, false)
        {
            //  this constructor would be used with fixed-width fields
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextField"/> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="DataType">Type of the data.</param>
        /// <param name="Length">The length.</param>
        /// <param name="Quoted">if set to <c>true</c> [quoted].</param>
        private TextField(string Name, TypeCode DataType, Int32 Length, bool Quoted)
        {
            //  set the property fields
            this.Name = Name;
            this.DataType = DataType;
            this.Length = Length;
            this.Quoted = Quoted;
            fieldValue = null;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return name; }
            set
            {
                if ((value.Length < 1) | (value == null) | (value == string.Empty))
                {
                    throw new ApplicationException("You can not set the Name property to a blank, empty or null value.");
                }
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        public TypeCode DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public Int32 Length
        {
            get { return length; }
            set
            {
                if (value < 1 & value != 0XFFF)
                {
                    throw new ApplicationException("You can not set the Length property to a zero or negative value.");
                }
                length = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:TextField"/> is quoted.
        /// </summary>
        /// <value><c>true</c> if quoted; otherwise, <c>false</c>.</value>
        public bool Quoted
        {
            get { return quoted; }
            set { quoted = value; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get { return fieldValue; }
            set
            {
                try
                {
                    // Do some additional work for booleans
                    if (dataType == TypeCode.Boolean)
                    {
                        switch (value.ToString().ToLower())
                        {
                            case "f":
                            case "0":
                                {
                                    value = "false";
                                    break;
                                }
                            case "t":
                            case "1":
                                {
                                    value = "true";
                                    break;
                                }
                        }
                    }

                    fieldValue = Convert.ChangeType(value, dataType);
                }
                catch
                {
                    throw new ArgumentException(
                        string.Format(
                            @"There was an error converting the value ""{0}"" to a {1} for the field ""{2}"".", value,
                            dataType.ToString(), name));
                }
            }
        }
    }

    #endregion 

    #region '" TextFieldCollection "' 

    /// <summary>
    /// A collection that stores 'TextField' objects.
    /// </summary>
    public class TextFieldCollection : CollectionBase
    {
       
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextFieldCollection"/> class.
        /// </summary>
        public TextFieldCollection() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextFieldCollection"/> class based on an already existing instance.
        /// </summary>
        /// <param name="texValue">The 'TextFieldCollection' from which the contents is copied.</param>
        public TextFieldCollection(TextFieldCollection texValue) : base()
        {
            AddRange(texValue);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TextFieldCollection"/> class with an array of 'TextField' objects.
        /// </summary>
        /// <param name="texValue">An array of 'TextField' objects with which to initialize the collection.</param>
        public TextFieldCollection(TextField[] texValue) : base()
        {
            AddRange(texValue);
        }

        /// <summary>
        /// Gets or sets the <see cref="T:TextField"/> with the specified int index position.
        /// </summary>
        /// <param name="intIndex">The zero-based index of the entry to locate in the collection.</param>
        /// <value>The entry at the specified index of the collection.</value>
        public TextField this /* TRANSWARNING: was Item */[int intIndex]
        {
            get { return ((TextField) (List[intIndex])); }
            set { List[intIndex] = value; }
        }

        /// <summary>
        /// Gets the <see cref="T:TextField"/> with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <value></value>
        public TextField this /* TRANSWARNING: was Item */[string name]
        {
            get
            {
                TextField returnField = null;
                foreach (TextField field in List)
                {
                        if (field.Name==name)
                        {
                            returnField= field;
                            break;
                        }
                }
                return returnField;
            }
         
        }

        /// <summary>
        /// Adds the specified text value with the specified value to the 'TextFieldCollection'.
        /// </summary>
        /// <param name="texValue">The text value to add.</param>
        /// <returns>The index at which the new element was inserted.</returns>
        public int Add(TextField texValue)
        {
            return List.Add(texValue);
        }


        /// <summary>
        /// ACopies the elements of an array at the end of this instance of 'TextFieldCollection'.
        /// </summary>
        /// <param name="texValue">An array of 'TextField' objects to add to the collection.</param>
        public void AddRange(TextField[] texValue)
        {
            int intCounter = 0;
            while ((intCounter < texValue.Length))
            {
                Add(texValue[intCounter]);
                intCounter = (intCounter + 1);
            }
        }


        /// <summary>
        /// Adds the contents of another 'TextFieldCollection' at the end of this instance.
        /// </summary>
        /// <param name="texValue">A 'TextFieldCollection' containing the objects to add to the collection.</param>
        public void AddRange(TextFieldCollection texValue)
        {
            int intCounter = 0;
            while ((intCounter < texValue.Count))
            {
                Add(texValue[intCounter]);
                intCounter = (intCounter + 1);
            }
        }


        /// <summary>
        /// Gets a value indicating whether the 'TextFieldCollection' contains the specified value.
        /// </summary>
        /// <param name="texValue">The item to locate.</param>
        /// <returns>
        /// 	<c>true</c> if [contains] [the specified text value]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(TextField texValue)
        {
            return List.Contains(texValue);
        }


        /// <summary>
        /// Copies the 'TextFieldCollection' values to a one-dimensional System.Array instance starting at the specified array index.
        /// </summary>
        /// <param name="texArray">The one-dimensional System.Array that represents the copy destination.</param>
        /// <param name="intIndex">The index in the array where copying begins.</param>
        public void CopyTo(TextField[] texArray, int intIndex)
        {
            List.CopyTo(texArray, intIndex);
        }


        /// <summary>
        /// Returns the index of a 'TextField' object in the collection.
        /// </summary>
        /// <param name="texValue">The 'TextField' object whose index will be retrieved.</param>
        /// <returns>If found, the index of the value; otherwise, -1.</returns>
        public int IndexOf(TextField texValue)
        {
            return List.IndexOf(texValue);
        }


        /// <summary>
        /// Inserts an existing 'TextField' into the collection at the specified index.
        /// </summary>
        /// <param name="intIndex">The zero-based index where the new item should be inserted.</param>
        /// <param name="texValue">The item to insert.</param>
        public void Insert(int intIndex, TextField texValue)
        {
            List.Insert(intIndex, texValue);
        }


        /// <summary>
        /// Returns an enumerator that can be used to iterate through the 'TextFieldCollection'.
        /// </summary>
        /// <returns>Returns an enumerator</returns>
        public new TextFieldEnumerator GetEnumerator()
        {
            return new TextFieldEnumerator(this);
        }


        /// <summary>
        /// Removes a specific item from the 'TextFieldCollection'.
        /// </summary>
        /// <param name="texValue">The item to remove from the 'TextFieldCollection'.</param>
        public void Remove(TextField texValue)
        {
            List.Remove(texValue);
        }


        /// <summary>
        /// A strongly typed enumerator for 'TextFieldCollection'
        /// </summary>
        public class TextFieldEnumerator : object, IEnumerator

        {
            private IEnumerator iEnBase;

            private IEnumerable iEnLocal;

            /// <summary>
            /// Enumerator constructor
            /// Initializes a new instance of the <see cref="T:TextFieldEnumerator"/> class.
            /// </summary>
            /// <param name="texMappings">The tex mappings.</param>
            public TextFieldEnumerator(TextFieldCollection texMappings) : base()
            {
                iEnLocal = texMappings;
                iEnBase = iEnLocal.GetEnumerator();
            }

            /// <summary>
            /// Gets the current element in the collection.
            /// </summary>
            /// <value></value>
            /// <returns>The current element in the collection.</returns>
            /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
            public TextField Current
            {
                get { return ((TextField) (iEnBase.Current)); }
            }

            /// <summary>
            /// Gets the system_ collections_ I enumerator_ current.
            /// </summary>
            /// <value>The system_ collections_ I enumerator_ current.</value>
            public object System_Collections_IEnumerator_Current
            {
                get { return iEnBase.Current; }
            } // interface properties implemented by System_Collections_IEnumerator_Current
            object IEnumerator.Current
            {
                get { return System_Collections_IEnumerator_Current; }
            }


            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            public bool MoveNext()
            {
                return iEnBase.MoveNext();
            }


            /// <summary>
            /// Advances the enumerator to the next element of the collection
            /// System_s the collections_ I enumerator_ move next.
            /// </summary>
            /// <returns></returns>
            public bool System_Collections_IEnumerator_MoveNext()
            {
                return iEnBase.MoveNext();
            }

            /// <summary>
            /// interface methods implemented by System_Collections_IEnumerator_MoveNext
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            bool IEnumerator.MoveNext()
            {
                return System_Collections_IEnumerator_MoveNext();
            }


            /// <summary>
            /// Sets the enumerator to the first element in the collection
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            public void Reset()
            {
                iEnBase.Reset();
            }


            /// <summary>
            /// Sets the enumerator to the first element in the collection
            /// System_s the collections_ I enumerator_ reset.
            /// </summary>
            public void System_Collections_IEnumerator_Reset()
            {
                iEnBase.Reset();
            }

            /// <summary>
            /// interface methods implemented by System_Collections_IEnumerator_Reset
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
            void IEnumerator.Reset()
            {
                System_Collections_IEnumerator_Reset();
            }
        }
    }

    #endregion
}