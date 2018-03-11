# OpenFaaS Backend für Beispiel Build-Status

Diese Beispiel basiert auf OpenFaaS-CLI Templates von FPommerening. Für die Erstellung der Funktion müssen diese per CLI heruntergeladen werden.

Hinweis: Die folenden Befehle arbeiten unter das Voraussetzung das die OpenFaaS CLI unter Windows im Ordner "C:\OpenFaaS" liegt.

## Template herunterladen

    c:\OpenFaaS\faas-cli.exe template pull https://github.com/fpommerening/openfaas-template-csharp.git

## Funktion bauen

    c:\OpenFaaS\faas-cli.exe build -f build-status