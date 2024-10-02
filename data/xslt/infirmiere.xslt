<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:ci="http://www.univ-grenoble-alpes.fr/l3miage/medical"
                xmlns:act='http://www.univ-grenoble-alpes.fr/l3miage/actes'>
    
    <!-- Génère à partir des données du cabinet présente dans le fichier cabinet.xml, une page html qui affiche 
    les informations nécessaires à l'infirmière dont l'id est passé en paramètre pour sa journée-->
    
    <xsl:param name="destinedId" select="001"/> <!-- id de l'infirmière -->
    
    <xsl:output method="html" indent="yes"/>
    
    <!-- Génère la page html. Affiche sous forme de tableau les visites à effectuer pour l'infirmière. -->
    <xsl:template match="/">
        <html lang="fr">
            <head>
                <title> Cabinet Infirmier </title>
                <script type="text/javascript" src="../js/facture.js"/> <!-- Script pour la facturation -->
                <link rel="stylesheet" href="../css/infirmiere.css"/> <!-- Feuille de style -->
            </head>
            <body> 
                <div>
                    <h1>Bonjour <xsl:value-of select="//ci:infirmier[@id=$destinedId]/ci:prénom"/> </h1>
                    <p> Aujourd'hui, vous avez <xsl:value-of select="count(//ci:patient/ci:visite[@intervenant=$destinedId])"/> patient(s).</p> <!-- TODO : voir le todo -->
                </div>
                <div>
                    <table>
                        <tr>
                            <th>Nom</th>
                            <th>Adresse</th>
                            <th>Liste des soins a effectuer</th>
                            <th>Facturation</th>
                        </tr>
                        <xsl:apply-templates select="//ci:patient[ci:visite/@intervenant=$destinedId]"/>
                    </table>
                </div>
            </body>
        </html>
    </xsl:template>
    
    <!-- Afficher les informations du patient à visiter, avec les informations de la visite et un bouton pour la facturation -->
    <xsl:template match="ci:patient">
        <tr>
            <!-- Les balises text servent ici à laisser un espace entre les différents éléments d'un meme cellule du tableau-->
            <td><xsl:value-of select="ci:prénom"/> <xsl:text> </xsl:text> <xsl:value-of select="ci:nom"/></td> 
            <td><xsl:value-of select="ci:adresse/ci:numéro"/> <xsl:text> </xsl:text> <xsl:value-of select="ci:adresse/ci:rue"/></td>
            <td><xsl:apply-templates select="ci:visite/ci:acte"/></td>
            <td> 
                <!-- Bouton pour la facturation. 
                Au click appelle la fonction openFacture(prénom, nom, actes) présente dans le fichier facture.js -->
                <xsl:element name="button">
                    <xsl:attribute name="onclick">
                        openFacture('<xsl:value-of select="ci:prénom"/>',
                                    '<xsl:value-of select="ci:nom"/>',
                                    '<xsl:value-of select="ci:visite/ci:acte"/>')
                    </xsl:attribute >
                    Facture
                </xsl:element>
            </td>
        </tr>
    </xsl:template>
    
    <!-- Afficher la description des actes à effectuer pendant la visite, compilant toutes les descriptions d'actes
     présentes dans le fichier actes.xml dans une chaine -->
    <xsl:template match="ci:acte">
        <xsl:variable name="actes" select="document('actes.xml', /)/act:ngap"/>
        <xsl:variable name="acteId" select="@id"/>
        <xsl:value-of select="$actes/act:actes/act:acte[@id=$acteId]"/>
    </xsl:template>
</xsl:stylesheet>