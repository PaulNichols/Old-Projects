<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts">
    
    <!-- Define the output as text file -->
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
    <xsl:variable name='blank'><xsl:text>&#32;</xsl:text></xsl:variable>
    <xsl:variable name='zero'><xsl:text>0</xsl:text></xsl:variable>
    <xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>

    <!-- define fixed lengths of each field -->
    <xsl:variable name='FundCode_len' select='6' />
    <xsl:variable name='Price_len' select='11' />
    
    <!-- Process the XML Document to produce the Plan It (mainframe) text file (HBOSUPDT.TXT)-->
    <xsl:template match="*">
        <xsl:call-template name="Header"/>
        <xsl:apply-templates select="ReleasedFunds" />
        <xsl:call-template name="Footer"/>
    </xsl:template>

    <!-- Render the header row-->
	<!--
		HEADER FORMAT
		==================================================
		Field Name			Value		Format		Length
		==================================================
		Record Type			"HEAD"		X(4)		4
		Seperator			","			X			1
		Price date			DDMMCCYY	X(8)		8
	-->
    <xsl:template name="Header">
		<xsl:text>HEAD</xsl:text>
        <xsl:value-of select='$delimiter' />
        <xsl:value-of select="user:returnDate()" />
        
        <!-- Throw a line -->
        <xsl:text>&#13;&#10;</xsl:text>
    </xsl:template>

    <!-- Render the footer row-->
	<!--
		FOOTER FORMAT
		==================================================
		Field Name			Value		Format		Length
		==================================================
		Record Type			"TAIL"		X(4)		4
		Seperator			","			X			1
		Number of records				x(4)
	-->
    <xsl:template name="Footer">
		<xsl:text>TAIL</xsl:text>
        <xsl:value-of select='$delimiter' />
        
        <!-- Output the number of rows for HBOS -->
        <xsl:value-of select="format-number(count(//ReleasedFunds/ExternalFundIdentifier), '0000')" />
    </xsl:template>

    <!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		==================================================
		Field Name			Value		Format		Length
		==================================================
		Record type			"DATA"		X(4)		4
		Seperator			","			X			1
		Fund code			"hbos"		X(6)		6
		Seperator			","			X			1
		Bid price			Price		X(5)(6)		11
		Seperator			","			X			1
		Offer price			Price		X(5)(6)		11
	-->
    <xsl:template match="ReleasedFunds">
        <!-- Test there is a value for the mainframe against the 'hbos' column -->
        <!-- To test if the element exists and is non-empty -->
        <xsl:if test="string(ExternalFundIdentifier)">

            <!-- Add record type value -->
            <xsl:text>DATA</xsl:text>
            <xsl:value-of select='$delimiter' />
            
            <!-- Fund code, padded to 6 spaces -->
		    <xsl:call-template name='padString'>
			    <xsl:with-param name='justify'	select='"L"'/> 
			    <xsl:with-param name='trunc'	select='"Y"'/> 
			    <xsl:with-param name='max' 		select='$FundCode_len'/>
			    <xsl:with-param name='char' 	select='$blank'/> <!--Pad with Spaces -->
			    <xsl:with-param name='value' 	select='ExternalFundIdentifier'/>
		    </xsl:call-template>
		    <xsl:value-of select='$delimiter' />
    		
		    <!-- Output the bid price, formatted to 6 decimal places -->
            <xsl:value-of select='translate(format-number(navSinglePricePerUnit, "00000.000000"), ".", "")' />
            <xsl:value-of select='$delimiter' />
    		
		    <!-- Output the offer price, formatted to 6 decimal places -->
            <xsl:value-of select='translate(format-number(navSinglePricePerUnit, "00000.000000"), ".", "")' />

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