Public Interface ICounterProvider

    Sub CreateCounters(ByVal counters As EnterpriseCounters)
    Sub CountersCreated(ByVal counters As EnterpriseCounters)

End Interface
