import {Component, OnInit} from '@angular/core';
import {FormControl, Validators} from "@angular/forms";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {WordModel} from "../models/WordModel";
import {environment} from "../../environments/environment";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
  standalone: false,
})
export class HomePage implements OnInit{

  constructor(private http: HttpClient) {
  }

  Words: WordModel[] = [];
  WordFc: FormControl<string | null> = new FormControl("", [Validators.required, Validators.minLength(1), Validators.maxLength(255)]);

  ngOnInit() {
    this.getMessagesMock();
  }

  async AddWord() {
    if (this.WordFc.value == null) return
    let word :WordModel = {
      word: this.WordFc.value,
    }

    await firstValueFrom(this.http.post<WordModel>(environment.baseURL+"SenndMessage", word))

    this.getMessages()
    console.log(word);
  }

  async DeleteWord(wordId: WordModel) {
    await firstValueFrom(this.http.delete(environment.baseURL + 'Profanity/' + wordId.word));
    this.getMessages()
  }

  private async getMessagesMock() {
    this.Words = [{ word: "Item 1" },{ word: "Item 2" },{ word: "Item 3" },{ word: "Item 4" },{ word: "Item 5" },{ word: "Item 6" },{ word: "Item 7" },{ word: "Item 8" },{ word: "Item 9" },{ word: "Item 10" },{ word: "Item 11" },{ word: "Item 12" },{ word: "Item 13" },{ word: "Item 14" },{ word: "Item 15" },{ word: "Item 16" },{ word: "Item 17" },{ word: "Item 18" },{ word: "Item 19" },{ word: "Item 20" }];
    //TODO remove before production, but its fine for testing visual
    console.log(this.Words)
  }
  private async getMessages() {
    const call = this.http.get<WordModel[]>(environment.baseURL + "Profanity");
    call.subscribe((resData: WordModel[]) => {
      this.Words = resData;
    })
  }
}
