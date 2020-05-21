<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="text"/>

<xsl:template match="DataGrid">AMP Unit Price Distribution System
Current Asset Fund Status
Fund Name,Type,Unit Price,Price Movement,Predicted AM,AM Variance,Within Tolerance
<xsl:apply-templates select="DataTable"/>
</xsl:template>

<xsl:template match="DataTable">
<xsl:value-of select="translate(FullName,',','')"/>,<xsl:value-of select="translate(AssetFundTypeString,',','')"/>,<xsl:value-of select="translate(AssetFundType,',','')"/>,<xsl:value-of select="translate(UnitPriceMovementDisplay,',','')"/>,<xsl:value-of select="translate(PredictedAssetMovementDisplay,',','')"/>,<xsl:value-of select="translate(AssetMovementVarianceDisplay,',','')"/>,<xsl:value-of select="translate(WithinAssetMovementToleranceDisplay,'','')"/>,<xsl:text>&#13;</xsl:text><xsl:text>&#10;</xsl:text>
</xsl:template></xsl:stylesheet>