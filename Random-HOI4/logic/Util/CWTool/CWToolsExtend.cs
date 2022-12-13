using CWTools.Process;
using System.Collections.Generic;

namespace Random_HOI4.logic.Util.CWTool
{
    public static class CWToolsExtend
    {
        public static void AddChild(this Node node, Child child)
        {
            var list = node.AllChildren;
            list.Add(child);
            node.AllChildren = list;
        }

        public static void AddChilds(this Node node, IEnumerable<Child> child)
        {
            var list = node.AllChildren;
            list.AddRange(child);
            node.AllChildren = list;
        }
    }
}
