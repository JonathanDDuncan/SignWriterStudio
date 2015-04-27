BEGIN TRANSACTION;
ALTER TABLE "main"."Dictionary" ADD COLUMN "Sorting" TEXT;

UPDATE Version SET Major =2, Minor = 0, DatabaseName = "Dictionary", DatabaseType = "Dictionary" WHERE IDVersion=2;
COMMIT;
