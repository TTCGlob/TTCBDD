# RestCall Class
Used to make REST calls to APIs

## Instantiating a `RestCall<T>` Object
* Each RestCall object has a data type associated with it. This data type is used as a template for the JSON data to be deserialized into. Examples include:
  * `RestCall<Employee>`
  * `RestCall<List<Employee>>`
  * `RestCall<object>` is used if you don't care about the body that is returned.
* The call is instantiated as follows:
    ```C#
    new RestCall<SomeType>(Method method, string baseUrl, string resource, DataFormat dataFormat = DataFormat.Json)
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
* `Traverse(<int/string> parameter)`
  * Allows the traversal of the JSON object before deserializing.
  * In instances where there is JSON where you only want to deserialize some part of it as your given object, passing the object parameters and/or array indices to Traverse will cause RestCall to traverse the response before deserializing.
  * Repeated calls to `Traverse()` add the parameters to a list which is accessed in the order submitted.
  * Example where you only want the one element in the `"results"` array:
    ```JSON
    {
      "results": [
        {
          "gender": "male",
          "name": {
            "title": "mr",
            "first": "brad",
            "last": "gibson"
          },
          "location": {
            "street": "9278 new road",
            "city": "kilcoole",
            "state": "waterford",
            "postcode": "93027",
            "coordinates": {
              "latitude": "20.9267",
              "longitude": "-7.9310"
            },
            "timezone": {
              "offset": "-3:30",
              "description": "Newfoundland"
            }
          },
          "email": "brad.gibson@example.com",
          "login": {
            "uuid": "155e77ee-ba6d-486f-95ce-0e0c0fb4b919",
            "username": "silverswan131",
            "password": "firewall",
            "salt": "TQA1Gz7x",
            "md5": "dc523cb313b63dfe5be2140b0c05b3bc",
            "sha1": "7a4aa07d1bedcc6bcf4b7f8856643492c191540d",
            "sha256": "74364e96174afa7d17ee52dd2c9c7a4651fe1254f471a78bda0190135dcd3480"
          },
          "dob": {
            "date": "1993-07-20T09:44:18.674Z",
            "age": 26
          },
          "registered": {
            "date": "2002-05-21T10:59:49.966Z",
            "age": 17
          },
          "phone": "011-962-7516",
          "cell": "081-454-0666",
          "id": {
            "name": "PPS",
            "value": "0390511T"
          },
          "picture": {
            "large": "https://randomuser.me/api/portraits/men/75.jpg",
            "medium": "https://randomuser.me/api/portraits/med/men/75.jpg",
            "thumbnail": "https://randomuser.me/api/portraits/thumb/men/75.jpg"
          },
          "nat": "IE"
        }
      ],
      "info": {
        "seed": "fea8be3e64777240",
        "results": 1,
        "page": 1,
        "version": "1.3"
      }
    }
  * You would call `Traverse()` twice:
    ```C#
    var content = new RestCall<RandomUser>(Method.GET, RandomUserHelper.url, "")
        .Traverse("results")
        .Traverse(0)
        .Data();
    ```
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
* `IRestResponse<T> Execute()`
  * This executes the response and returns an object of type `IRestResponse<T>`.
  * This type contains many useful fields such as response code, the URI we accessed, etc.
  * The most important field is `.Data` which contains the (hopefully) successfully deserialized data.
  * This data will be of type T which we specified what T was in the constructor.
  * This will be null if deserialization was unsuccessful.
* `IRestResponse<T> Execute(Action<IRestResponse<T>, bool> action)`
  * This first calls Execute().
  * Before it returns the response, it performs `action(response)` which calls the specified action on the response object.
  * An action is a function that has no return value.
  * This is used if there is any code you would like to call before the request is returned.
  * An example of this can be found in `UnitTest1.TestRestPost().`
* `bool Check(Func<IRestResponse, bool> success)`
  * This first calls `Execute()`.
  * Then it calls `success(response)` and returns the result.
  * `success` is a function passed to it that takes a response and returns a bool depending on whether the request was successful.
  * This may just be checking the response code or whether `.Data` is null or not.
* `T Data()`
  * First calls `Execute()`, then returns the `.Data` property of the response object.
  * Will throw an exception if `.Data` is null which usually happens when deserialization is unsuccessful.
  * This prevents a null pointer exception later on in your code.

## Constructing Classes for `RestCall<T>`
* The idea of RestCall is to use plain classes as the structure we are deserializing the JSON to.
* Once the data is deserialized it becomes exactly like any other C# object.
* The basic rules of a class are:
  * All fields that are present in the returned data or are to be serialized must be:
    * `public`
    * Properties, i.e.
    `Type Property { get; set; }`
    * Also supports the use of getter and setter methods inside the parameter.
  * The properties must also be of the same data type as the JSON data.
  * Certain things can be converted automatically such as `DateTime`, `TimeSpan`, etc.
  * Any time JSON has property that is an object, that object needs to have a class definition that follows all thes rules as well.
* These class definitions can include methods and other properties and fields that are not in the data.
* If the class does not have a constructor, then the deserializer will search the class for properties that match the property names and types of the JSON data.
* An alternative option is to use a constructor that has arguments named and typed with the same rules as above, and it needs the attribute `[JsonConstructor]`.
* When there are lists in the class, they should be defined with the list initialised automatically, i.e.
```C#
List<Employee> employees { get; set; } = new List<Employee>();
```
* This prevents null pointer errors when adding to or accessing the list.