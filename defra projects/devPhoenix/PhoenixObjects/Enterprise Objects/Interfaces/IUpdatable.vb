Public Interface IUpdatable
    Function SaveChanges() As Boolean
    Property RawDataset() As DataSet

    Property CheckSum() As Int32
End Interface
