Simple .NET 6 web api implemented using TDD and Clean Architecture principles

---

# How to run the application
Make sure to clone the repo to your local computer and to have docker installed. After validating this, run the following commands in the repository root folder.

1. Run the docker-compose file:
```
docker-compose up --build
```
*This command will build and run the api container at the port 5000 and the db container at the port 1400 by default. If you need to change this, please modify the ports sections of the corresponding container in the docker-compose file.

---
2. Make sure the containers are running. Copy the db initialization script to the database container:
```
docker cp Database/init-db.sql simplebankapp-db:/init-db.sql
```
---
3. Run the db initialization script using sqlcmd:

```
docker exec -it simplebankapp-db /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Aa12345678! -i /init-db.sql
```

*The database password should the same as in the docker-compose file.

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


Seq server can be used to better visualize the application logs. It can be accessed at:
```
http://localhost:8081
```

---

For more details about the design and implementation of the app, please refer to the Documentation.pdf file.


---


# AI Implementation
### 24/7 Customer Service
Users are getting more used to "talking" to computers in natural language, so this is something that can be added to the application. Lets imagine we as a bank have a call center or a chatbot, ideally our customers should be able to say or type whatever they need in natural language and the application should be able to execute the requested action succesfully. This could be implemented for common queries like account balance or loan details. 

### Personalized Recommendations
AI can be implemented to recognize a customer behavior and suggest investment opportunities, loan options, personalized interest rates, or credit card offers. 

### Fraud Detection
Using AI better fraud detection systems can be implemented, since AI should be capable of detecting anomalies in a customer behavior, preventing unauthorized transactions in a personalized manner.  