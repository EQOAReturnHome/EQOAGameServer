using System;
using System.Collections.Generic;
using System.IO;

namespace EQLogger
{
    public static class Logger
    {
        static StreamWriter writer = File.AppendText("info.log");
        static StreamWriter errwriter = File.AppendText("err.log");


        public static void Info(string logMessage)
        {
            if (writer.AutoFlush == false)
            {
                writer.AutoFlush = true;
            }

            writer.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")} : {logMessage}");
        }

        public static void Info(byte[] logMessage)
        {
            if (writer.AutoFlush == false)
            {
                writer.AutoFlush = true;
            }

            ///Writes lists into logs for us
            writer.Write($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")} :");
            foreach (byte b in logMessage)
            {
                writer.Write($" {b.ToString("X")}");
            }

            ///Ends our current list/message line
            writer.WriteLine("");
        }

        public static void Info(List<byte> logMessage)
        {
            ///Writes lists into logs for us
            writer.Write($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")} :");
            foreach (byte b in logMessage)
            {
                writer.Write($" {b.ToString("X")}");
            }

            ///Ends our current list/message line
            writer.WriteLine("");
        }

        public static void Err(string errMessage)
        {
            if (errwriter.AutoFlush == false)
            {
                errwriter.AutoFlush = true;
            }

            errwriter.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")} : {errMessage}");
        }
    }
}
