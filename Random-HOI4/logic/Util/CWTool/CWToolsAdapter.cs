using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CWTools.Parser.CKParser;
using static CWTools.Process.CK2Process;
using static CWTools.Parser.Types;
using static FParsec.Error;
using CWTools.CSharp;
using CWTools.Process;
using CWTools.Parser;

namespace Random_HOI4.Logic.Util.CWTool
{
    /// <summary>
    /// CWTools的适配器.
    /// </summary>
    internal class CWToolsAdapter
    {
        public EventRoot Root { get; }

        private CWToolsAdapter(EventRoot eventRoot)
        {
            Root = eventRoot;
        }

        static CWToolsAdapter()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contents"></param>
        /// <param name="adapter"></param>
        /// <returns>如果成功, 返回true, 否则返回false</returns>
        public static bool TryParseString(string fileName, string contents, out CWToolsAdapter? adapter)
        {
            var result = parseEventString(contents, fileName);
            if (result.IsSuccess)
                adapter = new CWToolsAdapter(processEventFile(result.GetResult()));
            else
                adapter = null;
            return result.IsSuccess;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="adapter"></param>
        /// <returns>如果成功, 返回true, 否则返回false</returns>
        public static bool TryParseFile(string filePath, out CWToolsAdapter? adapter)
        {
            var result = parseEventFile(filePath);
            if (result.IsSuccess)
                adapter = new CWToolsAdapter(processEventFile(result.GetResult()));
            else
                adapter = null;
            return result.IsSuccess;
        }
    }
}
