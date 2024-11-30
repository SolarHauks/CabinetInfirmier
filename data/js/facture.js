var AMIVAL = 3.15;
var AISVAL = 2.65;
var DIVAL = 10.0;

var totalFacture = 0.0;

function afficherFacture(prenom, nom, actes) {
    totalFacture = 0.0;
    var text = "<html lang=>\n";
    text +=
        "    <head>\n\
        <title>Facture</title>\n\
     </head>\n\
     <body>\n";


    text += "Facture pour " + prenom + " " + nom + "<br/>";


    // Trouver l'adresse du patient
    var xmlDoc = loadXMLDoc("../xml/cabinet.xml");
    var patients = xmlDoc.getElementsByTagName("ca:patient");
    var i = 0;
    var found = false;

    while ((i < patients.length) && (!found)) {
        var patient = patients[i];
        var localNom = patient.getElementsByTagName("ca:nom")[0].childNodes[0].nodeValue;
        var localPrenom = patient.getElementsByTagName("ca:prénom")[0].childNodes[0].nodeValue;
        if ((nom === localNom) && (prenom === localPrenom)) {
            found = true;
        } else {
            i++;
        }
    }


    if (found) {
        text += "Adresse: ";
        // On récupère l'adresse du patient
        var adresse;
        // adresse = ... à compléter par une expression DOM
        adresse = patient.getElementsByTagName("ca:adresse")[0];
        text += adresseToText(adresse);
        text += "<br/>";

        var nss;
        // nss = récupérer le numéro de sécurité sociale grâce à une expression DOM
        nss = patient.getElementsByTagName("ca:numéro")[0].childNodes[0].nodeValue;
        text += "Numéro de sécurité sociale: " + nss + "\n";
    }
    text += "<br/>";


    // Tableau récapitulatif des Actes et de leur tarif
    text += "<table border='1'  bgcolor='#CCCCCC'>";
    text += "<tr>";
    text += "<td> Type </td> <td> Clé </td> <td> Intitulé </td> <td> Coef </td> <td> Tarif </td>";
    text += "</tr>";
    
    // console.log("Actes:", actes);
    var acteIds = actes.split(" ");
    for (var j = 0; j < acteIds.length; j++) {
        text += "<tr>";
        var acteId = acteIds[j];
        text += acteTable(acteId);
        text += "</tr>";
    }

    text += "<tr><td colspan='4'>Total</td><td>" + totalFacture + "</td></tr>\n";

    text += "</table>";


    text +=
        "    </body>\n\
</html>\n";

    return text;
}

// Mise en forme d'un noeud adresse pour affichage en html
function adresseToText(adresse) {
    // Mise en forme de l'adresse du patient
    var str = "";
    var numero = adresse.getElementsByTagName("ca:numéro")[0]?.childNodes[0]?.nodeValue || "";
    var rue = adresse.getElementsByTagName("ca:rue")[0]?.childNodes[0]?.nodeValue || "";
    var codePostal = adresse.getElementsByTagName("ca:codePostal")[0]?.childNodes[0]?.nodeValue || "";
    var ville = adresse.getElementsByTagName("ca:ville")[0]?.childNodes[0]?.nodeValue || "";
    var etage = adresse.getElementsByTagName("ca:étage")[0]?.childNodes[0]?.nodeValue || "";

    if (etage) {
        str += "Étage: " + etage + ", ";
    }
    if (numero) {
        str += "Numéro: " + numero + ", ";
    }
    str += rue + ", " + codePostal + " " + ville;

    return str;
}


function acteTable(acteId) {
    var str = "";

    var xmlDoc = loadXMLDoc("../xml/actes.xml");
    var actes;
    // actes = récupérer les actes de xmlDoc
    actes = xmlDoc.getElementsByTagName("acte");

    // Clé de l'acte (3 lettres)
    var cle = "";
    // Coef de l'acte (nombre)
    var coef = 0;
    // Type id pour pouvoir récupérer la chaîne de caractères du type 
    //  dans les sous-éléments de types
    var typeId = "";
    // ...
    // Intitulé de l'acte
    var intitule = "";

    // Tarif = (lettre-clé)xcoefficient (utiliser les constantes 
    // var AMIVAL = 3.15; var AISVAL = 2.65; et var DIVAL = 10.0;)
    // (cf  http://www.infirmiers.com/votre-carriere/ide-liberale/la-cotation-des-actes-ou-comment-utiliser-la-nomenclature.html)      
    var tarif = 0.0;

    // Trouver l'acte qui correspond
    var i = 0;
    var found = false;
    // A dé-commenter dès que actes aura le bon type...
    while ((i < actes.length) && (!found)) {
        // A compléter (cf méthode plus haut)
        var acte = actes[i];
        if (acte.getAttribute("id") === acteId) {
            found = true;
            cle = acte.getAttribute("clé");
            coef = parseFloat(acte.getAttribute("coef"));
            typeId = acte.getAttribute("type");
            intitule = acte.childNodes[0].nodeValue.trim();

            // Calcul du tarif
            if (cle === "AMI") {
                tarif = coef * AMIVAL;
            } else if (cle === "AIS") {
                tarif = coef * AISVAL;
            } else if (cle === "DI") {
                tarif = coef * DIVAL;
            }

            // Debugging output
            // console.log("typeId:", typeId);
            // console.log("cle:", cle);
            // console.log("intitule:", intitule);
            // console.log("coef:", coef);
            // console.log("tarif:", tarif);
        }
        i++;
        
    }

    if (found) {
        str += "<td>" + typeId + "</td>";
        str += "<td>" + cle + "</td>";
        str += "<td>" + intitule + "</td>";
        str += "<td>" + coef + "</td>";
        str += "<td>" + tarif.toFixed(2) + "</td>";
        totalFacture += tarif;
    } else {
        str += "<td colspan='5'>Acte non trouvé</td>";
    }
    
    return str;
}


// Fonction qui charge un document XML
function loadXMLDoc(docName) {
    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    } else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }

    xmlhttp.open("GET", docName, false);
    xmlhttp.send();
    xmlDoc = xmlhttp.responseXML;

    return xmlDoc;
}

// Fonction rajoutée afin de ne pas la mettre dans le fichier html et de garder tout le js au meme endroit
function openFacture(prenom, nom, actes) {
    var width = 500;
    var height = 500;
    var left;
    var top;
    if (window.innerWidth) {
        left = (window.innerWidth - width) / 2;
        top = (window.innerHeight - height) / 2;
    } else {
        left = (document.body.clientWidth - width) / 2;
        top = (document.body.clientHeight - height) / 2;
    }
    var factureWindow = window.open("", 'facture', 'menubar=yes, scrollbars=yes, top=' + top + ', left=' + left + ', width=' + width + ', height=' + height + '');
    var factureText = afficherFacture(prenom, nom, actes);
    factureWindow.document.write(factureText);
}
