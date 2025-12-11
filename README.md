# RedArbor products management
This projects was created for products management in inventory using best practices of programming. The projects must be _dockerized_ for integral deployment



## Summary

* The user must be authenticated before make actions in the system
* If the authentication is success then the use can make transactions for the products management


## Techincal requirements
* __DataBase__
    * Docker container for DataBase server (SQL Server is preferred)
    * The tables no needed _foreign keys_

* __WebApi__
    * TDD, SOLID and Clean as programming best practices
    * Web Api could be created with .Net technology, in this case we will use **.NET 10**
    * The users must be authenticated before allow to use endpoints to manage products
    * CQRS is recommended for Read/Write methods
        * EF Core for read sentences
        * Dapper for write sentences

## Deployment
* For deployment both services, run the next command from the `compose.yml` path
```
docker compose up --build -d
```
* To stop de running containers
```
docker compose down
```
### Database initialization
* `db` folder in `src` path contains needed scripts `db_init.sql` and `db-init.sh` for initialize the database using the `entrypoint.sh` as main shell script


### WebApi instructions
* Swagger has been exposed in production mode as documentation purposes, this could be checked in  `http://localhost:5000/swagger` (or `http://localhost:5076/swagger` in development mode)