<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt">

    <!-- Define the output as text file -->
    <xsl:output method="text" encoding="us-ascii"/>

    <!-- Set and populate variables -->
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>
	<xsl:variable name='empty_string'><xsl:text></xsl:text></xsl:variable>

    <!-- Match all child nodes "*"; -->
    <xsl:template match="*">
		<xsl:call-template name="Header"/>
        <xsl:apply-templates select="ReleasedFunds"/>
    </xsl:template>

    <!-- Render the header row-->
	<!--
		FIRST LINE HEADER FORMAT
		==========================================================
		Field Name					Value		
		==========================================================
		Record Type					"Halifax"

		SECOND LINE HEADER FORMAT 
		====================================================================
		Field Name					Value
		==========================================================
		External fund identifier	"Mexid"
		Asset fund short name		"Fund name"
		Valuation date				"Valuation date"
		Currency code				"Currency"
		OEIC single price			"Mid"
		Difference between today
		and yesterday				"Change"
		HiPort yield value			"Yield"
		exDividend					"Xd"
		Fund size (always blank)	"Fund Size
		Valuation date				"Fund size Valuation date"
	-->
    <xsl:template name="Header">
        <!-- Output column headers (1) -->
        <xsl:text>Halifax</xsl:text>
        <xsl:text>&#13;&#10;</xsl:text>
        
        <!-- Output column headers (2) -->
        <xsl:text>Mexid</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>Fund name</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>Valuation date</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>Currency</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>Mid</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>Change</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>Yield</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>Xd</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>Fund Size</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>Fund Size Valuation date</xsl:text><xsl:value-of select='$delimiter' />
        <xsl:text>&#13;&#10;</xsl:text>
    </xsl:template>

    <!-- Match the released funds nodes and process to output the data -->
    <!-- Loop through the document and pick out FT data only -->
	<!--
		====================================================================
		Field Name					Format
		==========================================================
		External fund identifier	X (Upper case)
		Asset fund short name		X
		Valuation date				dd/mm/yy
		Currency code				X(3)
		OEIC single price			9(9)(2)
		Difference between today	
		and yesterday				9(9)(2)
		HiPort yield value			9(9)(2)
		exDividend					X(2) (Upper case)
		Fund size (always blank)	
		Valuation date				dd/mm/yy
	-->
    <xsl:template match="ReleasedFunds">
        
        <!-- Test there is a value for FT against the mexx column -->
        <!-- To test if the element exists and is non-empty -->
        <xsl:if test="string(ExternalFundIdentifier)">
            <!-- Output required column data -->
            <xsl:value-of select='ExternalFundIdentifier'/><xsl:value-of select='$delimiter' />
            <xsl:value-of select='assetFundName'/><xsl:value-of select='$delimiter' />
            
            <!-- Format the valuation date -->
            <xsl:call-template name="format-date">
                <xsl:with-param name="date" select="valuationDate" />
            </xsl:call-template>
            <xsl:value-of select='$delimiter' />
            
            <xsl:value-of select='ftCurrency'/><xsl:value-of select='$delimiter' />
			<xsl:value-of select='format-number(bidPrice, "########0.00")' /><xsl:value-of select='$delimiter' />
			<xsl:value-of select='format-number(changePrice, "########0.00")' /><xsl:value-of select='$delimiter' />
			<xsl:value-of select='format-number(yield, "########0.00")' /><xsl:value-of select='$delimiter' />
            <xsl:value-of select='exDividend'/><xsl:value-of select='$delimiter' />
            <xsl:value-of select='fundSize'/><xsl:value-of select='$delimiter' />

            <!-- Format the Fund Size Valuation date, which is the valuation date -->
            <xsl:call-template name="format-date">
                <xsl:with-param name="date" select="valuationDate" />
            </xsl:call-template>
            
            <!-- Make sure we throw an end of line and line feed character -->
            <xsl:text>&#13;&#10;</xsl:text>
        </xsl:if>
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
        <xsl:value-of select="concat($day, '/', $month, '/', $year)" />
    </xsl:template>
</xsl:stylesheet>