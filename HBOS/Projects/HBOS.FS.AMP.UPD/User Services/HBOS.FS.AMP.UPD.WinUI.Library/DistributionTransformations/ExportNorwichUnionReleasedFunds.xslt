<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 

	<!-- 
		CHANGE CONTROL
		=========================================================================
		#		Author	Date		Description
		=========================================================================
		1.0		SJR		26/0405		Adjustment of field lengths from 11 to 9 for:
									
									Field Name					Old Len		New Len
									===============================================
									Income dealing offer price	11			9
									Income dealing bid price	11			9
									Income dealing mid price	11			9
	-->
        
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
    <xsl:variable name='blank'><xsl:text>&#32;</xsl:text></xsl:variable>

    <!-- Process the XML Document to produce the Norwich Union text file (CGNUUPDT.TXT) -->
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
		Record Type			"H"			X			1
		Price date			DDMMCCYY	X(8)		8
	-->
    <xsl:template name="Header">
        <xsl:text>H</xsl:text>
        <xsl:value-of select="user:returnDate()"/> <!-- Call in line script function -->

        <!-- Throw a line -->
        <xsl:text>&#13;</xsl:text><xsl:text>&#10;</xsl:text>
    </xsl:template>

    <!-- Render the footer row-->
	<!--
		FOOTER FORMAT
		==================================================
		Field Name			Value		Format		Length
		==================================================
		Record Type			"T"			X			1
		Number of records				X(6)		6
	-->
    <xsl:template name="Footer">
        <xsl:text>T</xsl:text>
        
        <!-- Output the number of rows for OEICS -->
        <xsl:value-of select="format-number(count(//ReleasedFunds/ExternalFundIdentifier), '000000')"/>
    </xsl:template>

    <!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		======================================================================
		Field Name						Value				Format		Length
		======================================================================
		Record Type						"D"					X			1
		Fund identifier					"OEICS"				X(16)		16
		Fund class						Share class			X(8)		8
		Valuation date					DDMMCCYY			X(8)		8
		Valuation time					hhmm				X(4)		4
		Currency						ISO code			X(3)		3
		NAV offer price per unit		zero				9(5)(4)		9
		NAV bid price per unit			zero				9(5)(4)		9
		NAV single price per unit		OEIC single price	9(5)(4)		9
		Income equalisation				zero				9(7)(4)		11
		Income dealing offer price		zero				9(5)(4)		9 
		Income dealing bid price		zero				9(5)(4)		9 
		Income delaing mid price		OEIC single price	9(5)(4)		9 
		Base converion factor			always 1			9(6)(9)		15
		Accumulation conversion factor	always zero			9(6)(9)		15
	-->
    <xsl:template match="ReleasedFunds">
        <!-- Test there is a value for the Mainframe against the 'oecis' column -->
        <!-- To test if the element exists and is non-empty -->
        <xsl:if test="string(ExternalFundIdentifier)">
            <!-- Output the record type -->
            <xsl:text>D</xsl:text>
            
            <!-- Output fund identifier -->       
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='16'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with zeros -->
				<xsl:with-param name='value' 	select='ExternalFundIdentifier'/><!--There isn't a field to select so I've just enterd any old thing -->
			</xsl:call-template>

            <!-- Output fund class -->        
		    <xsl:call-template name='padString'>
			    <xsl:with-param name='justify'	select='"L"'/> 
			    <xsl:with-param name='trunc'	select='"Y"'/> 
			    <xsl:with-param name='max' 		select='8'/>
			    <xsl:with-param name='char' 	select='$blank'/> <!--Pad with Spaces -->
			    <xsl:with-param name='value' 	select='fundClass'/>
		    </xsl:call-template>

            <!-- Output valuation date and time -->        
            <xsl:value-of select='valuationDate'/>
            <xsl:value-of select='valuationTime'/>

            <!-- Output ISO Currency -->        
		    <xsl:call-template name='padString'>
			    <xsl:with-param name='justify'	select='"L"'/> 
			    <xsl:with-param name='trunc'	select='"Y"'/> 
			    <xsl:with-param name='max' 		select='3'/>
			    <xsl:with-param name='char' 	select='$blank'/>
			    <xsl:with-param name='value' 	select='ftCurrency'/>
		    </xsl:call-template>
		    
            <!-- Output net asset value offer price per unit -->        
            <xsl:value-of select='translate(format-number(navOfferPricePerUnit, "00000.0000"), ".", "")' />
		    
            <!-- Output net asset value bid price per unit -->        
            <xsl:value-of select='translate(format-number(navBidPricePerUnit, "00000.0000"), ".", "")' />
		    
            <!-- Output net asset value single price per unit (OEIC single price) -->
            <!-- Remove decimal places if they exist -->
            <xsl:value-of select='translate(format-number(navSinglePricePerUnit, "00000.0000"), ".", "")' />

            <!-- Output income equalisation -->        
            <xsl:value-of select='translate(format-number(incomeEqualisation, "0000000.0000"), ".", "")' />

            <!-- Output income dealing offer price -->        
            <xsl:value-of select='translate(format-number(incomeDealingOfferPrice, "00000.0000"), ".", "")' />

            <!-- Output income dealing bid price -->        
            <xsl:value-of select='translate(format-number(incomeDealingBidPrice, "00000.0000"), ".", "")' />

            <!-- Output income dealing mid price -->        
            <xsl:value-of select='translate(format-number(incomeDealingMidPrice, "00000.0000"), ".", "")' />

            <!-- Output base conversion factor -->        
            <xsl:value-of select='translate(format-number(baseConversionFactor, "000000.000000000"), ".", "")' />

            <!-- Output accumulation conversion factor -->        
            <xsl:value-of select='translate(format-number(0, "000000.000000000"), ".", "")' />

            <!-- Throw a line -->
            <xsl:text>&#13;&#10;</xsl:text>
        </xsl:if>
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