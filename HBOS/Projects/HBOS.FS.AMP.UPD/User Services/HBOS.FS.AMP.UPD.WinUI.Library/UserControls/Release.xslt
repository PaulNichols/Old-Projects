<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="text"/>

<xsl:template match="DataGrid">Fund Name,Asset Fund Name,Release?,Price,Predicted Price,PriceMovement,Predicted Price Movement,PriceVariance,Within Price Tolerance?,Asset Unit Price,Asset Movement,Predicted Asset Movement,Asset Movement Variance,Within Asset Movement Tolerance?,Price Status,Status Change Time
<xsl:apply-templates select="DataTable"/>
</xsl:template>

<xsl:template match="DataTable">
<xsl:value-of select="translate(FullName,',','')"/>,<xsl:value-of select="translate(AssetFundName,',','')"/>,<xsl:choose><xsl:when test="ProgressStatus = 'True'">Yes</xsl:when><xsl:otherwise>No</xsl:otherwise></xsl:choose>,<xsl:value-of select="translate(PriceDisplay,',','')"/>,<xsl:value-of select="translate(PredictedPriceDisplay,',','')"/>,<xsl:value-of select="translate(PriceMovementPercentDisplay,',','')"/>,<xsl:value-of select="translate(PredictedPriceMovementPercentDisplay,',','')"/>,<xsl:value-of select="translate(PriceMovementVarianceDisplay,',','')"/>,<xsl:value-of select="translate(PriceMovementToleranceDisplay,',','')"/>,<xsl:value-of select="translate(AssetUnitPriceDisplay,',','')"/>,<xsl:value-of select="translate(AssetMovementDisplay,',','')"/>,<xsl:value-of select="translate(PredictedAssetMovementDisplay,',','')"/>,<xsl:value-of select="translate(AssetMovementVarianceDisplay,',','')"/>,<xsl:value-of select="translate(AssetMovementToleranceDisplay,',','')"/>,<xsl:value-of select="translate(FundStatusDisplay,',','')"/>,<xsl:value-of select="translate(StatusChangedTime,',','')"/><xsl:text>&#13;</xsl:text><xsl:text>&#10;</xsl:text>
</xsl:template></xsl:stylesheet>