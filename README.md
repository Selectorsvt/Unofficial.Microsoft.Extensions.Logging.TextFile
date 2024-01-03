# Unofficial.Microsoft.Extensions.Logging.TextFile
### How to Use:
#### Add to Program.cs:
```C#
builder.Logging.AddFileLogger();
```
#### Add to application.json
```json
"Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "File": {
      "FileMap": {
        "Information": "Logs\\info.txt",
        "Warning": "Logs\\warn.txt"
      }
    }
  }
```
 