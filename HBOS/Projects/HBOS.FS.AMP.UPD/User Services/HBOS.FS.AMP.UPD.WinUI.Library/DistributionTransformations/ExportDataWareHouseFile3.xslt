<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- define constants -->
	<xsl:variable name='blank'><xsl:text>&#32;</xsl:text></xsl:variable>
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='CreationDate' select="user:returnDate()"/>       

    <!-- Process the XML Document to produce Data Warehouse file (FUND_GRP.dat text file)  -->
     <xsl:template match="*">
		<xsl:apply-templates select="ReleasedFunds">
		</xsl:apply-templates>
    </xsl:template>

    <!-- Render the header row-->
	<!--
		HEADER FORMAT
		==========================================================
		Field Name			Value			Format		Length
		==========================================================
		Record Id			"HEADER"		X(11)		11
		System Id			"P219"			X(5)		5
		File Number			01/02/03		9(2)		2
		Creation Date		CCYYMMDD format	9(8)		8
		Filler				Zero			X(7)		7
		Version				Version of 
							extract file 
							(000316)		X(6)		6
		File Type			"DAILY"			X(5)		5

	-->
	
	<xsl:template name="Header">
		<!-- Record Id -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='11'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/><!--Pad with blanks -->
			<xsl:with-param name='value' 	select='"HEADER"'/>
		</xsl:call-template>
		
		<!-- System Id -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='5'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/><!--Pad with blanks -->
			<xsl:with-param name='value' 	select='"P219"'/>
		</xsl:call-template>
		
		<!-- File Number -->
		<xsl:call-template name='FileNumber'/>
					
		<!-- Creation Date -->      
		<xsl:value-of select="$CreationDate" />
		
		<!-- Filler -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='7'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/><!--Pad with zeros -->
			<xsl:with-param name='value' 	select='test'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Version Number -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"R"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='6'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/><!--Pad with zeros -->
			<xsl:with-param name='value' 	select='VersionNumber'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- File type -->      
		<xsl:text>DAILY</xsl:text>
		
		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
	</xsl:template>
	
	<!-- Render the detail rows -->
	<!--
		Fund Prices:
		
		DETAILS FORMAT
		=====================================================================
		Field Name				Value					Format		Length		
		=====================================================================
		Fund Number				Fund number				9(5)		5
		Price Date				Valuation date in		9(8)		8
								CCYYMMDD format		
		Bid Price				Bid price				9(5)V9(1)	6
		Version					Version of extract 
								file (000316)			9(6)		6


		Funds:
		
		DETAILS FORMAT
		=====================================================================
		Field Name				Value					Format		Length		
		=====================================================================
		Fund Number				Fund number				9(5)		5
		Fund Description		Fund long name			X(40)		40
		Group No				Fund group no 
								1 to 10 inclusive and 30	9(3)		3
		Mex Code				For Accumulation funds
								, MEXID					X(6)		6
		Version					Version of extract file 
								(000316)				9(6)		6
								
		Fund groups:
		
		DETAILS FORMAT
		=====================================================================
		Field Name				Value					Format		Length		
		=====================================================================		
		Group no				Fund group no 
								1 to 10 inclusive and 30	9(3)		3
		Group description		Fund group description	X(40)		40
		Version					Version of extract file 
								(000316)				9(6)		6


		End of File Marker:
		
		DETAILS FORMAT
		=====================================================================
		Field Name				Value					Format		Length		
		=====================================================================	
		Record Id				"EOF"					X(3)		3
		Filler					" "						X(97)		97

	-->
	
	 <xsl:template match="ReleasedFunds">
		
		<!-- Header -->
		<!-- This has been put here so that we can get to the FileId-->
		<xsl:if test="position()=1 ">
			<xsl:call-template name="Header"/>
		</xsl:if>

		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"R"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='3'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/><!--Pad with spaces -->
			<xsl:with-param name='value' 	select='FundGroupNumber'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='40'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/><!--Pad with spaces -->
			<xsl:with-param name='value' 	select='FundGroupFullName'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- Version Number -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"R"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='6'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/><!--Pad with zeros -->
			<xsl:with-param name='value' 	select='VersionNumber'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>

		<xsl:value-of select='$newline' />
	
		<xsl:if test="position()=last()">
			<xsl:call-template name='Footer'/>
		</xsl:if>	
    </xsl:template>

	<!-- Render the footer row-->
	<!--
		FOOTER FORMAT
		==============================================================
		Field Name			Value				Format		Length
		==============================================================
		Record Id			"TRAILER"			X(11)			11
		System Id			"P219"				X(5)			5
		File Number			01/02/03			9(2)			2
		Creation Date		CCYYMMDD format		9(8)			8
		Trans Count			Total number of 
							recorded detail records 
							on the file			9(7)			7
		Version				Version of extract 
							file (000316)		9(6)			6
		File Type			"DAILY"				X(5)			5
	-->
	
    <xsl:template name="Footer">
		
		<!-- Record Id -->
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='11'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/><!--Pad with blanks -->
			<xsl:with-param name='value' 	select='"TRAILER"'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
	
		<!-- System Id -->
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='5'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/><!--Pad with blanks -->
			<xsl:with-param name='value' 	select='"P219"'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- File Number -->     
		<xsl:call-template name='FileNumber'/>
		
		<!-- Creation Date -->      
		<xsl:value-of select="$CreationDate" />
		
		<!-- Output the number of rows  -->
		<xsl:value-of select="format-number(position(), '0000000')"/>
		
		<!-- Version Number -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"R"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='6'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/><!--Pad with zeros -->
			<xsl:with-param name='value' 	select='VersionNumber'/><!--There isn't a field to select so I've just enterd any old thing -->
		</xsl:call-template>
		
		<!-- File type -->      
		<xsl:text>DAILY</xsl:text>

		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
		
    </xsl:template>
	
	<xsl:template name="FileNumber">
		<xsl:text>03</xsl:text>
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
		
<!--            SCRIPTING FUNCTIONS            -->
    
    <!-- Get todays date in required format -->
    <msxsl:script language="C#" implements-prefix="user">
        <![CDATA[
        public string returnDate()
        {
            return ( string.Format("{0:yyyyMMdd}", System.DateTime.Today) );
        }
        ]]>
    </msxsl:script>
	
	
</xsl:stylesheet>