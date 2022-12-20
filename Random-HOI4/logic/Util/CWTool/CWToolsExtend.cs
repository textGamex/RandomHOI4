using CWTools.Process;
using System.Collections.Generic;
using System.Linq;

namespace Random_HOI4.Logic.Util.CWTool
{
    public static class CWToolsExtend
    {
        public static void AddChildDirectly(this Node node, Child child)
        {
            var list = node.AllChildren;
            list.Add(child);
            node.AllChildren = list;
        }

        public static void AddChildsDirectly(this Node node, IEnumerable<Child> child)
        {
            var list = node.AllChildren;
            list.AddRange(child);
            node.AllChildren = list;
        }

        public static void ClearAllChilds(this Node node)
        {
            node.AllChildren = new List<Child>();
        }

        public static void AddNodeDirectly(this Node node, Node addNode)
        {
            node.AddChildDirectly(Child.NewNodeC(addNode));
        }
    }
}
