import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { HomePage } from './home.page';
import {RouterModule, Routes} from "@angular/router";
import {ArticlereaderComponent} from "./articlereader/articlereader.component";
import {DraftendpointsService} from "../services/draftendpoints.service";
import {HttpClientModule} from "@angular/common/http";

const routes: Routes = [
  { path: '', component: HomePage }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes),
    HttpClientModule
  ],
  declarations: [
    HomePage,
    ArticlereaderComponent
  ],
  providers: [DraftendpointsService]
})
export class HomePageModule {}
