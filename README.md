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
    * 
    * Web Api could be created with .Net technology, in this case we will use **.NET 10**
    * The users must be authenticated before allow to use endpoints to manage products
    * CQRS is recommended for Read/Write methods
        * EF Core for read sentences
        * Dapper for write sentences

