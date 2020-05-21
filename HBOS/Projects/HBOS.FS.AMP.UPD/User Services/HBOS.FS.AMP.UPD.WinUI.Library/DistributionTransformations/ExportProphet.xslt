<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>
	
    <!-- Process the XML Document to produce a Prophet text file (FPRCR.int) -->
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
    
	<!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		==========================================================================================================================
		Field Name				Value					Format			Length			Mandatory/Optional			Field delim
		==========================================================================================================================
		CMED Fund Code			Clerical Medical
								fund code				Variable		Variable		Mandatory					,
		Valuation Date			dd/mm/yyy				Variable		Variable		Mandatory					,
		Valuation Time			hh:mm					Variable		Variable		Mandatory					,
		Bid Price 
		of Accumulation Units	Bid price of the 
								fund where the 
								unit type is 2 
								(accumulation units)	Variable		Variable		Mandatory					,
		Bid price of the fund 
		where the unit type is 
		2 (accumulation units)	Bid price of the fund 
								where the unit type is 
								1 (initial units if 
								present)				Variable		Variable		Optional					,
	
		End of Row				C/R						X	1			Mandatory	 
					
	-->
	
	 <xsl:template match="ReleasedFunds">
        <xsl:if test="string(ExternalFundIdentifier) ">
			<xsl:if test="isInitialUnits=0"><!-- Accumulation -->
				<!-- Fund Code -->
				<xsl:value-of select='ExternalFundIdentifier' />
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />	
				<!-- Valuation Date-->
				<xsl:call-template name="format-date">
					<xsl:with-param name="date" select="valuationDate" />
				</xsl:call-template>		
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />	
				<!-- Valuation Time-->
				<xsl:call-template name="format-time">
					<xsl:with-param name="time" select="valuationTime" />
				</xsl:call-template>		
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />	
				<!-- Bid Price -->        
				<xsl:value-of select='format-number(bidPrice, "#.0")' />
			
				<xsl:if test="position()!=last() and following::isInitialUnits=1 and following::ExternalFundIdentifier=ExternalFundIdentifier"><!-- Initial -->
					
					<!-- Output Delimiter -->
					<xsl:value-of select='$delimiter' />	

					<!-- Bid Price -->        
					<xsl:value-of select='format-number(following::bidPrice, "#.0")' />
				</xsl:if>
				
				<!-- Carriage return -->
				<xsl:value-of select='$newline' />
			</xsl:if>
		</xsl:if>
    </xsl:template>

 
	<!--                                                -->
    <!--            TRANSFORMATION FUNCTIONS            -->
    <!--                                                -->
    
    <!-- Template: format-date -->
    <!-- Format date from ddmmccyy to dd/mm/yyyy -->
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
	
	 <xsl:template name="format-time">
        <xsl:param name="time" />

        <!-- Pick out date parts -->
        <xsl:variable name="hour" select="substring($time, 1, 2)" />
        <xsl:variable name="mins" select="substring($time, 3, 2)" />

        <!-- Concatinate date parts with seperators -->
        <xsl:value-of select="concat($hour, ':', $mins)" />
    </xsl:template>
</xsl:stylesheet>