# Sentence Builder Assessment

This project contains a .NET Web API backend and an Angular frontend for building sentences from grouped word types.

## Features

- Load all selectable words from the backend through `GET /api/words`.
- View available words grouped by word type: Noun, Verb, Adjective, Adverb, Pronoun, Preposition, Conjunction, Determiner, and Exclamation.
- Build a sentence by choosing a word type, choosing a word from that type, and adding it to the sentence.
- Reorder or remove words before saving.
- Create, recall, edit, view, and delete submitted sentences.
- Store submitted sentences in a local SQLite database.
- Seed selectable words from a persisted backend data file into SQLite; the frontend does not hardcode the word catalogue.
- Responsive layout for desktop and mobile widths.
- Backend Dockerfile included for containerization.

## Backend

Requirements:

- .NET 8 SDK

Run:

```bash
cd backend
dotnet restore
dotnet run
```

The API runs on `http://localhost:5000` and exposes:

- `GET /api/words`
- `GET /api/sentences`
- `GET /api/sentences/{id}`
- `POST /api/sentences`
- `PUT /api/sentences/{id}`
- `DELETE /api/sentences/{id}`

Container:

```bash
cd backend
docker build -t sentence-builder-api .
docker run --rm -p 5000:80 sentence-builder-api
```

## Frontend

Requirements:

- Node.js
- npm

Run:

```bash
cd frontend
npm install
npm start
```

The Angular app runs on `http://localhost:4200` and expects the API at `http://localhost:5000`.
