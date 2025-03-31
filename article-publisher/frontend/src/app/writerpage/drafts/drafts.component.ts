import { Component, OnInit } from '@angular/core';
import {DraftendpointsService} from "../../services/draftendpoints.service";
import {ModalController} from "@ionic/angular";
import {Article} from "../../models/Article";
import {ArticleeditorComponent} from "../articleeditor/articleeditor.component";
import {Draft} from "../../models/Draft";

@Component({
  selector: 'app-drafts',
  templateUrl: './drafts.component.html',
  styleUrls: ['./drafts.component.scss'],
  standalone: false,
})
export class DraftsComponent implements OnInit {
  drafts: Draft[] = [];

  constructor(
    private draftService: DraftendpointsService,
    private modalController : ModalController
  ) { }

  ngOnInit() {
    //this.loadDrafts();
    this.drafts = this.draftService.createTestDraft();
    console.log(this.drafts);
  }

  private loadDrafts() {
    this.draftService.getDrafts().subscribe((data: Draft[]) => {
      this.drafts = data;
      console.log('Loaded drafts:', this.drafts);
    });
  }

  async selectDraft(draft: Draft) {
    var editorview = await this.modalController.create({
      component: ArticleeditorComponent,
      componentProps: {
        article: draft
      }
    });
    await editorview.present();

    //Wait for the modal to be dismissed
    var {data} = await editorview.onDidDismiss();
    if (data?.updated) {
      this.loadDrafts();    //When modal has been dismissed, reload drafts
    }
  }
}
