using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegExRazorReplace.Data
{
  [Serializable]
  public class ReplaceMatchSegmentData : ReplaceSegmentData
  {
    public ReplaceMatchSegmentData(bool isFirst, bool isLast, Match match) 
      : base(isFirst: isFirst, isLast: isLast, content: null)
    {
      this.Match = match;
    }

    public  Match Match { get; }
    public override string Content => this.Match.Value;
  }
}
