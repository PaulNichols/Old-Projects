<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>
	
    <!-- Process the XML Document to produce a Marlborough Sterling text file -->
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
		Short Name				
		Full Name
		Bid Price
		Movement on Previous 
			Days bid price
		MEXID
		ISO Currency Code
					
	-->
	
	 <xsl:template match="ReleasedFunds">
        <xsl:if test="string(ExternalFundIdentifier) ">
			<!-- Short Name -->
			<xsl:value-of select='shortName' />
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />	
			<!-- Full Name -->
			<xsl:value-of select='fullname' />
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />	
			<!-- Bid Price -->        
			<xsl:value-of select='format-number(bidPrice, "#.00")' />
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />
			<!-- Movement -->
			<xsl:if test="changePrice >= 0 ">
				<xsl:value-of select="concat('+',format-number(changePrice, '#.00'))" />
			</xsl:if>
			<xsl:if test="changePrice &lt; 0 ">
				<xsl:value-of select="format-number(changePrice, '#.00')" />
			</xsl:if>
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />	
			<!-- MEXID -->
			<xsl:value-of select='ExternalFundIdentifier' />
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />	
			<!-- ISO Currency Code -->
			<xsl:value-of select='ftCurrency' />
		
			<!-- Carriage return -->
			<xsl:value-of select='$newline' />
        </xsl:if>
		
    </xsl:template>
</xsl:stylesheet>