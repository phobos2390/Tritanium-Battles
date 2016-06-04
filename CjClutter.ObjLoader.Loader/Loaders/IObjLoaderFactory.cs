namespace ObjLoader.Loader.Loaders
{
    public interface IObjLoaderFactory
    {
        IObjLoader Create(IMaterialStreamProvider materialStreamProvider);
        IObjLoader Create();
        IObjLoader Create(string mtlDirectory, IMaterialStreamProvider materialStreamProvider);
        IObjLoader Create(string mtlDirectory);
    }
}