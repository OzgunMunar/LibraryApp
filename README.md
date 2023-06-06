# This is an full app with Asp.Net Core WebApi backend and a React app that adds, updates, deletes and gets the data from MSSQL database.

This project has built by me to practice .Net Core Web Api and React data processes.

## What's in there?

This text is just an explanation section. For successful understanding, please download it and run.

### MSSQL Database

MSSQL Database runs in local that has three tables: Book, Student and BorrowInfo. BorrowInfo has table relations with the other two tables by using StudentId and BookId and shows that the student book borrows.

### ASP.NET Core Web Api 6.0

CORS and Scope settings has been done.
Dependency Injection has been used for DbContext and Repository Class.
Repository Patterns implements GET, GETBYID, POST, UPDATE, DELETE methods asynchronously in the controllers.
Controller Classes does their own job asynchronously, nothing extra.

### React App

There is no reliable CSS or fancy HTML template in the React App. Just to use HTTP methods, I build a simple page.
