
# User Info Service

This is a console app that prints out some user information based on [this endpoint](https://f43qgubfhf.execute-api.ap-southeast-2.amazonaws.com/sampletest).
It covers below requirements:
1. The users full name for id=42
1. All the users first names (comma separated) who are 23
1. The number of genders per Age, displayed from youngest to oldest<br/>
    e.g<br/>
    Age : 17 Female: 2 Male: 3<br/>
    Age : 18 Female: 6 Male: 3<br/>
    #### Result might look like this:</br>
    <img src="https://user-images.githubusercontent.com/43327562/219921441-41543576-2f3d-486e-aed6-c23de0cd8c42.png"  width="250" height="170" />

### Also, considering the resilience, it includes below settings:
1. Retry policy for transient Http error, set with random jitterer for better performance
1. Circuit breaker to protect a faulting system from overload
1. Timeout, not waiting forever

## How-to-start

Before you runs the project, please make sure you have [.Net 6 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed.

Clone this project to local and move to the path
```powershell
  cd this-project-path\UserInfoService\UserInfo.Console
```
Run the service
```powershell
  dotnet build
  dotnet run
```

## Configure the user HTTP endpoint

The resource endpoint is set in environment variable, you can swap it with another endpoint by updating [launchSettings](https://github.com/zinniawang149/UserInfoService/blob/master/UserInfoService/UserInfo.Console/Properties/launchsettings.json#L19) file.
If you are going deploy it into another environment, please setup **USER_SOURCE_URI** as your environment variable.

E.g.
```powershell
  $env:USER_SOURCE_URI = 'your new uri'
```

## Run the test

Move to the path

```powershell
  cd this-project-path\UserInfoService\UserInfoService.UnitTests
```

Run the tests
```powershell
  dotnet test
```

### Happy coding <img src="https://user-images.githubusercontent.com/43327562/219922476-09b1cdb3-7c07-4985-98cd-365990612666.png"  width="30" height="30" />

