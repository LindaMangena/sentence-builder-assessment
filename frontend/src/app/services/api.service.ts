import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Word {
  id: number;
  text: string;
}

export interface Sentence {
  id: number;
  text: string;
  createdAt: string;
}

export type WordsByType = Record<string, Word[]>;

@Injectable({ providedIn: 'root' })
export class ApiService {
  private base = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  getWords(): Observable<WordsByType> {
    return this.http.get<WordsByType>(`${this.base}/words`);
  }

  getSentences(): Observable<Sentence[]> {
    return this.http.get<Sentence[]>(`${this.base}/sentences`);
  }

  createSentence(text: string): Observable<Sentence> {
    return this.http.post<Sentence>(`${this.base}/sentences`, { text });
  }

  updateSentence(id: number, text: string): Observable<void> {
    return this.http.put<void>(`${this.base}/sentences/${id}`, { text });
  }

  deleteSentence(id: number): Observable<void> {
    return this.http.delete<void>(`${this.base}/sentences/${id}`);
  }
}
