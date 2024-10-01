<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act='http://www.univ-grenoble-alpes.fr/l3miage/actes'>
    
    <xsl:param name="destinedName" select="'Pourferlavésel'"/>
    
    <xsl:variable name="patient" select="//ci:patient[ci:nom=$destinedName]"/>

    <xsl:output method="xml" indent="yes"/>
    
    <xsl:template match="/">
        <patient>
            <nom><xsl:value-of select="$patient/ci:nom"/></nom>
            <prénom><xsl:value-of select="$patient/ci:prénom"/></prénom>
            <sexe><xsl:value-of select="$patient/ci:sexe"/></sexe>
            <naissance><xsl:value-of select="$patient/ci:naissance"/></naissance>
            <xsl:choose> <!-- optionalElement -->
                <xsl:when test="$patient/ci:numéro">
                    <optionalElement>
                        <xsl:value-of select="$patient/ci:numéro"/>
                    </optionalElement>
                </xsl:when>
            </xsl:choose>
            <adresse>
                <xsl:choose> <!-- optionalElement -->
                    <xsl:when test="$patient/ci:adresse/ci:étage">
                        <optionalElement>
                            <xsl:value-of select="$patient/ci:adresse/ci:étage"/>
                        </optionalElement>
                    </xsl:when>
                </xsl:choose>
                <xsl:choose> <!-- optionalElement -->
                    <xsl:when test="$patient/ci:adresse/ci:numéro">
                        <optionalElement>
                            <xsl:value-of select="$patient/ci:adresse/ci:numéro"/>
                        </optionalElement>
                    </xsl:when>
                </xsl:choose> <!-- optionalElement -->
                <rue><xsl:value-of select="$patient/ci:adresse/ci:rue"/></rue>
                <codePostal><xsl:value-of select="$patient/ci:adresse/ci:codePostal"/></codePostal>
                <xsl:choose>
                    <xsl:when test="$patient/ci:adresse/ci:ville">
                        <optionalElement>
                            <xsl:value-of select="$patient/ci:adresse/ci:ville"/>
                        </optionalElement>
                    </xsl:when>
                </xsl:choose>
            </adresse>
            <xsl:apply-templates select="$patient/ci:visite"/>
        </patient>
    </xsl:template>
    
    <xsl:template match="ci:visite">
        <xsl:element name="visite">
            <xsl:attribute name="date"><xsl:value-of select="@date"/></xsl:attribute>
            <intervenant>
                <nom><xsl:value-of select="//ci:infirmier[@id=current()/@intervenant]/ci:nom"/></nom>
                <prénom><xsl:value-of select="//ci:infirmier[@id=current()/@intervenant]/ci:prénom"/></prénom>
            </intervenant>
            <actes>
                <xsl:apply-templates select="ci:acte"/>
            </actes>
        </xsl:element>
    </xsl:template>

    <xsl:template match="ci:acte">
        <xsl:variable name="actes" select="document('actes.xml', /)/act:ngap"/>
        <xsl:variable name="acteId" select="@id"/>
        <xsl:value-of select="$actes/act:actes/act:acte[@id=$acteId]"/>
    </xsl:template>
</xsl:stylesheet>