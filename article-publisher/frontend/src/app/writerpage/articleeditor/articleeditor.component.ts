import {Component, Input, OnInit} from '@angular/core';
import {ModalController} from "@ionic/angular";
import {DraftendpointsService} from "../../services/draftendpoints.service";
import {Article} from "../../models/Article";

@Component({
  selector: 'app-articleeditor',
  templateUrl: './articleeditor.component.html',
  styleUrls: ['./articleeditor.component.scss'],
  standalone: false,
})
export class ArticleeditorComponent {
  @Input() article!: Article;

  constructor(
    private modalCtrl: ModalController,
    private draftService: DraftendpointsService
  ) {}

  async saveArticleDraft() {
    try {
      await this.draftService.saveDraft(this.article);
      this.modalCtrl.dismiss({ updated: true });  //Close modal and return value to indicate the article was updated
    } catch (error) {
      console.error('Failed to save article', error);
    }
  }


  //Close the modal
  close() {
    this.modalCtrl.dismiss();
  }

}
