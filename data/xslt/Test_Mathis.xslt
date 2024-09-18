<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical">
    <xsl:output method="html"/>
    
    <xsl:template match="/">
        <html>
            <head>
                <title> Cabinet Infirmier </title>
            </head>
            <body>
                <h1> Nom du cabinet : </h1> <!-- <xsl:value-of select="//ci:cabinet/ci:nom"/> -->
                <p> Il y a  infirmiers.</p> <!-- <xsl:value-of select="count(//ci:cabinet/ci:infirmier)"/> -->
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>