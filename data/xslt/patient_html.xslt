<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act='http://www.univ-grenoble-alpes.fr/l3miage/actes'
                xmlns:pat="http://www.univ-grenoble-alpes.fr/l3miage/patient">

    <!-- Transformation affichant sous forme de page html les données du patient dont le nom est passé en paramètre. 
    Récupère les données du fichier xml généré par la transformation extractionPatient.xslt-->

    <!-- paramètre contenant le nom du patient dont il faut extraire les infos -->
    <xsl:param name="destinedName" select="'Pourferlavésel'"/>

    <xsl:output method="html" indent="yes" encoding="UTF-8"/>

    <!-- Affiche sous forme de tableau les infos du patient. Un premier tableau affiche les infos personnelles, un second les visites
    A noter que toutes les informations optionnelles ne sont affichées que si elles sont présentes, d'où les conditions. -->
    <xsl:template match="/">
        <html lang="fr">
            <head>
                <meta charset="UTF-8"/>
                <title>Information sur le patient</title>
            </head>
            <body>
                <h1>Information de <xsl:value-of select="//pat:nom"/> <xsl:text> </xsl:text> <xsl:value-of select="//pat:prénom"/></h1>

                <!-- Tableau des informations personelles -->
                <table>
                    <tr>
                        <th>Sexe</th>
                        <th>Naissance</th>
                        <th>NSS</th>
                        <th>Adresse</th>
                    </tr>
                    <tr>
                        <td><xsl:value-of select="//pat:sexe"/></td>
                        <td><xsl:value-of select="//pat:naissance"/></td>
                        <td><xsl:value-of select="//pat:numéroSS"/></td>
                        <td>
                            <xsl:apply-templates select="//pat:adresse"/>
                        </td>
                    </tr>
                </table>

                <!-- Tableau des visites -->
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
    
    <!-- Affiche l'adresse du patient correctement mise en forme selon les informations disponibles-->
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
    
    <!-- Affiche les visites programmées pour le patient -->
    <xsl:template match="pat:visite">
        <tr>
            <td><xsl:value-of select="@date"/></td>
            <td> <!-- Nom et Prénom de l'intervenant effectuant la visite -->
                <xsl:value-of select="pat:intervenant/pat:nom"/>
                <xsl:text> </xsl:text>
                <xsl:value-of select="pat:intervenant/pat:prénom"/>
            </td>
            <td>
                <xsl:value-of select="pat:acte"/>
            </td>
        </tr>
    </xsl:template>
    
    
</xsl:stylesheet>