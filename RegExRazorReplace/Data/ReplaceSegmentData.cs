using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegExRazorReplace.Data
{
  [Serializable]
  public class ReplaceSegmentData
  {
    public ReplaceSegmentData(bool IsFirst)
    {
      this.IsFirst = IsFirst;
    }

    public ReplaceSegmentData(bool isFirst, bool isLast, string content)
    {
      this.IsFirst = isFirst;
      this.IsLast = isLast;
      this.Content = content;
    }

    public bool IsFirst { get; }
    public bool IsLast { get; }
    public virtual string Content { get; }
  }
}
