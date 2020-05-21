<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>
	<xsl:variable name='blank'><xsl:text>&#32;</xsl:text></xsl:variable>

    <!-- Process the XML Document to produce a Box extract text file -->
    <xsl:template match="*">
		<xsl:call-template name="Header"/>
        <xsl:apply-templates select="ReleasedFunds"/>
    </xsl:template>
	
	<!-- Render the header row-->
	<!--
		HEADER FORMAT
		HiPortfolio/3 Code
		HiPortfolio/3 Series Code
		CMED Code
		CMMF Code
		HECM Code
		HiPortfolio/3 Stock Code
		Sedol Code
		Mellon Fund Code
		Schroders Code
		Fund Currency
		Fund Bid Price
		Fund Offer Price
	-->



  <xsl:template name="Header">
		<xsl:text>HiPortfolio Code</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>Series Code</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>CMED Code</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>CMMF Code</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>HECM Code</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>Stock Code</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>SEDOL Code</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>Mellon Code</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>Fund Currency</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>Bid Price</xsl:text>
		<xsl:value-of select='$delimiter' />
		<xsl:text>Offer Price</xsl:text>
		
		<xsl:value-of select='$newline' />
    </xsl:template>

	<!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		========================================
		Field Name				Value				
		========================================
		HiPortfolio/3 Code
		HiPortfolio/3 Price Series Code
		CMED Code
		CMMF Code
		HECM Code
		Stock Code
		SEDOL Code
		Mellon Code
		Schroders Code
		Fund Currency
		Distributed Price		
	-->
	
	 <xsl:template match="ReleasedFunds">
		 
		<!-- HiPortfolio/3 Code -->
		<xsl:value-of select='HiPortfolioCode' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- HiPortfolio/3 Price Series Code -->
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='2'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/> <!--Pad with zeros -->
			<xsl:with-param name='value' 	select='HiPortfolioPriceSeriesCode'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- CMED Code -->
		<xsl:value-of select='CMEDCode' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
			
		<!-- CMMF Code -->
		<xsl:value-of select='CMMFCode' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- HECM Code -->
		<xsl:value-of select='HECMCode' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Stock Code -->
		<xsl:value-of select='STCKCode' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />		
		
			<!-- SEDOL Code -->
		<xsl:value-of select='SEDOLCode' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />	
		
		<!-- MELON Code -->
		<xsl:value-of select='MELONCode' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />	
		
		<!-- Fund Currency -->
		<xsl:value-of select='FundCurrency' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Distributed Price -->
		<xsl:value-of select='format-number(DistributedPrice, "#.0000")' />
		
		<!-- Output Delimiter -->
		<xsl:value-of select='$delimiter' />
		
		<!-- Distributed Offer Price -->
		<xsl:value-of select='format-number(DistributedOfferPrice, "#.0000")' />
				
		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
		
    </xsl:template>
	
	
	   <!--            TEMPLATE FUNCTIONS             -->
    
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