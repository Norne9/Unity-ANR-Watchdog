# ANR Watchdog

## Overview

This package will allow you to reduce the number of ANRs of your application. This is achieved by using a watchdog timer
which will restart the application in case something goes wrong.

## Installation instructions

[Official Package Manager installation instructions](https://docs.unity3d.com/Manual/upm-ui-install.html)

## Workflows

After adding the package to your project you are ready to go!

### Timeout Configuration

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

### Analytics

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

## Reference

### Watchdog.Timeout

You can change the timeout after which your app will restart. The ANR detection timeout in Android is 10 seconds.
Therefore, I recommend keeping it below this value. The default value is 9 seconds.

##### Example:

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

### Watchdog.TryGetStacktrace(out string stacktrace)

Allows you to get the stack trace of an application if it has terminated on a watchdog timer.

| Name | Description |
| ---- | ----------- |
| stacktrace | *System.String*<br>Stack trace of the main java thread of an application. In JSON format. |

#### Returns

True in case an application has terminated on a watchdog timer. False otherwise.

##### Example:

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

## Samples

- **Watchdog Sample** -
  Contains a sample that shows how the app reacts to ANRs and how to change the timeout
