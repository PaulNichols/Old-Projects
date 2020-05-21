<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>

    <!-- Process the XML Document to produce a GAF text file -->
    <xsl:template match="*">
        <xsl:apply-templates select="ReleasedFunds"/>
    </xsl:template>

	<!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		========================================
		Field Name				Value				
		========================================
		GAF Code
		Bid Price		
	-->
	
	 <xsl:template match="ReleasedFunds">
		<xsl:text>"</xsl:text>
		<!-- HiPortfolio/3 Code -->
		<xsl:value-of select='ExternalFundIdentifier' />
		<xsl:text>"</xsl:text>
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Bid Price -->
		<xsl:if test="bidPrice >= 0 ">
			<xsl:value-of select="concat('+',format-number(bidPrice, '00000.0'))" />
		</xsl:if>
		<xsl:if test="bidPrice &lt; 0 ">
			<xsl:value-of select="format-number(bidPrice, '00000.0')" />
		</xsl:if>
		<xsl:text>000</xsl:text>
		
		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
		
    </xsl:template>
</xsl:stylesheet>