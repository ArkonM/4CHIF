use testdb;
go

drop table if exists dbo.LogTest;
go

select top 1000000 
       identity(int, 1, 1) SomeID 
     , abs(checksum(newid())) % 50000 + 1 SomeInt 
     , char(abs(checksum(newid())) % 26 + 65) + 
       char(abs(checksum(newid())) % 26 + 65) SomeLetters2 
     , cast(abs(checksum(newid())) % 10000 / 100.0 as money) SomeMoney 
     , cast(rand(checksum(newid())) * 3653.0 + 36524.0 as datetime) SomeDate 
     , right(newid(), 12) SomeHex12 
  into dbo.LogTest
  from sys.all_columns ac1 cross join sys.all_columns ac2
; 
go


--a. Wofür ist die INTO-Klausel?			Wird benutzt, um Daten in die Tabelle zu geben
--b. Welchen Zweck hat CROSS JOIN?			Bildet das Kreuzprodukt von allen Datensätzen
--c. Was bewirkt TOP?						"select top x" bedetet, dass die ersten x Zeilen benutzt werden
--d. Wozu dient IDENTITY?					Identity erstellt eine Spalte, jeder Datensatz bekommt einen Identity Wert in dieser Spalte
--e. Was ist CHECKSUM, was NEWID()?			Mit CheckSum erstellt man einen Hash Index / NewID erstellt einen einzigartigen Wert
--f. Welche Auswirkung hat das Kommando?	Es erstellt dbo.LogTest und füllt die ersten 1000000 Zeilen mit Daten