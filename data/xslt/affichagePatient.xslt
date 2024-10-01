<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act='http://www.univ-grenoble-alpes.fr/l3miage/actes'
                xmlns:pat="http://www.univ-grenoble-alpes.fr/l3miage/patient">
    
    <xsl:param name="destinedName" select="'Pourferlavésel'"/>

    <xsl:output method="html" indent="yes"/>
    
    <xsl:template match="/">
        <html lang="fr">
            <head>
                <title>Information sur le patient</title>
            </head>
            <body>
                <h1>Information de <xsl:value-of select="//pat:nom"/> <xsl:text> </xsl:text> <xsl:value-of select="//pat:prénom"/></h1>
                
                <table>
                    <tr>
                        <th>Nom</th>
                        <th>Prénom</th>
                        <th>Sexe</th>
                        <th>Naissance</th>
                        <th>Numéro SS</th>
                        <th>Adresse</th>
                        <th>Visites</th>
                    </tr>
                    <tr>
                        <td><xsl:value-of select="//pat:nom"/></td>
                        <td><xsl:value-of select="//pat:prénom"/></td>
                        <td><xsl:value-of select="//pat:sexe"/></td>
                        <td><xsl:value-of select="//pat:naissance"/></td>
                        <td><xsl:value-of select="//pat:numéroSS"/></td>
                        <td>
                            <xsl:apply-templates select="//pat:adresse"/>
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <th>Date</th>
                        <th>Intervenant</th>
                        <th>Acte</th>
                    </tr>
                    <xsl:apply-templates select="//pat:visite"/>
                </table>
            </body>
        </html>
    </xsl:template>
    
    <xsl:template match="pat:adresse">
        <xsl:choose>
            <xsl:when test="pat:étage">
                <xsl:value-of select="pat:étage"/>
            </xsl:when>
        </xsl:choose>
        <xsl:text> </xsl:text>
        <xsl:choose>
            <xsl:when test="pat:numéro">
                <xsl:value-of select="pat:numéro"/>
            </xsl:when>
        </xsl:choose>
        <xsl:text> </xsl:text>
        <xsl:value-of select="pat:rue"/>
        <xsl:text> </xsl:text>
        <xsl:value-of select="pat:codePostal"/>
        <xsl:text> </xsl:text>
        <xsl:choose>
            <xsl:when test="pat:ville">
                <xsl:value-of select="pat:ville"/>
            </xsl:when>
        </xsl:choose>
    </xsl:template>
    
    <xsl:template match="pat:visite">
        <tr>
            <td><xsl:value-of select="@date"/></td>
            <td>
                <xsl:value-of select="pat:intervenant/pat:nom"/>
                <xsl:text> </xsl:text>
                <xsl:value-of select="pat:intervenant/pat:prénom"/>
            </td>
            <td>
                <xsl:apply-templates select="pat:acte"/>
            </td>
        </tr>
    </xsl:template>
</xsl:stylesheet>