# STLink-Manager (STLinkDevice Package)
![Build history](https://buildstats.info/nuget/STLinkDevice)

STLink USB device helper class for C#

## Install package

Search for ```STLinkDevice``` Package 

## How to use this nuget package?

```
using System.Collections.Generic;
using STLinkDevice;
namespace ConsoleApp
{
    class Program
    {
    
        static void Main(string[] args)
        {
            bool result = STLinkManager.Instance.ExecuteSTLinkCmd(arg[0],arg[1])

            ...
            ...
            ...
        }
    }
}
```

## Contributing

Feedback is welcome and pull requests get accepted.
