#PumTestProject
Console Web Api self-hosted application.

## Application start
Visual Studio must be running in administrator mode.
Server is listening on http://localhost:9876.

## Database creating procedure

Database: Postgresql v. 9.5.9
The database is created automaticly the first time the program is started (EntityFramework code-first database.CreateIfNotExists()).

Alternatively the database can be created by running the file create_Db.sql (comment line 29 in Startup/ServerStarter.cs first, in order to turn off automatic db creation).

## Usage
The app has no user interface. Http requests can be sent using the applications like Postman, Fiddler etc.

## Mainly used frameworks

.NET Framework 4.6.1
EntityFramework v.6.4.0
NUnit 3.12
NSubstitute 4.2.1
Unity 5.11.3







