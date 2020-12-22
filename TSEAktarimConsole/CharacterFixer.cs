using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appCS
{
  public static  class CharacterFixer
    {
      public static string fixto8(string str)
      {
          int l = str.Length;
          if (l == 8)
              return str;
          int zerocount = 8 - l;
          string sifirekle = string.Empty;

			
          for (int i = 0;i<zerocount; i++)
          {
              sifirekle += "0";
          }
          return sifirekle + str;
      }
      public static string dynamicfix(string str,int fixcount)
      {
          int l = str.Length;
          if (l == fixcount)
              return str;
          int zerocount = fixcount - l;
          string sifirekle = string.Empty;


          for (int i = 0; i < zerocount; i++)
          {
              sifirekle += "0";
          }
          return sifirekle + str;
      }
      public static DateTime? strDate(string sDate)
      {
          try
          {
              return Convert.ToDateTime(sDate);
          }
          catch
          {
              return null;
          }
      
      }
    }
}
