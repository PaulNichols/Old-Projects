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
    <xsl:text>Fund Group Membership,</xsl:text>
    <xsl:text>Link</xsl:text>
    <!--Add end-of-line markers-->	
    <xsl:text>&#13;</xsl:text>
    <xsl:text>&#10;</xsl:text>
	<xsl:apply-templates select="/ArrayList/*/Fund/FundGroups"/>         	
	</xsl:template>

	<!--Create data rows-->		
	<xsl:template match="IndividualFundGroup">
	<xsl:apply-templates select="../../.."/>
    <xsl:value-of select="','"/>
    <xsl:value-of select="translate(@FullName, ',','')"/>
    <xsl:value-of select="','"/>
    <xsl:value-of select="'Direct'"/>
    <xsl:text>&#13;</xsl:text>
    <xsl:text>&#10;</xsl:text>
	</xsl:template>

	<xsl:template match="AssetFundGroup">
	<xsl:apply-templates select="../../.."/>
    <xsl:value-of select="','"/>
    <xsl:value-of select="translate(@FullName, ',','')"/>
    <xsl:value-of select="','"/>
    <xsl:value-of select="translate(../../../@AssetFundID, ',','')"/>
    <xsl:text>&#13;</xsl:text>
    <xsl:text>&#10;</xsl:text>
	</xsl:template>


	<xsl:template match="FundGroups">
	<xsl:choose>
	<xsl:when test="count (./IndividualFundGroup) &gt; 0">
		<xsl:apply-templates select="./IndividualFundGroup"/>
		<xsl:apply-templates select="../ParentFundGroups/AssetFundGroup"/>
	</xsl:when>
	<xsl:otherwise>
		<xsl:choose>
			<xsl:when test="count (../ParentFundGroups/AssetFundGroup) &gt; 0">	
				<xsl:apply-templates select="../ParentFundGroups/AssetFundGroup"/>
			</xsl:when>
			<xsl:otherwise>				
				<xsl:apply-templates select="../.."/>
				<xsl:value-of select="',None, None'"/>
				<xsl:text>&#13;</xsl:text>
				<xsl:text>&#10;</xsl:text>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:otherwise>
	</xsl:choose>
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
	</xsl:template>

</xsl:stylesheet>
  