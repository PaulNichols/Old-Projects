<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

	<!-- define constants -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	
    <!-- Process the XML Document to produce a P182 text file -->
    <xsl:template match="*">
        <xsl:apply-templates select="ReleasedFunds"/>
    </xsl:template>

	<!-- Render the detail rows -->
	<!--
		 For Initial Prices
		 
		DETAILS FORMAT
		=============================================================================================
		Field Name				Value					Format			Length			
		=============================================================================================
		Management type			Company no  01			9(2)			2
		Fund Number				(Fund no + 200)			9(3)			3
		Valuation date			Price date in CCYYMMDD 
								format					9(8)			8
		Fund Value				Zero					9(13)			13
		Number of Office 
			Units held			Zero					9(13)			13
		Chosen Unrounded price	Zero					9(13)			13
		Published Bid price 
			in pounds			Bid price / 100			9(3)V9(3)		6
		Filler					Zero					9(12)			12
		Broker ID letter		Zero					9(1)			1
		No of Broker held 
			Office units		Zero					9(13)			13
		Policy holder ID 
			letter				Zero					9(1)			1
		No of Policy-holder 
			held Office units	Zero					9(13)			13
		Composite ID letter		Zero					9(1)			1
		Composite units			Zero					9(13)			13
		Filler					Zero					9(12)			12
		Published Offer price 
			in pounds			Offer price / 100		9(3)V9(3)		6
		Equitable ID letter		Zero					9(1)			1
		Number of Equitable 
			held Office units	Zero					9(13)			13
		Additional Source 
			1 ID letter			Zero					9(1)			1
		Number of Source 
			1 held Office units	Zero					9(13)			13
		Additional Source 
			2 ID letter			Zero					9(1)			1
		Number of Source 
			2 held Office units	Zero					9(13)			13
		Additional Source
			3 ID letter			Zero					9(1)			1
		Number of Source 
			3 held Office units	Zero					9(13)			13
		Additional Source 
			4 ID letter			Zero					9(1)			1
		Number of Source 
			4 held Office units	Zero					9(13)			13
		Additional Source 
			5 ID letter			Zero					9X(1)			1
		Number of Source 
			5 held Office units	Zero					9(13)			13
	-->
	
	 <xsl:template match="ReleasedFunds">
        <xsl:if test="string(ExternalFundIdentifier) ">
			
			<!-- Bid Price -->        
			<xsl:variable name='BidPriceInPounds' select='translate(format-number(bidPrice , "000.000"), ".", "")'></xsl:variable>
			
			<!-- Offer Price -->        
			<xsl:variable name='OfferPriceInPounds' select='translate(format-number(offerPrice , "000.000"), ".", "")'></xsl:variable>
			
			<!-- OfficeUnits -->
		<xsl:variable name='OfficeUnits' select='policyHolderUnits + compositeUnits + equitableUnits'></xsl:variable>
			
			<!-- Management Type -->
			<xsl:text>01</xsl:text>

			<xsl:if test="isInitialUnits=1">
				<!-- Fund Number-->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"R"'/> 
					<xsl:with-param name='trunc'	select='"Y"'/> 
					<xsl:with-param name='max' 		select='3'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='ExternalFundIdentifierAsNumeric + 200'/>
				</xsl:call-template>
				
				<!-- Valuation Date-->
				<xsl:call-template name="format-date">
					<xsl:with-param name="date" select="valuationDate" />
				</xsl:call-template>		
				
				<!-- Fund Value -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Number of Office Units held	 -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Chosen Unrounded Price -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Bid Price -->        
				<xsl:value-of select='$BidPriceInPounds' />
					
				<!-- Filler -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='12'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Broker ID letter -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- No of Broker held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Policy holder ID letter -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- No of Policy-holder held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Composite ID letter -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Composite units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Filler -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='12'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Offer Price -->   
				<xsl:value-of select='$OfferPriceInPounds' />
				
				<!-- Equitable ID letter -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Number of Equitable held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Additional Source 1 ID letter -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Number of Source 1 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Additional Source 2 ID letter -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Number of Source 2 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Additional Source 3 ID letter -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Number of Source 3 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Additional Source 4 ID letter -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Number of Source 4 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Additional Source 5 ID letter -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Number of Source 5 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
			</xsl:if>
			
			<xsl:if test="isInitialUnits=0">
				<!-- Fund Number-->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"R"'/> 
					<xsl:with-param name='trunc'	select='"Y"'/> 
					<xsl:with-param name='max' 		select='3'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='ExternalFundIdentifierAsNumeric'/>
				</xsl:call-template>
					
				<!-- Valuation Date -->
				<xsl:call-template name="format-date">
					<xsl:with-param name="date" select="valuationDate" />
				</xsl:call-template>	
				
				<!-- Fund Value -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"R"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='translate(format-number(FundValue, "0000000000.00"), ".", "")'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>

				<!-- Number of Office Units held -->
				<xsl:value-of select='translate(format-number($OfficeUnits, "0000000000.000"), ".", "")' />
				
				<!-- Unrounded price -->
				<xsl:value-of select='translate(format-number(unroundedPrice, "000.0000000000"), ".", "")' />
				
				<!-- Bid Price -->        
				<xsl:value-of select='$BidPriceInPounds' />
				
				<!-- Filler -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='12'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Broker ID letter -->
				<xsl:text>B</xsl:text>
				
				<!-- Number of Broker Units held -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Policy holder ID letter -->
				<xsl:text>P</xsl:text>
				
				<!-- Number of Policy holder Units held -->
				<xsl:value-of select='translate(format-number(policyHolderUnits, "0000000000.000"), ".", "")' />
				
				<!-- Composite ID letter -->
				<xsl:text>F</xsl:text>
				
				<!-- Number of Composite Units held -->
				<xsl:value-of select='translate(format-number(compositeUnits, "0000000000.000"), ".", "")' />
				
				<!-- Filler -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='12'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Offer Price -->  
				<xsl:value-of select='$OfferPriceInPounds' />
				
				<!-- Equitable  ID letter -->
				<xsl:text>M</xsl:text>
				
				<!-- Number of Equitable Units held -->
				<xsl:value-of select='translate(format-number(equitableUnits, "0000000000.000"), ".", "")' />
				
				<!-- Additional Source 1 ID letter -->
				<xsl:text>V</xsl:text>
				
				<!-- Number of Source 1 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Additional Source 2 ID letter -->
				<xsl:text>W</xsl:text>
				
				<!-- Number of Source 2 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Additional Source 3 ID letter -->
				<xsl:text>X</xsl:text>
				
				<!-- Number of Source 3 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Additional Source 4 ID letter -->
				<xsl:text>Y</xsl:text>
				
				<!-- Number of Source 4 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
				
				<!-- Additional Source 5 ID letter -->
				<xsl:text>Z</xsl:text>
				
				<!-- Number of Source 5 held Office units -->
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"N"'/> 
					<xsl:with-param name='max' 		select='13'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
					<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
				</xsl:call-template>
			</xsl:if>
		
			<!-- Carriage return -->
			<xsl:value-of select='$newline' />
        </xsl:if>

    </xsl:template>
	 
	 
	   <!--            TEMPLATE FUNCTIONS             -->
	  <xsl:template name="format-date">
        <xsl:param name="date" />

        <!-- Pick out date parts -->
        <xsl:variable name="day" select="substring($date, 1, 2)" />
        <xsl:variable name="month" select="substring($date, 3, 2)" />
        <xsl:variable name="century" select="substring($date, 5, 2)" />
        <xsl:variable name="year" select="substring($date, 7, 2)" />

        <!-- Concatinate date parts with seperators -->
        <xsl:value-of select="concat($century, $year, $month, $day)" />
    </xsl:template>
    
    <!--  Pad Field  -->
    <xsl:template name="padString">
    <xsl:param name="justify"/><!-- R OR L JUSTIFY --><!--Make sure you enclose "L" or "R" in quotes -->
    <xsl:param name="trunc"/>  <!-- Y OR N -->        <!--Make sure you enclose "Y" or "N" in quotes -->
    <xsl:param name="max" />   <!-- LENGTH OF OUTPUT FIELD -->
    <xsl:param name="char" />  <!-- CHARACTER TO PAD WITH.  USUALLY EITHER ZERO OR SPACE -->
    <xsl:param name="value" /> <!-- STARTING VALUE OF FIELD -->

    <!-- TRIM SPACES -->
    <xsl:variable name="trim-value"><xsl:value-of select="normalize-space($value)"/></xsl:variable>

    <xsl:if test="($justify = 'L')">
        <xsl:if test="($trunc = 'Y')"><!-- if left justify and truncate, select left-most value-->
        <xsl:value-of select="substring($trim-value,1,$max)" />
        </xsl:if>
        <xsl:if test="($trunc = 'N')">
        <xsl:value-of select="$trim-value" />
        </xsl:if>
    </xsl:if>

    <!-- if left justified, it writes the value first, then the padding  -->
    <!-- if right justified, it writes the padding first, then the value -->

    <xsl:if test="(string-length($trim-value) &lt; $max)"><!--If string below max, pad -->
        <xsl:call-template name="pad">
        <xsl:with-param name="num"   select="0"/>
        <xsl:with-param name="max"   select="$max - string-length($trim-value)"/>
        <xsl:with-param name="char"  select="$char"/>
        </xsl:call-template>
    </xsl:if>
    <xsl:if test="($justify = 'R')">
        <xsl:if test="$trunc='Y'">
        <xsl:choose>
            <xsl:when test="$max &lt; string-length($trim-value)"><!-- if right justify and truncate, select right-most value-->
            <xsl:value-of select="substring($trim-value, string-length($trim-value)-$max+1, string-length($trim-value))" />
            </xsl:when>
            <xsl:otherwise>
            <xsl:value-of select="$trim-value"/>
            </xsl:otherwise>
        </xsl:choose>
        </xsl:if>
        <xsl:if test="($trunc = 'N')">
        <xsl:value-of select="$trim-value" />
        </xsl:if>
    </xsl:if>
    </xsl:template>
	
	 <!--                PAD                     -->
    <xsl:template name="pad">
    <xsl:param name="num" />
    <xsl:param name="max" />
    <xsl:param name="char" />

    <xsl:if test="not($num = $max)">
        <xsl:value-of select="$char"/>
        <xsl:call-template name="pad">
        <xsl:with-param name="num" select="$num + 1"/>
        <xsl:with-param name="max" select="$max"/>
        <xsl:with-param name="char" select="$char"/>
        </xsl:call-template>
    </xsl:if>
    </xsl:template>
	
</xsl:stylesheet>