
## Objective

	Develop a web API in C# using .NET 6 or later to manage a small book library.
	This system should enable basic operations related to books in the system.
	Provide a maintainable and scalable codebase as in a professional solution.
	Utilise an in-memory database or a SQL Server database. Pre-populated on startup.
	Include comments in your code as you deem necessary.
	Provide a README file with details on how to setup and run your project.

## Additional Details and Clarifications
	1.	Functionality
		•	The exact operations and how they are implemented are left to your discretion. Consider what basic operations might be useful for a library system.
	2.	Database
		•	Feel free to use an in-memory database or a SQL server database.
		•	Pre-populate this database with some data.
	3.	Documentation
		•	Provide the necessary instructions in a README file for setting up and running your API.
	4.	Coding Approach
		•	Demonstrate your knowledge of C#, .NET, and general best practices in software development.
		•	How you apply these practices will be key to your evaluation.
	5.	Testing
		•	Please provide testing appropriate to your project.
		•	You do not need to have full coverage of the system but enough to showcase an understanding.

## Evaluation

	Take this as an opportunity to showcase your abilities as a senior developer. 
	Evaluation will be based on the design decisions and principles shown throughout your solution.
	You do NOT need to provide a fully featured solution as some features may become repetitive when the goal is showcasing your abilities.
	The evaluation will focus on your problem-solving approach, code quality, adherence to design and best practices, and the effectiveness of your implementation of .NET features.



## About

This is an API built in .NET9 as a solution to a coding challenge from EDT. I have spent just over 2 hours on this so I am submitting it with only one test case but have added a couple of actually useful tests that I would add next. 

I have written it as a Minimal API as I thought it would be right given the scope of the project. I also thought it would be interesting.
I have also thrown in .NET Aspire also as I thought it would be cool.

I have included a few simple API calls.

I have included one test case configured using the newer WebApplicationFactory rather than needing to use Mocks.

As this is .NET9 I have also showcased the use of OpenAPI for the API documentation generation as this has been added out of the box by Microsoft.
They have not added a UI so it still uses swaggerUI.


## Running the Application
Run the API project and navigate to /Swagger you can test the endpoints.

Run the test.

You can also set the EDT Book Library.AppHost as startup and run that if you want to run it with Aspire. 


