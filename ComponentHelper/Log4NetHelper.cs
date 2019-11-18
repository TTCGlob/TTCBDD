using log4net;
using log4net.Appender;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.ComponentHelper
{
    public class Log4NetHelper
    {
        private static ILog _XmlLogger;

        public static ILog GetXmlLogger(Type type)
        {
            if (_XmlLogger != null) return _XmlLogger;
            XmlConfigurator.Configure();
            _XmlLogger = LogManager.GetLogger(type);
            return _XmlLogger;
        }
    }
}
