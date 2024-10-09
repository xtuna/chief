# chief

NOTE: create a database first in the mssql and change the appsettingsjson aligned with your designated database

Steps:
1. create Model Folder and put the tables
2. create DAL Folder for the connection to database
3. right click and click nuget and install the entityframework, tools, design and sql
4. create the content of DAL
5. modify the appsettings.json base on your connection in the mssql
6. use add-migration new to package manager console
7. and update-database after
8. modify the shared > _layout.cshtml for header and footer
9. create pages for your desired menus
10. add-migration AddUploadedFileNameToApplicationFile
11. update-database
