<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act='http://www.univ-grenoble-alpes.fr/l3miage/actes'>
    
    <xsl:param name="destinedId" select="001"/>
    
    <xsl:output method="html"/>
    
    <xsl:template match="/">
        <html>
            <head>
                <title> Cabinet Infirmier </title>
            </head>
            <body> 
                <div>
                    <h1>Bonjour <xsl:value-of select="//ci:infirmier[@id=$destinedId]/ci:prénom"/> </h1>
                    <p> Aujourd'hui, vous avez <xsl:value-of select="count(//ci:patient/ci:visite[@intervenant=$destinedId])"/> patient(s).</p>
                </div>
                <div>
                    <table>
                        <tr>
                            <th>Nom</th>
                            <th>Adresse</th>
                            <th>Liste des soins a effectuer</th>
                        </tr>
                        <xsl:apply-templates select="//ci:patient[ci:visite/@intervenant=$destinedId]"/>
                    </table>
                </div>
            </body>
        </html>
    </xsl:template>
    
    <xsl:template match="ci:patient">
        <tr>
            <!-- Les balises text servent ici à laisser un espace entre les différents éléments d'un meme cellule du tableau-->
            <td><xsl:value-of select="ci:prénom"/> <xsl:text> </xsl:text> <xsl:value-of select="ci:nom"/></td> 
            <td><xsl:value-of select="ci:adresse/ci:numéro"/> <xsl:text> </xsl:text> <xsl:value-of select="ci:adresse/ci:rue"/></td>
            <td><xsl:apply-templates select="ci:visite/ci:acte"/></td>
        </tr>
    </xsl:template>
    
    <xsl:template match="ci:acte">
        <xsl:variable name="actes" select="document('actes.xml', /)/act:ngap"/>
        <xsl:variable name="acteId" select="@id"/>
        <xsl:value-of select="$actes/act:actes/act:acte[@id=$acteId]"/>
    </xsl:template>
</xsl:stylesheet>