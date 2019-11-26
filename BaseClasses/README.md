# RestCall Class
Used to make REST calls to APIs

## Instantiating a RestCall<T> Object
* Each RestCall object has a data type associated with it. This data type is used as a template for the JSON data to be deserialized into. Examples include:
  * `RestCall<Employee>`
  * `RestCall<List<Employee>>`
  * `RestCall<object>` is used if you don't care about the body that is returned.
* The call is instantiated as follows:
    ```C#
    new RestCall<SomeType>(Method method, string baseUrl, string resource, DataFormat dataFormat = DataFormat.Json)`
    ```
  * `SomeType` is the type of the data to be sent/recieved.
  * `Method method` The type of method you intend to do, e.g.
    * `Method.GET`
    * `Method.POST`
    * `Method.PUT`
    * `Method.DELETE`
  * `string baseUrl` is the URL of the API to be accessed.
  * `string resource` is the URI of the endpoint you are accessing.
  * `DataFormat dataFormat` the type of data we are using, defaults to `DataFormat.Json`.

## Modifying Methods
These methods modify the request before we send them. As all these methods return the object itself, they can be chained together with dot notation `.Notation()`
* `AddHeader(string key, string value)`
  * This adds an HTTP header to the request.
  * Whether you use this or not depends on the implementation of the REST API.
* `AddUrlParameter(string token, string/int value)`
  * In the resource argument of the RestCall constructor we can specify placeholder tokens for us to replace with values with this method.
  * These tokens are surrounded by curly braces, e.g. `employees/{id}`.
  * When we execute the request these tokens are replaced with the values specified with this method.
* `AddPayload(T payload)`
  * Adds an object to be sent with the request.
  * This must be of the type specified in the constructor.
* `Where(string key, string value)`
  * Adds a query string to the request.
  * Support depends on the REST API implementation.
  * See documentation for [json-server](https://github.com/typicode/json-server)
* `Where(string parameter)`
  * Work in Progress!
  * Another syntax for adding query strings:
    * *field relation value*
    * *field* is a field of the requested data
    * *relation* is a relational operator such as `==, >=, !=` or a word e.g. `like, is`
    * *value* is a value to be compared to.
  * Best  to see examples in productsdb.feature


## Execution
Once we are finished constructing the RestCall class we want to execute the call. There are several ways we can do this:
* `Execute()`
  * This executes the response and returns an object of type `IRestResponse<T>`.
  * This type contains many useful fields such as response code, the URI we accessed, etc.
  * The most important field is `.Data` which contains the (hopefully) successfully deserialized data.
  * This data will be of type T which we specified what T was in the constructor.
  * This will be null if deserialization was unsuccessful.
* `Execute(Action<IRestResponse<T>, bool> action)`
  * This first calls Execute().
  * Before it returns the response, it performs `action(response)` which calls the specified action on the response object.
  * An action is a function that has to return type.
  * This is used if there is any code you would like to call before the request is returned.
  * An example of this can be found in `UnitTest1.TestRestPost().`
* 