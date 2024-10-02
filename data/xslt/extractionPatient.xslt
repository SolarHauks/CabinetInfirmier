<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act='http://www.univ-grenoble-alpes.fr/l3miage/actes'
                xmlns:pat="http://www.univ-grenoble-alpes.fr/l3miage/patient">
    
    <!-- Transformation extrayant les informations du patient dont le nom est donné en paramètre
    Prend les infos dans le fichier cabinet.xml et génère un nouveau fichier xml -->

    <!-- paramètre contenant le nom du patient dont il faut extraire les infos -->
    <xsl:param name="destinedName"/> 
    
    <!-- variable contenant la partie de cabinet.xml concernant le patient dont le nom est celui donné en paramètre -->
    <xsl:variable name="patient" select="//ci:patient[ci:nom=$destinedName]"/>

    <xsl:output method="xml" indent="yes" encoding="UTF-8"/>
    
    <!-- A noter que toutes les informations optionnelles ne sont affichées que si elles sont présentes, d'où les conditions -->
    <xsl:template match="/">
        <pat:patient>
            <pat:nom><xsl:value-of select="$patient/ci:nom"/></pat:nom>
            <pat:prénom><xsl:value-of select="$patient/ci:prénom"/></pat:prénom>
            <pat:sexe><xsl:value-of select="$patient/ci:sexe"/></pat:sexe>
            <pat:naissance><xsl:value-of select="$patient/ci:naissance"/></pat:naissance>
            <xsl:choose> <!-- optionalElement -->
                <xsl:when test="$patient/ci:numéro">
                    <pat:numéroSS>
                        <xsl:value-of select="$patient/ci:numéro"/>
                    </pat:numéroSS>
                </xsl:when>
            </xsl:choose>
            
            <xsl:apply-templates select="$patient/ci:adresse"/> <!-- Affiche l'adresse du patient -->
            
            <xsl:apply-templates select="$patient/ci:visite"/> <!-- Affiche les visites programmées pour le patient -->
        </pat:patient>
    </xsl:template>
    
    <!-- Affiche l'adresse du patient -->
    <xsl:template match="ci:adresse">
        <pat:adresse>
            <xsl:choose> <!-- optionalElement -->
                <xsl:when test="$patient/ci:adresse/ci:étage">
                    <pat:étage>
                        <xsl:value-of select="$patient/ci:adresse/ci:étage"/>
                    </pat:étage>
                </xsl:when>
            </xsl:choose>
            <xsl:choose> <!-- optionalElement -->
                <xsl:when test="$patient/ci:adresse/ci:numéro">
                    <pat:numéro>
                        <xsl:value-of select="$patient/ci:adresse/ci:numéro"/>
                    </pat:numéro>
                </xsl:when>
            </xsl:choose> <!-- optionalElement -->
            <pat:rue><xsl:value-of select="$patient/ci:adresse/ci:rue"/></pat:rue>
            <pat:codePostal><xsl:value-of select="$patient/ci:adresse/ci:codePostal"/></pat:codePostal>
            <xsl:choose>
                <xsl:when test="$patient/ci:adresse/ci:ville">
                    <pat:ville>
                        <xsl:value-of select="$patient/ci:adresse/ci:ville"/>
                    </pat:ville>
                </xsl:when>
            </xsl:choose>
        </pat:adresse>
    </xsl:template>
    
    <!-- Affiche les visites programmées pour le patient -->
    <xsl:template match="ci:visite">
        <xsl:element name="pat:visite">
            <xsl:attribute name="date"><xsl:value-of select="@date"/></xsl:attribute>
            <pat:intervenant> <!-- Nom et Prénom de l'intervenant effectuant la visite -->
                <pat:nom><xsl:value-of select="//ci:infirmier[@id=current()/@intervenant]/ci:nom"/></pat:nom>
                <pat:prénom><xsl:value-of select="//ci:infirmier[@id=current()/@intervenant]/ci:prénom"/></pat:prénom>
            </pat:intervenant>
            <pat:acte>
                <xsl:apply-templates select="ci:acte"/>
            </pat:acte>
        </xsl:element>
    </xsl:template>

    <!-- Afficher la description des actes à effectuer pendant la visite, compilant toutes les descriptions d'actes
     présentes dans le fichier actes.xml dans une chaine -->
    <xsl:template match="ci:acte">
        <xsl:variable name="actes" select="document('actes.xml', /)/act:ngap"/>
        <xsl:variable name="acteId" select="@id"/>
        <xsl:value-of select="$actes/act:actes/act:acte[@id=$acteId]"/>
    </xsl:template>
</xsl:stylesheet>