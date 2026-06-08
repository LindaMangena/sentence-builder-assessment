# Sentence Builder Backend

Requirements: .NET 8 SDK

Run locally:

```bash
cd backend
dotnet restore
dotnet run
```

API endpoints:
- `GET /api/words` - returns words grouped by type
- `GET /api/sentences` - returns submitted sentences
- `GET /api/sentences/{id}` - returns one submitted sentence
- `POST /api/sentences` - create sentence
- `PUT /api/sentences/{id}` - update sentence
- `DELETE /api/sentences/{id}` - delete sentence

The selectable words are stored in `Data/words.json` and imported into the SQLite database on first run. Submitted sentences are persisted in `sentencebuilder.db`.

Run with Docker:

```bash
docker build -t sentence-builder-api .
docker run --rm -p 5000:80 sentence-builder-api
```
