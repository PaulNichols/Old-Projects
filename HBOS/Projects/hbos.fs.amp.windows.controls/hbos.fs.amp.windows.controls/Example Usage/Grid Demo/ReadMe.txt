For Printing and Extracting the data to a file, you must bind a DataView to the Grid (so you get sorting).
If you want to prevent the user clicking a cell (i.e. get RowSelect functionality), use a DataGridTextBoxReadOnlyColumn
To capture a double click and be aware of the selected row, use CellDoubleClicked
To capture a click and be aware of the selected row, use CellClicked.
To capture the user pressing "Enter", use a KeyUp and compare the KeyCode to Enter. CurrentRowIndex gives you the selected row number.
To extact/write the grid contents, you need an xslt file to do the conversion - it must be an embedded resource.
