import {Component, OnInit} from '@angular/core';
import {Article} from "../models/Article";
import {DraftendpointsService} from "../services/draftendpoints.service";
import {ModalController} from "@ionic/angular";
import {ArticlereaderComponent} from "./articlereader/articlereader.component";

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
  standalone: false,
})
export class HomePage implements OnInit {
  articles: Article[] = [];

  constructor(
    private draftService: DraftendpointsService,
    private modalController: ModalController
              ) {}

  ngOnInit() {
    //this.loadPublioshedArticles();
    this.articles = this.draftService.createTestDraft();
  }

  async selectArticle(article: Article) {
    var readerView = await this.modalController.create({
      component: ArticlereaderComponent,
      componentProps: {
        article: article
      }
    });
    await readerView.present();
  }

  private async loadPublioshedArticles() {
    this.articles = await this.draftService.getPublishedArticles();
  }
}
