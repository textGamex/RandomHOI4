using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWTools.Parser;
using CWTools.Common;
using CWTools.Process;
using Random_HOI4.Logic.Util.CWTool;
using static CWTools.Process.CK2Process;
using Random_HOI4.Logic.GameModel.State;
using System.IO;
using NLog;

namespace Random_HOI4.Logic.GameModel
{
    internal class HOI4ModFile
    {
        private readonly List<RandomState> _states = new List<RandomState>();
        private static readonly string _modFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Paradox Interactive", "Hearts of Iron IV", "mod");
        private static readonly string _modRootPath = Path.Combine(_modFolderPath, FolderName);
        public const string FolderName = "Random_Game";

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static class Key
        {
            public const string VERSION = "version";            
            public const string SUPPORTED_VERSION = "supported_version";            
            public const string NAME = "name";            
            public const string PATH = "path";            
        }

        public const string MODE_NAME = "Random Game";
        public const string VERSION = "1.0.0-beta";
        public const string SUPPORTED_VERSION = "1.12.*";        

        public void Write()
        {
            WriteModDescriptorFile();
            WriteStateFiles();
        }
        
        private void WriteModDescriptorFile()
        {
            var path = Path.Combine(_modFolderPath, $"{FolderName}.mod");

            File.WriteAllText(Path.Combine(_modRootPath, "descriptor.mod"), CreateModDescriptorFile());
            File.WriteAllText(path, CreateModDescriptorFile());
        }

        private static string CreateModDescriptorFile()
        {
            var node = EventRoot.Create("events");
            node.AddChildDirectly(CWToolsHelper.NewLeafWhitQString(Key.VERSION, VERSION));
            node.AddChildDirectly(CWToolsHelper.NewLeafWhitQString(Key.NAME, MODE_NAME));
            node.AddChildDirectly(CWToolsHelper.NewLeafWhitQString(Key.SUPPORTED_VERSION, SUPPORTED_VERSION));
            node.AddChildDirectly(CWToolsHelper.NewLeafWhitQString(Key.PATH, $"mod/{FolderName}"));
            var root = Child.NewNodeC(node);

            return CKPrinter.printKeyValueList(root.node.ToRaw, 0);
        }

        private void WriteStateFiles()
        {
            if (_states.Count == 0)
            {
                return;
            }

            var path = Path.Combine(_modRootPath, "history", "states");
            Directory.CreateDirectory(path);
            foreach (var state in _states)
            {                
                File.WriteAllText(Path.Combine(path, state.FileName), state.Content);
                //_logger.Info("{}", );
            }
        }

        public void AddState(RandomState state)
        {
            _states.Add(state);
        }
    }
}
