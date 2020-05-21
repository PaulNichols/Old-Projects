<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
    <xsl:variable name='blank'><xsl:text>&#32;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	
    <!-- Process the XML Document to produce the Norwich Union HUPS text file (FD43HUPS.txt) -->
    <xsl:template match="*">
        <xsl:call-template name="Header"/>
        <xsl:apply-templates select="ReleasedFunds"/>
        <xsl:call-template name="Footer"/>
    </xsl:template>

    <!-- Render the header row-->
	<!--
		HEADER FORMAT
		==================================================
		Field Name			Value		Format		Length
		==================================================
		Record Code	-Made up of(		
		Dummy type			Space						1
		Dummy code			"0000"		X(4)			4
		)
		Separator			","			X(1)			1
		Price date			DDMMCCYY	X(8)			8
	-->
	
	  <xsl:template name="Header">
		<xsl:value-of select='$blank' />
		<xsl:text>0000</xsl:text>
        <xsl:value-of select='$delimiter' />
		
		<xsl:call-template name="format-date">
            <xsl:with-param name="date" select="ReleasedFunds/valuationDate" />
        </xsl:call-template>	
        
        <!-- Throw a line -->
        <xsl:value-of select="$newline" />
    </xsl:template>

 <!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		====================================================================================
		Field Name									Value				Format		Length
		====================================================================================
		Fund Type								"HUPS"				X(1)				1
		Fund Code								Numeric				X(4)				4
		Fund balance							Zero 				X(15)				15
		Separator								","					X					1
		Undivided share - creation price		Zero 				X(11)				11
		Separator								","					X					1
		Undivided share - cancellation price	Zero 				X(11)				11
		Separator								","					X					1
		Accumulation unit , bid price			Bid price			X(11)				11
		Separator								","					X					1
		Accumulation unit , offer price			Offer price			X(11)				11
		Separator								","					X					1
		Accumulation unit , bare price			Bare price			X(11)				11
		Separator								","					X					1
		Spare									Zero 				X(23)				23
		Separator								","					X					1
		Income unit , bid price					Zero 				X(11)				11
		Separator								","					X					1
		Income unit , offer price				Zero				X(11)				11
		Separator								","					X					1
		Income unit , bare price				Zero				X(11)				11
		Separator								","					X					1
		Spare									Zero 				X(23)				23
		Separator								","					X					1
		Income variant							Zero 				X(4)				4
		Separator								","					X					1
		Spare									Zero 				X(6)				6
		Separator								","					X					1
		Variance indicator						"X"					X					1
	-->
	
	 <xsl:template match="ReleasedFunds">
        <!-- Fund Code -->        
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='5'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/> <!--Pad with zeros -->
			<xsl:with-param name='value' 	select='ExternalFundIdentifier'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Fund Balance -->        
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='15'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
			<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Creation price -->        
		<xsl:value-of select='translate(format-number(CreationPrice, "00000.000000"), ".", "")' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Cancellation price -->        
		<xsl:value-of select='translate(format-number(CancellationPrice, "00000.000000"), ".", "")' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Accumulation Bid Price -->        
		<xsl:value-of select='translate(format-number(AccumulationBidPrice, "00000.000000"), ".", "")' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Accumulation Offer Price -->        
		<xsl:value-of select='translate(format-number(AccumulationOffer, "00000.000000"), ".", "")' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Accumulation Bare Price -->        
		<xsl:value-of select='translate(format-number(AccumulationBarePrice, "00000.000000"), ".", "")' />
			
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Output Spare -->        
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='23'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
			<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Income Bid Price -->        
		<xsl:value-of select='translate(format-number(IncomeBidPrice, "00000.000000"), ".", "")' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Income Offer Price -->        
		<xsl:value-of select='translate(format-number(IncomeOfferPrice, "00000.000000"), ".", "")' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Income Bare Price -->        
		<xsl:value-of select='translate(format-number(IncomeBarePrice, "00000.000000"), ".", "")' />
			
		<!-- Output Delimiter --> 
		<xsl:value-of select='$delimiter' />
		
		<!-- Output Spare -->        
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='23'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
			<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Output Delimiter -->   
		<xsl:value-of select='$delimiter' />
		
		<!-- Output Income variant -->        
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='4'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
			<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Output Delimiter -->   
		<xsl:value-of select='$delimiter' />	
					
		<!-- Output Spare -->        
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='6'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/> <!--Pad with zeros -->
			<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Output Delimiter -->   
		<xsl:value-of select='$delimiter' />
		
		<!-- Output Variance Indicator -->   
		<xsl:text>X</xsl:text>
		
		<xsl:value-of select="$newline" />			
    </xsl:template>
	 
	 
 <!-- Render the footer row-->
	<!--
		FOOTER FORMAT
		==================================================
		Field Name			Value		Format		Length
		==================================================
		Record Code	-Made up of(		
		Dummy type			"Z"							1
		Dummy code			"9999"		X(4)			4
		)
		Seperator			","			X				1
		Number of records				X(4)			4
		Spare							X(6)			6
	-->
	
    <xsl:template name="Footer">
        <xsl:text>Z9999</xsl:text>
        
		<xsl:value-of select='$delimiter' />
        
		<!-- Output the number of rows-->
        <xsl:value-of select="format-number(count(//ReleasedFunds/ExternalFundIdentifier), '0000')"/>
		
		<!-- Output Spare -->        
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"N"'/> 
				<xsl:with-param name='max' 		select='6'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with zeros -->
				<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
			</xsl:call-template>
    </xsl:template>

  
  
	   <!--            TEMPLATE FUNCTIONS             -->
    
    <!-- Template: format-date -->
    <!-- Format date from ddmmccyy to dd/mm/yy -->
    <xsl:template name="format-date">
        <xsl:param name="date" />

        <!-- Pick out date parts -->
        <xsl:variable name="day" select="substring($date, 1, 2)" />
        <xsl:variable name="month" select="substring($date, 3, 2)" />
        <xsl:variable name="century" select="substring($date, 5, 2)" />
        <xsl:variable name="year" select="substring($date, 7, 2)" />

        <!-- Concatinate date parts with seperators -->
        <xsl:value-of select="concat($day, $month, $century, $year)" />
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