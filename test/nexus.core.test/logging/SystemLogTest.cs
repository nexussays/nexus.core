using System;
using nexus.core.logging;
using nexus.core.time;
using NUnit.Framework;

namespace nexus.core.test.logging
{
   [TestFixture]
   internal class SystemLogTest
   {
      private SystemLog m_log;

      [SetUp]
      public void Setup()
      {
         m_log = new SystemLog( new DefaultTimeProvider() );
      }

      [TestCase( LogLevel.Error )]
      [TestCase( LogLevel.Warn )]
      [TestCase( LogLevel.Info )]
      [TestCase( LogLevel.Trace )]
      public void after_writing_1_log_entry_a_newly_attached_sink_will_receive_that_entry( LogLevel level )
      {
         const Int32 count = 1;
         WriteRandomStringsToLog( level, count );
         var handledEntries = 0;
         m_log.AddSink( LogUtils.CreateLogSink( ( e, s ) => { handledEntries++; } ) );
         Assert.That( handledEntries, Is.EqualTo( count ) );
      }

      [TestCase( LogLevel.Error )]
      [TestCase( LogLevel.Warn )]
      [TestCase( LogLevel.Info )]
      [TestCase( LogLevel.Trace )]
      public void after_writing_1_log_entry_an_attached_sink_will_receive_that_entry( LogLevel level )
      {
         const Int32 count = 1;
         var handledEntries = 0;
         m_log.AddSink( LogUtils.CreateLogSink( ( e, s ) => { handledEntries++; } ) );
         WriteRandomStringsToLog( level, count );
         Assert.That( handledEntries, Is.EqualTo( count ) );
      }

      [TestCase( LogLevel.Error )]
      [TestCase( LogLevel.Warn )]
      [TestCase( LogLevel.Info )]
      [TestCase( LogLevel.Trace )]
      public void after_writing_50_log_entries_a_newly_attached_sink_will_receive_them_all_in_sequence_order(
         LogLevel level )
      {
         const Int32 count = 50;
         WriteRandomStringsToLog( level, count );
         var seqCheck = AddSinkCheckSequence( 0 );
         Assert.That(
            seqCheck.Item1,
            Is.EqualTo( count ),
            "On index {0} sequence number was {1}".F( seqCheck.Item1, seqCheck.Item2 ) );
      }

      [TestCase( LogLevel.Error )]
      [TestCase( LogLevel.Warn )]
      [TestCase( LogLevel.Info )]
      [TestCase( LogLevel.Trace )]
      public void after_writing_50_log_entries_a_newly_attached_sink_will_receive_all_entries( LogLevel level )
      {
         const Int32 count = 50;
         WriteRandomStringsToLog( level, count );
         var handledEntries = 0;
         m_log.AddSink( LogUtils.CreateLogSink( ( e, s ) => { handledEntries++; } ) );
         Assert.That( handledEntries, Is.EqualTo( count ) );
      }

      [TestCase( LogLevel.Error )]
      [TestCase( LogLevel.Warn )]
      [TestCase( LogLevel.Info )]
      [TestCase( LogLevel.Trace )]
      public void after_writing_50_log_entries_an_attached_sink_will_have_received_all_entries( LogLevel level )
      {
         const Int32 count = 50;
         var handledEntries = 0;
         m_log.AddSink( LogUtils.CreateLogSink( ( e, s ) => { handledEntries++; } ) );
         WriteRandomStringsToLog( level, count );
         Assert.That( handledEntries, Is.EqualTo( count ) );
      }

      [TestCase( LogLevel.Error )]
      [TestCase( LogLevel.Warn )]
      [TestCase( LogLevel.Info )]
      [TestCase( LogLevel.Trace )]
      public void after_writing_more_than_50_log_entries_a_newly_attached_sink_will_receive_the_50_most_recent_entries(
         LogLevel level )
      {
         const Int32 count = 61;
         WriteRandomStringsToLog( level, count );
         var handledEntries = 0;
         m_log.AddSink( LogUtils.CreateLogSink( ( e, s ) => { handledEntries++; } ) );
         Assert.That( handledEntries, Is.EqualTo( count ) );
      }

