import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService, Sentence, Word, WordsByType } from './services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  types: string[] = [];
  wordsByType: WordsByType = {};
  selectedType = '';
  selectedWordId: number | null = null;
  sentenceWords: string[] = [];
  sentences: Sentence[] = [];
  editId: number | null = null;
  loading = true;
  saving = false;
  error = '';

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.api.getWords().subscribe(res => {
      this.wordsByType = res;
      this.types = Object.keys(res);
      this.selectType(this.types[0] || '');
    }, () => this.error = 'Could not load words from the API.');

    this.loadSentences();
  }

  get wordsForSelectedType(): Word[] {
    return this.wordsByType[this.selectedType] || [];
  }

  get currentSentence(): string {
    return this.sentenceWords.join(' ');
  }

  selectType(type: string): void {
    this.selectedType = type;
    this.selectedWordId = this.wordsForSelectedType[0]?.id || null;
  }

  addWord(): void {
    if (!this.selectedWordId) return;

    const word = this.wordsForSelectedType.find(x => x.id === Number(this.selectedWordId));
    if (word) {
      this.sentenceWords.push(word.text);
    }
  }

  removeWord(index: number): void {
    this.sentenceWords.splice(index, 1);
  }

  moveWord(index: number, direction: -1 | 1): void {
    const nextIndex = index + direction;
    if (nextIndex < 0 || nextIndex >= this.sentenceWords.length) return;

    const [word] = this.sentenceWords.splice(index, 1);
    this.sentenceWords.splice(nextIndex, 0, word);
  }

  submitSentence(): void {
    const text = this.currentSentence.trim();
    if (!text || this.saving) return;

    this.saving = true;
    this.error = '';

    const request: Observable<Sentence | void> = this.editId
      ? this.api.updateSentence(this.editId, text)
      : this.api.createSentence(text);

    request.subscribe(() => {
      this.resetAndReload();
      this.saving = false;
    }, () => {
      this.error = 'Could not save the sentence.';
      this.saving = false;
    });
  }

  editSentence(sentence: Sentence): void {
    this.editId = sentence.id;
    this.sentenceWords = sentence.text.split(' ').filter(Boolean);
  }

  deleteSentence(sentence: Sentence): void {
    this.api.deleteSentence(sentence.id).subscribe(() => {
      if (this.editId === sentence.id) {
        this.resetBuilder();
      }
      this.loadSentences();
    }, () => this.error = 'Could not delete the sentence.');
  }

  resetBuilder(): void {
    this.sentenceWords = [];
    this.editId = null;
  }

  resetAndReload(): void {
    this.resetBuilder();
    this.loadSentences();
  }

  loadSentences(): void {
    this.loading = true;
    this.api.getSentences().subscribe(res => {
      this.sentences = res;
      this.loading = false;
    }, () => {
      this.error = 'Could not load submitted sentences.';
      this.loading = false;
    });
  }
}
