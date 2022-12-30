# ANR Watchdog

This package will allow you to reduce the number of ANRs of your application. This is achieved by using a watchdog timer
which will restart the application in case something goes wrong.

## How to use

Just add the package to your project and you are ready to go!

## Installing package

Open package manager and select "Add package from git URL..."

![image](https://user-images.githubusercontent.com/4660466/210089474-7c064036-38b1-4908-948a-8c22d0cc8f23.png)

Enter package url:

```
https://github.com/Norne9/Unity-ANR-Watchdog.git
```

And click "Add"

## Configuration

You can change the timeout after which your app will restart. The ANR detection timeout in Android is 10 seconds.
Therefore, I recommend keeping it below this value. The default value is 9 seconds.

Example:

```csharp
using Norne;
using UnityEngine;

public class ChangeTimeout: MonoBehaviour
{
    private void Start()
    {
        // Set timeout to 8 seconds
        Watchdog.Timeout = 8f;
    }
}
```

## Analytics

Using this package prevents ANR data from being automatically sent to Google Play. If you are interested in this data
you can still receive it and send it to any analytics service.

Example code:

```csharp
using Norne;
using UnityEngine;

public class SendStacktrace: MonoBehaviour
{
    private void Start()
    {
        // Check if we got an ANR the last time we ran the application
        if (Watchdog.TryGetStacktrace(out var stacktrace))
        {
            // If we did, send it to our analytics service
            Analytics.Send("ANR", stacktrace);
        }
    }
}
```