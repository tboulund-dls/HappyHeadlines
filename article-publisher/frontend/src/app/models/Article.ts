import {Draft} from "./Draft";

export interface Article  extends Draft{
  publishedAt?: Date;
}
