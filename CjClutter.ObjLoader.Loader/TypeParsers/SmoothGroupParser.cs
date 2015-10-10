using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.TypeParsers.Interfaces;

namespace ObjLoader.Loader.TypeParsers
{
    public class SmoothGroupParser : TypeParserBase, IGroupParser
    {
        private readonly IGroupDataStore _groupDataStore;

        public SmoothGroupParser(IGroupDataStore groupDataStore)
        {
            _groupDataStore = groupDataStore;
        }

        protected override string Keyword
        {
            get { return "s"; }
        }

        public override void Parse(string line)
        {
            _groupDataStore.PushGroup(line);
        }
    }
}
