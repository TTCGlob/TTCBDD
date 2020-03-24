using log4net;
using log4net.Config;
using System;

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
