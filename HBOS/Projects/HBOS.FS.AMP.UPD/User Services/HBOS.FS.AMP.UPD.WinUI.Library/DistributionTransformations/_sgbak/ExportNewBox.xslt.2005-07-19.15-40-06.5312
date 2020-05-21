<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>
	
    <!-- Process the XML Document to produce a New Box text file (FPRCR.int) -->
    <xsl:template match="*">
        <xsl:call-template name="Header"/>
        <xsl:apply-templates select="ReleasedFunds"/>
    </xsl:template>

    <!-- Render the header row-->
	<!--
		HEADER FORMAT
		====================================================================================================
		Field Name			Value				Format		Length	Mandatory/Optional	Field delim
		====================================================================================================
		Record Id			"HDR"				X(3)		3			Mandatory			,
		File Id	
		If regular			"FPRCR"				5						Mandatory			,
		If historical		"FPRCH"					
		Creating System		P219				X(4)		4			Mandatory			,
		Creating Program	UP0038S				X(7)		7			Mandatory			,
		Creation Date		DDMMCCYY format		9(8)		8			Mandatory			,
		Creation Time		HHMMSS format		9(6)		6			Mandatory			,
		Effective Date		DDMMCCYY format		9(8)		8			Mandatory			,
		Effective Time		HHMMSS format		9(6)		6			Mandatory			,
		No of Rows			Number of 
							records in 
							the file			variable	variable	Mandatory			,
		No of Cols			10					1			1			Mandatory	 
		End of Row			C/R					X(1)		1			Mandatory	 

	-->
	
	  <xsl:template name="Header">
		<!-- Record Id -->      
		<xsl:text>HDR</xsl:text>
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		<!-- File Id -->      
		<xsl:text>FPRCR</xsl:text>
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
		<xsl:value-of select="count(//ReleasedFunds[isInitialUnits=0])"/>
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
		==========================================================================================================================
		Field Name				Value					Format			Length			Mandatory/Optional			Field delim
		==========================================================================================================================
		Fund Number				Clerical Medical
								fund code				variable		variable		Mandatory					,
		Fund Name				Alpha					Variable		Variable		Mandatory					,
		Published Bid Price 
		of Accumulation Units	Bid price of the 
								fund where the 
								unit type is 2 
								(accumulation units)	Variable		Variable		Mandatory					,
		Published Offer Price 
		of Accumulation Units	Offer price of the 
								fund where the unit 
								type is 2 
								(accumulation units)	Variable		Variable		Optional					,
		Change in Published 
		Price of Accumulation 
		Units					Difference in bid 
								price of the fund 
								between today and 
								yesterday where the 
								unit type is 2 
								(accumulation units)	Variable		Variable		Mandatory					,
		% Change in Published 
		Price of Accumulation 
		Units					The change in published 
								price of accumulation 
								units expressed as a 
								percentage				Variable		Variable		Mandatory					,
		Bid price of the fund 
		where the unit type is 
		2 (accumulation units)	Bid price of the fund 
								where the unit type is 
								1 (initial units if 
								present)				Variable		Variable		Optional					,
		Offer price of the fund
		where the unit type is 
		1 (initial units if 
		present)				Offer price of the 
								fund where the unit 
								type is 1 (initial 
								units if present)		Variable		Variable		Optional					,
		Change in Published 
		Price of Initial Units	Difference in bid 
								price of the fund 
								between today and 
								yesterday where the 
								unit type is 1 
								(initial units)			Variable		Variable		Optional					,
		% Change in Published 
		Price of Initial Units	The change in published 
								price of initial units 
								expressed as a 
								percentage				Variable		Variable		Optional			
		End of Row				C/R						X	1			Mandatory	 
					
	-->
	
	 <xsl:template match="ReleasedFunds">
	 
		 <xsl:if test="number(isInitialUnits)=0">
				<!-- Fund Code -->
				<xsl:value-of select='ExternalFundIdentifier' />
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />	
				<!-- Fund Name -->
				<xsl:value-of select='shortName' />
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />	
		
				<!-- Bid Price -->        
				<xsl:value-of select='format-number(bidPrice, "#.000000")' />
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />	
				<!-- Offer Price -->        
				<xsl:value-of select='format-number(offerPrice, "#.000000")' />
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />	
				<!-- Change in Price -->        
				<xsl:value-of select='format-number(changePrice, "#.000000")' />
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />	
				<!--  Price -->        
				<xsl:value-of select='format-number(BidPriceMvt, "#.000000")' />
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />	
				
				<xsl:if test="position()!=last()">
					<xsl:if test=" number(following::isInitialUnits)=1 and following::ExternalFundIdentifier=ExternalFundIdentifier">
						<!-- Bid Price -->        
						<xsl:value-of select='format-number(following::bidPrice, "#.000000")' />
						<!-- Output Delimiter -->
						<xsl:value-of select='$delimiter' />	
						<!-- Offer Price -->        
						<xsl:value-of select='format-number(following::offerPrice, "#.000000")' />
						<!-- Output Delimiter -->
						<xsl:value-of select='$delimiter' />	
						<!-- Change in Price -->        
						<xsl:value-of select='format-number(following::changePrice, "#.000000")' />
						<!-- Output Delimiter -->
						<xsl:value-of select='$delimiter' />	
						<!--  Price -->        
						<xsl:value-of select='format-number(following::BidPriceMvt, "#.000000")' />
					</xsl:if>
					<xsl:if test="number(following::isInitialUnits)=0">
						<!-- Output Delimiter -->
						<xsl:value-of select='$delimiter' />	
						<!-- Output Delimiter -->
						<xsl:value-of select='$delimiter' />	
						<!-- Output Delimiter -->
						<xsl:value-of select='$delimiter' />	
					</xsl:if>
				</xsl:if>
				
					
				<xsl:if test="position()=last()">
					<!-- Output Delimiter -->
					<xsl:value-of select='$delimiter' />	
					<!-- Output Delimiter -->
					<xsl:value-of select='$delimiter' />	
					<!-- Output Delimiter -->
					<xsl:value-of select='$delimiter' />	
				</xsl:if>
				
				<!-- Carriage return -->
				<xsl:value-of select='$newline' />
		</xsl:if>
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