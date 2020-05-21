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
		<xsl:text>Fund Group Membership</xsl:text>
	    
		<!--Add end-of-line markers-->	
		<xsl:text>&#13;</xsl:text>
		<xsl:text>&#10;</xsl:text>
		<xsl:apply-templates select="/ArrayList/*/AssetFund/FundGroups"/>    
		       	
	</xsl:template>

	<!--Create data rows-->		
	<xsl:template match="AssetFundGroup">
		<xsl:apply-templates select="../../.."/>
		<xsl:value-of select="translate(@FullName, ',','')"/>
		<xsl:text>&#13;</xsl:text>
		<xsl:text>&#10;</xsl:text>
	</xsl:template>

	<xsl:template match="FundGroups">
		<xsl:choose>
		<xsl:when test="count (./AssetFundGroup) &gt; 0">
			<xsl:apply-templates select="./AssetFundGroup"/>
		</xsl:when>
		<xsl:otherwise>
			<xsl:apply-templates select="../.."/>
			<xsl:value-of select="'None'"/>		
			<xsl:text>&#13;</xsl:text>
			<xsl:text>&#10;</xsl:text>
		</xsl:otherwise>
		</xsl:choose>
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

  