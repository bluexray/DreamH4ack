using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Log4Net
{
    public class CustomLayout: PatternLayout
    {
        public CustomLayout()
        {
            base.AddConverter("TraceID", typeof(TraceId));
            base.AddConverter("LogMessage", typeof(LogMessage));
        }
    }
}
