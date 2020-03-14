# TheNeoGeoArchive
![.NET Core](https://github.com/CarloMicieli/TheNeoGeoArchive/workflows/.NET%20Core/badge.svg)

![Neo Geo](logo.png)

A playground application ".NET core all the things" - a tribute/archive to the best gaming console of all time.

The normal application is using Postgres, while tests are running against a Sqlite database.

## Data Loader

Filling the database using a bunch of csv files.

Inside the `src/DataLoader` directory, just type

```
$ dotnet run -- --mode rest
```

## The web api

```
$ dotnet run --project Src/TheNeoGeoArchive.WebApi
```

To see the OpenApi document go to [https://localhost:5001/swagger](https://localhost:5001/swagger).

### Games

The main web api is for Neo Geo games:

- `POST https://localhost:5001/api/v1/games`: to insert a new game

```javascript
{
    "gameId": "b0b576da-9ede-4d39-8a21-1970988af58c",
    "name": "fatfury1",
    "title": "Fatal Fury: King of Fighters",
    "genre": "Fighting",
    "modes": "Single-player, Multiplayer",
    "series": "Fatal Fury",
    "developer": "SNK",
    "publisher": "SNK",
    "year": 1991,
    "release": {
        "mvs": "1991-11-25T00:00:00",
        "aes": "1991-12-20T00:00:00",
        "cd": "1994-09-09T00:00:00"
    }
}
```

- `GET https://localhost:5001/api/v1/games`: returns all games
- `GET https://localhost:5001/api/v1/games/:name`: return the game with the `:name` name
- `GET https://localhost:5001/api/v1/games/id/:id`: return the game with the `:id` identifier

- `GET https://localhost:5001/api/v1/genres`: returns all games by genre

```javascript
[
    {
        "genre": "Fighting, Sports",
        "games": 
        [
            {
                "gameId": "61fcaa73-f8be-4a86-8880-7fef64fb5908",
                "name": "3countb",
                "title": "3 Count Bout"
            }
        ]
    }
]
```

- `GET https://localhost:5001/api/v1/genres/:genre`: returns all games with the `:genre` genre


### Platforms

- `POST https://localhost:5001/api/v1/platforms`: to insert a new platform

```javascript
{
    "platformId": "c101d369-cf37-4850-87ec-9866be46f812",
    "name": "Neo Geo AES",
    "slug": "neogeo",
    "manufacturer": "SNK Corporation",
    "generation": 4,
    "type": "Home video game console",
    "release": {
        "japan": "1990-04-26T00:00:00",
        "northAmerica": "1990-08-22T00:00:00",
        "europe": "1991-01-01T00:00:00"
    },
    "discontinued": 1997,
    "introductoryPrice": 64999.00000,
    "unitsSold": 1000000,
    "media": "ROM cartridge",
    "cpu": "Motorola 68000 @ 12MHz, Zilog Z80A @ 4MHz",
    "memory": "64KB RAM, 84KB VRAM, 2KB Sound Memory",
    "display": "320�224 resolution, 4096 on-screen colors out of a palette of 65536"
}
```

- `GET https://localhost:5001/api/v1/platforms`: returns all platforms
- `GET https://localhost:5001/api/v1/platforms/:slug`: return the platform with the `:slug` value

## gRPC

```
$ dotnet run --project Src/TheNeoGeoArchive.GrpcServices
```

To have a nice CLI tool, install [Evans](https://github.com/ktr0731/evans/releases).

```
$ evans -p 5001

  ______
 |  ____|
 | |__    __   __   __ _   _ __    ___
 |  __|   \ \ / /  / _. | | '_ \  / __|
 | |____   \ V /  | (_| | | | | | \__ \
 |______|   \_/    \__,_| |_| |_| |___/

 more expressive universal gRPC client


games.Games@127.0.0.1:5001>
```

```
games.Games@127.0.0.1:5001> show api
+---------------+----------------------+-----------------+
|      RPC      |     REQUEST TYPE     |  RESPONSE TYPE  |
+---------------+----------------------+-----------------+
| CreateGame    | CreateGameRequest    | CreateGameReply |
| GetGameByName | GetGameByNameRequest | GameInfo        |
| GetGames      | GetGamesRequest      | GameInfo        |
+---------------+----------------------+-----------------+
```

```
games.Games@127.0.0.1:5001> service Games

games.Games@127.0.0.1:5001> call CreateGame
gameId (TYPE_STRING) => 
```

## GraphQL

Todo
