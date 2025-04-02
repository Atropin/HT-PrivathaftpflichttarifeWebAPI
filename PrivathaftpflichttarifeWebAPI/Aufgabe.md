Programmieraufgabe: Privathaftpflichttarife WebAPI 

Ziel 
Entwickle eine .NET WebAPI, die es erm�glicht, Privathaftpflichttarife zu erfassen und 
abzurufen. Die Daten k�nnen in-Memory gespeichert werden, sollten aber so 
strukturiert sein, dass eine Datenbankanbindung sp�ter einfach implementiert werden 
kann. Dabei ist zu beachten, dass Bausteintarife bestehende Grundtarife derselben 
Gesellschaft verbessern k�nnen, niemals alleine berechnet werden d�rfen. 

Anforderungen 
1. Gesellschaft 
o Bezeichnung (string) 
2. Grundtarif 
o Bezeichnung (string) 
o G�ltigkeitsdatum (DateTime) 
o Pr�mie in EUR (decimal) 
o Gesellschaft (string) 
o Leistungsmerkmale (boolean, beliebig erweiterbar) 
3. Leistungsmerkmale (boolean, beliebig erweiterbar) 
o Mitversicherung von Kindern 
o Schl�sselverlust 
o Regressanspr�che 
4. Bausteintarif 
o Bezeichnung (string) 
o G�ltigkeitsdatum (DateTime) 
o Zusatzpr�mie in EUR (decimal) 
o Gesellschaft (string) 
o Leistungsmerkmale (boolean, beliebig erweiterbar) 

Aufgabenstellung 
1. Erstellen Sie eine .NET WebAPI: 
o Implementiere Endpunkte zum Erstellen, Abrufen und Aktualisieren von 
Gesellschaften, Grundtarifen und Bausteintarifen. 
2. Datenmodellierung: 
o Erstelle ein Datenmodell f�r Gesellschaften, Tarife und 
Leistungsmerkmale. 
o Nutze In-Memory-Speicherung, um die Daten zu verwalten. 
3. Mock-Daten (optionaler Bestandteil der Aufgabe): 
o Implementiere eine M�glichkeit, die Daten zu mocken, sodass die API 
ohne eine echte Datenbank funktioniert. 
o Stelle sicher, dass die Struktur der Mock-Daten so ist, dass eine echte 
Datenbankanbindung sp�ter einfach implementiert werden kann. 
4. Tarifberechnung: 
o Implementiere Endpunkte zur Berechnung von Tarifen basierend auf 
vorgegebenen Leistungsvorgaben. 
o Die Anwendung soll g�ltige Kombinationen von Grundtarifen und 
Bausteintarifen selektieren und die entsprechende Gesamtpr�mie 
berechnen. 
o Die Ausgabe der Berechnung soll die Gesellschaft- und Tarifbezeichnung, 
die berechnete Gesamtpr�mie, die Leistungsmerkmale mit ihren 
Auspr�gungen (true oder false) und eine Liste der enthaltenen 
Bausteintarife mit der Teilpr�mie und den Leistungsmerkmalen enthalten. 
Beispiel-Endpunkte 
� POST /api/gesellschaften - Erstellen einer neuen Gesellschaft 
� GET /api/gesellschaften - Abrufen aller Gesellschaften 
� POST /api/tarife - Erstellen eines neuen Tarifs (Grund- oder Bausteintarif) 
� GET /api/tarife - Abrufen aller Tarife 
� GET /api/tarife/{id} - Abrufen eines spezifischen Tarifs 
� PUT /api/tarife/{id} - Aktualisieren eines Tarifs 
� POST /api/tarife/berechnen - Berechnen von Tarifen basierend auf 
Leistungsvorgaben 

Erfolgskontrolle 
Der Erfolg des Projekts wird anhand bestehender End-to-End-Tests ermittelt. Folgende 
Testf�lle sollten abgedeckt sein: 
1. Anlage zwei Gesellschaften. Jede Gesellschaft hat min. einen Grund- und einen 
Bausteintarif. 
2. Berechnung mit korrekten Pr�mien aus Grund- und Bausteintarifen, 
entsprechend den geforderten Leistungsmerkmalen.  
Hinweise 
� Nutze .NET 8 oder h�her. 
� Dokumentiere wesentliche Aspekte deines Codes und der 
Architekturentscheidungen. 
� Implementiere End-to-End Tests, um die Funktionalit�t der API zu �berpr�fen. 
� Stell uns das Repository per Github zur Verf�gung. 
� Optional: Schreibe Unit-Tests f�r die wichtigsten Komponenten. 

Viel Erfolg bei der Umsetzung!  