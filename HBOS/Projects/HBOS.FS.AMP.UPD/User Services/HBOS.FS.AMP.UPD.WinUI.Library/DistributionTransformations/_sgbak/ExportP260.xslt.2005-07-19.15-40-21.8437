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
	
    <!-- Process the XML Document to produce a P260.txt text file  -->
     <xsl:template match="*">
        <xsl:call-template name="Header"/>
        <xsl:apply-templates select="ReleasedFunds"/>
        <xsl:call-template name="Footer"/>
    </xsl:template>

    <!-- Render the header row-->
	<!--
		HEADER FORMAT
		==========================================================
		Field Name			Value			Format		Length
		==========================================================
		Record Id			"HDR"			X(3)		3
		Fund Valuation Date	Valuation 
							date in			
							DDMMCCYY 
							format			9(8)		8
		Filler				n/a				X(22)		22
		Carriage Return		n/a				X(1)		1
	-->
	
	  <xsl:template name="Header">
		<!-- Record Id -->      
		<xsl:text>HDR</xsl:text>
		<!-- Fund Valuation Date -->      
		   <xsl:value-of select="user:returnDate()" />
	  	<!-- Filler -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='22'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/><!--Pad with zeros -->
			<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
        <!-- Carriage return -->
		<xsl:value-of select='$newline' />
    </xsl:template>

	<!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		=====================================================================
		Field Name				Value					Format		Length		
		=====================================================================
		Record Id				"DTL"					X(3)		3
		Fund Number				Clerical Medical 
								fund code				9(5)		5
		Unit Price				Bid price in pence		9(8)V9(3)	11
		Filler					n/a						X(14)		14
		Carriage Return			n/a						X(1)		1
	-->
	
	 <xsl:template match="ReleasedFunds">
        <xsl:if test="string(ExternalFundIdentifier) ">
			<!-- Record Id -->      
			<xsl:text>DTL</xsl:text>
			<!-- Fund Code -->
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"R"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='5'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/><!--Pad with zeros -->
				<xsl:with-param name='value' 	select='ExternalFundIdentifier'/><!--There isn't a field to select so I've just enterd any old thing -->
			</xsl:call-template>
			<!-- Bid Price -->        
			<xsl:value-of select='translate(format-number(bidPrice, "00000000.000"), ".", "")' />
			<!-- Filler -->      
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"N"'/> 
				<xsl:with-param name='max' 		select='14'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/><!--Pad with zeros -->
				<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
			</xsl:call-template>
			<!-- Carriage return -->
			<xsl:value-of select='$newline' />
        </xsl:if>
		
    </xsl:template>

	<!-- Render the footer row-->
	<!--
		FOOTER FORMAT
		==============================================================
		Field Name			Value				Format		Length
		==============================================================
		Record Id			"TLR"				X(3)		3
		Record Count		Total number of 
							detail records 
							on the file			9(6)		6
		Total Fund Number	Total of all Fund 
							Numbers on "DTL" 
							records				9(12)		12
				
		Total				Total of all bid
		Published Price		prices on "DTL"
							records (pence)		9(9)V9(3)	12
		Carriage Return		n/a					X(1)		1
	-->
	
    <xsl:template name="Footer">
		<!-- Record Id -->     
        <xsl:text>TLR</xsl:text>
        <!-- Output the number of rows in the DTL section -->
		<xsl:value-of select="format-number(count(//ReleasedFunds/ExternalFundIdentifier), '000000')"/>
		<!-- Output the Sum of the Fund Numbers (this sum has been done in the db but really shouldn't have-->
		
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"R"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='12'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/><!--Pad with zeros -->
			<xsl:with-param name='value' 	select='sum(//ReleasedFunds/ExternalFundIdentifierAsNumeric)'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Output the Total Published Price -->
		<xsl:value-of select='translate(format-number(sum(//ReleasedFunds/bidPrice), "000000000.000"), ".", "")' />
		
		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
    </xsl:template>
	
	
<!--            SCRIPTING FUNCTIONS            -->
    
    <!-- Get todays date in required format -->
    <msxsl:script language="C#" implements-prefix="user">
        <![CDATA[
        public string returnDate()
        {
            return ( string.Format("{0:ddMMyyyy}", System.DateTime.Today) );
        }
		
	
        ]]>
    </msxsl:script>

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