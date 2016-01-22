# core.logging [![Build status](https://ci.appveyor.com/api/projects/status/ibv6ies216rhdca3?svg=true)](https://ci.appveyor.com/project/nexussays/core-logging)

An extremely light-weight, modular, cross-platform logging library. Logs can be text or structured data. Control how log entries are serialized, where they are output to, what additional structured data should be attached, etc. Or implement the interfaces from scratch.

https://www.nuget.org/packages/nexus.core.logging
```powershell
Install-Package nexus.core.logging
```

## Static `Log`

For most use-cases it is entirely sufficient to just use the static methods on `Log`, e.g. `Log.Trace("log message")`, `Log.Error(exception)`

This uses the singleton log exposed via `LogSource.Instance` so in your main method or other entry point you can set up any decorators and sinks. E.g.:
```c#
LogSource.Instance
         .SetSerializer( new MyCustomLogSerializer() )
         .SetLogLevel( LogLevel.Trace )
         .WithDecorator( new SomeLogEntryDecorator( "foo", "bar", 50 ) )
         .WithSink( new ConsoleLogSink() )
         .WithSink( new DebugLogSink() )
         .WithSink( new LoggingBackend( "tcp://192.168.0.1:14142", serverKey ) );
```

## Custom decorators

There are a multitude of reasons you may want to add custom structured data to your log entries; to do so, simply implement `ILogEntryDecorator`.

The single method `Object Augment( ILogEntry entry, Exception ex, Boolean exceptionHandled )` will be provided the current log entry along with any exception. Return any object you want to attach to the log entry or `null` if there is nothing to add. The object will then be added and accessible from the log entry's `Data` property which you can read from a custom `ILogEntrySerializer` or `ILogSink` as necessary.

E.g.:
```c#
public class MyDecorator : ILogEntryDecorator
{
   public Object Augment( ILogEntry entry, Exception ex, Boolean exceptionHandled )
   {
      if(entry.Severity >= LogLevel.Warn && ex != null)
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

## Custom serialization and log sink (ie, output)

`ILogEntrySerializer` will convert a log entry to a string and `ILogSink` is where you send log entries for output, storage, aggregation, etc.

`ILogSink` has a single method `void Handle( ILogEntry entry, Deferred<String> serializedEntry )`.

If you want to use entirely structured data simple handle the `ILogEntry` as you desire; you can ignore the deferred serialized log entry and never incur the cost of serialization if that isn't something you need in your use-case.

You can also implement a custom serializer to correspond to any custom decorators you are using.
E.g.:
```c#
public class MySerializer : ILogEntrySerializer
{
   private readonly ILogEntrySerializer m_serializer = new DefaultLogEntrySerializer();

   public String Serialize( ILogEntry data )
   {
      var entry = m_serializer.Serialize( data );
      var foo = data.GetData<MyDecorator.FooBar>();
      return foo != null ? $"Found {foo.Baz} on log entry: {entry}" : entry;
   }
}
```

## See Also

* [`core`](https://github.com/nexussays/core)