      [TestCase( LogLevel.Error )]
      [TestCase( LogLevel.Warn )]
      [TestCase( LogLevel.Info )]
      [TestCase( LogLevel.Trace )]
      public void after_writing_more_than_50_log_entries_an_attached_sink_will_have_received_all_entries( LogLevel level )
      {
         const Int32 count = 61;
         var handledEntries = 0;
         m_log.AddSink( LogUtils.CreateLogSink( ( e, s ) => { handledEntries++; } ) );
         WriteRandomStringsToLog( level, count );
         Assert.That( handledEntries, Is.EqualTo( count ) );
      }

      [TestCase( LogLevel.Error )]
      [TestCase( LogLevel.Warn )]
      [TestCase( LogLevel.Info )]
      [TestCase( LogLevel.Trace )]
      public void after_writing_a_log_entry_attached_log_sinks_will_receive_the_proper_message( LogLevel level )
      {
         const Int32 count = 1;
         String entryValue = null;
         m_log.AddSink( LogUtils.CreateLogSink( ( e, s ) => { entryValue = e.FormatMessageAndArguments(); } ) );
         var writtenValue = WriteRandomStringsToLog( level, count );
         Assert.That( entryValue, Is.EqualTo( writtenValue[0] ) );
      }

      [TestCase( LogLevel.Error )]
      [TestCase( LogLevel.Warn )]
      [TestCase( LogLevel.Info )]
      [TestCase( LogLevel.Trace )]
      public void after_writing_a_log_entry_newly_attached_log_sinks_will_receive_the_proper_message( LogLevel level )
      {
         const Int32 count = 1;
         var writtenValue = WriteRandomStringsToLog( level, count );
         var check = AddSinkCheckEntryMessages( writtenValue );
         Assert.That( check.Item2, Is.Null, "Expected log message \"{0}\", actual message: ".F( check.Item1 ) );
      }

      private String[] WriteRandomStringsToLog( LogLevel level, Int32 count )
      {
         var results = new String[count];
         for(var x = 0; x < count; ++x)
         {
            var rand = TestContext.CurrentContext.Random.GetString( 10 );
            results[x] = "test {0}".F( rand );
            m_log.Write( level, "test {0}", rand );
         }
         return results;
      }

      private Tuple<String, String> AddSinkCheckEntryMessages( String[] expectedMessages )
      {
         var index = 0;
         String failedMessage = null;
         String expectedMessage = null;
         m_log.AddSink(
            LogUtils.CreateLogSink(
               ( e, s ) =>
               {
                  if(failedMessage != null)
                  {
                     // if we've already failed just no-op
                     return;
                  }
                  var msg = e.FormatMessageAndArguments();
                  expectedMessage = expectedMessages[index];
                  if(msg != expectedMessages[index])
                  {
                     failedMessage = msg;
                     return;
                  }
                  index++;
               } ) );
         return Tuple.Create( expectedMessage, failedMessage );
      }

      private Tuple<Int32, Int32> AddSinkCheckSequence( Int32 initialSequenceNumber )
      {
         var expectedSeqNumber = initialSequenceNumber;
         var failedSeqNumber = -1;
         m_log.AddSink(
            LogUtils.CreateLogSink(
               ( e, sequenceNumber ) =>
               {
                  if(failedSeqNumber != -1)
                  {
                     // if we've already failed just no-op
                     return;
                  }
                  if(sequenceNumber != expectedSeqNumber)
                  {
                     failedSeqNumber = sequenceNumber;
                     return;
                  }
                  expectedSeqNumber++;
               } ) );
         return Tuple.Create( expectedSeqNumber, failedSeqNumber );
      }

      [Test]
      public void default_log_level_is_trace()
      {
         Assert.That( m_log.CurrentLevel, Is.EqualTo( LogLevel.Trace ) );
      }
   }
}