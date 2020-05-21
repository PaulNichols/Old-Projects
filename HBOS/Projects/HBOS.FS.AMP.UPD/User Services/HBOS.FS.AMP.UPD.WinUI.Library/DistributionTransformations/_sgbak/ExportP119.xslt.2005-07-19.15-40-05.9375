<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>
	
    <!-- Process the XML Document to produce a P119 text file (FPRCR.int) -->
    <xsl:template match="*">
		<xsl:call-template name="Header"/>
        <xsl:apply-templates select="ReleasedFunds"/>
    </xsl:template>

	<xsl:template name="Header">
        <xsl:value-of select="//ReleasedFunds/companyName" />
        <xsl:text> - Valuation Date: </xsl:text>
		<xsl:call-template name="format-date">
			<xsl:with-param name="date" select="//ReleasedFunds/valuationDate" />
		</xsl:call-template>	
		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
    </xsl:template>
    
     <xsl:template name="format-date">
        <xsl:param name="date" />

        <!-- Pick out date parts -->
        <xsl:variable name="day" select="substring($date, 1, 2)" />
        <xsl:variable name="month" select="substring($date, 3, 2)" />
        <xsl:variable name="century" select="substring($date, 5, 2)" />
        <xsl:variable name="year" select="substring($date, 7, 2)" />

        <!-- Concatinate date parts with seperators -->
        <xsl:value-of select="concat($day, '/', $month, '/', $century, $year)" />
    </xsl:template>
	
    
	<!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		=============================================================================================
		Field Name				Value					Format			Length			Field delim
		=============================================================================================
		Fund Number				Clerical Medical
								fund code				variable		variable		,
		Bid Price 				Bid price of the 
								fund 					Variable		Variable		,
		Offer Price 			Offer price of the 
								fund where				Variable		Variable		,
		Price Movement			between yesterday and 
								todays bid price) in 
								pence to 1 decimal place
		End of Row				C/R						X				1				 
					
	-->
	
	 <xsl:template match="ReleasedFunds">
        <xsl:if test="string(ExternalFundIdentifier) ">
			<!-- Fund Code -->
			<xsl:value-of select='fullname' />
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />	
			<!-- Bid Price -->         
			<xsl:value-of select='format-number(bidPrice, "#.0")' />
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />	
			<!-- Offer Price -->        
			<xsl:value-of select='format-number(offerPrice, "#.0")' />
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />	
			<!-- Change in Price -->        
			<xsl:value-of select='format-number(changePrice, "#.0")' />
		
			<!-- Carriage return -->
			<xsl:value-of select='$newline' />
        </xsl:if>
		
    </xsl:template>


</xsl:stylesheet>