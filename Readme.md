# core [![Build status](https://img.shields.io/appveyor/ci/nexussays/core.svg?style=flat-square)](https://ci.appveyor.com/project/nexussays/core) [![NuGet](https://img.shields.io/nuget/v/nexus.core.svg?style=flat-square)](https://www.nuget.org/packages/nexus.core) [![MPLv2 License](https://img.shields.io/badge/license-MPLv2-blue.svg?style=flat-square)](https://www.mozilla.org/MPL/2.0/)

Core utilities and data structures.

## Getting Started

```powershell
Install-Package nexus.core
```

---

## core.logging [![NuGet](https://img.shields.io/nuget/dt/nexus.core.logging.svg?style=flat-square)](https://www.nuget.org/packages/nexus.core.logging)

An extremely light-weight, modular, cross-platform logging library. Logs can be text or structured data. Control how log entries are serialized, where they are output to, what additional structured data should be attached, etc. Or implement the interfaces from scratch.

### Getting Started

There are packages available for .NET 4.5, Android, and iOS.

```powershell
Install-Package nexus.core.logging-net
Install-Package nexus.core.logging-android
Install-Package nexus.core.logging-ios
```

There is a shared PCL library as well if you need to reference core.logging from a PCL library, .Net Core project, etc.

```powershell
Install-Package nexus.core.logging
```

### Static Log

For most use-cases it is entirely sufficient to just use the static methods on `Log`, e.g. `Log.Trace("log message")`, `Log.Error("bad thing happened")`

### Setup

The static methods on `Log` use a singleton `ILogSource` exposed via `LogSource.Instance`. If you aren't creating your own Log instance, in your main method or other entry point you can set up the log information for you application and add any decorators and sinks. E.g.:
```c#
LogSource.Instance
         .SetSerializer<MyCustomLogSerializer>()
         .SetLogLevel( LogLevel.Trace )
         .WithDecorator( new SomeLogEntryDecorator( "foo", "bar", 50 )
         .WithSink( new LoggingBackend( "tcp://192.168.0.1:14142", serverKey ) )
         .WithSink<ConsoleLogSink>()
         .WithSink<DebugLogSink>();
```

### Custom decorators

There are a multitude of reasons you may want to add custom structured data to your log entries; to do so, simply implement `ILogEntryDecorator`.

The single method `Object Augment( ILogEntry entry )` will be provided the current log entry and must return an object to attach to the log entry or `null` if there is nothing to add. The object will then be accessible from the log entry's `Data` property which you can read from a custom `ILogEntrySerializer` or `ILogSink` as necessary.

E.g.:
```c#
public class MyDecorator : ILogEntryDecorator
{
   public Object Augment( ILogEntry entry )
   {
      if(entry.Severity >= LogLevel.Warn)
      {
         return new FooBar() {Baz = "oh noes!"};
      }
      return null;
   }

   public class FooBar
   {
      public String Baz { get; set; }
   }
}
```

### Custom serialization and log sink (ie, output)

`ILogEntrySerializer` will convert a log entry to a string and `ILogSink` is where you send log entries for output, storage, aggregation, etc.

`ILogSink` has a single method `void Handle( ILogEntry entry, Deferred<String> serializedEntry )`.

If you want to use entirely structured data simple handle the `ILogEntry` as you desire; you can ignore the deferred serialized log entry and never incur the cost of serialization if that isn't something you need in your use-case.

You can also implement a custom serializer to correspond to any custom decorators you are using.
E.g.:
```c#
public class MySerializer : ILogEntrySerializer
{
   // compose the default serializer if we don't find our special data attached
   private readonly ILogEntrySerializer m_serializer = new DefaultLogEntrySerializer();

   public String Serialize( ILogEntry data )
   {
      var entry = m_serializer.Serialize( data );
      var foo = data.GetData<MyDecorator.FooBar>();
      return foo != null ? $"Found {foo.Baz} on log entry: {entry}" : entry;
   }
}
```
