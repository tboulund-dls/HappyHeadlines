import {Author} from "./Author";

export interface Draft {
  id?: string;
  title: string;
  content: string;
  author: Author;
}
