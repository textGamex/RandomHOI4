using CWTools.Process;
using Microsoft.FSharp.Collections;
using static CWTools.Parser.Types;

namespace Random_HOI4.Logic.Util.CWTool
{
    public static class CWToolsHelper
    {
        #region 快捷生成Leaf对象
        public static Child NewLeaf(string key, int value)
        {            
            return Leaf.Create(KeyValueItem.NewKeyValueItem(Key.NewKey(key), Value.NewInt(value)), CWTools.Utilities.Position.range.Zero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">带""的字符串</param>
        /// <returns></returns>
        public static Child NewLeafWhitQString(string key, string value)
        {
            return Leaf.Create(KeyValueItem.NewKeyValueItem(Key.NewKey(key), Value.NewQString(value)), CWTools.Utilities.Position.range.Zero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">不带""的字符串</param>
        /// <returns></returns>
        public static Child NewLeafWhitString(string key, string value)
        {
            return Leaf.Create(KeyValueItem.NewKeyValueItem(Key.NewKey(key), Value.NewString(value)), CWTools.Utilities.Position.range.Zero);
        }

        public static Child NewLeaf(string key, bool value)
        {
            return Leaf.Create(KeyValueItem.NewKeyValueItem(Key.NewKey(key), Value.NewBool(value)), CWTools.Utilities.Position.range.Zero);
        }

        public static Child NewLeaf(string key, double value)
        {
            return Leaf.Create(KeyValueItem.NewKeyValueItem(Key.NewKey(key), Value.NewFloat(value)), CWTools.Utilities.Position.range.Zero);
        }

        public static Child NewLeafWhitClause(string key, FSharpList<Statement> value)
        {
            return Leaf.Create(KeyValueItem.NewKeyValueItem(Key.NewKey(key), Value.NewClause(value)), CWTools.Utilities.Position.range.Zero);
        }
        #endregion
    }
}
