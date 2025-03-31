export interface Article {
  id: string;
  title: string;
  content: string;
  author: string;
  //TODO find the correct data format
  publishedAt?: Date;
}
