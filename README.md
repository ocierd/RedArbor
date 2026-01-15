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


* Login in `AuthController` in `Authenticate` endpoint for changes credentials for JWT
    * User credentials:
        * username:`admin`, password:`Password123!`, roles:[`Administrator`,`InventoryManager`]
        * username:`user1` & password: `Password123!`, roles: [`User`]
        * username: `inventoryManager`, password:`Password123!`, roles:[`InventoryManager`]
    * The JWT should be included in each endpoint, unless the endpoint uses `AllowAnonymous` attribute

*   Most endpoints must be requested with authorize header, and the next endpoints should have explicity authorization "Roles" and "Policies"
    * api/Products
        * Must be requested by `Administrator` role and comply with the `CanCheckoutProduct` policy.
    * api/Inventory/checkout 
        * This endpoint mus be requested by `Adminmistrator` or `InventoryManager` and comply with the `CanCheckoutProduct` policy.
    * Other endpoints only require the user to be authenticated