using System;
using System.IO;
using ObjLoader.Loader.Data.DataStore;
using ObjLoader.Loader.TypeParsers;

namespace ObjLoader.Loader.Loaders
{
    public interface IMaterialStreamProvider
    {
        Stream Open(string materialFilePath);
    }

    public class ObjLoaderFactory : IObjLoaderFactory
    {
        public IObjLoader Create()
        {
            return Create("");
        }

        public IObjLoader Create(IMaterialStreamProvider materialStreamProvider)
        {
            return Create("", materialStreamProvider);
        }

        public IObjLoader Create(string mtlDirectory)
        {
            return Create(mtlDirectory, new MaterialStreamProvider());
        }

        public IObjLoader Create(string mtlDirectory, IMaterialStreamProvider materialStreamProvider)
        {
            var dataStore = new DataStore();
            
            var faceParser = new FaceParser(dataStore);
            var groupParser = new GroupParser(dataStore);
            var normalParser = new NormalParser(dataStore);
            var textureParser = new TextureParser(dataStore);
            var vertexParser = new VertexParser(dataStore);
            var smoothGroupParser = new SmoothGroupParser(dataStore);
            var objectGroupParser = new ObjectGroupParser(dataStore);

            var materialLibraryLoader = new MaterialLibraryLoader(dataStore);
            var materialLibraryLoaderFacade = new MaterialLibraryLoaderFacade(mtlDirectory, materialLibraryLoader, materialStreamProvider);
            var materialLibraryParser = new MaterialLibraryParser(materialLibraryLoaderFacade);
            var useMaterialParser = new UseMaterialParser(dataStore);

            return new ObjLoader(dataStore, faceParser, groupParser, objectGroupParser, smoothGroupParser, normalParser, textureParser, vertexParser, materialLibraryParser, useMaterialParser);
        }
    }
}