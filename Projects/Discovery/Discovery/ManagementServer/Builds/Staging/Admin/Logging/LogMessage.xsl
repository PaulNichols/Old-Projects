<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/Exception">
    <html>
    <body>
    <!--
        This is an XSLT template file. Fill in this area with the
        XSL elements which will transform your XML to XHTML.
    -->
    
      <br/>
      <b>Description:</b>
      <br/>
      <xsl:value-of select="Description"></xsl:value-of>
      <br/>
       <b>Message:</b>
      <br/>
      <xsl:value-of select="Message"></xsl:value-of>
    </body>
    </html>
</xsl:template>

</xsl:stylesheet> 

