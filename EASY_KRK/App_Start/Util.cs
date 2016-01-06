using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace EASY_KRK.App_Start
{
    public class Util
    {
        public static string IgnorujPolskieZnaki(string s)
            {
              string formD = s.Normalize(NormalizationForm.FormD);
              StringBuilder sb = new StringBuilder();

              foreach (char ch in formD)
              {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                  sb.Append(ch);
                }
              }

              return sb.ToString().Normalize(NormalizationForm.FormC);
            }
    }
}