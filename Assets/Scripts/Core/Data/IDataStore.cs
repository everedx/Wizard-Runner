namespace Core.Data
{
    /// <summary>
    /// Interface for data store
    /// </summary>
    public interface IDataStore
    {
        void preSave();

        void postLoad();
    }
}