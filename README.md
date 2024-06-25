Simple .NET 6 web api implemented using TDD and Clean Architecture principles

---

# How to run the application
Make sure you have docker installed and to run the following commands in the repository root folder.

1. Run the docker-compose file:
```
docker-compose up
```
*This command will build and run the api container at the port 5000 and the db container at the port 1400 by default. If you need to change this, please modify the ports sections of the corresponding container in the docker-compose file.

---
2. Copy the db initialization script to the database container:
```
docker cp Database/init-db.sql simplebankapp-db:/init-db.sql
```
---
3. Run the intialization script using sqlcmd:

```
docker exec -it simplebankapp-db /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Aa12345678! -i /init-db.sql
```

*The database password is the same as in the docker-compose file.

---

4. Open the api swagger definition at:
```
http://localhost:5000/swagger
```
---


# How to use the app

1. Make sure to first call the register endpoint to create a user:

```
POST http://localhost:5000/simple-bank/auth/register
```

---
2. Then call the login endpoint to generate a JWT token:

```
POST http://localhost:5000/simple-bank/auth/login
```
---

3. Create a bank account:
```
POST http://localhost:5000/simple-bank/account
```

---

4. Use the bank account Id generated in the previous step to call any other endpoint in the application. Make sure to always provide the JWT bearer token for authentication.

---

For more details about the design and implementation of the app, please refer to the Documentation.pdf file.