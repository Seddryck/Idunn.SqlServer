# Idunn.SqlServer
Idunn.SqlServer is a software dedicated to check permissions and ensuring that a set of SQL permissions are effectively granted on different databases.

[![Build status](https://ci.appveyor.com/api/projects/status/erp2uy4c1a7dqbyk?svg=true)](https://ci.appveyor.com/project/Seddryck/idunnsql)
![Still maintained](https://img.shields.io/maintenance/yes/2017.svg)
![nuget](https://img.shields.io/nuget/v/Idunn.SqlServer.svg) 
![nuget pre](https://img.shields.io/nuget/vpre/Idunn.SqlServer.svg)
[![licence badge]][licence]
[![stars badge]][stars]

[licence badge]:https://img.shields.io/badge/License-Apache%202.0-yellow.svg
[stars badge]:https://img.shields.io/github/stars/Seddryck/IdunnSql.svg

[licence]:https://github.com/Seddryck/NBi/blob/master/LICENSE
[stars]:https://github.com/Seddryck/NBi/stargazers

## How-to

### Define the permissions to check
The permissions are defined in an xml file

* the root element is named ```idunn``` and is followed by one or more ```principal```. If you only have one element principal the root node can be ignored.
* for each ```principal```, you can define its name and one to many ```database``` 
* for each ```database```, you can define the ```server``` and the ```name``` with a set of ```permission``` and another set of ```securable```
* for each ```securable```, you'll have to define its name and its type (OBJECT, PROCEDURE, SCHEMA ...) and a set of ```permission```
* for each ```permission```, you'll have to define the permission's name.
 
example:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<principal>
  <database server="sql-001" name="db-001">
    <permission name="CONNECT"/>
    <securable type="schema" name="dbo">
      <permission name="SELECT"/>
      <permission name="UPDATE"/>
    </securable>
    <securable type="schema" name="admin">
      <permission name="INSERT"/>
    </securable>
  </database>
  <database server="sql-001" name="db-002">
    <securable type="table" name="dbo.Results">
      <permission name="SELECT"/>
    </securable>
    <securable type="procedure" name="dbo.Calculate">
      <permission name="EXECUTE"/>
    </securable>
  </database>
</principal>
```

## Console options

Idunn.SqlServer.Console offers two options: ```execute``` and ```generate```

### generate

The argument ```--source``` lets you specify the file containing the permissions to check (see above).
The argument  ```--destination``` lets you specify the file that will be generated.

The following command will generate a SQLCMD file with different connections to the databases and query to check the permissions. You can use this file in SSMS (don't forget to activate the SQLCMD mode). 
```
Idunn.SqlServer.Console.exe generate 
  --source "c:\temp\boo.xml" 
  --destination "c:\temp\boo.sql"
```

If no name is defined for the unique principal, then Idunn.SqlServer will create a script not impersonating another principal. In other cases or if the argument ```principal``` is specified then the principal will be impersonated.

If you wish, you can provide your own template to Idunn.SqlServer. You'll achieve this with parameter ```template```. 
```
Idunn.SqlServer.Console.exe generate --source "c:\temp\boo.xml" --destination "c:\temp\result.md" --template "c:\temp\template.md"
```
This template must use the following variable ```$principals$```. This vriable contains all the principals with its property ```name``` and their respective ```databases``` with properties ```name``` and ```server```. bellow each database, you'll find an object ```securables``` with three properties: ```type```, ```name``` and ```permission```.

example:
```md
$principals:{principal |
# $principal.name$
$principal.databases:{database |
## $database.server$

### $database.name$

|     object     |      type      |   permission   |
|----------------|----------------|----------------|
$database.securables:{securable |
| $securable.name$ | $securable.type$ | $securable.permission$ |  
}$}$}$
```
### execute

This option uses the same parameters ```source``` and ```principal``` than the option ```generate```, see above for more info.

The argument ```--output``` captures all the print events raised by SQL Server and redirect them to a text file.
```
Idunn.SqlServer.Console.exe execute 
  --source "c:\temp\boo.xml" 
  --principal "COLUMBIA\\cedri" 
  --output "c:\temp\result.txt"
```
