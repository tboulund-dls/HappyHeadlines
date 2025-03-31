import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {DraftsComponent} from "./drafts/drafts.component";
import {IonicModule} from "@ionic/angular";
import {FormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {DraftendpointsService} from "../services/draftendpoints.service";
import {ArticleeditorComponent} from "./articleeditor/articleeditor.component";


const routes: Routes = [
  { path: '', component: DraftsComponent }
];

@NgModule({
  declarations: [
    DraftsComponent,
    ArticleeditorComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes),
    HttpClientModule
  ],
  providers: [DraftendpointsService]
})
export class WriterpageModule { }
