<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>
	<xsl:variable name='LessThan'><xsl:text>&lt;&lt;</xsl:text></xsl:variable>
	<xsl:variable name='GreaterThan'><xsl:text>&gt;&gt;</xsl:text></xsl:variable>

    <!-- Process the XML Document to produce an EMail text file -->
    <xsl:template match="*">
		<xsl:call-template name="Header"/>
        <xsl:apply-templates select="ReleasedFunds"/>
    </xsl:template>


 <!-- Render the header row-->
	<!--
		HEADER FORMAT
		===================================
		Field Name			Value			
		===================================
		Fund Code			"Fund Code"			
		Fund Name			"Fund Name" 
		Date				"Date"
		Bid Price			"Bid Price"
		Currency Code		"Currency Code"
		Offer Price			"Offer Price"			
		Movement on Bid		"Movement on Bid"				
	-->
	
	  <xsl:template name="Header">
		<!-- Fund Code -->      
		<xsl:text>Fund Code</xsl:text>
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		<!-- Fund Name -->      
		<xsl:text>Fund Name</xsl:text>
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		<!-- Fund Valuation Date -->      
		<xsl:text>Date</xsl:text>
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		<!-- Currency Codee -->      
		<xsl:text>Curr</xsl:text>
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		<!-- Bid Price -->      
		<xsl:text>Bid Price</xsl:text>
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		<!-- Offer Price -->      
		<xsl:text>Offer Price</xsl:text>
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		<!-- Movement on Bid -->      
		<xsl:text>Movement</xsl:text>
		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
    </xsl:template>

	<!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		========================================
		Field Name				Value				
		========================================
		Fund Code					
		Fund Name			
		Date				
		Currency Code		
		Bid Price			
		Offer Price						
		Movement on Bid			
					
	-->
	
	 <xsl:template match="ReleasedFunds">
        <xsl:if test="string(ExternalFundIdentifier) ">
			<!-- Fund Code -->
			<xsl:value-of select='MEXXCode' />
			
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />
			
			<!-- Short Name -->
			<xsl:value-of select='$LessThan' />
			<xsl:value-of select='shortName' />
			<xsl:value-of select='$GreaterThan' />
			
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />	
			
			<!-- Valuation Date -->
			<xsl:value-of select='$LessThan' />
			<xsl:call-template name="format-date">
                <xsl:with-param name="date" select="valuationDate" />
            </xsl:call-template>			
			<xsl:value-of select='$GreaterThan' />
			
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />
			
			<!-- ISO Currency Code -->
			<xsl:value-of select='ftCurrency' />
			
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />
			
			<xsl:if test="isDual=0">
				<!-- Bid Price -->       
				<xsl:text>M</xsl:text>
				<xsl:if test="ftCurrency = 'GBX'">
					<xsl:value-of select='format-number(bidPrice, "#.0")' />
				</xsl:if>
				<xsl:if test="ftCurrency != 'GBX'">
					<xsl:value-of select='format-number(bidPrice, "#.000")' />
				</xsl:if>
				
			</xsl:if>
			
			<xsl:if test="isDual=1">
				
				<!-- Bid Price -->       
				<xsl:text>B</xsl:text>
				<xsl:if test="ftCurrency = 'GBX'">
					<xsl:value-of select='format-number(bidPrice, "#.0")' />
				</xsl:if>
				<xsl:if test="ftCurrency != 'GBX'">
					<xsl:value-of select='format-number(bidPrice, "#.000")' />
				</xsl:if>
				
				<!-- Output Delimiter -->
				<xsl:value-of select='$delimiter' />
				
				<!-- Offer Price -->        
				<xsl:text>O</xsl:text>
					<xsl:if test="ftCurrency = 'GBX'">
					<xsl:value-of select='format-number(offerPrice, "#.0")' />
				</xsl:if>
				<xsl:if test="ftCurrency != 'GBX'">
					<xsl:value-of select='format-number(offerPrice, "#.000")' />
				</xsl:if>
				
			</xsl:if>
			
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />
			
			<!-- Movement -->
			<xsl:text>C</xsl:text>
			<xsl:if test="changeOfferPrice > 0 ">
				<xsl:if test="ftCurrency = 'GBX'">
					<xsl:value-of select="concat('+',format-number(changeOfferPrice, '#.0'))" />
				</xsl:if>
				<xsl:if test="ftCurrency != 'GBX'">
					<xsl:value-of select="concat('+',format-number(changeOfferPrice, '#.000'))" />
				</xsl:if>
			</xsl:if>
			<xsl:if test="changeOfferPrice &lt; 0 ">
				<xsl:if test="ftCurrency = 'GBX'">
					<xsl:value-of select="concat('-',format-number(changeOfferPrice, '#.0'))" />
				</xsl:if>
				<xsl:if test="ftCurrency != 'GBX'">
					<xsl:value-of select="concat('-',format-number(changeOfferPrice, '#.000'))" />
				</xsl:if>
			</xsl:if>
			<xsl:if test="changeOfferPrice = 0 ">
				<xsl:text>-</xsl:text>
			</xsl:if>
					
					
			<!-- Carriage return -->
			<xsl:value-of select='$newline' />
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
</xsl:stylesheet>