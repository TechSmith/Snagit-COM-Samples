using System;
using System.Collections.Generic;
using System.Text;

//Simple class to hold a list of audio devices found on the system.
namespace VideoSample
{
   class AudioDevice
   {
      public AudioDevice( Int32 id, string name ) { AudioId = id; Name = name; }
      public Int32 AudioId
      {
         get;
         set;
      }
      public string Name
      {
         get;
         set;
      }
      public override string ToString()
      {
         return Name;
      }
   }
}
