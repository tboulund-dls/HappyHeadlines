import {Component, OnInit} from '@angular/core';
import {FormControl, Validators} from "@angular/forms";

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
  standalone: false,
})
export class HomePage implements OnInit{

  constructor() {}

  Words: string[] = [];
  WordFc: FormControl<string | null> = new FormControl("", [Validators.required, Validators.minLength(1), Validators.maxLength(255)]);

  ngOnInit() {
    this.getMessages();
  }

  async AddWord() {
    if (this.WordFc.value == null) return
    const word =  this.WordFc.value;
    console.log(word);
  }

  async DeleteWord(wordId: string) {
    console.log(wordId);
  }

  private async getMessages() {
    this.Words = ["item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9", "Item 10", "Item 11", "Item 12", "Item 13", "Item 14", "Item 15", "Item 16", "Item 17", "Item 18", "Item 19", "Item 20"];
  }
}
