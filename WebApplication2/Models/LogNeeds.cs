using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using WebApplication2.ENUMS;

namespace WebApplication2.Models
{
    public class LogNeeds
    {
        static NetworkStream stream;
        private List<LogData> logList;
        public static MessageTypeEnum E { get; set; }
        //tells us if we need to print all of the list (like in the begining of page load)
        public static int all=1;
        public string choice
        {
            get
            {
                return null;
            }
            set
            {
                if (value == null)
                {
                    all = 1;
                }

                    if (value == "INFO")
                {
                    E = MessageTypeEnum.INFO;
                    all = 0;
                }

                if (value == "WARNING")
                {
                    E = MessageTypeEnum.WARNING;
                    all = 0;
                }


                if (value == "FAIL")
                {
                    E = MessageTypeEnum.FAIL;
                    all = 0;
                }



            }
        }


        public LogNeeds()
        {
            this.logList = new List<LogData>();
            GetLog();
        }


        public void GetLog()
        {
            List<LogData> temp = new List<LogData>();


            SingletonClient.Instance.Connect();
            stream = SingletonClient.Instance.getClient().GetStream();

            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            JObject obj2 = new JObject();
            obj2["inst"] = "2";
            obj2["etc"] = "1";
            writer.Write(JsonConvert.SerializeObject(obj2));
            string ret = reader.ReadString();
            JObject objRet = JsonConvert.DeserializeObject<JObject>(ret);
            string toBreak = objRet["2"].ToString();

            foreach (string item in toBreak.Split('|'))
            {

                ENUMS.MessageTypeEnum typ;
                if (item.Split(';')[0] == "INFO" || item.Split(';')[0] == "Information")
                {
                    typ = ENUMS.MessageTypeEnum.INFO;
                    LogData logdata = new LogData(typ, item.Split(';')[1]);
                    temp.Add(logdata);
                }

                if (item.Split(';')[0] == "WARNING")
                {
                    typ = ENUMS.MessageTypeEnum.WARNING;
                    LogData logdata = new LogData(typ, item.Split(';')[1]);
                    temp.Add(logdata);
                }

                if (item.Split(';')[0] == "FAIL")
                {
                    typ = ENUMS.MessageTypeEnum.FAIL;
                    LogData logdata = new LogData(typ, item.Split(';')[1]);
                    temp.Add(logdata);
                }


            }
            this.logList = temp;
            SingletonClient.Instance.Closing();
        }




        public List<LogData> getListWithENUM()
        {
            if (all == 1)
            {
                return this.logList;
            }

            List<LogData> temp =new List<LogData>();

            foreach(LogData item in this.logList)
            {
                if (E.ToString() ==item.LogType.ToString())
                {
                    temp.Add(item);
                }
            }



            return temp;
        }






    }
}