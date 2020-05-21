<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="text"/>

<xsl:template match="DataGrid">ReportHeader
<xsl:apply-templates select="ValidationErrors"/>End of Report
</xsl:template>

<xsl:template match="ValidationErrors">
  <xsl:for-each select="*">
   <xsl:value-of select="."/>
   <xsl:if test="position() != last()">
    <xsl:value-of select="','"/>
   </xsl:if>
  </xsl:for-each>
<xsl:text>&#13;</xsl:text><xsl:text>&#10;</xsl:text>
</xsl:template>

</xsl:stylesheet>

