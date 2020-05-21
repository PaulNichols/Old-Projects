<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:user="urn:my-scripts"> 

    <!-- Define the output as text file -->
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- Set and populate variables -->
	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>&#32;</xsl:text></xsl:variable>
	<xsl:variable name='LessThan'><xsl:text>&lt;&lt;</xsl:text></xsl:variable>
	<xsl:variable name='GreaterThan'><xsl:text>&gt;&gt;</xsl:text></xsl:variable>
	<xsl:variable name='blank'><xsl:text>&#32;</xsl:text></xsl:variable>
	
    <!-- Match all child nodes "*"; -->
    <xsl:template match="*">
           <xsl:call-template name="Header"/>
           <xsl:apply-templates select="ReleasedFunds"/>    
    </xsl:template>

    <!-- Render the header row-->
	<!--
		FIRST LINE HEADER FORMAT
		==========================================================
		Field Name					Value				Length	
		==========================================================
		File Id						"UNITRED" X(7)		7
		Filler						new line 
									character X(2)		2


		SECOND LINE HEADER FORMAT 
		====================================================================
		Field Name					Value							Length
		====================================================================
		Comment Indicator			<#> This tells the F Times 
									processing program to ignore 
									the text on the rest of the 
									record.	X(1)					1
		File Contents				"Clerical Medical Investment 
									Group Unit Linked Fund 
									Prices   "	X(60)				60
		File Created Title			"file created "	X(13)			13
		File Creation Date			<DD MMM YYYY HH:MM>	X(17)		17
		Filler						newline character	X( 2)		2

	-->
    <xsl:template name="Header">
        <!-- File Id -->
        <xsl:text>UNITRED </xsl:text>
		<!--File Creation Date -->
        <xsl:value-of select="user:returnDate3()" />
        
		<!-- Filler -->
        <xsl:value-of select="$newline" />		
    </xsl:template>
    
  

	<!--
		================================================================================
		Field Name				Value							Format		Length
		================================================================================
		FT fund code			Mexid							X(6)		6
		Filler					space							X(1)		1
		Filler					"<<"							X(2)		2
		FT Fund name	For initial price (unit type =1)		X(45)		45
						Fund long name + " INIT"
		
						For accumulation price (unit type = 2)		
						Fund long name + " ACC"

						Where the fund does not have initial 
						and accumulation
						Fund long name only		
		Filler			">>"									X(2)		2
		Filler			Space									X(1)		1
		Filler			"<<"									X(2)		2
		Valuation Date	Price date in format dd/mm/yyyy			X(10)		10
		Filler			">>"									X(2)		2
		Filler			Space									X(1)		1
		ISO CCY code	"GBX"									X(3)		3
		Filler			Space									X(1)		1
		Filler			"B"										X(1)		1
		BID price		Bid price (pence)						9(5).9		7
		Filler			Space									X(1)		1
		Filler			"O"										X(1)		1
		OFFER price		Offer price	pence						9(5).9 		7
		Filler			Space									X(1)		1
		Filler			"C"										X(1)		1
		Published Price 
		Change			Actual price movement					S9(5)V9		8
		Filler			carriage return							X(2)		2
	-->
	
    <xsl:template match="ReleasedFunds">
		
		<xsl:if test="position()=1">
   			<xsl:value-of select="SubHeading" />
			
			<xsl:value-of select="$newline" />
		
			<xsl:text>#</xsl:text>
				
   			<xsl:value-of select="$newline" />
			   	
			<xsl:text># Fund prices for dealing on </xsl:text>
			
			<xsl:call-template name="format-date">
					<xsl:with-param name="date" select="valuationDate" />
				</xsl:call-template>	
			
		
			<xsl:value-of select="$newline" />
		</xsl:if >


		<!-- Fund Code -->
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='6'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/> <!--Pad with zeros -->
			<xsl:with-param name='value' 	select='ExternalFundIdentifier'/>
		</xsl:call-template>
     
		<!-- Space -->
		<xsl:value-of select="$blank" />
		
	 	<!-- Short Name -->
		<xsl:value-of select='$LessThan' />
		<xsl:if test="isInitialUnits=1">
			<xsl:value-of select='concat(fullname," INIT")'/>
		</xsl:if>
		<xsl:if test="isInitialUnits=0">
			<xsl:value-of select='concat(fullname," ACC")'/>
		</xsl:if>
		<xsl:if test="isInitialUnits!=1 and isInitialUnits!=0">
			<xsl:value-of select='fullName' />
		</xsl:if>
		<xsl:value-of select='$GreaterThan' />
		
		<!-- Space -->
		<xsl:value-of select="$blank" />
		
		<!-- Valuation Date -->
		<xsl:value-of select='$LessThan' />
		<xsl:call-template name="format-date">
            <xsl:with-param name="date" select="valuationDate" />
        </xsl:call-template>			
		<xsl:value-of select='$GreaterThan' />
		
		<!-- Space -->
		<xsl:value-of select="$blank" />
		
		<!-- ISO Currency Code -->
		<xsl:value-of select='ftCurrency' />
		
		<!-- Space -->
		<xsl:value-of select="$blank" />
		
		<xsl:if test="isDual=0">
			
			<!-- Bid Price -->       
			<xsl:text>M</xsl:text>
			<xsl:if test="ftCurrency = 'GBX'">
				<xsl:value-of select='format-number(bidPrice, "0.0")' />
			</xsl:if>
			<xsl:if test="ftCurrency != 'GBX'">
				<xsl:value-of select='format-number(bidPrice, "0.000")' />
			</xsl:if>
			
		</xsl:if>
		
		<xsl:if test="isDual=1">
			
			<!-- Bid Price -->       
			<xsl:text>B</xsl:text>
			<xsl:if test="ftCurrency = 'GBX'">
				<xsl:value-of select='format-number(bidPrice, "0.0")' />
			</xsl:if>
			<xsl:if test="ftCurrency != 'GBX'">
				<xsl:value-of select='format-number(bidPrice, "0.000")' />
			</xsl:if>
			
			<!-- Space -->
			<xsl:value-of select="$blank" />
			
			<!-- Offer Price -->        
			<xsl:text>O</xsl:text>
				<xsl:if test="ftCurrency = 'GBX'">
				<xsl:value-of select='format-number(offerPrice, "0.0")' />
			</xsl:if>
			<xsl:if test="ftCurrency != 'GBX'">
				<xsl:value-of select='format-number(offerPrice, "0.000")' />
			</xsl:if>
			
		</xsl:if>
		
		<!-- Space -->
		<xsl:value-of select="$blank" />

		<!-- Movement -->
		<xsl:text>C</xsl:text>
		<xsl:if test="changeOfferPrice > 0 ">
			<xsl:if test="ftCurrency = 'GBX'">
				<xsl:value-of select="concat('+',format-number(changeOfferPrice, '####0.0'))" />
			</xsl:if>
			<xsl:if test="ftCurrency != 'GBX'">
				<xsl:value-of select="concat('+',format-number(changeOfferPrice, '####0.000'))" />
			</xsl:if>
		</xsl:if>
		<xsl:if test="changeOfferPrice &lt; 0 ">
			<xsl:if test="ftCurrency = 'GBX'">
				<xsl:value-of select="format-number(changeOfferPrice, '####0.0')" />
			</xsl:if>
			<xsl:if test="ftCurrency != 'GBX'">
				<xsl:value-of select="format-number(changeOfferPrice, '####0.000')" />
			</xsl:if>
		</xsl:if>
		<xsl:if test="changeOfferPrice = 0 ">
			<xsl:text>-</xsl:text>
		</xsl:if>
		
				
		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
		
		<xsl:if test="number(IsHalifax)=0">
			<xsl:if test="number(following::IsHalifax)=1 ">
   				<xsl:value-of select="following::SubHeading" />
			   	
			   	<xsl:value-of select="$newline" />
			
				<xsl:text>#</xsl:text>
				
   				<xsl:value-of select="$newline" />			   		
			   		
				<xsl:text># Fund prices for dealing on </xsl:text>
				
				<!--File Creation Date -->
				<xsl:call-template name="format-date">
					<xsl:with-param name="date" select="valuationDate" />
				</xsl:call-template>	
				
			
				<xsl:value-of select="$newline" />
			</xsl:if >
		</xsl:if >
    </xsl:template>    





    <!--                                                -->
    <!--            TRANSFORMATION FUNCTIONS            -->
    <!--                                                -->
    
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
        <xsl:value-of select="concat($day, '/', $month, '/', $century, $year)" />
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
	
<!--            SCRIPTING FUNCTIONS            -->
    
    <!-- Get todays date in required format -->
    <msxsl:script language="C#" implements-prefix="user">
        <![CDATA[
        public string returnDate()
        {
            return ( string.Format("{0:dd MMM yyyy HH:mm}", System.DateTime.Now) );
        }
        
        public string returnDate3()
        {
            return ( string.Format("{0:ddMM:HHmm}", System.DateTime.Now) );
        }
		
		
        ]]>
    </msxsl:script>
</xsl:stylesheet>