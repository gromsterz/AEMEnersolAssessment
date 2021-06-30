# Platforms,Wells Sync

This is a solution for an assessment given by a company that I applied for a developer position. The task given to me is to build a synchronization application to synchronize data from and API to a local database

There are only 4 requirements in the use case which are:

- The application need to able to login to the webApi to access the Platform Endpoint
- Store the Platform data on platform table and well data on well table from the single endpoint
- If data already exist. Please update based on the id. If not exist please insert into respective table.
- The application should not break when the API return different set of data for example some key are missing or new key is added. However, it is not required to handle the newly added key. Just handle the same key on the original dataset. To test this functionality, another request to endpoint GetPlatformWellDummy can be made.

I spent roughly 6 hours to get all the requirements done, below are the breakdown to rough estimation items that I have done to complete the solution:

- ~ 1.5 hour and half to finish the api project and be able to fetch the data from the API (Api Related)
- ~ 1.5 hour to create EntityFramewok, database and setup the db context (Database Related)
- ~ 1.5 hour to create the application to use the library (Presentation layer that contains Console App and Web Api to trigger the sync)
- ~ 1 hour to refactor some of the codes