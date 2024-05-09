namespace GenericSmallBusinessApp.Server.Interfaces
{
    public interface IPrimaryRepository<T> where T : class
    {
        public Task<bool> Add(T type);
        public Task<bool> Delete(int id);
        public Task<List<T>> GetAll();
        public Task<T> GetById(int id);
        public Task<bool> Update(T type);
    }
}
