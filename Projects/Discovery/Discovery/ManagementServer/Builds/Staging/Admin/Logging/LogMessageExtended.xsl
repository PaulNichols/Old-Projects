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
        <b>Exception Type:</b>
        <br/>
        <xsl:value-of select="InnerException/ExceptionType"></xsl:value-of>
        <br/>
        <b>Stack Trace:</b>
        <br/>
        <xsl:value-of select="InnerException/StackTrace"></xsl:value-of>
        <br/>
        <b>Additional Info:</b>
        <br/>

        <xsl:for-each  select="additionalInfo/info">
         
            <xsl:value-of select="@name" />
          =
          <xsl:value-of select="@value" />
          <br/>
        </xsl:for-each>
        <br/>
        <b>Stack Trace:</b>
        <br/>
        <xsl:value-of select="StackTrace"></xsl:value-of>
        <br/>
        <hr></hr>
        <b>Inner Exception</b>
        <br/>
        <b>Message:</b>
        <br/>
        <xsl:value-of select="InnerException/Message"></xsl:value-of>
        <br/>
         <b>Exception Type:</b>
        <br/>
        <xsl:value-of select="InnerException/ExceptionType"></xsl:value-of>
        <br/>
          <b>Source:</b>
        <br/>
        <xsl:value-of select="InnerException/Source"></xsl:value-of>
        <br/>

        <b>Properties:</b>
        <br/>

        <xsl:for-each  select="InnerException/Property">

          <xsl:value-of select="@name" />
          =
          <xsl:value-of select="."></xsl:value-of>
          <br/>
        </xsl:for-each>
        <br/>
 <b>Stack Trace:</b>
        <br/>
        <xsl:value-of select="InnerException/StackTrace"></xsl:value-of>
        <br/>
     
      </body>
    </html>
</xsl:template>

</xsl:stylesheet> 

