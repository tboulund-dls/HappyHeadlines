import {Component, Input, input, OnInit} from '@angular/core';
import {Article} from "../../models/Article";

@Component({
  selector: 'app-articlereader',
  templateUrl: './articlereader.component.html',
  styleUrls: ['./articlereader.component.scss'],
  standalone: false,
})
export class ArticlereaderComponent {
  @Input() article!: Article;

  constructor() { }


}
