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
        private readonly NpgsqlDataSource _replica;
        private NpgsqlDataSource _current;

        public DataSourceSelector(
                [FromKeyedServices("Primary")] NpgsqlDataSource primary,
                [FromKeyedServices("Replica")] NpgsqlDataSource replica)
        {
            _primary = primary;
            _replica = replica;
            _current = primary; // по умолчанию — безопасно писать

        }

        public void UsePrimary() => _current = _primary;
        public void UseReplica() => _current = _replica;
        public NpgsqlDataSource Current() => _current;
    }
}
