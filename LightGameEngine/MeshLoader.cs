using ObjLoader.Loader.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightGameEngine.Model
{
    public class MeshLoader
    {
        IDictionary<string, LoadResult> loadedMeshes;

        public MeshLoader()
        {
            loadedMeshes = new Dictionary<string, LoadResult>();
        }

        public LoadResult LoadMesh(string fileName)
        {
            if(loadedMeshes.ContainsKey(fileName))
            {
                return loadedMeshes[fileName];
            }
            else
            {
                string fullFileName = Path.GetFullPath(fileName);

                var objLoaderFactory = new ObjLoaderFactory();
                var objLoader = objLoaderFactory.Create(new MaterialStreamProvider());

                var fileStream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read);

                var result = objLoader.Load(fileStream);
                loadedMeshes[fileName] = result;
                return result;
            }
        }
    }
}
