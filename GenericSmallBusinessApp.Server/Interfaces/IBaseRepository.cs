namespace GenericSmallBusinessApp.Server.Interfaces
{
    public interface IBaseRepository
    {
        public Task SaveData<P>(string sql, P parameters);
        public Task<T> GetObject<T, P>(string sql, P parameters);
        public Task<List<T>> GetData<T, P>(string sql, P parameters);
    }
}
