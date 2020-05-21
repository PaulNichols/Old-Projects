<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="text"/>
	<xsl:template match="/">
    <!-- Add a header row with all desired field names -->
    <xsl:text>Company,</xsl:text>
    <xsl:text>Asset Fund Code,</xsl:text>
    <xsl:text>Full Name,</xsl:text>
    <xsl:text>Short Name,</xsl:text>
    <xsl:text>Asset Fund Type,</xsl:text>
    <xsl:text>AM Tolerance (%),</xsl:text>
    <xsl:text>Exchange,</xsl:text>
    <xsl:text>Currency,</xsl:text>
    <xsl:text>Proportion (%)</xsl:text>
    <!--Add end-of-line markers-->	
    <xsl:text>&#13;</xsl:text>
    <xsl:text>&#10;</xsl:text>
	<xsl:apply-templates select="/ArrayList/*/AssetMovementConstituents/AssetMovementConstituentDecorator"/>           	
	</xsl:template>

	<!--Create data rows-->		
	<xsl:template match="AssetMovementConstituentDecorator">
	<xsl:apply-templates select="../.."/>
    <xsl:value-of select="translate(@BenchmarkDisplayName, ',','')"/>
    <xsl:value-of select="','"/>
    <xsl:value-of select="translate(@CurrencyCode, ',','')"/>
    <xsl:value-of select="','"/>
    <xsl:value-of select="translate(@ProportionDisplay, ',','')"/>
    <xsl:text>&#13;</xsl:text>
    <xsl:text>&#10;</xsl:text>
	</xsl:template>

	<xsl:template match="AssetFundDecorator">
		<xsl:value-of select="translate(./AssetFund/@CompanyCode, ',','')"/>
		<xsl:value-of select="','"/>
		<xsl:value-of select="translate(./AssetFund/@AssetFundCode, ',','')"/>
		<xsl:value-of select="','"/>
		<xsl:value-of select="translate(@FullName, ',','')"/>
		<xsl:value-of select="','"/>
		<xsl:value-of select="translate(./AssetFund/@ShortName, ',','')"/>
		<xsl:value-of select="','"/>
		<xsl:value-of select="translate(./AssetFund/@AssetFundTypeString, ',','')"/>
			<xsl:value-of select="','"/>
		<xsl:value-of select="translate(@AssetMovementToleranceDisplay2, ',','')"/>
		<xsl:value-of select="','"/>
	</xsl:template>

</xsl:stylesheet>

  