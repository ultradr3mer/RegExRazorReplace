using RegExRazorReplace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegExRazorReplace.Util
{
  internal static class RegexReplaceExtension
  {
    public static IList<ReplaceSegmentData> ReplaceWithNonMatches(this Regex regex, string text)
    {
      if (text == null)
      {
        return new List<ReplaceSegmentData>();
      }

      System.Collections.Generic.List<string> matches = regex.Matches(text).Cast<Match>().Select(m => m.Value).ToList();

      var matchCollection = regex.Matches(text);
      if (matchCollection.Count == 0)
      {
        return new List<ReplaceSegmentData>() {new ReplaceSegmentData(isFirst: true,
                                                                      isLast: true,
                                                                      content: text)};
      }

      int index = 0;
      IList<ReplaceSegmentData> result = new List<ReplaceSegmentData>();
      int matchIndex = 0;
      int matchCount = matchCollection.Count;
      foreach (Match match in matchCollection)
      {

        result.Add(new ReplaceSegmentData(isFirst: matchIndex == 0,
                                          isLast: false,
                                          content: text.Substring(index, match.Index - index)));

        result.Add(new ReplaceMatchSegmentData(isFirst: matchIndex == 0,
                                               isLast: matchIndex == matchCollection.Count - 1,
                                               match: match));

        index = match.Index + match.Length;
        matchIndex++;
      }

      result.Add(new ReplaceSegmentData(isFirst: matchIndex == 0,
                                        isLast: true,
                                        content: text.Substring(index, text.Length - index)));

      return result;
    }
  }
}
