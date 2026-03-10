use SGC
ALTER DATABASE SGC
ADD FILEGROUP SGC_MemoryOpt CONTAINS MEMORY_OPTIMIZED_DATA;

ALTER DATABASE SGC
ADD FILE
(
    NAME = SGC_MemoryOpt,
    FILENAME = 'D:\SQLData\SGC_MemoryOpt'
)
TO FILEGROUP SGC_MemoryOpt;


CREATE TABLE dbo.CounterRecords
(
    Id INT IDENTITY(1,1) NOT NULL,
    NumberValue INT NOT NULL,
    SavedDate DATETIME NOT NULL 
        CONSTRAINT DF_NumberRegistry_SavedDate DEFAULT SYSDATETIME(),

    CONSTRAINT PK_NumberRegistry 
        PRIMARY KEY NONCLUSTERED (Id)
)
WITH
(
    MEMORY_OPTIMIZED = ON,
    DURABILITY = SCHEMA_AND_DATA
);


EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'max server memory (MB)';


SELECT * FROM CounterRecords