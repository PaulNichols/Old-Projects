<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="text"/>

	<xsl:template match="/">
        <!-- Add a header row with all desired field names -->
        <xsl:text>Company,</xsl:text>
        <xsl:text>Full Name,</xsl:text>
        <xsl:text>Short Name,</xsl:text>
        <xsl:text>For Release,</xsl:text>
        <xsl:text>Fund Group Type</xsl:text>

    	<!--Add end-of-line markers-->	
        <xsl:text>&#13;</xsl:text>
        <xsl:text>&#10;</xsl:text>

		<!--Create data rows-->		
		<xsl:for-each select="/*/AssetFundGroup">		
            <xsl:value-of select="translate(@CompanyCode, ',','')"/>
            <xsl:value-of select="','"/>
            <xsl:value-of select="translate(@FullName, ',','')"/>
            <xsl:value-of select="','"/>
            <xsl:value-of select="translate(@ShortName, ',','')"/>
            <xsl:value-of select="','"/>
            <xsl:value-of select="translate(@ForReleaseDisplay, ',','')"/>
            <xsl:value-of select="','"/>
            <xsl:text>Asset Fund</xsl:text>
            <xsl:text>&#13;</xsl:text>
            <xsl:text>&#10;</xsl:text>
		</xsl:for-each>
		<xsl:for-each select="/*/IndividualFundGroup">
            <xsl:value-of select="translate(@CompanyCode, ',','')"/>
            <xsl:value-of select="','"/>
            <xsl:value-of select="translate(@FullName, ',','')"/>
            <xsl:value-of select="','"/>
            <xsl:value-of select="translate(@ShortName, ',','')"/>
            <xsl:value-of select="','"/>
            <xsl:value-of select="translate(@ForReleaseDisplay, ',','')"/>
            <xsl:value-of select="','"/>
            <xsl:text>Fund</xsl:text>
            <xsl:text>&#13;</xsl:text>
            <xsl:text>&#10;</xsl:text>
		</xsl:for-each>

	</xsl:template>

</xsl:stylesheet>