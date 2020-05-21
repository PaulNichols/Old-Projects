<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="text"/>

<xsl:template match="DataGrid">Column1,Column2
<xsl:apply-templates select="DataTable"/>My Footer Record
</xsl:template>

<xsl:template match="DataTable">
  <xsl:for-each select="*">
   <xsl:value-of select="."/>
   <xsl:if test="position() != last()">
    <xsl:value-of select="','"/>
   </xsl:if>
  </xsl:for-each>
<xsl:text>&#13;</xsl:text><xsl:text>&#10;</xsl:text>
</xsl:template>

</xsl:stylesheet>

