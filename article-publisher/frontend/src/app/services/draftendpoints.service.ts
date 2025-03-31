import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {ToastService} from "./toast.service";
import {Article} from "../models/Article";
import {Draft} from "../models/Draft";
import {Author} from "../models/Author";


@Injectable({
  providedIn: 'root'
})
export class DraftendpointsService {
  private baseUrl = environment.baseURL;

  constructor(private http: HttpClient, private toast: ToastService) { }

  getDrafts(): Observable<Draft[]> {
    return this.http.get<Draft[]>(`${this.baseUrl}/drafts`);
  }

  saveDraft(draft: Draft): Observable<Draft> {
    return this.http.post<Draft>(`${this.baseUrl}/draft`, draft);
  }

  updateDraft(draftId: string, draft: Draft): Observable<Draft> {
    return this.http.put<Draft>(`${this.baseUrl}/draft/${draftId}`, draft);
  }

  deleteDraft(draftId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/draft/${draftId}`);
  }

  publishDraft(draft: Draft): Observable<Article> {
    return this.http.post<Article>(`${this.baseUrl}/publish`, draft);
  }

  getPublishedArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(`${this.baseUrl}/published`);
  }

  updateArticle(articleId: string, article: Article): Observable<Article> {
    return this.http.put<Article>(`${this.baseUrl}/article/${articleId}`, article);
  }

  deleteArticle(articleId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/article/${articleId}`);
  }

  getStatus(articleId: string): Observable<{ status: string }> {
    return this.http.get<{ status: string }>(`${this.baseUrl}/status/${articleId}`);
  }



  createTestDraft(): Article[] {
    console.log('Creating test drafts');

    const author1: Author = {
      name: 'Lorem',
      email: 'lorem@example.com'
    };

    const author2: Author = {
      name: 'John Doe',
      email: 'john.doe@example.com'
    };

    var drafts: Article[] = [];
    var draft: Article = {
      title: 'What is Lorem Ipsum?',
      content: `What is Lorem Ipsum?
Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.`,
      author: author1,
      id: 'revival-languages',
      publishedAt: new Date()

    };

    var draft2: Article = {
      title: 'Where can I get Lorem Ipsum?',
      content: `Where can I get some?
There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.`,
      author: author2,
      id: 'creative-perks',
      publishedAt: new Date()
    };
    drafts.push(draft);
    drafts.push(draft2);
    return drafts;
  }

  // // Publish an article
  // publishArticle(article: Article): Promise<Article> {
  //   return this.http.post<Article>(`${this.baseUrl}/publish`, article)
  //     .toPromise()
  //     .catch(error => {
  //       this.toast.showError('Failed to publish article');
  //       return Promise.reject(error as any);
  //     }) as Promise<Article>;
  // }
  //
  //
  // // Save a draft
  // safeDraft(article: Article): Promise<Article> {
  //   return this.http.post<Article>(`${this.baseUrl}/safedraft`, article)
  //     .toPromise()
  //     .catch(error => {
  //       this.toast.showError('Failed to safe article');
  //       return Promise.reject(error as any);
  //     }) as Promise<Article>;
  // }
  //
  // //Get article status
  // getArticleStatus(articleId: string): Promise<{ status: string }> {
  //   return this.http.get<{ status: string }>(`${this.baseUrl}/status/${articleId}`)
  //     .toPromise()
  //     .catch(error => {
  //       this.toast.showError('Failed to get article status');
  //       return Promise.resolve({ status: 'unknown' });
  //     })as Promise<{ status: string }>;
  // }
  //
  //
  //
  // //Get published articles
  // getPublishedArticles(): Promise<Article[]> {
  //   return this.http.get<Article[]>(`${this.baseUrl}/published`)
  //     .toPromise()
  //     .catch(error => {
  //       this.toast.showError('Failed to fetch published articles');
  //       return Promise.reject(error);
  //     })as Promise<Article[]>;
  // }
  //
  // //Get drafts
  // getDrafts(): Promise<Article[]> {
  //   return this.http.get<Article[]>(`${this.baseUrl}/drafts`)
  //     .toPromise()
  //     .catch(error => {
  //       this.toast.showError('Failed to fetch draft articles');
  //       return Promise.reject(error);
  //     })as Promise<Article[]>;
  // }

}
