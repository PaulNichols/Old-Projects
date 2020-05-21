<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="text"/>
	<xsl:template match="/">
    <!-- Add a header row with all desired field names -->
    <xsl:text>Company,</xsl:text>
    <xsl:text>Fund Code,</xsl:text>
    <xsl:text>Full Name,</xsl:text>
    <xsl:text>Short Name,</xsl:text>
    <xsl:text>Asset Fund,</xsl:text>
    <xsl:text>Class / Series,</xsl:text>
    <xsl:text>Price Series Type,</xsl:text>
    <xsl:text>Benchmarkable,</xsl:text>
    <xsl:text>Fund on Hi3,</xsl:text>
    <xsl:text>Dual Price,</xsl:text>
    <xsl:text>Mid is Bid,</xsl:text>
    <xsl:text>Life,</xsl:text>
    <xsl:text>Ex Div,</xsl:text>
    <xsl:text>XFactor,</xsl:text>
    <xsl:text>Narrative,</xsl:text>
    <xsl:text>Scaling Factor,</xsl:text>
    <xsl:text>TPE,</xsl:text>
    <xsl:text>Reval Factor,</xsl:text>
    <xsl:text>Day of Entry,</xsl:text>
    <xsl:text>Last Day of Application,</xsl:text>
    <xsl:text>LT,</xsl:text>
    <xsl:text>UT,</xsl:text>
    <xsl:text>AM,</xsl:text>
    <xsl:text>PI,</xsl:text>
    <xsl:text>System 1,</xsl:text>
    <xsl:text>Code 1,</xsl:text>
    <xsl:text>System 2,</xsl:text>
    <xsl:text>Code 2,</xsl:text>
    <xsl:text>System 3,</xsl:text>
    <xsl:text>Code 3,</xsl:text>
    <xsl:text>System 4,</xsl:text>
    <xsl:text>Code 4,</xsl:text>
    <xsl:text>System 5,</xsl:text>
    <xsl:text>Code 5,</xsl:text>
    <xsl:text>System 6,</xsl:text>
    <xsl:text>Code 6,</xsl:text>
    <xsl:text>System 7,</xsl:text>
    <xsl:text>Code 7,</xsl:text>
    <xsl:text>System 8,</xsl:text>
    <xsl:text>Code 8,</xsl:text>
    <xsl:text>System 9,</xsl:text>
    <xsl:text>Code 9,</xsl:text>
    <xsl:text>System 10,</xsl:text>
    <xsl:text>Code 10</xsl:text>
    <!--Add end-of-line markers-->	
    <xsl:text>&#13;</xsl:text>
    <xsl:text>&#10;</xsl:text>
	<xsl:apply-templates select="/ArrayList/*"/>           	
	</xsl:template>

	<!--Create data rows-->		
	<xsl:template match="ExternalSystemID">
		<xsl:value-of select="','"/>
		<xsl:value-of select="translate(@SystemName, ',','')"/>
		<xsl:value-of select="','"/>
		<xsl:value-of select="translate(@FundCode, ',','')"/>
	</xsl:template>

	<xsl:template match="FundStaticDataExportFundDecorator">
        <xsl:value-of select="translate(./Fund/@CompanyCode, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(./Fund/@HiPortfolioCode, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@FullName, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(./Fund/@ShortName, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@AssetFundID, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(./Fund/@ClassOrSeriesCode, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(./Fund/@FundType, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@IsBenchMarkableDisplay, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@OnHiPortfolio3Display, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@IsDualPriceDisplay, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@UseMidPriceAsBidPriceDisplay, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@IsLifeDisplay, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@IsExDividendDisplay, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@XFactorDisplay, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(./Fund/@XFactorDescription, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@ScalingFactorDisplay, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@TPEDisplay2, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@RevaluationFactorDisplay2, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@RevaluationEffectiveDateDisplay, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@RevaluationEndDateDisplay, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@LowerToleranceDisplay2, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@UpperToleranceDisplay2, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@AssetMovementDisplay2, ',','')"/>
        <xsl:value-of select="','"/>
        <xsl:value-of select="translate(@PriceIncreaseOnlyDisplay, ',','')"/>
        <xsl:apply-templates select="./Fund/SystemIDs/ExternalSystemID"/>
        <xsl:text>&#13;</xsl:text>
		<xsl:text>&#10;</xsl:text>
	</xsl:template>
	

</xsl:stylesheet>
  