using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CWTools.Parser.CKParser;
using static CWTools.Process.CK2Process;
using static CWTools.Parser.Types;
using static FParsec.Error;
using CWTools.CSharp;
using CWTools.Process;
using CWTools.Parser;
using Microsoft.VisualBasic;

namespace Random_HOI4.logic.CWTools
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="contents"></param>
        /// <param name="adapter"></param>
        /// <returns>如果成功, 返回true, 否则返回false</returns>
        public static bool TryParseString(string fileName, string contents, out CWToolsAdapter? adapter)
        {
            var result = CKParser.parseEventString(contents, fileName);
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
            var result = CKParser.parseEventFile(filePath);
            if (result.IsSuccess)
                adapter = new CWToolsAdapter(processEventFile(result.GetResult()));
            else
                adapter = null;
            return result.IsSuccess;
        }
    }
}
