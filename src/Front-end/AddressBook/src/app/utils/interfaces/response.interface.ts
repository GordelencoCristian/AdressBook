import { Message } from "./message.interface";
import { PaginationModel } from "./pagination.interface";

export interface Response<T> {
  items: T;
  pagedSummary: PaginationModel;
  messages: Message[];
  success: boolean;
}

export interface ResponseArray<T> {
  items: T[];
  pagedSummary: PaginationModel;
  messages: Message[];
  success: boolean;
}
