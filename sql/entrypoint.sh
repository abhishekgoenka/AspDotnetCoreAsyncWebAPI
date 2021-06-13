#start SQL Server, start the script to create the DB and import the data, start the app
chmod +x ./setup.sh
/opt/mssql/bin/sqlservr & ./setup.sh & sleep infinity & wait