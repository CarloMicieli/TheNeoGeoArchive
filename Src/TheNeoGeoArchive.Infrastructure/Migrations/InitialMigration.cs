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
                .WithColumn("name").AsString(50).NotNullable()
                .WithColumn("slug").AsString(50).NotNullable()
                .WithColumn("manufacturer").AsString(50).NotNullable()
                .WithColumn("generation").AsInt32().Nullable()
                .WithColumn("platform_type").AsString(50).NotNullable()
                .WithColumn("released_jp").AsDateTime().Nullable()
                .WithColumn("released_eu").AsDateTime().Nullable()
                .WithColumn("released_na").AsDateTime().Nullable()
                .WithColumn("discontinued").AsInt32().Nullable()
                .WithColumn("introductory_price").AsDecimal().Nullable()
                .WithColumn("units_sold").AsInt32().Nullable()
                .WithColumn("media").AsString(50).Nullable()
                .WithColumn("cpu").AsString(250).Nullable()
                .WithColumn("memory").AsString(250).Nullable()
                .WithColumn("display").AsString(250).Nullable();

            Create.Index("IX_Platforms_Slug")
                .OnTable(Platforms)
                .OnColumn("slug")
                .Unique();

            Create.Table(Games)
                .WithColumn("game_id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString(25).NotNullable()
                .WithColumn("title").AsString(100).NotNullable()
                .WithColumn("genre").AsString(50).NotNullable()
                .WithColumn("modes").AsString(50).NotNullable()
                .WithColumn("series").AsString(100).Nullable()
                .WithColumn("developer").AsString(100).Nullable()
                .WithColumn("publisher").AsString(100).Nullable()
                .WithColumn("year").AsInt32().Nullable()
                .WithColumn("release_mvs").AsDateTime().Nullable()
                .WithColumn("release_aes").AsDateTime().Nullable()
                .WithColumn("release_cd").AsDateTime().Nullable();

            Create.Index("IX_Games_Name")
                .OnTable(Games)
                .OnColumn("name")
                .Unique();
        }

        public override void Down()
        {
            Delete.Table(Games);
            Delete.Table(Platforms);
        }
    }
}
