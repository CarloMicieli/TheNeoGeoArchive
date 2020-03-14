# TheNeoGeoArchive

A playground application ".NET core all the things" - a tribute/archive to the best gaming console of all time.

The normal application is using Postgres, while tests are running against a Sqlite database.

## Data Loader

Filling the database using a bunch of csv files.

Inside the `src/DataLoader` directory, just type

```
$ dotnet run
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

- `POST api/games/:name/port`: to insert a new game port (to a different platform)

## gRPC

Todo

## GraphQL

Todo
