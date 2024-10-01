<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act='http://www.univ-grenoble-alpes.fr/l3miage/actes'
                xmlns:pat="http://www.univ-grenoble-alpes.fr/l3miage/patient">
    
    <xsl:param name="destinedName" select="'Pourferlavésel'"/>
    
    <xsl:variable name="patient" select="//ci:patient[ci:nom=$destinedName]"/>

    <xsl:output method="xml" indent="yes"/>
    
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
            <xsl:apply-templates select="$patient/ci:visite"/>
        </pat:patient>
    </xsl:template>
    
    <xsl:template match="ci:visite">
        <xsl:element name="pat:visite">
            <xsl:attribute name="date"><xsl:value-of select="@date"/></xsl:attribute>
            <pat:intervenant>
                <pat:nom><xsl:value-of select="//ci:infirmier[@id=current()/@intervenant]/ci:nom"/></pat:nom>
                <pat:prénom><xsl:value-of select="//ci:infirmier[@id=current()/@intervenant]/ci:prénom"/></pat:prénom>
            </pat:intervenant>
            <pat:acte>
                <xsl:apply-templates select="ci:acte"/>
            </pat:acte>
        </xsl:element>
    </xsl:template>

    <xsl:template match="ci:acte">
        <xsl:variable name="actes" select="document('actes.xml', /)/act:ngap"/>
        <xsl:variable name="acteId" select="@id"/>
        <xsl:value-of select="$actes/act:actes/act:acte[@id=$acteId]"/>
    </xsl:template>
</xsl:stylesheet>