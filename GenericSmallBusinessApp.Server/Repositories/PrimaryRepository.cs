using GenericSmallBusinessApp.Server.Interfaces;
using GenericSmallBusinessApp.Server.Models;

namespace GenericSmallBusinessApp.Server.Repositories
{
    public class PrimaryRepository<T>(IBaseRepository repository) : IPrimaryRepository<T> where T : class
    {
        public async Task<bool> Add(T type)
        {
            var (properties, values, parameter) = AssignPropertiesAndValues();
            try
            {
                string sql = $"INSERT INTO [{typeof(T).Name}]({properties}) VALUES ({values})";
                await repository.SaveData(sql, type);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                string sql = $"DELETE FROM [{typeof(T).Name}] WHERE {typeof(T).Name}Id = @Id";
                await repository.SaveData(sql, new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                string sql = $"SELECT * FROM [{typeof(T).Name}]";
                var list = await repository.GetData<T, dynamic>(sql, new { });
                return list;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                string sql = $"SELECT * FROM [{typeof(T).Name}] WHERE {typeof(T).Name}Id = @Id";
                var result = await repository.GetObject<T, dynamic>(sql, new { Id = id });
                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<bool> Update(T type)
        {
            var (properties, values, parameter) = AssignPropertiesAndValues();
            try
            {
                string sql = $"UPDATE [{typeof(T).Name}] SET {parameter} WHERE {typeof(T).Name}Id = @{typeof(T).Name}Id";
                await repository.SaveData(sql, type);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        private static (string, string, string) AssignPropertiesAndValues()
        {
            if (typeof(T) == typeof(User))
            {
                var properties = string.Join(", ", typeof(T).GetProperties().Skip(3).Select(p => p.Name));
                var values = string.Join(", ", typeof(T).GetProperties().Skip(3).Select(p => "@" + p.Name));
                var parameter = string.Join(", ", string.Join(", ", typeof(T).GetProperties().Skip(3).Select(p => p.Name).Skip(2).Select(name => $"{name} = @{name}")));
                return (properties, values, parameter);
            }
            else
            {
                var properties = string.Join(", ", typeof(T).GetProperties().Skip(1).Select(p => p.Name));
                var values = string.Join(", ", typeof(T).GetProperties().Skip(1).Select(p => "@" + p.Name));
                var parameter = string.Join(", ", string.Join(", ", typeof(T).GetProperties().Skip(1).Select(p => p.Name).Select(name => $"{name} = @{name}")));
                return (properties, values, parameter);
            }
        }
    }
}
