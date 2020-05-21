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
	<xsl:variable name='HalifaxUPAS'>58</xsl:variable> 
	<xsl:variable name='EquitableUPAS'>61</xsl:variable> 
	<xsl:variable name='CMIGCUPAS'>62</xsl:variable> 
	<xsl:variable name='HIFMUPAS'>65</xsl:variable> 
	<xsl:variable name='HIFMEUPAS'>68</xsl:variable> 
	<xsl:variable name='CMIGEUPAS'>70</xsl:variable> 
	<xsl:variable name='CMIGHUPAS'>71</xsl:variable> 
	
    <!-- Process the XML Document to produce an UPAS text file -->
    <xsl:template match="*">
		<xsl:call-template name="Header"/>
		<xsl:apply-templates select="ReleasedFunds"/>
        <xsl:call-template name="Footer"/>
    </xsl:template>

	<!-- Render the Footer row-->
	<!--
		FOOTER FORMAT
		===================================
		Field Name			Value			
		===================================
	      03 F960-TRL-RECORD-TYPE             PIC X(01).    
          03 F960-TRL-FUND-COUNT              PIC X(05).
          03 FILLER                           PIC X(01).    
          03 F960-TRL-OFFER-PRICE             PIC X(10).
          03 F960-TRL-BID-PRICE               PIC X(10).
          03 F960-TRL-VARIANCE                PIC X(10).
          03 FILLER                           PIC X(02).
          03 F960-TRL-CANCEL-PRICE            PIC X(10).
          03 F960-TRL-YIELD                   PIC X(10).
          03 F960-TRL-CREATION-PRICE          PIC X(10).
          03 FILLER                           PIC X(01).    
          03 F960-TRL-CHARGE-FREE-PRICE       PIC X(10).

			
	-->
	
	<xsl:template name="Footer">
		<!-- Record Type -->      
		<xsl:text>9</xsl:text>
		
		<!-- Fund Count -->
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"R"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='5'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='0'/> <!--Pad with blanks -->
			<xsl:with-param name='value'	select='count(//ReleasedFunds)'/>
		</xsl:call-template>
		
		<!-- Filler -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
			<xsl:with-param name='value'	select='test'/>
		</xsl:call-template>
		
		<!-- Offer Price Total-->        
		<xsl:if test="ReleasedFunds/ValidationRule='O'">    
			<xsl:value-of select='format-number(0, "0000000.00")' />
		</xsl:if>
		<xsl:if test="ReleasedFunds/ValidationRule!='O'">    
			<xsl:value-of select='format-number(sum(//ReleasedFunds/offerPrice), "0000000.00")' />
		</xsl:if>
			
		<!-- Bid Price Total-->        
		<xsl:value-of select='format-number(sum(//ReleasedFunds/bidPrice), "0000000.00")' />
			
		<!-- Variance -->   
		<xsl:choose>
			<xsl:when test="sum(//ReleasedFunds/variance) >= 0 ">
				<xsl:value-of select="concat('+',format-number(sum(//ReleasedFunds/variance), '000000.00'))" />
			</xsl:when>
			<xsl:otherwise>
				<!-- Note. The negative sign is already assigned through SQL -->
				<xsl:value-of select="concat('',format-number(sum(//ReleasedFunds/variance), '000000.00'))" />
			</xsl:otherwise>
		</xsl:choose>
		
		<!-- RiskRow[GroupID=$lngGroupID -->
		
		<!-- Filler -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='2'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
			<xsl:with-param name='value'	select='test'/>
		</xsl:call-template>
		
		<!-- Cancellation Price Total-->        
		<xsl:value-of select='format-number(0, "0000000.00")' />
					
		<!-- Yield Price Total-->        
		<xsl:if test="ReleasedFunds/ValidationRule='O'">    
			<xsl:value-of select='format-number(0, "0000000.00")' />
		</xsl:if>
		<xsl:if test="ReleasedFunds/ValidationRule!='O'">    
			<xsl:value-of select='format-number(0, "0000000.00")' />
		</xsl:if>
			
		<!-- Creation Price Total-->        
		<xsl:if test="ReleasedFunds/ValidationRule='O'">    
			<xsl:value-of select='format-number(0, "0000000.00")' />
		</xsl:if>
		<xsl:if test="ReleasedFunds/ValidationRule!='O'">    
			<xsl:value-of select='format-number(0, "0000000.00")' />
		</xsl:if>
						
		<!-- Filler -->      
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"N"'/> 
			<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
			<xsl:with-param name='value'	select='test'/>
		</xsl:call-template>
		
		<!-- Change Free Price Total-->     
		<xsl:if test="ReleasedFunds/ValidationRule='O'">    
			<xsl:value-of select='format-number(sum(//ReleasedFunds/bidPrice), "0000000.00")' />
		</xsl:if>
		<xsl:if test="ReleasedFunds/ValidationRule!='O'">    
			<xsl:value-of select='format-number(sum(//ReleasedFunds/chargefreeprice), "0000000.00")' />
		</xsl:if>   		

    </xsl:template>
    
    
 <!-- Render the header row-->
	<!--
		HEADER FORMAT
		===================================
		Field Name			Value			
		===================================
          03 F960-HDR-RECORD-TYPE             PIC X.                 
          03 F960-HDR-START-DATE              PIC X(10).
          03 F960-HDR-END-DATE                PIC X(10).
          03 F960-HDR-MODE                    PIC X(10).
          03 F960-HDR-DESCRIPTION             PIC X(49).
	-->
	
	  <xsl:template name="Header">
		<!-- Record Type -->      
		<xsl:text>1</xsl:text>
		
		<!-- Start Date -->
		<xsl:call-template name="format-date">
            <xsl:with-param name="date" select="ReleasedFunds/StartDate" />
        </xsl:call-template>			
		
		<!-- End Date -->
		<xsl:if test="string-length(ReleasedFunds/EndDate)!=0">
			<xsl:call-template name="format-date">
				<xsl:with-param name="date" select="ReleasedFunds/EndDate" />
			</xsl:call-template>			
		</xsl:if>
		<xsl:if test="string-length(ReleasedFunds/EndDate)=0">
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='10'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value'	select='test'/>
			</xsl:call-template>		
		</xsl:if>
		
		<!-- Mode --> 
		<xsl:call-template name='padString'>
			<xsl:with-param name='justify'	select='"L"'/> 
			<xsl:with-param name='trunc'	select='"Y"'/> 
			<xsl:with-param name='max' 		select='10'/><!--Number of chars to pad with -->
			<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
			<xsl:with-param name='value' 	select='ReleasedFunds/Mode'/>
		</xsl:call-template>     
	
		<xsl:if test="ReleasedFunds/FileId=$HalifaxUPAS">
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='49'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value' 	select='"Halifax internal funds"'/>
			</xsl:call-template>   
		</xsl:if>
		<xsl:if test="ReleasedFunds/FileId=$EquitableUPAS">
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='49'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value' 	select='"Equitable internal funds"'/>
			</xsl:call-template>   
		</xsl:if>
		<xsl:if test="ReleasedFunds/FileId=$HIFMUPAS">
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='49'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value' 	select='"Halifax funds OEICs"'/>
			</xsl:call-template>   
		</xsl:if>
		<xsl:if test="ReleasedFunds/FileId=$HIFMEUPAS">
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='49'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value' 	select='"Equitable funds OEICs"'/>
			</xsl:call-template>   
		</xsl:if>
		<xsl:if test="ReleasedFunds/FileId=$CMIGHUPAS">
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='49'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value' 	select='"C Med (Dual Priced) Funds (for Halifax)"'/>
			</xsl:call-template>   
		</xsl:if>
		<xsl:if test="ReleasedFunds/FileId=$CMIGEUPAS">
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='49'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value' 	select='"C Med (Dual priced) Funds (for ELAS)"'/>
			</xsl:call-template>   
		</xsl:if>
			<xsl:if test="ReleasedFunds/FileId=$CMIGCUPAS">
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='49'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value' 	select='"Clerical Medical (Single pcd) Fds"'/>
			</xsl:call-template>   
		</xsl:if>

		<!-- Carriage return -->
		<xsl:value-of select='$newline' />
    </xsl:template>

	<!-- Render the detail rows -->
	<!--
		DETAILS FORMAT
		========================================
		Field Name				Value				
		========================================
		  03 F960-RECORD-TYPE                 PIC X.    
          03 F960-FUND-CODE                   PIC X(05).
          03 F960-VALIDATION-RULES            PIC X.    
          03 F960-OFFER-PRICE                 PIC X(10).
          03 F960-BID-PRICE                   PIC X(10).
          03 F960-VARIANCE                    PIC X(10).
          03 F960-EX-DIVIDEND                 PIC X(02).
          03 F960-CANCELLATION-PRICE          PIC X(10).
          03 F960-YIELD                       PIC X(10).
          03 F960-CREATION-PRICE              PIC X(10).
          03 F960-FUND-BASIS                  PIC X.    
          03 F960-CHARGE-FREE-PRICE           PIC X(10).	
					
	-->
	
	 <xsl:template match="ReleasedFunds">
        <xsl:if test="string(ExternalFundIdentifier) ">
        	<!-- Record Type -->      
			<xsl:text>2</xsl:text>
			
			<!-- Fund Code -->
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='5'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value' 	select='ExternalFundIdentifier'/>
			</xsl:call-template>   
			
			<!-- VALIDATION-RULES -->
			<xsl:value-of select='ValidationRule' />
			
			<!-- Offer Price -->    
			<xsl:if test="ValidationRule='O'">    
				<xsl:value-of select='format-number(0, "0000000.00")' />
			</xsl:if>
				<xsl:if test="ValidationRule!='O'">    
				<xsl:value-of select='format-number(offerPrice, "0000000.00")' />
			</xsl:if>
			
			<!-- Bid Price -->       
			<xsl:value-of select='format-number(bidPrice, "0000000.00")' />
				
			<!-- Variance -->  
			<xsl:choose>
				<xsl:when test="format-number(variance, '000000.00') &lt; 0">
					<xsl:value-of select="format-number(variance, '000000.00')" />
				</xsl:when>
				<xsl:otherwise>
					<xsl:value-of select="concat('+',format-number(variance, '000000.00'))" />
				</xsl:otherwise>
			</xsl:choose>
			<!--use after cr28-->
			<!--<xsl:value-of select="format-number(variance, '+000000.00;-000000.00')" />-->
					
			<!-- Ex Dividend -->
			<xsl:call-template name='padString'>
				<xsl:with-param name='justify'	select='"L"'/> 
				<xsl:with-param name='trunc'	select='"Y"'/> 
				<xsl:with-param name='max' 		select='2'/><!--Number of chars to pad with -->
				<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
				<xsl:with-param name='value'	select='test'/>
			</xsl:call-template>
			<!--</xsl:if>-->
			
			<!-- Cancellation Price -->        
			<xsl:if test="ValidationRule='O'">    
				<xsl:value-of select='format-number(CancellationPrice, "0000000.00")' />
			</xsl:if>
			<xsl:if test="ValidationRule!='O'">    
				<xsl:value-of select='format-number(0, "0000000.00")' />
			</xsl:if>    
			
			<!-- Yield -->        
			<xsl:if test="ValidationRule='O'">    
				<xsl:choose>
					<xsl:when test="format-number(yield, '000000.00') &lt; 0">
						<xsl:value-of select='format-number(0, "0000000.00")' />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="format-number(yield, '000000.00')" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:if>
			<xsl:if test="ValidationRule!='O'">    
				<xsl:value-of select='format-number(0, "0000000.00")' />
			</xsl:if>    
			
			<!-- Creation Price -->    
			<xsl:if test="ValidationRule='O'">    
				<xsl:value-of select='format-number(CreationPrice, "0000000.00")' />
			</xsl:if>
			<xsl:if test="ValidationRule!='O'">    
				<xsl:value-of select='format-number(0, "0000000.00")' />
			</xsl:if>    
			
			
			<!-- Fund basis -->
			<xsl:if test="ValidationRule='O'">    
				<xsl:text>S</xsl:text>
			</xsl:if>
			<xsl:if test="ValidationRule!='O'">    
				<xsl:call-template name='padString'>
					<xsl:with-param name='justify'	select='"L"'/> 
					<xsl:with-param name='trunc'	select='"Y"'/> 
					<xsl:with-param name='max' 		select='1'/><!--Number of chars to pad with -->
					<xsl:with-param name='char' 	select='$blank'/> <!--Pad with blanks -->
					<xsl:with-param name='value'	select='test'/>
				</xsl:call-template>
			</xsl:if>    
			
			<!-- Change free Price -->  
			
			<xsl:if test="ValidationRule='O'">    
				<xsl:value-of select='format-number(bidPrice, "0000000.00")' />
			</xsl:if>
			<xsl:if test="ValidationRule!='O'">    
				<xsl:if test="isDual=0">
					<xsl:value-of select='format-number(chargefreeprice, "0000000.00")' />
				</xsl:if>
			
				<xsl:if test="isDual=1">
					<xsl:value-of select='format-number(0, "0000000.00")' />
				</xsl:if>      
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
        <xsl:variable name="century" select="substring($date, 1, 2)" />
        <xsl:variable name="year" select="substring($date, 3, 2)" />
        <xsl:variable name="month" select="substring($date, 6, 2)" />
        <xsl:variable name="day" select="substring($date, 9, 2)" />

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
    
</xsl:stylesheet>