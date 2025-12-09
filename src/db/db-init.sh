# Waiting for Server DataBase start
sleep 15s

echo "running set up script"
# Script to create user, database, tables and initial data
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Passw0rd -d master -i db_init.sql -C

