# CourseManagementApp
This is my training Blazor Server application with Entity Framework code-first approach. It is used for managing and enrolling in courses. The basic idea of the application was very simple, but it grew as I encountered problems and had more ideas to implement. There is still room for improvement, for example implementing true authentication and authorization.

## Introduction
In this application, you have CRUD for courses with PIN verification. Also, users can join courses without registration, as long as course capacity isn't reached. Course attendee edit and delete can be done by course creator. Course lists implement search and paging.

## Database and migrations
Before running this application, check connection string and change it, if necessary.

### Migrations
1. Add migration
``` PackageManager Console
Add-Migration MigrationName -o DataAccessLayer/Migrations
```
2. Database update
``` PackageManager Console
Update-Database
```
* Or instead of the second step, just run the application