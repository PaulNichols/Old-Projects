<xsl:stylesheet version="1.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="urn:my-scripts"> 
       
    <!-- Specifiy the output type as a text file -->    
    <xsl:output method="text" encoding="us-ascii"/>

	<xsl:variable name='newline'><xsl:text>&#13;&#10;</xsl:text></xsl:variable>
	<xsl:variable name='delimiter'><xsl:text>,</xsl:text></xsl:variable>
 
  <xsl:template match="/">
        <xsl:call-template name="Header"/>
        <xsl:apply-templates select="/*/ExportStaticDataUserDetails"/>
    </xsl:template>
    
    	
	  <xsl:template name="Header">
	  
	  <!-- Render the header row-->
		<xsl:text>LogOnID,UserName</xsl:text>

		<xsl:for-each select="/*/ExportStaticDataUserDetails[not(@DisplayName=preceding::ExportStaticDataUserDetails/@DisplayName)]">
		
			<!-- Output Delimiter -->
			<xsl:value-of select='$delimiter' />

			<xsl:apply-templates  select='@DisplayName' />
				
		</xsl:for-each>
	  </xsl:template>
	  
	  
	<xsl:template match="*|@*|text()">
		<xsl:choose>
			<xsl:when test=".='True'">Y</xsl:when>
			<xsl:when test=".='False'">N</xsl:when>
			<xsl:otherwise> 
				<xsl:value-of select="translate(., ',',' ')"/> 
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	 <xsl:template match="ExportStaticDataUserDetails">
		
		 <xsl:if test="not(@LogOnID=preceding::ExportStaticDataUserDetails/@LogOnID)">
			<!-- Carriage return -->
			<xsl:value-of select='$newline' />
			<xsl:apply-templates  select='@UserName' />
			<xsl:value-of select='$delimiter' />
			<xsl:apply-templates  select='@LogOnID' />

		</xsl:if>
		
		<xsl:value-of select='$delimiter' />
		<xsl:apply-templates  select='@Granted' />
	 
    </xsl:template>


	

</xsl:stylesheet>