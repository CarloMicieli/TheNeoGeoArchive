using FluentMigrator;

namespace TheNeoGeoArchive.Infrastructure.Migrations
{
    [Migration(20200308000000)]
    public sealed class InitialMigration : Migration
    {
        private const string Platforms = "platforms";
        private const string Games = "games";

        public override void Up()
        {
            Create.Table(Platforms)
                .WithColumn("platform_id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(25).NotNullable()
                .WithColumn("manufacturer").AsString(50).NotNullable()
                .WithColumn("generation").AsInt32().Nullable()
                .WithColumn("released_at").AsDateTime().Nullable()
                .WithColumn("discontinued").AsDateTime().Nullable()
                .WithColumn("introductory_price").AsDecimal().Nullable()
                .WithColumn("units_sold").AsInt32().Nullable()
                .WithColumn("created_at").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);

            Create.Table(Games)
                .WithColumn("game_id").AsGuid().PrimaryKey()
                .WithColumn("title").AsString(100).NotNullable()
                .WithColumn("genre").AsString(50).NotNullable()
                .WithColumn("modes").AsString(50).NotNullable()
                .WithColumn("series").AsString(100).Nullable()
                .WithColumn("developer").AsString(100).Nullable()
                .WithColumn("publisher").AsString(100).Nullable()
                .WithColumn("release").AsInt32().Nullable()
                .WithColumn("created_at").AsDateTime().Nullable()
                .WithColumn("version").AsInt32().WithDefaultValue(1);
        }

        public override void Down()
        {
            Delete.Table(Games);
            Delete.Table(Platforms);
        }
    }
}