// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

using System;

namespace nexus.core
{
   public static partial class Bytes
   {
      /// <summary>
      /// zero byte
      /// </summary>
      public const Byte Null = 0;
      /// <summary>
      /// 32
      /// </summary>
      public const Byte Space = 32;
      /// <summary>
      /// 10
      /// </summary>
      public const Byte Linefeed = 10;

      /// <summary>
      /// The byte order of the underlying host platform
      /// </summary>
      public static ByteOrder HostEnvironmentByteOrder => BitConverter.IsLittleEndian
         ? ByteOrder.LittleEndian
         : ByteOrder.BigEndian;
   }
}
