import {Article} from "./Article";

export interface Author {
  name: string;
  email: string;
  articles?: Article[];
  comments?: any[];
}
