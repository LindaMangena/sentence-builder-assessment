# Sentence Builder Frontend

Requirements: Node.js and Angular CLI

Run locally:

```bash
cd frontend
npm install
npm start
```

The app expects the backend at `http://localhost:5000`.

Features:
- Select words grouped by word type.
- Build, reorder, and remove words before saving.
- Create, edit, and delete saved sentences.

Run with Docker:

```bash
docker build -t sentence-builder-web .
docker run --rm -p 4200:80 sentence-builder-web
```
