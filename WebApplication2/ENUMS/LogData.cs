using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ENUMS
{
    public class LogData
    {
        private string logMessage;
        private MessageTypeEnum logType;


        /// <param name="message"></param>
        public LogData(MessageTypeEnum type, string message)
        {
            this.LogMessage = message;
            this.LogType = type;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LogMessage
        {
            get
            {
                return this.logMessage;
            }
            set
            {
                this.logMessage = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public MessageTypeEnum LogType
        {
            get
            {
                return this.logType;
            }
            set
            {
                this.logType = value;
            }
        }
    }
}