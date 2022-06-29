--https://docs.microsoft.com/en-us/answers/questions/364258/how-should-i-get-a-list-of-table-names-and-field-n.html
SELECT
      s.name AS SchemaName
     ,t.name AS TableName
     ,c.name AS ColumnName
 FROM sys.schemas AS s
 JOIN sys.tables AS t ON t.schema_id = s.schema_id
 JOIN sys.columns AS c ON c.object_id = t.object_id
 ORDER BY
      SchemaName
     ,TableName
     ,ColumnName;
    
 SELECT
      TABLE_SCHEMA AS SchemaName
     ,TABLE_NAME AS TableName
     ,COLUMN_NAME AS ColumnName
 FROM INFORMATION_SCHEMA.COLUMNS
 ORDER BY
      SchemaName
     ,TableName
     ,ColumnName;