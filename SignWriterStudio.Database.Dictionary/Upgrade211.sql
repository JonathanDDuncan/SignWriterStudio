BEGIN TRANSACTION;
ALTER TABLE [SignSymbols] RENAME TO temp_table

CREATE TABLE [SignSymbols] ([IDSignSymbols] INTEGER PRIMARY KEY, [IDFrame] INTEGER, [code] int, [x] int, [y] int, [z] int, [hand] int, [handcolor] int, [palmcolor] int, [size] DOUBLE);
insert into [SignSymbols] select * from temp_table;
drop table temp_table;
UPDATE Version SET Major =1, Minor = 1, DatabaseName = "Dictionary", DatabaseType = "Dictionary" WHERE IDVersion=2;
COMMIT;
