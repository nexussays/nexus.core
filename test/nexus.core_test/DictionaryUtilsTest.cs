using System;
using System.Collections.Generic;
using nexus.core;
using NUnit.Framework;

namespace nexus.core_test
{
   [TestFixture]
   public class DictionaryUtilsTest
   {
      private const Int32 INVALID_INT_KEY = 42;
      private const String INVALID_STRING_KEY = "nope";
      private const Int32 VALID_INT_KEY_RETURNS_STRING = 13254646;
      private const String VALID_STRING_KEY_RETURNS_OBJECT = "anonymous";
      private const String VALID_STRING_KEY_RETURNS_STRING = "key";
      private readonly Object INVALID_OBJECT_KEY = INVALID_STRING_KEY;
      private readonly Object VALID_OBJECT_KEY_RETURNS_OBJECT = "anonymous";
      private readonly Object VALID_OBJECT_KEY_RETURNS_STRING = new {anon_key = 1};
      private Dictionary<Int32, Guid> m_intGuid;
      private Dictionary<Object, Object> m_objectObject;
      private Dictionary<Object, String> m_objectString;
      private Dictionary<String, Object> m_stringObject;

      [SetUp]
      public void SetUp()
      {
         m_stringObject = new Dictionary<String, Object>
         {
            {VALID_STRING_KEY_RETURNS_STRING, "value"},
            {"null", null},
            {"int", 28573},
            {VALID_STRING_KEY_RETURNS_OBJECT, new {value = 5}}
         };

         m_objectObject = new Dictionary<Object, Object>
         {
            {VALID_OBJECT_KEY_RETURNS_STRING, "value"},
            {"null", null},
            {"int", 28573},
            {VALID_OBJECT_KEY_RETURNS_OBJECT, new {value = 5}},
            {1, "one"},
            {true, "true"}
         };

         m_objectString = new Dictionary<Object, String>
         {
            {1, "one"},
            {"two", "two"},
            {true, "true"},
            {VALID_OBJECT_KEY_RETURNS_STRING, "anonymous"}
         };

         m_intGuid = new Dictionary<Int32, Guid>
         {
            {Int32.MinValue, Guid.NewGuid()},
            {-594356, Guid.NewGuid()},
            {0, Guid.NewGuid()},
            {VALID_INT_KEY_RETURNS_STRING, Guid.NewGuid()},
            {Int32.MaxValue, Guid.NewGuid()}
         };
      }

      [TearDown]
      public void TearDown()
      {
         m_stringObject = null;
      }

      [Test]
      public void should_return_default_value_when_provided_one_and_key_is_not_in_dictionary()
      {
         var stringAlt = "default";
         var guidAlt = Guid.NewGuid();

         Assert.AreEqual( stringAlt, m_stringObject.GetAs( INVALID_STRING_KEY, stringAlt ) );
         Assert.AreEqual( stringAlt, m_stringObject.GetAs( INVALID_STRING_KEY, () => stringAlt ) );

         Assert.AreEqual( stringAlt, m_objectString.Get( INVALID_OBJECT_KEY, stringAlt ) );
         Assert.AreEqual( stringAlt, m_objectString.Get( INVALID_OBJECT_KEY, () => stringAlt ) );

         Assert.AreEqual( stringAlt, m_objectObject.GetAs( INVALID_OBJECT_KEY, () => stringAlt ) );

         Assert.AreEqual( guidAlt, m_intGuid.Get( INVALID_INT_KEY, guidAlt ) );
         Assert.AreEqual( guidAlt, m_intGuid.Get( INVALID_INT_KEY, () => guidAlt ) );
      }

      [Test]
      public void should_return_default_value_when_provided_one_and_unable_to_cast()
      {
         var intAlt = 23435;

         Assert.AreEqual( intAlt, m_stringObject.GetAs( VALID_STRING_KEY_RETURNS_STRING, intAlt ) );
         Assert.AreEqual( intAlt, m_stringObject.GetAs( VALID_STRING_KEY_RETURNS_STRING, () => intAlt ) );
         
         Assert.AreEqual( intAlt, m_objectObject.GetAs( VALID_OBJECT_KEY_RETURNS_STRING, () => intAlt ) );
         
         Assert.AreEqual( intAlt, m_objectObject.GetAs( VALID_OBJECT_KEY_RETURNS_OBJECT, () => intAlt ) );
      }

      [Test]
      public void should_return_type_default_value_when_no_default_is_provided_and_key_is_not_in_dictionary()
      {
         Assert.AreEqual( default(Object), m_stringObject.Get( INVALID_STRING_KEY ) );

         Assert.AreEqual( default(String), m_objectString.Get( INVALID_OBJECT_KEY ) );

         Assert.AreEqual( default(Object), m_objectObject.Get( INVALID_OBJECT_KEY ) );

         Assert.AreEqual( default(Guid), m_intGuid.Get( INVALID_INT_KEY ) );
      }

      [Test]
      public void should_throw_invalidcastexception_when_unable_to_cast_and_no_default_is_provided()
      {
         Assert.That( () => m_stringObject.GetAs<Int32>( "key" ), Throws.TypeOf<InvalidCastException>() );
      }
   }
}