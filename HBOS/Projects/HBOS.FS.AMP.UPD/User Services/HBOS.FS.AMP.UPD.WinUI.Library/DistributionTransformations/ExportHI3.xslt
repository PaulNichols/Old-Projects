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
        <xsl:apply-templates select="ReleasedFunds"/>
    </xsl:template>
	
	<!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		========================================
		Field Name				length				
		========================================
		Security Code			12
		Bid Price				7.6
		Offer price				7.6 (possibly blank)
	-->
	
	 <xsl:template match="ReleasedFunds">
		 
		<!-- HiPortfolio/3 Code -->
		<xsl:value-of select='ExternalFundIdentifier' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
	
		<!-- Bid Price -->
		<xsl:value-of select='format-number(bidPrice, "0000000.000000")' />
				
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Offer Price -->
		<xsl:if test="offerPrice=0">
			<xsl:value-of select='format-number(bidPrice, "0000000.000000")' />
		</xsl:if>
		
		<xsl:if test="offerPrice!=0">
			<xsl:value-of select='format-number(offerPrice, "0000000.000000")' />
		</xsl:if>
		
		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
		
    </xsl:template>
	
</xsl:stylesheet>