<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="text"/>

<xsl:template match="DataGrid">AMP Unit Price Distribution System
Current Fund Status
Full Name,Imported Price,Predicted Price,Imported Price Movement,Predicted Price Movement,Price Variance,Within price Tolerance,Imported price for Asset Fund,Price Movement for Asset Fund,Predicted AM,AM Variance,Within AM Tolerance,Current Status,Last Status Changed
<xsl:apply-templates select="DataTable"/>
</xsl:template>

<xsl:template match="DataTable"><xsl:value-of select="translate(FullName,',','')"/>,<xsl:value-of select="translate(PriceDisplay,',','')"/>,<xsl:value-of select="translate(PredictedPriceDisplay,',','')"/>,<xsl:value-of select="translate(PriceMovementPercentDisplay,',','')"/>,<xsl:value-of select="translate(PredictedPriceMovementPercentDisplay,',','')"/>,<xsl:value-of select="translate(PriceMovementVarianceDisplay,',','')"/>,<xsl:value-of select="translate(PriceMovementRoundedToleranceDisplay,',','')"/>,<xsl:value-of select="translate(AssetUnitPriceDisplay,',','')"/>,<xsl:value-of select="translate(AssetMovementDisplay,',','')"/>,<xsl:value-of select="translate(PredictedAssetMovementDisplay,',','')"/>,<xsl:value-of select="translate(AssetMovementVarianceDisplay,',','')"/>,<xsl:value-of select="translate(AssetMovementToleranceDisplay,',','')"/>,<xsl:value-of select="FundStatusDisplay"></xsl:value-of>,<xsl:value-of select="translate(StatusChangedTime,',','')"/><xsl:text>&#13;</xsl:text><xsl:text>&#10;</xsl:text>
</xsl:template></xsl:stylesheet>