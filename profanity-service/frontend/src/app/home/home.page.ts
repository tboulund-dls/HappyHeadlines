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
    this.getMessages();
  }

  async AddWord() {
    if (this.WordFc.value == null) return
    let word :WordModel = {
      word: this.WordFc.value!,
    }

    const call = this.http.post<WordModel>(environment.baseURL,word);
    const newWord = await firstValueFrom(call);

    this.Words.push(newWord)
  }

  async DeleteWord(wordId: WordModel) {

    await firstValueFrom(this.http.delete(environment.baseURL + '/' + wordId.word));

    this.getMessages()
  }

  private async getMessages() {
    const call = this.http.get<WordModel[]>(environment.baseURL);
    call.subscribe((resData: WordModel[]) => {
      this.Words = resData;
    })
  }
}
