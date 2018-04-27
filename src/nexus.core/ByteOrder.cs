// Copyright M. Griffie <nexus@nexussays.com>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.

namespace nexus.core
{
   /// <summary>
   /// Enum to represent whether bytes are in little- or big- endian byte order
   /// </summary>
   public enum ByteOrder
   {
      /// <summary>
      /// The first byte (e.g. <c>[0]</c>) represents the least-significant byte
      /// </summary>
      LittleEndian,
      /// <summary>
      /// The first byte (e.g. <c>[0]</c>) represents the most-significant byte
      /// </summary>
      BigEndian
   }
}
