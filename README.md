# Test task for Notissimus
This repository contains an embedded stored procedure with the MS SQL database
# Getting Started

### 1.Edit appsettings config
Settings are located in appsettings.json.

* In file [appsettings.json](https://github.com/trueStape/TestTaskNotissimus/blob/master/appsettings.json) file, change the connection to the Server DataBase(variable "Server=").
```
For example : "DefaultConnection": "Server=DESKTOP-0000000;Database=TestTask;Trusted_Connection=True;MultipleActiveResultSets=true"
```
* Download [XML file](https://github.com/trueStape/TestTaskNotissimus/blob/master/Offers.xml) and in file [appsettings.json](https://github.com/trueStape/TestTaskNotissimus/blob/master/appsettings.json) change path to the XML File.
```
For example : "PathToXmlFile": "d:\\Offers.xml";
```
### 2.Code-first database 
Copy the code from [CreateDBTestTaskNotissimus](https://github.com/trueStape/TestTaskNotissimus/blob/master/CreateDBTestTaskNotissimus.sql). In Microsoft SQL Server Management Studio create a new request(ctr + N) and paste the code into request.

### 3.Run project

# Built With
* ASP.NET Core 3.1 Console App
* MS SQL

# Authors
* Yegor Oshlakov - [trueStape](https://github.com/trueStape)
