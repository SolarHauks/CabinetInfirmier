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
                <div>
                    <h1> Nom du cabinet : <xsl:value-of select="//ci:cabinet/ci:nom"/> </h1> <!--  -->
                    <p> Il y a <xsl:value-of select="count(//ci:infirmiers/ci:infirmier)"/> infirmiers.</p> <!--  -->

                    <h2> Tab des infirmiers du cabinet </h2>
                    <xsl:apply-templates select="//ci:infirmiers"/>
                    
                    <h2> Tab des patients du cabinet </h2>
                    <xsl:apply-templates select="//ci:patients"/>
                </div>
            </body>
        </html>
    </xsl:template>
    
    <xsl:template match="ci:infirmiers">
        <table>
            <tr>Id</tr>
            <xsl:apply-templates select="ci:infirmier">
                <xsl:sort select="@id"/>
            </xsl:apply-templates>
            
        </table>
    </xsl:template>

    <xsl:template match="ci:infirmier">
        <tr>
            <td><xsl:value-of select="@id"/></td>
            <td><xsl:value-of select="@"/></td>
        </tr>
    </xsl:template>

    
    
    
    <xsl:template match="ci:patients">
        <table>
            <tr>Id</tr>
            <xsl:apply-templates select="ci:patient">
                <xsl:sort select="@id"/>
            </xsl:apply-templates>
        </table>
    </xsl:template>

    <xsl:template match="ci:patient">
        <tr>
            <td><xsl:value-of select="@id"/></td>
        </tr>
    </xsl:template>
    
    
</xsl:stylesheet>