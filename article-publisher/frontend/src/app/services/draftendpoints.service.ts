import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {ToastService} from "./toast.service";
import {Article} from "../models/Article";


@Injectable({
  providedIn: 'root'
})
export class DraftendpointsService {
  private baseUrl = environment.baseURL;

  constructor(private http: HttpClient, private toast: ToastService) { }

  createTestDraft(): Article[] {
    console.log('Creating test drafts');
    var drafts: Article[] = [];
    var draft: Article = {
      title: 'What is Lorem Ipsum?',
      content: `What is Lorem Ipsum?
Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.`,
      author: 'Lorem',
      id: 'revival-languages',
      publishedAt: new Date()

    };

    var draft2: Article = {
      title: 'Where can I get Lorem Ipsum?',
      content: `Where can I get some?
There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.`,
      author: 'John Doe',
      id: 'creative-perks',
      publishedAt: new Date()
    };
    drafts.push(draft);
    drafts.push(draft2);
    return drafts;
  }

  // Publish an article
  publishArticle(article: Article): Promise<Article> {
    return this.http.post<Article>(`${this.baseUrl}/publish`, article)
      .toPromise()
      .catch(error => {
        this.toast.showError('Failed to publish article');
        return Promise.reject(error as any);
      }) as Promise<Article>;
  }


  // Save a draft
  safeDraft(article: Article): Promise<Article> {
    return this.http.post<Article>(`${this.baseUrl}/safedraft`, article)
      .toPromise()
      .catch(error => {
        this.toast.showError('Failed to safe article');
        return Promise.reject(error as any);
      }) as Promise<Article>;
  }

  //Get article status
  getArticleStatus(articleId: string): Promise<{ status: string }> {
    return this.http.get<{ status: string }>(`${this.baseUrl}/status/${articleId}`)
      .toPromise()
      .catch(error => {
        this.toast.showError('Failed to get article status');
        return Promise.resolve({ status: 'unknown' });
      })as Promise<{ status: string }>;
  }



  //Get published articles
  getPublishedArticles(): Promise<Article[]> {
    return this.http.get<Article[]>(`${this.baseUrl}/published`)
      .toPromise()
      .catch(error => {
        this.toast.showError('Failed to fetch published articles');
        return Promise.reject(error);
      })as Promise<Article[]>;
  }

  //Get drafts
  getDrafts(): Promise<Article[]> {
    return this.http.get<Article[]>(`${this.baseUrl}/drafts`)
      .toPromise()
      .catch(error => {
        this.toast.showError('Failed to fetch draft articles');
        return Promise.reject(error);
      })as Promise<Article[]>;
  }

}
