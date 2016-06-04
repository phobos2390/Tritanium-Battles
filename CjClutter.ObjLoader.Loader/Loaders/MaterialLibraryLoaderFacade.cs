namespace ObjLoader.Loader.Loaders
{
    public class MaterialLibraryLoaderFacade : IMaterialLibraryLoaderFacade
    {
        private readonly IMaterialLibraryLoader _loader;
        private readonly IMaterialStreamProvider _materialStreamProvider;
        private readonly string _mtlDirectory;

        public MaterialLibraryLoaderFacade(string mtlDirectory, IMaterialLibraryLoader loader, IMaterialStreamProvider materialStreamProvider)
        {
            _loader = loader;
            _materialStreamProvider = materialStreamProvider;
            _mtlDirectory = mtlDirectory;
        }

        public void Load(string materialFileName)
        {
            using (var stream = _materialStreamProvider.Open(_mtlDirectory + materialFileName))
            {
                if (stream != null)
                {
                    _loader.Load(stream);    
                }
            }
        }
    }
}