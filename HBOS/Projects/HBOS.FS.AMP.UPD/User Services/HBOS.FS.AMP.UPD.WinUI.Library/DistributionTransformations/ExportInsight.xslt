<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>

    <!-- Process the XML Document to produce a Hi3 text file -->
    <xsl:template match="*">
        <xsl:call-template name="Header"/>
        <xsl:apply-templates select="ReleasedFunds"/>
    </xsl:template>
	
	<!-- Render the header-->
	<!--
	HEADER FORMAT
	==============================================================================================
	Field Name			Value			Format		Length		Mandatory/Optional	Field delim
	==============================================================================================
	Record Id			"HDR"			X(3)		3			Mandatory			,
	File Id				If regular		X(6)		6			Mandatory			,
							"CMPGSR"				
						If historical				
							"CMPGSH"				
	Creating System		P219			X(4)		4			Mandatory			,
	Creating Program	UP0038S			X(7)		7			Mandatory			,
	Creation Date		DDMMCCYY format	9(8)		8			Mandatory			,
	Creation Time		HHMMSS format	9(6)		6			Mandatory			,
	Effective Date		DDMMCCYY format	9(8)		8			Mandatory			,
	Effective Time		HHMMSS format	9(6)		6			Mandatory			,
	No of Rows			Number of 
						detail records 
						in the file		variable	variable	Mandatory			,
	No of Cols			10				1			1			Mandatory	 
	End of Row			C/R				X(1)		1			Mandatory
	
	-->
	
	<xsl:template name="Header">
        <!-- Record Id -->
        <xsl:text>HDR</xsl:text>
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- File Id -->
        <xsl:text>CMPGSR</xsl:text>
        
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
        <!-- Creating System -->
        <xsl:text>P219</xsl:text>
         
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		 <!-- Creating Program -->
        <xsl:text>UP0038S</xsl:text>
         
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Creation Date -->      
        <xsl:value-of select="user:returnDate()" />
        
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Creation Time -->      
		<xsl:value-of select="user:returnTime()" />	
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Effective Date, Valuation Date -->
		<xsl:value-of select='ReleasedFunds/valuationDate' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Effective Time, Valuation Time -->
		<xsl:value-of select='ReleasedFunds/valuationTimeLong' />    
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Output Number of Rows -->
		<xsl:value-of select="count(//ReleasedFunds)"/>
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Number of Cols -->      
		<xsl:text>10</xsl:text>
		
        <!-- Carriage return -->
		<xsl:value-of select='$newline' />
		
    </xsl:template>
	
	
	<!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		========================================================================================================
		Field Name				Value				Format		Length		Mandatory/Optional	Field delim
		========================================================================================================
		Fund number (of 
			composite fund)		Numeric				variable	variable	Mandatory			,
		Fund name (of 
			composite fund)		Short composite 
									fund name 		variable	variable	Mandatory			,
		Fund number (of 
			individual fund)	Numeric				variable	variable	Mandatory			,
		Fund name (of 
			individual fund)	Short fund 
									name			variable	variable	Mandatory			,
		% Office Units			% of the 
									composite fund	variable	variable	Mandatory			,
		Office Units			Numeric, zero?		variable	variable	Mandatory			,
		Office Units price		Numeric, zero?		variable	variable	Mandatory			,
		End of Row				C/R					X(1)		1			Mandatory	 
	 

	-->
	
	 <xsl:template match="ReleasedFunds">
		 
		<!-- Fund number (of composite fund) -->
		<xsl:value-of select='assetFundNumber' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
	
		<!-- Fund name (of composite fund) -->
		<xsl:value-of select='assetFundName' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Fund number (of individual fund) -->
		<xsl:value-of select='ExternalFundIdentifier' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Fund name (of individual fund) -->
		<xsl:value-of select='shortName' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- % Office Units -->
		<xsl:value-of select='format-number(weighting, "0.000000")' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- OfficeUnits -->
		<xsl:value-of select='format-number(TotalOfficeUnits, "0.000000")' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Office Units price -->
		<xsl:value-of select='format-number(bidprice, "0.000000")' />
		
		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
		
    </xsl:template>
	
	<!--            SCRIPTING FUNCTIONS            -->
    
    <!-- Get todays date in required format -->
    <msxsl:script language="C#" implements-prefix="user">
        <![CDATA[
        public string returnDate()
        {
            return ( string.Format("{0:ddMMyyyy}", System.DateTime.Today) );
        }
		
		  public string returnTime()
        {
            return ( string.Format("{0:HHmmss}", System.DateTime.Now) );
        }
        ]]>
    </msxsl:script>
</xsl:stylesheet>