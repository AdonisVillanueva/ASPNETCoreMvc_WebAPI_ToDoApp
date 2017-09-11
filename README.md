# ASPNETCoreMvc_WebAPI_ToDoApp
This is a simple To Do app with a Web Api end point. It will accept JSON data and returns JSON data.

1. CMD > Navigate to "src\todapp" run dotnet restore
2. Run the app: dotnet run
3. Go to: http://localhost:(port)/swagger - the port is where the app is listening, in most cases it's 5000
4. Use Swagger ui to test the web api:  GET/POST/PUT/UPDATE and DELETE a To do item.

Alternatively, you can use Postman(https://www.getpostman.com/) to invoke the methods in the web api.

For adding a record in Postman: Choose POST > BODY > RAW > JSON
Paste the follwing as JSON data and hit send
{
    "name":"walk dog",
    "IsComplete":0,
    "Note":"walking the dog.",
    "DeadLine": "2017-09-23"
}

Choosing Get returns all records. Get with params: Key = ID, Value is the Id of the tod item. Put updates a record.
