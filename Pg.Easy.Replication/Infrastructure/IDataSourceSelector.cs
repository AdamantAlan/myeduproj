using Npgsql;

namespace Pg.Easy.Replication.Infrastructure
{
    public interface IDataSourceSelector
    {
        void UsePrimary();

        void UseReplica();

        NpgsqlDataSource Current();
    }

    public sealed class DataSourceSelector : IDataSourceSelector
    {
        private readonly NpgsqlDataSource _primary;
        private readonly NpgsqlDataSource _replica2;
        private readonly NpgsqlDataSource _replica3;
        private NpgsqlDataSource _current;

        public DataSourceSelector(
                [FromKeyedServices("Primary")] NpgsqlDataSource primary,
                [FromKeyedServices("Replica2")] NpgsqlDataSource replica2,
                [FromKeyedServices("Replica3")] NpgsqlDataSource replica3)
        {
            _primary = primary;
            _replica2 = replica2;
            _replica3 = replica3;
            _current = primary; // по умолчанию — безопасно писать

        }

        public void UsePrimary() => _current = _primary;
        public void UseReplica() => _current = new Random().Next(0, 2) == 0 ? _replica2 : _replica3;
        public NpgsqlDataSource Current() => _current;
    }
}
