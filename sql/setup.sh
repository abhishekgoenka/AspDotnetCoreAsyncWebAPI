# Wait for SQL Server to be started and then run the sql script
chmod +x ./wait-for-it.sh
./wait-for-it.sh dotnetcoreasysnsample-db:1433 --timeout=0 --strict -- sleep 5s && \
/opt/mssql-tools/bin/sqlcmd -S localhost -i InitializeDatabase.sql -U sa -P "$SA_PASSWORD"